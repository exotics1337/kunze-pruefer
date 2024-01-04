using kunze_prüfer.Models;

namespace kunze_prüfer.Views.Stammdaten
{
    using System.Windows.Controls;

    public partial class Abnahmegesellschaft : UserControl
    {
        public Abnahmegesellschaft()
        {
            InitializeComponent();
            cdg_abnahme.KonfiguriereSpaltenFuerModell(typeof(Abnahmegesellschaft));
            cdg_abnahme.InitializeData<DataBase.Abnahmegesellschaft>();
        }
    }
}