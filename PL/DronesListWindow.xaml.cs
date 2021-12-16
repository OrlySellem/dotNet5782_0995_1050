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
using IBL;
using IBL.BO;
namespace PL
{
    /// <summary>
    /// Interaction logic for DronesListWindow.xaml
    /// </summary>
    public partial class DronesListWindow : Window
    {
        IBL.IBL dronesBL;
        public DronesListWindow(IBL.IBL bl)
        {
            InitializeComponent();
            dronesBL = bl;

            DronesListView.ItemsSource = dronesBL.GetDrones();
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
        }

        private void ChooseDroneToShow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e) {}

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          StatusSelector.ItemsSource = (DroneStatuses)DronesListView.SelectedItems;

        }

        private void addDroneToList_Click(object sender, RoutedEventArgs e)
        {       
            new DroneWindow(dronesBL).ShowDialog();
        }
    }
}
