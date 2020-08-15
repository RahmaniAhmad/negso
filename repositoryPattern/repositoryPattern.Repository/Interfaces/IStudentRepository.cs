using System.Collections.Generic;
using repositoryPattern.Entities;

namespace repositoryPattern.Repository
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        IEnumerable<Student> GetAllStudent();
    }
}
