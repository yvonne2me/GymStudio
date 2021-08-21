using System;
using System.Threading.Tasks;
using GymStudioApi.Logging;
using GymStudioApi.Models;
using GymStudioApi.Models.Domain;

namespace GymStudioApi.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        GymStudioContext _context;
        IFileLogger logger;

        public BookingRepository(IFileLogger logger, GymStudioContext context)
        {
            this.logger = logger;
            _context = context;
        }

        public async Task<Booking> SaveBooking(Booking newBooking)
        {
            _context.Bookings.Add(newBooking);

            if(await _context.SaveChangesAsync() > 0)
            {
                return newBooking;
            }
            else
            {
                logger.LogError("BookingRepository - SaveBooking - Unable to Save Booking");
                throw new Exception("Error saving new Booking");
            }
        }
    }
}