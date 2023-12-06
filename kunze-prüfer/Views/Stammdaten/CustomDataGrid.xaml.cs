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
        public void KonfiguriereSpaltenFuerModell(Type modellTyp)
        {
            baseDataGrid.Columns.Clear();
    
            if (modellTyp == typeof(Kunde))
            {
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Kundenname", Binding = new Binding("k_name") });
                // Konfigurieren Sie hier die Spalten für Kunden
            }
            else if (modellTyp == typeof(Mitarbeiter))
            {
                // Konfigurieren Sie hier die Spalten für Mitarbeiter
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Mitarbeitername", Binding = new Binding("M_nname") });
            }

        }

        private async Task<List<T>> LoadAsync<T>() where T : class
        {
            DBQ db = new DBQ();
            var items = await db.GetAll<T>();
            return items;
        }

        public async void InitializeDate<T>() where T : class
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
            InitializeDate<Kunde>();
        }
    }
}