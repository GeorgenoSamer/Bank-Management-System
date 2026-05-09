using Bank_Manegment_system;

class Customer : Person, ICustomerOperations
{
    int balance = 0;
    List<string> transactions = new List<string>();
    public string SSN { get; set; }
    public string FullName { get; set; }
    int creditLimit = 0;
    int currentDebt = 0;
    bool hasCreditCard = false;
    public int Balance
    {
        get { return balance; }
    }
    public void Register(List<Customer> customers)
    {
        // Full Name
        while (true)
        {
            Console.WriteLine("Enter your full name (letters only):");
            string name = Console.ReadLine();

            bool isValid = true;

            foreach (char c in name)
            {
                if (!char.IsLetter(c) && c != ' ')
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid && name.Length > 0)
            {
                FullName = name;
                break;
            }
            else
            {
                Console.WriteLine("Invalid name. Use letters and spaces only.");
            }
        }

        // Username
        while (true)
        {
            Console.WriteLine("Enter Username (for login):");
            string username = Console.ReadLine();

            bool exists = false;

            foreach (Customer c in customers)
            {
                if (c.User == username)
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
            {
                Console.WriteLine("Username already exists. Try another.");
            }
            else
            {
                User = username;
                break;
            }
        }

        // Pass
        while (true)
        {
            Console.WriteLine("Enter Password:");
            string temp = Console.ReadLine();

            Console.WriteLine("Re-enter Password:");
            string confirm = Console.ReadLine();

            if (temp == confirm)
            {
                Pass = temp;
                break;
            }
            else
            {
                Console.WriteLine("Passwords don't match. Try again.");
            }
        }

        // SSN
        while (true)
        {
            Console.WriteLine("Enter last 4 digits of SSN:");
            string ssn = Console.ReadLine();

            // check size
            if (ssn.Length != 4 || !int.TryParse(ssn, out _))
            {
                Console.WriteLine("SSN must be exactly 4 digits.");
                continue;
            }

            // check if unique
            bool exists = false;

            foreach (Customer c in customers)
            {
                if (c.SSN == ssn)
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
            {
                Console.WriteLine("This SSN already exists. Try another.");
            }
            else
            {
                SSN = ssn;
                break;
            }
        }

        Console.WriteLine("Registration successful!");
    }

    public override void showbalance(int balance)
    {
        Console.WriteLine("Balance: " + this.balance);
    }

    public void Deposit()
    {
        Console.WriteLine("Enter amount:");
        int amount = int.Parse(Console.ReadLine());

        balance += amount;

        transactions.Add("Deposited: " + amount + " | New Balance: " + balance);
    }

    public void Withdraw()
    {
        Console.WriteLine("Enter amount:");
        int amount = int.Parse(Console.ReadLine());
        // Balance Check
        if (balance >= amount)
        {
            balance -= amount;
            transactions.Add("Withdrew: " + amount + " | New Balance: " + balance);
        }
        else
        {
            Console.WriteLine("Insufficient balance");
            transactions.Add("Failed withdrawal attempt: " + amount);
        }
    }
    public void ShowTransactions()
    {
        if (transactions.Count == 0)
        {
            Console.WriteLine("No transactions yet.");
            return;
        }

        Console.WriteLine("\n--- Transaction History ---");

        // Show last 5 transactions
        int start = Math.Max(0, transactions.Count - 5);

        for (int i = start; i < transactions.Count; i++)
        {
            Console.WriteLine(transactions[i]);
        }
    }
    public bool TransferTo(Customer receiver, int amount)
    {
        //Check for reciever
        if (receiver == null)
        {
            Console.WriteLine("Receiver not found");
            return false;
        }
        if (receiver == this)
        {
            Console.WriteLine("You cannot transfer to yourself");
            return false;
        }

        if (amount <= 0)
        {
            Console.WriteLine("Invalid amount");
            return false;
        }
        //Balance Check
        if (this.balance >= amount)
        {
            this.balance -= amount;
            receiver.balance += amount;

            transactions.Add("Sent " + amount + " to " + receiver.User);
            receiver.transactions.Add("Received " + amount + " from " + this.User);

            return true;
        }

        else
        {
            Console.WriteLine("Insufficient balance");
            transactions.Add("Failed transfer attempt: " + amount);
            return false;
        }
    }

    public void CreateCreditCard()
    {
        if (hasCreditCard)
        {
            Console.WriteLine("You already have a credit card.");
            return;
        }

        Console.WriteLine("Enter desired credit limit (max 15000):");

        int limit;
        if (!int.TryParse(Console.ReadLine(), out limit) || limit <= 3000 || limit > 15000)
        {
            Console.WriteLine("Invalid limit. Must be between 3000 and 15000.");
            return;
        }

        creditLimit = limit;
        currentDebt = 0;
        hasCreditCard = true;

        Console.WriteLine("Credit card created with limit: " + creditLimit);
    }
    public void MakePurchase()
    {
        if (!hasCreditCard)
        {
            Console.WriteLine("You don't have a credit card.");
            return;
        }

        Console.WriteLine("Enter purchase amount:");
        int amount;

        if (!int.TryParse(Console.ReadLine(), out amount) || amount <= 0)
        {
            Console.WriteLine("Invalid amount.");
            return;
        }

        if (currentDebt + amount > creditLimit)
        {
            Console.WriteLine("Purchase exceeds credit limit.");
            return;
        }

        currentDebt += amount;
        transactions.Add("Credit Purchase: " + amount + " | Debt: " + currentDebt);

        Console.WriteLine("Purchase successful.");
    }
    public void RepayDebt()
    {
        if (!hasCreditCard)
        {
            Console.WriteLine("No credit card found.");
            return;
        }

        if (currentDebt == 0)
        {
            Console.WriteLine("No debt to repay.");
            return;
        }

        Console.WriteLine("Enter repayment amount:");
        int amount;

        if (!int.TryParse(Console.ReadLine(), out amount) || amount <= 0)
        {
            Console.WriteLine("Invalid amount.");
            return;
        }

        //Balance check
        if (amount > balance)
        {
            Console.WriteLine("Not enough balance.");
            return;
        }

        // Overpay Check
        if (amount > currentDebt)
        {
            Console.WriteLine("Amount larger than debt only the debt amount is payed:" + currentDebt);
            amount = currentDebt;
        }


        balance -= amount;


        currentDebt -= amount;

        transactions.Add("Repayment: " + amount +
                         " | Remaining Debt: " + currentDebt +
                         " | Balance: " + balance);

        Console.WriteLine("Repayment successful.");
    }
    public void ShowCreditInfo()
    {
        if (!hasCreditCard)
        {
            Console.WriteLine("No credit card.");
            return;
        }

        Console.WriteLine("Credit Limit: " + creditLimit);
        Console.WriteLine("Current Debt: " + currentDebt);
    }
    public int GetDebt()
    {
        return currentDebt;
    }
    public void CloseAccount(List<Customer> customers)
    {
        customers.Remove(this);
        Console.WriteLine("Account deleted successfully");
    }
}
