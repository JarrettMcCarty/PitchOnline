using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchOnline.Core
{
    public class Team
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
    }
}
