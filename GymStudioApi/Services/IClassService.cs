using System;
using System.Threading.Tasks;
using GymStudioApi.Models.Domain;

namespace GymStudioApi.Services
{
    public interface IClassService
    {
        Task<Class> CreateClass(Class classRequest);
        bool DeleteClass(Guid classId);
    }
}