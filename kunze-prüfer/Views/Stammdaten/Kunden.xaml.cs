namespace kunze_prüfer.Views.Stammdaten
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using DataBase;

    public partial class Kunden : UserControl
    {
        public Kunden()
        {
            InitializeComponent();
            tesel.KonfiguriereSpaltenFuerModell(typeof(Kunde), showid:true);
            tesel.InitializeData<Kunde>();
            
        }
    }
}