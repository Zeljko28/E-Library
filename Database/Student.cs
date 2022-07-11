using BIBLIOTEKA.e_biblioteka;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_biblioteka
{
    public class Student : User
    {
        [Key]
        public int idStudenta { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string korisnickoIme { get; set; }
        public string lozinka { get; set; }
        public string aktivan { get; set; }
        public string tipKorisnika { get; set; }
        public string JMBG { get; set; }

        
        public override string GetUserType() { return tipKorisnika; }
        public override string GetUsername() { return korisnickoIme; }
        public override string GetPassword() { return lozinka; }

        public override void SetUserType(string userType) { this.tipKorisnika = userType; }
        public override void SetUsername(string username) { this.korisnickoIme = username; }
        public override void SetPassword(string password) { this.lozinka = password; }

    }
}
