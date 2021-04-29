using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchOnline.Core
{
    public class Game
    {
        public int[] Deck { get; set; }
        public int[] Table { get; set; }
        public int Bid { get; set; }
        public Player BiddingPlayer { get; set; }
    }
}
