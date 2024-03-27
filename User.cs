using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    internal class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Account Account { get; set; }
        public List<Card> Cards = [] ;
        public List<Bill> Bills = [] ;

        public User(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Bills.Add(new Bill(BillType.ELECTRICITY,"Electrica" , 200)); //just adding some degault bills to test the feature
            Bills.Add(new Bill(BillType.GAS,"E-on", 160));
            Bills.Add(new Bill(BillType.INTERNET,"Digi", 50));
        }


        public void CreateAndAddCard(string CardName)
        {
            if (Name != null)
            {
                Random random = new();
                string sequence1 = random.Next(1000, 9999).ToString();
                string sequence2 = random.Next(1000, 9999).ToString();
                string sequence3 = random.Next(1000, 9999).ToString();
                string sequence4 = random.Next(1000, 9999).ToString();
                Console.Write($"For {Name}, Enter new PIN: ");
                string pin = Console.ReadLine();
                Card NewCard = new()
                {
                    Id = Guid.NewGuid(),
                    Name = CardName,
                    CardNumber = string.Concat(sequence1, "-", sequence2, "-", sequence3, "-", sequence4),
                    ExpirationDate = DateOnly.FromDateTime(DateTime.Now).AddYears(5),
                    CVV = random.Next(100, 999).ToString(),
                    PIN = pin ?? "123",
                    CardHolderName = Name,
                    AccountLinkedTo = Account.Iban,
                    IsBlocked = false,
                };
                Cards.Add(NewCard);
            }
            else
            {
                Console.WriteLine("The user does not have a name and an account set !");
            }
        }

        public void SeeMyCards()
        {
            foreach (var card in Cards)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine($"ID: {card.Id}");
                Console.WriteLine($"CARD NAME: {card.Name}");
                Console.WriteLine($"CARD NUMBER: {card.CardNumber}");
                Console.WriteLine($"EXP DATE: {card.ExpirationDate.ToString()}");
                Console.WriteLine($"CVV: {card.CVV}");
                Console.WriteLine($"PIN: {card.PIN}");
                Console.WriteLine($"CARDHOLDER: {card.CardHolderName}");
                Console.WriteLine($"LINK TO IBAN: {card.AccountLinkedTo}");
                Console.WriteLine($"IS BLOKED: {card.IsBlocked.ToString()}");
                Console.WriteLine("**************************");
            }
        }

        public Card GetUserFirstCard()
        {
            return Cards[0];
        }

        public Bill GetOneBillByName(string name)
        {
            List<Bill> UnpaiedBills = Bill.GetUnpaiedBills(Bills);

            foreach (Bill bill in UnpaiedBills)
            {
                if (bill.Name.Equals(name))
                {
                    return bill;
                }
            }
            Console.WriteLine("Bill not found!");
            return null;
        }

        public void PayBill(Bill bill)
        {
            if (Account.Balance >= bill.Ammount)
            {
                Console.WriteLine($"The bill ammpint is: {bill.Ammount}, and your ballance is: {Account.Balance}");
                Console.WriteLine("Are you sure you want to pay?");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                int option = int.Parse(Console.ReadLine());
                if(option == 1)
                {
                    foreach(Bill b in Bills)
                    {
                        if (b.Name.Equals(bill.Name))
                        {
                            bill.IsPaid = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Mabye some other tine!");
                }
            }
        }
    }
}
