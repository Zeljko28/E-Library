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
using BIBLIOTEKA.Database;

namespace BIBLIOTEKA.MVVM.View
{
    /// <summary>
    /// Interaction logic for ShowStudentBook.xaml
    /// </summary>
    public partial class ShowStudentBook : UserControl
    {
        public ShowStudentBook()
        {
            InitializeComponent();

            List<Reservation> reservations = new List<Reservation>();
            reservations = SqliteDataAccess.LoadReservation(GLOBALS.USERNAME);
            populateList(reservations);
        }

        public void populateList(List<Reservation> reservations)
        {
            listReservations.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            listReservations.Background = new SolidColorBrush(Color.FromRgb(54, 86, 150));
            listReservations.ItemsSource = reservations;

        }

        public void getSelectedRes(object sender, SelectionChangedEventArgs e)
        {
            Reservation res = (Reservation)listReservations.SelectedItems[0];
            GLOBALS.CANCEL_RES_ID = res.idKnjige;
        }

    }
}
