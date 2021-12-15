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
using IBL.BO;
using IBL;

namespace PL
{

    public partial class MainWindow : Window
    {
        //Button insertToDroneList;
        public MainWindow()
        {
            InitializeComponent();
            //insertToDroneList.Click += new EventHandler(ShowDronesButton_Click);
        }

        private void ShowDronesButton_Click(object sender, RoutedEventArgs e)
        {
            //new DronesListWindow(b1).Show();
        }

        IBL.IBL mainBl = new IBL.BL();

        private void insertToDroneList_Click(object sender, RoutedEventArgs e)
        {
            DronesListWindow window = new DronesListWindow(mainBl);

            window.ShowDialog();
        }

        //private void insertToDroneList_Click(object sender, RoutedEventArgs e)
        //{
        //    insertToDroneList.Content = "thank you";
        //    foo();
        //}

        //void foo()
        //{ //Drone d;
        //    try
        //    {
        //        mainBl.getDrone(200);
        //    }
        //    catch (Exception x)
        //    {
        //        MessageBox.Show(x.Message , "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //    } 
        //}
    }
}
