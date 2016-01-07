using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Autofac;

using Xamarin.Forms;
using Hubbl.Core.Service;
using Hubbl.Core.Messages;

namespace Hubbl.Mobile
{
	public partial class NewHubPage : ContentPage
	{
		public NewHubPage ()
		{
			InitializeComponent ();

			Done.Clicked += Done_Clicked;

			Name.TextChanged += Name_TextChanged;
		}

		void Name_TextChanged (object sender, TextChangedEventArgs e)
		{
			App.Container.Resolve<ISession> ().CurrentUser.Hub = e.NewTextValue;
		}

		async void Done_Clicked (object sender, EventArgs e)
		{
			App.Container.Resolve<ISession> ().CurrentUser.IsHub = true;
			var msg = new HelloMessage (App.Container.Resolve<ISession> ().CurrentUser);
			App.Router.Publish (msg).Run ();
			Navigation.PopAsync ();
		}
	}
}

