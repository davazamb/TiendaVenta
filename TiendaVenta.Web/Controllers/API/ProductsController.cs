using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaVenta.Web.Data;

namespace TiendaVenta.Web.Controllers.API
{
	[Route("api/[Controller]")]
	public class ProductsController : Controller
	{
		private readonly IProductRepository productRepository;

		public ProductsController(IProductRepository productRepository)
		{
			this.productRepository = productRepository;
		}

		[HttpGet]
		public IActionResult GetProducts()
		{
			return this.Ok(this.productRepository.GetAll());
		}

	}
}
