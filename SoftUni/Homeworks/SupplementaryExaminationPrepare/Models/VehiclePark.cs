namespace VehiclePark.Models
{
    using Interfaces;
    using System.Collections.Generic;

    public class VehiclePark : IVehiclePark
    {
        private int sectors;
        private int places;
        private int[,] parkMatrix;
        private IList<IVehicle> vehicleCollection;

        public VehiclePark(int secotrs, int places)
        {
            this.Sectors = secotrs;
            this.Places = places;
        }

        public int Sectors
        {
            get
            {
                return this.sectors;
            }
            set
            {
                this.sectors = value;
            }
        }

        public int Places
        {
            get
            {
                return this.places;
            }
            set
            {
                this.places = value;
            }
        }

        public int[,] ParkMatrix
        {
            get { throw new System.NotImplementedException(); }
            set
            {
                for (int i = 0; i < this.Sectors; i++)
                {
                    for (int j = 0; j < this.Places; j++)
                    {
                        this.ParkMatrix[i, j] = 0;
                    }
                }
            }
        }

        public IList<IVehicle> VehicleCollection
        {
            get { throw new System.NotImplementedException(); }
        }

        public void AddVehicle(IVehicle vehicle)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveVehicle(IVehicle vehicle)
        {
            throw new System.NotImplementedException();
        }

        public string Status()
        {
            throw new System.NotImplementedException();
        }

        public string FindVehicle(string plateNumber)
        {
            throw new System.NotImplementedException();
        }

        public string FindVehicleByOwner(string ownerName)
        {
            throw new System.NotImplementedException();
        }
    }
}
