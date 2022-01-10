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
using System.Collections.ObjectModel;
using BlApi;
using BO;
namespace PL
{
    /// <summary>
    /// Interaction logic for CustomersListPage.xaml
    /// </summary>
    public partial class CustomersListPage : Page
    {
        IBL approachBL;
        private ObservableCollection <BO.CustomerToList> allCustomers = new ObservableCollection<BO.CustomerToList>();

        public CustomersListPage(IBL bl)
        {
            InitializeComponent();
            approachBL = bl;

            foreach (BO.CustomerToList c in approachBL.getAllCustomers())
                allCustomers.Add(c);

            CustomerListView.ItemsSource = allCustomers;
        }
        private void addCustomerToList_Click(object sender, RoutedEventArgs e)
        {
                new CustomerWindow(approachBL).ShowDialog();

                //allCustomers.Clear();

                //foreach (BO.CustomerToList c in approachBL.getAllCustomers())
                //    allCustomers.Add(c);

                CustomerListView.ItemsSource = allCustomers;
        }

        private void CustomerListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                Action updateCustomer = (() =>
                { new CustomerWindow(approachBL, (CustomerToList)CustomerListView.SelectedItem).ShowDialog(); });

                if (CustomerListView.SelectedItem != null)
                {
                    updateCustomer();
                }
                CustomerListView.ItemsSource = approachBL.getAllCustomers();
        }
    }
}
