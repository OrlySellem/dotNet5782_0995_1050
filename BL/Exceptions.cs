using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IBL
{
    namespace BO
    {
        [Serializable]
        public class UpdateProblemException : Exception
        {
            public UpdateProblemException() : base() { }
            public UpdateProblemException(Exception ex) : base() {}
            public UpdateProblemException(string message) : base(message) { }
            public UpdateProblemException(string message, Exception inner) : base(message, inner) { }
            protected UpdateProblemException(SerializationInfo info, StreamingContext context) : base(info, context) { }

            public override string ToString()
            {
                return "Error update ";
            }
        }

        [Serializable]
        public class GetDetailsProblemException : Exception
        {
            public GetDetailsProblemException() : base() { }
            public GetDetailsProblemException(string message) : base(message) { }
            public GetDetailsProblemException(string message, Exception inner) : base(message, inner) { }
            protected GetDetailsProblemException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

        [Serializable]
        public class DeletedProblemException : Exception
        {
            public DeletedProblemException() : base() { }
            public DeletedProblemException(string message) : base(message) { }
            public DeletedProblemException(string message, Exception inner) : base(message, inner) { }
            protected DeletedProblemException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

        [Serializable]
        public class AddingProblemException : Exception
        {
            public AddingProblemException() : base() { }
            public AddingProblemException(string message) : base(message) { }
            public AddingProblemException(string message, Exception inner) : base(message, inner) { }
            protected AddingProblemException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

        [Serializable]
        public class InvalidValueException : Exception
        {
            public InvalidValueException() : base() { }
            public InvalidValueException(string message) : base(message) { }
            public InvalidValueException(string message, Exception inner) : base(message, inner) { }
            protected InvalidValueException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

    }
}

    /* 

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
                public DroneChargeException(string message) : base(message)
                {
                    throw new message;
                }
            }
        }

    }
    } 
    
     */

