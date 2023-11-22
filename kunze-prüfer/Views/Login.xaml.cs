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
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

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
            TestPdf();
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

        private void TestPdf() // code später rauslöschen
        {
            InvoiceTemplate.Adresse adresse = new InvoiceTemplate.Adresse();
            adresse.Land = "Deutschland";
            adresse.Name = "Max Mustermann";
            adresse.Ort = "Musterstadt";
            adresse.PLZ = "12345";
            adresse.Strasse = "Musterstraße";
            adresse.Hausnummer = "1";
            adresse.USTID = "DE123456789";
            
            InvoiceTemplate.AngebotModel model = new InvoiceTemplate.AngebotModel();
            model.AbsenderAdresse = adresse;
            model.KundenAdresse = adresse;
            model.AngebotNr = "ANG-123";
            model.Betreff = "Neues Angebot";

            InvoiceTemplate.Artikel artikel1 = new InvoiceTemplate.Artikel
            {
                Menge = 1,
                Name = "Artikel 1",
                Preis = 10
            };
            
            InvoiceTemplate.Artikel artikel2 = new InvoiceTemplate.Artikel
            {
                Menge = 3,
                Name = "Artikel 2",
                Preis = 24.5
            };
                        
            List<InvoiceTemplate.Artikel> artikelList = new List<InvoiceTemplate.Artikel>();
            artikelList.Add(artikel1);
            artikelList.Add(artikel2);

            model.ArtikelList = artikelList;
            
            var document = new InvoiceGenerator(model);
            document.GeneratePdf("invoice.pdf");
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