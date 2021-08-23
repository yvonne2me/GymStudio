using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymStudioApi.Logging;
using GymStudioApi.Models;
using GymStudioApi.Models.Domain;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Class> GetClassById(Guid classId)
        {
            return await _context.Classes.Where(c => c.Id.Equals(classId)).FirstOrDefaultAsync();
        }

        public async Task<Class> GetClassByName(string className)
        {
            return await _context.Classes.Where(c => c.ClassName.Equals(className)).FirstOrDefaultAsync();
        }

        public async Task<Class> SaveClass(Class newClass)
        {
            CreateClassSessions(newClass);

            _context.Classes.Add(newClass);

            if(await _context.SaveChangesAsync() > 0)
            {
                return newClass;
            }
            else
            {
                logger.LogError("ClassRepository - SaveClass - Unable to Save Class");
                throw new Exception("Error saving new Class");
            }
        }

        public async Task<List<ClassSession>> GetAllClassSessions(Guid classId)
        {
            return await _context.ClassSessions.Where(c => c.ClassId.Equals(classId)).ToListAsync();
        }

        public async Task<ClassSession> GetClassSessionsByDate(Guid classId, DateTime date)
        {
            return await _context.ClassSessions.Where(c => c.ClassId.Equals(classId) && c.ClassDate.Date.Equals(date.Date)).FirstOrDefaultAsync();
        }

        private void CreateClassSessions(Class newClass)
        {
            var numberOfClassSessions = GetCountOfClassSessions(newClass.Start_Date, newClass.End_Date);

            for(var i=0; i < numberOfClassSessions; i++)
            {
                ClassSession classSession = new ClassSession
                {
                    Id = Guid.NewGuid(),
                    ClassId = newClass.Id,
                    ClassName = newClass.ClassName,
                    ClassDate = newClass.Start_Date.AddDays(i).Date,
                    Capacity = newClass.Capacity
                };

                _context.ClassSessions.Add(classSession);
            }
        }

        private int GetCountOfClassSessions(DateTime startDate, DateTime endDate)
        {
            return (endDate.Date - startDate.Date).Days;
        }
    }
}