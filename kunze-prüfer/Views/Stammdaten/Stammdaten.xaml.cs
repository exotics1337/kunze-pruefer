﻿namespace kunze_prüfer.Views.Stammdaten
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
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
            Kunden.tesel.baseDataGrid.RowEditEnding += (s, e) => HandleNewEntity<Kunde>(Kunden.tesel, e);
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
            Stumm.Add("Werkstoff");
            AuswahlCb1.SelectedIndex = 0;
        }

        private void ChangeUser()
        {
            CustomDataGrid dg = new CustomDataGrid();
            dg.RefreshData();
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
                        UpdateEntity(Kunden.tesel.baseDataGrid.ItemsSource as IEnumerable<Kunde>,"k_nr");
                        break;
                    case "Mitarbeiter":
                        break;
                    case "Abnahmegesellschaft":
                        break; 
                    case "Ansprechpartner":
                        break;
                    case "Fertigstellungszeit":
                        break; 
                    case "Norm":
                        break;   
                    case "Mehrwertsteuer":
                        break;
                    case "Angebot":
                        break; 
                    case "Prüfungstypen":
                        break;                    
                } 
            }
            else
            {
                Console.WriteLine("ich hasse alle");
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
                CustomDataGrid dg = new CustomDataGrid();
                dg.RefreshData();
            }
        }
        
        private void HandleNewEntity<T>(CustomDataGrid dataGrid, DataGridRowEditEndingEventArgs e) where T : class, new()
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var newRow = e.Row.Item as T;
                if (newRow != null && IsNewEntity(newRow))
                {
                   AddEntityAsync(newRow); 
                }
            }
        }

        private bool IsNewEntity<T>(T entity) where T : class
        {
            if (typeof(T) == typeof(Kunden))
            {
                var kunde = entity as Kunde;
                return kunde != null && kunde.k_nr == 0; 
            }
            return false;
        }
        private async void AddEntityAsync<T>(T entity) where T : class
        {
            try
            {
                await DAS.AddAsync(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        
        private void searchalgo(int id)
        {
            DBQ db = new DBQ();
            db.GetEntityByIdAsync<Kunde, int>(id);
        }

       
        
    }
}