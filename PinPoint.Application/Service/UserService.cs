using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PinPoint.Application.ApplicationException;
using PinPoint.Application.Interface;
using PinPoint.Core.UnitOfWork.Base;
using PinPoint.Data.Domain;
using PinPoint.DataAccess.Helpers;
using PinPoint.Infrastructure.MapperService.Models.User;
using PinPoint.Infrastructure.Response;
using PinPoint.Infrastructure.UnitOfWork.Base;
using System.Collections.Generic;

namespace PinPoint.Application.Service
{
    public class UserService : ControllerBase, IUserService<User>
    {
        public DataContext _dataContext;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public UserService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _uow = new UnitOfWork(_dataContext);
            _mapper = mapper;
        }

        public async Task<IActionResult> GetUserListAsync()
        {
            IEnumerable<GetUserDTO> _user = _mapper.Map<List<GetUserDTO>>(await _uow.userRepository.GetAllAsync());
            if (!_user.Any())
                return NotFound("No records found.");

            
            return Ok(_user.ToList());
        }
        public async Task<ServiceResponse<GetUserDTO>> GetByIdUserAsync(Guid id)
        {
            ServiceResponse<GetUserDTO> _serviceResponse = new ServiceResponse<GetUserDTO>();
            var _user = await _uow.userRepository.GetByIDAsync(id);
            var results = _uow.fluentValidationUser.GetByIdRules().Validate(_user);
            if (!results.IsValid)
            {
                List<Error> _errorObj = new List<Error>();
                foreach (var ValidateItem in results.Errors)
                {
                    _errorObj.Add(new Error() { errorCode = ValidateItem.ErrorCode, propertyName = ValidateItem.PropertyName + " (" + ValidateItem.AttemptedValue + ") ", errorMessage = ValidateItem.ErrorMessage });
                }
                _serviceResponse.error = _errorObj;
                _serviceResponse.statusCode = StatusCodes.Status400BadRequest;
                return _serviceResponse;
            }
            _serviceResponse.Entity = _mapper.Map<List<GetUserDTO>>(_mapper.Map<GetUserDTO>(_user));
            _serviceResponse.statusCode = StatusCodes.Status200OK;
            return _serviceResponse;
        }
        public async Task<ServiceResponse<GetUserDTO>> PostUserAsync(PostUserDTO postUserDTO)
        {
            ServiceResponse<GetUserDTO> _serviceResponse = new ServiceResponse<GetUserDTO>();
            var _user = _mapper.Map<User>(postUserDTO);
            var results = _uow.fluentValidationUser.PostRules().Validate(_user);
            if (!results.IsValid)
            {
                List<Error> _errorObj = new List<Error>();
                foreach (var ValidateItem in results.Errors)
                {
                    _errorObj.Add(new Error() { errorCode = ValidateItem.ErrorCode, propertyName = ValidateItem.PropertyName + " (" + ValidateItem.AttemptedValue + ") ", errorMessage = ValidateItem.ErrorMessage });
                }
                _serviceResponse.error = _errorObj;
                _serviceResponse.statusCode = StatusCodes.Status400BadRequest;
                return _serviceResponse;
            }
            _user = await _uow.userRepository.AddAsync(_user);
            _uow.Complete();
            _serviceResponse.Entity = _mapper.Map<List<GetUserDTO>>(_mapper.Map<GetUserDTO>(_user));
            _serviceResponse.statusCode = StatusCodes.Status200OK;
            return _serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDTO>> PostBulkUserAsync(IEnumerable<PostUserDTO> postUserDTO)
        {
            ServiceResponse<GetUserDTO> _serviceResponse = new ServiceResponse<GetUserDTO>();
            List<Error> _errorObj = new List<Error>();
            foreach (var postUserDTOItem in postUserDTO)
            {
                var _user = _mapper.Map<User>(postUserDTOItem);
                var results = _uow.fluentValidationUser.PostRules().Validate(_user);
                if (!results.IsValid)
                {
                    foreach (var ValidateItem in results.Errors)
                    {
                        _errorObj.Add(new Error() { errorCode = ValidateItem.ErrorCode, propertyName = ValidateItem.PropertyName + " (" + ValidateItem.AttemptedValue + ") ", errorMessage = ValidateItem.ErrorMessage });
                    }
                    _serviceResponse.error = _errorObj;
                    _serviceResponse.statusCode = StatusCodes.Status400BadRequest;
                    return _serviceResponse;
                }
            }

            var getUserDTO = new List<GetUserDTO>();
            foreach (var postUserDTOItem in postUserDTO)
            {
                var _user = await _uow.userRepository.AddAsync(_mapper.Map<User>(postUserDTOItem));
                _uow.Complete();
                getUserDTO.Add(_mapper.Map<GetUserDTO>(_user));

            }
            _serviceResponse.Entity = getUserDTO;
            _serviceResponse.statusCode = StatusCodes.Status200OK;
            return _serviceResponse;
        }
    }
}
