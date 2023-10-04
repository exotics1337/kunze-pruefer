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
                MessageBox.Show("Bitte wählen Sie einen Artikel aus.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int selectedIndex = ListBox_Rechnungspos.SelectedIndex;
                ListView.Items.Add(quickInstance.RechnungsElemente[selectedIndex]);
                AdonisUI.Controls.MessageBox.Show(quickInstance.RechnungsElemente[selectedIndex].Artikelname);
            }
        }
    }
}