﻿namespace kunze_prüfer.Views.Stammdaten
{
    using System.Windows.Controls;

    public partial class Mehrwertsteuer : UserControl
    {
        public Mehrwertsteuer()
        {
            InitializeComponent();
            cdg_mehrwert.KonfiguriereSpaltenFuerModell(typeof(Mehrwertsteuer));
            cdg_mehrwert.InitializeData<DataBase.Mehrwertsteuer>();
        }
    }
}