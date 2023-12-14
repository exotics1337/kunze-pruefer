using System;
using System.Windows.Controls;
using kunze_prüfer.DataBase;

namespace kunze_prüfer.Views.Auftragsverwaltung.DBSichten
{
    public partial class AuftragAnlegen : UserControl
    {
        private DBQ db = new DBQ();
        public AuftragAnlegen()
        {
            InitializeComponent();
            Auftragsverwaltung.SubmitButtonClicked += OnSubmitButtonClicked;
        }
        
        private void OnSubmitButtonClicked()
        {
            Auftrag newAuftrag = new Auftrag
            {
                Auf_liefertermin = DateTime.Parse(DatePickerLiefertermin.Text),
                n_nr = int.Parse(TextBoxNormnr.Text),
                auf_bestell_nr = int.Parse(TextBoxBestellnr.Text),
                Auf_prüflos = int.Parse(TextBoxPrüflos.Text),
                // Anspr_nr = int.Parse(Ansprechpartnernummer.Text),
                // Prob_nr = int.Parse(Probennummer.Text)
            };

        }
    }
}