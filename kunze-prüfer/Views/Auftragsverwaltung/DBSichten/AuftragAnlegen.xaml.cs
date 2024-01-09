using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using kunze_prüfer.DataBase;
using static kunze_prüfer.Models.TextBoxChecker;

namespace kunze_prüfer.Views.Auftragsverwaltung.DBSichten
{
    public partial class AuftragAnlegen : UserControl
    {
        private DBQ db = new DBQ();
        private Auftrag _auftrag;
        private bool _isPrüfnormSelected = false;
        private Norm _norm = new Norm();
        public AuftragAnlegen()
        {
            InitializeComponent();
            _auftrag = Auftragsverwaltung.SharedResources.CurrentAuftrag;
            Auftragsverwaltung.SubmitButtonClicked += OnSubmitButtonClicked;
            Auftragsverwaltung.CurrentAuftragChanged += OnCurrentAuftragChanged;
        }
        
        private void OnCurrentAuftragChanged()
        {
            _auftrag = Auftragsverwaltung.SharedResources.CurrentAuftrag;
        }
        private async void OnSubmitButtonClicked()
        {
            if (Auftragsverwaltung.SharedResources.Step == 2)
            {
             if (AreAllTextBoxesFilled(GroupBoxNormDetails) && AreAllTextBoxesFilled(GroupBoxAuftragDetails))
            {
                if (!_isPrüfnormSelected)
                {
                    _norm = new Norm()
                    {
                        N_bez = TextBoxPrüfnormbezeichnung.Text,
                        N_nr = int.Parse(TextBoxNormnr.Text)
                    };
                }
                else
                {
                    int nnr = int.Parse(TextBoxNormnr.Text);
                    _norm = db.Set<Norm>().FirstOrDefault(n => n.N_nr == nnr);
                }
                
                if (DatePickerLiefertermin.SelectedDate.HasValue)
                {
                    _auftrag.Auf_liefertermin = DatePickerLiefertermin.SelectedDate.Value;
                }
                else
                {
                    AdonisUI.Controls.MessageBox.Show("Es wurden nicht alle notwendingen Felder ausgefüllt!", "Speichern nicht erfolgreich!", AdonisUI.Controls.MessageBoxButton.OK);
                    return;
                }
                if (!_isPrüfnormSelected)
                {
                    db.Set<Norm>().Add(_norm);
                }
                
                _auftrag.auf_bestell_nr = int.Parse(TextBoxBestellnr.Text);
                _auftrag.Auf_prüflos = int.Parse(TextBoxPrüflos.Text);
                _auftrag.n_nr = _norm.N_nr;
                _auftrag.Status_nr = 2;
                _auftrag.Auf_nr = int.Parse(TextBoxAuftragsnr.Text);
                var entityInDb = db.Set<Auftrag>().Find(int.Parse(TextBoxAuftragsnr.Text));
        
                if (entityInDb == null)
                {
                    db.Set<Auftrag>().Add(_auftrag);
                }
                else
                {
                    db.Entry(entityInDb).CurrentValues.SetValues(_auftrag);
                }
                await db.SaveChangesAsync();
                Auftragsverwaltung.SharedResources.Step = 2;
                Auftragsverwaltung.SharedResources.CurrentAuftrag = _auftrag;
                AdonisUI.Controls.MessageBox.Show("Auftrag erfolgreich erstellt / aktualisiert!", "Speichern erfolgreich!", AdonisUI.Controls.MessageBoxButton.OK);
                MainGrid.Background = new SolidColorBrush(Color.FromRgb(178, 255, 171));
            }
            else
            {
                AdonisUI.Controls.MessageBox.Show("Es wurden nicht alle notwendingen Felder ausgefüllt!", "Speichern nicht erfolgreich!", AdonisUI.Controls.MessageBoxButton.OK);
            }   
            }
        }

        private async void ButtonPrüfnormAuswählen_OnClick(object sender, RoutedEventArgs e)
        {
            SearchWindow sw = new SearchWindow(TextBoxSuchen.Text, "Norm");
            if (int.TryParse(TextBoxSuchen.Text, out int result))
            {
                // Suche mit ID implementieren
            }
            else if (TextBoxSuchen.Text != "")
            {
                await sw.SearchWithType(TextBoxSuchen.Text);
            }
            sw.ShowDialog();
            if (sw.CurrentlySelectedID != 0)
            {
                var prüfnorm = await db.GetEntityByIdAsync<Norm, int>(sw.CurrentlySelectedID);
                if (prüfnorm != null)
                {
                    _isPrüfnormSelected = true;
                    TextBlockSelectedNorm.Text = prüfnorm.N_bez;
                    TextBoxNormnr.Text = prüfnorm.N_nr.ToString();
                    TextBoxPrüfnormbezeichnung.Text = prüfnorm.N_bez;
                }
            }
        }

        private void TextBlockSelectionClearNorm_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBoxPrüfnormbezeichnung.Text = "";
            TextBoxNormnr.Text = "";
            TextBlockSelectedNorm.Text = "Keine Auswahl";
            _isPrüfnormSelected = false;
        }

        private void DatePickerLiefertermin_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var lastAuftrag = db.GetLastEntity<Auftrag, int>(a => a.Auf_nr);
            if (lastAuftrag != null)
            {
                int newAuf_nr = lastAuftrag.Auf_nr + 1;
                TextBoxAuftragsnr.Text = newAuf_nr.ToString();
            }
            else
            {
                TextBoxAuftragsnr.Text = "1";
            }
        }

        private void TextBoxPrüfnormbezeichnung_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isPrüfnormSelected)
            {
                var lastNorm = db.GetLastEntity<Norm, int>(n => n.N_nr);
                if (lastNorm == null)
                {
                    TextBoxNormnr.Text = "1";
                    return;
                }
                int newN_nr = lastNorm.N_nr + 1;
                TextBoxNormnr.Text = newN_nr.ToString();
            }
        }
    }
}