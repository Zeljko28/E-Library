using System.ComponentModel.DataAnnotations;
using BIBLIOTEKA.e_biblioteka;

namespace e_biblioteka
{
    public class Librarian : User
    {

        [Key]
        public int idBibliotekara { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string tipKorisnika { get; set; }
        public string korisnickoIme { get; set; }
        public string lozinka { get; set; }

        override public string GetUserType() { return tipKorisnika; }
        public override string GetUsername() { return korisnickoIme; }
        public override string GetPassword() { return lozinka; }

        public override void SetUserType(string userType) { this.tipKorisnika = userType; }
        public override void SetUsername(string username) { this.korisnickoIme = username; }
        public override void SetPassword(string password) { this.lozinka = password; }
    }
}