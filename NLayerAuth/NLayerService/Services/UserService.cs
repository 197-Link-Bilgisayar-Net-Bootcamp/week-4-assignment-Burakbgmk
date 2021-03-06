using Microsoft.AspNetCore.Identity;
using NLayerCore.DTOs;
using NLayerCore.Models;
using NLayerCore.Services;
using SharedLibrary.Response;

namespace NLayerService.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;

        public UserService(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new UserApp { Email = createUserDto.Email, UserName = createUserDto.UserName };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return new Response<UserAppDto>  {Status = 400 };
            }
            return new Response<UserAppDto> { Status = 200 };
        }

        public async Task<Response<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return new Response<UserAppDto> { Errors = new List<string> { "User Name not found" }, Status = 404 };
            }

            return new Response<UserAppDto> { Data = ObjectMapper.Mapper.Map<UserAppDto>(user) ,Status = 200 };
        }
    }

}

