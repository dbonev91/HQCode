namespace VehiclePark.Interfaces
{
    using System.Collections.Generic;

    public interface IVehiclePark
    {
        int Sectors { get; }

        int Places { get; }

        int[,] ParkMatrix { get; }

        IList<IVehicle> VehicleCollection { get; }

        void AddVehicle(IVehicle vehicle);

        void RemoveVehicle(IVehicle vehicle);

        string Status();

        string FindVehicle(string plateNumber);

        string FindVehicleByOwner(string ownerName);
    }
}
