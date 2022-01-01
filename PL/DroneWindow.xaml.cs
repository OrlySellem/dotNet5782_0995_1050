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

        #region ADD drone

        BlApi.IBL approachBL;     
        public DroneWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            updataGrid.Visibility = Visibility.Hidden;
            UpGrid.Visibility = Visibility.Visible;
      
            approachBL = bl;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));

            List <StationToList> stationFreeCharging = approachBL.display_station_with_freeChargingStations().ToList();

            foreach (var item in stationFreeCharging)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = item.Id;
                idStation.Items.Add(newItem);
            }

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

                List<StationToList> stationFreeCharging = approachBL.display_station_with_freeChargingStations().ToList();
                int idSt = stationFreeCharging[idStation.SelectedIndex].Id;

                approachBL.addDrone(newDrone, idSt);

                this.Close();

            }
            catch (AlreadyExistException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DoesntExistentObjectException ex)
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
            if (TextBoxId.Text != "" && TextBoxModel.Text != "" && WeightSelector.SelectedItem != null && idStation.SelectedItem != null)
                addDrone.IsEnabled = true;
            else
                addDrone.IsEnabled = false;

        }

        #endregion ADD drone

        //private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    MessageBox.Show(e.NewValue.ToString());
        //}

        #region updat Drone

        static DroneToList TheChosenDrone;
        public DroneWindow(BlApi.IBL bl, BO.DroneToList drone)
        {
            InitializeComponent();
            approachBL = bl;
            UpGrid.Visibility = Visibility.Hidden;
            updataGrid.Visibility = Visibility.Visible;
            TheChosenDrone = drone;


            id.Text = drone.Id.ToString();
            Model.Text = drone.Model.ToString();
            MaxWeight.Text = drone.MaxWeight.ToString();
            Battery.Text = drone.Battery.ToString();
            Status.Text = drone.Status.ToString();
            idParcel.Text = drone.idParcel.ToString();
            LattitudeTextBox.Text = drone.CurrentLocation.Lattitude.ToString();
            LongitudeTextBox.Text = drone.CurrentLocation.Longitude.ToString();


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
            try
            {
                if (TheChosenDrone.Status == DroneStatuses.available)
                    approachBL.chargingDrone(TheChosenDrone.Id);



                ReleaseDroneFromCharging.Visibility = Visibility.Visible;

                SendingDroneForCharging.Visibility = Visibility.Hidden;
                SendDroneForDelivery.Visibility = Visibility.Hidden;
                ParcelCollection.Visibility = Visibility.Hidden;
                ParcelArriveToDestination.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
        }

        private void ReleaseDroneFromCharging_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TheChosenDrone.Status == DroneStatuses.maintenance)
                    approachBL.freeDroneFromCharging(TheChosenDrone.Id, DateTime.Now);

                SendingDroneForCharging.Visibility = Visibility.Visible;
                SendDroneForDelivery.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void SendDroneForDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TheChosenDrone.Status == DroneStatuses.available)
                    approachBL.assignDroneToParcel(TheChosenDrone.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ParcelCollection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                approachBL.dronePickParcel(TheChosenDrone.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void ParcelArriveToDestination_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                approachBL.deliveryArivveToCustomer(TheChosenDrone.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Model.Text != "" && TheChosenDrone.Model != Model.Text)
                {
                    approachBL.updateModelDrone(TheChosenDrone.Id, Model.Text.ToString());
                    UpdateData.IsEnabled = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void moveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void OpenParcelWindow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ParcelToList p = (from parcel in approachBL.getAllParcels()
                                  where parcel.Id == TheChosenDrone.idParcel
                                  select parcel).FirstOrDefault();
                new ParcelWindow(approachBL, p).ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        #endregion updat Drone

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
