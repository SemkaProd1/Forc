using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShips
{
    public class Rowing_Boat : Ship
    {
        public Rowing_Boat(int xpos, int ypos, ShipDirection direction)
            : base(SeaType.Rowing_Boat, xpos, ypos, ShipConstants.ROWING_BOAT_LENGTH, direction, 0, false, "R", "Rowing Boat")
        {
        }
    }
}
