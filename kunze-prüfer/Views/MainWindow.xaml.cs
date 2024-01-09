using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AdonisUI.Controls;
using kunze_prüfer.Models;
using kunze_prüfer.Views;
using kunze_prüfer.Views.Auftragsverwaltung;
using kunze_prüfer.Views.QuickPDF;
using MessageBox = AdonisUI.Controls.MessageBox;
using MessageBoxImage = AdonisUI.Controls.MessageBoxImage;
using MessageBoxResult = AdonisUI.Controls.MessageBoxResult;

namespace kunze_prüfer
{
    using Views.Stammdaten;

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisUI.Controls.AdonisWindow, INotifyPropertyChanged
    {
        public static User CurrentUser { get; set; }
        private int selectedPage;
        public MainWindow(User user = null)
        {
            InitializeComponent();
            DataContext = this;
            selectedPage = 0;
            ViewHandler();
            CurrentUser = user;
            if (user != null) ImageProfile.ToolTip = user.Name;
            
            ChangeView += (view) =>
            {
                CurrentView = view;
            };
        }

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged(nameof(CurrentView));
                }
            }
        }
        
        public static event Action<UserControl> ChangeView;

        public static void OnChangeView(UserControl newView)
        {
            ChangeView?.Invoke(newView);
        }


        // Event für die Property-Änderung
        public event PropertyChangedEventHandler PropertyChanged;

        // Methode zum Feuern des PropertyChanged-Events
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            switch (selectedPage)
            {
                case 0:
                    NavItem1.Style = selectedStyle;
                    CurrentView = new Dashboard();
                    break;
                case 2:
                    NavItem2.Style = selectedStyle;
                    CurrentView = new Auftragsverwaltung();
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
                    CurrentView = new Stammdaten();
                    break;
                case 10:
                    NavItem8.Style = selectedStyle;
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
            AdonisUI.Controls.MessageBox.Show("" +
                                              "Sie sind eingeloggt als: " + CurrentUser.Name + "\n" +
                                              "Admin: " + CurrentUser.IsAdmin + "\n" +
                                              "", "Login-Information", AdonisUI.Controls.MessageBoxButton.OK);
        }

        private void SignOut_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // close this window, clear the CurrentUser and open the LoginWindow
            Login Login = new Login();
            Login.Show();
            this.Close();
            CurrentUser = null;
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

        private void NavItem8_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            InvoiceDataSource invoiceDataInstance = new InvoiceDataSource();
            
            invoiceDataInstance.AddBaseElement( "Test", 1.0, 1.0);
            invoiceDataInstance.AddBaseElement( "Test2", 13.0, 193.0);
            invoiceDataInstance.AddBaseElement( "Test3", 10.0, 1194.0);
            
            PDFCreator pdfCreator = new PDFCreator(invoiceDataInstance);
            pdfCreator.Show();
        }
    }
}
