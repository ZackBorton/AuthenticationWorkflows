using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace API.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly IUserLogin _userLogin;
        private readonly IJsonWebTokenLogic _jsonWebTokenLogic;

        public AuthenticateController(IUserLogin userLogin, IJsonWebTokenLogic jsonWebTokenLogic)
        {
            _userLogin = userLogin;
            _jsonWebTokenLogic = jsonWebTokenLogic;
        }

        /// <summary>
        ///     A sample controller route
        /// </summary>
        /// <param name="portfolioPolicy"></param>
        /// <returns></returns>
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost]
        public async Task<object> Login([FromBody] AuthUser user)
        {
            if (_userLogin.LoginUser(user).Item1)
                return Ok(await _jsonWebTokenLogic.GenerateJwtToken(user.Email, user.LastName));
            return Unauthorized("User is not registered or password is not correct");
        }

        /// <summary>
        ///     Log a user out of the system
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Logout()
        {
            throw new NotImplementedException();
        }
    }
}