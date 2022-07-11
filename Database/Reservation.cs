using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTEKA.Database
{
    public class Reservation
    {

        public int idStudenta { get; set;}
        public int idKnjige { get; set; }
        public string datumUzeto { get; set; }
        public string datumVraceno { get; set; }
        public string rezervacijaUsatima { get; set; }
        public string naziv { get; set; }
    }
}
