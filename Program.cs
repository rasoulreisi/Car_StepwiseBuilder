namespace Car_StepwiseBuilder
{
    public enum CarType
    {
        Sedan,
        Crossover
    }

    public class Car
    {
        public CarType Type;
        public int WheelSize;
    }

    public class CarBuilder
    {
        private class Impl : ISpecifyCarType, ISpecifyCarWheelSize, IBuildCar
        {
            Car Car = new Car();
            public Car Build()
            {
                return Car;
            }

            public IBuildCar WithWheels(int wheelSize)
            {
                switch (Car.Type)
                {
                    case CarType.Sedan when wheelSize != 16:
                    case CarType.Crossover when wheelSize < 15 || wheelSize > 20:
                        throw new ArgumentOutOfRangeException(nameof(wheelSize));
                }

                Car.WheelSize = wheelSize;
                return this;
            }

            ISpecifyCarWheelSize ISpecifyCarType.OfType(CarType type)
            {
                Car.Type = type;
                return this;
            }
        }

        public static ISpecifyCarType New => new Impl();
    }

    public interface ISpecifyCarType
    {
        ISpecifyCarWheelSize OfType(CarType type);
    }
    
    public interface ISpecifyCarWheelSize
    {
        IBuildCar WithWheels(int wheelSize);
    }

    public interface IBuildCar
    {
        Car Build();
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Car car = CarBuilder.New.OfType(CarType.Sedan).WithWheels(16).Build();
            Console.WriteLine(car.Type);
            Console.WriteLine(car.WheelSize);
        }
    }
}