﻿
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using PinkPoint.Application.ApplicationException;
using PinkPoint.Application.Interface;
using PinkPoint.Core.Interfaces;
using PinkPoint.Core.UnitOfWork.Base;
using PinkPoint.Data.Domain;
using PinkPoint.DataAccess.Helpers;
using PinkPoint.FluentValidation.AbstractValidation;
using PinkPoint.Infrastructure.Logging;
using PinkPoint.Infrastructure.UnitOfWork.Base;
using System.ComponentModel.DataAnnotations;

namespace PinkPoint.Application.Service
{
    public class UserService:IUserService<User>
    {
        private readonly IUnitOfWork _uow;
        private readonly UserValidator _userValidator;
        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
            _uow = new UnitOfWork(_dataContext);
            _userValidator = new UserValidator();
        }

        public async Task<IEnumerable<User>> GetUserListAsync()
        {
            var _user = await _uow.userRepository.GetAllAsync();
            return _user;
        }
        public async Task<User> GetByIdUserAsync(Guid id)
        {
            var _user = await _uow.userRepository.GetByIDAsync(id);
            return _user;
        }
        public async Task<User> PostUserAsync(User user)
        {
            _userValidator.Validate(user, options =>
            {
                options.ThrowOnFailures();
            });
            var _user=await _uow.userRepository.AddAsync(user);
            _uow.Complete();
            return _user;
        }

        public async Task<User> PutUserAsync(User user)
        {
            _userValidator.Validate(user, options =>
            {
                options.ThrowOnFailures();
            });


            throw new NotImplementedException();
        }
    }
}
