﻿using Foodie.Service.AuthAPI.Models;

namespace Foodie.Service.AuthAPI.Service.IService
{
	public interface IJwtTokenGenerator
	{
		string GenerateToken(ApplicationUser applicationUser,IEnumerable<string> roles);
	}
}
