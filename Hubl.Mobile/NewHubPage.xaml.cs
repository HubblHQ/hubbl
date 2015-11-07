using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace Hubl.Mobile
{
	public partial class NewHubPage : ContentPage
	{
		ObservableCollection<Song> songs = new ObservableCollection<Song>();
		public NewHubPage ()
		{
			InitializeComponent ();
			Source.Clicked += Source_Clicked;
			Done.Clicked += Done_Clicked;
			FirstSongs.ItemsSource = songs;
		}

		void Done_Clicked (object sender, EventArgs e)
		{
			
		}

		void Source_Clicked (object sender, EventArgs e)
		{
			var picker = DependencyService.Get<ISongsPicker> ();
			picker.StartPicking ();
		}
	}
}

