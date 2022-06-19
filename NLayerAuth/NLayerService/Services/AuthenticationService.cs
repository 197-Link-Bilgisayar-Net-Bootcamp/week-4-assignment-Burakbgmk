using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLayerCore.DTOs;
using NLayerCore.Models;
using NLayerCore.Repositories;
using NLayerCore.Services;
using NLayerCore.UnitOfWork;
using SharedLibrary.Response;


namespace NLayerService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;
        public AuthenticationService(ITokenService tokenService, UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenService = userRefreshTokenService;
        }

        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return new Response<TokenDto> { Errors = new List<string> { "Email or Password is wrong" }, Status = 404 };

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return new Response<TokenDto> { Errors = new List<string> { "Email or Password is wrong" }, Status = 404 };
            }
            var token = _tokenService.CreateToken(user);

            var userRefreshToken = await _userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken == null)
            {
                await _userRefreshTokenService.Add(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _unitOfWork.Commit();

            return new Response<TokenDto> { Data = token ,Status = 200 };
        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return new Response<TokenDto> { Errors = new List<string> { "Refresh Token not found" }, Status = 404 };
            }

            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

            if (user == null)
            {
                return new Response<TokenDto> { Errors = new List<string> { "UserID not found" }, Status = 404 };
            }

            var tokenDto = _tokenService.CreateToken(user);

            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.Commit();

            return new Response<TokenDto> { Status = 200 };
        }
        public async Task<Response<string>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return new Response<string> { Errors = new List<string> { "Refresh token not found" }, Status = 404};
            }

            _userRefreshTokenService.Delete(existRefreshToken);

            await _unitOfWork.Commit();

            return new Response<string> { Status = 200 };
        }
    }
}
