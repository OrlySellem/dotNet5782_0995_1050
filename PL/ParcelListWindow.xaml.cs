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
            ParcelListView.ItemsSource = approachBL.getAllParcels();
        
        }

        private void ParcelListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            new ParcelWindow(approachBL, (ParcelToList)ParcelListView.SelectedItem).ShowDialog();
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
            new ParcelWindow(approachBL).ShowDialog();
            ParcelListView.ItemsSource = approachBL.getAllParcels();
        }
    }
}

