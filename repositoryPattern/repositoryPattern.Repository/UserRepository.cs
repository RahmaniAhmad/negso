using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;
using repositoryPattern.Entities;
using repositoryPattern.Data;

namespace repositoryPattern.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
       public User Find(string email, string password)
        {
            return _dbContext.Users.FirstOrDefault(w=>w.Email==email&&w.Password==password);
        }
    }
}
