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
using PinPoint.Infrastructure.MapperService.Models;
using PinPoint.Infrastructure.UnitOfWork.Base;
using System.Linq;
using System.Net;

namespace PinPoint.Application.Service
{
    public class ContactService : BaseService, IContactService<Contact>
    {
        public ContactService(DataContext dataContext, IMapper mapper, ILoggerManager loggerManager) : base(dataContext, mapper, loggerManager) { }
        public async Task<IActionResult> GetContactListAsync()
        {
            try
            {
                List<PinPointResponse> pinpointResponse = new List<PinPointResponse>();
                IEnumerable<GetContactDTO> _contacts = _mapper.Map<List<GetContactDTO>>(await _uow.contactRepository.GetAllAsync());
                if (!_contacts.Any())
                {
                    pinpointResponse.Add(new PinPointResponse() { Code = HttpStatusCode.NotFound.ToString(), PropertyName = "Contact", AttemptedValue = "", Message = "No records found." });
                    return NotFound(pinpointResponse);
                }

                return Ok(_contacts.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public async Task<IActionResult> GetByIdContactAsync(Guid id)
        {
            try
            {
                List<PinPointResponse> pinpointResponse = new List<PinPointResponse>();
                var _contact = await _uow.contactRepository.GetByIDAsync(id);
                if (_contact == null)
                {
                    return NotFound(new PinPointResponse() { Code = HttpStatusCode.NotFound.ToString(), PropertyName = "contact_id", AttemptedValue = id.ToString(), Message = "No records found." });
                }
                return Ok(_mapper.Map<List<GetContactDTO>>(_mapper.Map<GetContactDTO>(_contact)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public async Task<IActionResult> PostContactAsync(PostContactDTO postContactDTO)
        {
            try
            {
                var _contact = _mapper.Map<Contact>(postContactDTO);
                var results = _uow.fluentValidationContact.PostValidationRules().Validate(_contact);
                if (!results.IsValid)
                {
                    List<PinPointResponse> pinpointResponse = new List<PinPointResponse>();
                    foreach (var ValidateItem in results.Errors)
                    {
                        pinpointResponse.Add(new PinPointResponse() { Code = ValidateItem.ErrorCode, PropertyName = ValidateItem.PropertyName, AttemptedValue = ValidateItem.AttemptedValue?.ToString() ?? "", Message = ValidateItem.ErrorMessage });
                    }
                    return BadRequest(pinpointResponse);
                }
                else
                {
                    _contact = await _uow.contactRepository.AddAsync(_contact);
                    _uow.Complete();
                    return Ok(_mapper.Map<List<GetContactDTO>>(_mapper.Map<GetContactDTO>(_contact)));
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public async Task<IActionResult> PostBulkContactAsync(IEnumerable<PostContactDTO> postContactDTOs)
        {
            try
            {
                List<PinPointResponse> pinpointResponse = new List<PinPointResponse>();

                foreach (var postContactDTOItem in postContactDTOs)
                {
                    var _contact = _mapper.Map<Contact>(postContactDTOItem);

                    var results = _uow.fluentValidationContact.PostValidationRules().Validate(_contact);
                    if (!results.IsValid)
                    {
                        foreach (var ValidateItem in results.Errors)
                        {
                            var isExisting = pinpointResponse.Any(x => x.AttemptedValue == ValidateItem.AttemptedValue.ToString() && x.Code == ValidateItem.ErrorCode);
                            if (isExisting) continue;
                            pinpointResponse.Add(new PinPointResponse() { Code = ValidateItem.ErrorCode, PropertyName = ValidateItem.PropertyName, AttemptedValue = ValidateItem.AttemptedValue?.ToString() ?? "", Message = ValidateItem.ErrorMessage });
                        }
                    }
                    else
                    {
                        _contact = await _uow.contactRepository.AddAsync(_mapper.Map<Contact>(postContactDTOItem));
                        _uow.Complete();
                        pinpointResponse.Add(new PinPointResponse() { Code = HttpStatusCode.OK.ToString(), PropertyName = "contact_id", AttemptedValue = _contact.contact_id.ToString(), Message = "Successful" });
                    }
                }
                return Ok(pinpointResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public async Task<IActionResult> PutContactAsync(Guid id, PutContactDTO putContactDTO)
        {
            try
            {
                var _contact = _mapper.Map<Contact>(putContactDTO);
                Contact newData = await _uow.fluentValidationContact.PutCompareRulesAsync(id, _contact);
                if (newData != null)
                {
                    var results = _uow.fluentValidationContact.PutValidationRules().Validate(newData);
                    if (!results.IsValid)
                    {
                        List<PinPointResponse> pinpointResponse = new List<PinPointResponse>();
                        foreach (var ValidateItem in results.Errors)
                        {
                            pinpointResponse.Add(new PinPointResponse() { Code = ValidateItem.ErrorCode, PropertyName = ValidateItem.PropertyName, AttemptedValue = ValidateItem.AttemptedValue?.ToString() ?? "", Message = ValidateItem.ErrorMessage });
                        }
                        return BadRequest(pinpointResponse);
                    }
                    else
                    {
                        _contact = _uow.contactRepository.Update(newData);
                        _uow.Complete();
                        return Ok(_mapper.Map<List<GetContactDTO>>(_mapper.Map<GetContactDTO>(_contact)));
                    }
                }
                else
                    return NotFound(new PinPointResponse() { Code = HttpStatusCode.NotFound.ToString(), PropertyName = _contact.contact_id.ToString(), AttemptedValue = null, Message = "No records found." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public async Task<IActionResult> DeleteContactAsync(DeleteContactDTO deleteContactDTO)
        {
            try
            {
                List<PinPointResponse> pinpointResponse = new List<PinPointResponse>();
                var _contact = await _uow.contactRepository.GetByIDAsync(deleteContactDTO.contact_id);
                if (_contact == null)
                {
                    return NotFound(new PinPointResponse() { Code = HttpStatusCode.NotFound.ToString(), PropertyName = "contact_id", AttemptedValue = deleteContactDTO.contact_id.ToString(), Message = "No records found." });
                }
                else
                {
                    _uow.contactRepository.Remove(_contact);
                    _uow.Complete();
                    return Ok(new PinPointResponse() { Code = HttpStatusCode.OK.ToString(), PropertyName = "contact_id", AttemptedValue = deleteContactDTO.contact_id.ToString(), Message = "Successful" });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public async Task<IActionResult> DeleteBulkContactAsync(IEnumerable<DeleteContactDTO> deleteContactDTOs)
        {
            try
            {
                List<PinPointResponse> pinpointResponse = new List<PinPointResponse>();
                foreach (var deleteContactDTOItem in deleteContactDTOs)
                {
                    var _contact = await _uow.contactRepository.GetByIDAsync(deleteContactDTOItem.contact_id);
                    if (_contact == null)
                        pinpointResponse.Add(new PinPointResponse() { Code = HttpStatusCode.NotFound.ToString(), PropertyName = "contact_id", AttemptedValue = deleteContactDTOItem.contact_id.ToString(), Message = "No records found." });
                    else
                    {
                        _uow.contactRepository.Remove(_contact);
                        _uow.Complete();
                        pinpointResponse.Add(new PinPointResponse() { Code = HttpStatusCode.OK.ToString(), PropertyName = "contact_id", AttemptedValue = deleteContactDTOItem.contact_id.ToString(), Message = "Successful" });
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
