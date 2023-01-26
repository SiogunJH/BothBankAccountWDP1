using System;
namespace Bank
{
    public class Account : IAccount
    {
        protected const int PRECISION = 4;

        public string Name { get; }
        public decimal Balance { get; private set; }
        public bool IsBlocked { get; private set; } = false;
        public void Block() => IsBlocked = true;
        public void Unblock() => IsBlocked = false;

        public Account(string name, decimal initialBalance = 0)
        {
            if (name == null || initialBalance < 0)
                throw new ArgumentOutOfRangeException();
            Name = name.Trim();
            if (Name.Length < 3)
                throw new ArgumentException();
            Balance = Math.Round(initialBalance, PRECISION);
        }

        public bool Deposit(decimal amount)
        {
            if (amount <= 0 || IsBlocked) return false;

            Balance = Math.Round(Balance += amount, PRECISION);
            return true;
        }

        public bool Withdrawal(decimal amount)
        {
            if (amount <= 0 || IsBlocked || amount > Balance) return false;

            Balance = Math.Round(Balance -= amount, PRECISION);
            return true;
        }

        public override string ToString() =>
            IsBlocked ? $"Account name: {Name}, balance: {Balance:F2}, blocked"
                        : $"Account name: {Name}, balance: {Balance:F2}";
    }
}

/*
using System;

namespace Bank
{
    public class Account : IAccount
    {
        //Zmienne Klasy
        public string Name { get; }
        public decimal Balance { get; set; }
        public bool IsBlocked { get; set; }

        //Wpłata
        public bool Deposit(decimal Money)
        {
            if (Money <= 0 || this.IsBlocked) return false;

            this.Balance += RoundBalance(Money);
            return true;
        }

        //Wypłata
        public bool Withdrawal(decimal Money)
        {
            if (Money <= 0 || RoundBalance(Money) > this.Balance || this.IsBlocked) return false;

            this.Balance -= RoundBalance(Money);
            return true;
        }

        //Zablokuj konto
        public void Block()
        {
            this.IsBlocked = true;
        }

        //Odblokuj konto
        public void Unblock()
        {
            this.IsBlocked = false;
        }

        //Weryfikuj składnię nazwy
        private string VerifyName(string AccName)
        {
            if (AccName == null) throw new ArgumentOutOfRangeException();
            if (AccName.Trim().Length < 3) throw new ArgumentException();
            return AccName.Trim();
        }

        //Wryfikuj wartość numeryczną
        private decimal VerifyBalance(decimal AccBalance)
        {
            if (AccBalance < 0) throw new ArgumentOutOfRangeException();
            return RoundBalance(AccBalance);
        }
        //Zaokrąglaj
        private decimal RoundBalance(decimal AccBalance)
        {
            return Math.Round(AccBalance, 4);
        }

        //Konstruktor
        public Account(string AccName, decimal AccBalance = 0.00m)
        {
            this.Name = VerifyName(AccName);

            this.Balance = VerifyBalance(AccBalance);

            this.IsBlocked = false;
        }

        //ToString
        public override string ToString() => String.Format("Account name: {0}, balance: {1:N2}{2}", this.Name, this.Balance, this.IsBlocked ? ", blocked" : "");
    }
}
*/