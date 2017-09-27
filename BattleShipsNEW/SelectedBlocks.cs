using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShips
{
    public class SelectedBlocks : Ship
    {
        public SelectedBlocks(int xpos, int ypos, int length, ShipDirection direction) : base(SeaType.EmptySea, xpos, ypos, length, direction, 0, false, "+", "Selected")
        {
            foreach (Block b in this.blocks)
            {
                b.selected = true;
            }
        }


        public bool Move(MoveDirection movedirection)
        {
            bool moved = false;

            if (!isOutsideOfSea(movedirection))
            {
                switch (movedirection)
                {
                    case MoveDirection.Left:
                        foreach (Block b in this.blocks)
                            b.xPos -= 1;
                        break;
                    case MoveDirection.Right:
                        foreach (Block b in this.blocks)
                            b.xPos += 1;
                        break;
                    case MoveDirection.Up:
                        foreach (Block b in this.blocks)
                            b.yPos -= 1;
                        break;
                    case MoveDirection.Down:
                        foreach (Block b in this.blocks)
                            b.yPos += 1;
                        break;
                    default:
                        break;
                }
            }

            return moved;
        }

        public void ChangeDirection()
        {
            Block[] oldblocks = this.blocks;

            if (this.direction == ShipDirection.Horizontal)
            {
                blocks = new Block[this.length];
                this.direction = ShipDirection.Vertical;
                foreach (Block b in this.blocks)
                {

                }

            }
            else
            {

            }

        }


    }
}
