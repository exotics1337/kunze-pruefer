using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
using kunze_prüfer.DataBase;

namespace kunze_prüfer.Views.Auftragsverwaltung.DBSichten
{
    using System;

    public partial class WerkstoffprüfungFinished : UserControl
    {
        private DataBase.DBQ db = new DataBase.DBQ();
        private Auftrag _auftrag;
        public ObservableCollection<Probe_Unter> ProbeUnterList = new ObservableCollection<Probe_Unter>();
        public WerkstoffprüfungFinished()
        {
            InitializeComponent();
            _auftrag = Auftragsverwaltung.SharedResources.CurrentAuftrag;
            Auftragsverwaltung.CurrentAuftragChanged += OnCurrentAuftragChanged;
            Auftragsverwaltung.SubmitButtonClicked += OnSubmitButtonClicked;
            ProbeUnterList = Auftragsverwaltung.SharedResources.CurrentProbeUnterList;
            DataGridWerkstoffpruefung.ItemsSource = ProbeUnterList;
        }

        private void OnCurrentAuftragChanged()
        {
            _auftrag = Auftragsverwaltung.SharedResources.CurrentAuftrag;
            ProbeUnterList = Auftragsverwaltung.SharedResources.CurrentProbeUnterList;
        }
        
        private async void OnSubmitButtonClicked()
        {
            try
            {
                if (Auftragsverwaltung.SharedResources.Step == 5)
                {
                    if (CheckBoxPruefungBestaetigen.IsChecked.HasValue && CheckBoxPruefungBestaetigen.IsChecked.Value)
                    {
                        _auftrag.Status_nr = 5;
                        var entityInDbAuftrag = db.Set<Auftrag>().Find(_auftrag.Auf_nr);
                        db.Entry(entityInDbAuftrag).CurrentValues.SetValues(_auftrag);
                        await db.SaveChangesAsync();
                        Auftragsverwaltung.SharedResources.Step = 6;
                        Auftragsverwaltung.SharedResources.CurrentAuftrag = _auftrag;
                        AdonisUI.Controls.MessageBox.Show("Werkstoffprüfung wurde erfolgreich bestätigt!", "Speichern erfolgreich!", AdonisUI.Controls.MessageBoxButton.OK);
                        MainGrid.Background = new SolidColorBrush(Color.FromRgb(178, 255, 171));
                    }
                    else
                    {
                        AdonisUI.Controls.MessageBox.Show("Werkstoffprüfung wurde nicht bestätigt!", "Speichern nicht erfolgreich!", AdonisUI.Controls.MessageBoxButton.OK);
                    }   
                }
            }
            catch (Exception ex)
            {
                AdonisUI.Controls.MessageBox.Show($"Ein Fehler ist aufgetreten: {ex.Message}", "Fehler", AdonisUI.Controls.MessageBoxButton.OK);
            }
            
        }
    }
}