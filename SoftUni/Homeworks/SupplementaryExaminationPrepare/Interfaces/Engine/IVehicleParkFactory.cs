namespace VehiclePark.Interfaces.Engine
{
    public interface IVehicleParkFactory
    {
        IVehiclePark CreateVehiclePark(int sectors, int places);
    }
}
