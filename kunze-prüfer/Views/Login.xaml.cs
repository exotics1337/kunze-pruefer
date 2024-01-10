using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using kunze_prüfer.DataBase;
using kunze_prüfer.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
//using Configuration = kunze_prüfer.Migrations.Configuration;

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
            QuestPDF.Settings.License = LicenseType.Community;
        }

        private async void CheckConnection()
        {
            ButtonLogin.IsEnabled = false;
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
                            ButtonLogin.IsEnabled = true;
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
                    User user = new User();
                    user.MitNr = mitnr;
                    user.IsAdmin = await CheckLogin.IsAdmin(mitnr);
                    user.Name = await CheckLogin.GetName(mitnr);
                    MainWindow mw = new MainWindow(user);
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


        private void TextBlockDatabaseConfig_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DatabaseConfig dbConfig = new DatabaseConfig();
            dbConfig.ShowDialog();
        }
    }
}