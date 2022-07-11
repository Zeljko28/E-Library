using System;
using e_biblioteka;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTEKA.e_biblioteka
{
    class Administrator : User
    {

        //[Key]
        public int id { get; set; }
        public string korisnickoIme { get; set; }
        public string lozinka { get; set; }

        //public string aktivan { get; set; }
        public string tipKorisnika { get; set; }


        public override string GetUserType() { return tipKorisnika; }
        public override string GetUsername() { return korisnickoIme; }
        public override string GetPassword() { return lozinka; }

        public override void SetUserType(string userType) { this.tipKorisnika = userType; }
        public override void SetUsername(string username) { this.korisnickoIme = username; }
        public override void SetPassword(string password) { this.lozinka = password; }
    }
}
