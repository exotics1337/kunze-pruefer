namespace kunze_prüfer.Views.Stammdaten
{
    using System.Windows.Controls;

    public partial class Werkstoffprüfung : UserControl
    {
        public Werkstoffprüfung()
        {
            InitializeComponent();
            cdg_werkstoffpruef.KonfiguriereSpaltenFuerModell(typeof(Werkstoffprüfung));
            cdg_werkstoffpruef.InitializeData<DataBase.Werkstoff_Pruefung>();
        }
    }
}