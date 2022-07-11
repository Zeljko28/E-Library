using System;
using BIBLIOTEKA.e_biblioteka;
using BIBLIOTEKA.Database;
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

namespace BIBLIOTEKA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }

        //Sign in button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SignIn window = new SignIn();
            this.Close();
            window.Show();
            //Dugme za izlazak
            this.Close();
        }


        //LogIn button 
        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {

            UserFactory factory = new UserFactory();
            User student = factory.CreateUser("student");
            User librarian = factory.CreateUser("bibliotekar");
            User admin = factory.CreateUser("administrator");

            student.SetUsername(usernameText.Text);
            student.SetPassword(passwordText.Password.ToString());
            student.SetUserType("student");

            librarian.SetUsername(usernameText.Text);
            librarian.SetPassword(passwordText.Password.ToString());
            librarian.SetUserType("bibliotekar");

            admin.SetUsername(usernameText.Text);
            admin.SetPassword(passwordText.Password.ToString());
            admin.SetUserType("administrator");


            GLOBALS.REG_NOT_CONFIRMED = false;

            if (SqliteDataAccess.checkIfExist(student))
            {
                GrantAcces(student);
            }
            else if (SqliteDataAccess.checkIfExist(librarian))
            {
                GrantAcces(librarian);
            }
            else if (SqliteDataAccess.checkIfExist(admin)) 
            {
                GrantAcces(admin);
            }
            else
            {
                if (!GLOBALS.REG_NOT_CONFIRMED)
                {
                    MessageBox.Show("Netačno korisničko ime ili lozinka! Molimo pokušajte ponovo.");
                }
            }


            
        }

        private void GrantAcces(User user)
        {
            Main window = new Main(user);
            this.Close();
            window.Show();
        }

            private void lblRegistration_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
                SignIn window = new SignIn();
                this.Close();
                window.Show();
            }

            private void Button_Click_2(object sender, RoutedEventArgs e)
            {

            }

            private void Button_Click_Cancel(object sender, RoutedEventArgs e)
            {
                this.Close();
            }
        }
    }

