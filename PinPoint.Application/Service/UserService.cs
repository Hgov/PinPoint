using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PinPoint.Application.Interface;
using PinPoint.Core.FluentValidation;
using PinPoint.Core.LoggerManager;
using PinPoint.Core.UnitOfWork.Base;
using PinPoint.Data.Domain;
using PinPoint.DataAccess.Helpers;
using PinPoint.FluentValidation.AbstractValidation;
using PinPoint.Infrastructure.MapperService.Models.User;
using PinPoint.Infrastructure.Response;
using PinPoint.Infrastructure.UnitOfWork.Base;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace PinPoint.Application.Service
{
    public class UserService : IUserService<User>
    {
        public DataContext _dataContext;
        private readonly IUnitOfWork _uow;
        private readonly IFluentValidation<User> _userValidator;
        private readonly IMapper _mapper;
        public UserService(DataContext dataContext,IMapper mapper)
        {
            _dataContext = dataContext;
            _uow = new UnitOfWork(_dataContext);
            _userValidator = new UserValidator(_dataContext);
            _mapper= mapper;
        }

        public async Task<ServiceResponse<GetUserDTO>> GetUserListAsync()
        {
            ServiceResponse<GetUserDTO> _serviceResponse=new ServiceResponse<GetUserDTO>(); 
            _uow.loggerManager.LogInfo("Test"+DateTime.Now);
            var _user = _mapper.Map<List<GetUserDTO>>(await _uow.userRepository.GetAllAsync());
            if (!_user.Any())
            {
                List<Error> _errorObj = new List<Error>();
                _errorObj.Add(new Error() { errorMessage = "No records found." });
                _serviceResponse.error = _errorObj;
                _serviceResponse.statusCode = StatusCodes.Status400BadRequest;
                return _serviceResponse;
            }
            _serviceResponse.Entity= _user;
            _serviceResponse.statusCode = StatusCodes.Status200OK;
            return _serviceResponse;
        }
        public async Task<ServiceResponse<GetUserDTO>> GetByIdUserAsync(Guid id)
        {
            ServiceResponse<GetUserDTO> _serviceResponse = new ServiceResponse<GetUserDTO>();
            var _user = await _uow.userRepository.GetByIDAsync(id);
            var results = _userValidator.GetByIdRules().Validate(_user);
            if (!results.IsValid)
            {
                List<Error> _errorObj = new List<Error>();
                foreach (var item in results.Errors)
                {
                    _errorObj.Add(new Error() { errorCode = item.ErrorCode, propertyName = item.PropertyName, errorMessage = item.ErrorMessage });
                }
                _serviceResponse.error = _errorObj;
                _serviceResponse.statusCode = StatusCodes.Status400BadRequest;
                return _serviceResponse;
            }
            _serviceResponse.Entity= _mapper.Map<List<GetUserDTO>>(_mapper.Map<GetUserDTO>(_user));
            _serviceResponse.statusCode = StatusCodes.Status200OK;
            return _serviceResponse;
        }
        public async Task<ServiceResponse<GetUserDTO>> PostUserAsync(PostUserDTO postUserDTO)
        {
            ServiceResponse<GetUserDTO> _serviceResponse = new ServiceResponse<GetUserDTO>();
            var _user = _mapper.Map<User>(postUserDTO);
            var results = _userValidator.PostRules().Validate(_user);
            if (!results.IsValid)
            {
                List<Error> _errorObj = new List<Error>();
                foreach (var item in results.Errors)
                {
                    _errorObj.Add(new Error() { errorCode = item.ErrorCode, propertyName = item.PropertyName, errorMessage = item.ErrorMessage });
                }
                _serviceResponse.error = _errorObj;
                _serviceResponse.statusCode = StatusCodes.Status400BadRequest;
                return _serviceResponse;
            }
            _user = await _uow.userRepository.AddAsync(_user);
            _uow.Complete();
            _serviceResponse.Entity= _mapper.Map<List<GetUserDTO>>(_mapper.Map<GetUserDTO>(_user));
            _serviceResponse.statusCode = StatusCodes.Status200OK;
            return _serviceResponse;
        }

        public Task<User> PutUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
