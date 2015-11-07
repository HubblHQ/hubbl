﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace Hubl.Mobile
{	
	public class Song {
		public string Name { get; set; }
		public string Author {get; set; }
	}

	public class State {
		public double Progress {get; set; }
		public string Current { get; set; }
		public DateTime Remaining {get; set; }
	}

	public partial class OneHubPage : ContentPage
	{
		ObservableCollection<Song> songs = new ObservableCollection<Song>();
		public double Progress {get; set; }
		public string Current { get; set; }
		public DateTime Remaining {get; set; }
		public OneHubPage ()
		{
			InitializeComponent ();
			BindingContext = this;
			songs.Add (new Song{ Name = "Lose yourself", Author = "Eminem" });
			songs.Add (new Song{ Name = "Remember the name", Author = "Fort Minor" });
			SongsView.ItemsSource = songs;
			Current = "staff";
			Progress = 0.5;
			Remaining = DateTime.Now;
			SongProgress.ProgressTo (0.5, 1, Easing.CubicOut);
		}
	}
}
