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
        private int selectedPage;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ViewHandler()
        {
            Style defaultStyle = this.FindResource("NavItem") as Style;
            Style selectedStyle = this.FindResource("NavItemSelected") as Style;
            navItem_1.Style = defaultStyle;
            navItem_2.Style = defaultStyle;
            navItem_3.Style = defaultStyle;
            navItem_4.Style = defaultStyle;
            navItem_5.Style = defaultStyle;
            navItem_6.Style = defaultStyle;
            navItem_7.Style = defaultStyle;
            navItem_8.Style = defaultStyle;
            navItem_9.Style = defaultStyle;
            navItem_10.Style = defaultStyle;
            navItem_11.Style = defaultStyle;
            navItem_12.Style = defaultStyle;
            navItem_13.Style = defaultStyle;
            switch (selectedPage)
            {
                case 1:
                    navItem_1.Style = selectedStyle;
                    break;
                case 2:
                    navItem_2.Style = selectedStyle;
                    break;
                case 4:
                    navItem_3.Style = selectedStyle;
                    break;
                case 5:
                    navItem_4.Style = selectedStyle;
                    break;
                case 7:
                    navItem_5.Style = selectedStyle;
                    break;
                case 8:
                    navItem_6.Style = selectedStyle;
                    break;
                case 10:
                    navItem_7.Style = selectedStyle;
                    break;
                case 11:
                    navItem_8.Style = selectedStyle;
                    break;
                case 13:
                    navItem_9.Style = selectedStyle;
                    break;
                case 14:
                    navItem_10.Style = selectedStyle;
                    break;
                case 15:
                    navItem_11.Style = selectedStyle;
                    break;
                case 16:
                    navItem_12.Style = selectedStyle;
                    break;
                case 17:
                    navItem_13.Style = selectedStyle;
                    break;
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPage = listBox.SelectedIndex;
            ViewHandler();
        }

        private void Profile_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void SignOut_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Exit_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
