using System;
namespace Bank
{
    class Base
    {
        public static void Main()
        {

            // sytuacje specjalne
            // konto z zerowym stanem
            var account = new AccountPlus("John", initialBalance: 0, initialLimit: 0);
            Console.WriteLine(account);
            account.Withdrawal(10);
            Console.WriteLine(account);

            // zerowe saldo, limit 50
            account.OneTimeDebetLimit = 50;
            Console.WriteLine(account);
            account.Withdrawal(0); // zerowa wypłata
            Console.WriteLine(account);
            account.Withdrawal(10); // wypłata w ramach debetu
            Console.WriteLine(account);
            account.Unblock(); // próba odblokowania konta
            Console.WriteLine(account);
            account.Deposit(10); // likwidacja debetu, zerowy bilans
            Console.WriteLine(account);
        }
    }
}