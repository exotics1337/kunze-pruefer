using System.ComponentModel;
using System.Windows.Controls;
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
            
            //DataGrid.SelectionChanged += DataGrid_SelectionChanged;
        }
        
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //
        }
        
        async void GetAufträge()
        {
           DataGrid.KonfiguriereSpaltenFuerModell(typeof(Auftrag), false, false);
           DataGrid.InitializeData<Auftrag>();
           DataGrid.RefreshData();
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
        
    }
}