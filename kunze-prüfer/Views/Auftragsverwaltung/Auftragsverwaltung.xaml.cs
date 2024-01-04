using System;
using System.ComponentModel;
using System.Threading.Tasks;
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
                    CurrentView = new DBSichten.StammdatenAnlegen();
                    break;
                case 2:
                    CurrentView = new DBSichten.AuftragAnlegen();
                    break;
                case 3:
                    CurrentView = new DBSichten.Angebotbestätigung();
                    break;
                case 4:
                    CurrentView = new DBSichten.Werkstoffprüfung();
                    break;
                case 5:
                    CurrentView = new DBSichten.WerkstoffprüfungFinished();
                    break;
                case 6:
                    CurrentView = new DBSichten.Zahlungseingang();
                    break;
                case 7:
                    CurrentView = new DBSichten.AuftragErledigt();
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