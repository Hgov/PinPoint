using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PinPoint.Application.Interface;
using PinPoint.Core.Data;
using PinPoint.Core.LoggerManager;
using PinPoint.Core.UnitOfWork.Base;
using PinPoint.Data.Domain;
using PinPoint.DataAccess.Helpers;
using PinPoint.Infrastructure.BaseClass;
using PinPoint.Infrastructure.MapperService.Models.User;
using PinPoint.Infrastructure.UnitOfWork.Base;
using System.Linq;
using System.Net;

namespace PinPoint.Application.Service
{
    public class UserService : BaseService, IUserService<User>
    {
        public UserService(DataContext dataContext, IMapper mapper, ILoggerManager loggerManager) : base(dataContext, mapper, loggerManager) { }
        public async Task<IActionResult> GetUserListAsync()
        {
            try
            {
                List<PinPointResponse> pinpointResponse = new List<PinPointResponse>();
                IEnumerable<GetUserDTO> _users = _mapper.Map<List<GetUserDTO>>(await _uow.userRepository.GetAllAsync());
                if (!_users.Any())
                {
                    pinpointResponse.Add(new PinPointResponse() { Code = HttpStatusCode.NotFound.ToString(), PropertyName = "User", AttemptedValue = "", Message = "No records found." });
                    return NotFound(pinpointResponse);
                }

                return Ok(_users.ToList());
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
                List<PinPointResponse> pinpointResponse = new List<PinPointResponse>();
                var _user = await _uow.userRepository.GetByIDAsync(id);
                if (_user == null)
                {
                    pinpointResponse.Add(new PinPointResponse() { Code = HttpStatusCode.NotFound.ToString(), PropertyName = "user_id", AttemptedValue = id.ToString(), Message = "No records found." });
                    return NotFound(pinpointResponse);
                }
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
                    List<PinPointResponse> pinpointResponse = new List<PinPointResponse>();
                    foreach (var ValidateItem in results.Errors)
                    {
                        pinpointResponse.Add(new PinPointResponse() { Code = ValidateItem.ErrorCode, PropertyName = ValidateItem.PropertyName, AttemptedValue = ValidateItem.AttemptedValue.ToString(), Message = ValidateItem.ErrorMessage });
                    }
                    return BadRequest(pinpointResponse);
                }
                else
                {
                    _user = await _uow.userRepository.AddAsync(_user);
                    _uow.Complete();
                    return Ok(_mapper.Map<List<GetUserDTO>>(_mapper.Map<GetUserDTO>(_user)));
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public async Task<IActionResult> PostBulkUserAsync(IEnumerable<PostUserDTO> postUserDTOs)
        {
            try
            {
                List<PinPointResponse> pinpointResponse = new List<PinPointResponse>();

                foreach (var postUserDTOItem in postUserDTOs)
                {
                    var _user = _mapper.Map<User>(postUserDTOItem);

                    var results = _uow.fluentValidationUser.PostValidationRules().Validate(_user);
                    if (!results.IsValid)
                    {
                        foreach (var ValidateItem in results.Errors)
                        {
                            var isExisting = pinpointResponse.Any(x => x.AttemptedValue == ValidateItem.AttemptedValue.ToString() && x.Code == ValidateItem.ErrorCode);
                            if (isExisting) continue;
                            pinpointResponse.Add(new PinPointResponse() { Code = ValidateItem.ErrorCode, PropertyName = ValidateItem.PropertyName, AttemptedValue = ValidateItem.AttemptedValue.ToString(), Message = ValidateItem.ErrorMessage });
                        }
                    }
                    else
                    {
                        _user = await _uow.userRepository.AddAsync(_mapper.Map<User>(postUserDTOItem));
                        _uow.Complete();
                        pinpointResponse.Add(new PinPointResponse() { Code = HttpStatusCode.OK.ToString(), PropertyName = _user.user_id.ToString(), AttemptedValue = null, Message = "Successful" });
                    }
                }
                return Ok(pinpointResponse);
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
                        List<PinPointResponse> pinpointResponse = new List<PinPointResponse>();
                        foreach (var ValidateItem in results.Errors)
                        {
                            pinpointResponse.Add(new PinPointResponse() { Code = ValidateItem.ErrorCode, PropertyName = ValidateItem.PropertyName, AttemptedValue = ValidateItem.AttemptedValue.ToString(), Message = ValidateItem.ErrorMessage });
                        }
                        return BadRequest(pinpointResponse);
                    }
                    else
                    {
                        _user = _uow.userRepository.Update(newData);
                        _uow.Complete();
                        return Ok(_mapper.Map<List<GetUserDTO>>(_mapper.Map<GetUserDTO>(_user)));
                    }
                }
                else
                    return NotFound(new PinPointResponse() { Code = HttpStatusCode.NotFound.ToString(), PropertyName = _user.user_id.ToString(), AttemptedValue = null, Message = "Successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            try
            {
                List<PinPointResponse> pinpointResponse = new List<PinPointResponse>();
                var _user = await _uow.userRepository.GetByIDAsync(id);
                if (_user == null)
                    pinpointResponse.Add(new PinPointResponse() { Code = HttpStatusCode.NotFound.ToString(), PropertyName = "user_id", AttemptedValue = id.ToString(), Message = "No records found." });
                else
                {
                    _uow.userRepository.Remove(_user);
                    _uow.Complete();
                    pinpointResponse.Add(new PinPointResponse() { Code = HttpStatusCode.OK.ToString(), PropertyName = _user.user_id.ToString(), AttemptedValue = id.ToString(), Message = "Successful" });
                }
                return Ok(pinpointResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public async Task<IActionResult> DeleteBulkUserAsync(IEnumerable<DeleteUserDTO> deleteUserDTOs)
        {
            try
            {
                List<PinPointResponse> pinpointResponse = new List<PinPointResponse>();
                foreach (var deleteUserDTOItem in deleteUserDTOs)
                {
                    var _user = await _uow.userRepository.GetByIDAsync(deleteUserDTOItem.user_id);
                    if (_user == null)
                        pinpointResponse.Add(new PinPointResponse() { Code = HttpStatusCode.NotFound.ToString(), PropertyName = "user_id", AttemptedValue = deleteUserDTOItem.user_id.ToString(), Message = "No records found." });
                    else
                    {
                        _uow.userRepository.Remove(_user);
                        _uow.Complete();
                        pinpointResponse.Add(new PinPointResponse() { Code = HttpStatusCode.OK.ToString(), PropertyName = _user.user_id.ToString(), AttemptedValue = deleteUserDTOItem.user_id.ToString(), Message = "Successful" });
                    }
                }
                return Ok(pinpointResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
