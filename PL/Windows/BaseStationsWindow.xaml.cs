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
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStationsWindow.xaml
    /// </summary>
    /// 


    public partial class BaseStationsWindow : Window
    {
        private void moveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        #region ADD BaseStations
        BlApi.IBL approachBL;
        ObservableCollection<BO.StationToList> BaseStationsList;

        public BaseStationsWindow(BlApi.IBL bl, ObservableCollection<BO.StationToList> BaseStations)
        {
            InitializeComponent();
            BaseStationsList = BaseStations;
            approachBL = bl;
            updataGrid.Visibility = Visibility.Hidden;
            addGrid.Visibility = Visibility.Visible;

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
             
                StationToList s = (from addS in approachBL.getAllStations()
                                    where addS.Id == int.Parse(TextBoxId.Text)
                                    select addS).FirstOrDefault();
                BaseStationsList.Add(s);

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
        private Station selectedBaseStation = new Station();
        private StationToList original = new StationToList();
        public BaseStationsWindow(IBL bl, StationToList BaseStation, ObservableCollection<BO.StationToList> stations)
        {
            try
            {
                InitializeComponent();
                approachBL = bl;
                selectedBaseStation = approachBL.getStation(BaseStation.Id);
                DataContext = selectedBaseStation;               
                updataGrid.Visibility = Visibility.Visible;
                addGrid.Visibility = Visibility.Hidden;
                updataGrid.DataContext = selectedBaseStation;
                original = BaseStation;
                BaseStationsList = stations;
                UpdateData.IsEnabled = true;
            }
            catch (DoesntExistentObjectException)
            {
                MessageBox.Show("התחנה לא קיימת במערכת", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result;
                StationToList s;
                //in case the user change the name of base station
                if (StationName.Text != "" && original.Name != StationName.Text)
                {
                    if (AddChargeSlots.Text == "")
                    {
                        approachBL.updateStation(selectedBaseStation.Id, StationName.Text.ToString(), 0);
                        result = MessageBox.Show("!תחנת הבסיס עודכנה בהצלחה");
                    }
                       
                    else
                    {
                        approachBL.updateStation(selectedBaseStation.Id, StationName.Text.ToString(), int.Parse(AddChargeSlots.Text));
                        result = MessageBox.Show("!תחנת הבסיס עודכנה בהצלחה");
                    }
                        

                }
                else if (AddChargeSlots.Text != "")
                {
                    approachBL.updateStation(selectedBaseStation.Id, "", int.Parse(AddChargeSlots.Text));
                     result = MessageBox.Show("!תחנת הבסיס עודכנה בהצלחה");
                }
                else
                {
                    result = MessageBox.Show("לא התבצע כלל שינוי");
                }

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
                approachBL.deleteFromStations(selectedBaseStation.Id);
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