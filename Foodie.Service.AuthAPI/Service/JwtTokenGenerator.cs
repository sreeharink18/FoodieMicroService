﻿using Foodie.Service.AuthAPI.Models;
using Foodie.Service.AuthAPI.Service.IService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Foodie.Service.AuthAPI.Service
{
	public class JwtTokenGenerator : IJwtTokenGenerator
	{
		private JwtOptions _jwtOptions;
		public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions) { 
			_jwtOptions = jwtOptions.Value;
		}
		public string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

			var claimList = new List<Claim> {
				new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
				new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
				new Claim(JwtRegisteredClaimNames.Name, applicationUser.Name),
			};
			claimList.AddRange(roles.Select(role=> new Claim(ClaimTypes.Role, role)));
			var tokenDescription = new SecurityTokenDescriptor
			{
				Audience = _jwtOptions.Audience,
				Issuer = _jwtOptions.Issuer,
				Subject = new ClaimsIdentity(claimList),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescription);
			return tokenHandler.WriteToken(token);
		}
	}
}
