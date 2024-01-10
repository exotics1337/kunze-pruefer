using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using kunze_prüfer.DataBase;
using kunze_prüfer.Models;
using kunze_prüfer.Views.QuickPDF;

namespace kunze_prüfer.Views.Auftragsverwaltung.DBSichten
{
    public partial class AuftragErledigt : UserControl
    {
        private DataBase.DBQ db = new DataBase.DBQ();
        private Auftrag _auftrag;
        private Rechnung _rechnung;
        public ObservableCollection<Probe_Unter> ProbeUnterList = new ObservableCollection<Probe_Unter>();
        public AuftragErledigt()
        {
            InitializeComponent();
            _auftrag = Auftragsverwaltung.SharedResources.CurrentAuftrag;
            ProbeUnterList = Auftragsverwaltung.SharedResources.CurrentProbeUnterList;
            Auftragsverwaltung.SubmitButtonClicked += OnSubmitButtonClicked;
            Auftragsverwaltung.CurrentAuftragChanged += OnCurrentAuftragChanged;
        }
        
        private void OnCurrentAuftragChanged()
        {
            _auftrag = Auftragsverwaltung.SharedResources.CurrentAuftrag;
            ProbeUnterList = Auftragsverwaltung.SharedResources.CurrentProbeUnterList;
        }
        
        private async void OnSubmitButtonClicked()
        {
            if (Auftragsverwaltung.SharedResources.Step == 6)
            {
                if (TextBoxRechnungsnr.Text != "" && DatePickerRechnungsdatum.SelectedDate.HasValue && DatePickerZahlungsziel.SelectedDate.HasValue && CheckBoxSkonto.IsChecked.HasValue && CheckBoxSkonto.IsChecked.Value)
                {
                    _auftrag.Status_nr = 6;
                    
                    var _angebot = await db.Set<Angebot>().FirstOrDefaultAsync(a => a.Auf_nr == Auftragsverwaltung.SharedResources.CurrentAuftrag.Auf_nr);
                    
                    _rechnung = new Rechnung()
                    {
                        r_datum = DatePickerRechnungsdatum.SelectedDate.Value,
                        r_zahlungsziel = DatePickerZahlungsziel.SelectedDate.Value,
                        r_skontofaehig = CheckBoxSkonto.IsChecked.Value,
                        Ang_nr = _angebot.Ang_nr,
                    };
                    db.Set<Rechnung>().Add(_rechnung);
                    var entityInDbAuftrag = db.Set<Auftrag>().Find(_auftrag.Auf_nr);
                    db.Entry(entityInDbAuftrag).CurrentValues.SetValues(_auftrag);
                    
                    foreach (var el in Auftragsverwaltung.SharedResources.CurrentRechnungspositionList)
                    {
                        el.r_nr = _rechnung.r_nr;
                        db.Set<Rechnungsposition>().Add(el);
                    }
                    
                    await db.SaveChangesAsync();
                    Auftragsverwaltung.SharedResources.Step = 6;
                    Auftragsverwaltung.SharedResources.CurrentAuftrag = _auftrag;
                    AdonisUI.Controls.MessageBox.Show("Auftrag wurde erfolgreich bestätigt!", "Speichern erfolgreich!", AdonisUI.Controls.MessageBoxButton.OK);
                }
                else
                {
                    AdonisUI.Controls.MessageBox.Show("Auftrag wurde nicht bestätigt!", "Speichern nicht erfolgreich!", AdonisUI.Controls.MessageBoxButton.OK);
                }
            }
        }

        private async void ButtonRechnungErstellen_OnClick(object sender, RoutedEventArgs e)
        {
            var lastRechnung = db.GetLastEntity<Rechnung, int>(r => r.r_nr);
            if (lastRechnung == null)
            {
                TextBoxRechnungsnr.Text = "1";
            }
            else
            {
                int newR_nr = lastRechnung.r_nr + 1;
                TextBoxRechnungsnr.Text = newR_nr.ToString();
            }
            
            InvoiceDataSource IS = new InvoiceDataSource();

            foreach (var pu in ProbeUnterList)
            {
                InvoiceTemplate.Artikel A = new InvoiceTemplate.Artikel();
                A.Name = pu.P_nr.ToString();
            }
            
            InvoiceTemplate IT = new InvoiceTemplate();
            InvoiceTemplate.InvoiceKunde IK = new InvoiceTemplate.InvoiceKunde();
            InvoiceTemplate.RechnungModel RM = new InvoiceTemplate.RechnungModel();

            var kunde = await db.Set<Kunde>().FirstOrDefaultAsync(k => k.k_nr == _auftrag.k_nr);
            IK.Name = kunde.k_name;
            IK.Adresse = kunde.k_rech_adresse;
            IK.USTID = kunde.k_ust_id;

            RM.Kunde = IK;
            RM.RechnungNr = int.Parse(TextBoxRechnungsnr.Text);
            
            
            PDFCreator pdfCreator = new PDFCreator(IS, IK, int.Parse(TextBoxRechnungsnr.Text), null, RM, false);
            pdfCreator.Show();
            pdfCreator.Closed += (o, args) =>
            {

            };
        }
    }
}