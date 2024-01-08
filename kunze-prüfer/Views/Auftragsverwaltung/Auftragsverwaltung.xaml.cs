using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using kunze_prüfer.DataBase;
using kunze_prüfer.Models;

namespace kunze_prüfer.Views.Auftragsverwaltung
{
    public partial class Auftragsverwaltung : UserControl
    {
        public static event Action SubmitButtonClicked;
        private DBQ db = new DBQ();
        public static class SharedResources
        {
            public static Auftrag CurrentAuftrag = new Auftrag();
            public static int Step = 1;
        }
        public Auftragsverwaltung(int auftragsnummer = 0) // Wert von 0 als Auftragsnummer impliziert, dass ein neuer Auftrag angelegt werden soll
        {
            InitializeComponent();
            DataContext = this;
            CurrentStep = 1;
            CurrentDetailsView = new DashboardDetails(auftragsnummer);
            StepHandler(CurrentStep);
            ButtonSubmit.Click += ButtonSubmit_Click;
            if (auftragsnummer == 0)
            {
                try
                {
                    var auftrag = db.GetLastEntity<Auftrag, int>(c => c.Auf_nr);
                    if (auftrag != null)
                    {
                        auftragsnummer = auftrag.Auf_nr + 1;
                        CurrentStep = auftrag.Status_nr;
                        TextStatus.Text = auftrag.Status.Status_bez;
                        TextAuftrag.Text = $"Auftrag #{auftragsnummer}";
                    }
                    else
                    {
                        auftragsnummer = 1;
                        CurrentStep = 1;
                        TextAuftrag.Text = "Neuer Auftrag";
                    }
                    MessageBox.Show(auftragsnummer.ToString());
                }
                catch(Exception e)
                {
                    ErrorLogger.Log(e);
                }
            }
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            SubmitButtonClicked?.Invoke();
            CurrentStep = SharedResources.Step;
        }

        private UserControl _currentDetailsView;
        private int _currentStep;
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
        public UserControl CurrentDetailsView
        {
            get { return _currentDetailsView; }
            set
            {
                if (_currentDetailsView != value)
                {
                    _currentDetailsView = value;
                    OnPropertyChanged(nameof(CurrentDetailsView));
                }
            }
        }
        
        public int CurrentStep
        {
            get { return _currentStep; }
            set
            {
                if (value <= 7 && value > 0)
                {
                    _currentStep = value;
                    OnPropertyChanged(nameof(CurrentStep));
                    StepHandler(_currentStep);
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void StepHandler(int step)
        {
            foreach (var control in ViewGrid.Children)
            {
                if (control is UserControl)
                {
                    ((UserControl)control).Visibility = Visibility.Collapsed;
                }
            }
            switch (step)
            {
                case 1:
                    viewStammdatenAnlegen.Visibility = Visibility.Visible;
                    SharedResources.Step = 1;
                    break;
                case 2:
                    viewAuftragAnlegen.Visibility = Visibility.Visible;
                    SharedResources.Step = 2;
                    break;
                case 3:
                    viewAngebotbestätigung.Visibility = Visibility.Visible;
                    SharedResources.Step = 3;
                    break;
                case 4:
                    viewWerkstoffprüfung.Visibility = Visibility.Visible;
                    SharedResources.Step = 4;
                    break;
                case 5:
                    viewWerkstoffprüfungFinished.Visibility = Visibility.Visible;
                    SharedResources.Step = 5;
                    break;
                case 6:
                    viewZahlungseingang.Visibility = Visibility.Visible;
                    SharedResources.Step = 6;
                    break;
                case 7:
                    viewAuftragErledigt.Visibility = Visibility.Visible;
                    SharedResources.Step = 7;
                    break;
                default:
                    Debug.WriteLine($"Unexpected step: {step}");
                    break;
            }
            UpdateStatusText();
        }

        private void UpdateStatusText()
        {
            switch (CurrentStep)
            {
                case 1:
                    TextStatus.Text = "Stammdaten anlegen";
                    break;
                case 2:
                    TextStatus.Text = "Auftrag anlegen";
                    break;
                case 3:
                    TextStatus.Text = "Angebotbestätigung";
                    break;
                case 4:
                    TextStatus.Text = "Werkstoffprüfung";
                    break;
                case 5:
                    TextStatus.Text = "Werkstoffprüfung abgeschlossen";
                    break;
                case 6:
                    TextStatus.Text = "Zahlungseingang";
                    break;
                case 7:
                    TextStatus.Text = "Auftrag erledigt";
                    break;
            }
        }

        private void ButtonNext_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentStep++;
        }

        private void ButtonBack_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentStep--;
        }
    }
}