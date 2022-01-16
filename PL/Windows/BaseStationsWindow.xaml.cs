using BlApi;
using BO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStationsWindow.xaml
    /// </summary>
    /// 


    public partial class BaseStationsWindow : Window
    {
        #region ADD BaseStations
        BlApi.IBL approachBL;
        public BaseStationsWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            approachBL = bl;
            updataGrid.Visibility = Visibility.Hidden;
            addGrid.Visibility = Visibility.Visible;
            
        }

        private void moveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void cancelAddBaseStation_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addBaseStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Location address = new Location()
                {
                    Longitude = SliderLattitude.Value,
                    Lattitude = SliderLongitude.Value
                };
                Station newStation = new Station()
                {

                    Id = int.Parse(TextBoxId.Text),
                    Name = TextBoxName.Text,
                    Address = address,
                    ChargeSlots = int.Parse(TextBoxChargeSlots.Text),
                    Charging_drones = null
                };
                approachBL.addStation(newStation);
                MessageBoxResult result = MessageBox.Show("!תחנת הבסיס נוספה בהצלחה");
                this.Close();
            }
            catch (AlreadyExistException)
            {
                MessageBox.Show("התחנה קיימת במערכת", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void TextBoxId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxId.Text != "" && TextBoxName.Text != "" && TextBoxChargeSlots.Text != "")
                addBaseStation.IsEnabled = true;
            else
                addBaseStation.IsEnabled = false;
        }

        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxId.Text != "" && TextBoxName.Text != "" && TextBoxChargeSlots.Text != "")
                addBaseStation.IsEnabled = true;
            else
                addBaseStation.IsEnabled = false;
        }

        private void TextBoxChargeSlots_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxId.Text != "" && TextBoxName.Text != "" && TextBoxChargeSlots.Text != "")
                addBaseStation.IsEnabled = true;
            else
                addBaseStation.IsEnabled = false;
        }


        #endregion ADD BaseStations


        #region UPDATE BaseStations
        static StationToList TheChosenBaseStation;

        public BaseStationsWindow(IBL bl, StationToList BaseStation)
        {
            InitializeComponent();
            DataContext = BaseStation;
            approachBL = bl;         
            TheChosenBaseStation = BaseStation;
            updataGrid.Visibility = Visibility.Visible;
            addGrid.Visibility = Visibility.Hidden;
            updataGrid.DataContext = BaseStation;
            UpdateData.IsEnabled = true;
      
        }

            private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //in case the user change the name of base station
                if (StationName.Text != "" && TheChosenBaseStation.Name != StationName.Text)
                {
                    if (AddChargeSlots.Text == "")
                        approachBL.updateStation(TheChosenBaseStation.Id,StationName.Text.ToString(),0);
                    else
                        approachBL.updateStation(TheChosenBaseStation.Id, StationName.Text.ToString(), int.Parse(AddChargeSlots.Text));

                }
                else if (AddChargeSlots.Text != "")
                {
                    approachBL.updateStation(TheChosenBaseStation.Id, "", int.Parse(AddChargeSlots.Text));
                }

                MessageBoxResult result = MessageBox.Show("!תחנת הבסיס עודכנה בהצלחה");
            }

            catch (DoesntExistentObjectException) 
            { 
                MessageBox.Show("התחנה לא קיימת במערכת", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); 
            }
            catch (AlreadyExistException)
            {
                MessageBox.Show("התחנה קיימת במערכת", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UpdateData.IsEnabled = false;
        }


        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BaseStationToDelete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                approachBL.deleteFromStations(TheChosenBaseStation.Id);
                MessageBoxResult result = MessageBox.Show("!תחנת הבסיס נמחקה בהצלחה");
            }
            catch (DoesntExistentObjectException)
            {
                MessageBox.Show("התחנה לא קיימת במערכת", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion UPDATE BaseStations

       
    }
}
