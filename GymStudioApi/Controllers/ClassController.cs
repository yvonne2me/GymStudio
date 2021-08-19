﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using GymStudioApi.Logging;
using GymStudioApi.Models.API;
using GymStudioApi.Models.Domain;
using GymStudioApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymStudioApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassController : ControllerBase
    {
        IFileLogger logger;
        IClassService classService;
        IMapper mapper;

        public ClassController(IFileLogger logger, IClassService classService, IMapper mapper)
        {
            this.logger = logger;
            this.classService = classService;
            this.mapper = mapper;
        }
        
        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] ClassRequest classRequest)
        {
            if(classRequest == null)
            {
                logger.LogInfo("Class info null - Returning Bad Request");
                return BadRequest("No Class information provided");
            }

            var createClassRequest = this.mapper.Map<Class>(classRequest);

            Class response = null;

            try
            {
                response = await classService.CreateClass(createClassRequest);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(Guid classId)
        {
            //Validate classId
            //Call ClassService
            //return Class

            return Ok(new Class(){Id = classId, ClassName = "New Class Name"});
        }
    }
}