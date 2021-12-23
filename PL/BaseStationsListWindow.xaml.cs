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
    /// Interaction logic for BaseStationsListWindow.xaml
    /// </summary>
    public partial class BaseStationsListWindow : Window
    {
        BlApi.IBL approachBL;
        public BaseStationsListWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            approachBL = bl;
            BaseStationsListView.ItemsSource = approachBL.getAllStations();

            //יוצר רשימה של כל מספרי תחנות הטענה הפנויות
            IEnumerable<int> NomOfChargingStations = from findStations in approachBL.display_station_with_freeChargingStations()
                                                     select findStations.ChargeSlotsFree;
            //מסדר את המספרים לפי הסדר 
            NomOfChargingStations.ToList().Sort();

            //עובר על הרשימה ויוצר רשימה חדשה בלי כפלויות 
            List<int> listWithoutDuplicates = new List<int>();
            listWithoutDuplicates.Add(NomOfChargingStations.First());
            int index = 0;
            foreach (var WithoutDuplicates in NomOfChargingStations)
            {
                if (WithoutDuplicates != listWithoutDuplicates[index])
                {
                    listWithoutDuplicates.Add(WithoutDuplicates);
                    index++;
                }

            }
            AmountOfFreeChargingStations.ItemsSource = listWithoutDuplicates;


        }

        private void AmountOfFreeChargingStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AmountOfFreeChargingStations.SelectedItem == null)//אם לא נבחר מספר תציג את כל המספרים
            {
                AmountOfFreeChargingStations.ItemsSource = approachBL.display_station_with_freeChargingStations();
            }
            else //נבחר מספר תציג את כול התחנות שמקימות אותו 
            {

                AmountOfFreeChargingStations.ItemsSource = (from findStations in approachBL.display_station_with_freeChargingStations()
                                                            where findStations.ChargeSlotsFree == (int)AmountOfFreeChargingStations.SelectedItem
                                                            select findStations).ToList();



            }
        }

        private void BaseStationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            new BaseStationsWindow(approachBL, (StationToList)BaseStationsListView.SelectedItem).ShowDialog(); 
        }

        private void FreeChargeSlots_Click(object sender, RoutedEventArgs e)
        {

            AmountOfFreeChargingStations.ItemsSource = approachBL.display_station_with_freeChargingStations();



        }

        private void addBaseStationsToList_Click(object sender, RoutedEventArgs e)
        {
            new BaseStationsWindow(approachBL).ShowDialog();
        }
    }
}
