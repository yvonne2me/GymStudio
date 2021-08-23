
using System;
using System.Threading.Tasks;
using AutoMapper;
using GymStudioApi.Exceptions;
using GymStudioApi.Logging;
using GymStudioApi.Models.API;
using GymStudioApi.Models.Domain;
using GymStudioApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymStudioApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : ControllerBase
    {
        IFileLogger logger;
        IBookingService bookingService;
        IMapper mapper;

        public BookingsController(IFileLogger logger, IBookingService bookingService, IMapper mapper)
        {
            this.logger = logger;
            this.bookingService = bookingService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] BookingRequest bookingRequest)
        {
            if(bookingRequest == null)
            {
                logger.LogInfo("Booking Request info null - Returning Bad Request");
                return BadRequest("No Booking information provided");
            }

            var createBookingRequest = this.mapper.Map<Booking>(bookingRequest);

            Booking response = null;

            try
            {
                response = await bookingService.CreateBooking(createBookingRequest);
            }
            catch(ClassNotFoundException classNotFoundException)
            {
                return NotFound(classNotFoundException.Message);
            }
            catch(ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }
            catch(Exception)
            {
                throw new Exception("Error occurred while saving Booking");
            }

            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(Guid classId)
        {
            var response = await bookingService.GetBookings(classId);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}