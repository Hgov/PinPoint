using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PinPoint.Application.ApplicationException;
using PinPoint.Application.Interface;
using PinPoint.Core.Data;
using PinPoint.Core.UnitOfWork.Base;
using PinPoint.Data.Domain;
using PinPoint.DataAccess.Helpers;
using PinPoint.Infrastructure.MapperService.Models.User;
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
        public async Task<IActionResult> GetByIdUserAsync(Guid id)
        {
            var _user = await _uow.userRepository.GetByIDAsync(id);
            if (_user == null)
                return NotFound("No records found.");
            return Ok(_mapper.Map<List<GetUserDTO>>(_mapper.Map<GetUserDTO>(_user)));
        }
        public async Task<IActionResult> PostUserAsync(PostUserDTO postUserDTO)
        {
            var _user = _mapper.Map<User>(postUserDTO);
            var results = _uow.fluentValidationUser.PostRules().Validate(_user);
            if (!results.IsValid)
            {
                List<ValidationError> _errorObj = new List<ValidationError>();
                foreach (var ValidateItem in results.Errors)
                {
                    _errorObj.Add(new ValidationError() { errorCode = ValidateItem.ErrorCode, propertyName = ValidateItem.PropertyName + " (" + ValidateItem.AttemptedValue + ") ", errorMessage = ValidateItem.ErrorMessage });
                }
                return BadRequest(_errorObj);
            }
            _user = await _uow.userRepository.AddAsync(_user);
            _uow.Complete();
            return Ok(_mapper.Map<List<GetUserDTO>>(_mapper.Map<GetUserDTO>(_user)));
        }
         
        public async Task<IActionResult> PostBulkUserAsync(IEnumerable<PostUserDTO> postUserDTO)
        {
            List<ValidationError> _errorObj = new List<ValidationError>();
            foreach (var postUserDTOItem in postUserDTO)
            {
                var _user = _mapper.Map<User>(postUserDTOItem);
                var results = _uow.fluentValidationUser.PostRules().Validate(_user);
                if (!results.IsValid)
                {
                    foreach (var ValidateItem in results.Errors)
                    {
                        _errorObj.Add(new ValidationError() { errorCode = ValidateItem.ErrorCode, propertyName = ValidateItem.PropertyName + " (" + ValidateItem.AttemptedValue + ") ", errorMessage = ValidateItem.ErrorMessage });
                    }
                    return BadRequest(_errorObj);
                }
            }
            var bulkPostTogetUserDTO = new List<GetUserDTO>();
            foreach (var postUserDTOItem in postUserDTO)
            {
                var _user = await _uow.userRepository.AddAsync(_mapper.Map<User>(postUserDTOItem));
                _uow.Complete();
                bulkPostTogetUserDTO.Add(_mapper.Map<GetUserDTO>(_user));
            }
            return Ok(bulkPostTogetUserDTO);
        }
    }
}
