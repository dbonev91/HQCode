using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehiclePark.Engine
{
    public static class EngineConstants
    {
        #region Commands
        public const string SetupPark = "SetupPark";
        public const string Park = "Park";
        public const string Exit = "Exit";
        public const string Ststus = "Status";
        public const string FindVehicle = "FindVehicle";
        public const string VehiclesByOwner = "VehiclesByOwner";
        #endregion

        #region Vehicle types
        public const string VehicleTypeCar = "car";
        public const string VehicleTypeMotorbike = "motorbike";
        public const string VehicleTypeTruck = "truck";
        #endregion

        #region Error messages
        public const string WrongNumberOfSectors = "The number of sectors must be positive";
        public const string WrongNumberOfPlaces = "The number of places per sector must be positive";
        public const string VehicleParkDoesntExists = "The vehicle park has not been set up";
        public const string WrongSector = "There is no sector {0} in the park";
        public const string WrongPlace = "There is no place {0} in sector {1}";
        public const string OccupiedPlace = "The place ({0},{1}) is occupied";
        public const string DublicatedPlateNumber = "There is already a vehicle with license plate {0} in the park";
        public const string LicensePlateDoesntExists = "There is no vehicle with license plate {0} in the park";
        public const string OwnerDoesntExists = "No vehicles by {0}";
        #endregion

        #region Success messages
        public const string ParkCreated = "Vehicle park created";
        public const string ParkSuccessfuly = "{0} parked successfully at place ({1},{2})";
        public const string VehicleFounded = "{0} [{1}], owned by {2}\nParked at ({3},{4})";
        #endregion
    }
}
