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

        public Stammdaten Stammdaten { get; set; }

        private void Update_btn_OnClick(object sender, RoutedEventArgs e)
        {
            Stammdaten.UpdateData();
        }

        

        
        public void UpdateEntities<T>(IEnumerable<T> entities) where T : class
        {
            try
            {
                DBQ db = new DBQ();
                foreach (var entity in entities)
                {
                    var entry = db.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        db.Set<T>().Attach(entity);
                    }
                    else if (entry.State != EntityState.Unchanged)
                    {
                        // Detach the entity if it's being tracked but not in Unchanged state
                        entry.State = EntityState.Detached;
                        db.Set<T>().Attach(entity);
                    }
                    entry.State = EntityState.Modified;
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