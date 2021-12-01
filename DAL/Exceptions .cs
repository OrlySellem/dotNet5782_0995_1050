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

            public stationException(string message) : base(message)
            {
                throw "The station " + message;
            }

            // public override string ToString() => base.ToString() + $", miss information between stations: {FirstStation} and {SecondStation}";
        }


        public class droneException : Exception  ///drone
        {
            public droneException(string message) : base(message)
            {
                throw "The drone " + message;
            }
            // public override string ToString() => base.ToString() + $", miss information between stations: {FirstStation} and {SecondStation}";
        }

        public class customerException : Exception   ///customer
        {

            public customerException(string message) : base(message)
            {
                throw "The customer " + message;
            }

            // public override string ToString() => base.ToString() + $", miss information between stations: {FirstStation} and {SecondStation}";
        }

        public class parcelException : Exception   //parcel
        { 
            public parcelException(string message) : base(message)
            {
                throw "The parcel" + message;
            }
            // public override string ToString() => base.ToString() + $", miss information between stations: {FirstStation} and {SecondStation}";
        }

        public class DroneChargeException : Exception
        {
            public DroneChargeException (string message) : base(message)
            {
                throw new message;
            }
        }
    }

}

