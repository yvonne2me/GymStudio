using System;
using GymStudioApi.Logging;
using GymStudioApi.Models.API;
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

        public ClassController(IFileLogger logger, IClassService classService)
        {
            this.logger = logger;
            this.classService = classService;
        }
        
        [HttpPost]
        [Produces("application/json")]
        public IActionResult Post([FromBody] ClassRequest classRequest)
        {
            if(classRequest == null)
            {
                logger.LogInfo("Class info null - Returning Bad Request");
                return BadRequest("No Class information provided");
            }

            var response = new ClassResponse();
            try
            {
                response = classService.CreateClass(classRequest);  
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(response);
        }
    }
}