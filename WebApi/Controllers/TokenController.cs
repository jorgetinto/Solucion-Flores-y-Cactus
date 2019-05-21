using Business;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Authentication;
using WebApi.Authentication.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private ITokenProvider _tokenProvider;
        private IUnitOfWork _unitOfWork;

        public TokenController(ITokenProvider tokenProvider, IUnitOfWork unitOfWork)
        {
            _tokenProvider = tokenProvider;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public JsonwebToken Post([FromBody] UserEntity userLogin)
        {
            var user = _unitOfWork.User.ValidateUser(userLogin.Email, userLogin.Password);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var token = new JsonwebToken
            {
                Access_Token = _tokenProvider.CreateToken(user, DateTime.UtcNow.AddHours(8)),
                Expires_in = 480 //minutos
            };

            return token;
        }
    }
}