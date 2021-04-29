using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchOnline.Core
{
    public class LobbyListInfo
    {
        
        public string ShortId { get; set; }
        
        public string LobbyName { get; set; }
        
        public string LobbyStatus { get; set; }
        public int LobbyJoin { get; set; }
        
        public int Dealer { get; set; }
        
        public bool IsPrivate { get; set; }

        public bool IsJoinable { get; set; }

        public string JoiningUser { get; set; }
        public List<string> Users { get; set; }
        //public Game[] Games { get; set; }
        //public Team Team1 { get; set; }
        //public Team Team2 { get; set; }
        
        public int Players { get; set; }
        //public List<byte[]> Avatars { get; set; }

    }
}
