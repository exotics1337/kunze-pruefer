using System.Configuration;
using System.Windows;

namespace kunze_prüfer.Views
{
    public partial class DatabaseConfig : Window
    {
        private Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public DatabaseConfig()
        {
            InitializeComponent();
            LoadConfig();
        }

        void LoadConfig()
        {
            string conString = ConfigurationManager.AppSettings["connectionString"];
            int ServerIndex = conString.IndexOf("Server=");
            string Server = conString.Substring(0, ServerIndex); // zu ende bringen
        }

        private void AcceptBtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void DenyBtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}