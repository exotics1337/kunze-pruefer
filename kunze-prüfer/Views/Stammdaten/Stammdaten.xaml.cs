namespace kunze_prüfer.Views.Stammdaten
{
    using System;
    using System.Collections.ObjectModel;
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


        }
        //Databinding
        
        public ObservableCollection<string> Stumm { get; set; }
        private void loadbox()
        {
            Stumm.Add("Kunden");
            Stumm.Add("Angebot");
            Stumm.Add("Ansprechpartner");
            Stumm.Add("Auftrag");
            Stumm.Add("Mitarbeiter");
            Stumm.Add("Abnahmegesellschaft");
            Stumm.Add("Mehrwertsteuer");
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
                }    
            }
            else
            {
                
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