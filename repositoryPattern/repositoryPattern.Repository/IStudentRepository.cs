using System.Collections.Generic;
using repositoryPattern.Entities;

namespace repositoryPattern.Repository
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAllStudents();
    }
}