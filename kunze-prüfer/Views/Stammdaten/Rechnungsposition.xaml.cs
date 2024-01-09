namespace kunze_prüfer.Views.Stammdaten
{
    using System.Windows.Controls;

    public partial class Rechnungsposition : UserControl
    {
        public Rechnungsposition()
        {
            InitializeComponent();
            cdg_rechnungsposi.KonfiguriereSpaltenFuerModell(typeof(Rechnungsposition));
            cdg_rechnungsposi.InitializeData<DataBase.Rechnungsposition>();
        }
    }
}