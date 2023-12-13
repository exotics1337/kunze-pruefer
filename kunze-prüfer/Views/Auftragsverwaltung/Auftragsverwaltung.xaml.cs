using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace kunze_prüfer.Views.Auftragsverwaltung
{
    public partial class Auftragsverwaltung : UserControl
    {
        public static event Action SubmitButtonClicked;
        public Auftragsverwaltung()
        {
            InitializeComponent();
            DataContext = this;
            CurrentView = new DashboardDetails(0); 
            ButtonSubmit.Click += ButtonSubmit_Click;
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            SubmitButtonClicked?.Invoke();
        }


        private int _currentStep;
        private UserControl _currentView;
        private UserControl _currentStepView;
        
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
        public UserControl CurrentStepView
        {
            get { return _currentStepView; }
            set
            {
                if (_currentStepView != value)
                {
                    _currentStepView = value;
                    OnPropertyChanged(nameof(CurrentStepView));
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
                    // ...
                    break;
                case 2:
                    // ...
                    break;
                case 3:
                    // ...
                    break;
                case 4:
                    // ...
                    break;
                case 5:
                    // ...
                    break;
                case 6:
                    // ...
                    break;
            }
        }
    }
}