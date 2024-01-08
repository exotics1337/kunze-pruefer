using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using kunze_prüfer.DataBase;
using static kunze_prüfer.Models.TextBoxChecker;
namespace kunze_prüfer.Views.Auftragsverwaltung.DBSichten
{
    public partial class Werkstoffprüfung : UserControl
    {
        private DataBase.DBQ db = new DataBase.DBQ();
        private Probe_Kopf _probeKopf;
        private Abnahmegesellschaft _abnahmegesellschaft;
        private Auftrag _auftrag;
        private bool _isAbnahmegesellschaftSelected = false;
        public ObservableCollection<Probe_Unter> ProbeUnterList = new ObservableCollection<Probe_Unter>();

        
        public Werkstoffprüfung()
        {
            InitializeComponent();
            this.DataContext = this;
            DataGridWerkstoffpruefung.ItemsSource = ProbeUnterList;
            _auftrag = Auftragsverwaltung.SharedResources.CurrentAuftrag;
            Auftragsverwaltung.SubmitButtonClicked += OnSubmitButtonClicked;
            ComboBoxFertigstellungszeit.ItemsSource = db.GetAll<Fertigstellung_Zeit>().Result.ToList();
            ComboBoxFertigstellungszeit.DisplayMemberPath = "P_fertigstellung_zeit_bez";
            ComboBoxFertigstellungszeit.SelectedValuePath = "P_fertigstellung_zeit_nr";
        }

        private async void OnSubmitButtonClicked()
        {
            if (Auftragsverwaltung.SharedResources.Step == 4)
            {
                if (
                    TextBoxNameAbnahmegesellschaft != null && TextBoxAbnahmenr != null &&
                    TextBoxPruefungsnr != null && TextBoxProbeanzahl != null &&
                    TextBoxProbenr != null && DatePickerProbeEingangsdatum.SelectedDate.HasValue &&
                    DatePickerFertigstellungsdatum.SelectedDate.HasValue && ComboBoxFertigstellungszeit.SelectedIndex != -1 &&
                    DatePickerAbnahmedatum.SelectedDate.HasValue && TextBoxChargeNr != null &&
                    TextBoxBemerkung != null
                    )
                {
                    if (!_isAbnahmegesellschaftSelected)
                    {
                        _abnahmegesellschaft = new Abnahmegesellschaft()
                        {
                            Abnhme_bez = TextBoxNameAbnahmegesellschaft.Text,
                            Abnahme_nr = int.Parse(TextBoxAbnahmenr.Text)
                        };
                    }
                    else
                    {
                        int abnahmenr = int.Parse(TextBoxAbnahmenr.Text);
                        _abnahmegesellschaft = db.Set<Abnahmegesellschaft>().FirstOrDefault(ab => ab.Abnahme_nr == abnahmenr);
                    }
                    
                    _probeKopf = new Probe_Kopf()
                    {
                        P_nr = int.Parse(TextBoxPruefungsnr.Text),
                        P_anzahl = int.Parse(TextBoxProbeanzahl.Text),
                        Prob_nr = int.Parse(TextBoxProbenr.Text),
                        P_eingang = DatePickerProbeEingangsdatum.SelectedDate.Value,
                        P_fertigstellung_dat = DatePickerFertigstellungsdatum.SelectedDate.Value,
                        P_fertigstellung_zeit_nr = int.Parse(ComboBoxFertigstellungszeit.SelectedValue.ToString()),
                        P_abnahme_dat = DatePickerAbnahmedatum.SelectedDate.Value,
                        P_charge_nr = TextBoxChargeNr.Text,
                        P_bemerkung = TextBoxBemerkung.Text,
                        Abnahme_nr = _abnahmegesellschaft.Abnahme_nr,
                    };

                    if (_isAbnahmegesellschaftSelected)
                    {
                        db.Set<Abnahmegesellschaft>().Add(_abnahmegesellschaft);
                    }
                    db.Set<Probe_Kopf>().Add(_probeKopf);

                    foreach(Probe_Unter pb in ProbeUnterList)
                    {
                        pb.P_nr = _probeKopf.P_nr;
                        db.Set<Probe_Unter>().Add(pb);
                    }

                    db.SaveChangesAsync();
                    Auftragsverwaltung.SharedResources.Step = 5;
                    Auftragsverwaltung.SharedResources.CurrentProbeUnterList = ProbeUnterList;
                    AdonisUI.Controls.MessageBox.Show("Werkstoffprüfung wurde erfolgreich erstellt!", "Speichern erfolgreich!", AdonisUI.Controls.MessageBoxButton.OK);
                    MainGrid.Background = new SolidColorBrush(Color.FromRgb(178, 255, 171));
                }
                else
                {
                    AdonisUI.Controls.MessageBox.Show("Es wurden nicht alle notwendingen Felder ausgefüllt!", "Speichern nicht erfolgreich!", AdonisUI.Controls.MessageBoxButton.OK);
                }
            }
        }

        private async void ButtonAbnahmegesellschaftAuswaehlen_OnClick(object sender, RoutedEventArgs e)
        {
            SearchWindow sw = new SearchWindow(TextBoxSucheAbnahmegesellschaft.Text, "Abnahmegesellschaft");
            if (int.TryParse(TextBoxSucheAbnahmegesellschaft.Text, out int result))
            {
                // Suche mit ID implementieren
            }
            else if (TextBoxSucheAbnahmegesellschaft.Text != "")
            {
                await sw.SearchWithType(TextBoxSucheAbnahmegesellschaft.Text);
            }
            sw.ShowDialog();
            if (sw.CurrentlySelectedID != 0)
            {
                var abnahmegesellschaft = await db.GetEntityByIdAsync<DataBase.Abnahmegesellschaft, int>(sw.CurrentlySelectedID);
                if (abnahmegesellschaft != null)
                {
                    _isAbnahmegesellschaftSelected = true;
                    TextBoxAbnahmenr.Text = abnahmegesellschaft.Abnahme_nr.ToString();
                    TextBoxNameAbnahmegesellschaft.Text = abnahmegesellschaft.Abnhme_bez;
                }
            }
        }

        private void TextBlockSelectionClearAbnahme_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _isAbnahmegesellschaftSelected = false;
            TextBoxAbnahmenr.Text = "";
            TextBoxNameAbnahmegesellschaft.Text = "";
        }

        private void TextBoxNameAbnahmegesellschaft_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isAbnahmegesellschaftSelected)
            {
                var lastAbnahme = db.GetLastEntity<Abnahmegesellschaft, int>(ab => ab.Abnahme_nr);
                if (lastAbnahme != null)
                {
                    int newW_nr = lastAbnahme.Abnahme_nr + 1;
                    TextBoxAbnahmenr.Text = newW_nr.ToString();
                }
                else
                {
                    TextBoxAbnahmenr.Text = "1";
                }
            }
        }
    }
}