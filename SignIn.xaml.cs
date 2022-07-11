using BIBLIOTEKA.e_biblioteka;
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
using System.Windows.Shapes;
using e_biblioteka;

namespace BIBLIOTEKA
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Student s = new Student();
            s.SetUserType("student");
            s.ime = nameText.Text.ToString();
            s.prezime = lastnameText.Text;
            s.korisnickoIme = usernameText.Text.ToString();
            s.lozinka = passwordText.Password;
            s.aktivan = "naCekanju";
            s.JMBG = JMBGText.Text.ToString();

            Librarian lib = new Librarian();
            lib.SetUserType("bibliotekar");
            lib.korisnickoIme = s.korisnickoIme;

            Administrator admin = new Administrator();
            admin.SetUserType("administrator");
            admin.korisnickoIme = s.korisnickoIme;



            if (s.ime.Equals("") || s.prezime.Equals("") || s.korisnickoIme.Equals("") || s.lozinka.Equals("") || s.JMBG.Equals(""))
            {
                MessageBox.Show("Niste uneli sve podatke!");
            }

            else if (SqliteDataAccess.checkIfUsernameExists(lib)) 
            {
                MessageBox.Show("Korisnik sa ovim korisnickim imenom već postoji!");
            }

            else if (SqliteDataAccess.checkIfUsernameExists(admin))
            {
                MessageBox.Show("Korisnik sa ovim korisnickim imenom već postoji!");
            }

            else if (!SqliteDataAccess.checkIfUsernameExists(s))
            {
                SqliteDataAccess.saveStudent(s);
                MainWindow window = new MainWindow();
                this.Close();
                window.Show();
            }

            else
            {
                MessageBox.Show("Student sa ovim podacima već postoji!"); //Korisnik vec postoji
            }

            /*
                usernameText.Text = "";
                passwordText.Password = "";
                nameText.Text = "";
                lastnameText.Text = "";
                JMBGText.Text = "";
            */
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
