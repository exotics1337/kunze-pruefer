using System.Windows;
using System.Windows.Input;

namespace kunze_prüfer.Views.QuickPDF
{
    public partial class PDFCreator : Window
    {
        private Models.InvoiceCreator invoiceInstance = new Models.InvoiceCreator();
        public PDFCreator(Models.InvoiceCreator invoiceInstance)
        {
            InitializeComponent();
            foreach (var element in invoiceInstance.InvoiceElements)
            {
                ListBox_Rechnungspos.Items.Add(element.Artikelname);
            }

            this.invoiceInstance = invoiceInstance;
        }

        private void BtnPosAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (ListBox_Rechnungspos.SelectedIndex != -1)
            {
                int selectedIndex = ListBox_Rechnungspos.SelectedIndex;
                int lastListViewIndex = ListView.Items.Count;
                if (lastListViewIndex != -1)
                {
                    invoiceInstance.EditPos(selectedIndex, lastListViewIndex + 1);
                }
                else
                {
                    invoiceInstance.EditPos(selectedIndex, 1);
                }
                ListView.Items.Add(invoiceInstance.InvoiceElements[selectedIndex]);
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
                        invoiceInstance.EditPos(currentIndex, lastListViewIndex + 1);
                    }
                    else
                    {
                        invoiceInstance.EditPos(currentIndex, 1);
                    }
                    ListView.Items.Add(invoiceInstance.InvoiceElements[currentIndex]);
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

        private void BtnDeletePos_OnClick(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ListBox_Rechnungspos.SelectedIndex;
            int lastListViewIndex = ListView.Items.Count;
            
            if (selectedIndex != -1)
            {
                if (lastListViewIndex != -1)
                {
                    for(int i = selectedIndex + 1; i < lastListViewIndex; i++)
                    {
                        invoiceInstance.EditPos(i, i - 1);
                    }
                    invoiceInstance.DeleteElement(selectedIndex);
                }
                else
                {
                    MessageBox.Show("Es sind keine Positionen verfügbar.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie einen Artikel aus.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}