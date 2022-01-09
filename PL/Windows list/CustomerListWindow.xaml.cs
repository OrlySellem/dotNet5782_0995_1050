using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        IBL approachBL;
        private ObservableCollection <CustomerToList> allCustomers;

        public CustomerListWindow(IBL bl)
        {
            InitializeComponent();
            approachBL = BlFactory.GetBl();
            allCustomers = new ObservableCollection<CustomerToList>();
            foreach (BO.CustomerToList c in approachBL.getAllCustomers())
                allCustomers.Add(c);

            CustomerListView.ItemsSource = allCustomers;
        }

        private void addCustomerToList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new CustomerWindow(approachBL).ShowDialog();


                allCustomers.Clear();

                foreach (BO.CustomerToList c in approachBL.getAllCustomers())
                   allCustomers.Add(c);

                CustomerListView.ItemsSource = allCustomers;
            }
            catch (AlreadyExistException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CustomerListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                 Action updateCustomer=(() =>
                { new CustomerWindow(approachBL, (CustomerToList)CustomerListView.SelectedItem).ShowDialog();});

                if (CustomerListView.SelectedItem != null)
                {
                    updateCustomer();
                }
                    CustomerListView.ItemsSource = approachBL.getAllCustomers();                               
            }
            catch (DoesntExistentObjectException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(AlreadyExistException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

    }
}

