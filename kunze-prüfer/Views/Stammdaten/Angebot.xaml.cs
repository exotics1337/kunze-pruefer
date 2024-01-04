namespace kunze_prüfer.Views.Stammdaten
{
    using System.Windows.Controls;

    public partial class Angebot : UserControl
    {
        public Angebot()
        {
            InitializeComponent();
            cdg_angebot.KonfiguriereSpaltenFuerModell(typeof(Angebot),showAll:true);
            cdg_angebot.InitializeData<DataBase.Angebot>();
        }
    }
}