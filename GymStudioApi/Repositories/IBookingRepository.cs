using System.Threading.Tasks;
using GymStudioApi.Models.Domain;

namespace GymStudioApi.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking> SaveBooking(Booking newBooking);
    }
}