namespace kunze_prüfer.Views.Stammdaten
{
    using System.Windows.Controls;

    public partial class Kundenansprechpartner : UserControl
    {
        public Kundenansprechpartner()
        {
            InitializeComponent();
            cdg_kundenansprech.KonfiguriereSpaltenFuerModell(typeof(Kundenansprechpartner));
            cdg_kundenansprech.InitializeData<DataBase.Kunden_Ansprechpartner>();
        }
    }
}