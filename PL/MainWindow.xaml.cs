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
using BlApi;
using BO;
using BL;
using System.Collections.ObjectModel;

namespace PL
{

    public partial class MainWindow : Window
    {
        
        public BlApi.IBL mainBl =  BlApi.BlFactory.GetBl();

        //על מנת שנוכל להזיז את החלון
        private void moveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        } 

        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ViewDroneList_Click(object sender, RoutedEventArgs e)
        {
            tableLists.Content = new DronesListPage(mainBl);
            
        }

        private void ViewBaseStationList_Click(object sender, RoutedEventArgs e)
        {
            tableLists.Content = new BaseStationsListPage(mainBl);
           
        }

        private void ViewCustomerList_Click(object sender, RoutedEventArgs e)
        {
            tableLists.Content = new CustomersListPage(mainBl);
        }

        private void ViewParcelList_Click(object sender, RoutedEventArgs e)
        {
            tableLists.Content = new ParcelsListPage(mainBl);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
