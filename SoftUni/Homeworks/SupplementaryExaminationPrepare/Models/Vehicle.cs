using System;

namespace VehiclePark.Models
{
    using Engine;
    using Interfaces;
    using System.Text;

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

        public int Sector { get; set; }

        public int Place { get; set; }

        public DateTime ParkDate { get; set; }

        public DateTime ExitDate { get; set; }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine("********************");
            output.AppendLine(string.Format("{0} [{1}], owned by {2}", this.GetType().Name, this.LicensePlate, this.OwnerName));
            output.AppendLine(string.Format("at place ({0},{1})", this.Sector, this.Place));
            output.AppendLine(string.Format("Rate: {0}", this.RegularRate * this.ReservedHours));
            output.AppendLine(string.Format("Overtime rate: {0}", this.OvertimeRate));
            output.AppendLine("--------------------");
            output.AppendLine("Total: <total_amount>");
            output.AppendLine("Paid: <paid>");
            output.AppendLine("Change: <change>");
            output.AppendLine("********************");

            return output.ToString();
        }

        private decimal CalculateRegularRate()
        {
            double hours = (this.ExitDate - this.ParkDate).TotalHours;
            double overtimeHours = CalculateOvertimeHours(hours - this.ReservedHours);
            decimal price = (this.RegularRate * this.ReservedHours) + (this.OvertimeRate * (decimal)overtimeHours);

            return price;
        }

        private double CalculateOvertimeHours(double overtimeHours)
        {
            if (overtimeHours <= 0)
            {
                overtimeHours = 0;
                return overtimeHours;
            }

            return overtimeHours;
        }
    }
}
