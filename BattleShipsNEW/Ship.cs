using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShips
{
    public class ShipConstants
    {
        public const int BATTLESHIP_LENGTH = 1;
        public const int CRUISER_LENGTH = 2;
        public const int SUBMARINE_LENGTH = 3;
        public const int ROWING_BOAT_LENGTH = 4;

        public const int TOTAL_BATTLESHIPS = 4;
        public const int TOTAL_CRUISERS = 3;
        public const int TOTAL_SUBMARINES = 2;
        public const int TOTAL_ROWING_BOATS = 1;
    }

    public enum ShipDirection
    {
        Horizontal = 0,
        Vertical
    }

    public enum MoveDirection
    {
        Left = 0,
        Right,
        Up,
        Down
    }

    public class Ship
    {

        
        public SeaType shipType { get; set; }
        public ShipDirection direction { get; set; }
        public Block[] blocks { get; set; }

        public string name { get; set; }
        public int timesHit { get; set; }
        public int length { get; set; }
        public bool sunk { get; set; }


        //constructor
        public Ship(SeaType shipType, int xpos, int ypos, int length, ShipDirection direction, int timesHit, bool sunk, string printchar, string name)
        {
            this.shipType = shipType;

            this.blocks = new Block[length];

            for (int i = 0; i < length; i++)
            {
                if (direction == ShipDirection.Horizontal)
                    blocks[i] = new Block(shipType, xpos + i , ypos, printchar , name);
                else
                    blocks[i] = new Block(shipType, xpos, ypos + i, printchar, name);
            }

            this.length = length;

            this.direction = direction;
            this.timesHit = timesHit;

            this.sunk = sunk;
        }

        public bool isHit(int xposH, int yposH)
        {
            bool hit = false;

            foreach (Block b in this.blocks)
            {
                if (b.xPos == xposH && b.yPos == yposH)
                {
                    hit = true;
                    if (timesHit < this.length)
                    {
                        this.timesHit++;
                    }

                    if (timesHit >= this.length)
                    {
                        this.sunk = true;
                    }
                }
            }
            return hit;

        }//isHit

        public bool isOutsideOfSea()
        {
            bool outside = false;

            foreach (Block b in blocks)
                if (b.isOutsideOfSea())
                    outside = true;

            return outside;
        }
        public bool isOutsideOfSea(MoveDirection movedirection)
        {
            bool outside = false;

            foreach (Block b in blocks)
                if (b.isOutsideOfSea(movedirection))
                    outside = true;

            return outside;
        }


        public bool isShipIntersecting(Ship ship)
        {
            bool intersecting = false;

            foreach (Block b in this.blocks)
            {
                foreach (Block b2 in ship.blocks)
                {
                    if (b.xPos == b2.xPos && b.yPos == b2.yPos)
                    {
                        intersecting = true;
                        break;
                    }
                }
                if (intersecting) break;
            }

            return intersecting;
        }


    }
}
