using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShips
{
    public class Cruiser : Ship
    {
        public Cruiser(int xpos, int ypos, ShipDirection direction)
            : base(SeaType.Cruiser, xpos, ypos, ShipConstants.CRUISER_LENGTH, direction, 0, false, "C", "Cruiser")
        {
        }
    }
}
