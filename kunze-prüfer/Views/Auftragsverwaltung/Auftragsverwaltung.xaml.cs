using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using kunze_prüfer.Views.Auftragsverwaltung.DBSichten;

namespace kunze_prüfer.Views.Auftragsverwaltung
{
    public partial class Auftragsverwaltung : UserControl
    {
        public static event Action SubmitButtonClicked;
        public Auftragsverwaltung()
        {
            InitializeComponent();
            DataContext = this;
            CurrentStep = 1; // TODO: Von DB aktuellen Status abfragen
            CurrentDetailsView = new DashboardDetails(0); // TODO: Details ID basierend auf aktuellem Auftrag abfragen, aber auch erst wenn der Auftrag existiert
            StepHandler(CurrentStep);
            ButtonSubmit.Click += ButtonSubmit_Click;
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            SubmitButtonClicked?.Invoke();
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
                if (value < 7 && value > 0)
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
            switch (step)
            {
                case 1:
                    CurrentView = new StammdatenAnlegen();
                    break;
                case 2:
                    CurrentView = new AuftragAnlegen();
                    break;
                case 3:
                    CurrentView = new Angebotbestätigung();
                    break;
                case 4:
                    CurrentView = new Werkstoffprüfung();
                    break;
                case 5:
                    CurrentView = new WerkstoffprüfungFinished();
                    break;
                case 6:
                    CurrentView = new Zahlungseingang();
                    break;
                case 7:
                    CurrentView = new AuftragErledigt();
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