﻿using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace API.Controllers
{
    public class UsersController : BaseAPIController
    {
        private IUserRepository repository;
        private IMapper mapper;

        public UsersController(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers() 
        {
            var users = await repository.GetUsersAsync();

            var usersToReturn = mapper.Map<IEnumerable<MemberDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await repository.GetUserByUsername(username);
            return mapper.Map<MemberDto>(user);            
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await repository.GetUserByUsername(username);

            mapper.Map(memberUpdateDto, user);

            repository.Update(user);

            if (await repository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }
    }
}
