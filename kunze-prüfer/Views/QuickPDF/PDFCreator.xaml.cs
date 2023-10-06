using System.Windows;
using System.Windows.Input;
using QuickPDF = kunze_prüfer.Models.QuickPDF;

namespace kunze_prüfer.Views.QuickPDF
{
    public partial class PDFCreator : Window
    {
        private Models.QuickPDF quickInstance = new Models.QuickPDF();
        public PDFCreator(Models.QuickPDF quickInstance)
        {
            InitializeComponent();
            foreach (var element in quickInstance.RechnungsElemente)
            {
                ListBox_Rechnungspos.Items.Add(element.Artikelname);
            }

            this.quickInstance = quickInstance;
        }

        private void BtnPosAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (ListBox_Rechnungspos.SelectedIndex != -1)
            {
                int selectedIndex = ListBox_Rechnungspos.SelectedIndex;
                int lastListViewIndex = ListView.Items.Count;
                if (lastListViewIndex != -1)
                {
                    quickInstance.EditPos(selectedIndex, lastListViewIndex + 1);
                }
                else
                {
                    quickInstance.EditPos(selectedIndex, 1);
                }
                ListView.Items.Add(quickInstance.RechnungsElemente[selectedIndex]);
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie einen Artikel aus.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnPosAddAll_OnClick(object sender, RoutedEventArgs e)
        {
            if (ListBox_Rechnungspos.Items.Count != -1)
            {
                int currentIndex = 0;
                foreach (var item in ListBox_Rechnungspos.Items)
                {
                    int lastListViewIndex = ListView.Items.Count;
                    if (lastListViewIndex != -1)
                    {
                        quickInstance.EditPos(currentIndex, lastListViewIndex + 1);
                    }
                    else
                    {
                        quickInstance.EditPos(currentIndex, 1);
                    }
                    ListView.Items.Add(quickInstance.RechnungsElemente[currentIndex]);
                    currentIndex++;
                }
            }
            else
            {
                MessageBox.Show("Es sind keine Positionen verfügbar.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}