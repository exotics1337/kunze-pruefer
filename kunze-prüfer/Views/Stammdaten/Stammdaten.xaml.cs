namespace kunze_prüfer.Views.Stammdaten
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity.Infrastructure.Design;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Xml.Schema;
    using kunze_prüfer.Views.Stammdaten;
    using kunze_prüfer.DataBase;

    public partial class Stammdaten : UserControl
    {
        public Stammdaten()
        {
            InitializeComponent();
            this.DataContext = this;
            Stumm = new ObservableCollection<string>();
            loadbox();
            Kunden.kunde_layout.Stammdaten = this;
            Mitarbeiter.Mitarbei_layout.Stammdaten = this;
            Abnahmegesellschaft.Abnahme_layout.Stammdaten = this;
            Angebot.Angebot_layout.Stammdaten = this;
            Ansprechpartner.Ansprech_layout.Stammdaten = this;
            Auftrag.Auftrag_layout.Stammdaten = this;
            Abnahmegesellschaft.Abnahme_layout.Stammdaten = this;
            Mehrwertsteuer.Mehrwert_layout.Stammdaten = this;
            Werkstoff.Werkstoff_layout.Stammdaten = this;
            Norm.Norm_layout.Stammdaten = this;
            Prüfungstypen.Prüf_layout.Stammdaten = this;
            Fertigstellungszeit.Fertigstell_layout.Stammdaten = this;
            Textbausteine.Textbaustein_layout.Stammdaten = this; 
            
        }
        //Databinding
        public ObservableCollection<string> Stumm { get; set; }
        private DataAccessService DAS = new DataAccessService();
        
        public void loadbox()
        {
            Stumm.Add("Kunden");
            Stumm.Add("Angebot");
            Stumm.Add("Ansprechpartner");
            Stumm.Add("Auftrag");
            Stumm.Add("Mitarbeiter");
            Stumm.Add("Abnahmegesellschaft");
            Stumm.Add("Mehrwertsteuer");
            Stumm.Add("Werkstoff");
            Stumm.Add("Norm");
            Stumm.Add("Prüfungstypen");
            Stumm.Add("Fertigstellungszeit");
            Stumm.Add("Textbausteine");
            AuswahlCb1.SelectedIndex = 0;
        }

        private void ChangeUser()
        {
            CustomDataGrid dg = new CustomDataGrid();
            dg.RefreshData<Kunde>();
            if (AuswahlCb1.SelectedItem!=null)
            {
                string test = AuswahlCb1.SelectedItem.ToString();
                //Console.WriteLine(test);
                foreach (var control in UserGrid.Children)
                {
                    if (control is UserControl)
                    {
                        ((UserControl)control).Visibility = Visibility.Collapsed;
                    }
                }

                switch (test)
                {
                    case "Kunden":
                        Kunden.Visibility = Visibility.Visible;
                        break;
                    case "Mitarbeiter":
                        Mitarbeiter.Visibility = Visibility.Visible;
                        break;
                    case "Abnahmegesellschaft":
                        Abnahmegesellschaft.Visibility = Visibility.Visible;
                        break; 
                    case "Ansprechpartner":
                        Ansprechpartner.Visibility = Visibility.Visible; 
                        break;
                    case "Fertigstellungszeit":
                        Fertigstellungszeit.Visibility = Visibility.Visible; 
                        break; 
                    case "Norm":
                        Norm.Visibility = Visibility.Visible;
                        break;   
                    case "Mehrwertsteuer":
                        Mehrwertsteuer.Visibility = Visibility.Visible; 
                        break;
                    case "Angebot":
                        Angebot.Visibility = Visibility.Visible; 
                        break; 
                    case "Prüfungstypen":
                        Prüfungstypen.Visibility = Visibility.Visible;
                        break;
                    case"Textbausteine":
                        Textbausteine.Visibility = Visibility.Visible;
                        break;                    
                    case"Auftrag":
                        Auftrag.Visibility = Visibility.Visible;
                        break;  
                    case"Werkstoff":
                        Werkstoff.Visibility = Visibility.Visible;
                        break;  
                    
                }    
            }
        }

        public void UpdateData()
        {
            if (AuswahlCb1.SelectedItem != null)
            {
                switch (AuswahlCb1.SelectedItem.ToString())
                {
                    case "Kunden":
                        UpdateEntity(Kunden.tesel.baseDataGrid.ItemsSource as IEnumerable<DataBase.Kunde>,"k_nr");
                        break;
                    case "Mitarbeiter":
                        UpdateEntity(Mitarbeiter.cdg_mitarbeiter.baseDataGrid.ItemsSource as IEnumerable<DataBase.Mitarbeiter>,"M_nr");
                        break;
                    case "Abnahmegesellschaft":
                        UpdateEntity(Abnahmegesellschaft.cdg_abnahme.baseDataGrid.ItemsSource as IEnumerable<DataBase.Abnahmegesellschaft>,"Abnahme_nr");
                        break; 
                    case "Ansprechpartner":
                        UpdateEntity(Ansprechpartner.cdg_ansprech.baseDataGrid.ItemsSource as IEnumerable<DataBase.Ansprechpartner>, "Anspr_nr");
                        break;
                    case "Fertigstellungszeit":
                        UpdateEntity(Fertigstellungszeit.cdg_fertigstellung.baseDataGrid.ItemsSource as IEnumerable<DataBase.Fertigstellung_Zeit>, "P_fertigstellung_zeit_nr");
                        break; 
                    case "Norm":
                        UpdateEntity(Norm.cdg_norm.baseDataGrid.ItemsSource as IEnumerable<DataBase.Norm>, "N_nr");
                        break;   
                    case "Mehrwertsteuer":
                        UpdateEntity(Mehrwertsteuer.cdg_mehrwert.baseDataGrid.ItemsSource as IEnumerable<DataBase.Mehrwertsteuer>,"Mwst_nr");
                        break;
                    case "Angebot":
                        UpdateEntity(Angebot.cdg_angebot.baseDataGrid.ItemsSource as IEnumerable<DataBase.Angebot> as IEnumerable<DataBase.Angebot>, "Ang_nr");
                        break; 
                    case "Prüfungstypen":
                        UpdateEntity(Prüfungstypen.cdg_prüf.baseDataGrid.ItemsSource as IEnumerable<DataBase.Pruefungstyp>, "Pe_Typ_nr");
                        break;
                    case "Auftrag":
                        UpdateEntity(Auftrag.cdg_auftrag.baseDataGrid.ItemsSource as IEnumerable<DataBase.Auftrag>, "Auf_nr");
                        break;
                    case "Werkstoff":
                        UpdateEntity(Werkstoff.cdg_werkstoff.baseDataGrid.ItemsSource as IEnumerable<DataBase.Werkstoff>,"w_nr");
                        break;
                    case "Textbausteine":
                        UpdateEntity(Textbausteine.cdg_textbaustein.baseDataGrid.ItemsSource as IEnumerable<DataBase.Textbaustein>, "Textbaustein_nr");
                        break;
                    
                } 
            }
            else
            {
                Console.WriteLine("Debug");
            }

        } 
        
        private void AuswahlCb1_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeUser();
        }

        private void UpdateEntity<T>(IEnumerable<T> itemstoUpdate, string id) where T : class
        {
            if (itemstoUpdate != null)
            {
                DAS.UpdateEntities(itemstoUpdate,id);
            }
        }
        
        private void searchalgo(int id)
        {
            DBQ db = new DBQ();
            db.GetEntityByIdAsync<Kunde, int>(id);
        }


   
    }
}