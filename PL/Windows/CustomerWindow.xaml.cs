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
using System.Collections.ObjectModel;
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {

        private void moveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        #region ADD Customer Window

        BlApi.IBL approachBL;
        ObservableCollection<BO.CustomerToList> customerList;
        public CustomerWindow(BlApi.IBL bl, ObservableCollection<BO.CustomerToList> customers)
        {
            InitializeComponent();
            customerList = customers;
            approachBL = bl;
            updataGrid.Visibility = Visibility.Hidden;
            addGrid.Visibility = Visibility.Visible;

        }

        private void addCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Location address = new Location()
                {
                    Longitude = SliderLattitude.Value,
                    Lattitude = SliderLongitude.Value
                };
                Customer newCustomer = new Customer()
                {

                    Id = int.Parse(TextBoxId.Text),
                    Phone = TextBoxPhone.Text,
                    Name = TextBoxName.Text,
                    Address = address,
                };
                approachBL.addCustomer(newCustomer);
                MessageBoxResult result = MessageBox.Show("!הלקוח נוסף בהצלחה");

                CustomerToList c = (from addC in approachBL.getAllCustomers()
                                    where addC.Id == int.Parse(TextBoxId.Text)
                                    select addC).FirstOrDefault();
                customerList.Add(c);

                this.Close();
            }
            catch (AlreadyExistException)
            {

                MessageBox.Show("לקוח כבר קיים במערכת", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void TextBoxId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxId.Text != "" && TextBoxName.Text != "" && TextBoxPhone.Text != "")
            {
                addCustomer.IsEnabled = true;
            }
            else
            {
                addCustomer.IsEnabled = false;
            }
        }

        private void TextBoxPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxId.Text != "" && TextBoxName.Text != "" && TextBoxPhone.Text != "")
            {
                addCustomer.IsEnabled = true;
            }
            else
            {
                addCustomer.IsEnabled = false;
            }

        }

        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxId.Text != "" && TextBoxName.Text != "" && TextBoxPhone.Text != "")
            {
                addCustomer.IsEnabled = true;
            }
            else
            {
                addCustomer.IsEnabled = false;
            }
        }


        private void cancelAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        #endregion ADD Customer Window





        private Customer selectedCustomer;
        public CustomerWindow(IBL bl, CustomerToList customer)
        {
            InitializeComponent();
            approachBL = bl;
            selectedCustomer = approachBL.getCustomer(customer.Id);
            DataContext = selectedCustomer;
            updataGrid.DataContext = selectedCustomer;
            updataGrid.Visibility = Visibility.Visible;
            addGrid.Visibility = Visibility.Hidden;
            UpdateData.IsEnabled = true;


        }
        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((CustomerName.Text != "" && CustomerName.Text != selectedCustomer.Name) || (Phone.Text != "" && Phone.Text != selectedCustomer.Phone))
                {
                    approachBL.updateCustomer(selectedCustomer.Id, CustomerName.Text, Phone.Text);
                    UpdateData.IsEnabled = false;
                }
                MessageBoxResult result = MessageBox.Show("!הלקוח עודכן בהצלחה");

            }
            catch (DoesntExistentObjectException)
            {
                MessageBox.Show("הלקוח לא קיים במערכת", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (AlreadyExistException)
            {
                MessageBox.Show("הלקוח קיים במערכת", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }

}
