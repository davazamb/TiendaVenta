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
		private readonly IRepository repository;
		private readonly IUserHelper userHelper;


		public ProductsController(IRepository repository, IUserHelper userHelper)
        {
			this.repository = repository;
			this.userHelper = userHelper;
		}

        // GET: Products
        public IActionResult Index()
        {
            return View(this.repository.GetProducts());
        }
		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = this.repository.GetProduct(id.Value);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Product product)
		{
			if (ModelState.IsValid)
			{
				//TODO: Change for the logged user
				product.User = await this.userHelper.GetUserByEmailAsync("david.zambrano10@gmail.com");
				this.repository.AddProduct(product);
				await this.repository.SaveAllAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(product);
		}

		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = this.repository.GetProduct(id.Value);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Product product)
		{
			if (ModelState.IsValid)
			{
				try
				{
					this.repository.UpdateProduct(product);
					await this.repository.SaveAllAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!this.repository.ProductExists(product.Id))
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

		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = this.repository.GetProduct(id.Value);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var product = this.repository.GetProduct(id);
			this.repository.RemoveProduct(product);
			await this.repository.SaveAllAsync();
			return RedirectToAction(nameof(Index));
		}

		// GET: Products/Details/5
		//public async Task<IActionResult> Details(int? id)
		//{
		//    if (id == null)
		//    {
		//        return NotFound();
		//    }

		//    var product = await _context.Products
		//        .FirstOrDefaultAsync(m => m.Id == id);
		//    if (product == null)
		//    {
		//        return NotFound();
		//    }

		//    return View(product);
		//}

		//// GET: Products/Create
		//public IActionResult Create()
		//{
		//    return View();
		//}

		//// POST: Products/Create
		//// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		//// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Create([Bind("Id,Name,Price,ImageUrl,LastPurchase,LastSale,IsAvailabe,Stock")] Product product)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        _context.Add(product);
		//        await _context.SaveChangesAsync();
		//        return RedirectToAction(nameof(Index));
		//    }
		//    return View(product);
		//}

		//// GET: Products/Edit/5
		//public async Task<IActionResult> Edit(int? id)
		//{
		//    if (id == null)
		//    {
		//        return NotFound();
		//    }

		//    var product = await _context.Products.FindAsync(id);
		//    if (product == null)
		//    {
		//        return NotFound();
		//    }
		//    return View(product);
		//}

		//// POST: Products/Edit/5
		//// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		//// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ImageUrl,LastPurchase,LastSale,IsAvailabe,Stock")] Product product)
		//{
		//    if (id != product.Id)
		//    {
		//        return NotFound();
		//    }

		//    if (ModelState.IsValid)
		//    {
		//        try
		//        {
		//            _context.Update(product);
		//            await _context.SaveChangesAsync();
		//        }
		//        catch (DbUpdateConcurrencyException)
		//        {
		//            if (!ProductExists(product.Id))
		//            {
		//                return NotFound();
		//            }
		//            else
		//            {
		//                throw;
		//            }
		//        }
		//        return RedirectToAction(nameof(Index));
		//    }
		//    return View(product);
		//}

		//// GET: Products/Delete/5
		//public async Task<IActionResult> Delete(int? id)
		//{
		//    if (id == null)
		//    {
		//        return NotFound();
		//    }

		//    var product = await _context.Products
		//        .FirstOrDefaultAsync(m => m.Id == id);
		//    if (product == null)
		//    {
		//        return NotFound();
		//    }

		//    return View(product);
		//}

		//// POST: Products/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> DeleteConfirmed(int id)
		//{
		//    var product = await _context.Products.FindAsync(id);
		//    _context.Products.Remove(product);
		//    await _context.SaveChangesAsync();
		//    return RedirectToAction(nameof(Index));
		//}

		//private bool ProductExists(int id)
		//{
		//    return _context.Products.Any(e => e.Id == id);
		//}
	}
}
