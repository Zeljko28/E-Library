using BIBLIOTEKA.Database;
using e_biblioteka;
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
    /// Interaction logic for ShowLibrariansView.xaml
    /// </summary>
    public partial class ShowLibrariansView : UserControl
    {
        public ShowLibrariansView()
        {
            InitializeComponent();
            List<Librarian> librarians = new List<Librarian>();
            librarians = SqliteDataAccess.LoadLibrarians();
            populateList(librarians);
        }

        public void populateList(List<Librarian> librarians)
        {
            librariansListViewStack.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            librariansListViewStack.Background = new SolidColorBrush(Color.FromRgb(54, 86, 150));
            librariansListViewStack.ItemsSource = librarians;

        }

        private void getSelectedLibrarian(object sender, SelectionChangedEventArgs e)
        {
            Librarian librarian = (Librarian)librariansListViewStack.SelectedItems[0];
            GLOBALS.SELECTED_LIBRARIAN = librarian.idBibliotekara;
        }
    }
}
