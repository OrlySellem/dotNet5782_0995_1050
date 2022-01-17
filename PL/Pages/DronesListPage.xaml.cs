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
    /// Interaction logic for DronesListPage.xaml
    /// </summary>
    public partial class DronesListPage : Page
    {
        BlApi.IBL approachBL;
        private ObservableCollection<BO.DroneToList> allDrones = new ObservableCollection<BO.DroneToList>();

        public DronesListPage(BlApi.IBL bl)
        {
            InitializeComponent();
            approachBL = bl;
            DronesListView.ItemsSource = allDrones;

            foreach (BO.DroneToList d in approachBL.GetDrones())
                allDrones.Add(d);

            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StattusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            

        }

        private void addDroneToList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new DroneWindow(approachBL,allDrones).ShowDialog();
              ;
            }
            catch (AlreadyExistException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void StattusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (StattusSelector.SelectedItem == null)
                {
                    DronesListView.ItemsSource = approachBL.GetDrones();
                }
                else if (WeightSelector.SelectedItem!=null)
                {
                    DronesListView.ItemsSource = approachBL.GetDrones(x => x.Status == (DroneStatuses)StattusSelector.SelectedItem && x.MaxWeight == (WeightCategories)WeightSelector.SelectedItem);
                }
                else
                {
                    DronesListView.ItemsSource = approachBL.GetDrones(x => x.Status == (DroneStatuses)StattusSelector.SelectedItem);
                }
            }
            catch (DoesntExistentObjectException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (WeightSelector.SelectedItem == null)
                {
                    DronesListView.ItemsSource = approachBL.GetDrones();
                }
                else if(StattusSelector.SelectedItem!=null)
                {
                    DronesListView.ItemsSource = approachBL.GetDrones(x => x.MaxWeight == (WeightCategories)WeightSelector.SelectedItem && x.Status == (DroneStatuses)StattusSelector.SelectedItem);
                }
                else
                {
                    DronesListView.ItemsSource = approachBL.GetDrones(x => x.MaxWeight == (WeightCategories)WeightSelector.SelectedItem);
                }
            }
            catch (DoesntExistentObjectException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void DronesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DronesListView.SelectedItem != null)
            {
                new DroneWindow(approachBL, (DroneToList)DronesListView.SelectedItem).ShowDialog();
                DronesListView.SelectedItem = null;
            }

            DronesListView.ItemsSource = approachBL.GetDrones();
            DronesListView.Items.Refresh();
        }


        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            StattusSelector.SelectedItem = null;
            WeightSelector.SelectedItem = null;
            DronesListView.ItemsSource = allDrones;
        }
    }
}

