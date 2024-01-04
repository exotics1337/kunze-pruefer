namespace kunze_prüfer.Views.Stammdaten
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using DataBase;
    using Models;

    public partial class CustomBtnLayout : UserControl
    {
        public CustomBtnLayout()
        {
            InitializeComponent();
            
            
        }

        public Stammdaten Stammdaten { get; set; }

        private void Update_btn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Stammdaten.UpdateData();
            }
            catch (Exception exception)
            {
                AdonisUI.Controls.MessageBox.Show("Bitte überprüfen sie Ihre Eingaben");
            }
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
        private async void Refresh_btn_OnClick(object sender, RoutedEventArgs e)
        {
            var kundenTask = Stammdaten.Kunden.tesel.InitializeData<DataBase.Kunde>();
            var mitarbeiterTask = Stammdaten.Mitarbeiter.cdg_mitarbeiter.InitializeData<DataBase.Mitarbeiter>();
            var abnahmegesellschaftTask = Stammdaten.Abnahmegesellschaft.cdg_abnahme.InitializeData<DataBase.Abnahmegesellschaft>();
            var ansprechpartnerTask = Stammdaten.Ansprechpartner.cdg_ansprech.InitializeData<DataBase.Ansprechpartner>();
            var fertigstellungszeitTask = Stammdaten.Fertigstellungszeit.cdg_fertigstellung.InitializeData<DataBase.Fertigstellung_Zeit>();
            var normTask = Stammdaten.Norm.cdg_norm.InitializeData<DataBase.Norm>();
            var mehrwertsteuerTask = Stammdaten.Mehrwertsteuer.cdg_mehrwert.InitializeData<DataBase.Mehrwertsteuer>();
            var angebotTask = Stammdaten.Angebot.cdg_angebot.InitializeData<DataBase.Angebot>();
            var pruefungstypenTask = Stammdaten.Prüfungstypen.cdg_prüf.InitializeData<DataBase.Pruefungstyp>();
            var auftragTask = Stammdaten.Auftrag.cdg_auftrag.InitializeData<DataBase.Auftrag>();
            var werkstoffTask = Stammdaten.Werkstoff.cdg_werkstoff.InitializeData<DataBase.Werkstoff>();
            var textbausteineTask = Stammdaten.Textbausteine.cdg_textbaustein.InitializeData<DataBase.Textbaustein>();

            await Task.WhenAll(kundenTask, mitarbeiterTask, abnahmegesellschaftTask, ansprechpartnerTask, fertigstellungszeitTask, normTask, mehrwertsteuerTask, angebotTask, pruefungstypenTask, auftragTask, werkstoffTask, textbausteineTask);
        }


    }
}