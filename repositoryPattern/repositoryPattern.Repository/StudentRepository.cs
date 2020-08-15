using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using repositoryPattern.Entities;
using repositoryPattern.Data;

namespace repositoryPattern.Repository
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        private readonly ApplicationContext _dbContext;

        public StudentRepository(ApplicationContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Student> GetAllStudent()
        {
           return _dbContext.Students.ToList();
        }
    }
}
