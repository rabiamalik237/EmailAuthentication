using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text;
using WebApplication2.Entity;
using WebApplication2.Model;


namespace WebApplication2.Services
{
    public class UserService : IUserService
    {
        private readonly UserDbContext _userService;
        public UserService(UserDbContext usDb)
        {
            _userService = usDb;
        }
        public async Task<UserResponseDto> LoginUserAsync(UserLoginDto loginRequestDto)
        {
            try
            {
                var entity = await _userService.Users.FirstOrDefaultAsync(u => u.Email == loginRequestDto.Email && u.Password == loginRequestDto.Password) ?? throw new Exception("inavlid credentaials");
                return new UserResponseDto { Token = GenerateFakeJwtToken() };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> AddUserAsync(UserRequestDto adduser)
        {
            var addUser = new User()
            {
                Name = adduser.Name,
                Email = adduser.Email,
                Password = adduser.Password,
                ConfirmPwd = adduser.ConfirmPwd
            };

            _userService.Users.Add(addUser);
            _userService.SaveChanges();

            await SendEmailAsync(addUser.Email, adduser);
            return addUser;
        }

        public static implicit operator UserService(UserDbContext v)
        {
            throw new NotImplementedException();
        }   
        public static string GenerateFakeJwtToken()
        {
            // Simulated header: {"alg":"HS256","typ":"JWT"}
            string header = Convert.ToBase64String(Encoding.UTF8.GetBytes("{\"alg\":\"HS256\",\"typ\":\"JWT\"}"));

            // Simulated payload: {"sub":"1234567890","name":"John Doe","iat":1516239022}
            string payload = Convert.ToBase64String(Encoding.UTF8.GetBytes("{\"sub\":\"1234567890\",\"name\":\"John Doe\",\"iat\":1516239022}"));

            // Generate a random signature-like string
            var randomBytes = new byte[32];
            new Random().NextBytes(randomBytes);
            string signature = Convert.ToBase64String(randomBytes);

            // Remove base64 padding for a more authentic JWT look
            header = header.TrimEnd('=');
            payload = payload.TrimEnd('=');
            signature = signature.TrimEnd('=');

            return $"{header}.{payload}.{signature}";
        }

        //public User updateUser(int id, GetUser updateUser)
        //{
        //    var UserIndex = _userService.FindIndex(idx => idx.Id == id);
        //    if (UserIndex > 0)
        //    {
        //        var user = _userService[UserIndex];
        //        user.Name = updateUser.Name;
        //        user.Email = updateUser.Email;
        //        user.Password = updateUser.Password;
        //        user.ConfirmPwd = updateUser.ConfirmPwd;

        //        _userService[UserIndex] = user;

        //        return user;

        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}
        //public bool deleteById(int id)
        //{
        //    var userDt = _userService.Find(x => x.Id == id);
        //    if (userDt is not null)
        //    {
        //        }_userService.Remove(userDt);
        //    }
        //    return true;
        //}
        //public bool deleteById(int id)
        //{
        //    var userDt = _userService.FindIndex(x => x.Id == id);
        //    if (userDt > 0)
        //    {
        //        _userService.RemoveAt(userDt);
        //    }
        //    return true;
        //}


        public async Task<bool> SendEmailAsync(string email, UserRequestDto emailDto)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("rabia.malik.se@gmail.com");

            var subject = "Account Confirmation";
            var to = new EmailAddress(email);
            var plainContent = $"<strong> Email: {emailDto.Email} <br> Password: {emailDto.Password}</strong>";
            var htmlContent = @$"You have successfuly registered.<br> <strong> <a href='http://localhost:4200/'> Click here to Login</a> <br> {plainContent}";
            var mailMessage = MailHelper.CreateSingleEmail(from, to, subject, plainContent, htmlContent);
            var response = await client.SendEmailAsync(mailMessage);

            return true;
        }
    }
}
