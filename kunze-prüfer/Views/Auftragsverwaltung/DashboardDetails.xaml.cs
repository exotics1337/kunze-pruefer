using System;
using System.Windows.Controls;
using AdonisUI.Controls;
using kunze_prüfer.DataBase;

namespace kunze_prüfer.Views.Auftragsverwaltung
{
    public partial class DashboardDetails : UserControl
    {
        private DBQ db = new DBQ();
        public DashboardDetails(int id)
        {
            InitializeComponent();
            if (id != 0)
            { 
                LoadDetails(id);
            }
        }

        private async void LoadDetails(int id)
        {
            var auftrag = await db.GetEntityByIdAsync<Auftrag, int>(id);
            var kunde = await db.GetEntityByIdAsync<Kunde, int>(auftrag.k_nr);
            var ansprechpartner = await db.GetEntityByIdAsync<Ansprechpartner, int>(auftrag.Anspr_nr);
            Dispatcher.Invoke(() =>
            {
                LieferterminText.Text = auftrag.Auf_liefertermin.ToString();
                KundeText.Text = kunde.k_name;
                AnsprechpartnerText.Text = ansprechpartner.Anspr_vname + " " + ansprechpartner.Anspr_nname;
                AnsprechpartnerEMailText.Text = ansprechpartner.Anspr_email;
                AuftragsnummerText.Text = auftrag.Auf_nr.ToString();
            }); // TODO: Fix this
        }
    }
}