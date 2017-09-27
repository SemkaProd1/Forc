/*
 * Copyright 2011 Gelmis R. 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using BattleShips;

class Battle
{
    static GameConsole gc;

    static void Main()
    {
        int mode;

        do
        {
            gc = new GameConsole();

            do
            {
                //choose game mode
                mode = ChooseMode();

                switch (mode)
                {
                    case 1://one player
                        Console.Clear();
                        gc.Write("Please get a piece of paper and draw a grid that has " + SeaConstants.SEA_SIZE + " columns that each have " + SeaConstants.SEA_SIZE + @" rows.
Number your columns and rows from 0 to " + (SeaConstants.SEA_SIZE - 1)  + @".
Then, place " + ShipConstants.TOTAL_BATTLESHIPS + " battleship(s) which are " + ShipConstants.BATTLESHIP_LENGTH + @" block(s) long.
And then, place " + ShipConstants.TOTAL_CRUISERS + " cruiser(s) which are " + ShipConstants.CRUISER_LENGTH + @" block(s) long.
And then, place " + ShipConstants.TOTAL_SUBMARINES + " submarine(s) which are " + ShipConstants.SUBMARINE_LENGTH + @" block(s) long.
And then, place " + ShipConstants.TOTAL_ROWING_BOATS + " rowing boats(s) which are " + ShipConstants.ROWING_BOAT_LENGTH + @" block(s) long.

When you are done, press enter to continue..");
                        Console.ReadLine();
                        OnePlayer();
                        break;
                    case 2://two player
                        Console.Clear();
                        TwoPlayer();
                        break;
                    case 3://Ps vs ps
                        Console.Clear();
                       // PsVsPs();
                        break;
                    default:
                        break;

                }
            } while (mode != 1 && mode != 2);

        } while (gc.PlayAgain());


        //Console.ReadLine();
    }

    static int ChooseMode()
    {
        int rtrn = 1;
        string str = "";

        Console.Clear();

        gc.Write(@"
Welcome to Battleships!
Created by ILYA!!!


Select one of the modes below and your game will start! Please read the user manual to find out how the game works.

One Player = You will use a piece of paper to draw your ships on.
Two Player = There will be two ship boards for you and the computer. You will not need to use paper to draw your ships on.

1. One Player Mode
2. Two Player Mode
3. Ps vs Ps Mode -  maybe later.
"
);
        gc.Write("Choose mode: ");
        str = Console.ReadLine();
        if (Int32.TryParse(str, out rtrn))
        {
            return rtrn;
        }
        else
        {
            rtrn = ChooseMode();
        }
        return rtrn;
    }



    static void OnePlayer()
    {
        Block b = new Block(SeaType.EmptySea, 0, 0, "+", "Empty Sea");
        Console.Clear();

        gc.ComputerSea.consoleXPos = 0;

        gc.PrintSea(false);
        gc.PlaceAllShips(false);

        gc.displayColourKey(gc.SeaWidth + 6, 0);

        gc.AddMessage(MessageType.System, gc.m.NEW_GAME);

        while (!GameOver())
        {
            do
            {
                b = gc.GetAttackPosition(b.xPos, b.yPos);
            } while (!gc.AttackComputer(b.xPos, b.yPos));

            gc.AttackUserOnePlayer();
        }

        Console.Clear();

        if (gc.UserSea.attacksLeft == 0)
        {
            gc.AddMessage(MessageType.System, gc.m.CHEATED);
        }

        if (gc.ComputerSea.attacksLeft == 0 || gc.ComputerSea.shipLeft == 0)
        {
            gc.AddMessage(MessageType.System, gc.m.WON);
        }

        if (gc.UserSea.shipLeft == 0)
        {
            gc.AddMessage(MessageType.System, gc.m.LOST);
        }

        Console.WriteLine("GAME OVER");


    }

    static void TwoPlayer()
    {
        Block b = new Block(SeaType.EmptySea, 0, 0, "+", "Empty Sea");
        Console.Clear();

        gc.AddMessage(MessageType.System, gc.m.NEW_GAME_PLACE_SHIPS);
        gc.displayColourKey(gc.SeaWidth * 2 + 6, 0);

        gc.PrintSea(true);
        gc.PlaceAllShips(true);

        gc.PrintSea(false);
        gc.PlaceAllShips(false);


        gc.messages = new ArrayList();
        gc.AddMessage(MessageType.System, gc.m.NEW_GAME);

        while (!GameOver())
        {
            do
            {
                b = gc.GetAttackPosition(b.xPos, b.yPos);
                gc.Write(b.xPos + " - " + b.yPos);
            } while (!gc.AttackComputer(b.xPos, b.yPos));
            System.Threading.Thread.Sleep(500);
            gc.AttackUserTwoPlayer();
        }

        Console.Clear();
        if (gc.ComputerSea.shipLeft == 0 || gc.ComputerSea.attacksLeft == 0)
        {
            gc.AddMessage(MessageType.System, gc.m.WON);

        }
        if (gc.UserSea.shipLeft == 0 || gc.UserSea.attacksLeft == 0)
        {
            gc.AddMessage(MessageType.System, gc.m.LOST);
        }

        //gc.PrintSea(gc.ComputerSea);
        Console.WriteLine("GAME OVER");
    }

   

    static bool GameOver()
    {
        bool isGameOver = false;

        if (gc.ComputerSea.shipLeft == 0 || gc.ComputerSea.attacksLeft == 0) isGameOver = true;
        if (gc.UserSea.shipLeft == 0 || gc.UserSea.attacksLeft == 0) isGameOver = true;

        return isGameOver;
    }

}