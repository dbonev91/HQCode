namespace VehiclePark.Interfaces
{
    public interface IVehicle
    {
        decimal RegularRate { get; }

        decimal OvertimeRate { get; }

        string LicensePlate { get; }

        string OwnerName { get; }

        int ReservedHours { get; }
    }
}
