using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Autofac;

using Xamarin.Forms;
using Hubl.Core.Service;

namespace Hubl.Mobile
{
	public partial class NewHubPage : ContentPage
	{
		ObservableCollection<Song> songs = new ObservableCollection<Song>();
		public NewHubPage ()
		{
			InitializeComponent ();

			Done.Clicked += Done_Clicked;
			FirstSongs.ItemsSource = songs;
			Name.TextChanged += Name_TextChanged;
		}

		void Name_TextChanged (object sender, TextChangedEventArgs e)
		{
			App.Container.Resolve<ISession> ().CurrentUser.Hub = e.NewTextValue;
		}

		void Done_Clicked (object sender, EventArgs e)
		{
			App.Container.Resolve<ISession> ().CurrentUser.IsHub = true;
			Navigation.PopAsync ();
		}
	}
}

