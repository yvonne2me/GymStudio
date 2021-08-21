using System;
using System.Threading.Tasks;
using GymStudioApi.Models.Domain;

namespace GymStudioApi.Services
{
    public interface IClassService
    {
        Task<Class> CreateClass(Class newClass);
        Task<Class> GetClass(Guid classId);
        bool DeleteClass(Guid classId);
    }
}