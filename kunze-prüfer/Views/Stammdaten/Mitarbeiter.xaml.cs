namespace kunze_prüfer.Views.Stammdaten
{
    using System.Windows.Controls;

    public partial class Mitarbeiter : UserControl
    {
        public Mitarbeiter()
        {
            InitializeComponent();
            cdg_mitarbeiter.KonfiguriereSpaltenFuerModell(typeof(Mitarbeiter));
            cdg_mitarbeiter.InitializeData<DataBase.Mitarbeiter>();
        }
    }
}