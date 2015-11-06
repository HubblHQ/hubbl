using System;

using Xamarin.Forms;

namespace Hubl.Mobile
{
	public class User {
		public string Name {get; set; }
		public Guid Guid {get; set; }
	}
	public class App : Application
	{		
		public static User User {get; set; }
		public App ()
		{	
			MainPage = new NavigationPage (new Hubl.Mobile.NickPage ());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

