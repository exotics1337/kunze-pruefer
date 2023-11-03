using System.Windows;
using System.Diagnostics;
using System.Windows.Input;

namespace kunze_prüfer.Views.QuickPDF
{
    public partial class PDFCreator : Window
    {
        private Models.InvoiceCreator invoiceInstance = new Models.InvoiceCreator();
        public PDFCreator(Models.InvoiceCreator invoiceInstance)
        {
            InitializeComponent();
            foreach (var element in invoiceInstance.InvoiceBaseElements)
            {
                ListBox_Rechnungspos.Items.Add(element.Artikelname);
            }

            this.invoiceInstance = invoiceInstance;
        }

        private void BtnPosAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (ListBox_Rechnungspos.SelectedIndex != -1)
            {
              invoiceInstance.AddElement(ListView.Items.Count + 1, ListBox_Rechnungspos.SelectedIndex);
              ListView.Items.Add(invoiceInstance.InvoiceElements[ListView.Items.Count - 1]);
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
            //
        }
        
        private void RebuildListView()
        {

        }
    }
}