﻿using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models.DTO.AuthDTOFolder
{
	public class LoginRequestDto
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
