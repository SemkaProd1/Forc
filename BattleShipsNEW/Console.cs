using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BattleShips
{
    public class ConsoleConstants
    {
        public const String CONSOLE_TITLE = "Battleships Game - By ILYA";

        //colours;
        public const ConsoleColor CLR_BACKGROUND = ConsoleColor.Black;
        public const ConsoleColor CLR_FOREGROUND = ConsoleColor.White;
        // b = background, f = foreground
        public const ConsoleColor CLR_SELECTED_SQUARE_B = ConsoleColor.Yellow;
        public const ConsoleColor CLR_SELECTED_SQUARE_F = ConsoleColor.Red;

        public const ConsoleColor CLR_SQUARE_CONNCTR_B = ConsoleColor.DarkGray;
        public const ConsoleColor CLR_SQUARE_CONNCTR_F = ConsoleColor.Black;

        public const ConsoleColor CLR_NORMAL_SQUARE_B = ConsoleColor.White;
        public const ConsoleColor CLR_NORMAL_SQUARE_F = ConsoleColor.Black;

        public const ConsoleColor CLR_ATTACKED_SQUARE_B = ConsoleColor.DarkRed;
        public const ConsoleColor CLR_ATTACKED_SQUARE_F = ConsoleColor.White;

        public const ConsoleColor CLR_HIT_SQUARE_B = ConsoleColor.Red;
        public const ConsoleColor CLR_HIT_SQUARE_F = ConsoleColor.White;

        public const ConsoleColor CLR_BORDER_B = ConsoleColor.DarkGreen;

        public const ConsoleColor CLR_MESSAGE_USER_B = ConsoleColor.DarkGray;
        public const ConsoleColor CLR_MESSAGE_COMPUTER_B = ConsoleColor.DarkRed;
        public const ConsoleColor CLR_MESSAGE_SYSTEM_B = ConsoleColor.DarkYellow;

        public const int BUFFERHEIGHT = 30;
        public const int BUFFERWIDTH = 80;

        public const int WINDOWHEIGHT = 30;
        public const int WINDOWWIDTH = 80;

        public const int MAXMESSAGES = 8;

        public const bool DEBUG = true;
    }

    public class GameConsole
    {

        public Sea UserSea { get; set; }
        public Sea ComputerSea { get; set; }

        public int SeaWidth { get; set; }

        public int BufferHeight { get; set; }
        public int BufferWidth { get; set; }

        public int WindowHeight { get; set; }
        public int WindowWidth { get; set; }

        public string title { get; set; }

        public ArrayList messages { get; set; }

        public Messages m { get; set; }

        public GameConsole()
        {
            this.BufferHeight = ConsoleConstants.BUFFERHEIGHT;
            this.BufferWidth = ConsoleConstants.BUFFERWIDTH;

            if (ConsoleConstants.WINDOWHEIGHT <= Console.LargestWindowHeight && ConsoleConstants.WINDOWWIDTH <= Console.LargestWindowWidth)
            {
                this.WindowHeight = ConsoleConstants.WINDOWHEIGHT;
                this.WindowWidth = ConsoleConstants.WINDOWWIDTH;
            }
            else
            {
                this.WindowHeight = Console.LargestWindowHeight;
                this.WindowWidth = Console.LargestWindowWidth;
            }

            this.title = ConsoleConstants.CONSOLE_TITLE;


            this.SeaWidth = SeaConstants.SEA_SPACING + (SeaConstants.SEA_SPACING * SeaConstants.SEA_SIZE) + 1;

            this.ComputerSea = new Sea();
            this.ComputerSea.BlockChanged += new Sea.BlockChangedEventHandler(ComputerSea_BlockChanged);
            this.ComputerSea.consoleXPos = SeaWidth + 1;

            this.UserSea = new Sea();
            this.UserSea.BlockChanged += new Sea.BlockChangedEventHandler(UserSea_BlockChanged);

            this.messages = new ArrayList();

            this.m = new Messages();

            this.Initialise();
        }


        public GameConsole(int bh, int bw, int wh, int ww, string title)
        {
            this.BufferHeight = bh;
            this.BufferWidth = bw;

            this.WindowHeight = wh;
            this.WindowWidth = ww;

            this.title = ConsoleConstants.CONSOLE_TITLE;

            this.Initialise();
        }

        public void Initialise()
        {
          
                Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;



            Console.WindowHeight = this.WindowHeight;
            Console.WindowWidth = this.WindowWidth;

            Console.Title = this.title;

            Console.Clear();

            ResetColours();
        }

        private void ComputerSea_BlockChanged(Block block)
        {
            printOnPos(ComputerSea, PrintBlock(block, ConsoleConstants.DEBUG), block.xPos, block.yPos);
            ResetColours();
        }

        private void UserSea_BlockChanged(Block block)
        {
            printOnPos(UserSea, PrintBlock(block, true), block.xPos, block.yPos);
            ResetColours();
        }

        public void SetBackground(ConsoleColor clr)
        {
            Console.BackgroundColor = clr;
        }
        public void SetForeground(ConsoleColor clr)
        {
            Console.ForegroundColor = clr;
        }

        public void ResetColours()
        {
            SetBackground(ConsoleConstants.CLR_BACKGROUND);
            SetForeground(ConsoleConstants.CLR_FOREGROUND);
        }

        public void Write(string str)
        {
            Console.Write(str);
        }

        internal void PlaceComputerShip(object sh)
        {
            throw new NotImplementedException();
        }

        public void Write(string str, int xpos, int ypos)
        {
            int oldxpos;
            int oldypos;

            oldxpos = Console.CursorLeft;
            oldypos = Console.CursorTop;

            Console.SetCursorPosition(xpos, ypos);
            Console.Write(str);
            Console.SetCursorPosition(oldxpos, oldypos);
        }

        public void ConsoleNewLine(int xpos)
        {
            Console.WriteLine();
            Console.SetCursorPosition(xpos, Console.CursorTop);
        }

        public void WriteFormatted(string str)
        {

            Console.Write("{0," + SeaConstants.SEA_SPACING + "}", str);

        }

        public void WriteFormatted(int str)
        {

            Console.Write("{0," + SeaConstants.SEA_SPACING + "}", str);

        }
        //false = computer sea
        //true = user sea
        public void PrintSea(bool usersea)
        {
            Sea sea;
            bool debug = true;

            if (usersea)
            {
                sea = UserSea;
            }
            else
            {
                sea = ComputerSea;
                debug = ConsoleConstants.DEBUG;
            }

            if ((sea.consoleXPos + SeaConstants.SEA_SIZE < this.BufferWidth) && (sea.consoleYPos + SeaConstants.SEA_SIZE < this.BufferHeight))
            {
                int i, j;


                SetBackground(ConsoleConstants.CLR_BORDER_B);

                Console.SetCursorPosition(sea.consoleXPos, sea.consoleYPos);
                WriteFormatted(" ");
                Console.SetCursorPosition(sea.consoleXPos, sea.consoleYPos + 1);
                WriteFormatted(" ");

                //print top numbers and border
                for (i = 0; i < SeaConstants.SEA_SIZE; i++)
                {
                    Console.SetCursorPosition(sea.consoleXPos + (i * SeaConstants.SEA_SPACING) + SeaConstants.SEA_SPACING, sea.consoleYPos);

                    WriteFormatted("-");

                    if (i == SeaConstants.SEA_SIZE - 1)
                        Write(" ");

                    ConsoleNewLine(sea.consoleXPos + (i * SeaConstants.SEA_SPACING) + SeaConstants.SEA_SPACING);

                    WriteFormatted(i);

                    if (i == SeaConstants.SEA_SIZE - 1)
                        Write(" ");
                }

                ResetColours();

                //print the sea
                for (i = 0; i < SeaConstants.SEA_SIZE; i++)
                {
                    ConsoleNewLine(sea.consoleXPos);

                    //print numbers next to row
                    SetBackground(ConsoleConstants.CLR_BORDER_B);
                    WriteFormatted(i);
                    ResetColours();

                    for (j = 0; j < SeaConstants.SEA_SIZE; j++)
                    {

                        WriteFormatted(PrintBlock(sea.blocks[i, j], debug));
                        ResetColours();
                    }//end j for

                    //border
                    SetBackground(ConsoleConstants.CLR_BORDER_B);
                    Write(" ");
                    ResetColours();

                }//end i for

                SetBackground(ConsoleConstants.CLR_BORDER_B);
                ConsoleNewLine(sea.consoleXPos);
                for (i = 0; i < SeaConstants.SEA_SIZE + 1; i++)
                    WriteFormatted(" ");
                Write(" ");

                ResetColours();
            }

        }//end printsea

        public void printOnPos(Sea sea, string str, int xpos, int ypos)
        {
            //xpos += 1;
            Write(str, sea.consoleXPos + (SeaConstants.SEA_SPACING + (xpos * SeaConstants.SEA_SPACING) + (SeaConstants.SEA_SPACING - 1)), sea.consoleYPos + (2 + ypos));
        }

        public void SetOnPos(Sea sea, int xpos, int ypos)
        {
            Console.SetCursorPosition(sea.consoleXPos + (SeaConstants.SEA_SPACING + (xpos * SeaConstants.SEA_SPACING) + (SeaConstants.SEA_SPACING - 1)), sea.consoleYPos + (2 + ypos));
        }

        private string PrintBlock(Block b, bool debug)
        {
            string temp;

            if (!b.selected)
            {
                if (b.blockType == SeaType.Attacked)
                {
                    SetBackground(ConsoleConstants.CLR_ATTACKED_SQUARE_B);
                    SetForeground(ConsoleConstants.CLR_ATTACKED_SQUARE_F);
                }
                else if (b.blockType == SeaType.Hit)
                {
                    SetBackground(ConsoleConstants.CLR_HIT_SQUARE_B);
                    SetForeground(ConsoleConstants.CLR_HIT_SQUARE_F);
                }
                else
                {
                    if (b.blockType == SeaType.EmptySea)
                    {
                        ResetColours();
                    }

                    if (debug)
                    {
                        if (b.blockType != SeaType.EmptySea)
                        {
                            SetBackground(ConsoleConstants.CLR_NORMAL_SQUARE_B);
                            SetForeground(ConsoleConstants.CLR_NORMAL_SQUARE_F);
                        }
                    }
                    else
                    {
                        b.printChar = "~";
                    }
                }
                temp = b.printChar;
            }
            else
            {
                SetBackground(ConsoleConstants.CLR_SELECTED_SQUARE_B);
                SetForeground(ConsoleConstants.CLR_SELECTED_SQUARE_F);
                temp = "+";
            }

            return temp;
        }

        public void displayColourKey(int startX, int startY)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(startX, startY);

            Console.WriteLine("Colour/Square Key");

            //SELECTED SQUARE
            Console.SetCursorPosition(startX, startY + 1);

            Console.ForegroundColor = ConsoleConstants.CLR_SELECTED_SQUARE_F;
            Console.BackgroundColor = ConsoleConstants.CLR_SELECTED_SQUARE_B;
            Console.Write("+");

            ResetColours();

            Console.WriteLine(" - Selected square");

            //SHIP
            Console.SetCursorPosition(startX, startY + 2);

            Console.ForegroundColor = ConsoleConstants.CLR_NORMAL_SQUARE_F;
            Console.BackgroundColor = ConsoleConstants.CLR_NORMAL_SQUARE_B;
            Console.Write("B");

            ResetColours();

            Console.WriteLine(" - Ship");

            //ATTACKED
            Console.SetCursorPosition(startX, startY + 3);

            Console.ForegroundColor = ConsoleConstants.CLR_ATTACKED_SQUARE_F;
            Console.BackgroundColor = ConsoleConstants.CLR_ATTACKED_SQUARE_B;
            Console.Write("A");

            ResetColours();

            Console.WriteLine(" - Attacked sea");

            //HIT
            Console.SetCursorPosition(startX, startY + 4);

            Console.ForegroundColor = ConsoleConstants.CLR_HIT_SQUARE_F;
            Console.BackgroundColor = ConsoleConstants.CLR_HIT_SQUARE_B;
            Console.Write("H");

            ResetColours();

            Console.WriteLine(" - Hit sea");


            //SEA
            Console.SetCursorPosition(startX, startY + 5);
            Console.WriteLine("~ - Sea");

            //MESSSAGES KEY
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(startX, startY + 6);
            Console.WriteLine("Message Colour Key");

            //USER
            Console.SetCursorPosition(startX, startY + 7);

            Console.BackgroundColor = ConsoleConstants.CLR_MESSAGE_USER_B;
            Console.Write("USER");

            ResetColours();

            Console.WriteLine(" - What you have done");
            //COMPUTER
            Console.SetCursorPosition(startX, startY + 8);

            Console.BackgroundColor = ConsoleConstants.CLR_MESSAGE_COMPUTER_B;
            Console.Write("COMPUTER");

            ResetColours();

            Console.WriteLine(" - Computer events");
            //SYSTEM
            Console.SetCursorPosition(startX, startY + 9);

            Console.BackgroundColor = ConsoleConstants.CLR_MESSAGE_SYSTEM_B;
            Console.Write("SYSTEM");

            ResetColours();
            Console.WriteLine(" - Game messages/Errors");

        }


        //FALSE = will place into computer sea
        //TRUE = will place into user sea
        public void PlaceAllShips(bool usersea)
        {
            int i;
            Ship sh;

            for (i = 0; i < ShipConstants.TOTAL_BATTLESHIPS; i++)
            {
                sh = new BattleShip(0, 0, ShipDirection.Horizontal);
                if (usersea)
                    PlaceUserShip(sh);
                else
                    PlaceComputerShip(sh);

            }

            for (i = 0; i < ShipConstants.TOTAL_CRUISERS; i++)
            {
                sh = new Cruiser(0, 0, ShipDirection.Horizontal);
                if (usersea)
                    PlaceUserShip(sh);
                else
                    PlaceComputerShip(sh);
            }

            for (i = 0; i < ShipConstants.TOTAL_SUBMARINES; i++)
            {
                sh = new Submarine(0, 0, ShipDirection.Horizontal);
                if (usersea)
                    PlaceUserShip(sh);
                else
                    PlaceComputerShip(sh);
            }

            for (i = 0; i < ShipConstants.TOTAL_ROWING_BOATS; i++)
            {
                sh = new Rowing_Boat(0, 0, ShipDirection.Horizontal);
                if (usersea)
                    PlaceUserShip(sh);
                else
                    PlaceComputerShip(sh);
            }

        }

        private void PlaceComputerShip(Ship sh)
        {
            Ship shnew;
            ShipDirection direction;
            int xpp, ypp;

            do
            {
                direction = (ShipDirection)randomNum(0, 2);
                xpp = randomNum(0, SeaConstants.SEA_SIZE);
                ypp = randomNum(0, SeaConstants.SEA_SIZE);

                shnew = new Ship(sh.shipType, xpp, ypp, sh.length, direction, 0, false, sh.blocks[0].printChar, sh.blocks[0].shipname);

            } while (!ComputerSea.AddShip(shnew));

        }

        private void PlaceUserShip(Ship sh)
        {
            ConsoleKeyInfo rkey;

            SelectedBlocks sb = new SelectedBlocks(sh.blocks[0].xPos, sh.blocks[0].yPos, sh.length, sh.direction);

            UserSea.SelectBlocks(sb);

            do
            {
                rkey = Console.ReadKey(true);
                if (rkey.Key != ConsoleKey.Enter &&
                    (rkey.Key == ConsoleKey.LeftArrow || rkey.Key == ConsoleKey.RightArrow || rkey.Key == ConsoleKey.UpArrow || rkey.Key == ConsoleKey.DownArrow || rkey.Key == ConsoleKey.D))
                {
                    UserSea.UnSelectBlocks(sb);
                    switch (rkey.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            sb.Move(MoveDirection.Left);
                            break;
                        case ConsoleKey.RightArrow:
                            sb.Move(MoveDirection.Right);
                            break;
                        case ConsoleKey.UpArrow:
                            sb.Move(MoveDirection.Up);
                            break;
                        case ConsoleKey.DownArrow:
                            sb.Move(MoveDirection.Down);
                            break;
                        case ConsoleKey.D:
                            UserSea.ChangeDirection(ref sb);
                            break;
                        default:
                            break;
                    }
                    UserSea.SelectBlocks(sb);
                }
                else
                {
                    for (int i = 0; i < sh.length; i++)
                    {
                        sh.blocks[i].xPos = sb.blocks[i].xPos;
                        sh.blocks[i].yPos = sb.blocks[i].yPos;
                    }


                    if (!UserSea.AddShip(sh))
                    {
                        //didnt add ship
                        AddMessage(MessageType.System, this.m.SHIP_INTERSECT);
                        rkey = new ConsoleKeyInfo();
                    }

                }

            } while (rkey.Key != ConsoleKey.Enter);
        }

        public Block GetAttackPosition(int xpos = 0, int ypos = 0)
        {
            ConsoleKeyInfo rkey;
            SelectedBlocks sb = new SelectedBlocks(xpos, ypos, 1, ShipDirection.Horizontal);

            Block b = sb.blocks[0];

            do
            {
                ComputerSea.SelectBlocks(sb);
                rkey = Console.ReadKey(true);
                if (rkey.Key != ConsoleKey.Enter &&
                    (rkey.Key == ConsoleKey.LeftArrow || rkey.Key == ConsoleKey.RightArrow || rkey.Key == ConsoleKey.UpArrow || rkey.Key == ConsoleKey.DownArrow || rkey.Key == ConsoleKey.D))
                {
                    ComputerSea.UnSelectBlocks(sb);
                    switch (rkey.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            sb.Move(MoveDirection.Left);
                            break;
                        case ConsoleKey.RightArrow:
                            sb.Move(MoveDirection.Right);
                            break;
                        case ConsoleKey.UpArrow:
                            sb.Move(MoveDirection.Up);
                            break;
                        case ConsoleKey.DownArrow:
                            sb.Move(MoveDirection.Down);
                            break;
                        case ConsoleKey.D:
                            ComputerSea.ChangeDirection(ref sb);
                            break;
                        default:
                            break;
                    }

                }
                else
                {

                    b = sb.blocks[0];

                    if (b.blockType == SeaType.Attacked && b.blockType == SeaType.Hit)
                    {
                        //ALREADY ATTACKED
                        rkey = new ConsoleKeyInfo();
                        AddMessage(MessageType.System, this.m.USER_ALREADY_ATTACKED);
                    }
                    else
                    {
                        ComputerSea.UnSelectBlocks(sb);
                    }
                }

            } while (rkey.Key != ConsoleKey.Enter);

            return b;

        }

        public bool AttackComputer(int xpos, int ypos)
        {
            bool attacked = false;
            int shipsbeforeattack = 0;

            if (ComputerSea.blocks[ypos, xpos].blockType != SeaType.Attacked && ComputerSea.blocks[ypos, xpos].blockType != SeaType.Hit)
            {
                shipsbeforeattack = ComputerSea.shipsList.Count;
                if (ComputerSea.AttackPos(xpos, ypos))
                {
                    //hit ship
                    if (ComputerSea.shipsList.Count != shipsbeforeattack)
                    {
                        //sunk a ship
                        AddMessage(MessageType.User, this.m.USER_SUNK_SHIP, ComputerSea.blocks[ypos, xpos].shipname);
                    }
                    else
                    {
                        //only hit a ship
                        AddMessage(MessageType.User, this.m.USER_HIT_SHIP, ComputerSea.blocks[ypos, xpos].shipname);
                    }
                }
                else
                {
                    //didnt hit ship
                    AddMessage(MessageType.User, this.m.USER_MISSED_SHIP);
                }
                attacked = true;
            }
            else
            {
                //already attacked.
                AddMessage(MessageType.System, this.m.USER_ALREADY_ATTACKED);

            }

            return attacked;
        }

        public void AttackUserOnePlayer()
        {
            int xpos = randomNum(0, SeaConstants.SEA_SIZE);
            int ypos = randomNum(0, SeaConstants.SEA_SIZE);

            while (UserSea.blocks[ypos, xpos].blockType == SeaType.Attacked || UserSea.blocks[ypos, xpos].blockType == SeaType.Hit)
            {

                if (xpos == SeaConstants.SEA_SIZE - 1)
                {
                    xpos = 0;
                    if (ypos == SeaConstants.SEA_SIZE - 1)
                    {
                        ypos = 0;
                    }
                    else
                    {
                        ypos++;
                    }
                }
                else
                {
                    xpos++;
                }
            }

            string result;

            do
            {
                Console.SetCursorPosition(0, 25);
                Console.WriteLine("                                                      ");
                Console.WriteLine("                                                     ");
                Console.SetCursorPosition(0, 25);
                Console.WriteLine(/*"(" + UserSea.shipLeft + ")(" + UserSea.attacksLeft + ")*/"I want to attack: row " + xpos + ", column " + ypos);
                Console.Write("Did I hit anything? (y/n): ");
                result = Console.ReadLine();
                result = result.ToLower();


                if (result == "y")
                {
                    //hit a ship
                    UserSea.shipLeft--;
                    UserSea.attacksLeft--;
                    AddMessage(MessageType.Computer, this.m.COMPUTER_HIT_SHIP);
                }

                if (result == "n")
                {
                    //didn't hit a ship
                    UserSea.attacksLeft--;
                    AddMessage(MessageType.Computer, this.m.COMPUTER_MISSED_SHIP);

                }
                if (result == "exit")
                {
                    Environment.Exit(1);

                }
            } while (result != "y" && result != "n" && result != "exit");

        }

        public void AttackUserTwoPlayer()
        {
            int xpos = randomNum(0, SeaConstants.SEA_SIZE);
            int ypos = randomNum(0, SeaConstants.SEA_SIZE);
            int shipsbeforeattack = 0;

            while (UserSea.blocks[ypos, xpos].blockType == SeaType.Attacked || UserSea.blocks[ypos, xpos].blockType == SeaType.Hit)
            {

                if (xpos == SeaConstants.SEA_SIZE - 1)
                {
                    xpos = 0;
                    if (ypos == SeaConstants.SEA_SIZE - 1)
                    {
                        ypos = 0;
                    }
                    else
                    {
                        ypos++;
                    }
                }
                else
                {
                    xpos++;
                }
            }

            shipsbeforeattack = UserSea.shipsList.Count;
            if (UserSea.AttackPos(xpos, ypos))
            {
                if (UserSea.shipsList.Count != shipsbeforeattack)
                {
                    //sunk a ship
                    AddMessage(MessageType.Computer, this.m.COMPUTER_SUNK_SHIP, UserSea.blocks[ypos, xpos].shipname);
                }
                else
                {
                    //hit ship
                    AddMessage(MessageType.Computer, this.m.COMPUTER_HIT_SHIP);
                }
            }
            else
            {
                AddMessage(MessageType.Computer, this.m.COMPUTER_MISSED_SHIP);
                //didnt hit ship
            }


        }

        private Random random = new Random();
        public int randomNum(int min, int max)
        {
            return random.Next(min, max);
        }


        public bool PlayAgain()
        {
            bool playagain = false;
            string result;

            do
            {
                Console.SetCursorPosition(0, 25);
                Console.WriteLine("                                                      ");
                Console.SetCursorPosition(0, 25);
                Console.Write("Do you want to play again? (y/n): ");
                result = Console.ReadLine();
                result = result.ToLower();

                if (result == "y")
                {
                    playagain = true;
                }

            } while (result != "y" && result != "n");

            return playagain;

        }

        public void AddMessage(MessageType type, string[] str, string extra = "")
        {
            string mes;

            if (this.messages.Count == ConsoleConstants.MAXMESSAGES)
            {
                this.messages.RemoveAt(0);
            }

            //get random message
            mes = str[randomNum(0, str.Length)];
            if (extra != "" && mes.Contains("{extra}"))
            {
                mes = mes.Replace("{extra}", extra.ToLower());
            }

            this.messages.Add(new Message(type, mes));

            //clear messages
            Console.SetCursorPosition(0, SeaConstants.SEA_SIZE + 5);
            for (int i = 0; i < ConsoleConstants.MAXMESSAGES; i++)
            {
                for (int j = 0; j < ConsoleConstants.BUFFERWIDTH; j++)
                {
                    Console.Write(" ");
                }
            }

            //print messages
            Console.SetCursorPosition(0, SeaConstants.SEA_SIZE + 4);
            Console.WriteLine("------MESSAGES------");

            foreach (Message m in this.messages)
            {
                if (m.type == MessageType.User)
                {
                    SetBackground(ConsoleConstants.CLR_MESSAGE_USER_B);
                }

                if (m.type == MessageType.Computer)
                {
                    SetBackground(ConsoleConstants.CLR_MESSAGE_COMPUTER_B);
                }

                if (m.type == MessageType.System)
                {
                    SetBackground(ConsoleConstants.CLR_MESSAGE_SYSTEM_B);
                }

                //if (extra != "" && mes.Contains("{extra}"))
                //{
                //    mes.Replace("{extra}", extra.ToLower());
                //}

                Console.WriteLine(m.message);

            }

            ResetColours();

        }
    }
}
