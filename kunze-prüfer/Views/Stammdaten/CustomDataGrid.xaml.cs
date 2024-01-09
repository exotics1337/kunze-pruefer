using System.Diagnostics;
using AdonisUI.Controls;
using kunze_prüfer.Models;

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
        
        DBQ db = new DBQ();
        public int currentlySelectedId = 0;
        public bool handleSelectionChanged = false;
        public event Action SelectionChangedEvent;
        public bool IsReadOnly
        {
            get => baseDataGrid.IsReadOnly;
            set => baseDataGrid.IsReadOnly = value;
        }
        public void KonfiguriereSpaltenFuerModell(Type modellTyp, bool showDeleted = false, bool showAll = true, bool showid = false)
        {
            baseDataGrid.Columns.Clear();
            
            if (modellTyp == typeof(Kunde))
            {
                if (showid)
                {
                    baseDataGrid.Columns.Add(new DataGridTextColumn{ Header = "ID", Binding = new Binding("k_nr"), IsReadOnly = true});
                }
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Kundenname", Binding = new Binding("k_name")});
                baseDataGrid.Columns.Add(new DataGridTextColumn{ Header = "Umsatzsteuer", Binding = new Binding("k_ust_id")});
                baseDataGrid.Columns.Add(new DataGridTextColumn{ Header = "Lieferaddresse", Binding = new Binding("k_lief_adresse")});
                baseDataGrid.Columns.Add(new DataGridTextColumn{ Header = "Gelöscht", Binding = new Binding("k_geloescht")});
                
            }
            else if (modellTyp == typeof(Mitarbeiter))
            {
                // Konfigurieren Sie hier die Spalten für Mitarbeiter
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Id", Binding = new Binding("M_nr"), IsReadOnly = true});
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Vorname", Binding = new Binding("M_vname") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Nachname", Binding = new Binding("M_nname") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Passwort", Binding = new Binding("M_pass") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Admin", Binding = new Binding("M_admin") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("M_geloescht") });
            }
            else if (modellTyp == typeof(Auftrag))
            {
                if (showAll)
                {
                    baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Auftragsnummer", Binding = new Binding("Auf_nr"), IsReadOnly = true});
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
            else if (modellTyp == typeof(Mehrwertsteuer))
            {
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("Mwst_nr"), IsReadOnly = true});
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Steuersatz", Binding = new Binding("Mswt_satz") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("Mswt_geloescht") });
            }
            else if (modellTyp == typeof(Norm))
            {
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("N_nr"), IsReadOnly = true});               
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Bezeichnung", Binding = new Binding("N_bez") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("N_geloescht") });
            }
            else if (modellTyp == typeof(Abnahmegesellschaft))
            {
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("Abnahme_nr"), IsReadOnly = true});               
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Bezeichnung", Binding = new Binding("Abnhme_bez") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("Abnahme_geloescht") });
            }
            else if (modellTyp == typeof(Ansprechpartner))
            {
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("Anspr_nr"), IsReadOnly = true });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Vorname", Binding = new Binding("Anspr_vname") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Nachname", Binding = new Binding("Anspr_nname") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Telefon", Binding = new Binding("Anspr_tel") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "E-Mail", Binding = new Binding("Anspr_email") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Position", Binding = new Binding("Anspr_position") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("Anspr_geloescht") });
            }
            else if (modellTyp == typeof(Fertigstellungszeit))
            {
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Fertigstellungszeit-ID", Binding = new Binding("P_fertigstellung_zeit_nr"), IsReadOnly = true });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Bezeichnung", Binding = new Binding("P_fertigstellung_zeit_bez") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("P_fertigstellung_geloescht") });
            }
            else if (modellTyp == typeof(Prüfungstypen))
            {
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Typ-ID", Binding = new Binding("Pe_Typ_nr"), IsReadOnly = true });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Typbezeichnung", Binding = new Binding("Pe_typ_bez") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Durchschnittspreis", Binding = new Binding("Pe_durch_preis") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("Pe_geloescht") });
            }
            else if (modellTyp == typeof(Textbausteine))
            {
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Textbaustein-Nr", Binding = new Binding("Textbaustein_nr"), IsReadOnly = true });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Überschrift", Binding = new Binding("Text_Ueberschrift") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Inhalt", Binding = new Binding("Text_Inhalt") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("Text_geloescht") });
            }
            else if (modellTyp == typeof(Werkstoff))
            {
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Nr", Binding = new Binding("w_nr"), IsReadOnly = true });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Name", Binding = new Binding("w_name") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Kurzbeschreibung", Binding = new Binding("w_kurz") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Kennzeichen", Binding = new Binding("w_kennzeichen") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Oberfläche", Binding = new Binding("w_oberflaeche") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Höhe", Binding = new Binding("w_hoehe") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Breite", Binding = new Binding("w_breite") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Länge", Binding = new Binding("w_laenge") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Gewicht", Binding = new Binding("w_gewicht") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("w_geloescht") });
            }
            else if (modellTyp==typeof(Angebot))
            {
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Angebot-Nr", Binding = new Binding("Ang_nr"), IsReadOnly = true });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Probevoraussetzung", Binding = new Binding("Ang_probe_vorraussetzung") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Angenommen", Binding = new Binding("Ang_angenommen") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Gültigkeitsdatum", Binding = new Binding("Ang_gueltigkeit_dat") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "MwSt-Nr", Binding = new Binding("Mwst_nr") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Auftrags-Nr", Binding = new Binding("Auf_nr") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("Ang_geloescht") });
            }
            
            else if (modellTyp == typeof(Rechnung))
            {
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Rechnungs-Nr", Binding = new Binding("r_nr"), IsReadOnly = true });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Datum", Binding = new Binding("r_datum") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Zahlungsziel", Binding = new Binding("r_zahlungsziel") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Angebots-Datum", Binding = new Binding("r_angebots_dat") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Prüf-Datum", Binding = new Binding("r_pruef_dat") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Skontofähig", Binding = new Binding("r_skontofaehig") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Angebots-Nr", Binding = new Binding("Ang_nr") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Gelöscht", Binding = new Binding("r_geloescht") });
            }
            else if (modellTyp == typeof(Angebotsposition))
            {
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Angebots-Nr", Binding = new Binding("Ang_nr"), IsReadOnly = true });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Prüfungstyp-Nr", Binding = new Binding("Pe_typ_nr") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Preis", Binding = new Binding("Rp_preis") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Menge", Binding = new Binding("Rp_menge") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Bemerkung", Binding = new Binding("Rp_bemerkung") });
            }
            else if (modellTyp == typeof(Rechnungsposition))
            {
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Rechnungs-Nr", Binding = new Binding("r_nr"), IsReadOnly = true });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Prüfungstyp-Nr", Binding = new Binding("Pe_typ_nr") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Preis", Binding = new Binding("Rp_preis") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Menge", Binding = new Binding("Rp_menge") });
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Bemerkung", Binding = new Binding("Rp_bemerkung") });
            }
            else if (modellTyp == typeof(Kundenansprechpartner))
            {
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Kunden-Nr", Binding = new Binding("K_nr")});
                baseDataGrid.Columns.Add(new DataGridTextColumn { Header = "Ansprechpartner-Nr", Binding = new Binding("Anspr_nr")});
            }
        }

        private async Task<List<T>> LoadAsync<T>() where T : class
        {
            var items = await db.GetAll<T>();
            return items;
        }

        public async Task InitializeData<T>(bool showDeleted = true) where T : class
        {
            var itemList = await Task.Run(()=> LoadAsync<T>());
            var itemsObserv = new ObservableCollection<T>(itemList);
            Dispatcher.Invoke(() =>
            {
                baseDataGrid.ItemsSource = itemsObserv;
            });
        }

        public void RefreshData<T>() where T : class
        {
            InitializeData<T>();
        }

        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!handleSelectionChanged) return;
            currentlySelectedId = (int)baseDataGrid.SelectedCells[0].Item.GetType().GetProperty("Auf_nr")
                .GetValue(baseDataGrid.SelectedCells[0].Item, null); 
            SelectionChangedEvent?.Invoke();
        }
        
        public (int Id, Type Type) GetDataFromCurrentlySelectedId()
        {
            try
            {
                var item = baseDataGrid.SelectedCells[0].Item;
                var type = item.GetType();
                var idProperty = type.GetProperty("Id");
                if (idProperty != null)
                {
                    var id = (int)idProperty.GetValue(item);
                    return (id, type);
                }
                else
                {
                    throw new Exception($"The type {type.Name} does not have an 'Id' property.");
                }
            }
            catch (Exception e)
            {
                ErrorLogger.Log(e);
                return (0, null);
            }
        }
        
        public void Search<T>(string query) where T : class
        {
            if (baseDataGrid.ItemsSource != null)
            {
                var items = baseDataGrid.ItemsSource as IEnumerable<T>;
                var filtered = items.Where(item =>
                {
                    var type = item.GetType();
                    var properties = type.GetProperties();

                    return properties.Any(property =>
                    {
                        var value = property.GetValue(item);
                        return value != null && value.ToString().Contains(query);
                    });
                });
                Dispatcher.Invoke(() =>
                {
                    baseDataGrid.ItemsSource = filtered;
                });
            }
            else
            {
                AdonisUI.Controls.MessageBox.Show("Es wurden keine Ergebnisse gefunden",
                    "Suche war nicht Erfolgreich!", AdonisUI.Controls.MessageBoxButton.OK);
            }
        }
        
        public void Search<T>(int query) where T : class
        {
            var items = baseDataGrid.ItemsSource as IEnumerable<T>;
            var filtered = items.Where(item =>
            {
                var type = item.GetType();
                var properties = type.GetProperties();

                return properties.Any(property =>
                {
                    var value = property.GetValue(item);
                    return value != null && value.ToString().Contains(query.ToString());
                });
            });
            baseDataGrid.ItemsSource = filtered;
        }
        
        public void SetItemsSource<T>(IEnumerable<T> items) where T : class
        {
            Dispatcher.Invoke(() =>
                {
                    baseDataGrid.ItemsSource = items;
                    baseDataGrid.Items.Refresh();
                    Debug.WriteLine(items.Count());
                    Debug.WriteLine(baseDataGrid.Items.Count);
                }
            );
        }
    }
}