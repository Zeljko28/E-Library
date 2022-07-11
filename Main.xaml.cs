using BIBLIOTEKA.e_biblioteka;
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
using BIBLIOTEKA.Database;
using BIBLIOTEKA.MVVM.View;

namespace BIBLIOTEKA
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private void _this_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        //Student s = new Student();

        public Main(User user) // User user
        {

            GLOBALS.USERNAME = user.GetUsername();
            GLOBALS.PASSWORD = user.GetPassword();
            GLOBALS.TYPE = user.GetUserType();
            InitializeComponent();
            welcomeText.Text = "Dobrodošli, " + user.GetUsername() + "!";
            
            if (user.GetUserType().ToLower() == "student")
            {
                studentStackpanel.Visibility = Visibility.Visible;
                bibliotekarStackpanel.Visibility = Visibility.Hidden;
                administratorStackpanel.Visibility = Visibility.Hidden;
            }
            else if (user.GetUserType().ToLower() == "bibliotekar")
            {
                studentStackpanel.Visibility = Visibility.Hidden;
                bibliotekarStackpanel.Visibility = Visibility.Visible;
                administratorStackpanel.Visibility = Visibility.Hidden;
            }
            else if (user.GetUserType().ToLower() == "administrator") 
            {
                studentStackpanel.Visibility = Visibility.Hidden;
                bibliotekarStackpanel.Visibility = Visibility.Hidden;
                administratorStackpanel.Visibility = Visibility.Visible;

            }

        }



        private void HomeRadio_Checked(object sender, RoutedEventArgs e)
        {
            /*
            if (//AddProductButton != null) 
            {
                //AddProductButton.Visibility = Visibility.Visible;
            }*/

            //SignOutButton.Visibility = Visibility.Hidden;


            //LogOutRadioStudent.Visibility = Visibility.Hidden;
            //Preview.Visibility = Visibility.Visible;

        }


        private void StudentBook_Checked(object sender, RoutedEventArgs e) 
        {
            Reserve.Visibility = Visibility.Hidden;
            CancelRes.Visibility = Visibility.Visible;
            if (ConfirmRes != null)
            {
                ConfirmRes.Visibility = Visibility.Visible;
            }
        }

        private void StudentHome_Checked(object sender, RoutedEventArgs e)
        {
            Reserve.Visibility = Visibility.Visible;
            CancelRes.Visibility = Visibility.Hidden;
            if (ConfirmRes != null) {
                ConfirmRes.Visibility = Visibility.Hidden;
            }
        }

        private void ShowRented_Checked(object sender, RoutedEventArgs e)
        {
            if (Reserve != null) { Reserve.Visibility = Visibility.Hidden; }
            if (CancelRes != null) { CancelRes.Visibility = Visibility.Hidden; }
            if (ConfirmRes != null)
            {
                ConfirmRes.Visibility = Visibility.Hidden;
            }
        }


            // Admin Menu Buttons

        private void AdminShowStudents_Checked(object sender, RoutedEventArgs e) 
        {
            if (deleteStudentButton != null) 
            {
                deleteStudentButton.Visibility = Visibility.Visible;
            }
            if (deleteLibrarianButton != null)
            {
                deleteLibrarianButton.Visibility = Visibility.Hidden;
            }
            GLOBALS.SELECTED_STUDENT = -1;
        }

        private void AdminShowLibrarians_Checked(object sender, RoutedEventArgs e)
        {
            if (deleteStudentButton != null)
            {
                deleteStudentButton.Visibility = Visibility.Hidden;
            }
            if (deleteLibrarianButton != null) 
            {
                deleteLibrarianButton.Visibility = Visibility.Visible;
            }
            GLOBALS.SELECTED_LIBRARIAN = -1;
        }
        private void AdminAddLibrarian_Checked(object sender, RoutedEventArgs e) 
        {
            if (deleteStudentButton != null)
            {
                deleteStudentButton.Visibility = Visibility.Hidden;
            }
            if (deleteLibrarianButton != null)
            {
                deleteLibrarianButton.Visibility = Visibility.Hidden;
            }
        }


        // Librarian Menu Buttons

        private void LibrarianShowBooks_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ActivateStudent_Checked(object sender, RoutedEventArgs e) 
        {
        
        }

        private void AddBookRadio_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void AddBookTypeRadio_Checked(object sender, RoutedEventArgs e)
        {

        }


        /*
        private void AdminShowBooks_Checked(object sender, RoutedEventArgs e)
        {

        }*/

        private void OrderRadio_Checked(object sender, RoutedEventArgs e)
        {
            //AddProductButton.Visibility = Visibility.Hidden;
        }

        private void MaterialRadio_Checked(object sender, RoutedEventArgs e)
        {
            //AddProductButton.Visibility = Visibility.Hidden;
        }

        private void BuyRadio_Checked(object sender, RoutedEventArgs e)
        {
            //AddProductButton.Visibility = Visibility.Hidden;
        }

        private void ProfileRadio_Checked(object sender, RoutedEventArgs e)
        {
            //ConfirmButton.Visibility = Visibility.Hidden;
            //SignOutButton.Visibility = Visibility.Visible;

            Reserve.Visibility = Visibility.Hidden;
            CancelRes.Visibility = Visibility.Hidden;
            if (deleteStudentButton != null)
            {
                deleteStudentButton.Visibility = Visibility.Hidden;
            }
            if (deleteLibrarianButton != null)
            {
                deleteLibrarianButton.Visibility = Visibility.Hidden;
            }
        }

        private void LogOutRadio_Checked(object sender, RoutedEventArgs e)
        {

            GLOBALS.USERNAME = "";
            GLOBALS.PASSWORD = "";
            GLOBALS.TYPE = "";

            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
        }

        private void MakeReservation_Click(object sender, RoutedEventArgs e)
        {
            SqliteDataAccess.MakeReservation(GLOBALS.RES_ID, GLOBALS.USERNAME);
        }

        private void CancelReservation_Click(object sender, RoutedEventArgs e) 
        {
            SqliteDataAccess.CancelReservation(GLOBALS.CANCEL_RES_ID, GLOBALS.USERNAME);
        }

        private void ConfirmReservation_Click(object sender, RoutedEventArgs e)
        {
            SqliteDataAccess.ConfirmReservation(GLOBALS.CANCEL_RES_ID, GLOBALS.USERNAME);
        }

        private void DeleteStudent_Click(object sender, RoutedEventArgs e) 
        {
            SqliteDataAccess.DeleteStudent(GLOBALS.SELECTED_STUDENT);
        }

        private void DeleteLibrarian_Click(object sender, RoutedEventArgs e)
        {
            SqliteDataAccess.DeleteLibrarian(GLOBALS.SELECTED_LIBRARIAN);
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}

