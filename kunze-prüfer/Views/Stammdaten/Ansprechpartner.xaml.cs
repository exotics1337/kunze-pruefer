namespace kunze_prüfer.Views.Stammdaten
{
    using System.Windows.Controls;

    public partial class Ansprechpartner : UserControl
    {
        public Ansprechpartner()
        {
            InitializeComponent();
            cdg_ansprech.KonfiguriereSpaltenFuerModell(typeof(Ansprechpartner));
            cdg_ansprech.InitializeData<DataBase.Ansprechpartner>();
        }
    }
}