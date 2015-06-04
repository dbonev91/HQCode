namespace VehiclePark.Models
{
    using Interfaces;
    using Engine;

    public abstract class Vehicle : IVehicle
    {
        private decimal regularRate;
        private decimal overtimeRate;
        private string licensePlate;
        private string ownerName;
        private int reservedHours;

        protected Vehicle(string licansePlate, string ownerName, int reservedHours, decimal regularRate, decimal overtimeRate)
        {
            this.RegularRate = regularRate;
            this.OvertimeRate = overtimeRate;
            this.LicensePlate = licansePlate;
            this.OwnerName = ownerName;
            this.ReservedHours = reservedHours;
        }

        public decimal RegularRate
        {
            get
            {
                return this.regularRate;
            }
            set
            {
                this.regularRate = value;
            }
        }

        public decimal OvertimeRate
        {
            get
            {
                return this.overtimeRate;
            }
            set
            {
                this.overtimeRate = value;
            }
        }

        public string LicensePlate
        {
            get
            {
                return this.licensePlate;
            }
            set
            {
                this.licensePlate = value;
            }
        }

        public string OwnerName
        {
            get
            {
                return this.ownerName;
            }
            set
            {
                this.ownerName = value;
            }
        }

        public int ReservedHours
        {
            get
            {
                return this.reservedHours;
            }
            set
            {
                this.reservedHours = value;
            }
        }
    }
}
