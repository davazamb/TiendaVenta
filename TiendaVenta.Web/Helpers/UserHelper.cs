﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaVenta.Web.Data.Entities;

namespace TiendaVenta.Web.Helpers
{
	public class UserHelper : IUserHelper
	{
		private readonly UserManager<User> userManager;

		public UserHelper(UserManager<User> userManager)
		{
			this.userManager = userManager;
		}

		public async Task<IdentityResult> AddUserAsync(User user, string password)
		{
			return await this.userManager.CreateAsync(user, password);
		}

		public async Task<User> GetUserByEmailAsync(string email)
		{
			return await this.userManager.FindByEmailAsync(email);
		}
	}

}
