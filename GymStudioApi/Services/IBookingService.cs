using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GymStudioApi.Models.Domain;

namespace GymStudioApi.Services
{
    public interface IBookingService
    {
        Task<Booking> CreateBooking(Booking newBooking);
        Task<List<Booking>> GetBookings(Guid classId);
    }
}