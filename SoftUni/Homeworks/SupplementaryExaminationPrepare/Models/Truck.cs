namespace VehiclePark.Models
{
    using Interfaces;

    public class Truck : Vehicle, ITruck
    {
        public Truck(string licansePlate, string ownerName, int reservedHours, decimal regularRate, decimal overtimeRate)
            : base(licansePlate, ownerName, reservedHours, regularRate, overtimeRate)
        {
        }
    }
}
