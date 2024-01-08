﻿using System;
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
            ComboBoxMwstSatz.ItemsSource = db.GetAll<Mehrwertsteuer>().Result.ToList();
            ComboBoxMwstSatz.DisplayMemberPath = "Mwst_satz";
            ComboBoxMwstSatz.SelectedValuePath = "Mwst_nr";
        }

        private async void OnSubmitButtonClicked()
        {
            if (Auftragsverwaltung.SharedResources.Step == 3)
            {
                if (
                    TextBoxAngebotsnr != null && TextBoxProbeVoraussetzung != null &&
                    DatePickerGueltigkeitsdatum.SelectedDate.HasValue && ComboBoxMwstSatz.SelectedIndex != -1 &&
                    CheckBoxAngebotAngenommen.IsChecked.HasValue
                    )
                {
                    _angebot = new Angebot()
                    {
                        Ang_probe_vorraussetzung = TextBoxProbeVoraussetzung.Text,
                        Ang_angenommen = CheckBoxAngebotAngenommen.IsChecked.Value,
                        Ang_gueltigkeit_dat = DatePickerGueltigkeitsdatum.SelectedDate.Value,
                        Mwst_nr = int.Parse(ComboBoxMwstSatz.SelectedValue.ToString()),
                        Auf_nr = _auftrag.Auf_nr
                    };
                    MessageBox.Show(Convert.ToString(ComboBoxMwstSatz.SelectedValue));
                    db.Set<Angebot>().Add(_angebot);
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

        private void ButtonAngebotErstellen_OnClick(object sender, RoutedEventArgs e)
        {
            
            PDFCreator pdfCreator = new PDFCreator(new InvoiceDataSource());
            pdfCreator.Show();
            pdfCreator.Closed += (o, args) =>
            {
                var lastAngebot = db.GetLastEntity<Angebot, int>(a => a.Ang_nr);
                if (lastAngebot == null)
                {
                    TextBoxAngebotsnr.Text = "1";
                    return;
                }
                int newAng_nr = lastAngebot.Ang_nr + 1;
                TextBoxAngebotsnr.Text = newAng_nr.ToString();
            };
        }
    }
}