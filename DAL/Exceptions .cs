using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;


namespace IDAL
{
    namespace DO
    {
        [Serializable]
        public class AlreadyExistException : Exception //Add An Existing Object Exception
        {
            public AlreadyExistException() : base() { }
            public AlreadyExistException(string message) : base(message) { }
            public AlreadyExistException(string message, Exception inner) : base(message, inner) { }
            protected AlreadyExistException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

            public override string ToString()
            {
                return string.Format("The {0} already exist in the system", Message);
            }
        }


        [Serializable]

        public class DoesntExistentObjectException : Exception   //class Update of an non Existing Object Exception 
        {
            public DoesntExistentObjectException() : base() { }
            public DoesntExistentObjectException(string message) : base(message) { }
            public DoesntExistentObjectException(string message, Exception inner) : base(message, inner) { }
            protected DoesntExistentObjectException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

            public override string ToString()
            {
                return string.Format("The {0} doesn't exist in the system", Message);
            }
        }

        [Serializable]

        public class chargingException : Exception 
        {
            public chargingException() : base() { }
            public chargingException(string message) : base(message) { }
            public chargingException(string message, Exception inner) : base(message, inner) { }
            protected chargingException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

            public override string ToString()
            {
                return string.Format("{0}", Message);
            }
        }
    }
}