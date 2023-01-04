using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using PinPoint.Application.Interface;
using PinPoint.Application.Service;
using PinPoint.Core.LoggerManager;
using PinPoint.Data.Domain;
using PinPoint.DataAccess.Helpers;
using PinPoint.Infrastructure.MapperService.Models.User;

namespace PinPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private IUserService<User> _userService;
        private readonly DataContext _dataContext;
        public UserController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _userService = new UserService(_dataContext,mapper);
        }
        // GET: UserController
        [HttpGet("list")]
        public async Task<IActionResult> Index()
        {
            return Json(await _userService.GetUserListAsync());
        }

        // GET: UserController/Details/5
        [HttpGet("detail/{id?}")]
        public async Task<IActionResult> Details(Guid id)
        {
            return Json(await _userService.GetByIdUserAsync(id));
        }


        // POST: UserController/Create
        [HttpPost("create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostUserDTO postUserDTO)
        {
            return Json(await _userService.PostUserAsync(postUserDTO));
        }

        //// POST: UserController/Create
        //[HttpPost("bulkcreate")]
        ////[ValidateAntiForgeryToken]
        //public IActionResult BulkCreate(IEnumerable<User> users)
        //{
        //    try
        //    {
        //        var result = _uow.userRepository.AddRangeAsync(users);
        //        _uow.Complete();
        //        return Ok("Success Add");
        //    }
        //    catch (AppException ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //// POST: UserController/Edit/5
        //[HttpPut("edit/{id}")]
        ////[ValidateAntiForgeryToken]
        //public ActionResult Edit(Guid id, User user)
        //{
        //    try
        //    {
        //        user.user_id = id;
        //        _uow.userRepository.update(user);
        //        _uow.Complete();
        //        return Ok("Success Update");
        //    }
        //    catch (AppException ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}
        //// POST: UserController/Delete/5
        //[HttpDelete("deleteid/{id}")]
        ////[ValidateAntiForgeryToken]
        //public ActionResult Delete(Guid id)
        //{
        //    try
        //    {
        //        _uow.userRepository.RemoveAsync(id);
        //        _uow.Complete();
        //        return Ok("Success Deleted");
        //    }
        //    catch (AppException ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //[HttpDelete("bulkdelete")]
        //public ActionResult BulkDelete(IEnumerable<User> users)
        //{
        //    try
        //    {
        //        _uow.userRepository.RemoveRangeAsync(users);
        //        _uow.Complete();
        //        return Ok("Success Deleted");
        //    }
        //    catch (AppException ex)
        //    {
        //        return BadRequest(new {message=ex.Message});
        //    }
        //}
    }
}
