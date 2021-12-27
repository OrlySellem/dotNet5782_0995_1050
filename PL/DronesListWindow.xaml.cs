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
    /// Interaction logic for DronesListWindow.xaml
    /// </summary>
    public partial class DronesListWindow : Window
    {
        BlApi.IBL approachBL;
        public DronesListWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            approachBL = bl;
            DronesListView.ItemsSource = approachBL.GetDrones();
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StattusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
        }
         
        private void ChooseDroneToShow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void addDroneToList_Click(object sender, RoutedEventArgs e)
        {       
            new DroneWindow(approachBL).ShowDialog();
            DronesListView.ItemsSource = approachBL.GetDrones();
        }

        private void StattusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StattusSelector.SelectedItem == null)
            {
              DronesListView.ItemsSource = approachBL.GetDrones();
            }
            else
            {
                DronesListView.ItemsSource = approachBL.GetDrones(x=>x.Status == (DroneStatuses)StattusSelector.SelectedItem);
            }
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeightSelector.SelectedItem == null)
            {
                DronesListView.ItemsSource = approachBL.GetDrones();
            }
            else
            {
                DronesListView.ItemsSource = approachBL.GetDrones(x => x.MaxWeight == (WeightCategories)WeightSelector.SelectedItem);
            }
        }

        private void DronesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            new DroneWindow(approachBL, (DroneToList)DronesListView.SelectedItem).ShowDialog();
            DronesListView.Items.Refresh();
        }
    }
}
