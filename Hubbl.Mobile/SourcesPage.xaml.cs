using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace Hubbl.Mobile
{
	public class Source {
		public string Name {get; set; }
		public Action Function {get; set; }
		public override string ToString ()
		{
			return string.Format ("{0}", Name);
		}
	}
	public partial class SourcesPage : ContentPage
	{
		ObservableCollection<Source> sources = new ObservableCollection<Source>();
		public SourcesPage ()
		{
			InitializeComponent ();
			sources.Add (new Source{Name = "Медиатека", Function = () => {
					
				}
			});
			SourcesView.ItemsSource = sources;
		}
	}
}

