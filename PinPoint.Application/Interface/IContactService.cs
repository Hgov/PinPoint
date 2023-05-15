using Microsoft.AspNetCore.Mvc;
using PinPoint.Infrastructure.MapperService.Models;

namespace PinPoint.Application.Interface
{
    public interface IContactService<Contact> where Contact : class
    {
        Task<IActionResult> GetContactListAsync();
        Task<IActionResult> GetByIdContactAsync(Guid id);
        Task<IActionResult> PostContactAsync(PostContactDTO postContactDTO);
        Task<IActionResult> PostBulkContactAsync(IEnumerable<PostContactDTO> postContactDTO);
        Task<IActionResult> PutContactAsync(Guid id, PutContactDTO putContactDTO);
        Task<IActionResult> DeleteContactAsync(DeleteContactDTO deleteContactDTO);
        Task<IActionResult> DeleteBulkContactAsync(IEnumerable<DeleteContactDTO> deleteContactDTO);
    }
}
