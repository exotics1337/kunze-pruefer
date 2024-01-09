using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using AdonisUI.Controls;
using kunze_prüfer.DataBase;
using kunze_prüfer.Views.Auftragsverwaltung;


namespace kunze_prüfer.Views
{
    using Stammdaten;
    public partial class Dashboard : UserControl
    {
        private DBQ db = new DBQ();

        public Dashboard()
        {
            InitializeComponent();
            DataContext = this;
            CurrentView = new DashboardDetails(0);
            GetAufträge();
            DataGrid.SelectionChangedEvent += OnCustomDataGridSelectionChanged;
            DataGrid.IsReadOnly = true;
            DataGrid.handleSelectionChanged = true;
        }


        private void OnCustomDataGridSelectionChanged()
        {
            CurrentView = new DashboardDetails(DataGrid.currentlySelectedId);
        }
        async Task GetAufträge()
        {
            DataGrid.KonfiguriereSpaltenFuerModell(typeof(DataBase.Auftrag), false, false);
            var aufträge = await db.GetAll<DataBase.Auftrag>().ConfigureAwait(false);
            var filteredAufträge = aufträge.Where(a=> !a.Auf_geloescht).ToList();
            DataGrid.SetItemsSource(filteredAufträge);
        }

        
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged(nameof(CurrentView));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void ButtonEditAuftrag_OnClick(object sender, RoutedEventArgs e)
        {
            int CurrentlySelectedID = 0;
            var row = (DataGridRow)DataGrid.baseDataGrid.ItemContainerGenerator.ContainerFromIndex(DataGrid.baseDataGrid.SelectedIndex);

            if (row != null)
            {
                DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);

                if (presenter != null)
                {
                    DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(0);

                    if (cell != null)
                    {
                        TextBlock txt = cell.Content as TextBlock;

                        if (txt != null)
                        {
                            CurrentlySelectedID = int.Parse(txt.Text);
                        }
                    }
                }
            }
            Auftragsverwaltung.Auftragsverwaltung av = new Auftragsverwaltung.Auftragsverwaltung(CurrentlySelectedID);
            await av.InitializeAsync();
            MainWindow.OnChangeView(av);
        }
        
        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}