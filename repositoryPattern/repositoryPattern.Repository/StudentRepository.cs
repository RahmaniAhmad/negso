using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using repositoryPattern.Data;
using repositoryPattern.Entities;

namespace repositoryPattern.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationContext _context;

        public StudentRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }
    }
}