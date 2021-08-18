using System;
using GymStudioApi.Models.API;

namespace GymStudioApi.Services
{
    public interface IClassService
    {
        ClassResponse CreateClass(ClassRequest gymClassRequest);
        bool DeleteClass(Guid classId);
    }
}