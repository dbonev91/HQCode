namespace VehiclePark.Models
{
    using Interfaces;

    public class Car : Vehicle, ICar
    {
        public Car(string licansePlate, string ownerName, int reservedHours, decimal regularRate, decimal overtimeRate)
            : base(licansePlate, ownerName, reservedHours, regularRate, overtimeRate)
        {
        }
    }
}
