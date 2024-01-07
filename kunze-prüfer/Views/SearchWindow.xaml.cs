using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using kunze_prüfer.DataBase;
using kunze_prüfer.Views.Stammdaten;
using Abnahmegesellschaft = kunze_prüfer.DataBase.Abnahmegesellschaft;
using Angebot = kunze_prüfer.DataBase.Angebot;
using Ansprechpartner = kunze_prüfer.DataBase.Ansprechpartner;
using Auftrag = kunze_prüfer.DataBase.Auftrag;
using Mehrwertsteuer = kunze_prüfer.DataBase.Mehrwertsteuer;
using Mitarbeiter = kunze_prüfer.DataBase.Mitarbeiter;
using Norm = kunze_prüfer.DataBase.Norm;
using Werkstoff = kunze_prüfer.DataBase.Werkstoff;

namespace kunze_prüfer.Views
{
    public partial class SearchWindow : Window
    {
        private Type _tableType;
        private string _tableTypeString;
        public int CurrentlySelectedID;
        private DBQ db;

        public SearchWindow(string query, string table) 
        {
            InitializeComponent();
            Type tableType = Type.GetType("kunze_prüfer.DataBase." + table);
            _tableType = tableType;
            _tableTypeString = table;
            DataGrid.IsReadOnly = true;
            db = new DBQ();
            KonfiguriereSpaltenFuerModell(tableType, showid: true);
        }

        public async Task SearchWithType(string query = "", int queryInt = 0)
        {

            
                if (!string.IsNullOrEmpty(query))
                {
                    switch (_tableTypeString)
                    {
                        case "Abnahmegesellschaft":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Abnahmegesellschaft>(query);
                            break;
                        case "Angebot_Textbaustein":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Angebot_Textbaustein>(query);
                            break;
                        case "Angebot":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Angebot>(query);
                            break;
                        case "Angebotsposition":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Angebotsposition>(query);
                            break;
                        case "Ansprechpartner":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Ansprechpartner>(query);
                            break;
                        case "Auftrag":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Auftrag>(query);
                            break;
                        case "Fertigstellung_Zeit":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Fertigstellung_Zeit>(query);
                            break;
                        case "Kunden_Ansprechpartner":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Kunden_Ansprechpartner>(query);
                            break;
                        case "Kunde":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Kunde>(query);
                            break;
                        case "Mehrwertsteuer":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Mehrwertsteuer>(query);
                            break;
                        case "Mitarbeiter":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Mitarbeiter>(query);
                            break;
                        case "Norm":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Norm>(query);
                            break;
                        case "Probe_Kopf":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Probe_Kopf>(query);
                            break;
                        case "Probe_Unter":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Probe_Unter>(query);
                            break;
                        case "Pruefungstyp":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Pruefungstyp>(query);
                            break;
                        case "Rechnung":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Rechnung>(query);
                            break;
                        case "Rechnungsposition":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Rechnungsposition>(query);
                            break;
                        case "Status":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Status>(query);
                            break;
                        case "Textbaustein":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Textbaustein>(query);
                            break;
                        case "Werkstoff_Pruefung":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Werkstoff_Pruefung>(query);
                            break;
                        case "Werkstoff":
                            DataGrid.ItemsSource = await db.GetFilteredAsync<DataBase.Werkstoff>(query);
                            break;
                    }
                }
        }

        private Func<T, bool> GetPredicate<T>(string query) where T : class
        {
            return entity =>
            {
                foreach (var prop in typeof(T).GetProperties())
                {
                    if (prop.PropertyType == typeof(string))
                    {
                        var value = prop.GetValue(entity) as string;
                        if (!string.IsNullOrEmpty(value) && value.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            };
        }


        private void ButtonSelect_OnClick(object sender, RoutedEventArgs e)
        {
            var row = (DataGridRow)DataGrid.ItemContainerGenerator.ContainerFromIndex(DataGrid.SelectedIndex);

            if (row != null)
            {
                // Get the DataGridCellsPresenter from the row
                DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);

                if (presenter != null)
                {
                    // Get the first cell of the selected row
                    DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(0);

                    if (cell != null)
                    {
                        // Assuming the cell contains text, you might need to adjust this for other types of content
                        TextBlock txt = cell.Content as TextBlock;

                        if (txt != null)
                        {
                            CurrentlySelectedID = int.Parse(txt.Text);
                            this.Close();
                        }
                    }
                }
            }
        }
        
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = (DataGridRow)DataGrid.ItemContainerGenerator.ContainerFromIndex(DataGrid.SelectedIndex);

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
                            this.Close();
                        }
                    }
                }
            }
        }

        private void KonfiguriereSpaltenFuerModell(Type modellTyp, bool showDeleted = false, bool showAll = true, bool showid = false)
        {
            DataGrid.Columns.Clear();
            
            if (modellTyp == typeof(Kunde))
            {
                if (showid)
                {
                    DataGrid.Columns.Add(new DataGridTextColumn{ Header = "ID", Binding = new Binding("k_nr"), IsReadOnly = true});
                }
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Kundenname", Binding = new Binding("k_name")});
                DataGrid.Columns.Add(new DataGridTextColumn{ Header = "Umsatzsteuer", Binding = new Binding("k_ust_id")});
                DataGrid.Columns.Add(new DataGridTextColumn{ Header = "Lieferaddresse", Binding = new Binding("k_lief_adresse")});
                DataGrid.Columns.Add(new DataGridTextColumn{ Header = "Gelöscht", Binding = new Binding("k_geloescht")});
                
            }
            else if (modellTyp == typeof(Mitarbeiter))
            {
                // Konfigurieren Sie hier die Spalten für Mitarbeiter
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Id", Binding = new Binding("M_nr"), IsReadOnly = true});
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Vorname", Binding = new Binding("M_vname") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Nachname", Binding = new Binding("M_nname") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Passwort", Binding = new Binding("M_pass") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Admin", Binding = new Binding("M_admin") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("M_geloescht") });
            }
            else if (modellTyp == typeof(Auftrag))
            {
                if (showAll)
                {
                    DataGrid.Columns.Add(new DataGridTextColumn { Header = "Auftragsnummer", Binding = new Binding("Auf_nr"), IsReadOnly = true});
                    DataGrid.Columns.Add(new DataGridTextColumn { Header = "Auftragsannahme", Binding = new Binding("Auf_angenommen") });
                    DataGrid.Columns.Add(new DataGridTextColumn { Header = "Liefertermin", Binding = new Binding("Auf_liefertermin") });
                    DataGrid.Columns.Add(new DataGridTextColumn { Header = "Kundennummer", Binding = new Binding("k_nr") });
                    DataGrid.Columns.Add(new DataGridTextColumn { Header = "Statusnummer", Binding = new Binding("Status_nr") });
                    DataGrid.Columns.Add(new DataGridTextColumn { Header = "Werkstoffnummer", Binding = new Binding("w_nr") });
                    DataGrid.Columns.Add(new DataGridTextColumn { Header = "Normnummer", Binding = new Binding("n_nr") });
                    DataGrid.Columns.Add(new DataGridTextColumn { Header = "Bestellnummer", Binding = new Binding("auf_bestell_nr") });
                    DataGrid.Columns.Add(new DataGridTextColumn { Header = "Prüflos", Binding = new Binding("Auf_prüflos") });
                    DataGrid.Columns.Add(new DataGridTextColumn { Header = "Ansprechpartnernummer", Binding = new Binding("Anspr_nr") });
                    DataGrid.Columns.Add(new DataGridTextColumn { Header = "Probennummer", Binding = new Binding("Prob_nr") }); 
                }
                else
                {
                    DataGrid.Columns.Add(new DataGridTextColumn { Header = "Auftragsnummer", Binding = new Binding("Auf_nr") });
                    DataGrid.Columns.Add(new DataGridTextColumn { Header = "Statusnummer", Binding = new Binding("Status_nr") });
                    DataGrid.Columns.Add(new DataGridTextColumn { Header = "Liefertermin", Binding = new Binding("Auf_liefertermin") });
                    DataGrid.Columns.Add(new DataGridTextColumn { Header = "Kundennummer", Binding = new Binding("k_nr") });
                }

                if (showDeleted)
                {
                    DataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("Auf_geloescht") });
                }
            }
            else if (modellTyp == typeof(Mehrwertsteuer))
            {
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("Mwst_nr"), IsReadOnly = true});
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Steuersatz", Binding = new Binding("Mswt_satz") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("Mswt_geloescht") });
            }
            else if (modellTyp == typeof(Norm))
            {
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("N_nr"), IsReadOnly = true});               
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Bezeichnung", Binding = new Binding("N_bez") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("N_geloescht") });
            }
            else if (modellTyp == typeof(Abnahmegesellschaft))
            {
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("Abnahme_nr"), IsReadOnly = true});               
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Bezeichnung", Binding = new Binding("Abnhme_bez") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("Abnahme_geloescht") });
            }
            else if (modellTyp == typeof(Ansprechpartner))
            {
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("Anspr_nr"), IsReadOnly = true });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Vorname", Binding = new Binding("Anspr_vname") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Nachname", Binding = new Binding("Anspr_nname") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Telefon", Binding = new Binding("Anspr_tel") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "E-Mail", Binding = new Binding("Anspr_email") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Position", Binding = new Binding("Anspr_position") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("Anspr_geloescht") });
            }
            else if (modellTyp == typeof(Fertigstellungszeit))
            {
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Fertigstellungszeit-ID", Binding = new Binding("P_fertigstellung_zeit_nr"), IsReadOnly = true });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Bezeichnung", Binding = new Binding("P_fertigstellung_zeit_bez") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("P_fertigstellung_geloescht") });
            }
            else if (modellTyp == typeof(Prüfungstypen))
            {
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Typ-ID", Binding = new Binding("Pe_Typ_nr"), IsReadOnly = true });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Typbezeichnung", Binding = new Binding("Pe_typ_bez") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Durchschnittspreis", Binding = new Binding("Pe_durch_preis") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("Pe_geloescht") });
            }
            else if (modellTyp == typeof(Textbausteine))
            {
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Textbaustein-Nr", Binding = new Binding("Textbaustein_nr"), IsReadOnly = true });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Überschrift", Binding = new Binding("Text_Ueberschrift") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Inhalt", Binding = new Binding("Text_Inhalt") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("Text_geloescht") });
            }
            else if (modellTyp == typeof(Werkstoff))
            {
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Nr", Binding = new Binding("w_nr"), IsReadOnly = true });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Name", Binding = new Binding("w_name") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Kurzbeschreibung", Binding = new Binding("w_kurz") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Kennzeichen", Binding = new Binding("w_kennzeichen") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Oberfläche", Binding = new Binding("w_oberflaeche") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Höhe", Binding = new Binding("w_hoehe") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Breite", Binding = new Binding("w_breite") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Länge", Binding = new Binding("w_laenge") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Gewicht", Binding = new Binding("w_gewicht") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("w_geloescht") });
            }
            else if (modellTyp==typeof(Angebot))
            {
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Angebot-Nr", Binding = new Binding("Ang_nr"), IsReadOnly = true });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Probevoraussetzung", Binding = new Binding("Ang_probe_vorraussetzung") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Angenommen", Binding = new Binding("Ang_angenommen") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Gültigkeitsdatum", Binding = new Binding("Ang_gueltigkeit_dat") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "MwSt-Nr", Binding = new Binding("Mwst_nr") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Auftrags-Nr", Binding = new Binding("Auf_nr") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("Ang_geloescht") });
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            db.Dispose();
            base.OnClosed(e);
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
