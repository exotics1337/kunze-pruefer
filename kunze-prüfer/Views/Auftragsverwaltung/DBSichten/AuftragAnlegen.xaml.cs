using System.Windows;
using System.Windows.Controls;
using kunze_prüfer.DataBase;

namespace kunze_prüfer.Views.Auftragsverwaltung.DBSichten
{
    public partial class AuftragAnlegen : UserControl
    {
        private DBQ db = new DBQ();
        private Auftrag _auftrag;
        public AuftragAnlegen()
        {
            InitializeComponent();
            _auftrag = Auftragsverwaltung.SharedResources.CurrentAuftrag;
            Auftragsverwaltung.SubmitButtonClicked += OnSubmitButtonClicked;
        }
        
        
        private void OnSubmitButtonClicked()
        {
            
        }

        private async void ButtonPrüfnormAuswählen_OnClick(object sender, RoutedEventArgs e)
        {
            SearchWindow sw = new SearchWindow(TextBoxSuchen.Text, "Norm");
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
                var prüfnorm = await db.GetEntityByIdAsync<Norm, int>(sw.CurrentlySelectedID);
                if (prüfnorm != null)
                {
                    TextBlockSelectedNorm.Text = prüfnorm.N_bez;
                    TextBoxNormnr.Text = prüfnorm.N_nr.ToString();
                    TextBoxPrüfnormbezeichnung.Text = prüfnorm.N_bez;
                }
            }
        }
    }
}