using Microsoft.Win32;
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
    /// Interaction logic for AddBookView.xaml
    /// </summary>
    public partial class AddBookView : UserControl
    {
        public AddBookView()
        {
            InitializeComponent();
            GLOBALS.vrsteDela = SqliteDataAccess.LoadBookTypes();
            typeComboBox.ItemsSource = GLOBALS.vrsteDela;

        }

        public void AddBook_Click(object sender, RoutedEventArgs e)
        {
            string type = typeComboBox.Text;
            string isbn = null;
            if (ISBNText.Text.Trim() != "") { isbn = ISBNText.Text; }
            string naziv = NazivText.Text;
            string autor = AutorText.Text;
            string mentor = null;
            if (MentorText != null) { mentor = MentorText.Text; }
            string datum = datumIzdavanjaText.Text;
            string izdavac = IzdavacText.Text;
            string naslovna = "";
            if (GLOBALS.FRONT_PAGE_NAME != null) { naslovna = GLOBALS.FRONT_PAGE_NAME; }
            string broj = brojDostupnihText.Text;
            string status;
            if (broj != "")
            {
                if (int.Parse(broj) > 0)
                {
                    status = "dostupno";
                }

                else
                {
                    status = "nedostupno";
                }
            }
            else 
            {
                status = null;
            }

            if (type.Trim().Equals("") || naziv.Trim().Equals("") || autor.Trim().Equals("") || datum.Trim().Equals("") || izdavac.Trim().Equals("") || naslovna.Trim().Equals("") || broj.Trim().Equals(""))
            {
                MessageBox.Show("Niste popunili sva potrebna polja!");
            }
            else 
            {
                Book book;
                if (type.Trim().Equals("udžbenik") || type.Trim().Equals("zbirka") || type.Trim().Equals("praktikum") || type.Trim().Equals("monografija"))
                {
                    book = new Book.Builder().setISBN(isbn).setNaziv(naziv).setAutor(autor).setDatumIzdavanja(datum).setIzdavac(izdavac).setNaslovnaStrana(naslovna).setBrojDostupnih(broj).setStatus(status).setVrsta(type).build();
                }
                else 
                {
                    book = new Book.Builder().setISBN(isbn).setNaziv(naziv).setAutor(autor).setMentor(mentor).setDatumIzdavanja(datum).setIzdavac(izdavac).setNaslovnaStrana(naslovna).setBrojDostupnih(broj).setStatus(status).setVrsta(type).build();
                }

                if (SqliteDataAccess.checkIfExistsBook(book))
                {
                    MessageBox.Show("Ova knjiga već postoji!");
                }
                else 
                {
                    SqliteDataAccess.AddBook(book);
                    MessageBox.Show("Uspešno ste dodali knjigu!");
                }
            }
            /*
            NazivText.Text = "";
            AutorText.Text = "";
            datumIzdavanjaText.Text = "";
            brojDostupnihText.Text = "";
            IzdavacText.Text = "";
            ISBNText.Text = "";
            if (MentorText != null) { MentorText.Text = ""; }*/
        }

        public void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Odaberite naslovnu stranu";
            op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (op.ShowDialog() == true)
            {
                string [] splt = op.FileName.Split("\\"); 
                GLOBALS.FRONT_PAGE_NAME = splt[splt.Length - 1];
                MessageBox.Show("Uspešno ste učitali naslovnu stranu: " + GLOBALS.FRONT_PAGE_NAME);
            }
        }

        public void comboBoxSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            string type = (string)typeComboBox.SelectedItem; // vraca prethodno selektovanu vrednost
            if (type.Equals("udžbenik") || type.Equals("zbirka") || type.Equals("praktikum") || type.Equals("monografija"))
            {
                if (MentorText != null)
                {
                    MentorText.Visibility = Visibility.Hidden;
                    panel.Margin = new Thickness(0, -25, 0, 0);
                    borderMentor.Visibility = Visibility.Hidden;
                }
            }
            else 
            {
                MentorText.Visibility = Visibility.Visible;
                panel.Margin = new Thickness(0, 20, 0, 0);
                borderMentor.Visibility = Visibility.Visible;
            }

        }

    }
}
