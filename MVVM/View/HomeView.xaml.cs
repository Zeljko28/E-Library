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
using e_biblioteka;

namespace BIBLIOTEKA.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public Orientation Orientation { get; set; }
        public HomeView()
        {
            InitializeComponent();
            
            List<Student> items = new List<Student>();
            items = SqliteDataAccess.LoadStudents(GLOBALS.USERNAME);
            populateList(items);
            
        }

        public void populateList(List<Student> items)
        {
            listViewStack.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            listViewStack.Background = new SolidColorBrush(Color.FromRgb(54, 86, 150));
            listViewStack.ItemsSource = items;

        }


        // funkcija za uzimanje id reda koji je kliknut


        private void getSelectedStudent(object sender, SelectionChangedEventArgs e)
        {
            Student student = (Student)listViewStack.SelectedItems[0];
            GLOBALS.SELECTED_STUDENT = student.idStudenta;
        }
    }
}
