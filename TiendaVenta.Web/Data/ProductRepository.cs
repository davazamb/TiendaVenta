using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaVenta.Web.Data.Entities;

namespace TiendaVenta.Web.Data
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		public ProductRepository(DataContext context) : base(context)
		{
		}
	}

}
