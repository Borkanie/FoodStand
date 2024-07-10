using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPlaylistManagementService
    {
        public IPlaylistManagementService GetSeervice();

        Playlist CreatePlaylist(string name, string description, User owner);

        void AddSong(Playlist playlist, Song song);

        void RemoveSong(Playlist playlist, Song song);

        bool ChangeName(Playlist playlist, string name);

        bool ChangeDescription(Playlist playlist, string description);

    }
}
