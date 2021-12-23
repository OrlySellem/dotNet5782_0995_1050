using BlApi;
using BO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStationsWindow.xaml
    /// </summary>
    public partial class BaseStationsWindow : Window
    {
        BlApi.IBL approachBL;
        public BaseStationsWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            approachBL = bl;
            updataGrid.Visibility = Visibility.Hidden;
            addGrid.Visibility = Visibility.Visible;

        }

        static StationToList TheChosenBaseStation;
        public BaseStationsWindow(IBL bl, StationToList BaseStation)
        {
            InitializeComponent();
            approachBL = bl;
            TheChosenBaseStation = BaseStation;
            updataGrid.Visibility = Visibility.Visible;
            addGrid.Visibility = Visibility.Hidden;

            id.Text = BaseStation.Id.ToString();
            Name.Text = BaseStation.Name.ToString();
            FreeChargeSlots.Text = BaseStation.ChargeSlotsFree.ToString();
            FullChargeSlots.Text = BaseStation.ChargeSlotsFull.ToString();



        }

        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {


            if ((Name.Text != "" && TheChosenBaseStation.Name != int.Parse(Name.Text)))
            {
                approachBL.updateStation(TheChosenBaseStation.Id, int.Parse(Name.Text), int.Parse(AddChargeSlots.Text));
            }
        }

        private void cancelAddBaseStation_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void BaseStationToDelete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            approachBL.deleteFromStations(TheChosenBaseStation.Id);
        }

        private void addBaseStation_Click(object sender, RoutedEventArgs e)
        {
            Location address = new Location()
            {
                Longitude = SliderLattitude.Value,
                Lattitude = SliderLongitude.Value
            };
            Station newStation = new Station()
            {

                Id = int.Parse(TextBoxId.Text),
                Name = int.Parse(TextBoxName.Text),
                Address = address,
                ChargeSlots = int.Parse(TextBoxChargeSlots.Text),
                Charging_drones = null
            };
            approachBL.addStation(newStation);

            this.Close();
        }
    }
}
