﻿using System;
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

        public DronesListPage(BlApi.IBL bl)
        {
            InitializeComponent();
            approachBL = bl;
            DronesListView.ItemsSource = approachBL.GetDrones();
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StattusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            
        }

        private void addDroneToList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new DroneWindow(approachBL).ShowDialog();
                DronesListView.ItemsSource = approachBL.GetDrones();
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
            new DroneWindow(approachBL, (DroneToList)DronesListView.SelectedItem).ShowDialog();
            DronesListView.Items.Refresh();
        }
    }
}
