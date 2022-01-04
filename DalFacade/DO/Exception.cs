using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;//לבדוק את זה 
using System.Text;
using System.Threading.Tasks;

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

    public class stringException : Exception
    {
        public stringException() : base() { }
        public stringException(string message) : base(message) { }
        public stringException(string message, Exception inner) : base(message, inner) { }
        protected stringException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

        public override string ToString()
        {
            return string.Format("the string is wrong");
        }
    }

    public class LoadingException : Exception
    {
        private string filePath;
        private string v;
        private Exception ex;

        public LoadingException() : base() { }
        public LoadingException(string message) : base(message) { }
        public LoadingException(string message, Exception inner) : base(message, inner) { }
        public LoadingException(string filePath, string v, Exception ex)
        {
            this.filePath = filePath;
            this.v = v;
            this.ex = ex;
        }
        protected LoadingException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }


    }
}


