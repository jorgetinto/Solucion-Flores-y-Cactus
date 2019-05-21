using Entity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using WebApi.Authentication.Interfaces;

namespace WebApi.Authentication
{
    public class JwtProvider : ITokenProvider
    {
        private RsaSecurityKey _key;
        private string _algoritm;
        private string _issuer;
        private string _audience;

        public JwtProvider(string issuer, string audience, string KeyName)
        {
            CspParameters parameters = new CspParameters() { KeyContainerName = KeyName };
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider(2048, parameters);
            _key = new RsaSecurityKey(provider);
            _algoritm = SecurityAlgorithms.RsaSha256Signature;
            _issuer = issuer;
            _audience = audience;
        }

        public string CreateToken(UserEntity user, DateTime expiry)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsIdentity identity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Roles),
                new Claim(ClaimTypes.PrimarySid, user.Id.ToString())
            }, "custon");


            return tokenHandler.WriteToken(tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                Audience = _audience,
                Issuer = _issuer,
                SigningCredentials = new SigningCredentials(_key, _algoritm),
                Expires = expiry.ToUniversalTime(),
                Subject = identity
            }));
        }

        public TokenValidationParameters GetValidationParameters() => new TokenValidationParameters
        {
            IssuerSigningKey = _key,
            ValidAudience = _audience,
            ValidIssuer = _issuer,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(0)
        };
    }
}
