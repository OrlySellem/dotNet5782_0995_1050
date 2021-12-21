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

        private void ShowDronesButton_Click(object sender, RoutedEventArgs e)
        {
            //new DronesListWindow(b1).Show();
        }

        private void insertToDroneList_Click(object sender, RoutedEventArgs e)
        {
            new DronesListWindow(mainBl).ShowDialog();
        }

       
    }
}
