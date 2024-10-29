using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;
        public UserController(IUserService user)
        {
            _user = user;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserLoginDto loginRequestDto)
        {
            try
            {
                if (loginRequestDto is null) throw new Exception("invalid payload");
                return Ok(await _user.LoginUserAsync(loginRequestDto));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    var user = _user.GetById(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(user);
        //}

        [HttpPost]
        public async Task<IActionResult> SaveAsync(UserRequestDto requestDto)
        {
            var user =await _user.AddUserAsync(requestDto);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(new
            {
                message = "User Created Successfully...!",
                id = user!.Id,
                success = true
            });
        }
        //[HttpPut("update")]
        //public IActionResult Put(int id, RegisterUserDto obj)
        //{
        //    var user = _user.updateUser(id, obj);
        //    if (user == null)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(new
        //    {
        //        message = "User Updated Successfully...!",
        //        id = user!.Id
        //    });
        //}
        //[HttpDelete("{id}")]
        //public IActionResult Delete([FromRoute] int id)
        //{

        //    if (!_user.deleteById(id))
        //    {
        //        return NotFound();
        //    }
        //    return Ok(new
        //    {
        //        message = "User Deleted Successfully..!",
        //        id = id
        //    });
        //}
    }
}
