using Microsoft.AspNetCore.Mvc;
using PinPoint.Application.Interface;
using PinPoint.Data.Domain;
using PinPoint.Infrastructure.MapperService.Models;

namespace PinPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ContactController : Controller
    {
        protected readonly IContactService<Contact> _contactService;
        public ContactController(IContactService<Contact> contactService)
        {
            _contactService = contactService;
        }
        // GET: ContactController
        [HttpGet("list")]
        public async Task<IActionResult> Index()
        {
            return Json(await _contactService.GetContactListAsync());
        }

        // GET: ContactController/Details/5
        [HttpGet("detail")]
        public async Task<IActionResult> Details(string id)
        {
            return Json(await _contactService.GetByIdContactAsync(new Guid(id)));
        }

        // POST: ContactController/Create
        [HttpPost("create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostContactDTO postContactDTO)
        {
            return Json(await _contactService.PostContactAsync(postContactDTO));
        }

        // POST: ContactController/multiple/create
        [HttpPost("bulk/create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IEnumerable<PostContactDTO> postContactDTO)
        {
            return Json(await _contactService.PostBulkContactAsync(postContactDTO));
        }

        // PUT: ContactController/Edit/5
        [HttpPut("edit/{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, PutContactDTO putContactDTO)
        {
            return Json(await _contactService.PutContactAsync(new Guid(id), putContactDTO));
        }

        // DELETE: ContactController/bulk/Delete
        [HttpDelete("delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteContactDTO deleteContactDTO)
        {
            return Json(await _contactService.DeleteContactAsync(deleteContactDTO));
        }

        // DELETE: ContactController/bulk/Delete
        [HttpDelete("bulk/delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(IEnumerable<DeleteContactDTO> deleteContactDTO)
        {
            return Json(await _contactService.DeleteBulkContactAsync(deleteContactDTO));
        }
    }
}
