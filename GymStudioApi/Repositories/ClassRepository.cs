using System;
using System.Threading.Tasks;
using GymStudioApi.Logging;
using GymStudioApi.Models;
using GymStudioApi.Models.Domain;

namespace GymStudioApi.Repositories
{
    public class ClassRepository : IClassRepository
    {
        GymStudioContext _context;
        IFileLogger logger;

        public ClassRepository(IFileLogger logger, GymStudioContext context)
        {
            _context = context;
            this.logger = logger;
        }
        public Task<Class> GetClass(Guid classId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveClass(Class newClass)
        {
            _context.Classes.Add(newClass);

            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            else
            {
                logger.LogError("ClassRepository - SaveClass - Unable to Save Class");
                throw new Exception("Error saving new Class");
            }
        }
    }
}