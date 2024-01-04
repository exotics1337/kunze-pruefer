namespace kunze_prüfer.Views.Stammdaten
{
    using System.Windows.Controls;
    using DataBase;

    public partial class Textbausteine : UserControl
    {
        public Textbausteine()
        {
            InitializeComponent();
            cdg_textbaustein.KonfiguriereSpaltenFuerModell(typeof(Textbausteine));
            cdg_textbaustein.InitializeData<Textbaustein>();
        }
    }
}