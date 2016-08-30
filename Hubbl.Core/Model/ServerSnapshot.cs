using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Hubbl.Core.Model
{
    public class ServerSnapshot
    {
        [DataMember]
        public IEnumerable<PlaylistEntry> Playlist { get; set; }

        [DataMember]
        public PlaylistEntry PlayingTrack { get; set; }

        [DataMember]
        public ServerStatus Status { get; set; }
    }
}