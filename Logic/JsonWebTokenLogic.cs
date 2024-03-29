﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Logic
{
    public class JsonWebTokenLogic : IJsonWebTokenLogic
    {
        public async Task<object> GenerateJwtToken(string email, string user)
        {
            var claims = new List<Claim>
                         {
                             new Claim(JwtRegisteredClaimNames.Sub, email),
                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                             new Claim(ClaimTypes.NameIdentifier, user)
                         };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YeahThisIsSuperSecureTrustMeImAnEngineer"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(4));
            var token = new JwtSecurityToken(
                "Guess",
                "What",
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}