using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AdonisUI;
using AdonisUI.Controls;
using kunze_prüfer.Models;
using kunze_prüfer.Views.QuickPDF;
using MessageBox = AdonisUI.Controls.MessageBox;
using MessageBoxImage = AdonisUI.Controls.MessageBoxImage;
using MessageBoxResult = AdonisUI.Controls.MessageBoxResult;

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
            NavItem1.Style = defaultStyle;
            NavItem2.Style = defaultStyle;
            NavItem3.Style = defaultStyle;
            NavItem4.Style = defaultStyle;
            NavItem5.Style = defaultStyle;
            NavItem6.Style = defaultStyle;
            NavItem7.Style = defaultStyle;
            NavItem8.Style = defaultStyle;
            NavItem9.Style = defaultStyle;
            NavItem10.Style = defaultStyle;
            NavItem11.Style = defaultStyle;
            NavItem12.Style = defaultStyle;
            switch (selectedPage)
            {
                case 0:
                    NavItem1.Style = selectedStyle;
                    break;
                case 2:
                    NavItem2.Style = selectedStyle;
                    break;
                case 3:
                    NavItem3.Style = selectedStyle;
                    break;
                case 5:
                    NavItem4.Style = selectedStyle;
                    break;
                case 6:
                    NavItem5.Style = selectedStyle;
                    break;
                case 7:
                    NavItem6.Style = selectedStyle;
                    break;
                case 9:
                    NavItem7.Style = selectedStyle;
                    break;
                case 10:
                    NavItem8.Style = selectedStyle;
                    break;
                case 11:
                    NavItem9.Style = selectedStyle;
                    break;
                case 12:
                    NavItem10.Style = selectedStyle;
                    break;
                case 13:
                    NavItem11.Style = selectedStyle;
                    break;
                case 14:
                    NavItem12.Style = selectedStyle;
                    break;
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPage = ListBox.SelectedIndex;
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
            var messageBox = new MessageBoxModel
            {
                Text = "Möchten Sie das Programm wirklich beenden?",
                Caption = "Programm beenden?",
                Buttons = MessageBoxButtons.YesNo("Ja", "Nein"),
                Icon = MessageBoxImage.Exclamation,
            };

            AdonisUI.Controls.MessageBoxResult result = AdonisUI.Controls.MessageBox.Show(messageBox);
            
            if (result == AdonisUI.Controls.MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void NavItem14_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            InvoiceDataSource quickInstance = new InvoiceDataSource();
            
            quickInstance.AddBaseElement( "Test", 1.0, 1.0);
            quickInstance.AddBaseElement( "Test2", 13.0, 193.0);
            quickInstance.AddBaseElement( "Test3", 10.0, 1194.0);
            
            PDFCreator pdfCreator = new PDFCreator(quickInstance);
            pdfCreator.Show();
        }
    }
}
