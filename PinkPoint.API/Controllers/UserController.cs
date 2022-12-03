using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PinkPoint.Application.Interface;
using PinkPoint.Application.Service;
using PinkPoint.Core.Repositories;
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
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public UserController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _userService = new UserService(_dataContext);
            _mapper = mapper;
        }
        // GET: UserController
        [HttpGet("list")]
        public async Task<IActionResult> Index()
        {
            var result = await _userService.GetUserListAsync();
            //var _userMapped = mapper.Map<List<User>, List<DTOUserGet>>(GetUserAll.ToList());
            return Ok(result.Select(x => new
            {
                user_id = x.user_id,
                first_name = x.first_name,
                last_name = x.last_name,
                email = x.email,
                phone = x.phone,
                bio = x.bio,
                creation_tsz = x.creation_tsz,
                status_active = x.status_active,
                status_visibility = x.status_visibility
            }));
        }

        // GET: UserController/Details/5
        [HttpGet("detail/{id?}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _userService.GetByIdUserAsync(id);
            return Ok(new
            {
                user_id = result.user_id,
                first_name = result.first_name,
                last_name = result.last_name,
                email = result.email,
                phone = result.phone,
                bio = result.bio,
                creation_tsz = result.creation_tsz,
                status_active = result.status_active,
                status_visibility = result.status_visibility
            });
        }


        // POST: UserController/Create
        [HttpPost("create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GetUserDTO user)
        {
            var _mappedUser = _mapper.Map<GetUserDTO,User>(user);
            var result = await _userService.PostUserAsync(_mappedUser);
            return Ok();
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
