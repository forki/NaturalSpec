using System;

namespace CarSellingLib
{
    public class Dealer : IDealer
    {
        public Dealer(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public Car SellCar(int amount)
        {
            if (amount > 20000)
                return new Car(CarType.BMW, 200);

            if (amount > 3000)
                return new Car(CarType.Fiat, 45);

            throw new Exception("Need more money");
        }

        public override string ToString()
        {
            return Name;
        }
    }
}