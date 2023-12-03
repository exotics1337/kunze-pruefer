namespace kunze_prüfer.Views.Stammdaten
{
    using System.Collections.ObjectModel;
    using System.Windows.Controls;

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
            Stumm.Add("test");
        }
    }
}