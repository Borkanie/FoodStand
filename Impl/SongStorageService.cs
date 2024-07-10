using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SongStorageService
    {
        private static SongStorageService instance;

        public static SongStorageService Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new SongStorageService();
                }
                return instance;
            }
        }

        private SongStorageService()
        {
            
        }

        public FileStream GetSong(Song target)
        {
            return null;
        }

        public bool DeleteSong(Song target)
        {
            return true;
        }

        public bool ModeSong(Song target, Uri newUri) 
        {
            return true;
        }
    }
}
