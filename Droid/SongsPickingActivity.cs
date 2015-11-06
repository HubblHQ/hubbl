
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Hubl.Mobile.Droid
{
	[Activity (Label = "SongsPickingActivity")]			
	public class SongsPickingActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Intent intent = new Intent(Intent.ActionPick, Android.Provider.MediaStore.Audio.Media.ExternalContentUri);
			this.StartActivityForResult(Intent.CreateChooser(intent, "Gallery"), 111);
		}
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if (requestCode == 111 && resultCode == Result.Ok) {
				Console.WriteLine (data.Data);
			}
			base.OnActivityResult (requestCode, resultCode, data);
		}
	}
}

