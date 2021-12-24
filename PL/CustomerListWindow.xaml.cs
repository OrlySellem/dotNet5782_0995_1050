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
    /// Interaction logic for CustomerListWindow.xaml
    /// </summary>
    public partial class CustomerListWindow : Window
    {
        BlApi.IBL approachBL;
        public CustomerListWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            approachBL = bl;
            CustomerListView.ItemsSource = approachBL.getAllCustomers();

        }

      

        private void addCustomerToList_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(approachBL).ShowDialog();
        }

        private void CustomerListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            new CustomerWindow(approachBL, (CustomerToList)CustomerListView.SelectedItem).ShowDialog();
        }
    }
}
