using System;
using System.Threading.Tasks;
using GymStudioApi.Models.Domain;

namespace GymStudioApi.Services
{
    public interface IBookingService
    {
        Task<Booking> CreateBooking(Booking newBooking);
        Task<Booking> GetBooking(Guid bookingId);
    }
}