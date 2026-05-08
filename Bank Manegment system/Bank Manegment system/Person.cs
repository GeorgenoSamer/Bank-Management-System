abstract class Person
{
    public string User { get; set; }
    public string Pass { get; set; }

    public void Register()
    {
        Console.WriteLine("Enter Username:");
        User = Console.ReadLine();

        string temp;
        Console.WriteLine("Enter Password:");
        temp = Console.ReadLine();

        Console.WriteLine("Re-enter Password:");
        Pass = Console.ReadLine();

        if (temp != Pass)
        {
            Console.WriteLine("Passwords don't match. Try again.");
            Register();
        }
    }

    public bool Login(string user, string pass)
    {
        Console.WriteLine("Enter Username:");
        string u = Console.ReadLine();

        Console.WriteLine("Enter Password:");
        string p = Console.ReadLine();

        if (u == user && p == pass)
        {
            Console.WriteLine("Login successful");
            return true;
        }

        Console.WriteLine("Wrong credentials");
        return false;
    }
    public abstract void showbalance(int balance);
}
