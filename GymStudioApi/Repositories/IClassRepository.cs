using System;
using System.Threading.Tasks;
using GymStudioApi.Models.Domain;

namespace GymStudioApi.Repositories
{
    public interface IClassRepository
    {
        Task<bool> SaveClass(Class newClass);
        Task<Class> GetClass(Guid classId);
    }
}