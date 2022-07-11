using e_biblioteka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTEKA.e_biblioteka
{
    public class UserFactory 
    {

        public User CreateUser(string userType) 
        {
            userType = userType.ToLower();
            if (userType == "student") { return new Student(); }
            else if (userType == "bibliotekar") { return new Librarian(); }
            else if (userType == "administrator") { return new Administrator(); }
            else { return null; }
        }

    }
}
