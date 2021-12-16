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
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

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
                    Id = TextBoxId.Text,
                    Model = TextBoxModel.Text,


                };

                droneBL.addDrone(newDrone, (int)idStation.Text);
                
            }
            catch (AlreadyExistException ex)
            {

                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void cancelAddDrone_Click(object sender, RoutedEventArgs e)
        {
            cancelAddDrone.IsEnabled
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
