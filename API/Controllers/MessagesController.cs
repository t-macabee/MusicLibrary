﻿using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //[Authorize]
    public class MessagesController : BaseAPIController
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public MessagesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.GetUsername();

            if (username == createMessageDto.RecipientUsername.ToLower())
                return BadRequest("You cannot send messages to yourself!");

            var sender = await unitOfWork.UserRepository.GetUserByUsername(username);

            var recipient = await unitOfWork.UserRepository.GetUserByUsername(createMessageDto.RecipientUsername);

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

            unitOfWork.MessageRepository.AddMessage(message);

            if(await unitOfWork.Complete())
                return Ok(mapper.Map<MessageDto>(message));

            return BadRequest("Failed to send message");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery]MessageParams messageParams)
        {
            var user = await unitOfWork.UserRepository.GetUserByUsername(User.GetUsername());

            messageParams.Username = user.UserName;

            var messages = await unitOfWork.MessageRepository.GetMessageForUser(messageParams);

            Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPage);

            return Ok(messages);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var user = await unitOfWork.UserRepository.GetUserByUsername(User.GetUsername());

            var message = await unitOfWork.MessageRepository.GetMessage(id);

            if(message.Sender.UserName != user.UserName && message.Recipient.UserName != user.UserName) 
                return Unauthorized();

            if (message.Sender.UserName == user.UserName)
                message.SenderDeleted = true;

            if (message.Recipient.UserName == user.UserName)
                message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted)
                unitOfWork.MessageRepository.DeleteMessage(message);

            if (await unitOfWork.Complete())
                return Ok();

            return BadRequest("Problem deleting the message");
        }

    }
}
