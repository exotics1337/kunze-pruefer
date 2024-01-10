using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using kunze_prüfer.DataBase;
using kunze_prüfer.Models;
using kunze_prüfer.Views.QuickPDF;
using static kunze_prüfer.Models.TextBoxChecker;

namespace kunze_prüfer.Views.Auftragsverwaltung.DBSichten
{
    public partial class Angebotbestätigung : UserControl
    {
        private DBQ db = new DBQ();
        private Auftrag _auftrag;
        private Angebot _angebot;
        public Angebotbestätigung()
        {
            InitializeComponent();
            _auftrag = Auftragsverwaltung.SharedResources.CurrentAuftrag;
            Auftragsverwaltung.SubmitButtonClicked += OnSubmitButtonClicked;
            Auftragsverwaltung.CurrentAuftragChanged += OnCurrentAuftragChanged;
            try
            {
                ComboBoxMwstSatz.ItemsSource = db.GetAll<Mehrwertsteuer>().Result.ToList();
                ComboBoxMwstSatz.DisplayMemberPath = "Mwst_satz";
                ComboBoxMwstSatz.SelectedValuePath = "Mwst_nr";
            }
            catch (Exception ex)
            {
                AdonisUI.Controls.MessageBox.Show($"Fehler beim Laden der Mehrwertsteuersätze: {ex.Message}", "Initialisierungsfehler", AdonisUI.Controls.MessageBoxButton.OK);
            }
        }

        private void OnCurrentAuftragChanged()
        {
            _auftrag = Auftragsverwaltung.SharedResources.CurrentAuftrag;
        }
        
        private async void OnSubmitButtonClicked()
        {
            try
            {
                if (Auftragsverwaltung.SharedResources.Step == 3)
                {
                    if (TextBoxAngebotsnr != null && TextBoxProbeVoraussetzung != null &&
                        DatePickerGueltigkeitsdatum.SelectedDate.HasValue && ComboBoxMwstSatz.SelectedIndex != -1 &&
                        CheckBoxAngebotAngenommen.IsChecked.HasValue)
                    {
                        _angebot = new Angebot()
                        {
                            Ang_probe_vorraussetzung = TextBoxProbeVoraussetzung.Text,
                            Ang_angenommen = CheckBoxAngebotAngenommen.IsChecked.Value,
                            Ang_gueltigkeit_dat = DatePickerGueltigkeitsdatum.SelectedDate.Value,
                            Mwst_nr = int.Parse(ComboBoxMwstSatz.SelectedValue.ToString()),
                            Auf_nr = _auftrag.Auf_nr
                        };

                        var entityInDb = db.Set<Angebot>().Find(int.Parse(TextBoxAngebotsnr.Text));
                        if (entityInDb == null)
                        {
                            foreach (var el in Auftragsverwaltung.SharedResources.CurrentAngebotspositionList)
                            {
                                el.Ang_nr = _angebot.Ang_nr;
                                db.Set<Angebotsposition>().Add(el);
                            }
                            db.Set<Angebot>().Add(_angebot);
                        }
                        else
                        {
                            db.Entry(entityInDb).CurrentValues.SetValues(_angebot);
                        }

                        _auftrag.Status_nr = 3;
                        var entityInDbAuftrag = db.Set<Auftrag>().Find(_auftrag.Auf_nr);
                        db.Entry(entityInDbAuftrag).CurrentValues.SetValues(_auftrag);

                        await db.SaveChangesAsync();
                        Auftragsverwaltung.SharedResources.Step = 4;
                        Auftragsverwaltung.SharedResources.CurrentAuftrag = _auftrag;
                        AdonisUI.Controls.MessageBox.Show("Angebot wurde erfolgreich erstellt!", "Speichern erfolgreich!", AdonisUI.Controls.MessageBoxButton.OK);
                        MainGrid.Background = new SolidColorBrush(Color.FromRgb(178, 255, 171));
                    }
                    else
                    {
                        AdonisUI.Controls.MessageBox.Show("Es wurden nicht alle notwendingen Felder ausgefüllt!", "Speichern nicht erfolgreich!", AdonisUI.Controls.MessageBoxButton.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                AdonisUI.Controls.MessageBox.Show($"Ein Fehler ist aufgetreten: {ex.Message}", "Fehler", AdonisUI.Controls.MessageBoxButton.OK);
                // Optional: Logging des Fehlers
            }
        }


        
        private async void ButtonAngebotErstellen_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var lastAngebot = db.GetLastEntity<Angebot, int>(a => a.Ang_nr);
                if (lastAngebot == null)
                {
                    TextBoxAngebotsnr.Text = "1";
                }
                else
                {
                    int newAng_nr = lastAngebot.Ang_nr + 1;
                    TextBoxAngebotsnr.Text = newAng_nr.ToString();
                }

                InvoiceDataSource IS = new InvoiceDataSource();
                InvoiceTemplate IT = new InvoiceTemplate();
                InvoiceTemplate.InvoiceKunde IK = new InvoiceTemplate.InvoiceKunde();
                InvoiceTemplate.AngebotModel AM = new InvoiceTemplate.AngebotModel();

                var kunde = await db.Set<Kunde>().FirstOrDefaultAsync(k => k.k_nr == _auftrag.k_nr);
                if (kunde != null)
                {
                    IK.Name = kunde.k_name;
                    IK.Adresse = kunde.k_lief_adresse;
                    IK.USTID = kunde.k_ust_id;
                }

                AM.Kunde = IK;
                AM.AngebotNr = int.Parse(TextBoxAngebotsnr.Text);

                if (ComboBoxMwstSatz.SelectedIndex != -1)
                {
                    AM.MwSt = double.Parse(ComboBoxMwstSatz.Text);
                }
                else
                {
                    AM.MwSt = 19;
                }

                PDFCreator pdfCreator = new PDFCreator(IS, IK, int.Parse(TextBoxAngebotsnr.Text), AM, null, true);
                pdfCreator.Show();
                pdfCreator.Closed += (o, args) =>
                {
                    // Schließ-Logik, falls erforderlich
                };
            }
            catch (Exception ex)
            {
                AdonisUI.Controls.MessageBox.Show($"Ein Fehler ist aufgetreten: {ex.Message}", "Fehler", AdonisUI.Controls.MessageBoxButton.OK);
            }
        }

    }
}