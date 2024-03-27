using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    internal class Account
    {
        public Guid Id { get; set; }
        public AccountType Type { get; set; }
        public string Iban {  get; set; }

        public float Balance { get; set; }

        public Currency Currency { get; set; }

        public Account(AccountType accountType, float balance, Currency currency) 
        {
            Type = accountType;
            Balance = balance;
            Currency = currency;
        }

        public void AddBalance(float balance)
        {
            if(balance > 0)
            {
                Balance += balance;
            }
            else
            {
                Console.WriteLine("Balance should be more than 0");
            }
        }

        public void SubtractBalance(float balance)
        {
            if(balance > Balance)
            {
                Console.WriteLine($"Not enough fonds, only {Balance}, {Currency}");
            }
            else
            {
                Balance -= balance;
                Console.WriteLine($"Transaction succesful, remaining balance: {Balance}, {Currency}");
            }
        }
    }
}
