using System.Runtime.InteropServices.ComTypes;

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

        private readonly IUserInterface userInterface;

        private VehicleParkEngine()
        {
            this.vehicleParkFactory = new VehicleParkFactory();
            this.vehicleFactory = new VehicleFactory();
            this.vehicles = new Dictionary<string, IVehicle>();
            this.userInterface = new ConsoleInterface();
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

        public void Start()
        {
            var commands = this.ReadCommand();
            var processCommands = this.ProcessCommands(commands);
            this.userInterface.Output(processCommands);
        }

        private ICollection<ICommand> ReadCommand()
        {
            var commands = new List<ICommand>();

            foreach (var command in this.userInterface.Input())
            {
                commands.Add(Command.Parse(command));
            }

            return commands;
        }

        private IEnumerable<string> ProcessCommands(ICollection<ICommand> commands)
        {
            var commandResults = new List<string>();

            foreach (var command in commands)
            {
                string commandResult;

                switch (command.Name)
                {
                    case EngineConstants.SetupPark:
                        commandResult = this.SetupPark(int.Parse(command.Parameters["sectors"]), int.Parse(command.Parameters["placesPerSector"]));
                        break;
                    /*case EngineConstants.Park:
                        switch (command.Parameters["type"])
                        {
                            case EngineConstants.VehicleTypeCar:
                                if (!this.IsPlaceFree(int.Parse(command.Parameters["sector"]),
                                    int.Parse(command.Parameters["place"])))
                                {
                                    commandResult = string.Format(EngineConstants.OccupiedPlace,
                                        int.Parse(command.Parameters["sector"]), int.Parse(command.Parameters["place"]));
                                    commandResults.Add(commandResult);
                                    break;
                                }

                                if (!this.IsSectorExists(int.Parse(command.Parameters["sector"])))
                                {
                                    commandResult = string.Format(EngineConstants.WrongSector, int.Parse(command.Parameters["sector"]));
                                    commandResults.Add(commandResult);
                                    break;
                                }

                                if (!this.IsPlaceExists(int.Parse(command.Parameters["place"])))
                                {
                                    commandResult = string.Format(EngineConstants.WrongPlace,
                                        int.Parse(command.Parameters["place"]), int.Parse(command.Parameters["sector"]));
                                    commandResults.Add(commandResult);
                                    break;
                                }

                                commandResult = this.ParkCar(command.Parameters["licensePlate"],
                                        command.Parameters["owner"],
                                        int.Parse(command.Parameters["hours"]));
                                break;
                            case EngineConstants.VehicleTypeMotorbike:
                                if (!this.IsPlaceFree(int.Parse(command.Parameters["sector"]),
                                    int.Parse(command.Parameters["place"])))
                                {
                                    commandResult = string.Format(EngineConstants.OccupiedPlace,
                                        int.Parse(command.Parameters["sector"]), int.Parse(command.Parameters["place"]));
                                    commandResults.Add(commandResult);
                                    break;
                                }

                                if (!this.IsSectorExists(int.Parse(command.Parameters["sector"])))
                                {
                                    commandResult = string.Format(EngineConstants.WrongSector, int.Parse(command.Parameters["sector"]));
                                    commandResults.Add(commandResult);
                                    break;
                                }

                                if (!this.IsPlaceExists(int.Parse(command.Parameters["place"])))
                                {
                                    commandResult = string.Format(EngineConstants.WrongPlace,
                                        int.Parse(command.Parameters["place"]), int.Parse(command.Parameters["sector"]));
                                    commandResults.Add(commandResult);
                                    break;
                                }

                                commandResult = this.ParkMotorbike(command.Parameters["licensePlate"],
                                        command.Parameters["owner"],
                                        int.Parse(command.Parameters["hours"]));
                                break;
                            case EngineConstants.VehicleTypeTruck:
                                if (!this.IsPlaceFree(int.Parse(command.Parameters["sector"]),
                                    int.Parse(command.Parameters["place"])))
                                {
                                    commandResult = string.Format(EngineConstants.OccupiedPlace,
                                        int.Parse(command.Parameters["sector"]), int.Parse(command.Parameters["place"]));
                                    commandResults.Add(commandResult);
                                    break;
                                }

                                if (!this.IsSectorExists(int.Parse(command.Parameters["sector"])))
                                {
                                    commandResult = string.Format(EngineConstants.WrongSector, int.Parse(command.Parameters["sector"]));
                                    commandResults.Add(commandResult);
                                    break;
                                }

                                if (!this.IsPlaceExists(int.Parse(command.Parameters["place"])))
                                {
                                    commandResult = string.Format(EngineConstants.WrongPlace,
                                        int.Parse(command.Parameters["place"]), int.Parse(command.Parameters["sector"]));
                                    commandResults.Add(commandResult);
                                    break;
                                }

                                commandResult = this.ParkTruck(command.Parameters["licensePlate"],
                                        command.Parameters["owner"],
                                        int.Parse(command.Parameters["hours"]));
                                break;
                        }
                        break;*/
                    default:
                        throw new InvalidOperationException("Invalid command");
                }

                commandResults.Add(commandResult);
            }

            return commandResults;
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

        private string SetupPark(int sectors, int places)
        {
            string output = "";

            if (!this.IsSectorsPositive(sectors))
            {
                output = EngineConstants.WrongNumberOfSectors;
            }
            else if (!this.IsPlacesPositive(places))
            {
                output = EngineConstants.WrongNumberOfPlaces;
            }
            else
            {
                this.vehicleParkFactory.CreateVehiclePark(sectors, places);
                output = EngineConstants.ParkCreated;
            }

            return output;
        }

        private bool IsSectorsPositive(int sectors)
        {
            if (sectors < 1)
            {
                return false;
            }

            return true;
        }

        private bool IsPlacesPositive(int places)
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
