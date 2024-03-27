using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    internal class Bank
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public AtmMachine Atm = new AtmMachine();

        public User ActiveUser { get; set; }

        public List<User> Users = [];
        public Bank(string name)
        {
            Name = name;
        }

        public void RegisterUserAndOpenAccountThenCreateCardLinkedToAccount(User user, string cardName)
        {
            user.Account = new(AccountType.Current, 100, Currency.RON);


            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.Account.Iban = Name + "_" + user.Name + "_" + user.Id;
            user.CreateAndAddCard(cardName);
            Users.Add(user);
        }

        public void CreateNewUserCard(User user)
        {
            foreach (var u in Users)
            {
                if (u.Id == user.Id)
                {
                    u.CreateAndAddCard(user.Name);
                    break;
                }
            }
            Console.WriteLine($"That user \"{user.Name}\" is not registered yet !!!\nUse \"RegisterUserAndOpenAccount\" to register");
        }

        public void SeeAllUsersAndCards()
        {
            foreach (var u in Users)
            {
                u.SeeMyCards();
            }
        }

        public void SeeAllUsers()
        {
            foreach (var u in Users)
            {
                Console.WriteLine($"{u.Name} \n{u.SeeMyCards}");
                Console.WriteLine();
            }
        }

        public void SelectUser(string name)
        {
            foreach (var u in Users)
            {
                if (u.Name == name)
                {
                    ActiveUser = u;
                    Console.WriteLine($"You are now loged in as {u.Name}");
                    InsertCard();
                    break;
                }

                else Console.WriteLine($"User: {name}, not found");
            }
        }

        public void InsertCard()
        {
            bool validate = Atm.ValidatePin(ActiveUser.Cards.First());
            if(validate)
            {
                Console.WriteLine("You're in :)))");
                ShowInsideMenu();
            }
        }

        public void ShowInsideMenu()
        {
            int option = 0;
            while (option < 9)
            {
                Console.WriteLine($"Hello! {ActiveUser.Name}, Ballance: {ActiveUser.Account.Balance} {ActiveUser.Account.Currency}");
                Console.WriteLine();
                Console.WriteLine("1. Deposit ballance");
                Console.WriteLine("2. Withdraw from ballance");
                Console.WriteLine("3. See unpaied bills");
                Console.WriteLine("4. Select a bill to pay");
                Console.WriteLine("9. Disconect");
                Console.WriteLine();
                Console.Write("Your option is:  ");
                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        float BallanceToAdd = float.Parse(Console.ReadLine());
                        ActiveUser.Account.AddBalance(BallanceToAdd);
                        break;
                    case 2:
                        float BallanceToWithdraw = float.Parse(Console.ReadLine());
                        ActiveUser.Account.SubtractBalance(BallanceToWithdraw);
                        break;
                    case 3:
                        Bill.GetUnpaiedBills(ActiveUser.Bills);
                        break;
                    case 4:
                        string BillName = Console.ReadLine();
                        Bill BillToPay = ActiveUser.GetOneBillByName(BillName);
                        ActiveUser.PayBill(BillToPay);
                        break;

                    case 9:
                        ActiveUser = null;
                        break;
                }
            }
        }
    }
}
