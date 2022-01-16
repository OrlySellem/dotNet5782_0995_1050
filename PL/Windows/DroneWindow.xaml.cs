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
using System.Collections.ObjectModel;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        BlApi.IBL approachBL;


        //private DronePO dronePO = new DronePO();
        #region ADD drone



        public DroneWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            updataGrid.Visibility = Visibility.Hidden;
            addGrid.Visibility = Visibility.Visible;

            approachBL = bl;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));

            List<StationToList> stationFreeCharging = approachBL.display_station_with_freeChargingStations().ToList();

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
                int idSt = stationFreeCharging [idStation.SelectedIndex].Id;

                approachBL.addDrone(newDrone, idSt);

                MessageBoxResult result = MessageBox.Show("!הרחפן נוסף בהצלחה");

                this.Close();

            }
            catch (AlreadyExistException)
            {
                MessageBox.Show("הרחפן כבר קיים במערכת", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DoesntExistentObjectException)
            {
                MessageBox.Show("הרחפן לא קיים במערכת", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
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
        static ParcelToList parcel;

        public DroneWindow(BlApi.IBL bl, BO.DroneToList drone)
        {
            InitializeComponent();
            DataContext = drone;
            approachBL = bl;
            addGrid.Visibility = Visibility.Hidden;
            updataGrid.Visibility = Visibility.Visible;
            TheChosenDrone = drone;
            updataGrid.DataContext = drone;



            Regular.Visibility = Visibility.Hidden;

            if (drone.Status == DroneStatuses.available)
            {
                SendingDroneForCharging.Visibility = Visibility.Visible;
                ReleaseDroneFromCharging.Visibility = Visibility.Hidden;
                assignDroneToParcel.Visibility = Visibility.Visible;
                pickUpParcel.Visibility = Visibility.Hidden;
                ParcelArriveToCustomer.Visibility = Visibility.Hidden;
            }

            if (drone.Status == DroneStatuses.maintenance)
            {
                SendingDroneForCharging.Visibility = Visibility.Hidden;
                ReleaseDroneFromCharging.Visibility = Visibility.Visible;
                assignDroneToParcel.Visibility = Visibility.Hidden;
                pickUpParcel.Visibility = Visibility.Hidden;
                ParcelArriveToCustomer.Visibility = Visibility.Hidden;
            }

            if (drone.Status == DroneStatuses.delivery)
            {
                parcel = (from findParcel in approachBL.getAllParcels()
                          where findParcel.Id == drone.idParcel
                          select findParcel).FirstOrDefault();

                SendingDroneForCharging.Visibility = Visibility.Hidden;
                ReleaseDroneFromCharging.Visibility = Visibility.Hidden;
                assignDroneToParcel.Visibility = Visibility.Hidden;
                pickUpParcel.Visibility = Visibility.Hidden;
                ParcelArriveToCustomer.Visibility = Visibility.Hidden;

                if (parcel.ParcelStatus == ParcelStatus.scheduled)
                    pickUpParcel.Visibility = Visibility.Visible;

                if (parcel.ParcelStatus == ParcelStatus.PickedUp)
                    ParcelArriveToCustomer.Visibility = Visibility.Visible;
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
                {
                    if (TheChosenDrone.Battery >= 100)
                    {
                        MessageBox.Show("הבטריה מלאה", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    approachBL.chargingDrone(TheChosenDrone.Id);

                    SendingDroneForCharging.Visibility = Visibility.Hidden;
                    ReleaseDroneFromCharging.Visibility = Visibility.Visible;
                    assignDroneToParcel.Visibility = Visibility.Hidden;
                    pickUpParcel.Visibility = Visibility.Hidden;
                    ParcelArriveToCustomer.Visibility = Visibility.Hidden;

                    MessageBoxResult result = MessageBox.Show("!הרחפן נשלח לטעינה בהצלחה");
                }
            }
            catch (chargingException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TheDroneCanNotBeSentForCharging ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DoesntExistentObjectException)
            {

                MessageBox.Show("הרחפן לא קיים במערכת", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ReleaseDroneFromCharging_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TheChosenDrone.Status == DroneStatuses.maintenance)// if the drone in charging slot
                {
                    approachBL.freeDroneFromCharging(TheChosenDrone.Id, DateTime.Now);

                    //SendingDroneForCharging.Visibility = Visibility.Visible;
                    ReleaseDroneFromCharging.Visibility = Visibility.Hidden;
                    assignDroneToParcel.Visibility = Visibility.Visible;
                    pickUpParcel.Visibility = Visibility.Hidden;
                    ParcelArriveToCustomer.Visibility = Visibility.Hidden;

                    MessageBoxResult result = MessageBox.Show("!הרחפן שוחרר מטעינה בהצלחה");
                }

            }
            catch (DoesntExistentObjectException)
            {
                MessageBox.Show("הרחפן לא קיים במערכת", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (OnlyMaintenanceDroneWillBeAbleToBeReleasedFromCharging ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void AssignDroneToParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TheChosenDrone.Status == DroneStatuses.available)
                {
                    approachBL.assignDroneToParcel(TheChosenDrone.Id, DateTime.Now);
                    MessageBoxResult result = MessageBox.Show("!הרחפן שוייך לחבילה בהצלחה");

                    SendingDroneForCharging.Visibility = Visibility.Hidden;
                    ReleaseDroneFromCharging.Visibility = Visibility.Hidden;
                    assignDroneToParcel.Visibility = Visibility.Hidden;
                    pickUpParcel.Visibility = Visibility.Visible;
                    ParcelArriveToCustomer.Visibility = Visibility.Hidden;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void PickUpParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                approachBL.dronePickParcel(TheChosenDrone.Id, DateTime.Now);
                MessageBoxResult result = MessageBox.Show("!הרחפן אסף חבילה בהצלחה");

                SendingDroneForCharging.Visibility = Visibility.Hidden;
                ReleaseDroneFromCharging.Visibility = Visibility.Hidden;
                assignDroneToParcel.Visibility = Visibility.Hidden;
                pickUpParcel.Visibility = Visibility.Hidden;
                ParcelArriveToCustomer.Visibility = Visibility.Visible;

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

        private void ParcelArriveToCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                approachBL.deliveryArivveToCustomer(TheChosenDrone.Id, DateTime.Now);
                MessageBoxResult result = MessageBox.Show("!הרחפן ביצע את המשלוח בהצלחה");

                SendingDroneForCharging.Visibility = Visibility.Visible;
                ReleaseDroneFromCharging.Visibility = Visibility.Hidden;
                assignDroneToParcel.Visibility = Visibility.Visible;
                pickUpParcel.Visibility = Visibility.Hidden;
                ParcelArriveToCustomer.Visibility = Visibility.Hidden;

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
            if (TheChosenDrone.idParcel == 0)
            {
                MessageBox.Show("הרחפן לא משוייך לחבילה", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }

            try
            {

                ParcelToList p = (from parcel in approachBL.getAllParcels()
                                  where parcel.Id == TheChosenDrone.idParcel
                                  select parcel).FirstOrDefault();
                new ParcelWindow(approachBL , p).ShowDialog();
            }
            catch (DoesntExistentObjectException)
            {
                MessageBox.Show("החבילה לא קיימת", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        #region thread

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


        BackgroundWorker Worker;


        private void updateDrone() => Worker.ReportProgress(2);
        private bool checkStop() => Worker.CancellationPending;

        #region Simulator
        private void Automatic_Click(object sender, RoutedEventArgs e)
        {
            Regular.IsEnabled = true;
            Worker = new BackgroundWorker();
            Worker.DoWork += (sender, args) => approachBL.openSimulator((int)args.Argument, updateDrone, checkStop);
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            Worker.RunWorkerAsync(int.Parse(id.Text));

            Worker.WorkerReportsProgress = true;
            Worker.WorkerSupportsCancellation = true;

        }


        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e) //פונקיה שמעדכנת את השינוים
        {
            //view the data of the chosen drone
            DroneToList drone = approachBL.getDrone(TheChosenDrone.Id);
            //PrecentsBattery.Text = drone.Battery.ToString() + " %";
            //Status.Text = drone.Status.ToString();
            //idParcel.Text = drone.idParcel.ToString();
            //LocationTextBox.Text = drone.CurrentLocation.ToString();

            DataContext = drone;
            int progress = e.ProgressPercentage;
            BatteryProgressBar.Value = progress;

        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) //cancel panding
        {
            Worker = null;
            Regular.IsEnabled = false;
            Close();
        }

        #endregion Simulator

        #endregion updat Drone

        private void Regular_Click(object sender, RoutedEventArgs e)
        {
            if (Worker.WorkerSupportsCancellation == true)
                Worker.CancelAsync(); // Cancel the asynchronous operation.
        }


    }
}