﻿using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class MessageHub : Hub
    {
        private IHubContext<PresenceHub> presenceHub;
        private IMessageRepository messageRepository;
        private IUserRepository userRepository;
        private IMapper mapper;
        private PresenceTracker tracker;

        public MessageHub(IMessageRepository messageRepository, IUserRepository userRepository, IMapper mapper, IHubContext<PresenceHub> presenceHub, PresenceTracker tracker) 
        {
            this.presenceHub = presenceHub;
            this.messageRepository = messageRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.tracker = tracker;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            var otherUser = httpContext.Request.Query["user"].ToString();

            var groupName = GetGroupName(Context.User.GetUsername(), otherUser);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var group = await AddToGroup(groupName);

            await Clients.Group(groupName).SendAsync("UpdatedGroup", group);

            var messages = await messageRepository.GetMessageThread(Context.User.GetUsername(), otherUser);

            await Clients.Caller.SendAsync("RecieveMessageThread", messages);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var group = await RemoveFromMessageGroup();

            await Clients.Group(group.Name).SendAsync("UpdatedGroup", group);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(CreateMessageDto createMessageDto)
        {
            var username = Context.User.GetUsername();

            if (username == createMessageDto.RecipientUsername.ToLower())
            {
                throw new HubException("You cannot send messages to yourself!");                
            }

            var sender = await userRepository.GetUserByUsernameAsync(username);

            var recipient = await userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

            if (recipient == null)
            { 
                throw new HubException("User not found");            
            }

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessageDto.Content
            };

            var groupName = GetGroupName(sender.UserName, recipient.UserName);

            var group = await messageRepository.GetMessageGroup(groupName);

            if(group.Connections.Any(x => x.UserName == recipient.UserName))
            {
                message.DateRead = DateTime.UtcNow;
            }
            else
            {
                var connections = await tracker.GetConnectionsForUser(recipient.UserName);
                if(connections != null)
                {
                    await presenceHub.Clients.Clients(connections).SendAsync("NewMessageRecieved",
                        new { username = sender.UserName, knownAs = sender.KnownAs });
                }
            }

            messageRepository.AddMessage(message);

            if (await messageRepository.SaveAllAsync())
            {                
                await Clients.Group(groupName).SendAsync("NewMessage", mapper.Map<MessageDto>(message));
            }            
        }

        private async Task<Group> AddToGroup(string groupName)
        {
            var group = await messageRepository.GetMessageGroup(groupName);

            var connection = new Connection(Context.ConnectionId, Context.User.GetUsername());

            if(group == null)
            {
                group = new Group(groupName);
                messageRepository.AddGroup(group);
            }

            group.Connections.Add(connection);

            if(await messageRepository.SaveAllAsync()) return group;

            throw new HubException("Failed to join group");

        }

        private async Task<Group> RemoveFromMessageGroup()
        {
            var group = await messageRepository.GetGroupForConnection(Context.ConnectionId);

            var connection = group.Connections.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

            messageRepository.RemoveConnection(connection);

            if (await messageRepository.SaveAllAsync()) return group;

            throw new HubException("Failed to remove from group");
        }

        private string GetGroupName(string caller, string other)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }
    }
}
