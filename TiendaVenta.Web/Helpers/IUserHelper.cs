using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaVenta.Web.Data.Entities;
using TiendaVenta.Web.Models;

namespace TiendaVenta.Web.Helpers
{
	public interface IUserHelper
	{
		Task<User> GetUserByEmailAsync(string email);

		Task<IdentityResult> AddUserAsync(User user, string password);
		Task<SignInResult> LoginAsync(LoginViewModel model);
		Task LogoutAsync();


	}

}
