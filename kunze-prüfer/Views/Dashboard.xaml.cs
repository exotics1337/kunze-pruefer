using System.Windows.Controls;

namespace kunze_prüfer.Views
{
    using DataBase;
    using Stammdaten;

    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
            CustomDataGrid dg = new CustomDataGrid();
            dg.RefreshData();
        }
    }
}