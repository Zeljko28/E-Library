using Dapper;
using System;
using System.Collections.Generic;
using e_biblioteka;
using System.Data;
using System.Windows;
using BIBLIOTEKA.Database;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using BIBLIOTEKA.e_biblioteka;
using System.Windows.Controls;

namespace BIBLIOTEKA.Database
{
    public class SqliteDataAccess
    {



        public static List<Librarian> LoadLibrarians()
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                string query = " select * from Bibliotekar"; // WHERE korisnickoIme like " + username  ; 
                var output = cnn.Query<Librarian>(query, new DynamicParameters());

                cnn.Close();

                return output.ToList();
            }
        }

        
        public static List<Student> LoadStudents(string username)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                string query = " select * from Student"; 
                var output = cnn.Query<Student>(query , new DynamicParameters());

                cnn.Close();

                return output.ToList();
            }
        }


        public static List<Student> LoadPendingStudents() 
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string aktivan = '"' + "naCekanju" + '"';
                cnn.Open();
                string query = " select * from Student WHERE aktivan = " + aktivan; 
                var output = cnn.Query<Student>(query, new DynamicParameters());

                cnn.Close();

                return output.ToList();
            }
        }

        public static List<Student> LoadActiveStudents()
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string aktivan = '"' + "da" + '"';
                cnn.Open();
                string query = " select * from Student WHERE aktivan = " + aktivan;
                var output = cnn.Query<Student>(query, new DynamicParameters());

                cnn.Close();

                return output.ToList();
            }
        }

        public static List<Reservation> LoadReservation(string username) //insert default value
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                username = '"' + username + '"';

                cnn.Open();
                string query = " select idStudenta from Student WHERE korisnickoIme = " + username;
                var output = cnn.Query<Student>(query, new DynamicParameters());
                int ids = output.ToList()[0].idStudenta;

                string table = '"' + "Student-Knjiga" + '"';
                query = " select s.*, d.Naziv from " + table + " as s, Dela as d WHERE idStudenta = " + ids + " AND datumVraceno is null AND s.idKnjige = d.idKnjige";
                var outputRes = cnn.Query<Reservation>(query, new DynamicParameters());

                cnn.Close();

                for (int i = 0; i < outputRes.ToList().Count; i++) {
                    outputRes.ToList()[i].rezervacijaUsatima = DateTime.Parse(outputRes.ToList()[i].datumUzeto).AddDays(1).Subtract(DateTime.Now).ToString();
                    outputRes.ToList()[i].rezervacijaUsatima = outputRes.ToList()[i].rezervacijaUsatima.Substring(0, outputRes.ToList()[i].rezervacijaUsatima.Length - 8);
                    // ako je nula obrisi | promeni nula u veci broj za proveru
                    if (int.Parse(outputRes.ToList()[i].rezervacijaUsatima.Substring(0, 2)) <= 0)  {
                        Reservation res2 = outputRes.ToList()[i];
                        cnn.Execute("DELETE FROM " + table + " WHERE idKnjige = @idKnjige AND idStudenta = @idStudenta", res2);
                        outputRes.ToList().RemoveAt(i);
                    }
                }

                return outputRes.ToList();
            }
        }

        public static List<Reservation> LoadRentedBooks(string username)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                username = '"' + username + '"';

                cnn.Open();
                string query = " select idStudenta from Student WHERE korisnickoIme = " + username;
                var output = cnn.Query<Student>(query, new DynamicParameters());
                int ids = output.ToList()[0].idStudenta;

                string table = '"' + "Student-Knjiga" + '"';
                query = " select s.*, d.Naziv from " + table + " as s, Dela as d WHERE idStudenta = " + ids + " AND rezervacijaUsatima is null AND s.idKnjige = d.idKnjige";
                var outputRes = cnn.Query<Reservation>(query, new DynamicParameters());

                cnn.Close();

                return outputRes.ToList();
            }
        }

        public static List<Book> LoadBooks(string type = "", string column = "", string search = "", int id = -1)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                
                if (id != -1)
                {
                    string query = " select * from Dela WHERE idKnjige = " + id;
                    var output = cnn.Query<Book>(query, new DynamicParameters());

                    GLOBALS.BOOKS = output.ToList();

                    return output.ToList();
                }
                

                else if(type != "" && search == "" && column == "")
                {
                    type = '"' + type + '"';
                    string query = "SELECT * FROM Dela WHERE vrstaDela = " + type.ToLower(); 
                    var output = cnn.Query<Book>(query, new DynamicParameters());

                    GLOBALS.BOOKS = output.ToList();
                    return output.ToList();
                }

                else if (type != "" && search != "" && column != "")
                {
                    type = '"' + type + '"';
                    column = '"' + column + '"';
                    search = '"' + search + '"';
                    string query = "SELECT * FROM Dela WHERE vrstaDela = " + type + " AND " + column + " LIKE " + search; //Mozda ovde LIke imesto =
                    var output = cnn.Query<Book>(query, new DynamicParameters());

                    GLOBALS.BOOKS = output.ToList();
                    return output.ToList();
                }

                else if (type == "" && search != "" && column != "")
                {
                    column = '"' + column + '"';
                    search = '"' + search + '"';
                    string query = "SELECT * FROM Dela WHERE " + column + " LIKE " + search;
                    var output = cnn.Query<Book>(query, new DynamicParameters());

                    GLOBALS.BOOKS = output.ToList();
                    return output.ToList();
                }

                else
                {
                    List<Book> books = new List<Book>();
                    return books.ToList();
                }

            }
        }

        public static void saveStudent(User student) //Student student?
        {

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                cnn.Execute(" insert into Student (Ime, Prezime, korisnickoIme, Lozinka, aktivan, tipKorisnika, JMBG)  values (@ime ,@prezime, @korisnickoIme, @lozinka, @aktivan, @tipKorisnika, @JMBG)  ", student);
                cnn.Close();
            }
        }


        public static void SaveLibrarian(Librarian lib)
        {

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                cnn.Execute(" insert into Bibliotekar (Ime, Prezime, tipKorisnika, korisnickoIme, lozinka)  values (@ime ,@prezime, @tipKorisnika, @korisnickoIme, @lozinka)  ", lib);
                cnn.Close();
            }
            
        }

        public static bool checkIfExist(User user)
        {

            string userType = user.GetUserType();
            userType = userType.ToLower();
            userType = userType.Substring(0, 1).ToUpper() + userType.Substring(1, userType.Length - 1);

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                cnn.Open();
                string query = "SELECT * FROM " + userType + " WHERE korisnickoIme = @username AND lozinka = @password";
                SQLiteCommand cmd = new SQLiteCommand(query, cnn);
                cmd.Parameters.AddWithValue("@username", user.GetUsername());
                cmd.Parameters.AddWithValue("@password", user.GetPassword());
                SQLiteDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    if (userType.ToLower().Equals("student"))
                    {
                        if (rdr.Read())
                        {
                            if (rdr["aktivan"].Equals("ne") || rdr["aktivan"].Equals("naCekanju"))
                            {
                                rdr.Close();
                                cnn.Close();
                                MessageBox.Show("Vaša registracija još nije potvrđena");
                                GLOBALS.REG_NOT_CONFIRMED = true;
                                return false;
                            }

                            else
                            {
                                rdr.Close();
                                cnn.Close();
                                return true;
                            }
                        }
                        else 
                        {
                            rdr.Close();
                            cnn.Close();
                            return false;
                        }
                    }
                    else
                    {
                        rdr.Close();
                        cnn.Close();
                        return true;
                    }
                }
                else
                {
                    rdr.Close();
                    cnn.Close();
                    return false;
                }
            }
        }


        public static bool checkIfUsernameExists(User user)
        {
            string userType = user.GetUserType();
            userType = userType.ToLower();
            userType = userType.Substring(0, 1).ToUpper() + userType.Substring(1, userType.Length - 1);

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                cnn.Open();
                string query = "SELECT * FROM " + userType + " WHERE korisnickoIme = @username";
                SQLiteCommand cmd = new SQLiteCommand(query, cnn);
                cmd.Parameters.AddWithValue("@username", user.GetUsername());
                SQLiteDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    rdr.Close();
                    cnn.Close();
                    return true;
                }

                else 
                {
                    rdr.Close();
                    cnn.Close();
                    return false;
                }
            }
        }

        public static bool checkIfExistsBook(Book book)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                cnn.Open();
                string query = "SELECT * FROM Dela WHERE Naziv = @naziv";
                SQLiteCommand cmd = new SQLiteCommand(query, cnn);
                cmd.Parameters.AddWithValue("@naziv", book.Naziv);
                SQLiteDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    rdr.Close();
                    cnn.Close();
                    return true;
                }

                else
                {
                    rdr.Close();
                    cnn.Close();
                    return false;
                }
            }
        }

        public static bool bookTypeExist(string type)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                cnn.Open();
                string query = "SELECT * FROM VrstaDela WHERE vrsta = @naziv";
                SQLiteCommand cmd = new SQLiteCommand(query, cnn);
                cmd.Parameters.AddWithValue("@naziv", type);
                SQLiteDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    rdr.Close();
                    cnn.Close();
                    return true;
                }

                else
                {
                    rdr.Close();
                    cnn.Close();
                    return false;
                }
            }
        }

        public static List<int> getBookTypeNumber(string naziv)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                naziv = '"' + naziv + '"';
                string query = " select polja from VrstaDela where vrsta = " + naziv;
                var output = cnn.Query<int>(query, new DynamicParameters());
                cnn.Close();
                return output.ToList();
            }
        }


        public static void updateUsersPassword(User user) 
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                if (user.GetUserType().ToLower() == "student")
                {
                    cnn.Execute("update Student set lozinka = @lozinka  where korisnickoIme = @korisnickoIme ", user);
                }
                else if (user.GetUserType().ToLower() == "bibliotekar")
                {
                    cnn.Execute("update Bibliotekar set lozinka = @lozinka  where korisnickoIme = @korisnickoIme ", user);
                }
                else 
                {
                    cnn.Execute("update Administrator set lozinka = @lozinka  where korisnickoIme = @korisnickoIme ", user);
                }
                cnn.Close();
            }
        }

        public static void MakeReservation(int bookID, string username)
        {
            if (bookID != -1)
            {
                using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string usr = '"' + username + '"';
                    cnn.Open();
                    string query = " select idStudenta from Student WHERE korisnickoIme = " + usr;
                    var output = cnn.Query<Student>(query, new DynamicParameters());

                    int ids = output.ToList()[0].idStudenta;


                    Reservation res = new Reservation();
                    res.idStudenta = ids;
                    res.idKnjige = bookID;
                    string table = '"' + "Student-Knjiga" + '"';

                    query = "SELECT * FROM " + table + " WHERE idKnjige = @idKnjige AND idStudenta = @idStudenta";
                    SQLiteCommand cmd = new SQLiteCommand(query, cnn);
                    cmd.Parameters.AddWithValue("@idKnjige", res.idKnjige);
                    cmd.Parameters.AddWithValue("@idStudenta", res.idStudenta);
                    SQLiteDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        rdr.Close();
                        cnn.Close();
                        MessageBox.Show("Već ste rezervisali ili uzeli ovu knjigu!");
                    }

                    else
                    {
                        rdr.Close();
                        DateTime time1 = DateTime.Now;
                        res.datumUzeto = time1.ToString();
                        DateTime time2 = time1.AddDays(30);
                        DateTime expire = time1.AddDays(1);
                        res.datumVraceno = time2.ToString();
                        res.rezervacijaUsatima = expire.Subtract(time1).ToString();


                        cnn.Execute(" insert into " + table + " (idStudenta, idKnjige, datumUzeto, rezervacijaUsatima)  values (@idStudenta ,@idKnjige, @datumUzeto, @rezervacijaUsatima)  ", res);
                        cnn.Close();
                        MessageBox.Show("Uspešno ste rezervisali knjigu!");
                    }
                    GLOBALS.RES_ID = -1;
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali rezervaciju!");
            }

        }
        public static void CancelReservation(int bookID, string username)
        {
            if (bookID != -1)
            {
                using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string usr = '"' + username + '"';
                    cnn.Open();
                    string query = " select idStudenta from Student WHERE korisnickoIme = " + usr;
                    var output = cnn.Query<Student>(query, new DynamicParameters());

                    int ids = output.ToList()[0].idStudenta;

                    Reservation res = new Reservation();
                    res.idKnjige = bookID;
                    res.idStudenta = ids;

                    string table = '"' + "Student-Knjiga" + '"';
                    cnn.Execute(" DELETE FROM " + table + " WHERE idKnjige=@idKnjige AND idStudenta=@idStudenta", res);
                    cnn.Close();
                    GLOBALS.CANCEL_RES_ID = -1;
                    MessageBox.Show("Uspešno ste otkazali rezervaciju!");
                    
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali rezervaciju!");
            }
        }


        public static void ConfirmReservation(int bookID, string username)
        {
            if (bookID != -1)
            {
                using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    Reservation res = new Reservation();
                    res.datumUzeto = DateTime.Now.ToString();
                    res.datumVraceno = DateTime.Parse(res.datumUzeto).AddDays(30).ToString();
                    res.idKnjige = bookID;

                    string usr = '"' + username + '"';
                    cnn.Open();
                    string query = " select idStudenta from Student WHERE korisnickoIme = " + usr;
                    var output = cnn.Query<Student>(query, new DynamicParameters());

                    int ids = output.ToList()[0].idStudenta;
                    res.idStudenta = ids;

                    // Status Knjige Update
                    query = " select brojDostupnih from Dela WHERE idKnjige = " + bookID;
                    var output2 = cnn.Query<Book>(query, new DynamicParameters());
                    Book book = new Book();
                    book.brojDostupnih = output2.ToList()[0].brojDostupnih;
                    int broj = int.Parse(book.brojDostupnih);
                    broj = broj - 1;
                    book.idKnjige = bookID;
                    if (broj > 0 || broj == 0)
                    {
                        book.brojDostupnih = broj.ToString();
                        cnn.Execute("UPDATE Dela set brojDostupnih = @brojDostupnih WHERE idKnjige = @idKnjige", book);

                        string table = '"' + "Student-Knjiga" + '"';
                        cnn.Execute("update " + table + " set datumUzeto = @datumUzeto, datumVraceno = @datumVraceno, rezervacijaUsatima = NULL where idKnjige = @idKnjige AND idStudenta=@idStudenta", res);
                        MessageBox.Show("Uspešno ste potvrdili rezervaciju!");
                    }
                    else
                    {
                        broj = 0;
                        book.brojDostupnih = broj.ToString();
                        book.status = "nedostupno";
                        cnn.Execute("UPDATE Dela set brojDostupnih = @brojDostupnih, status = @status WHERE idKnjige = @idKnjige", book);
                        MessageBox.Show("Potvrda rezervacije neuspešna! Nijedan primerak ove knjige se trenutno ne nalazi u biblioteci.");
                    }

                    cnn.Close();
                    GLOBALS.CANCEL_RES_ID = -1;
                }
            }
            else 
            {
                MessageBox.Show("Niste odabrali rezervaciju!");
            }
        }

        public static void DeleteStudent(int id)
        {
            if (id != -1)
            {
                using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    Student s = new Student();
                    s.idStudenta = id;
                    cnn.Open();
                    cnn.Execute(" DELETE FROM Student WHERE idStudenta=@idStudenta", s);
                    cnn.Close();
                    GLOBALS.SELECTED_STUDENT = -1;
                    MessageBox.Show("Uspešno ste obrisali studenta!");
                }
            }
            else 
            {
                MessageBox.Show("Niste selektovali studenta!");
            }
        }


        public static void DeleteLibrarian(int id)
        {
            if (id != -1)
            {
                using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    Librarian lib = new Librarian();
                    lib.idBibliotekara = id;
                    cnn.Open();
                    cnn.Execute(" DELETE FROM Bibliotekar WHERE idBibliotekara=@idBibliotekara", lib);
                    cnn.Close();
                    GLOBALS.SELECTED_LIBRARIAN = -1;
                    MessageBox.Show("Uspešno ste obrisali bibliotekara!");
                }
            }
            else 
            {
                MessageBox.Show("Niste selektovali bibliotekara!");
            }
        }

        public static void ChangeStudentStatusConfirm(Student student)
        {

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                cnn.Execute("update Student set aktivan = 'da'  where korisnickoIme = @korisnickoIme ", student);
                cnn.Close();
            }
        }

        public static void ChangeStudentStatusDecline(Student student)
        {

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                cnn.Execute("update Student set aktivan = 'ne'  where korisnickoIme = @korisnickoIme ", student);
                cnn.Close();
            }
        }


        public static List<string> LoadBookTypes()
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                string query = " select vrsta from VrstaDela ";
                var output = cnn.Query<string>(query, new DynamicParameters());
                cnn.Close();
                return output.ToList();
            }
        }

        public static void AddBook(Book book) 
        {

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                cnn.Execute(" insert into Dela (ISBN, Naziv, Autori, Mentori, Editori, datumIzdavanja, izdavac, naslovnaStrana, brojDostupnih, status, vrstaDela)  values (@ISBN ,@Naziv, @Autori, @Mentori, @Editori, @datumIzdavanja, @izdavac, @naslovnaStrana, @brojDostupnih, @status, @vrsta)  ", book);
                cnn.Close();
            }
        }

        public static void AddBookType(string type)
        {

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                type = '"' + type + '"';
                cnn.Open();
                cnn.Execute(" insert into VrstaDela (vrsta) VALUES (" + type + ")");
                cnn.Close();
            }
        }



        private static string LoadConnectionString()
        {
            //hardkodovana je putaza za bazu to bi trebalo da se promeni
            string connectionString = @" Data Source =" + GLOBALS.PATH + "\\Database\\e-biblioteka.db; Version = 3";
            return connectionString;
        }
    }
}
