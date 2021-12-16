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
using IBL;
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBL.IBL droneBL;

        public DroneWindow(IBL.IBL bl)
        {
            InitializeComponent();
            droneBL = bl;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            idStation.ItemsSource = droneBL.display_station_with_freeChargingStations();


        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MessageBox.Show(e.NewValue.ToString());
        }

        private void addDrone_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Drone newDrone = new Drone()
                {
                    Id = int.Parse(TextBoxId.Text),
                    Model = TextBoxModel.Text,
                    MaxWeight = (WeightCategories)WeightSelector.SelectedItem

                };
            //    int station = int.Parse(idStation.SelectedItem.ToString());

                droneBL.addDrone(newDrone,(int) idStation.SelectedItem);

            }
            catch (AlreadyExistException ex)
            {

                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (TextBoxId.Text != "" && TextBoxModel.Text != "" && WeightSelector.SelectedItem != null && idStation.SelectedItem != null)
                addDrone.IsEnabled = true;
            else
                addDrone.IsEnabled = false;

        }

        private void idStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TextBoxId.Text != "" && TextBoxModel.Text != "" && WeightSelector.SelectedItem != null && idStation.SelectedItem != null)
                addDrone.IsEnabled = true;
            else
                addDrone.IsEnabled = false;
        }

        private void cancelAddDrone_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBoxModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxId.Text!="" && TextBoxModel.Text!=""&& WeightSelector.SelectedItem!= null && idStation.SelectedItem!=null)
                addDrone.IsEnabled = true;
            else
                addDrone.IsEnabled = false;

        }

        //private void cancelAddDrone_Click(object sender, RoutedEventArgs e)
        //{
        //    cancelAddDrone.IsEnabled;
        //}

        /*     <ComboBox.ItemTemplate>
                <DataTemplate>
                    <TextBlock Text="{Binding Name}"/>
                </DataTemplate>
              </ComboBox.ItemTemplate>
            </ComboBox>
        */

    }
}
