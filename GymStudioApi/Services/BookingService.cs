using System;
using System.Threading.Tasks;
using GymStudioApi.Models.Domain;

namespace GymStudioApi.Services
{
    public class BookingService : IBookingService
    {
        public Task<Booking> CreateBooking(Booking newBooking)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetBooking(Guid bookingId)
        {
            throw new NotImplementedException();
        }
    }
}