﻿namespace kunze_prüfer.Views.Stammdaten
{
    using System.Windows.Controls;

    public partial class Auftrag : UserControl
    {
        public Auftrag()
        {
            InitializeComponent();
            cdg_auftrag.KonfiguriereSpaltenFuerModell(typeof(Auftrag));
            cdg_auftrag.InitializeData<DataBase.Auftrag>();
        }
    }
}