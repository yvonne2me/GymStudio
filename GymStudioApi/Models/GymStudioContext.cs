using GymStudioApi.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymStudioApi.Models
{
    public class GymStudioContext : DbContext
    {
         public GymStudioContext(DbContextOptions<GymStudioContext> options) 
            : base(options){}  

        public DbSet<Class> Classes { get; set; }

        public DbSet<ClassSession> ClassSessions { get; set; }

        public DbSet<Booking> Bookings { get; set; }
            
    }
}