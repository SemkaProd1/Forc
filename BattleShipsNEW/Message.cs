using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShips
{
    public enum MessageType
    {
        User = 0,
        Computer, 
        System
    }

    public class Messages
    {
        public string[] NEW_GAME_PLACE_SHIPS = new string[] { "New game, use your arrow keys to move ships and press enter to place them." };

        public string[] NEW_GAME = new string[] { "New game, use your arrow keys to move and press enter to attack." };

        public string[] SHIP_INTERSECT = new string[] { "You cannot place a ship on top of another ship, try again", "Placing a ship on another ship is not allowed" };


        public string[] USER_HIT_SHIP = new string[] { "Woah, you hit a ship.", "You hit a {extra}." };
        public string[] USER_SUNK_SHIP = new string[] { "You sunk the computer's {extra}.", "Nice! You sunk a {extra}." };
        public string[] USER_MISSED_SHIP = new string[] { "Looks like you missed, you didn't hit anything.", "Computer Says: Ha, you missed my ships!" };
        public string[] USER_ALREADY_ATTACKED = new string[] { "You already attacked this position, attack something else.", "Pay attention, you already have attacked this position" };

        public string[] COMPUTER_HIT_SHIP = new string[] { "Computer Says: AHAHA, I hit one of your ships!", "Computer has hit one of your ships." };
        public string[] COMPUTER_SUNK_SHIP = new string[] { "Watch out! The computer has sunk one of your ships!", "The computer has sunk your {extra}." };
        public string[] COMPUTER_MISSED_SHIP = new string[] { "Computer has missed your ships.", "Computer Says: Looks like I didn't hit anything :(" };

        public string[] CHEATED = new string[] { "It looks like you cheated. YOU LOSE!", "Don't cheat again or there will be trouble! YOU LOSE!" };

        public string[] WON = new string[] { "YOU WON! Congratulations!", "YOU WON THIS GAME! Way to go!" };
        
        public string[] LOST = new string[] { "You lost! Try playing again and I bet you will win.", "Oh. It looks like you lost. Try playing again." };

    }

    public class Message
    {
        public MessageType type { get; set; }
        public string message { get; set; }

        public Message(MessageType type, string message)
        {
            this.type = type;
            this.message = message;
        }


        //public string PrintMessage(int index)
        //{
        //    if (index <= this.messages.Length - 1)
        //    {
        //        return messages[index];
        //    }
        //    else
        //    {
        //        return "";
        //    }

        //}

    }
}
