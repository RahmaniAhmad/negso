using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using repositoryPattern.Data;
using repositoryPattern.Entities;
using repositoryPattern.Repository;

namespace repositoryPattern.Business
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public User Find(string email, string password)
        {
            return _unitOfWork.UserRepository.Find(email,password);
        }
    }
}
