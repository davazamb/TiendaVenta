﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaVenta.Web.Data;
using TiendaVenta.Web.Data.Entities;
using TiendaVenta.Web.Helpers;
using TiendaVenta.Web.Models;

namespace TiendaVenta.Web.Controllers
{
    public class ProductsController : Controller
    {
		private readonly IProductRepository productRepository;
		private readonly IUserHelper userHelper;


		public ProductsController(IProductRepository productRepository, IUserHelper userHelper)
        {
			this.productRepository = productRepository;
			this.userHelper = userHelper;
		}
		private ProductViewModel ToProducViewModel(Product product)
		{
			return new ProductViewModel
			{
				Id = product.Id,
				ImageUrl = product.ImageUrl,
				IsAvailabe = product.IsAvailabe,
				LastPurchase = product.LastPurchase,
				LastSale = product.LastSale,
				Name = product.Name,
				Price = product.Price,
				Stock = product.Stock,
				User = product.User
			};
		}

		private Product ToProduct(ProductViewModel view, string path)
		{
			return new Product
			{
				Id = view.Id,
				ImageUrl = path,
				IsAvailabe = view.IsAvailabe,
				LastPurchase = view.LastPurchase,
				LastSale = view.LastSale,
				Name = view.Name,
				Price = view.Price,
				Stock = view.Stock,
				User = view.User
			};
		}

		// GET: Products
		public IActionResult Index()
		{
			return View(this.productRepository.GetAll().OrderBy(p => p.Name));
		}

		// GET: Products/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await this.productRepository.GetByIdAsync(id.Value);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// GET: Products/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Products/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductViewModel view)
		{
			if (ModelState.IsValid)
			{
				var path = string.Empty;

				if (view.ImageFile != null && view.ImageFile.Length > 0)
				{
					var guid = Guid.NewGuid().ToString();
					var file = $"{guid}.jpg";

					path = Path.Combine(
						Directory.GetCurrentDirectory(),
						"wwwroot\\images\\Products",
						file);


					using (var stream = new FileStream(path, FileMode.Create))
					{
						await view.ImageFile.CopyToAsync(stream);
					}

					path = $"~/images/Products/{file}";
				}


				// TODO: Pending to change to: this.User.Identity.Name
				view.User = await this.userHelper.GetUserByEmailAsync("david.zambrano10@gmail.com");
				var product = this.ToProduct(view, path);
				await this.productRepository.CreateAsync(product);
				return RedirectToAction(nameof(Index));
			}

			return View(view);
		}


		// GET: Products/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await this.productRepository.GetByIdAsync(id.Value);
			if (product == null)
			{
				return NotFound();
			}

			var view = this.ToProducViewModel(product);
			return View(view);
		}

		// POST: Products/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(ProductViewModel view)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var path = view.ImageUrl;

					if (view.ImageFile != null && view.ImageFile.Length > 0)
					{
						var guid = Guid.NewGuid().ToString();
						var file = $"{guid}.jpg";

						path = Path.Combine(
							Directory.GetCurrentDirectory(),
							"wwwroot\\images\\Products",
							file);
						
						using (var stream = new FileStream(path, FileMode.Create))
						{
							await view.ImageFile.CopyToAsync(stream);
						}

						path = $"~/images/Products/{file}";
					}

					// TODO: Pending to change to: this.User.Identity.Name
					view.User = await this.userHelper.GetUserByEmailAsync("david.zambrano10@gmail.com");
					var product = this.ToProduct(view, path);

					await this.productRepository.UpdateAsync(product);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!await this.productRepository.ExistAsync(view.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}

			return View(view);
		}

		// GET: Products/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await this.productRepository.GetByIdAsync(id.Value);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// POST: Products/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var product = await this.productRepository.GetByIdAsync(id);
			await this.productRepository.DeleteAsync(product);
			return RedirectToAction(nameof(Index));
		}


		

		//private bool ProductExists(int id)
		//{
		//    return _context.Products.Any(e => e.Id == id);
		//}
	}
}
