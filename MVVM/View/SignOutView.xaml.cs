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

namespace BIBLIOTEKA.MVVM.View
{
    /// <summary>
    /// Interaction logic for SignOutView.xaml
    /// </summary>
    public partial class SignOutView : UserControl
    {
        public SignOutView()
        {
            InitializeComponent();
        }

        public void SignOut(string username) {
            // nadji u bazi sa tim username-om i stavi aktivan=0

            /*
            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
            */

        }

    }
}
