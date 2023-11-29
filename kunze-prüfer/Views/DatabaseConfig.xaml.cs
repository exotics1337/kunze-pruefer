using System;
using System.Configuration;
using System.Data.SqlClient;
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
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["KunzeDB"].ConnectionString;
                ConnectionStringInfo connectionInfo = ParseConnectionString(connectionString);

                TextBoxIP.Text = connectionInfo.Server;
                TextBoxDBName.Text = connectionInfo.Database;
                TextBoxDBUser.Text = connectionInfo.UserId;
                TextBoxDBPasswort.Password = connectionInfo.Password;
            }
            catch
            {
                AdonisUI.Controls.MessageBox.Show("Datenbankkonfiguration konnte nicht geladen werden. Wir Empfehlen die Datenbank 'KunzeDB' zu nennen, damit die Konfiguration automatisch geladen wird", "Datenbankkonfiguration fehlgeschlagen!", AdonisUI.Controls.MessageBoxButton.OK);
            }
        }
        
        static ConnectionStringInfo ParseConnectionString(string connectionString)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);

            string server = builder.DataSource;
            string database = builder.InitialCatalog;
            string userId = builder.UserID;
            string password = builder.Password;

            return new ConnectionStringInfo(server, database, userId, password);
        }
        
        class ConnectionStringInfo
        {
            public string Server { get; }
            public string Database { get; }
            public string UserId { get; }
            public string Password { get; }

            public ConnectionStringInfo(string server, string database, string userId, string password)
            {
                Server = server;
                Database = database;
                UserId = userId;
                Password = password;
            }
        }

        private void AcceptBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DenyBtn_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateConnectionString(TextBoxIP.Text, TextBoxDBName.Text, TextBoxDBUser.Text, TextBoxDBPasswort.Password);
            this.Close();
        }
        static void UpdateConnectionString(string newServer, string newDatabase, string newUser, string newPassword)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            ConnectionStringsSection connectionStringsSection = config.ConnectionStrings;
            if (connectionStringsSection != null)
            {
                ConnectionStringSettings connectionStringSettings = connectionStringsSection.ConnectionStrings["KunzeDB"];
                if (connectionStringSettings != null)
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionStringSettings.ConnectionString);
                    builder.DataSource = newServer;
                    builder.InitialCatalog = newDatabase;
                    builder.UserID = newUser;
                    builder.Password = newPassword;

                    connectionStringSettings.ConnectionString = builder.ToString();
                    config.Save(ConfigurationSaveMode.Modified);

                    ConfigurationManager.RefreshSection("connectionStrings");
                }
            }
        }
    }
}