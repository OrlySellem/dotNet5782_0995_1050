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
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {



        #region ADD Parcel

        BlApi.IBL approachBL;
        public ParcelWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            approachBL = bl;
            updataGrid.Visibility = Visibility.Hidden;
            addGrid.Visibility = Visibility.Visible;

        }
        #endregion ADD Parcel

        #region UPDATA Parcel

        static ParcelToList TheChosenParcel;
        public ParcelWindow(IBL bl, ParcelToList parcel)
        {
            InitializeComponent();
            approachBL = bl;
            TheChosenParcel = parcel;
            updataGrid.Visibility = Visibility.Visible;
            addGrid.Visibility = Visibility.Hidden;

            id.Text = TheChosenParcel.Id.ToString();
            Name.Text = TheChosenParcel.Senderld.ToString();
            FreeChargeSlots.Text = TheChosenParcel.Targetld.ToString();
            FullChargeSlots.Text = TheChosenParcel.Weight.ToString();
            FullChargeSlots.Text = TheChosenParcel.Priority.ToString();
            FullChargeSlots.Text = TheChosenParcel.ParcelStatus.ToString();

        }
        #endregion UPDATA Parcel

        private void cancelAddBaseStation_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
