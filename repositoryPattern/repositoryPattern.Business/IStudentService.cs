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

    }
}
