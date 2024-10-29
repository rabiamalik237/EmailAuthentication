using WebApplication2.Model;

namespace WebApplication2.Services
{
    public interface IUserService
    {
        //List<User> GetAllList();
        Task<UserResponseDto> LoginUserAsync(UserLoginDto loginRequestDto);
        Task<User> AddUserAsync(UserRequestDto adduser);
        //User updateUser(int id, GetUser addAndUpdate);
        //bool deleteById(int id);
    }
}

