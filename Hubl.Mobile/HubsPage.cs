using System;

using Xamarin.Forms;

namespace Hubl.Mobile
{
	public class HubsPage : ContentPage
	{
		public HubsPage ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}


