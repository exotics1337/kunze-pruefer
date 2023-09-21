using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
            navItem_1.Style = (Style)FindResource("NavItem");
            navItem_2.Style = (Style)FindResource("NavItem");
            switch (selectedIndex)
            {
                case 1:
                    navItem_1.Style = (Style)FindResource("NavItemSelected");
                    break;
                case 2:
                    navItem_2.Style = (Style)FindResource("NavItemSelected");
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
