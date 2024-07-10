using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SongMetadataService
    {
        private static SongMetadataService instance;

        public static SongMetadataService Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new SongMetadataService();
                }
                return instance;
            }
        }

        private SongMetadataService()
        {
            
        }

        public Song SaveSong()
        {
            Song result = new Song();

            return result;
        }

        public Song SaveSong(string name, string description)
        {
            Song result = new Song();

            return result;
        }

        public List<Song> Find(string query)
        {
            List<Song> result = new List<Song>();

            return result;
        }

        public bool RemoveSong(Song target)
        {


            return true;
        }

        public bool UpdateSongName(Song song,string name)
        {
            return true;
        }

        public bool UpdateSongDescription(Song song, string description)
        {
            return true;
        }

        public bool LikeSong(Song song, User user)
        {
            return true;
        }

        public bool DislikeSong(Song song, User user)
        {
            return true;
        }
    }
}
