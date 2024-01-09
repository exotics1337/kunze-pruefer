namespace kunze_prüfer.Views.Stammdaten
{
    using System.Windows.Controls;

    public partial class Angebotstextbausteine : UserControl
    {
        public Angebotstextbausteine()
        {
            InitializeComponent();
            cdg_angebotstext.KonfiguriereSpaltenFuerModell(typeof(Angebotstextbausteine));
            cdg_angebotstext.InitializeData<DataBase.Angebot_Textbaustein>();
        }
    }
}