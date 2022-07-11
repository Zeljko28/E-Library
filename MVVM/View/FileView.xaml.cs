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
using e_biblioteka;

namespace BIBLIOTEKA.MVVM.View
{
    /// <summary>
    /// Interaction logic for FileView.xaml
    /// </summary>
    public partial class FileView : UserControl
    {
        Student student = new Student();

        public FileView()
        {
            InitializeComponent();

            List<Student> students = new List<Student>();
            students = SqliteDataAccess.LoadActiveStudents();

            student.korisnickoIme = "";
            PopulateList(students);
        }

        public void PopulateList(List<Student> students)
        {
            ListViewControl.Foreground = new SolidColorBrush(Color.FromRgb(13, 0, 115));
            ListViewControl.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            ListViewControl.ItemsSource = students;
        }

        public void PopulateListFiles(List<Reservation> studentKnjiga)
        {
            ListViewControlFile.Foreground = new SolidColorBrush(Color.FromRgb(13, 0, 115));
            ListViewControlFile.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            ListViewControlFile.ItemsSource = studentKnjiga;
        }

        private void ListViewControl_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            Student students = (Student)ListViewControl.SelectedItems[0];

            student.korisnickoIme = students.korisnickoIme;
            student.lozinka = students.lozinka;
            student.ime = students.ime;
            student.prezime = students.prezime;

        }



        private void ShowFile_Click(object sender, RoutedEventArgs e)
        {
            if (student.korisnickoIme.Equals(""))
            {
                MessageBox.Show("Molimo vas prvo izaberite studenta!");
            }
            else
            {
                List<Reservation> studentKnjigaLista = new List<Reservation>();
                studentKnjigaLista = SqliteDataAccess.LoadRentedBooks(student.korisnickoIme);  // || LOad reservation

                PopulateListFiles(studentKnjigaLista);
            }
        }
    }
}
