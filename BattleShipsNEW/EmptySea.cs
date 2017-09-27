using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShips
{
    class EmptySea : Block
    {
        public EmptySea(int xpos, int ypos)
            : base(SeaType.EmptySea, xpos, ypos, "~", "Empty Sea")
        {
        }
    }
}
