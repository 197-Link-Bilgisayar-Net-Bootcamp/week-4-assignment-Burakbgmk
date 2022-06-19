﻿using NLayerCore.DTOs;
using SharedLibrary.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerCore.Services
{
    public interface IAuthenticationService
    {
        Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto);

        Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken);

        Task<Response<string>> RevokeRefreshToken(string refreshToken);

        //Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto);
    }
}
