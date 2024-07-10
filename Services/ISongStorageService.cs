using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ISongStorageService
    {
        public ISongStorageService GetService();

        Song SaveSong(string name,Playlist playlist, string description = "");
    
        void DeleteSong(Song song);

        void MoveSong(Song song, string newPath = "");

        String LoadSong(Song song);

        List<Song> SearchSongs(string hint);

        bool ChangeName(Song song, string name);

        bool ChangeDescription(Song song, string description);
    }
}
