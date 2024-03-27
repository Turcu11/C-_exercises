using AtmMachine;

Bank bt = new("BT");
int option = 0;

while(option < 9)
{
    Console.WriteLine();
    Console.WriteLine("1. Create a new user and register");
    Console.WriteLine("2. See all users and their cards");
    Console.WriteLine("3. See all users");
    Console.WriteLine("4. Select an user");
    Console.WriteLine("9. Exit.");
    Console.WriteLine();
    Console.Write("Enter your option: ");
    option = int.Parse(Console.ReadLine());
    switch (option)
    {
        case 1:
            Console.Write("Username:");
            string username = Console.ReadLine();
            Console.Write("Card name:");
            string cardName = Console.ReadLine();
            bt.RegisterUserAndOpenAccountThenCreateCardLinkedToAccount(new User(username), cardName);
            break;

        case 2:
            bt.SeeAllUsersAndCards();
            break;

        case 3:
            bt.SeeAllUsers();
            break;

        case 4:
            Console.Write("Enter the user you want to select: ");
            string user = Console.ReadLine();
            bt.SelectUser(user);
            break;

        //am ramas ca trebuie sa fac partea de insert a card,
        //si sa verific mai multe chestii, in user este metoda de GetFIrstCard
    }
}