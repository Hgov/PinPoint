using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PinkPoint.Application.Interface;
using PinkPoint.Application.Service;
using PinkPoint.Data.Domain;
using PinkPoint.DataAccess.Helpers;
using PinkPoint.Mapper.Models.User;

namespace PinkPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private IUserService<User> _userService;
        private readonly ILogger<UserController> _logger;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public UserController(DataContext dataContext, IMapper mapper, ILogger<UserController> logger)
        {
            _dataContext = dataContext;
            _userService = new UserService(_dataContext);
            _mapper = mapper;
            _logger = logger;
        }
        // GET: UserController
        [HttpGet("list")]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("sadasdasdasdasdasd");

            var result = await _userService.GetUserListAsync();
            var _mapped = _mapper.Map<List<GetUserDTO>>(result);
            return Ok(_mapped);
        }

        // GET: UserController/Details/5
        [HttpGet("detail/{id?}")]
        public async Task<IActionResult> Details(Guid id)
        {
            User result = await _userService.GetByIdUserAsync(id);
            var _mapped = _mapper.Map<GetUserDTO>(result);
            return Ok(_mapped);
        }


        // POST: UserController/Create
        [HttpPost("create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostUserDTO postUserDTO)
        {
            var _mapped = _mapper.Map<User>(postUserDTO);
            var result = await _userService.PostUserAsync(_mapped);
            return Ok(new
            {
                CreatedUserId = result.user_id
            });
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
