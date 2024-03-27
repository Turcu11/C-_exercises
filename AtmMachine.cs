using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    internal class AtmMachine
    {
        public bool ValidatePin(Card card)
        {
            int chances = 1;
            if (card == null && !card.IsBlocked) return false;
            while (chances <= 3)
            {
                Console.Write("PIN: ");
                string userInput = Console.ReadLine();
                if (userInput.Equals(card.PIN)) return true;
                chances++;
                Console.WriteLine($"Wrong PIN try again \n you still have {4-chances}, until block");
            }
            Console.WriteLine("\nYour card is now BLOCKED\n");
            card.IsBlocked = true;
            return false;
        }
    }
}
