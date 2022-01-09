namespace BO
{
    public class DroneToList
    {
        public int                       Id                { get; set; }

        public string                    Model              { get; set; }

        public WeightCategories          MaxWeight          { get; set; }

        public double                    Battery            { get; set; }
         
        public DroneStatuses             Status             { get; set; }

        public Location                 CurrentLocation     { get; set; }

        public int                      idParcel            { get; set; }

        public override string ToString()
        {
            return string.Format("\nId is:{0}\nModel is:{1}\nMaxWeight is:{2}\nBattery is:{3}\nStatus is:{4}\nThe current location is:{5}Parcel in transfer is:{6}\n", Id, Model, MaxWeight, Battery, Status, CurrentLocation, idParcel);
        }

    }

}
