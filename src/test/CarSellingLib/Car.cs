namespace CarSellingLib
{
    public class Car
    {
        public Car(CarType type, int horsePower)
        {
            Type = type;
            HorsePower = horsePower;
        }

        public CarType Type { get; set; }
        public int HorsePower { get; set; }

        # region ToString, Equals & GetHashCode

        public override string ToString()
        {
            return string.Format("{0} ({1} HP)", Type, HorsePower);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(Car) && Equals((Car)obj);
        }

        public bool Equals(Car other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Type, Type) && other.HorsePower == HorsePower;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Type.GetHashCode() * 397) ^ HorsePower;
            }
        }
        #endregion

    }

}