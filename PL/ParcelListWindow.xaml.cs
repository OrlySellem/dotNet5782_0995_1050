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
    /// Interaction logic for ParcelListWindow.xaml
    /// </summary>
    public partial class ParcelListWindow : Window
    {

        BlApi.IBL approachBL;
        public ParcelListWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            approachBL = bl;
            //BaseStationsListView.ItemsSource = approachBL.getAllStations();

            senderOrTargetID.ItemsSource = approachBL.getAllParcels();

        }

        private void ParcelListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            new ParcelWindow(approachBL, (ParcelToList)ParcelListView.SelectedItem).ShowDialog();
        }

        private void senderCheckBox_check(object sender, RoutedEventArgs e)
        {
            if (senderCheckBox.IsChecked != null && targetCheckBox == null)
            {
                senderOrTargetID.ItemsSource = (from parcel in approachBL.getAllParcels()
                                                where parcel.Senderld != 0
                                                select parcel.Senderld).ToList();

                senderOrTargetID.IsEnabled = true;
            }
            else if (senderCheckBox.IsChecked != null && targetCheckBox != null)
            {
                MessageBox.Show("Please check one option");
            }

        }

        private void targetCheckBox_check(object sender, RoutedEventArgs e)
        {
            if (senderCheckBox.IsChecked == null && targetCheckBox != null)
            {
                senderOrTargetID.ItemsSource = (from parcel in approachBL.getAllParcels()
                                                where parcel.Targetld != 0
                                                select parcel.Senderld).ToList();
                senderOrTargetID.IsEnabled = true;
            }
            else if (senderCheckBox.IsChecked != null && targetCheckBox != null)
            {
                MessageBox.Show("Please check one option");
            }
        }


        private void senderOrTargetID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (senderCheckBox.IsChecked != null)
            {

            }
            else
            {

            }


        }

        private void addParcelToList_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(approachBL).ShowDialog();
        }
    }
}

