namespace VehiclePark.Models
{
    using Interfaces;

    public class Motorbike : Vehicle, IMotorbike
    {
        public Motorbike(string licansePlate, string ownerName, int reservedHours, decimal regularRate, decimal overtimeRate)
            : base(licansePlate, ownerName, reservedHours, regularRate, overtimeRate)
        {
        }
    }
}
