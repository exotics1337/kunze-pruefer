using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;
using AdonisUI;
using kunze_prüfer.DataBase;

namespace kunze_prüfer.Views
{
    /// <summary>
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : AdonisUI.Controls.AdonisWindow
    {
        public class DbClass : KunzeDB
        {
            public bool CheckConnection()
            {
                return Database.Exists();
            }
        }
        public Login()
        {   
            InitializeComponent();
            DbClass db = new DbClass();
            MessageBox.Show(db.CheckConnection().ToString()); // returns false
        }
        
    }
}
