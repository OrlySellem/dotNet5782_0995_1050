using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace PL
{
    public class BaseStationPO : INotifyPropertyChanged//(מחלקת עזר להזרמת מידע (נלמד בשיעור)
    {
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Id"));
                }
            }
        }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }
        private int _ChargeSlotsFree;
        public int ChargeSlotsFree
        {
            get { return _ChargeSlotsFree; }
            set
            {
                _ChargeSlotsFree = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ChargeSlotsFree"));
                }
            }
        }
        private int _ChargeSlotsFull;
        public int ChargeSlotsFull
        {
            get { return _ChargeSlotsFull; }
            set
            {
                _ChargeSlotsFull = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ChargeSlotsFull"));
                }
            }
        }
  
        public event PropertyChangedEventHandler PropertyChanged;
    }
}