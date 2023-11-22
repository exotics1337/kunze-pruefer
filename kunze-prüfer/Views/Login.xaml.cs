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
using kunze_prüfer.Models;

namespace kunze_prüfer.Views
{
    /// <summary>
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : AdonisUI.Controls.AdonisWindow
    {
        public Login()
        {   
            InitializeComponent();
            CheckConnection();
        }

        private async void CheckConnection()
        {
            await Task.Run(() =>
            {
                using (var db = new KunzeDB())
                {
                    if (db.Database.Exists())
                    {
                        Dispatcher.Invoke(() =>
                        {
                            TextBlockDatabaseStatus.Text = "Datenbank Verbunden";
                            ImageDatabaseOk.Visibility = Visibility.Visible;
                        });
                    }
                    else
                    {
                        Dispatcher.Invoke(() =>
                        {
                            TextBlockDatabaseStatus.Text = "Datenbank nicht Verbunden";
                        });
                    }
                }
            });
        }

        private async void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
            if (BoxMitNr.Text != "" && PasswordBox.Password != "")
            {
                int mitnr = Convert.ToInt32(BoxMitNr.Text);
                string password = PasswordBox.Password;
                bool loginResult = await CheckLogin.ValidateLogin(mitnr, password);
                if (loginResult)
                {
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Close();
                }
                else
                {
                    AdonisUI.Controls.MessageBox.Show("Mitarbeiter Nr. und/oder Passwort existiert nicht!", "Login fehlgeschlagen!", AdonisUI.Controls.MessageBoxButton.OK);
                }
            }
            else
            {
                AdonisUI.Controls.MessageBox.Show("Bitte geben Sie Ihre Mitarbeiter Nummer und Ihr Passwort ein!",
                    "Login fehlgeschlagen!", AdonisUI.Controls.MessageBoxButton.OK);
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Visibility == Visibility.Visible)
            {
                // Passwort anzeigen
                PasswordBox.Visibility = Visibility.Collapsed;
                TextBoxPassword.Visibility = Visibility.Visible;
                TextBoxPassword.Text = PasswordBox.Password;
            }
            else
            {
                // Passwort verbergen
                PasswordBox.Visibility = Visibility.Visible;
                TextBoxPassword.Visibility = Visibility.Collapsed;
                PasswordBox.Password = TextBoxPassword.Text;
            }
        }
    }
}