namespace kunze_prüfer.Views.Stammdaten
{
    using System.Windows.Controls;

    public partial class Norm : UserControl
    {
        public Norm()
        {
            InitializeComponent();
            cdg_norm.KonfiguriereSpaltenFuerModell(typeof(Norm));
            cdg_norm.InitializeData<DataBase.Norm>();
        }
    }
}