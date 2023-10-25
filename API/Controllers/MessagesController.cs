using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //[Authorize]
    public class MessagesController : BaseAPIController
    {
        private IUserRepository userRepository {  get; set; }
        private IMessageRepository messageRepository {  get; set; }
        private IMapper mapper { get; set; }

        public MessagesController(IUserRepository userRepository, IMessageRepository messageRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.messageRepository = messageRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.GetUsername();

            if (username == createMessageDto.RecipientUsername.ToLower())
                return BadRequest("You cannot send messages to yourself!");

            var sender = await userRepository.GetUserByUsernameAsync(username);

            var recipient = await userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

            if (recipient == null)
                return NotFound();

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessageDto.Content
            };

            messageRepository.AddMessage(message);

            if(await messageRepository.SaveAllAsync())
                return Ok(mapper.Map<MessageDto>(message));

            return BadRequest("Failed to send message");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery]MessageParams messageParams)
        {
            var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());

            messageParams.Username = user.UserName;

            var messages = await messageRepository.GetMessageForUser(messageParams);

            Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPage);

            return Ok(messages);
        }

        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        {
            var currentUsername = User.GetUsername();
            
            return Ok(await messageRepository.GetMessageThread(currentUsername, username));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());

            var message = await messageRepository.GetMessage(id);

            if(message.Sender.UserName != user.UserName && message.Recipient.UserName != user.UserName) 
                return Unauthorized();

            if (message.Sender.UserName == user.UserName)
                message.SenderDeleted = true;

            if (message.Recipient.UserName == user.UserName)
                message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted)
                messageRepository.DeleteMessage(message);

            if (await messageRepository.SaveAllAsync())
                return Ok();

            return BadRequest("Problem deleting the message");
        }

    }
}
