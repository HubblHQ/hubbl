using PropertyChanged;

namespace Hubbl.Mobile.Models
{
	[ImplementPropertyChanged]
	public class Hub
	{
		public string Name { get; set; }
		public string Admin { get; set; }
		public string CurrentSong { get; set; }
	}
}
