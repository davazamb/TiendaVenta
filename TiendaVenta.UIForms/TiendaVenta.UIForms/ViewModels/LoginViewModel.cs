using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TiendaVenta.UIForms.Views;
using Xamarin.Forms;

namespace TiendaVenta.UIForms.ViewModels
{
	public class LoginViewModel
	{
		public string Email { get; set; }

		public string Password { get; set; }

		public ICommand LoginCommand => new RelayCommand(this.Login);
		public LoginViewModel()
		{
			this.Email = "david.zambrano10@gmail.com";
			this.Password = "123456";
		}

		private async void Login()
		{
			if (string.IsNullOrEmpty(this.Email))
			{
				await Application.Current.MainPage.DisplayAlert("Error", "You must enter an email", "Accept");
				return;
			}

			if (string.IsNullOrEmpty(this.Password))
			{
				await Application.Current.MainPage.DisplayAlert("Error", "You must enter a password", "Accept");
				return;
			}

			if (!this.Email.Equals("david.zambrano10@gmail.com") || !this.Password.Equals("123456"))
			{
				await Application.Current.MainPage.DisplayAlert("Error", "Incorrect user or password", "Accept");
				return;
			}

			//await Application.Current.MainPage.DisplayAlert("Ok", "Fuck yeah!!!", "Accept");
			MainViewModel.GetInstance().Products = new ProductsViewModel();
			await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());

		}
	}

}
