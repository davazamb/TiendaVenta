using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaVenta.Web.Data;
using TiendaVenta.Web.Data.Entities;
using TiendaVenta.Web.Helpers;

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

		// GET: Products
		// GET: Products
		public IActionResult Index()
		{
			return View(this.productRepository.GetAll());
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
		public async Task<IActionResult> Create(Product product)
		{
			if (ModelState.IsValid)
			{
				// TODO: Pending to change to: this.User.Identity.Name
				product.User = await this.userHelper.GetUserByEmailAsync("david.zambrano10@gmail.com");
				await this.productRepository.CreateAsync(product);
				return RedirectToAction(nameof(Index));
			}

			return View(product);
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

			return View(product);
		}

		// POST: Products/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Product product)
		{
			if (ModelState.IsValid)
			{
				try
				{
					// TODO: Pending to change to: this.User.Identity.Name
					product.User = await this.userHelper.GetUserByEmailAsync("david.zambrano10@gmail.com");
					await this.productRepository.UpdateAsync(product);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!await this.productRepository.ExistAsync(product.Id))
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

			return View(product);
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
