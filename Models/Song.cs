using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Song
    {
        public Song()
        {
            
        }

        public Guid Id { get; set; }

        public string Name{ get; set; }

        public string Description { get; set; } = "";

        public Uri FilePath { get; set; }
    }
}
