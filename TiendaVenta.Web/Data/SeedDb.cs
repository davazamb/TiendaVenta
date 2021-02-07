namespace TiendaVenta.Web.Data
{
	using Microsoft.AspNetCore.Identity;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using TiendaVenta.Web.Data.Entities;
	using TiendaVenta.Web.Helpers;

	public class SeedDb
	{
		private readonly DataContext context;
		private readonly IUserHelper userHelper;
		private Random random;

		public SeedDb(DataContext context, IUserHelper userHelper)
		{
			this.context = context;
			this.userHelper = userHelper;
			this.random = new Random();
		}


		public async Task SeedAsync()
		{
			await this.context.Database.EnsureCreatedAsync();
			await this.userHelper.CheckRoleAsync("Admin");
			await this.userHelper.CheckRoleAsync("Customer");

			var user = await this.userHelper.GetUserByEmailAsync("david.zambrano10@gmail.com");
			if (user == null)
			{
				user = new User
				{
					FirstName = "David",
					LastName = "Zambrano",
					Email = "david.zambrano10@gmail.com",
					UserName = "david.zambrano10@gmail.com",
					PhoneNumber = "56990512688"
				};

				var result = await this.userHelper.AddUserAsync(user, "123456");
				if (result != IdentityResult.Success)
				{
					throw new InvalidOperationException("Could not create the user in seeder");
				}
				await this.userHelper.AddUserToRoleAsync(user, "Admin");

			}
			var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
			if (!isInRole)
			{
				await this.userHelper.AddUserToRoleAsync(user, "Admin");
			}

			if (!this.context.Products.Any())
			{
				this.AddProduct("Iphone X", user);
				this.AddProduct("Samsung S10", user);
				this.AddProduct("Huawei P30", user);
				await this.context.SaveChangesAsync();
			}
		}

		private void AddProduct(string name, User user)
		{
			this.context.Products.Add(new Product
			{
				Name = name,
				Price = this.random.Next(100),
				IsAvailabe = true,
				Stock = this.random.Next(100),
				User = user
			});
		}
	}
}
