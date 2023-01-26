using System;

namespace Bank
{
    public class AccountPlus : Account, IAccountWithLimit
    {
        //Zmienne Klasy
        public new string Name { get; }
        public decimal _Balance;
        public new decimal Balance
        {
            get
            {
                if (_Balance < 0) return 0;
                return _Balance;
            }
            private set { _Balance = value; }
        }
        public new bool IsBlocked { get; private set; } = false;
        private decimal _OneTimeDebetLimit;
        public decimal OneTimeDebetLimit
        {
            get { return _OneTimeDebetLimit; }
            set
            {
                if (value < 0 || this.IsBlocked) return;
                _OneTimeDebetLimit = value;
            }
        }
        public decimal AvaibleFounds
        {
            get { return _OneTimeDebetLimit + _Balance; }
        }

        public new bool Deposit(decimal amount)
        {
            if (amount <= 0) return false;

            Balance = Math.Round(_Balance += amount, PRECISION);
            if (_Balance >= 0) this.IsBlocked = false;
            return true;
        }

        public new bool Withdrawal(decimal amount)
        {
            if (amount <= 0 || IsBlocked || amount > AvaibleFounds) return false;

            Balance = Math.Round(_Balance -= amount, PRECISION);
            if (_Balance < 0) this.IsBlocked = true;
            return true;
        }
        //Zablokuj konto

        public new void Block() => IsBlocked = true;

        //Odblokuj konto
        public new void Unblock()
        {
            if (_Balance < 0) return;
            this.IsBlocked = false;
        }

        //Weryfikuj składnię nazwy
        private string VerifyName(string AccName)
        {
            if (AccName == null) throw new ArgumentOutOfRangeException();
            if (AccName.Trim().Length < 3) throw new ArgumentException();
            return AccName.Trim();
        }

        //Konstruktor
        public AccountPlus(string name, decimal initialBalance = 0.00m, decimal initialLimit = 100.00m) : base(name, initialBalance)
        {
            if (name == null || initialBalance < 0)
                throw new ArgumentOutOfRangeException();

            Name = name.Trim();
            if (Name.Length < 3)
                throw new ArgumentException();

            Balance = Math.Round(initialBalance, PRECISION);

            if (initialLimit <= 0) this.OneTimeDebetLimit = 0;
            else this.OneTimeDebetLimit = initialLimit;
        }

        //ToString
        public override string ToString() =>
            IsBlocked ? $"Account name: {Name}, balance: {Balance:F2}, blocked, avaible founds: {AvaibleFounds:F2}, limit: {OneTimeDebetLimit:F2}"
                        : $"Account name: {Name}, balance: {Balance:F2}, avaible founds: {AvaibleFounds:F2}, limit: {OneTimeDebetLimit:F2}";
    }
}