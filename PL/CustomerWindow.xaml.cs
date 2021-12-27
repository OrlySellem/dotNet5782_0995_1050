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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {


        #region ADD Customer Window

        BlApi.IBL approachBL;
        public CustomerWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            approachBL = bl;
            updataGrid.Visibility = Visibility.Hidden;
            addGrid.Visibility = Visibility.Visible;

        }

        private void addCustomer_Click(object sender, RoutedEventArgs e)
        {
            Location address = new Location()
            {
                Longitude = SliderLattitude.Value,
                Lattitude = SliderLongitude.Value
            };
            Customer newCustomer = new Customer()
            {

                Id = int.Parse(TextBoxId.Text),
                Phone=TextBoxPhone.Text,
                Name = TextBoxName.Text,
                Address = address,
            };
            approachBL.addCustomer(newCustomer);
            
            this.Close();
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

        

        #endregion ADD Customer Window





        static CustomerToList TheChosenCustomer;
    public CustomerWindow(IBL bl, CustomerToList Customer)
    {
        InitializeComponent();
        approachBL = bl;
        TheChosenCustomer = Customer;
        updataGrid.Visibility = Visibility.Visible;
        addGrid.Visibility = Visibility.Hidden;

            id.Text = TheChosenCustomer.Id.ToString();
            CustomerName.Text = TheChosenCustomer.Name.ToString();
            Phone.Text = TheChosenCustomer.Phone.ToString();
            Sented_and_provided_parcels.Text = TheChosenCustomer.Num_of_sented_and_provided_parcels.ToString();
            Sented_and_unprovided_parcels.Text = TheChosenCustomer.Num_of_sented_and_unprovided_parcels.ToString();
            Received_parcels.Text = TheChosenCustomer.Num_of_received_parcels.ToString();
            Parcels_onTheWay_toCustomer.Text = TheChosenCustomer.Num_of_parcels_onTheWay_toCustomer.ToString();
    }
    private void UpdateData_Click(object sender, RoutedEventArgs e)
    {
        if ((CustomerName.Text != "" && CustomerName.Text != TheChosenCustomer.Name) || (Phone.Text != "" && Phone.Text != TheChosenCustomer.Phone))
            {
                approachBL.updateCustomer(TheChosenCustomer.Id, CustomerName.Text, Phone.Text);
                this.Close();
            }
            

    }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
     
    }

}

