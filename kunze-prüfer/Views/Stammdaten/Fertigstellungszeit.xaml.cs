namespace kunze_prüfer.Views.Stammdaten
{
    using System.Windows.Controls;
    using DataBase;

    public partial class Fertigstellungszeit : UserControl
    {
        public Fertigstellungszeit()
        {
            InitializeComponent();
            cdg_fertigstellung.KonfiguriereSpaltenFuerModell(typeof(Fertigstellungszeit));
            cdg_fertigstellung.InitializeData<Fertigstellung_Zeit>();
        }
    }
}