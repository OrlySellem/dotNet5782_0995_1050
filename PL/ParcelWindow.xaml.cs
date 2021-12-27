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
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {



        #region ADD Parcel

        BlApi.IBL approachBL;
        public ParcelWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            approachBL = bl;
            updataGrid.Visibility = Visibility.Hidden;
            addGrid.Visibility = Visibility.Visible;

            senderSelector.ItemsSource = (from parcel in approachBL.getAllParcels()
                                          where parcel.Senderld != 0
                                          select parcel.Senderld).ToList();

            targetSelector.ItemsSource = (from parcel in approachBL.getAllParcels()
                                          where parcel.Targetld != 0
                                          select parcel.Targetld).ToList();

            weightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));

            prioritySelector.ItemsSource = Enum.GetValues(typeof(Priorities));



        }
        #endregion ADD Parcel

        #region UPDATA Parcel

        static ParcelToList TheChosenParcel;
        public ParcelWindow(IBL bl, ParcelToList parcel)
        {
            InitializeComponent();
            approachBL = bl;
            TheChosenParcel = parcel;
            updataGrid.Visibility = Visibility.Visible;
            addGrid.Visibility = Visibility.Hidden;

            senderId.Text = TheChosenParcel.Senderld.ToString();
            targetId.Text = TheChosenParcel.Targetld.ToString();
            weightSelector.Text = TheChosenParcel.Weight.ToString();
            prioritySelector.Text = TheChosenParcel.Priority.ToString();
            parcelStatusTextBox.Text = TheChosenParcel.ParcelStatus.ToString();
            //parcelStatusComboBox.ItemsSource = Enum.GetValues(typeof(ParcelStatus));

            if (parcel.ParcelStatus == ParcelStatus.scheduled || parcel.ParcelStatus == ParcelStatus.PickedUp)
                PickedUp_OR_Delivered.Visibility = Visibility.Visible;
            else
                PickedUp_OR_Delivered.Visibility = Visibility.Hidden;
        }
        #endregion UPDATA Parcel

        private void cancelAddBaseStation_Click(object sender, RoutedEventArgs e)
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

        private void senderSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (senderSelector.SelectedItem != null && targetSelector.SelectedItem != null && weightSelector.SelectedItem != null && prioritySelector.SelectedItem != null)
                addParcel.IsEnabled = true;
            else
                addParcel.IsEnabled = false;
        }

        private void targetSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                   
                   Senderld= int.Parse(senderSelector.SelectedItem.ToString()),
                   Targetld = int.Parse(senderSelector.SelectedItem.ToString()),
                   Weight = (WeightCategories)weightSelector.SelectedItem,
                   Priority=(Priorities)prioritySelector.SelectedItem
                };

                approachBL.addParcel(newParcel);

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

        private void parcelStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PickedUp_OR_Delivered_Checked(object sender, RoutedEventArgs e)
        {
            if (PickedUp_OR_Delivered.IsChecked != null)
            {
                if(TheChosenParcel.ParcelStatus== ParcelStatus.scheduled)
                approachBL.dronePickParcel(TheChosenParcel.Id);
                if (TheChosenParcel.ParcelStatus == ParcelStatus.PickedUp)
                    approachBL.deliveryAriveToCustomer(TheChosenParcel.Id);

            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
                if (TheChosenParcel.ParcelStatus == ParcelStatus.scheduled)
                    approachBL.dronePickParcel(TheChosenParcel.Id);
                if (TheChosenParcel.ParcelStatus == ParcelStatus.PickedUp)
                    approachBL.deliveryAriveToCustomer(TheChosenParcel.Id);

            
        }
    }
}
