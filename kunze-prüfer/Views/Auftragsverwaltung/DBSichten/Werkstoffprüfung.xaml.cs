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
            _auftrag = Auftragsverwaltung.SharedResources.CurrentAuftrag;
            Auftragsverwaltung.SubmitButtonClicked += OnSubmitButtonClicked;
            Auftragsverwaltung.CurrentAuftragChanged += OnCurrentAuftragChanged;
            DataGridWerkstoffpruefung.ItemsSource = ProbeUnterList;
            ComboBoxFertigstellungszeit.ItemsSource = db.GetAll<Fertigstellung_Zeit>().Result.ToList();
            ComboBoxFertigstellungszeit.DisplayMemberPath = "P_fertigstellung_zeit_bez";
            ComboBoxFertigstellungszeit.SelectedValuePath = "P_fertigstellung_zeit_nr";
        }

        private void OnCurrentAuftragChanged()
        {
            _auftrag = Auftragsverwaltung.SharedResources.CurrentAuftrag;
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

                    if (!_isAbnahmegesellschaftSelected)
                    {
                        db.Set<Abnahmegesellschaft>().Add(_abnahmegesellschaft);
                    }
                    var entityInDb = db.Set<Probe_Kopf>().Find(int.Parse(TextBoxPruefungsnr.Text));
        
                    if (entityInDb == null)
                    {
                        db.Set<Probe_Kopf>().Add(_probeKopf);
                    }
                    else
                    {
                        _probeKopf.P_nr = entityInDb.P_nr;
                        db.Entry(entityInDb).CurrentValues.SetValues(_probeKopf);
                    }
                    await db.SaveChangesAsync();

                    int index = 1;
                    foreach(Probe_Unter pb in ProbeUnterList)
                    {
                        var entitiesInDb = db.Set<Probe_Unter>().Find(pb.P_nr, index);
        
                        if (entitiesInDb == null)
                        {
                            pb.P_nr = _probeKopf.P_nr;
                            pb.Pe_nr = index;
                            db.Set<Probe_Unter>().Add(pb);
                            index++;
                        }
                        else
                        {
                            pb.P_nr = _probeKopf.P_nr;
                            pb.Pe_nr = index;
                            db.Entry(entitiesInDb).CurrentValues.SetValues(pb);
                            index++;
                        }
                    }

                    _auftrag.Status_nr = 4;
                    var entityInDbAuftrag = db.Set<Auftrag>().Find(_auftrag.Auf_nr);
                    db.Entry(entityInDbAuftrag).CurrentValues.SetValues(_auftrag);
                    await db.SaveChangesAsync();
                    Auftragsverwaltung.SharedResources.Step = 4;
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

        private void TextBoxProbeanzahl_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var lastPruefung = db.GetLastEntity<Probe_Kopf, int>(pk => pk.P_nr);
            if (lastPruefung == null)
            {
                TextBoxPruefungsnr.Text = "1";
                TextBoxProbenr.Text = Auftragsverwaltung.SharedResources.CurrentAuftrag.Auf_nr.ToString();
                return;
            }
            int newP_nr = lastPruefung.P_nr + 1;
            TextBoxPruefungsnr.Text = newP_nr.ToString();
            TextBoxProbenr.Text = Auftragsverwaltung.SharedResources.CurrentAuftrag.Auf_nr.ToString();
        }
    }
}