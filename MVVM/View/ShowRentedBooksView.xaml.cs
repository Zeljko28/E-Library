using e_biblioteka;
using BIBLIOTEKA.Database;
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

namespace BIBLIOTEKA.MVVM.View
{
    /// <summary>
    /// Interaction logic for ShowRentedBooksView.xaml
    /// </summary>
    public partial class ShowRentedBooksView : UserControl
    {
        public ShowRentedBooksView()
        {
            InitializeComponent();
            List<Reservation> reservations = new List<Reservation>();
            reservations = SqliteDataAccess.LoadRentedBooks(GLOBALS.USERNAME);
            populateList(reservations);
        }

        public void populateList(List<Reservation> reservations)
        {
            listRented.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            listRented.Background = new SolidColorBrush(Color.FromRgb(54, 86, 150));
            listRented.ItemsSource = reservations;

        }

}
}
