using System.Collections.Generic;
using Hubl.Core.Model;
using System.Collections.ObjectModel;

namespace Hubl.Core.Service
{
    public interface ISession
    {
		User CurrentUser { get; set;}
		List<PlaylistEntry> Playlist { get; }
    }
}
