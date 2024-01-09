namespace kunze_prüfer.Views.Stammdaten
{
    using System.Windows.Controls;

    public partial class Angebotsposition : UserControl
    {
        public Angebotsposition()
        {
            InitializeComponent();
            cdg_angebotsposi.KonfiguriereSpaltenFuerModell(typeof(Angebotsposition));
            cdg_angebotsposi.InitializeData<DataBase.Angebotsposition>();
        }
    }
}