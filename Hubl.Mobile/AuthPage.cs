using System;

using Xamarin.Forms;

namespace Hubl.Mobile
{
	public class AuthPage : ContentPage
	{
		public AuthPage ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}


