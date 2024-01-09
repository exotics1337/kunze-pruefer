namespace kunze_prüfer.Views.Stammdaten
{
    using System.Windows.Controls;

    public partial class Rechnung : UserControl
    {
        public Rechnung()
        {
            InitializeComponent();
            cdg_rechnung.KonfiguriereSpaltenFuerModell(typeof(Rechnung));
            cdg_rechnung.InitializeData<DataBase.Rechnung>();
        }
    }
}