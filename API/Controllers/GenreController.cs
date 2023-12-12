using API.Data;
using API.DTO;
using API.DTOs;
using API.DTOs.UpdateDTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class GenreController : BaseAPIController
    {
        private DataContext context;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;

        public GenreController(DataContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.context = context;            
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<GenreDto>> GetGenres()
        {
            var result = await unitOfWork.GenreRepository.GetAllGenres();
            
            return Ok(result);
        }           
    }
}
