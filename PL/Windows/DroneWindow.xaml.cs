using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;
using System.ComponentModel;
using System.Diagnostics;
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
        BackgroundWorker Worker;

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

                MessageBoxResult result = MessageBox.Show("!הרחפן נוסף בהצלחה");

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

        #region updat Drone

        static DroneToList TheChosenDrone;
        public DroneWindow(BlApi.IBL bl, BO.DroneToList drone)
        {
            InitializeComponent();
            approachBL = bl;
            UpGrid.Visibility = Visibility.Hidden;
            updataGrid.Visibility = Visibility.Visible;
            TheChosenDrone = drone;

            //view the data of the chosen drone
            id.Text = drone.Id.ToString();
            Model.Text = drone.Model.ToString();
            MaxWeight.Text = drone.MaxWeight.ToString();
            PrecentsBattery.Content = drone.Battery.ToString() + " %";
            Status.Text = drone.Status.ToString();
            idParcel.Text = drone.idParcel.ToString();
            LattitudeTextBox.Text = drone.CurrentLocation.Lattitude.ToString();
            LongitudeTextBox.Text = drone.CurrentLocation.Longitude.ToString();

            Regular.Visibility = Visibility.Hidden;

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

            //Worker = new BackgroundWorker();
            //Worker.DoWork += Worker_DoWork;
            //Worker.ProgressChanged += Worker_ProgressChanged;
            //Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            //Worker.WorkerReportsProgress = true;
            //Worker.WorkerSupportsCancellation = true;
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
                MessageBoxResult result = MessageBox.Show("!הרחפן נשלח לטעינה בהצלחה");
            }
            catch (chargingException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DoesntExistentObjectException ex)
            {

                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
        }

        private void ReleaseDroneFromCharging_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TheChosenDrone.Status == DroneStatuses.maintenance)// if the drone in charging slot
                    approachBL.freeDroneFromCharging(TheChosenDrone.Id, DateTime.Now);

                ReleaseDroneFromCharging.Visibility = Visibility.Hidden;
                SendingDroneForCharging.Visibility = Visibility.Visible;
                assignDroneToParcel.Visibility = Visibility.Visible;
                MessageBoxResult result = MessageBox.Show("!הרחפן שוחרר מטעינה בהצלחה");

            }
            catch (DoesntExistentObjectException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (OnlyMaintenanceDroneWillBeAbleToBeReleasedFromCharging ex)
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
                MessageBoxResult result = MessageBox.Show("!הרחפן שוייך לחבילה בהצלחה");
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
                MessageBoxResult result = MessageBox.Show("!הרחפן אסף חבילה בהצלחה");
            }
            catch (DelivereyAlreadyArrive ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DeliveryCannotBeMade ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DoesntExistentObjectException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (AlreadyExistException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ParcelArriveToDestination_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                approachBL.deliveryArivveToCustomer(TheChosenDrone.Id);
                MessageBoxResult result = MessageBox.Show("!הרחפן ביצע את המשלוח בהצלחה");
            }
            catch (DroneCantBeAssigend ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DoesntExistentObjectException ex)
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
            catch (DoesntExistentObjectException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (AlreadyExistException ex)
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
            catch (DoesntExistentObjectException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        #region thread
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int length = (int)e.Argument;

            for (int i = 1; i <= length; i++) 
            {
                if (Worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    Thread.Sleep(500);
                    if (Worker.WorkerReportsProgress == true)
                        Worker.ReportProgress(i * 100 / length);
                }
            }
            e.Result = stopwatch.ElapsedMilliseconds;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            PrecentsBattery.Content = (progress + "%");
            BatteryProgressBar.Value = progress;
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                PrecentsBattery.Content = "Canceled!";
            }
            else if (e.Error != null)
            {
                PrecentsBattery.Content = "Error: " + e.Error.Message; // Exception Message
            }
            else
            {
                long result = (long)e.Result;
                if (result < 100)
                {
                    PrecentsBattery.Content = result + " %";
                    
                    TheChosenDrone.Battery = result;
                }
                else
                {
                    PrecentsBattery.Content = 100 + " %";
                    
                    TheChosenDrone.Battery = result;
                }
                   
            }
        }
        private void Automatic_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow.Visibility = Visibility.Hidden;
            UpdateData.Visibility = Visibility.Hidden;
            SendingDroneForCharging.Visibility = Visibility.Hidden;
            ReleaseDroneFromCharging.Visibility = Visibility.Hidden;
            assignDroneToParcel.Visibility = Visibility.Hidden;
            pickUpParcel.Visibility = Visibility.Hidden;
            ParcelArriveToCustomer.Visibility = Visibility.Hidden;
            Regular.Visibility = Visibility.Visible;


            if (Worker.IsBusy != true)
            {
                Worker.RunWorkerAsync(35);
            }
           





        }

        private void Manual_Click(object sender, RoutedEventArgs e)
        {

            CloseWindow.Visibility = Visibility.Visible;
            UpdateData.Visibility = Visibility.Visible;
            SendingDroneForCharging.Visibility = Visibility.Hidden;
            ReleaseDroneFromCharging.Visibility = Visibility.Hidden;
            assignDroneToParcel.Visibility = Visibility.Hidden;
            pickUpParcel.Visibility = Visibility.Hidden;
            ParcelArriveToCustomer.Visibility = Visibility.Hidden;
            Regular.Visibility = Visibility.Visible;
        }
        #endregion thread


        #endregion updat Drone

    }
}
