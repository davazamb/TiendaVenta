using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using TiendaVenta.Common.Models;
using TiendaVenta.Common.Services;
using Xamarin.Forms;

namespace TiendaVenta.UIForms.ViewModels
{
	public class ProductsViewModel : BaseViewModel
	{
		private readonly ApiService apiService;

		private ObservableCollection<Product> products;
		private bool isRefreshing;


		public ObservableCollection<Product> Products
		{
			get { return this.products; }
			set { this.SetValue(ref this.products, value); }
		}
		public bool IsRefreshing
		{
			get => this.isRefreshing;
			set => this.SetValue(ref this.isRefreshing, value);
		}

		public ICommand RefreshCommand => new RelayCommand(this.LoadProducts);

		public ProductsViewModel()
		{
			this.apiService = new ApiService();
			this.LoadProducts();
		}

		private async void LoadProducts()
		{

			this.IsRefreshing = true;

			var response = await this.apiService.GetListAsync<Product>(
				"http://dzambranob-001-site1.dtempurl.com",
				"/api",
				"/Products");
			if (!response.IsSuccess)
			{
				await Application.Current.MainPage.DisplayAlert(
					"Error",
					response.Message,
					"Accept");
				this.IsRefreshing = false;

				return;
			}

			var products = (List<Product>)response.Result;
			this.Products = new ObservableCollection<Product>(products);
			this.IsRefreshing = false;

		}

	}
}
