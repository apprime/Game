using System;

namespace Core.Entities.Humans
{
    /// <summary>
    /// Ink is currency. Instead of Copper, Silver, Gold and Platinum, we have;
    /// Basic, Advanced, Expert and Master (TODO: Change these names to something less generic).
    /// However, the base is always just a large positive integer (ulong). 
    /// </summary>
    public class Ink
    {
        public Ink(ulong amount)
        {
            Amount = amount;
        }

        private ulong _amount;
        private int _basic;
        private int _advanced;
        private int _expert;
        private int _master;

        public ulong Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                CurrencyOverflow(value);

                _amount = value;
                Basic = Convert.ToInt32(_amount % 100);
                Advanced = Convert.ToInt32(_amount % 10000) - Basic;
                Expert = Convert.ToInt32(_amount % 1000000) - Basic - Advanced;
                Master = Convert.ToInt32(_amount % 100000000) - Basic - Advanced - Expert;
            }
        }

        private void CurrencyOverflow(ulong value)
        {
            if(ulong.MaxValue - value < Amount)
            {
                throw new Exception("Player has gained more money than is possible.");
            }
        }

        public int Basic { get; private set; }
        public int Advanced { get; private set; }
        public int Expert { get; private set; }
        public int Master { get; private set; }
     
        public Ink Add(ulong amount)
        {
            Amount += amount;
            return this;
        }

        public Ink Remove(ulong amount)
        {
            Amount -= amount;
            return this;
        }

        public static Ink operator + (Ink a, Ink b)
        {
            return new Ink(a.Amount + b.Amount);
        }   
    }
}