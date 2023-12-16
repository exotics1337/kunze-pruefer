namespace kunze_prüfer.Views.Stammdaten
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Data;
    using DataBase;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public partial class CustomDataGrid : UserControl
    {
        public CustomDataGrid()
        {
            InitializeComponent();
        }
        public void KonfiguriereSpaltenFuerModell(Type modellTyp, bool showDeleted = false, bool showAll = true)
        {
            baseDataGrid.Columns.Clear();
            
            if (modellTyp == typeof(Kunde))
            {
                baseDataGrid.Columns.Add(new DataGridTextColumn{ Header = "ID", Binding = new Binding("k_nr")});
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Kundenname", Binding = new Binding("k_name")});
                baseDataGrid.Columns.Add(new DataGridTextColumn{ Header = "Umsatzsteuer", Binding = new Binding("k_ust_id")});
                baseDataGrid.Columns.Add(new DataGridTextColumn{ Header = "Lieferaddresse", Binding = new Binding("k_lief_adresse")});
                baseDataGrid.Columns.Add(new DataGridTextColumn{ Header = "Gelöscht", Binding = new Binding("k_geloescht")});
            }
            else if (modellTyp == typeof(Mitarbeiter))
            {
                // Konfigurieren Sie hier die Spalten für Mitarbeiter
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Mitarbeitername", Binding = new Binding("M_nname") });
            }
            else if (modellTyp == typeof(Auftrag))
            {
                if (showAll)
                {
                    baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Auftragsnummer", Binding = new Binding("Auf_nr") });
                    baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Auftragsannahme", Binding = new Binding("Auf_angenommen") });
                    baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Liefertermin", Binding = new Binding("Auf_liefertermin") });
                    baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Kundennummer", Binding = new Binding("k_nr") });
                    baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Statusnummer", Binding = new Binding("Status_nr") });
                    baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Werkstoffnummer", Binding = new Binding("w_nr") });
                    baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Normnummer", Binding = new Binding("n_nr") });
                    baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Bestellnummer", Binding = new Binding("auf_bestell_nr") });
                    baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Prüflos", Binding = new Binding("Auf_prüflos") });
                    baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Ansprechpartnernummer", Binding = new Binding("Anspr_nr") });
                    baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Probennummer", Binding = new Binding("Prob_nr") }); 
                }
                else
                {
                    baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Auftragsnummer", Binding = new Binding("Auf_nr") });
                    baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Statusnummer", Binding = new Binding("Status_nr") });
                    baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Liefertermin", Binding = new Binding("Auf_liefertermin") });
                    baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Kundennummer", Binding = new Binding("k_nr") });
                }

                if (showDeleted)
                {
                    baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("Auf_geloescht") });
                }
            }
        }

        private async Task<List<T>> LoadAsync<T>() where T : class
        {
            DBQ db = new DBQ();
            var items = await db.GetAll<T>();
            return items;
        }

        public async void InitializeData<T>() where T : class
        {
            var itemList = await Task.Run(()=> LoadAsync<T>());
            var itemsObserv = new ObservableCollection<T>(itemList);
            Dispatcher.Invoke(() =>
            {
                baseDataGrid.ItemsSource = itemsObserv;
            });
        }

        public void RefreshData()
        {
            InitializeData<Kunde>();
        }

        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}