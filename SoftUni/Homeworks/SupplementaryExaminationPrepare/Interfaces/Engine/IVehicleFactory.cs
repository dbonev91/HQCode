namespace VehiclePark.Interfaces.Engine
{
    public interface IVehicleFactory
    {
        ICar ParkCar(string licensePlate, string ownerName, int reservedHowrs);

        IMotorbike ParkMotorbike(string licensePlate, string ownerName, int reservedHowrs);

        ITruck ParkTruck(string licensePlate, string ownerName, int reservedHowrs);
    }
}
