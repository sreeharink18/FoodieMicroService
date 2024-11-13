using Microsoft.AspNetCore.Identity;

namespace Foodie.Service.AuthAPI.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
	}
}
