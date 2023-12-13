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
            // Stammdaten = this.Stammdaten;
            //Stammdaten stammdaten = new Stammdaten();
            
        }

        public Stammdaten Stammdaten { get; set; }

        private void Update_btn_OnClick(object sender, RoutedEventArgs e)
        {
            Stammdaten.UpdateData();
        }

        

        public void UpdateEntities<T>(IEnumerable<T> entities) where T : class
        {
            try
            {
                DBQ db = new DBQ(); // Besser wäre ein globaler DbContext
                foreach (var entity in entities)
                {
                    // Prüfen, ob die Entität bereits getrackt wird
                    var entry = db.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        db.Set<T>().Attach(entity); // Attach, wenn die Entität nicht getrackt wird
                    }
                    entry.State = EntityState.Modified; // Setzt den Zustand auf Modified
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