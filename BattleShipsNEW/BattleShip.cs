using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShips
{
    public class BattleShip : Ship
    {
        public BattleShip(int xpos, int ypos, ShipDirection direction) 
            : base(SeaType.BattleShip, xpos, ypos, ShipConstants.BATTLESHIP_LENGTH, direction, 0, false, "B", "Battleship")
        {
        }

    }
}
