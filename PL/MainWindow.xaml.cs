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
            new DronesListWindow(mainBl).ShowDialog();
        }

        private void insertToBaseStations_Click(object sender, RoutedEventArgs e)
        {
            new BaseStationsListWindow(mainBl).ShowDialog();
        }

        private void ViewCustomerList_Click(object sender, RoutedEventArgs e)
        {
            new CustomerListWindow(mainBl).ShowDialog();
        }

        private void ViewParcelList_Click(object sender, RoutedEventArgs e)
        {
            new ParcelListWindow(mainBl).ShowDialog();
        }
    }
}
