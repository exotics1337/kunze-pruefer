namespace kunze_prüfer.Views.Stammdaten
{
    using System.Windows.Controls;

    public partial class Werkstoff : UserControl
    {
        public Werkstoff()
        {
            InitializeComponent();
            cdg_werkstoff.KonfiguriereSpaltenFuerModell(typeof(Werkstoff));
            cdg_werkstoff.InitializeData<DataBase.Werkstoff>();
        }
    }
}