namespace kunze_prüfer.Views.Stammdaten
{
    using System.Windows.Controls;
    using DataBase;

    public partial class Prüfungstypen : UserControl
    {
        public Prüfungstypen()
        {
            InitializeComponent();
            cdg_prüf.KonfiguriereSpaltenFuerModell(typeof(Prüfungstypen));
            cdg_prüf.InitializeData<Pruefungstyp>();
        }
    }
}