using System.Collections.Generic;
using repositoryPattern.Entities;

namespace repositoryPattern.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
           User Find(string email, string password);
    }
}
