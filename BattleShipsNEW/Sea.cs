using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShips
{
    public class SeaConstants
    {
        public const int SEA_SIZE = 10;
        public const int SEA_SPACING = 2;
    }


    public class Sea
    {
        public delegate void BlockChangedEventHandler(Block block);
        public event BlockChangedEventHandler BlockChanged;

        public Block[,] blocks { get; set; }
        public SeaType curState { get; set; }
        public List<Ship> shipsList { get; set; }
        //public SeaType prevState { get; set; }
        public int consoleXPos { get; set; }
        public int consoleYPos { get; set; }
        public int shipLeft { get; set; }
        public int attacksLeft { get; set; }

        //constructor
        public Sea()
        {
            this.blocks = new Block[SeaConstants.SEA_SIZE, SeaConstants.SEA_SIZE];

            for (int i = 0; i < SeaConstants.SEA_SIZE; i++)
            {
                for (int j = 0; j < SeaConstants.SEA_SIZE; j++)
                {
                    this.blocks[i, j] = new EmptySea(j, i);
                }
            }

            this.shipsList = new List<Ship>();
            //this.curState = SeaType.EmptySea;
            //this.ShipCoords = new List<string>();
            this.consoleXPos = 0;
            this.consoleYPos = 0;

            this.shipLeft = (ShipConstants.TOTAL_BATTLESHIPS * ShipConstants.BATTLESHIP_LENGTH) + (ShipConstants.TOTAL_CRUISERS * ShipConstants.CRUISER_LENGTH) + (ShipConstants.TOTAL_SUBMARINES * ShipConstants.SUBMARINE_LENGTH) + (ShipConstants.TOTAL_ROWING_BOATS * ShipConstants.ROWING_BOAT_LENGTH);
            this.attacksLeft = SeaConstants.SEA_SIZE * SeaConstants.SEA_SIZE;
        }

        public bool AddShip(Ship ship)
        {
            bool AddShip = true;
            //check if its outside
            if (!ship.isOutsideOfSea())
            {
                if (shipsList.Count > 0)
                {
                    //check if it intersects any ships
                    foreach (Ship s in shipsList)
                    {
                        if (s.isShipIntersecting(ship))
                        {
                            AddShip = false;
                            break;
                        }
                    }

                }
                //else
                //we add it since it's the first ship

                if (AddShip)
                {
                    shipsList.Add(ship);
                    PlaceShipInSea(ship);
                }

            }
            else
            {
                AddShip = false;
            }

            return AddShip;
        }

        private void PlaceShipInSea(Ship ship)
        {
            foreach (Block b in ship.blocks)
            {
                this.blocks[b.yPos, b.xPos] = new Block(b);
                this.BlockChanged(this.blocks[b.yPos, b.xPos]);
            }

        }

        public void SelectBlocks(SelectedBlocks selected)
        {
            foreach (Block b in selected.blocks)
            {
                this.blocks[b.yPos, b.xPos].selected = true;
                this.BlockChanged(this.blocks[b.yPos, b.xPos]);
            }

        }

        public void UnSelectBlocks(SelectedBlocks selected)
        {
            
            foreach (Block b in selected.blocks)
            {
                this.blocks[b.yPos, b.xPos].selected = false;
                this.BlockChanged(this.blocks[b.yPos, b.xPos]);
            }

        }

        public void ChangeDirection(ref SelectedBlocks selected)
        {
            int SeaSize = SeaConstants.SEA_SIZE - 1;
            int cxpos = selected.blocks[0].xPos;
            int cypos = selected.blocks[0].yPos;

            if (selected.direction == ShipDirection.Horizontal)
            {
                if (!(cypos + selected.length - 1 > SeaSize))
                {
                    UnSelectBlocks(selected);

                    selected.direction = ShipDirection.Vertical;

                    selected.blocks = new Block[selected.length];
                    for (int i = 0; i < selected.length; i++)
                    {
                        selected.blocks[i] = new Block(SeaType.EmptySea, cxpos, cypos + i, "+", "Empty Sea");
                    }

                    SelectBlocks(selected);
                }
            }
            else
            {
                if (!(cxpos + selected.length - 1 > SeaSize))
                {
                    UnSelectBlocks(selected);

                    selected.direction = ShipDirection.Horizontal;

                    selected.blocks = new Block[selected.length];
                    for (int i = 0; i < selected.length; i++)
                    {
                        selected.blocks[i] = new Block(SeaType.EmptySea, cxpos+i, cypos, "+", "Empty Sea");
                    }

                    SelectBlocks(selected);
                }

            }
        }

        //will return if it hit a ship
        public bool AttackPos(int xpos, int ypos)
        {
            bool hitship = false;

            foreach (Ship s in shipsList)
            {
                if (s.isHit(xpos, ypos))
                {
                    hitship = true;

                    this.shipLeft--;

                    this.blocks[ypos, xpos].blockType = SeaType.Hit;
                    this.blocks[ypos, xpos].printChar = "H";
                    this.BlockChanged(this.blocks[ypos, xpos]);
                    
                    //check if sunk
                    if (s.sunk)
                    {
                        //sunk
                        this.shipsList.Remove(s);
                        break;
                    }
                }

            }

            if (!hitship)
            {
                this.blocks[ypos, xpos].blockType = SeaType.Attacked;
                this.blocks[ypos, xpos].printChar = "A";
                this.BlockChanged(this.blocks[ypos, xpos]);
            }

            this.attacksLeft--;

            return hitship;
        }
        public void DeleteShip(Ship ship)
        {
            foreach (Block b in ship.blocks)
                blocks[b.yPos, b.xPos] = new EmptySea(b.yPos, b.xPos);

            shipsList.Remove(ship);
        }
        

        public bool MoveShip(MoveDirection movedirection, Ship ship)
        {
            bool moved = false;

            DeleteShip(ship);

            switch (movedirection)
            {
                case MoveDirection.Left:

                    break;
                case MoveDirection.Right:
                    break;
                case MoveDirection.Up:
                    break;
                case MoveDirection.Down:
                    break;
                default:
                    break;
            }

            return moved;

        }
        

    }
}
