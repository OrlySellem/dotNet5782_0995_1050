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
using System.Collections.ObjectModel;
using BlApi;
using BO;
namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelsListPage.xaml
    /// </summary>
    public partial class ParcelsListPage : Page
    {
        BlApi.IBL approachBL;
        private ObservableCollection<BO.ParcelToList> allParcels = new ObservableCollection<BO.ParcelToList>();
        public ParcelsListPage(BlApi.IBL bl)
        {
            InitializeComponent();
            approachBL = bl;

            ParcelListView.ItemsSource = allParcels;
            foreach (BO.ParcelToList p in approachBL.getAllParcels())
                allParcels.Add(p);

           
        }

        private void ParcelListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            if (ParcelListView.SelectedItem != null)
            {
                new ParcelWindow(approachBL, (ParcelToList)ParcelListView.SelectedItem, allParcels).ShowDialog();
            }
            ParcelListView.Items.Refresh();
        }

        private void senderCheckBox_check(object sender, RoutedEventArgs e)
        {

            senderOrTargetID.SelectedItem = null;
            if (senderCheckBox.IsChecked != false)
            {
                senderOrTargetID.ItemsSource = (from parcel in approachBL.getAllParcels()
                                                where parcel.Senderld != 0
                                                select parcel.Senderld).ToList().Distinct();
                targetCheckBox.IsChecked = false;
                senderOrTargetID.IsEnabled = true;
            }



        }

        private void TargetCheckBox_check(object sender, RoutedEventArgs e)
        {
            senderOrTargetID.SelectedItem = null;
            if (targetCheckBox.IsChecked != false)
            {
                senderOrTargetID.ItemsSource = (from parcel in approachBL.getAllParcels()
                                                where parcel.Targetld != 0
                                                select parcel.Targetld).ToList().Distinct();
                senderCheckBox.IsChecked = false;
                senderOrTargetID.IsEnabled = true;
            }

        }


        private void SenderOrTargetID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (targetCheckBox.IsChecked != false)
            {
                ParcelListView.ItemsSource = (from parcel in approachBL.getAllParcels()
                                              where parcel.Targetld == int.Parse(senderOrTargetID.SelectedItem.ToString())
                                              select parcel).ToList();

            }
            if (senderCheckBox.IsChecked != false)
            {
                ParcelListView.ItemsSource = (from parcel in approachBL.getAllParcels()
                                              where parcel.Senderld == int.Parse(senderOrTargetID.SelectedItem.ToString())
                                              select parcel).ToList();

            }




        }

        private void AddParcelToList_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(approachBL, allParcels).ShowDialog();
           
        }
    }
}

