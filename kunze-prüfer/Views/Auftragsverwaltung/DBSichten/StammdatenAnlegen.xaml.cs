using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AdonisUI.Converters;
using kunze_prüfer.DataBase;
using static kunze_prüfer.Models.TextBoxChecker;
using kunze_prüfer.Views.Stammdaten;
using Auftrag = kunze_prüfer.DataBase.Auftrag;
using Werkstoff = kunze_prüfer.DataBase.Werkstoff;

namespace kunze_prüfer.Views.Auftragsverwaltung.DBSichten
{
    public partial class StammdatenAnlegen : UserControl
    {
        private DBQ db = new DBQ();
        private Auftrag _auftrag;
        private Kunde _kunde = new Kunde();
        private DataBase.Werkstoff _werkstoff = new Werkstoff();
        private bool _isKundeSelected = false;
        private bool _isWerkstoffSelected = false;
        public StammdatenAnlegen()
        {
            InitializeComponent();
            _auftrag = Auftragsverwaltung.SharedResources.CurrentAuftrag;
            Auftragsverwaltung.SubmitButtonClicked += OnSubmitButtonClicked;
        }
        
        
        private async void OnSubmitButtonClicked()
        {
            if (Auftragsverwaltung.SharedResources.Step == 1)
            {
                if (CheckBoxLieferadresseWieRechnungsadresse.IsChecked == true)
            {
                TextBoxKundenRechnungsadresse.Text = TextBoxKundenAdresse.Text;
            }
            if (AreAllTextBoxesFilled(GroupBoxKundendetails) && AreAllTextBoxesFilled(GroupBoxWerkstoffDetails))
            {
                if (!_isKundeSelected)
                {
                    _kunde = new Kunde()
                    {
                        k_name = TextBoxKundenname.Text,
                        k_ust_id = TextBoxKundenUstId.Text,
                        k_lief_adresse = TextBoxKundenAdresse.Text,
                        k_rech_adresse = TextBoxKundenRechnungsadresse.Text
                    };
                }
                else
                {
                    int knr = int.Parse(TextBoxKundennr.Text);
                    _kunde = db.Set<Kunde>().FirstOrDefault(k => k.k_nr == knr);
                }
                
                if (!_isWerkstoffSelected)
                {
                    _werkstoff = new DataBase.Werkstoff()
                    {
                        w_name = TextBoxName.Text,
                        w_kennzeichen = TextBoxKennzeichnen.Text,
                        w_oberflaeche = TextBoxOberflaeche.Text,
                        w_hoehe = int.Parse(TextBoxHoehe.Text),
                        w_breite = int.Parse(TextBoxBreite.Text),
                        w_laenge = int.Parse(TextBoxLaenge.Text),
                        w_gewicht = int.Parse(TextBoxGewicht.Text),
                        w_kurz = TextBoxKurzname.Text
                    };
                }
                else
                {
                    int wnr = int.Parse(TextBoxWerkstoffnr.Text);
                    _werkstoff = db.Set<DataBase.Werkstoff>().FirstOrDefault(w => w.w_nr == wnr);
                }
                
                _auftrag.Anspr_nr = int.Parse(TextBoxAnsprechpartner.Text);
                _auftrag.k_nr = _kunde.k_nr;
                _auftrag.w_nr = _werkstoff.w_nr;
                if (!_isKundeSelected)
                {
                    db.Set<Kunde>().Add(_kunde);
                }
                if (!_isWerkstoffSelected)
                {
                    db.Set<DataBase.Werkstoff>().Add(_werkstoff);
                }
                await db.SaveChangesAsync();
                Auftragsverwaltung.SharedResources.Step = 1;
                Auftragsverwaltung.SharedResources.CurrentAuftrag = _auftrag;
                MainGrid.Background = new SolidColorBrush(Color.FromRgb(178, 255, 171));
            }
            else
            {
                AdonisUI.Controls.MessageBox.Show("Es wurden nicht alle notwendingen Felder ausgefüllt!", "Speichern nicht erfolgreich!", AdonisUI.Controls.MessageBoxButton.OK);
            }
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
                    _isKundeSelected = true;
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
                    _isWerkstoffSelected = true;
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
        private void CheckBoxLieferadresseWieRechnungsadresse_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckBoxLieferadresseWieRechnungsadresse.IsChecked == true)
            {
                StackPanelRechnungsadresse.Visibility = Visibility.Collapsed;
            }
            else if (CheckBoxLieferadresseWieRechnungsadresse.IsChecked == false)
            {
                StackPanelRechnungsadresse.Visibility = Visibility.Visible;
            }
        }

        private void TextBoxKundenname_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isKundeSelected)
            {
                var lastKunde = db.GetLastEntity<Kunde, int>(k => k.k_nr);
                if (lastKunde != null)
                {
                    int newK_nr = lastKunde.k_nr + 1;
                    TextBoxKundennr.Text = newK_nr.ToString();
                }
                else
                {
                    TextBoxKundennr.Text = "1";
                }
            }
        }

        private void TextBoxName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isWerkstoffSelected)
            {
                var lastWerkstoff = db.GetLastEntity<Werkstoff, int>(w => w.w_nr);
                if (lastWerkstoff != null)
                {
                    int newW_nr = lastWerkstoff.w_nr + 1;
                    TextBoxWerkstoffnr.Text = newW_nr.ToString();
                }
                else
                {
                    TextBoxWerkstoffnr.Text = "1";
                }
            }
        }
    }
}