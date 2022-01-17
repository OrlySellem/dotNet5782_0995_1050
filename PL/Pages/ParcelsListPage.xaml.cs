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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using BlApi;
using BO;
namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelsListPage.xaml
    /// </summary>
    public partial class ParcelsListPage : Page
    {
        BlApi.IBL approachBL;
        private ObservableCollection<BO.ParcelToList> allParcels = new ObservableCollection<BO.ParcelToList>();
        public ParcelsListPage(BlApi.IBL bl)
        {
            InitializeComponent();
            approachBL = bl;

            ParcelListView.ItemsSource = allParcels;
            foreach (BO.ParcelToList p in approachBL.getAllParcels())
                allParcels.Add(p);

           
        }

        private void ParcelListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            if (ParcelListView.SelectedItem != null)
            {
                new ParcelWindow(approachBL, (ParcelToList)ParcelListView.SelectedItem, allParcels).ShowDialog();
            }
            ParcelListView.Items.Refresh();
        }


        private void AddParcelToList_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(approachBL, allParcels).ShowDialog();
           
        }

        private void groupingSender_Click(object sender, RoutedEventArgs e)
        {
            RemoveGroupings_Click(sender, e);
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Senderld");
            SortDescription sortDescription = new SortDescription("Senderld", ListSortDirection.Ascending);
            view.GroupDescriptions.Add(groupDescription);
            view.SortDescriptions.Add(sortDescription);
            groupingTarget.IsEnabled = false;
        }
        private void groupingTarget_Click(object sender, RoutedEventArgs e)
        {
            RemoveGroupings_Click(sender, e);
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Targetld");
            SortDescription sortDescription = new SortDescription("Targetld", ListSortDirection.Ascending);
            view.GroupDescriptions.Add(groupDescription);
            view.SortDescriptions.Add(sortDescription);
            groupingTarget.IsEnabled = false;

        }
        private void RemoveGroupings_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view= (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            view.GroupDescriptions.Clear();
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            allParcels.Clear();

            foreach (BO.ParcelToList p in approachBL.getAllParcels())
                allParcels.Add(p);
        }
    }
}

