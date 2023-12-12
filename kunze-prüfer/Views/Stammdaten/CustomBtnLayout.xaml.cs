namespace kunze_prüfer.Views.Stammdaten
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using DataBase;

    public partial class CustomBtnLayout : UserControl
    {
        public CustomBtnLayout()
        {
            InitializeComponent();
        }

        private DBQ db = new DBQ();
        public Stammdaten Stammdaten { get; set; }

        private void Update_btn_OnClick(object sender, RoutedEventArgs e)
        {
            // Stammdaten stammi = this.Parent as Stammdaten;
            if (Stammdaten != null)
            {
                find_tabel(Stammdaten);
            }
            else
            {
                Console.WriteLine("huso");
            }
            // if (sender is Stammdaten stammi)
            // {
            //     Console.WriteLine(stammi);
            // }
        }

        private void find_tabel(Stammdaten stammi)
        {
            Console.WriteLine(stammi);
            if (stammi.AuswahlCb1.SelectedItem != null)
            {
                switch (stammi.AuswahlCb1.SelectedItem.ToString())
                {
                    case "Kunden":
                        Console.WriteLine("Got Kunde");
                        UpdateEntities(db.Kunden.ToList());
                        break;
                }    
            }
            else
            {
                Console.WriteLine("BOMBA");
            }

        }

        private void UpdateEntities<T>(IEnumerable<T> entities) where T : class
        {
            try
            {
                foreach (var entity in entities)
                {
                    db.Entry(entity).State = EntityState.Modified;
                }

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}