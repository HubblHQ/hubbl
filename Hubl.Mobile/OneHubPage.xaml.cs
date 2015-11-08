using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Autofac;
using Xamarin.Forms;
using Hubl.Core.Model;
using Hubl.Core.Service;
using Autofac.Core.Lifetime;
using System.Linq;
using System.Diagnostics;

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
		User hub;
		bool isPlaying;
		ObservableCollection<Track> tracks;
		Track currentTrack;
		public OneHubPage (User hub)
		{
			this.hub = hub;
			InitializeComponent ();
			BindingContext = this;
			Play.Clicked += Play_Clicked;
			tracks = App.Container.Resolve<ISession> ().Playlist;
			SongsView.ItemsSource = tracks;
			tracks.CollectionChanged += Tracks_CollectionChanged;

			//CurrentSong.Text = "Current song";
			//ElapsedTime.Text = TimeSpan.FromSeconds (100).ToString (@"mm\:ss");
			//RemainingTime.Text = TimeSpan.FromSeconds (150).ToString (@"mm\:ss");
			//SongProgress.ProgressTo (100.0/250, 5, Easing.CubicOut);
			ToolbarItems.Add (new ToolbarItem("Add", "", async () => {
				Navigation.PushAsync (new SongsPage(hub));	
			}));
		}

		void Tracks_CollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (tracks.Count == 1) {
				currentTrack = tracks.First ();
				DependencyService.Get<IMusicPlayerBackend> ().PlayTrack (currentTrack);
				var player = DependencyService.Get<IMusicPlayerBackend> ();
			}
		}

		void Play_Clicked (object sender, EventArgs e)
		{
			if (isPlaying) {
				DependencyService.Get<IMusicPlayerBackend> ().PauseCurrentTrack ();
			} else {
				DependencyService.Get<IMusicPlayerBackend> ().PlayTrack (currentTrack);
			}
		}
	}
}

