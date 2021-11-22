using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public class stationException : Exception  ///station
        {
            public stationException(int s) : DalObject()
            {
                throw "The station already exist";
            }
            public stationException(int s, string message) : base(message)
            {
                throw "The station isn't exist\n";
            }
            public stationException(int s,int av,  string message) : base(message)
            {
                throw "There aren't available charge slots\n";
            }
            // public override string ToString() => base.ToString() + $", miss information between stations: {FirstStation} and {SecondStation}";
        }


        public class droneException : Exception  ///drone
        {
            public droneException(int d) : base()
            {
                throw "The drone already exist\n";
            }
            public droneException(int d, string message) : base(message)
            {
                throw "The drone isn't exist\n";
            }
            public droneException(int d,char da, string message) : base(message)
            {
                throw "The drone isn't available\n";
            }
            // public override string ToString() => base.ToString() + $", miss information between stations: {FirstStation} and {SecondStation}";
        }

        public class customerException : Exception   ///customer
        {

            public customerException(int c) : base()
            {
                throw "The customer already exist\n";
            }
            public customerException(int c, string message) : base(message)
            {
                throw "The customer isn't exist\n";
            }
            // public override string ToString() => base.ToString() + $", miss information between stations: {FirstStation} and {SecondStation}";
        }

        public class parcelException : Exception   //parcel
        { 
            public parcelException(int p) : base()
            {
                throw "The parcel already exist\n";
            }
            public parcelException(int station1, string message) : base(message)
            {
                throw "The parcel isn't exist\n";
            }
            // public override string ToString() => base.ToString() + $", miss information between stations: {FirstStation} and {SecondStation}";
        }
    }

}

