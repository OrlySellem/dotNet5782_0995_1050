using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStationsListWindow.xaml
    /// </summary>
    public partial class BaseStationsListWindow : Window
    {
        BlApi.IBL approachBL;
        public BaseStationsListWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            approachBL = bl;
            BaseStationsListView.ItemsSource = approachBL.getAllStations();
            amountOfFreeChargingStations.ItemsSource = approachBL.display_station_with_freeChargingStations();
            //  BaseStationsListView.ItemsSource = Enum.GetValues(typeof(DroneStatuses));

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (amountOfFreeChargingStations.SelectedItem == null)//אם לא נבחר מספר תציג את כל המספרים
            {
                amountOfFreeChargingStations.ItemsSource = approachBL.display_station_with_freeChargingStations();
            }
            else //נבחר מספר תציג את כול התחנות שמקימות אותו 
            {
                amountOfFreeChargingStations.ItemsSource = (from findStations in approachBL.display_station_with_freeChargingStations()
                                                            where findStations.ChargeSlotsFree == (int)amountOfFreeChargingStations.SelectedItem
                                                            select findStations).ToList();

              //  amountOfFreeChargingStations.ItemsSource = dronesBL.GetDrones(x => x.Status == (DroneStatuses)StattusSelector.SelectedItem);
            }
        }

        private void BaseStationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MouseDoubleClick="ChooseBaseStationsToShow_DoubleClick"       מיועד לקבלת הרחפן אישי
        }
    }
}
