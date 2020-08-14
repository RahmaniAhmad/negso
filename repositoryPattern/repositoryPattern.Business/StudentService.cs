using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using repositoryPattern.Repository;
using repositoryPattern.Entities;

namespace repositoryPattern.Business
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _context;

        public StudentService(IStudentRepository context)
        {
            _context = context;
        }

        public  IEnumerable<Student> GetAll()
        {
            return _context.GetAllStudents();
        }
    }
}
