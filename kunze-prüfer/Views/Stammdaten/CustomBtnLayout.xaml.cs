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
            Task taskToAwait = null;
            Stammdaten.SearchBox.Text = "Suche";

            switch (Stammdaten.AuswahlCb1.SelectedItem.ToString())
            {
                case "Kunden":
                    taskToAwait = Stammdaten.Kunden.tesel.InitializeData<DataBase.Kunde>();
                    break;
                case "Mitarbeiter":
                    taskToAwait = Stammdaten.Mitarbeiter.cdg_mitarbeiter.InitializeData<DataBase.Mitarbeiter>();
                    break;
                case "Abnahmegesellschaft":
                    taskToAwait = Stammdaten.Abnahmegesellschaft.cdg_abnahme
                        .InitializeData<DataBase.Abnahmegesellschaft>();
                    break;
                case "Ansprechpartner":
                    taskToAwait = Stammdaten.Ansprechpartner.cdg_ansprech.InitializeData<DataBase.Ansprechpartner>();
                    break;
                case "Fertigstellungszeit":
                    taskToAwait = Stammdaten.Fertigstellungszeit.cdg_fertigstellung
                        .InitializeData<DataBase.Fertigstellung_Zeit>();
                    break;
                case "Norm":
                    taskToAwait = Stammdaten.Norm.cdg_norm.InitializeData<DataBase.Norm>();
                    break;
                case "Mehrwertsteuer":
                    taskToAwait = Stammdaten.Mehrwertsteuer.cdg_mehrwert.InitializeData<DataBase.Mehrwertsteuer>();
                    break;
                case "Angebot":
                    taskToAwait = Stammdaten.Angebot.cdg_angebot.InitializeData<DataBase.Angebot>();
                    break;
                case "Prüfungstypen":
                    taskToAwait = Stammdaten.Prüfungstypen.cdg_prüf.InitializeData<DataBase.Pruefungstyp>();
                    break;
                case "Auftrag":
                    taskToAwait = Stammdaten.Auftrag.cdg_auftrag.InitializeData<DataBase.Auftrag>();
                    break;
                case "Werkstoff":
                    taskToAwait = Stammdaten.Werkstoff.cdg_werkstoff.InitializeData<DataBase.Werkstoff>();
                    break;
                case "Textbausteine":
                    taskToAwait = Stammdaten.Textbausteine.cdg_textbaustein.InitializeData<DataBase.Textbaustein>();
                    break;
                case "Angebotsposition":
                    taskToAwait = Stammdaten.Angebotsposition.cdg_angebotsposi.InitializeData<DataBase.Angebotsposition>();
                    break;
                case "Kundenansprechpartner":
                    taskToAwait = Stammdaten.Kundenansprechpartner.cdg_kundenansprech
                        .InitializeData<DataBase.Kunden_Ansprechpartner>();
                    break;
                case "Rechnung":
                    taskToAwait = Stammdaten.Rechnung.cdg_rechnung.InitializeData<DataBase.Rechnung>();
                    break;
                case "Rechnungsposition":
                    taskToAwait = Stammdaten.Rechnungsposition.cdg_rechnungsposi
                        .InitializeData<DataBase.Rechnungsposition>();
                    break;
                case "Angebotstextbausteine":
                    taskToAwait = Stammdaten.Angebotstextbausteine.cdg_angebotstext
                        .InitializeData<DataBase.Angebot_Textbaustein>();
                    break;
                case "Werkstoffprüfung":
                    taskToAwait = Stammdaten.Werkstoffprüfung.cdg_werkstoffpruef
                        .InitializeData<DataBase.Werkstoff_Pruefung>();
                    break;
                default:
                    MessageBox.Show("Unbekannte Auswahl. Bitte wählen Sie eine gültige Option.");
                    return;
            }

            if (taskToAwait != null)
            {
                await taskToAwait;
            }
        }


        private async void Search_btn_OnClick(object sender, RoutedEventArgs e)
        {
            SearchWindow sw = new SearchWindow(Stammdaten.SearchBox.Text, GetTable());
            if (Stammdaten.SearchBox.Text != "" && int.TryParse(Stammdaten.SearchBox.Text ,out int result ))
            {
                await sw.SearchWithType(result);
            }
            else if (Stammdaten.SearchBox.Text != "")
            {
                await sw.SearchWithType(Stammdaten.SearchBox.Text);
            }
            ChooseUpdateView(sw);

        }

        private string GetTable()
        {
            switch (Stammdaten.AuswahlCb1.SelectedItem.ToString())
            {
                case "Kunden":
                    return "Kunde";
                case "Mitarbeiter":
                    return "Mitarbeiter";
                case "Abnahmegesellschaft":
                    return "Abnahmegesellschaft";
                case "Ansprechpartner":
                    return "Ansprechpartner";
                case "Fertigstellungszeit":
                    return "Fertigstellung_Zeit";
                case "Norm":
                    return "Norm";
                case "Mehrwertsteuer":
                    return "Mehrwertsteuer";
                case "Angebot":
                    return "Angebot";
                case "Prüfungstypen":
                    return "Pruefungstyp";
                case"Textbausteine":
                    return "Textbaustein";
                case"Auftrag":
                    return "Auftrag";
                case"Werkstoff":
                    return "Werkstoff";
                case "Angebotsposition":
                    return "Angebotsposition";
                case "Rechnung":
                    return "Rechnung";
                case "Rechnungsposition":
                    return "Rechnungsposition";
               case "Kundenansprechpartner":
                   return "Kundenansprechpartner";
               case "Angebotstextbausteine":
                   return "Angebot_Textbaustein";
               case "Werkstoffprüfung":
                   return "Werkstoff_Pruefung"; 
               
            }

            return "hmm";
        }

        private void ChooseUpdateView(SearchWindow sw)
        {
            switch (Stammdaten.AuswahlCb1.SelectedItem.ToString())
            {
                case "Kunden":
                    Stammdaten.Kunden.tesel.baseDataGrid.ItemsSource = sw.DataGrid.ItemsSource;
                    break;
                case "Mitarbeiter":
                    Stammdaten.Mitarbeiter.cdg_mitarbeiter.baseDataGrid.ItemsSource = sw.DataGrid.ItemsSource;
                    break;
                case "Abnahmegesellschaft":
                    Stammdaten.Abnahmegesellschaft.cdg_abnahme.baseDataGrid.ItemsSource = sw.DataGrid.ItemsSource;
                    break; 
                case "Ansprechpartner":
                    Stammdaten.Ansprechpartner.cdg_ansprech.baseDataGrid.ItemsSource = sw.DataGrid.ItemsSource;
                    break;
                case "Fertigstellungszeit":
                    Stammdaten.Fertigstellungszeit.cdg_fertigstellung.baseDataGrid.ItemsSource =
                        sw.DataGrid.ItemsSource;
                    break; 
                case "Norm":
                    Stammdaten.Norm.cdg_norm.baseDataGrid.ItemsSource = sw.DataGrid.ItemsSource;
                    break;   
                case "Mehrwertsteuer":
                    Stammdaten.Mehrwertsteuer.cdg_mehrwert.baseDataGrid.ItemsSource = sw.DataGrid.ItemsSource;
                    break;
                case "Angebot":
                    Stammdaten.Angebot.cdg_angebot.baseDataGrid.ItemsSource = sw.DataGrid.ItemsSource;
                    break; 
                case "Prüfungstypen":
                    Stammdaten.Prüfungstypen.cdg_prüf.baseDataGrid.ItemsSource = sw.DataGrid.ItemsSource;
                    break;
                case"Textbausteine":
                    Stammdaten.Textbausteine.cdg_textbaustein.baseDataGrid.ItemsSource = sw.DataGrid.ItemsSource;
                    break;                    
                case"Auftrag":
                    Stammdaten.Auftrag.cdg_auftrag.baseDataGrid.ItemsSource = sw.DataGrid.ItemsSource;
                    break;  
                case"Werkstoff":
                    Stammdaten.Werkstoff.cdg_werkstoff.baseDataGrid.ItemsSource = sw.DataGrid.ItemsSource;
                    break;  
                case"Rechnung":
                    Stammdaten.Rechnung.cdg_rechnung.baseDataGrid.ItemsSource = sw.DataGrid.ItemsSource;
                    break;
                case "Rechnungsposition":
                    Stammdaten.Rechnungsposition.cdg_rechnungsposi.baseDataGrid.ItemsSource = sw.DataGrid.ItemsSource;
                    break;
                case"Angebotsposition":
                    Stammdaten.Angebotsposition.cdg_angebotsposi.baseDataGrid.ItemsSource = sw.DataGrid.ItemsSource;
                    break;
                case "Kundenansprechpartner":
                    Stammdaten.Kundenansprechpartner.cdg_kundenansprech.baseDataGrid.ItemsSource =
                        sw.DataGrid.ItemsSource;
                    break;
                case "Werkstoffprüfung":
                    Stammdaten.Werkstoffprüfung.cdg_werkstoffpruef.baseDataGrid.ItemsSource = sw.DataGrid.ItemsSource;
                    break;
                case "Angebotstextbausteine":
                    Stammdaten.Angebotstextbausteine.cdg_angebotstext.baseDataGrid.ItemsSource =
                        sw.DataGrid.ItemsSource;
                    break;
                                
            }
        }

      
    }
}