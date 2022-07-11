using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIBLIOTEKA.Database;

namespace BIBLIOTEKA
{
    class GLOBALS
    {
        
        public static string USERNAME { get; set; }
        public static string TYPE { get; set; }
        public static string PASSWORD { get; set; }
        public static List<Book> BOOKS { get; set; }

        public static int RES_ID { get; set; }

        public static int CANCEL_RES_ID { get; set; }
        public static int SELECTED_STUDENT { get; set; }
        public static int SELECTED_LIBRARIAN { get; set; }

        public static string PATH = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().IndexOf("\\bin"));

        public static bool REG_NOT_CONFIRMED = false;

        public static List<string> vrsteDela;
        public static string FRONT_PAGE_NAME { get; set; }
    }
}
