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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;
using BlApi;
namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStationsListPage.xaml
    /// </summary>
    public partial class BaseStationsListPage : Page
    {
        BlApi.IBL approachBL;
        public BaseStationsListPage(BlApi.IBL bl)
        {
            InitializeComponent();
            approachBL = bl;
            BaseStationsListView.ItemsSource = approachBL.getAllStations();

            //יוצר רשימה של כל מספרי תחנות הטענה הפנויות
            AmountOfFreeChargingStations.ItemsSource = (from findStations in approachBL.display_station_with_freeChargingStations()
                                                        select findStations.ChargeSlotsFree).ToList().Distinct();
        }

        private void AmountOfFreeChargingStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (AmountOfFreeChargingStations.SelectedItem != null)
            {
                BaseStationsListView.ItemsSource = (from findStations in approachBL.getAllStations()
                                                    where findStations.ChargeSlotsFree == int.Parse(AmountOfFreeChargingStations.SelectedItem.ToString())
                                                    select findStations).ToList();
            }

        }

        private void BaseStationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

                new BaseStationsWindow(approachBL, (StationToList)BaseStationsListView.SelectedItem).ShowDialog();
                BaseStationsListView.Items.Refresh();
            
        }

        private void FreeChargeSlots_Click(object sender, RoutedEventArgs e)
        {

                BaseStationsListView.ItemsSource = approachBL.display_station_with_freeChargingStations();
        }


        private void addBaseStationsToList_Click(object sender, RoutedEventArgs e) //add base station
        {
                new BaseStationsWindow(approachBL).ShowDialog(); 
                BaseStationsListView.ItemsSource = approachBL.getAllStations(); 
                MessageBoxResult result = MessageBox.Show("!תחנת הבסיס הוכנסה בהצלחה");

        }

    }

}
