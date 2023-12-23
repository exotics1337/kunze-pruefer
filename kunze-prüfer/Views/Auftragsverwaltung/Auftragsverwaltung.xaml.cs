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
            _currentStep = 1; // TODO: Von DB aktuellen Status abfragen
            CurrentDetailsView = new DashboardDetails(0);
            StepHandler(_currentStep);
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
    }
}