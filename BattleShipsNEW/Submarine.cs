using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShips
{
    public class Submarine : Ship
    {
        public Submarine(int xpos, int ypos, ShipDirection direction)
            : base(SeaType.Submarine, xpos, ypos, ShipConstants.SUBMARINE_LENGTH, direction, 0, false, "S", "Submarine")
        {
        }
    }
}
