using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIBLIOTEKA.e_biblioteka;


namespace BIBLIOTEKA.Database
{
    public class Book
    {
        [Key]
        public int idKnjige { get; set; }
        public string ISBN { get; set; }
        public string Naziv { get; set; }
        public string Autori { get; set; }
        public string Mentori { get; set; }
        public string Editori { get; set; }
        public string datumIzdavanja { get; set; }
        public string izdavac { get; set; }
        public string naslovnaStrana { get; set; }
        public string brojDostupnih { get; set; }
        public string status { get; set; }
        public string vrsta { get; set; }


        public Book() { }

        public Book(string m_ISBN, string m_Naziv, string m_Autor, string m_Mentor, string m_Editor, string m_datum, string m_izdavac, string m_naslovna, string m_broj, string m_status, string m_vrsta)
        {
            this.ISBN = m_ISBN;
            this.Naziv = m_Naziv;
            this.Autori = m_Autor;
            this.Mentori = m_Mentor;
            this.Editori = m_Editor;
            this.datumIzdavanja = m_datum;
            this.izdavac = m_izdavac;
            this.naslovnaStrana = m_naslovna;
            this.brojDostupnih = m_broj;
            this.status = m_status;
            this.vrsta = m_vrsta;
     
        }

        public class Builder {

            private int b_idKnjige { get; set; }
            private string b_ISBN { get; set; }
            private string b_Naziv { get; set; }
            private string b_Autori { get; set; }
            private string b_Mentori { get; set; }
            private string b_Editori { get; set; }
            private string b_datumIzdavanja { get; set; }
            private string b_izdavac { get; set; }
            private string b_naslovnaStrana { get; set; }
            private string b_brojDostupnih { get; set; }
            private string b_status { get; set; }
            private string b_vrsta { get; set; }


            public Builder() { }

            public Builder setISBN(string isbn) 
            {
                this.b_ISBN = isbn;
                return this;
            }

            public Builder setNaziv(string naziv)
            {
                this.b_Naziv = naziv;
                return this;
            }

            public Builder setAutor(string autor)
            {
                this.b_Autori = autor;
                return this;
            }

            public Builder setMentor(string mentor)
            {
                this.b_Mentori = mentor;
                return this;
            }

            public Builder setEditor(string editor)
            {
                this.b_Editori = editor;
                return this;
            }

            public Builder setDatumIzdavanja(string datumIzdavanja)
            {
                this.b_datumIzdavanja = datumIzdavanja;
                return this;
            }

            public Builder setIzdavac(string izdavac)
            {
                this.b_izdavac = izdavac;
                return this;
            }

            public Builder setNaslovnaStrana(string naslovnaStrana)
            {
                this.b_naslovnaStrana = naslovnaStrana;
                return this;
            }

            public Builder setBrojDostupnih(string brojDostupnih)
            {
                this.b_brojDostupnih = brojDostupnih;
                return this;
            }

            public Builder setStatus(string status)
            {
                this.b_status = status;
                return this;
            }

            public Builder setVrsta(string vrsta)
            {
                this.b_vrsta = vrsta;
                return this;
            }

            public Book build() 
            {
                return new Book(b_ISBN, b_Naziv, b_Autori, b_Mentori, b_Editori, b_datumIzdavanja, b_izdavac, b_naslovnaStrana, b_brojDostupnih, b_status, b_vrsta);
            }

        }

    }
}
