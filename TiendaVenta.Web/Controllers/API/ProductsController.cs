using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaVenta.Web.Data;

namespace TiendaVenta.Web.Controllers.API
{
	[Route("api/[Controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
			return this.Ok(this.productRepository.GetAllWithUsers());
		}

	}
}
