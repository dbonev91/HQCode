namespace VehiclePark.Engine
{
    using System;
    using Interfaces.Engine;
    using Interfaces;
    using Factories;
    using System.Collections.Generic;
    using System.Linq;

    public class VehicleParkEngine : IVehicleParkEngine
    {
        private static IVehicleParkEngine instance;

        private readonly IVehicleParkFactory vehicleParkFactory;
        private readonly IVehicleFactory vehicleFactory;

        private readonly IVehiclePark vehiclePark;
        private readonly IDictionary<string, IVehicle> vehicles;

        private VehicleParkEngine()
        {
            this.vehicleParkFactory = new VehicleParkFactory();
            this.vehicleFactory = new VehicleFactory();
            this.vehicles = new Dictionary<string, IVehicle>();
        }

        public static IVehicleParkEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VehicleParkEngine();
                }

                return instance;
            }
        }

        public void Start(string command)
        {
            Console.WriteLine(this.ProcessCommands(command));
        }

        private string ProcessCommands(string command)
        {
            string output;
            string [] commands = command.Split('.');
            switch (commands[0])
            {
                case "car":
                    output = this.ParkCar(commands[1], commands[2], int.Parse(commands[3]));
                    return output;
                case "motorbike":
                    output = this.ParkMotorbike(commands[1], commands[2], int.Parse(commands[3]));
                    return output;
                case "truck":
                    output = this.ParkTruck(commands[1], commands[2], int.Parse(commands[3]));
                    return output;
                default:
                    output = "Invalid command";
                    return output;
            }
        }

        private string ParkCar(string licensePlate, string ownerName, int reservedHours)
        {
            if (this.IsValidLicense(licensePlate))
            {
                var car = vehicleFactory.ParkCar(licensePlate, ownerName, reservedHours);
                this.vehicles[licensePlate] = car;
                return "Car with licnese \"" + licensePlate + "\" parked successfully!";
            }
            else
            {
                return "Invalid licanse";
            }
        }

        private string ParkMotorbike(string licensePlate, string ownerName, int reservedHours)
        {
            if (this.IsValidLicense(licensePlate))
            {
                var car = vehicleFactory.ParkCar(licensePlate, ownerName, reservedHours);
                this.vehicles[licensePlate] = car;
                return "Motorbike with licnese \"" + licensePlate + "\" parked successfully!";
            }
            else
            {
                return "Invalid licanse";
            }
        }

        private string ParkTruck(string licensePlate, string ownerName, int reservedHours)
        {
            if (this.IsValidLicense(licensePlate))
            {
                var car = vehicleFactory.ParkCar(licensePlate, ownerName, reservedHours);
                this.vehicles[licensePlate] = car;
                return "Truck with licnese \"" + licensePlate + "\" parked successfully!";
            }
            else
            {
                return "Invalid licanse";
            }
        }

        private bool SectorsCountCheck(int sectors)
        {
            if (sectors < 1)
            {
                return false;
            }

            return true;
        }

        private bool PlacesCountCheck(int places)
        {
            if (places < 1)
            {
                return false;
            }

            return true;
        }

        private bool IsValidLicense(string licenseNumber)
        {
            char[] licenseCharArray = licenseNumber.ToCharArray();
            string[] validLicensePatterns = new string[] { "AA1111AA", "A1111AA" };
            string currentLicensePattern = "";

            foreach (var currentChar in licenseCharArray)
            {
                bool isLetterChar = Char.IsLetter(currentChar);
                if (isLetterChar)
                {
                    currentLicensePattern += "A";
                }
                else
                {
                    currentLicensePattern += "1";
                }
            }

            for (int i = 0; i < validLicensePatterns.Length; i++)
            {
                if (validLicensePatterns[i] == currentLicensePattern)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsSectorExists(int sector)
        {
            if (sector > this.vehiclePark.Sectors)
            {
                return false;
            }

            return true;
        }

        private bool IsPlaceExists(int place)
        {
            if (place > this.vehiclePark.Places)
            {
                return false;
            }

            return true;
        }

        private bool IsPlaceFree(int sector, int place)
        {
            if (this.vehiclePark.ParkMatrix[sector, place] == 1)
            {
                return false;
            }

            return true;
        }

        private bool IsLicenseUnique(string license)
        {
            if (this.vehicles.Any(x => x.Key == license))
            {
                return false;
            }

            return true;
        }

        private bool IsLicenseExists(string license)
        {

            if (this.vehicles.Any(x => x.Key == license))
            {
                return true;
            }

            return false;
        }

        private bool IsOwnerExists(string owner)
        {
            if (this.vehicles.Any(x => x.Value.OwnerName == owner))
            {
                return true;
            }

            return false;
        }
    }
}
