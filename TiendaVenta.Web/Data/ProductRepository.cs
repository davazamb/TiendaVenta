﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaVenta.Web.Data.Entities;

namespace TiendaVenta.Web.Data
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		private readonly DataContext context;

		public ProductRepository(DataContext context) : base(context)
		{
			this.context = context;
		}

		public IQueryable GetAllWithUsers()
		{
			return this.context.Products.Include(p => p.User).OrderBy(p => p.Name);
		}
	}

}
