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
using BL;
namespace PL
{

    public partial class MainWindow : Window
    {
        
        public BlApi.IBL mainBl =  BlApi.BlFactory.GetBl();

        //Button insertToDroneList;
        public MainWindow()
        {
            InitializeComponent();         
        }

        private void ViewDroneList_Click(object sender, RoutedEventArgs e)
        {
            new DronesListPage(mainBl).Visibility = Visibility.Visible;
        }

        private void insertToBaseStations_Click(object sender, RoutedEventArgs e)
        {
            new CustomersListPage(mainBl).Visibility = Visibility.Visible;
            DronesListView.Visibility = Visibility.Hidden;
            BaseStationsListView.Visibility = Visibility.Visible;

            //new BaseStationsListWindow(mainBl).ShowDialog();
        }

        private void ViewCustomerList_Click(object sender, RoutedEventArgs e)
        {
            new CustomersListPage(mainBl).Visibility = Visibility.Visible;
        }

        private void ViewParcelList_Click(object sender, RoutedEventArgs e)
        {
            new ParcelsListPage(mainBl).Visibility = Visibility.Visible;
        }

        private void sign_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(mainBl).ShowDialog();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                CustomerToList c = (from customer in mainBl.getAllCustomers()
                                    where IDTextBox.Text == customer.Id.ToString() && passwordUserName.Password == customer.Password
                                    select customer).FirstOrDefault();

                passwordUserName.Visibility = Visibility.Hidden;
                ID.Visibility = Visibility.Hidden;
                Password.Visibility = Visibility.Hidden;
                IDTextBox.Visibility = Visibility.Hidden;
                signUp.Visibility = Visibility.Hidden;
                LogIn.Visibility = Visibility.Hidden;
                MainGrid.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DronesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BaseStationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CustomerListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
