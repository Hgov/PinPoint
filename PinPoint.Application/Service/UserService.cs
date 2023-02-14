using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Identity.Client;
using NLog.LayoutRenderers;
using PinPoint.Application.ApplicationException;
using PinPoint.Application.Interface;
using PinPoint.Core.Data;
using PinPoint.Core.UnitOfWork.Base;
using PinPoint.Data.Domain;
using PinPoint.DataAccess.Helpers;
using PinPoint.Infrastructure.MapperService.Models.User;
using PinPoint.Infrastructure.UnitOfWork.Base;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

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
            try
            {
                IEnumerable<GetUserDTO> _user = _mapper.Map<List<GetUserDTO>>(await _uow.userRepository.GetAllAsync());
                if (!_user.Any())
                    return NotFound("No records found.");

                return Ok(_user.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public async Task<IActionResult> GetByIdUserAsync(Guid id)
        {
            try
            {
                var _user = await _uow.userRepository.GetByIDAsync(id);
                if (_user == null)
                    return NotFound("No records found.");
                return Ok(_mapper.Map<List<GetUserDTO>>(_mapper.Map<GetUserDTO>(_user)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public async Task<IActionResult> PostUserAsync(PostUserDTO postUserDTO)
        {
            try
            {
                var _user = _mapper.Map<User>(postUserDTO);
                var results = _uow.fluentValidationUser.PostValidationRules().Validate(_user);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public async Task<IActionResult> PostBulkUserAsync(IEnumerable<PostUserDTO> postUserDTO)
        {
            try
            {
                List<ValidationError> _errorObj = new List<ValidationError>();
                foreach (var postUserDTOItem in postUserDTO)
                {
                    var _user = _mapper.Map<User>(postUserDTOItem);
                    var results = _uow.fluentValidationUser.PostValidationRules().Validate(_user);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public async Task<IActionResult> PutUserAsync(Guid id, PutUserDTO putUserDTO)
        {
            try
            {
                var _user = _mapper.Map<User>(putUserDTO);
                User newData = await _uow.fluentValidationUser.PutCompareRulesAsync(id, _user);
                if (newData != null)
                {
                    var results = _uow.fluentValidationUser.PutValidationRules().Validate(newData);
                    if (!results.IsValid)
                    {
                        List<ValidationError> _errorObj = new List<ValidationError>();
                        foreach (var ValidateItem in results.Errors)
                        {
                            _errorObj.Add(new ValidationError() { errorCode = ValidateItem.ErrorCode, propertyName = ValidateItem.PropertyName + " (" + ValidateItem.AttemptedValue + ") ", errorMessage = ValidateItem.ErrorMessage });
                        }
                        return BadRequest(_errorObj);
                    }

                    var dd = _uow.userRepository.Update(newData);
                    _uow.Complete();
                    return Ok(_mapper.Map<List<GetUserDTO>>(_mapper.Map<GetUserDTO>(newData)));
                }
                else
                    return NotFound("No records found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public async Task<IActionResult> DeleteByIdUserAsync(Guid id)
        {
            try
            {
                var _user = await _uow.userRepository.GetByIDAsync(id);
                if (_user == null)
                    return NotFound("No records found.");

                _uow.userRepository.Remove(_user);
                _uow.Complete();
                return Ok(_mapper.Map<List<GetUserDTO>>(_mapper.Map<GetUserDTO>(_user)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
