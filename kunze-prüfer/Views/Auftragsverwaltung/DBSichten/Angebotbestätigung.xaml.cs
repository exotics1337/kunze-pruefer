using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        }

        private async void OnSubmitButtonClicked()
        {
            if (Auftragsverwaltung.SharedResources.Step == 3)
            {
                if (AreAllTextBoxesFilled(GroupBoxAngebot))
                {
                    _angebot = new Angebot()
                    {
                        Ang_nr = int.Parse(TextBoxAngebotsnr.Text),
                        Ang_probe_vorraussetzung = TextBoxProbeVoraussetzung.Text,
                        Ang_angenommen = CheckBoxAngebotAngenommen.IsChecked.Value,
                        Ang_gueltigkeit_dat = DatePickerGueltigkeitsdatum.SelectedDate.Value,
                        Mwst_nr = ComboBoxMwstSatz.SelectedIndex + 1,
                        Auf_nr = _auftrag.Auf_nr
                    };
                    db.Set<Angebot>().Add(_angebot);
                    await db.SaveChangesAsync();
                    Auftragsverwaltung.SharedResources.Step = 4;
                    Auftragsverwaltung.SharedResources.CurrentAuftrag = _auftrag;
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