using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PlaylistService
    {
        private static PlaylistService instance;

        public static PlaylistService Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new PlaylistService();
                }
                return instance;
            }
        }
        private PlaylistService()
        {

        }

        public Playlist CreatePlaylist(string name,User owner,string description = "",List<Song> songs = null)
        {
            Playlist result = new Playlist();

            return result;
        }

        public bool DeletePlaylist(Playlist target)
        {
            return true;
        }

        public bool AddSongToPlaylist(Playlist target,Song song)
        {
            return true;
        }

        public bool RemoveSongFromPlaylist(Playlist target,Song song)
        {
            return true;
        }

        public bool ChangePLaylistName(Playlist playlist, string name)
        {
            return true;
        }

        public bool ChangePLaylistDescription(Playlist playlist, string description)
        {
            return true;
        }
    }
}
