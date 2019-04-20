namespace TiendaVenta.Web.Data
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Entities;

	public class Repository
	{
		private readonly DataContext context;

		//Constructor de la clase
		public Repository(DataContext context)
		{
			this.context = context;
		}

		//Metodos de carga los productos en lista enumerable ordebada por nombres
		public IEnumerable<Product> GetProducts()
		{
			return this.context.Products.OrderBy(p => p.Name);
		}
		//Metodo de carga de producto con parametro ID
		public Product GetProduct(int id)
		{
			return this.context.Products.Find(id);
		}
		//Agregar Producto
		public void AddProduct(Product product)
		{
			this.context.Products.Add(product);
		}
		//Actualiza producto con un modelo
		public void UpdateProduct(Product product)
		{
			this.context.Update(product);
		}
		//Elimina lista de Producto
		public void RemoveProduct(Product product)
		{
			this.context.Products.Remove(product);
		}
		//Metodo asincrono para guarda todo lo que contenta el contexto
		public async Task<bool> SaveAllAsync()
		{
			return await this.context.SaveChangesAsync() > 0;
		}
		//Metodo que valida si el producto existe en el contexto, ID primary Key
		public bool ProductExists(int id)
		{
			return this.context.Products.Any(p => p.Id == id);
		}

	}
}
