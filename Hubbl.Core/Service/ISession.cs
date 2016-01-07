using System.Collections.Generic;
using System.Collections.ObjectModel;
using Hubbl.Core.Model;

namespace Hubbl.Core.Service
{
    public interface ISession
    {
		HubblUser CurrentUser { get; set;}
		List<PlaylistEntry> Playlist { get; }
    }
}
