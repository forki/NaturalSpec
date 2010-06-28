using System;

namespace Articles
{
    public class Account
    {
        public int AccountNo { get; set; }

        public decimal Balance { get; set; }

        public void Transfer(decimal amount, Account toAccount)
        {
            if (amount > Balance)
            {
                throw new Exception(String.Format("Cannot transfer ${0}. The available balance is ${1}.", amount,
                                                  Balance));
            }

            Balance -= amount;
            toAccount.Balance += amount;
        }

        public override string ToString()
        {
            return string.Format("\"Account {0}\"", AccountNo);
        }
    }
}