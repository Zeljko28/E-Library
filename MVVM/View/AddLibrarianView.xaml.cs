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
using BIBLIOTEKA.e_biblioteka;
using e_biblioteka;

namespace BIBLIOTEKA.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddLibrarianView.xaml
    /// </summary>
    public partial class AddLibrarianView : UserControl
    {
        public AddLibrarianView()
        {
            InitializeComponent();
        }


        public void AddLibrarian_Click(object sender, RoutedEventArgs e) 
        {
            string name = nameText.Text;
            string lastname = lastnameText.Text;
            string username = usernameText.Text;
            string password = passwordText.Password;

            if (name.Trim().Equals("") || lastname.Trim().Equals("") || username.Trim().Equals("") || password.Trim().Equals(""))
            {
                MessageBox.Show("Niste popunili sva polja!");
            }

            else
            {
                Librarian lib = new Librarian();
                lib.ime = name;
                lib.prezime = lastname;
                lib.korisnickoIme = username;
                lib.lozinka = password;
                lib.tipKorisnika = "bibliotekar";

                Student s = new Student();
                s.SetUserType("student");
                s.korisnickoIme = lib.korisnickoIme;

                Administrator admin = new Administrator();
                admin.SetUserType("administrator");
                admin.korisnickoIme = lib.korisnickoIme;

                if (SqliteDataAccess.checkIfUsernameExists(s)) 
                {
                    MessageBox.Show("Korisnik sa ovim korisničkim imenom već postoji!");
                }

                else if (SqliteDataAccess.checkIfUsernameExists(admin))
                {
                    MessageBox.Show("Korisnik sa ovim korisničkim imenom već postoji!");
                }

                else if (SqliteDataAccess.checkIfUsernameExists(lib))
                {
                    MessageBox.Show("Bibliotekar sa ovim korisničkim imenom već postoji!");
                }
                else
                {
                    SqliteDataAccess.SaveLibrarian(lib);
                    MessageBox.Show("Uspešno ste dodali bibliotekara!");
                }


                nameText.Text = "";
                lastnameText.Text = "";
                passwordText.Password = "";
                usernameText.Text = "";


            }

        }
    }
}
