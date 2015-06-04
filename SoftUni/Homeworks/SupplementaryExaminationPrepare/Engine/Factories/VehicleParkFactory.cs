namespace VehiclePark.Engine.Factories
{
    using Interfaces.Engine;
    using Interfaces;
    using Models;

    public class VehicleParkFactory : IVehicleParkFactory
    {
        public IVehiclePark CreateVehiclePark(int sectors, int places)
        {
            IVehiclePark vehiclePark = new VehiclePark(sectors, places);
            return vehiclePark;
        }
    }
}
