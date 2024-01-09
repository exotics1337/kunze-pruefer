using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using kunze_prüfer.DataBase;

namespace kunze_prüfer.Views.Auftragsverwaltung.DBSichten
{
    public partial class AuftragErledigt : UserControl
    {
        private DataBase.DBQ db = new DataBase.DBQ();
        private Auftrag _auftrag;
        private Rechnung _rechnung;
        public AuftragErledigt()
        {
            InitializeComponent();
            _auftrag = Auftragsverwaltung.SharedResources.CurrentAuftrag;
        }
        
        private void OnCurrentAuftragChanged()
        {
            _auftrag = Auftragsverwaltung.SharedResources.CurrentAuftrag;
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
    }
}