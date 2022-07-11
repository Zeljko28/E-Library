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
    /// Interaction logic for AddBookType.xaml
    /// </summary>
    public partial class AddBookType : UserControl
    {
        public AddBookType()
        {
            InitializeComponent();
        }

        public void AddType_Click(object sender, RoutedEventArgs e) 
        {
            string type = TypeText.Text.Trim();
            if (type.Equals("")) { MessageBox.Show("Niste upisali ime vrste dela!"); TypeText.Text = ""; }
            else 
            {
                if (SqliteDataAccess.bookTypeExist(type))
                {
                    MessageBox.Show("Ova vrsta dela već postoji!");
                }
                else
                {
                    SqliteDataAccess.AddBookType(type);
                    MessageBox.Show("Uspešno ste dodali novu vrstu dela!");
                }
                TypeText.Text = "";
            }
        }

    }
}
