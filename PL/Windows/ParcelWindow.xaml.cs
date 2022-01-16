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
using System.Collections.ObjectModel;
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        #region ADD Parcel

        BlApi.IBL approachBL;
        ObservableCollection<BO.ParcelToList> parcelsList = new ObservableCollection<ParcelToList>();
        private Parcel selectedParcel = new Parcel();

        public ParcelWindow(BlApi.IBL bl, ObservableCollection<ParcelToList> parcels)
        {
            InitializeComponent();
            approachBL = bl;
            parcelsList = parcels;
            updataGrid.Visibility = Visibility.Hidden;
            addGrid.Visibility = Visibility.Visible;

            senderSelector.ItemsSource = (from parcel in approachBL.getAllParcels()
                                          where parcel.Senderld != 0
                                          select parcel.Senderld).ToList().Distinct();

            targetSelector.ItemsSource = (from parcel in approachBL.getAllParcels()
                                          where parcel.Targetld != 0
                                          select parcel.Targetld).ToList().Distinct();

            weightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));

            prioritySelector.ItemsSource = Enum.GetValues(typeof(Priorities));



        }


        private void moveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void cancelAddParce_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void WeightSelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (senderSelector.SelectedItem != null && targetSelector.SelectedItem != null && weightSelector.SelectedItem != null && prioritySelector.SelectedItem != null)
                addParcel.IsEnabled = true;
            else
                addParcel.IsEnabled = false;
        }

        private void SenderSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (senderSelector.SelectedItem != null && targetSelector.SelectedItem != null && weightSelector.SelectedItem != null && prioritySelector.SelectedItem != null)
                addParcel.IsEnabled = true;
            else
                addParcel.IsEnabled = false;
        }

        private void TargetSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (senderSelector.SelectedItem != null && targetSelector.SelectedItem != null && weightSelector.SelectedItem != null && prioritySelector.SelectedItem != null)
                addParcel.IsEnabled = true;
            else
                addParcel.IsEnabled = false;
        }

        private void prioritySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (senderSelector.SelectedItem != null && targetSelector.SelectedItem != null && weightSelector.SelectedItem != null && prioritySelector.SelectedItem != null)
                addParcel.IsEnabled = true;
            else
                addParcel.IsEnabled = false;
        }

        private void addParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Parcel newParcel = new Parcel()
                {

                    Senderld = int.Parse(senderSelector.SelectedItem.ToString()),
                    Targetld = int.Parse(senderSelector.SelectedItem.ToString()),
                    Weight = (WeightCategories)weightSelector.SelectedItem,
                    Priority = (Priorities)prioritySelector.SelectedItem,
                    Requested = DateTime.Now
                };

                approachBL.addParcel(newParcel);
                MessageBoxResult result = MessageBox.Show("!החבילה נוספה בהצלחה");

                parcelsList.Clear();
                foreach (BO.ParcelToList p in approachBL.getAllParcels())
                    parcelsList.Add(p);

                this.Close();

            }
            catch (AlreadyExistException)
            {
                MessageBox.Show("", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DoesntExistentObjectException ex)
            {
                MessageBox.Show(ex.Message.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion ADD Parcel

        #region UPDATA Parcel


        public ParcelWindow(IBL bl, ParcelToList parcelToList, ObservableCollection<ParcelToList> parcels = null)
        {
            InitializeComponent();
            approachBL = bl;
            selectedParcel = approachBL.getParcel(parcelToList.Id);
            parcelsList = parcels;
            DataContext = selectedParcel;
            updataGrid.DataContext = selectedParcel;
            updataGrid.Visibility = Visibility.Visible;
            addGrid.Visibility = Visibility.Hidden;

            Requested.SelectedDate = selectedParcel.Requested;
            Requested.IsEnabled = false;
            Scheduled.IsEnabled = false;
            PickedUp.IsEnabled = false;
            Delivered.IsEnabled = false;

            if (selectedParcel.Scheduled == null)
            {
                delParcel.Visibility = Visibility.Visible;
            }
            else
            {
                delParcel.Visibility = Visibility.Hidden;
            }
        }
           

        //private void PickedUp_OR_Delivered_Checked(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (PickedUp_OR_Delivered.IsChecked != null)
        //        {
        //            if (TheChosenParcel.ParcelStatus == ParcelStatus.scheduled)
        //                approachBL.dronePickParcel(TheChosenParcel.Id);

        //            if (TheChosenParcel.ParcelStatus == ParcelStatus.PickedUp)
        //                approachBL.deliveryArivveToCustomer(TheChosenParcel.Id);
        //        }
        //    }
        //    catch (DoesntExistentObjectException ex)
        //    {
        //        MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    catch (AlreadyExistException ex)
        //    {
        //        MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }

        //}

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PickedUp != null)
                    approachBL.dronePickParcel(selectedParcel.Id, PickedUp.DisplayDate);

                if (Delivered != null)
                    approachBL.deliveryArivveToCustomer(selectedParcel.Id, Delivered.DisplayDate);

                UpdateData.IsEnabled = false;
            }
            catch (DelivereyAlreadyArrive)
            {
                MessageBox.Show("החבילה כבר אוספקה", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
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
        #endregion UPDATA Parcel



        private void ViewSender_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerToList c = (from customer in approachBL.getAllCustomers()
                                    where customer.Id == selectedParcel.Senderld
                                    select customer).FirstOrDefault();
                new CustomerWindow(approachBL, c).ShowDialog();
            }
            catch (DoesntExistentObjectException)
            {
                MessageBox.Show("הלקוח אינו קיים במערכת", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void ViewTargetr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerToList c = (from customer in approachBL.getAllCustomers()
                                    where customer.Id == selectedParcel.Targetld
                                    select customer).FirstOrDefault();
                new CustomerWindow(approachBL, c).ShowDialog();
            }
            catch (DoesntExistentObjectException)
            {
                MessageBox.Show("הלקוח אינו קיים במערכת", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ViewDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Parcel p = approachBL.getParcel(selectedParcel.Id);

                new DroneWindow(approachBL, approachBL.getDrone(p.Droneld)).ShowDialog();
            }
            catch (DoesntExistentObjectException)
            {
                MessageBox.Show("הרחפן לא קיים במערכת", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void deleteParcel_click(object sender, RoutedEventArgs e)
        {
            try
            {
                approachBL.deleteFromParcels(selectedParcel.Id);

                parcelsList.Clear();
                foreach (BO.ParcelToList p in approachBL.getAllParcels())
                    parcelsList.Add(p);

                this.Close();
            }
            catch (DoesntExistentObjectException)
            {
                MessageBox.Show("החבילה לא קיימת במערכת", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}