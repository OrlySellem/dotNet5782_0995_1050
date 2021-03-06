using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace BO
{
    /// <summary>
    /// Add An Existing Object Exception
    /// </summary>
    [Serializable]
    public class AlreadyExistException : Exception
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

    /// <summary>
    /// Non Existent Object Exception
    /// </summary>
    [Serializable]
    public class DoesntExistentObjectException : Exception
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

    /// <summary>
    /// charging Exception
    /// </summary>
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

    /// <summary>
    /// No Free Charging Stations
    /// </summary>
    [Serializable]
    public class NoFreeChargingStations : Exception
    {
        public NoFreeChargingStations() : base() { }
        public NoFreeChargingStations(string message) : base(message) { }
        public NoFreeChargingStations(string message, Exception inner) : base(message, inner) { }
        protected NoFreeChargingStations(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

        public override string ToString()
        {
            return "Error! There are no free charging stations in this basestation";
        }
    }
    /// <summary>
    /// More Drone In Charging Than The Proposed ChargingStations
    /// </summary>
    [Serializable]
    public class MoreDroneInChargingThanTheProposedChargingStations : Exception
    {
        public MoreDroneInChargingThanTheProposedChargingStations() : base() { }
        public MoreDroneInChargingThanTheProposedChargingStations(string message) : base(message) { }
        public MoreDroneInChargingThanTheProposedChargingStations(string message, Exception inner) : base(message, inner) { }
        protected MoreDroneInChargingThanTheProposedChargingStations(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

        public override string ToString()
        {
            return "Error More drone in charging than the proposed charging stations";
        }
    }
    /// <summary>
    /// The Drone Can Not Be Sent For Charging
    /// </summary>
    [Serializable]
    public class TheDroneCanNotBeSentForCharging : Exception
    {
        public TheDroneCanNotBeSentForCharging() : base() { }
        public TheDroneCanNotBeSentForCharging(string message) : base(message) { }
        public TheDroneCanNotBeSentForCharging(string message, Exception inner) : base(message, inner) { }
        protected TheDroneCanNotBeSentForCharging(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

        public override string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// Only Maintenance Drone Will Be Able To Be Released From Charging
    /// </summary>
    [Serializable]
    public class OnlyMaintenanceDroneWillBeAbleToBeReleasedFromCharging : Exception
    {
        public OnlyMaintenanceDroneWillBeAbleToBeReleasedFromCharging() : base() { }
        public OnlyMaintenanceDroneWillBeAbleToBeReleasedFromCharging(string message) : base(message) { }
        public OnlyMaintenanceDroneWillBeAbleToBeReleasedFromCharging(string message, Exception inner) : base(message, inner) { }
        protected OnlyMaintenanceDroneWillBeAbleToBeReleasedFromCharging(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

        public override string ToString()
        {
            return "Error Only a maintenance drone will be able to be released from charging";
        }
    }
    /// <summary>
    /// No Suitable Psrcel Was Found To Belong To The Drone
    /// </summary>
    [Serializable]
    public class NoSuitablePsrcelWasFoundToBelongToTheDrone : Exception
    {
        public NoSuitablePsrcelWasFoundToBelongToTheDrone() : base() { }
        public NoSuitablePsrcelWasFoundToBelongToTheDrone(string message) : base(message) { }
        public NoSuitablePsrcelWasFoundToBelongToTheDrone(string message, Exception inner) : base(message, inner) { }
        protected NoSuitablePsrcelWasFoundToBelongToTheDrone(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

        public override string ToString()
        {
            return "Error! No suitable package was found to belong to the drone";
        }
    }

    /// <summary>
    /// Drone Cant Be Assigend
    /// </summary>
    [Serializable]
    public class DroneCantBeAssigend : Exception
    {
        public DroneCantBeAssigend() : base() { }
        public DroneCantBeAssigend(string message) : base(message) { }
        public DroneCantBeAssigend(string message, Exception inner) : base(message, inner) { }
        protected DroneCantBeAssigend(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

        public override string ToString()
        {
            return "Error the drone is not free";
        }
    }
    /// <summary>
    ///  Deliverey already arrive
    /// </summary>
    [Serializable]
    public class DelivereyAlreadyArrive : Exception
    {
        public DelivereyAlreadyArrive() : base() { }
        public DelivereyAlreadyArrive(string message) : base(message) { }
        public DelivereyAlreadyArrive(string message, Exception inner) : base(message, inner) { }
        protected DelivereyAlreadyArrive(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

        public override string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// Delivery Cannot Be Made
    /// </summary>
    [Serializable]
    public class DeliveryCannotBeMade : Exception
    {
        public DeliveryCannotBeMade() : base() { }
        public DeliveryCannotBeMade(string message) : base(message) { }
        public DeliveryCannotBeMade(string message, Exception inner) : base(message, inner) { }
        protected DeliveryCannotBeMade(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

        public override string ToString()
        {
            return Message;
        }
    }

    public class enoughBattery : Exception
    {
        public enoughBattery() : base() { }
        public enoughBattery(string message) : base(message) { }
        public enoughBattery(string message, Exception inner) : base(message, inner) { }
        protected enoughBattery(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

        public override string ToString()
        {
            return Message;
        }
    }



    public class string_Exception : Exception
    {
        public string_Exception() : base() { }
        public string_Exception(string message) : base(message) { }
        public string_Exception(string message, Exception inner) : base(message, inner) { }
        protected string_Exception(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

        public override string ToString()
        {
            return string.Format("the string is wrong");
        }
    }
}
