namespace BankManegmentSystem
{
    class Program
    {
        static List<Customer> customers = new List<Customer>();

        static void Main()
        {
            int choice = -1;
            Customer loggedCustomer = null;
            Admin admin = new Admin();
            bool adminRegistered = false;
            int adminNumber = 0;
            bool customerRegistered = false;
            //Main menu

            while (choice != 0)
            {
                Console.WriteLine("\n--- Banking System ---");
                Console.WriteLine("1. Admin Login");
                Console.WriteLine("2. Customer Login");
                Console.WriteLine("3. Customer Registration");
                Console.WriteLine("4. Admin Registration");
                Console.WriteLine("5. Exit");

                choice = int.Parse(Console.ReadLine());
                //switch case
                switch (choice)
                {
                    case 1: // Admin Login
                        if (!adminRegistered)
                        {
                            Console.WriteLine("Admin must register first!");
                        }
                        else if (admin.Login(admin.User, admin.Pass))
                        {
                            AdminMenu(admin);
                        }
                        break;

                    case 2:

                        if (customers.Count == 0)
                        {
                            Console.WriteLine("No customers registered. Please register first.");
                            break;
                        }

                        loggedCustomer = CustomerLogin();

                        if (loggedCustomer != null)
                        {
                            CustomerMenu(loggedCustomer);
                        }

                        break;

                    case 3: // Customer Registration
                        Customer newCustomer = new Customer();
                        newCustomer.Register(customers);
                        customerRegistered = true;
                        customers.Add(newCustomer);
                        Console.WriteLine("Customer registered successfully!");
                        break;

                    case 4: // Admin Registration
                        if (adminNumber == 0)
                        {
                            admin.Register();
                            adminRegistered = true;
                            adminNumber = 1;
                            Console.WriteLine("Admin registered successfully!");
                        }
                        else
                        {
                            Console.WriteLine("An Admin has already registered");
                        }
                        break;

                    case 5:
                        Console.WriteLine("Goodbye!");
                        choice = 0;
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        static Customer CustomerLogin()
        {
            Console.WriteLine("Enter Username:");
            string u = Console.ReadLine();

            Console.WriteLine("Enter Password:");
            string p = Console.ReadLine();

            foreach (Customer c in customers)
            {
                if (c.User == u && c.Pass == p)
                {
                    Console.WriteLine("Login successful");
                    return c;
                }
            }

            Console.WriteLine("User not found");
            return null;
        }


        static Customer FindCustomer(string username)
        {
            foreach (Customer c in customers)
            {
                if (c.User == username)
                    return c;
            }
            return null;
        }


        static void CustomerMenu(Customer c)
        {
            int choice = -1;

            while (choice != 11)
            {
                Console.WriteLine("\n--- Customer Menu ---");
                Console.WriteLine("1. Display Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transfer Money");
                Console.WriteLine("5. View Transactions");
                Console.WriteLine("6. Close Account");
                Console.WriteLine("7. Create Credit Card");
                Console.WriteLine("8. Make Purchase");
                Console.WriteLine("9. Repay Debt");
                Console.WriteLine("10. View Credit Info");
                Console.WriteLine("11. Exit");

                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        c.DisplayBalance();
                        break;

                    case 2:
                        c.Deposit();
                        break;

                    case 3:
                        c.Withdraw();
                        break;

                    case 4:
                        Console.WriteLine("Enter receiver username:");
                        string receiverName = Console.ReadLine();

                        Customer receiver = FindCustomer(receiverName);


                        if (receiver == null)
                        {
                            Console.WriteLine("User not found");
                            break;
                        }

                        if (receiver == c)
                        {
                            Console.WriteLine("You cannot transfer to yourself");
                            break;
                        }

                        Console.WriteLine("Enter amount:");
                        int amount = int.Parse(Console.ReadLine());

                        if (c.TransferTo(receiver, amount))
                        {
                            Console.WriteLine("Transfer successful");
                        }
                        break;

                    case 5:
                        c.ShowTransactions();
                        break;

                    case 6:
                        c.CloseAccount(customers);
                        return;

                    case 7:
                        c.CreateCreditCard();
                        break;

                    case 8:
                        c.MakePurchase();
                        break;

                    case 9:
                        c.RepayDebt();
                        break;

                    case 10:
                        c.ShowCreditInfo();
                        break;

                    case 11:
                        break;
                }
            }
        }


        static void AdminMenu(Admin a)
        {
            int choice = -1;

            while (choice != 4)
            {
                Console.WriteLine("\n--- Admin Menu ---");
                Console.WriteLine("1. Display All Customers");
                Console.WriteLine("2. Display Bank Reserves");
                Console.WriteLine("3. Back");
                Console.WriteLine("4. Exit Program");

                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        a.DisplayCustomers(customers);
                        break;

                    case 2:
                        Console.WriteLine("Bank Reserves: " + a.DisplayBankreserves());
                        break;

                    case 3:
                        return;

                    case 4:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}