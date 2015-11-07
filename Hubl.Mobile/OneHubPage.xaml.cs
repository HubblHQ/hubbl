using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Autofac;
using Xamarin.Forms;
using Hubl.Core.Model;
using Hubl.Core.Service;

namespace Hubl.Mobile
{	
	public class Song {
		public Track Track { get; set; }
		public bool Selected {get; set; }
		public override string ToString ()
		{
			return Track.Name;
		}
	}

	public class State {
		public double Progress {get; set; }
		public string Current { get; set; }
		public DateTime Remaining {get; set; }
	}

	public partial class OneHubPage : ContentPage
	{
		ObservableCollection<Song> songs = new ObservableCollection<Song>();
		User hub;
		public OneHubPage (User hub)
		{
			this.hub = hub;
			InitializeComponent ();
			BindingContext = this;
			//songs.Add (new Song{ Name = "Lose yourself", Author = "Eminem" });
			//songs.Add (new Song{ Name = "Remember the name", Author = "Fort Minor" });
			SongsView.ItemsSource = App.Container.Resolve<ISession> ().Playlist;
			CurrentSong.Text = "Current song";
			ElapsedTime.Text = TimeSpan.FromSeconds (100).ToString (@"mm\:ss");
			RemainingTime.Text = TimeSpan.FromSeconds (150).ToString (@"mm\:ss");
			SongProgress.ProgressTo (100.0/250, 5, Easing.CubicOut);
			ToolbarItems.Add (new ToolbarItem("Add", "", async () => {
				Navigation.PushAsync (new SongsPage(hub));	
			}));
		}
	}
}

