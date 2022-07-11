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
    /// Interaction logic for ManageStudentView.xaml
    /// </summary>
    public partial class ManageStudentView : UserControl
    {
        Student student = new Student();
        public ManageStudentView()
        {
            InitializeComponent();
            List<Student> students = new List<Student>();
            students = SqliteDataAccess.LoadStudents(GLOBALS.USERNAME); // LoadPendingStudents() samo koji su aktivan="na cekanju"

            student.korisnickoIme = "";
            PopulateList(students);
        }

        public void PopulateList(List<Student> students)
        {
            ListViewControl.Foreground = new SolidColorBrush(Color.FromRgb(13, 0, 115));
            ListViewControl.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            ListViewControl.ItemsSource = students;
        }

        private void ActivateButton_Click(object sender, RoutedEventArgs e)
        {
            if (student.korisnickoIme.Equals(""))
            {
                MessageBox.Show("Molimo vas prvo izaberite studenta");
            }
            else
            {
                SqliteDataAccess.ChangeStudentStatusConfirm(student);
                MessageBox.Show("Status studenta postavljen na aktivan");
            }
        }

        private void BlockButton_Click(object sender, RoutedEventArgs e)
        {
            if (student.korisnickoIme.Equals(""))
            {
                MessageBox.Show("Molimo vas prvo izaberite studenta");
            }
            else
            {
                SqliteDataAccess.ChangeStudentStatusDecline(student);
                MessageBox.Show("Status studenta postavljen na blokiran");
            }
        }

        private void ListViewControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Student students = (Student)ListViewControl.SelectedItems[0];

            student.korisnickoIme = students.korisnickoIme;
            student.lozinka = students.lozinka;
            student.ime = students.ime;
            student.prezime = students.prezime;

        }
    }
}
