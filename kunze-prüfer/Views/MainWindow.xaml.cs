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
using AdonisUI;

namespace kunze_prüfer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisUI.Controls.AdonisWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ViewHandler(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 1:
                    navItem_1.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0, 0));
                    break;
                case 2:
                    navItem_2.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0, 0));
                    break;
            }
        }

        private void navItem_1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewHandler(1);
        }

        private void navItem_2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewHandler(2);
        }
    }
}
