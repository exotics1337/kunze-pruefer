using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using kunze_prüfer.DataBase;
using kunze_prüfer.Views.Stammdaten;
using Auftrag = kunze_prüfer.DataBase.Auftrag;

namespace kunze_prüfer.Views.Auftragsverwaltung.DBSichten
{
    public partial class StammdatenAnlegen : UserControl
    {
        private DBQ db = new DBQ();
        private Auftrag _auftrag;
        private Kunde _kunde;
        private DataBase.Werkstoff _werkstoff;
        private bool _isKundeSelected = false;
        private bool _isWerkstoffSelected = false;
        public StammdatenAnlegen()
        {
            InitializeComponent();
            _auftrag = Auftragsverwaltung.SharedResources.CurrentAuftrag;
            Auftragsverwaltung.SubmitButtonClicked += OnSubmitButtonClicked;
        }
        
        
        private void OnSubmitButtonClicked()
        {
            if (AreAllTextBoxesFilled(GroupBoxKundendetails))
            {
                // Submit the form
            }
            else
            {
                AdonisUI.Controls.MessageBox.Show("Es wurden nicht alle notwendingen Felder ausgefüllt!", "Speichern nicht erfolgreich!", AdonisUI.Controls.MessageBoxButton.OK);
            }
        }

        private async void ButtonKundeAuswählen_OnClick(object sender, RoutedEventArgs e)
        {
            SearchWindow sw = new SearchWindow(TextBoxSuchen.Text, "Kunde");
            if (int.TryParse(TextBoxSuchen.Text, out int result))
            {
                // Suche mit ID implementieren
            }
            else if (TextBoxSuchen.Text != "")
            {
                await sw.SearchWithType(TextBoxSuchen.Text);
            }
            sw.ShowDialog();
            if (sw.CurrentlySelectedID != 0)
            {
                var kunden = await db.GetEntityByIdAsync<DataBase.Kunde, int>(sw.CurrentlySelectedID);
                if (kunden != null)
                {
                    TextBlockSelectedKunde.Text = kunden.k_name;
                    TextBoxKundennr.Text = kunden.k_nr.ToString();
                    TextBoxKundenname.Text = kunden.k_name;
                    TextBoxKundenUstId.Text = kunden.k_ust_id;
                    TextBoxKundenAdresse.Text = kunden.k_lief_adresse;
                    TextBoxKundenRechnungsadresse.Text = kunden.k_rech_adresse;
                    if (TextBoxKundenAdresse.Text == TextBoxKundenRechnungsadresse.Text)
                    {
                        CheckBoxLieferadresseWieRechnungsadresse.IsChecked = true;
                    }
                    else
                    {
                        CheckBoxLieferadresseWieRechnungsadresse.IsChecked = false;
                    }
                    var ansprechpartnerList = db.Set<Kunden_Ansprechpartner>()
                        .Include(k => k.Ansprechpartner)
                        .Where(a => a.K_nr == kunden.k_nr)
                        .ToList();
                    DataGridAnsprechpartner.ItemsSource = ansprechpartnerList;
                    _isKundeSelected = true;
                }
            }
        }

        private async void TextBoxKundennr_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(TextBoxKundennr.Text, out int result))
            {
                // Search for Ansprechpartner with the same K_nr
            }
        }

        private void CheckBoxLieferadresseWieRechnungsadresse_OnUnchecked(object sender, RoutedEventArgs e)
        {
            TextBoxKundenRechnungsadresse.Visibility = Visibility.Visible;
        }

        private void CheckBoxLieferadresseWieRechnungsadresse_OnChecked(object sender, RoutedEventArgs e)
        {
            //TextBoxKundenRechnungsadresse.Visibility = Visibility.Collapsed;
        }
        
        private bool AreAllTextBoxesFilled(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
                {
                    return false;
                }
                else if (child is DependencyObject && !AreAllTextBoxesFilled(child))
                {
                    return false;
                }
            }
            return true;
        }

        private async void ButtonWerkstoffAuswählen_OnClick(object sender, RoutedEventArgs e)
        {
            SearchWindow sw = new SearchWindow(TextBoxSuchen.Text, "Werkstoff");
            if (int.TryParse(TextBoxSuchen.Text, out int result))
            {
                // Suche mit ID implementieren
            }
            else if (TextBoxSuchen.Text != "")
            {
                await sw.SearchWithType(TextBoxSuchenWerkstoff.Text);
            }
            sw.ShowDialog();
            if (sw.CurrentlySelectedID != 0)
            {
                var werkstoff = await db.GetEntityByIdAsync<DataBase.Werkstoff, int>(sw.CurrentlySelectedID);
                if (werkstoff != null)
                {
                    TextBlockSelectedWerkstoff.Text = werkstoff.w_name;
                    TextBoxWerkstoffnr.Text = werkstoff.w_nr.ToString();
                    TextBoxName.Text = werkstoff.w_name;
                    TextBoxKennzeichnen.Text = werkstoff.w_kennzeichen;
                    TextBoxOberflaeche.Text = werkstoff.w_oberflaeche;
                    TextBoxHoehe.Text = werkstoff.w_hoehe.ToString();
                    TextBoxBreite.Text = werkstoff.w_breite.ToString();
                    TextBoxLaenge.Text = werkstoff.w_laenge.ToString();
                    TextBoxGewicht.Text = werkstoff.w_gewicht.ToString();
                    TextBoxKurzname.Text = werkstoff.w_kurz;
                    _isWerkstoffSelected = true;
                }
            }
        }

        private void TextBlockSelectionClearKunde_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _isKundeSelected = false;
            TextBoxKundennr.Text = "";
            TextBoxKundenname.Text = "";
            TextBoxKundenUstId.Text = "";
            TextBoxKundenAdresse.Text = "";
            TextBoxKundenRechnungsadresse.Text = "";
            CheckBoxLieferadresseWieRechnungsadresse.IsChecked = false;
            DataGridAnsprechpartner.ItemsSource = null;
            TextBlockSelectedKunde.Text = "Keine Auswahl";
        }

        private void TextBlockSelectionClearWerkstoff_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _isWerkstoffSelected = false;
            TextBlockSelectedWerkstoff.Text = "";
            TextBoxWerkstoffnr.Text = "";
            TextBoxName.Text = "";
            TextBoxKennzeichnen.Text = "";
            TextBoxOberflaeche.Text = "";
            TextBoxHoehe.Text = "";
            TextBoxBreite.Text = "";
            TextBoxLaenge.Text = "";
            TextBoxGewicht.Text = "";
            TextBoxKurzname.Text = "";
            TextBlockSelectedWerkstoff.Text = "Keine Auswahl";
        }
    }
}