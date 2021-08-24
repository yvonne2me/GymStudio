using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GymStudioApi.Models.API;
using GymStudioApi.Models.Domain;
using GymStudioApi.Repositories;

namespace GymStudioApi.Services
{
    public class ClassService : IClassService
    {
        IClassRepository classRepository;

        public ClassService(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }

        public async Task<Class> CreateClass(Class newClass)
        {
            CheckClassRequestInfo(newClass);

            newClass.Id = Guid.NewGuid();

            await CheckIfClassAlreadyExists(newClass);

            return await this.classRepository.SaveClass(newClass);
        }

        public async Task<Class> GetClass(Guid classId)
        {
            return await this.classRepository.GetClassById(classId);
        }

        public async Task<ClassSession> GetClassSessionsByDate(Guid classId, DateTime date)
        {
            return await this.classRepository.GetClassSessionsByDate(classId, date);
        }


        private static void CheckClassRequestInfo(Class classRequest)
        {
            if(classRequest.ClassName == null)
            {
                throw new ArgumentException("A Class Name is required to create classes");
            }

            if(classRequest.Start_Date.Date > classRequest.End_Date.Date)
            {
                throw new ArgumentException("Start_Date occurs after End_Date");
            }

            if(classRequest.Start_Date.Date < DateTime.UtcNow.Date)
            {
                throw new ArgumentException("Start_Date provided is historical or not current.");
            }

            //Limit on Classes to 30 Days to prevent large volume of ClassSessions being created
            if((classRequest.End_Date.Date - classRequest.Start_Date.Date).Days > 30)
            {
                throw new ArgumentException("Classes would be spanning more than 30 days - Limit Reached");
            }

            if(classRequest.Id != Guid.Empty)
            {
                throw new ArgumentException("Id field should not be provided.");
            }
        }

        private async Task CheckIfClassAlreadyExists(Class newClass)
        {
            var classWithSameName = await this.classRepository.GetClassByName(newClass.ClassName);
            var classWithSameId = await this.classRepository.GetClassById(newClass.Id);

            if(classWithSameName != null || classWithSameId != null)
            {
                throw new ArgumentException("Class already exists - Please review the details that you provided.");
            } 
        }
    }
}