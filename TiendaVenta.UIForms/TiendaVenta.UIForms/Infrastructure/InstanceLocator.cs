using System;
using System.Collections.Generic;
using System.Text;
using TiendaVenta.UIForms.ViewModels;

namespace TiendaVenta.UIForms.Infrastructure
{
	public class InstanceLocator
	{
		public MainViewModel Main { get; set; }

		public InstanceLocator()
		{
			this.Main = new MainViewModel();
		}
	}

}
