using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShips
{
    public enum SeaType
    {
        EmptySea = 0,
        Attacked,
        BattleShip,
        Cruiser,
        Submarine,
        Rowing_Boat,
        Hit
        //Selected
    }

    public class Block
    {
        public SeaType blockType { get; set; }
        //public SeaType PrevBlockType { get; set; }

        public int xPos { get; set; }
        public int yPos { get; set; }
        public bool selected { get; set; }
        public string printChar { get; set; }
        public string shipname { get; set; }

        public Block(Block block)
        {
            this.blockType = block.blockType;
            //this.PrevBlockType = SeaType.EmptySea;
            this.xPos = block.xPos;
            this.yPos = block.yPos;

            this.selected = block.selected;

            this.printChar = block.printChar;
            this.shipname = block.shipname;
        }

        public Block(SeaType bt, int xpos, int ypos, string pc, string shipn)
        {
            this.blockType = bt;
            //this.PrevBlockType = SeaType.EmptySea;
            this.xPos = xpos;
            this.yPos = ypos;

            this.selected = false;

            this.printChar = pc;
            this.shipname = shipn;
        }

        public bool isOutsideOfSea()
        {
            bool outside = false;
            int SeaSize = SeaConstants.SEA_SIZE - 1;

            if (this.xPos < 0) outside = true;
            if (this.xPos > SeaSize) outside = true;
            if (this.yPos < 0) outside = true;
            if (this.yPos > SeaSize) outside = true;


            return outside;
        }
        public bool isOutsideOfSea(MoveDirection movedirection)
        {
            bool outside = false;
            int SeaSize = SeaConstants.SEA_SIZE - 1;


            switch (movedirection)
            {
                case MoveDirection.Left:
                    if (this.xPos - 1 < 0) outside = true;
                    break;
                case MoveDirection.Right:
                    if (this.xPos + 1 > SeaSize) outside = true;
                    break;
                case MoveDirection.Up:
                    if (this.yPos - 1 < 0) outside = true;
                    break;
                case MoveDirection.Down:
                    if (this.yPos + 1 > SeaSize) outside = true;
                    break;
                default:
                    break;
            }

            return outside;
        }
    }
}
