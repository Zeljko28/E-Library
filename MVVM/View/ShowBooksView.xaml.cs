using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ShowBooksView.xaml
    /// </summary>
    public partial class ShowBooksView : UserControl
    {
        public Orientation Orientation { get; set; }
        string bookType = "";
        string bookSearch = "";
        string search2 = "";

        public ShowBooksView()
        {
            InitializeComponent();
            bookSearch = "";
            List<Book> books = new List<Book>();
            books = SqliteDataAccess.LoadBooks();
            populateList(books, 1111111);
            GLOBALS.vrsteDela = SqliteDataAccess.LoadBookTypes();
            bookTypesComboBox.ItemsSource = GLOBALS.vrsteDela;
            bookComboBox.ItemsSource = new List<string> { "ISBN", "Naziv", "Autori", "Mentori", "Editori", "Godina izdanja", "Izdavac"};
            
        }

        public void populateList(List<Book> books, int number)
        {
            booksListViewStack.ItemsSource = books;
            GridViewColumn column = new GridViewColumn();
            GridViewColumn column1 = new GridViewColumn();
            GridViewColumn column2 = new GridViewColumn();
            GridViewColumn column3 = new GridViewColumn();
            GridViewColumn column4 = new GridViewColumn();
            GridViewColumn column5 = new GridViewColumn();
            GridViewColumn column6 = new GridViewColumn();
            GridViewColumn column7 = new GridViewColumn();

            GridViewControl.Columns.Clear();

            column.Header = "ISBN";
            column.Width = 40;
            column.DisplayMemberBinding = new Binding("ISBN");

            column1.Header = "Naziv";
            column1.Width = 80;
            column1.DisplayMemberBinding = new Binding("Naziv");

            column2.Header = "Autori";
            column2.Width = 80;
            column2.DisplayMemberBinding = new Binding("Autori");

            column3.Header = "Izdavac";
            column3.Width = 80;
            column3.DisplayMemberBinding = new Binding("izdavac");

            column4.Header = "Datum izdavanja";
            column4.Width = 80;
            column4.DisplayMemberBinding = new Binding("datumIzdavanja");

            column5.Header = "Mentori";
            column5.Width = 60;
            column5.DisplayMemberBinding = new Binding("Mentori");

            column6.Header = "Editori";
            column6.Width = 80;
            column6.DisplayMemberBinding = new Binding("Editori");

            column7.Header = "Broj dostupnih ";
            column7.Width = 80;
            column7.DisplayMemberBinding = new Binding("brojDostupnih");

            Trace.WriteLine(number);
            bool Column7 = number % 10 == 1;
            number = number / 10;
            bool Column6 = number % 10 == 1;
            number = number / 10;
            bool Column5 = number % 10 == 1;
            number = number / 10;
            bool Column4 = number % 10 == 1;
            number = number / 10;
            bool Column3 = number % 10 == 1;
            number = number / 10;
            bool Column2 = number % 10 == 1;
            number = number / 10;
            bool Column1 = number % 10 == 1;

            Trace.WriteLine(Column1);
            Trace.WriteLine(Column2);
            Trace.WriteLine(Column3);
            Trace.WriteLine(Column4);
            Trace.WriteLine(Column5);
            Trace.WriteLine(Column6);
            Trace.WriteLine(Column7);

            if (Column1 == true)
            {
                GridViewControl.Columns.Add(column);
            }
            if (Column2 == true)
            {
                GridViewControl.Columns.Add(column1);
            }
            if (Column3 == true)
            {
                GridViewControl.Columns.Add(column2);
            }
            if (Column4 == true)
            {
                GridViewControl.Columns.Add(column3);
            }
            if (Column5 == true)
            {
                GridViewControl.Columns.Add(column4);

            }
            if (Column6 == true)
            {
                GridViewControl.Columns.Add(column5);

            }
            if (Column7 == true)
            {
                GridViewControl.Columns.Add(column6);
            }
            GridViewControl.Columns.Add(column7);


            booksListViewStack.Foreground = new SolidColorBrush(Color.FromRgb(13, 0, 115));
            booksListViewStack.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            /*
            booksListViewStack.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            booksListViewStack.Background = new SolidColorBrush(Color.FromRgb(54, 86, 150));
            booksListViewStack.ItemsSource = books;
            */

        }


        private string getStringOfTypes(int number)
        {

            List<string> types = new List<string>();
            string typesString = "";
            bool Column7 = number % 10 == 1;
            number = number / 10;
            bool Column6 = number % 10 == 1;
            number = number / 10;
            bool Column5 = number % 10 == 1;
            number = number / 10;
            bool Column4 = number % 10 == 1;
            number = number / 10;
            bool Column3 = number % 10 == 1;
            number = number / 10;
            bool Column2 = number % 10 == 1;
            number = number / 10;
            bool Column1 = number % 10 == 1;


            if (Column1 == true)
            {
                types.Add("ISBN");
            }
            if (Column2 == true)
            {
                types.Add("Naziv");

            }
            if (Column3 == true)
            {
                types.Add("Autori");

            }
            if (Column4 == true)
            {
                types.Add("izdavac");

            }
            if (Column5 == true)
            {
                types.Add("datumIzdavanja");

            }
            if (Column6 == true)
            {
                types.Add("Mentori");

            }
            if (Column7 == true)
            {
                types.Add("Editori");

            }

            bool firstElement = true;
            foreach (string element in types)
            {
                if (firstElement)
                {
                    typesString = typesString + element;
                    firstElement = false;
                }
                else
                {
                    typesString = typesString + "," + element;
                }

            }
            return typesString;
        }


        private void getSelectedItem(object sender, MouseButtonEventArgs e)
        {
            Book book = (Book)booksListViewStack.SelectedItems[0];
            ShowImage win = new ShowImage();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(GLOBALS.PATH + "\\Images\\Dela\\Dela\\" + book.naslovnaStrana);
            bitmap.EndInit();
            win.img2.Source = bitmap;
            win.Show();
        }

        public void getSelectedBook(object sender, SelectionChangedEventArgs e) {

            Book book = (Book)booksListViewStack.SelectedItems[0];
            GLOBALS.RES_ID = book.idKnjige;
        }

        private void cancelSearchButton_Click(object sender, RoutedEventArgs e)
        {
            bookSearch = "";
            bookType = "";
            search2 = "";
            searchText.Text = "";
        }


        private void Search_Click(object sender, RoutedEventArgs e) 
        {
            //GLOBALS.RES_ID = -1;
            string type = (string)bookTypesComboBox.SelectedItem;
            string column = (string)bookComboBox.SelectedItem;

            bookType = (string)bookTypesComboBox.SelectedItem;
            bookSearch = (string)bookComboBox.SelectedItem;


            if (type is null) { type = ""; bookType = ""; }
            if (type == "Izdavac") { type = "izdavac"; bookType = "izdavac"; }
            if (type == "Godina izdanja") { type = "datumIzdavanja"; bookType = "datumIzdavanja"; }
            if (column is null) { column = ""; bookSearch = ""; }
            string search = searchText.Text;
            search2 = searchText.Text;
            List<Book> books = new List<Book>();
            /*
            books = SqliteDataAccess.LoadBooks(type, column.ToLower(), search);
            populateList(books);*/
            if (bookSearch.Equals(""))
            {
                if (bookType.Equals(""))
                {
                    if (search2.Equals(""))
                    {
                        books = SqliteDataAccess.LoadBooks(type, column.ToLower(), search);
                        populateList(books, 1111111);
                    }
                    else
                    {
                        MessageBox.Show("Molimo vas prvo izaberite kategoriju pretrage");

                    }

                }
                else
                {
                    if (search2.Equals(""))
                    {
                        List<int> number = new List<int>();
                        number = SqliteDataAccess.getBookTypeNumber(bookType);
                        int numberOfType = number[0];
                        string columnsForType = getStringOfTypes(numberOfType);

                        Trace.WriteLine(columnsForType);
                        books = SqliteDataAccess.LoadBooks(type, column.ToLower(), search);
                        populateList(books, numberOfType);
                    }
                    else
                    {
                        MessageBox.Show("Molimo vas prvo izaberite kategoriju pretrage");

                    }

                }

            }
            else
            {
                if (bookType.Equals(""))
                {
                    if (search2.Equals(""))
                    {
                        books = SqliteDataAccess.LoadBooks(type, column.ToLower(), search);
                        populateList(books, 1111111);
                    }
                    else
                    {
                        books = SqliteDataAccess.LoadBooks(type, column.ToLower(), search);
                        populateList(books, 1111111);
                    }

                }
                else
                {
                    if (search2.Equals(""))
                    {
                        List<int> number = new List<int>();
                        number = SqliteDataAccess.getBookTypeNumber(bookType);
                        int numberOfType = number[0];
                        string columnsForType = getStringOfTypes(numberOfType);
                        books = SqliteDataAccess.LoadBooks(type, column.ToLower(), search);
                        populateList(books, numberOfType);
                    }
                    else
                    {
                        List<int> number = new List<int>();
                        number = SqliteDataAccess.getBookTypeNumber(bookType);
                        int numberOfType = number[0];
                        string columnsForType = getStringOfTypes(numberOfType);

                        books = SqliteDataAccess.LoadBooks(type, column.ToLower(), search);
                        populateList(books, numberOfType);
                        //knjige = SqliteDataAccess.LoadBooksWithSearchAndTypeAndTextSearch(search, bookSearch, bookType);
                    }
                }
            }
        }


    }
}
