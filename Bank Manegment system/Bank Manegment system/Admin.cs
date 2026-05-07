using Bank_Manegment_system;

class Admin : Person, IAdminOperations
{
    int bankreserves = 100000;

    public int DisplayBankreserves()
    {
        return bankreserves;
    }

    public void DisplayCustomers(List<Customer> customers)
    {
        //check for customers
        if (customers.Count == 0)
        {
            Console.WriteLine("No customers found.");
            return;
        }

        Console.WriteLine("\n--- Customer List ---");

        foreach (Customer c in customers)
        {
            Console.WriteLine(
                "Name: " + c.FullName +
                " | Balance: " + c.Balance +
                " | SSN: " + c.SSN +
                " | Debt: " + c.GetDebt()
            );
        }
    }
}