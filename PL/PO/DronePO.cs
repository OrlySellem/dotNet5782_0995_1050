//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.ComponentModel;


//namespace PL
//{
//    public class DronePO : INotifyPropertyChanged//(מחלקת עזר להזרמת מידע (נלמד בשיעור)
//    {
//        private int _Id;
//        public int Id
//        {
//            get { return Id; }
//            set
//            {
//                Id = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged(this, new PropertyChangedEventArgs("Id"));
//                }
//            }
//        }
//        private string _Model;
//        public string Model
//        {
//            get { return _Model; }
//            set
//            {
//                _Model = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged(this, new PropertyChangedEventArgs("Model"));
//                }
//            }
//        }
//        private BO.WeightCategories _MaxWeight;
//        public BO.WeightCategories MaxWeight
//        {
//            get { return _MaxWeight; }
//            set
//            {
//                _MaxWeight = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged(this, new PropertyChangedEventArgs("MaxWeight"));
//                }
//            }
//        }
//        private int _Battery;
//        public int Battery
//        {
//            get { return _Battery; }
//            set
//            {
//                _Battery = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged(this, new PropertyChangedEventArgs("Longitude"));
//                }
//            }
//        }
//        private int _Status;
//        public int Status
//        {
//            get { return _Status; }
//            set
//            {
//                _Status = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged(this, new PropertyChangedEventArgs("Latitude"));
//                }
//            }
//        }
//        private double _Battery;
//        public double Battery
//        {
//            get { return _Battery; }
//            set
//            {
//                _Battery = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
//                }
//            }
//        }
//        private BO.DroneStatuses _Status;
//        public BO.DroneStatuses Status
//        {
//            get { return _Status; }
//            set
//            {
//                _Status = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged(this, new PropertyChangedEventArgs("Status"));
//                }
//            }
//        }
//        private int _ParcelTransferredNumber;
//        public int ParcelTransferredNumber
//        {
//            get { return _ParcelTransferredNumber; }
//            set
//            {
//                _ParcelTransferredNumber = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged(this, new PropertyChangedEventArgs("ParcelTransferredNumber"));
//                }
//            }
//        }
//        private BO.ParcelInTransfer _ParcelInTransfer;
//        public BO.ParcelInTransfer ParcelInTransfer
//        {
//            get { return _ParcelInTransfer; }
//            set
//            {
//                _ParcelInTransfer = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged(this, new PropertyChangedEventArgs("ParcelInTransfer"));
//                }
//            }
//        }
//        public event PropertyChangedEventHandler PropertyChanged;
//    }
//}