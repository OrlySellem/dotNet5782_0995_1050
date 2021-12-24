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
        BlApi.IBL approachBL;
        public CustomerWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            approachBL = bl;
            updataGrid.Visibility = Visibility.Hidden;
            addGrid.Visibility = Visibility.Visible;

        }

      

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        static CustomerToList TheChosenCustomer;
        public CustomerWindow(IBL bl, CustomerToList Customer)
        {
            InitializeComponent();
            approachBL = bl;
            TheChosenCustomer = Customer;
            updataGrid.Visibility = Visibility.Visible;
            addGrid.Visibility = Visibility.Hidden;
        }
        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
            if ((Name.Text!="" && Name.Text!= TheChosenCustomer.Name)|| (Phone.Text!=""&& Phone.Text!= TheChosenCustomer.Phone))
            approachBL.updateCustomer(TheChosenCustomer.Id, Name.Text, Phone.Text);
        }

        private void addCustomer_Click(object sender, RoutedEventArgs e)
        {
            Location address = new Location()
            {
                Longitude = SliderLattitude.Value,
                Lattitude = SliderLongitude.Value
            };
            Station newStation = new Station()
            {

                Id = int.Parse(TextBoxId.Text),
                Name = int.Parse(TextBoxName.Text),
                Address = address,
                ChargeSlots = int.Parse(TextBoxChargeSlots.Text),
                Charging_drones = null
            };
            approachBL.addStation(newStation);

            this.Close();
        }
    }
}
