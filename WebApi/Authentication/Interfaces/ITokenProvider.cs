using Entity;
using Microsoft.IdentityModel.Tokens;
using System;

namespace WebApi.Authentication.Interfaces
{
    public interface ITokenProvider
    {
        string CreateToken(UserEntity user, DateTime expiry);
        TokenValidationParameters GetValidationParameters();
    }
}
