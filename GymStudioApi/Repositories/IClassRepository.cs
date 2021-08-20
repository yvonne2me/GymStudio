using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GymStudioApi.Models.Domain;

namespace GymStudioApi.Repositories
{
    public interface IClassRepository
    {
        Task<Class> SaveClass(Class newClass);
        Task<Class> GetClass(Guid classId);
        Task<List<ClassSession>> GetClassSessions(Guid classId);
    }
}