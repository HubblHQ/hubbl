using Hubl.Core.Model;
using System.Collections.ObjectModel;

namespace Hubl.Core.Service
{
    public interface ISession
    {
		User CurrentUser { get; set;}
		ObservableCollection<Track> Playlist { get; }
    }
}
