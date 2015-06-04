namespace VehiclePark.Engine.Factories
{
    using Interfaces.Engine;
    using Interfaces;
    using Models;

    public class VehicleFactory : IVehicleFactory
    {
        public ICar ParkCar(string licensePlate, string ownerName, int reservedHours)
        {
            decimal carRegularRate = 2m;
            decimal carOvertimeRate = 3.5m;

            ICar car = new Car(licensePlate, ownerName, reservedHours, carRegularRate, carOvertimeRate);
            return car;
        }

        public IMotorbike ParkMotorbike(string licensePlate, string ownerName, int reservedHours)
        {
            decimal motorbikeRegularRate = 1.35m;
            decimal motorbikeOvertimeRate = 3m;

            IMotorbike motorbike = new Motorbike(licensePlate, ownerName, reservedHours, motorbikeOvertimeRate, motorbikeOvertimeRate);
            return motorbike;
        }

        public ITruck ParkTruck(string licensePlate, string ownerName, int reservedHours)
        {
            decimal motorbikeRegularRate = 4.75m;
            decimal motorbikeOvertimeRate = 6m;

            ITruck truck = new Truck(licensePlate, ownerName, reservedHours, motorbikeOvertimeRate, motorbikeOvertimeRate);
            return truck;
        }
    }
}
