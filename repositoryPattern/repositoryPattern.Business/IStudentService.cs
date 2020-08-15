using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using repositoryPattern.Entities;

namespace repositoryPattern.Business
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAll();

        IEnumerable<Student> GetAllFromStudentRepository();

        bool Add(Student entity);
         bool Update(int id, Student entity);
        bool Delete(int id);
    }
}
