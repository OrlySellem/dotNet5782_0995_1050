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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        BlApi.IBL droneBL;

     
        public DroneWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            updataGrid.Visibility = Visibility.Hidden;
            UpGrid.Visibility = Visibility.Visible;
      
            droneBL = bl;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));

            List <StationToList> stationFreeCharging = droneBL.display_station_with_freeChargingStations().ToList();

            for (int i = 0; i < stationFreeCharging.Count(); i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = stationFreeCharging[i].Id;
                idStation.Items.Add(newItem);
            }

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MessageBox.Show(e.NewValue.ToString());
        }

        private void addDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Drone newDrone = new Drone()
                {
                    
                    Id = int.Parse(TextBoxId.Text),
                    Model = TextBoxModel.Text,
                    MaxWeight = (WeightCategories)WeightSelector.SelectedItem
                };

                List<StationToList> stationFreeCharging = droneBL.display_station_with_freeChargingStations().ToList();
                int idSt = stationFreeCharging[idStation.SelectedIndex].Id;

                droneBL.addDrone(newDrone, idSt);

                this.Close();

            }
            catch (AlreadyExistException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(DoesntExistentObjectException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (TextBoxId.Text != "" && TextBoxModel.Text != "" && WeightSelector.SelectedItem != null && idStation.SelectedItem != null)
                addDrone.IsEnabled = true;
            else
                addDrone.IsEnabled = false;
        }

        private void idStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TextBoxId.Text != "" && TextBoxModel.Text != "" && WeightSelector.SelectedItem != null && idStation.SelectedItem != null)
                addDrone.IsEnabled = true;
            else
                addDrone.IsEnabled = false;
        }

        private void cancelAddDrone_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBoxModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxId.Text!="" && TextBoxModel.Text!=""&& WeightSelector.SelectedItem!= null && idStation.SelectedItem!=null)
                addDrone.IsEnabled = true;
            else
                addDrone.IsEnabled = false;

        }




        //פעולות
        //סגירת חלון
        //לא סיימתי להחביא את  הכפתורים הניצרכים

        static DroneToList TheChosenDrone;
        public DroneWindow(BlApi.IBL bl, BO.DroneToList drone)
        {
            InitializeComponent();
            droneBL = bl;
            UpGrid.Visibility = Visibility.Hidden;
            updataGrid.Visibility = Visibility.Visible;
            TheChosenDrone = drone;


            id.Text = drone.Id.ToString();
            Model.Text = drone.Model.ToString();
            MaxWeight.Text = drone.Model.ToString();
            Battery.Text = drone.Battery.ToString();
            Status.Text = drone.Status.ToString();
            Lattitude.Text = drone.CurrentLocation.Lattitude.ToString();
            Longitude.Text = drone.CurrentLocation.Longitude.ToString();


            if (drone.Status == DroneStatuses.available)
            {
                ReleaseDroneFromCharging.Visibility = Visibility.Hidden;
                ParcelCollection.Visibility = Visibility.Hidden;
                ParcelArriveToDestination.Visibility = Visibility.Hidden;
            }

            if (drone.Status == DroneStatuses.maintenance)
            {
                SendingDroneForCharging.Visibility = Visibility.Hidden;
                SendDroneForDelivery.Visibility = Visibility.Hidden;
                ParcelCollection.Visibility = Visibility.Hidden;
                ParcelArriveToDestination.Visibility = Visibility.Hidden;
            }

            if (drone.Status == DroneStatuses.delivery)
            {
                SendingDroneForCharging.Visibility = Visibility.Hidden;
                ReleaseDroneFromCharging.Visibility = Visibility.Hidden;
                SendDroneForDelivery.Visibility = Visibility.Hidden;
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SendingDroneForCharging_Click(object sender, RoutedEventArgs e)
        {
            if(TheChosenDrone.Status== DroneStatuses.available)
            droneBL.chargingDrone(TheChosenDrone.Id);

        
            
            ReleaseDroneFromCharging.Visibility = Visibility.Visible;

            SendingDroneForCharging.Visibility = Visibility.Hidden;
            SendDroneForDelivery.Visibility = Visibility.Hidden;
            ParcelCollection.Visibility = Visibility.Hidden;
            ParcelArriveToDestination.Visibility = Visibility.Hidden;

        }

        private void ReleaseDroneFromCharging_Click(object sender, RoutedEventArgs e)
        {
            if (TheChosenDrone.Status == DroneStatuses.maintenance)
                droneBL.freeDroneFromCharging(TheChosenDrone.Id,DateTime.Now);



            SendingDroneForCharging.Visibility = Visibility.Visible;
            SendDroneForDelivery.Visibility = Visibility.Visible;
        }

        private void SendDroneForDelivery_Click(object sender, RoutedEventArgs e)
        {
            if (TheChosenDrone.Status == DroneStatuses.available)
                droneBL.assignDroneToParcel(TheChosenDrone.Id);
        }

        private void ParcelCollection_Click(object sender, RoutedEventArgs e)
        {
            droneBL.dronePickParcel(TheChosenDrone.Id);
        }

        private void ParcelArriveToDestination_Click(object sender, RoutedEventArgs e)
        {
            droneBL.deliveryAriveToCustomer(TheChosenDrone.Id);
        }

        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
            if (Model.Text != ""&& TheChosenDrone.Model!= Model.Text)
            {
                string t = Model.Text;
                droneBL.updateModelDrone(TheChosenDrone.Id, t);
            }
        
        }

        //private void cancelAddDrone_Click(object sender, RoutedEventArgs e)
        //{
        //    cancelAddDrone.IsEnabled;
        //}

        /*     <ComboBox.ItemTemplate>
                <DataTemplate>
                    <TextBlock Text="{Binding Name}"/>
                </DataTemplate>
              </ComboBox.ItemTemplate>
            </ComboBox>
        */

    }
}
