﻿namespace kunze_prüfer.Views.Stammdaten
{
    using System;
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
            // CustomBtnLayout cbl = new CustomBtnLayout();
            // cbl.Update_btn.stammm = this;
            Kunden.kunde_layout.Stammdaten = this;
        }
        //Databinding
        
        public ObservableCollection<string> Stumm { get; set; }
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
            CustomBtnLayout cbl = new CustomBtnLayout();
            DBQ db = new DBQ();
            if (AuswahlCb1.SelectedItem != null)
            {
                switch (AuswahlCb1.SelectedItem.ToString())
                {
                    case "Kunden":
                        Console.WriteLine("boom");
                        cbl.UpdateEntities(db.Kunden.ToList());
                        break;
                    case "Mitarbeiter":
                        
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
            else
            {
                Console.WriteLine("ich hasse alle");
            }

        } 
        private void AuswahlCb1_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeUser();
            
        }

        private void searchalgo(int id)
        {
            DBQ db = new DBQ();
            db.GetEntityByIdAsync<Kunde, int>(id);
        }

       
        
    }
}