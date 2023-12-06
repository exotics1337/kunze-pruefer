using System.Windows.Controls;

namespace kunze_prüfer.Views.Auftragsverwaltung
{
    public partial class DashboardDetails : UserControl
    {
        public DashboardDetails(int id, bool IsAuftragsverwaltung = false)
        {
            InitializeComponent();
            AuftragsverwaltungUI(IsAuftragsverwaltung);
        }

        private void AuftragsverwaltungUI(bool IsAuftragsverwaltung)
        {
            if (IsAuftragsverwaltung)
            {
                AuftragsbearbeiterFeld.Visibility = System.Windows.Visibility.Collapsed;
                AuftragsnummerHeaderText.Text = "Auftragslieferungstermin";
                AuftragsnummerText.Text = "01.01.2021";
            }
        }
    }
}