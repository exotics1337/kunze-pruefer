﻿using System;
using System.Windows;
using System.Windows.Controls;
using kunze_prüfer.Models;

namespace kunze_prüfer.Views.QuickPDF
{
    public partial class PDFCreator_ElementEditor : Window
    {
        private Models.InvoiceDataSource invoiceInstance = new Models.InvoiceDataSource();
        public Models.InvoiceDataSource ResultInstance { get; private set; }
        private bool _editMode;
        public PDFCreator_ElementEditor(bool editMode, Models.InvoiceDataSource invoiceInstance,
            int invoiceElementIndex = -1, int newPosition = -1) // Optionale Parameter
        {
            InitializeComponent();
            _editMode = editMode;
            this.invoiceInstance = invoiceInstance;
            string SrcBinding;
            if (editMode && invoiceElementIndex != -1)
            {
                TextBoxPosition.Text = this.invoiceInstance.InvoiceElements[invoiceElementIndex].Rechnungs_pos.ToString();
                TextBoxPosition.IsEnabled = false;
                TextBoxName.Text = this.invoiceInstance.InvoiceElements[invoiceElementIndex].Artikelname;
                TextBoxMenge.Text = this.invoiceInstance.InvoiceElements[invoiceElementIndex].Artikel_menge.ToString();
                TextBoxEinzelBrutto.Text = this.invoiceInstance.InvoiceElements[invoiceElementIndex].Artikel_einzel_preis.ToString();
                TextBoxBemerkung.Text = this.invoiceInstance.InvoiceElements[invoiceElementIndex].Artikel_bemerkung;
                CheckBoxFreiposition.IsChecked =
                    this.invoiceInstance.InvoiceElements[invoiceElementIndex].Artikel_IstFreiposition;
                LabelLetztePosition.Visibility = Visibility.Hidden;
                
                
                BtnText.Text = "Element bearbeiten";
                //string imagePath = "Media/Icons/edit-row-52.png";
                //Uri uri = new Uri(imagePath, UriKind.Relative);
                //BitmapImage bitmapImage = new BitmapImage(uri);
                //BtnImg.Source = bitmapImage;
            }
            else
            {
                LabelLetztePosition.Content = "Letzte Position: " + (newPosition - 1);
                LabelLetztePosition.Visibility = Visibility.Visible;
                TextBoxPosition.Text = newPosition.ToString();
            }
            
        }

        private void AcceptBtn_OnClick(object sender, RoutedEventArgs e)
        {
            // Ausbauen und Fehlermeldungen richtig catchen.
            if (!_editMode)
            {
                try
                {
                    invoiceInstance.AddBaseElement(TextBoxName.Text, Convert.ToDouble(TextBoxMenge.Text), Convert.ToDouble(TextBoxEinzelBrutto.Text), CheckBoxFreiposition.IsChecked == true ? true : false);
                    invoiceInstance.AddElement(Convert.ToInt32(TextBoxPosition.Text), invoiceInstance.InvoiceBaseElements.Count - 1, TextBoxBemerkung.Text != null || TextBoxBemerkung.Text != "" ? TextBoxBemerkung.Text : "");
                    ResultInstance = invoiceInstance;
                    DialogResult = true;
                    Close();
                }
                catch
                {
                    MessageBox.Show("Bitte überprüfen Sie Ihre Eingaben.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }   
            }
            else
            {
                try
                {
                    invoiceInstance.EditElement(Convert.ToInt32(TextBoxPosition.Text) - 1, Convert.ToInt32(TextBoxPosition.Text), TextBoxName.Text, Convert.ToDouble(TextBoxMenge.Text), Convert.ToDouble(TextBoxEinzelBrutto.Text), CheckBoxFreiposition.IsChecked == true ? true : false, TextBoxBemerkung.Text != null || TextBoxBemerkung.Text != "" ? TextBoxBemerkung.Text : "");
                    ResultInstance = invoiceInstance;
                    DialogResult = true;
                    Close();
                }
                catch
                {
                    MessageBox.Show("Bitte überprüfen Sie Ihre Eingaben.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DenyBtn_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void TextBoxEinzelNetto_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBoxEinzelBrutto.Text = String.Format("{0:0.00}", NettoBrutto.ZuBrutto(Convert.ToDouble(TextBoxEinzelNetto.Text)));
                TextBoxGesamtBrutto.Text = String.Format("{0:0.00}", Convert.ToDouble(TextBoxEinzelBrutto.Text) * Convert.ToDouble(TextBoxMenge.Text));
                TextBoxEinzelMwst.Text = String.Format("{0:0.00}", Convert.ToDouble(TextBoxEinzelBrutto.Text) - Convert.ToDouble(TextBoxEinzelNetto.Text));
                TextBoxGesamtMwst.Text = String.Format("{0:0.00}", Convert.ToDouble(TextBoxEinzelMwst.Text) * Convert.ToDouble(TextBoxMenge.Text));
            }
            catch{}
        }

        private void TextBoxEinzelBrutto_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBoxEinzelNetto.Text = String.Format("{0:0.00}", NettoBrutto.ZuNetto(Convert.ToDouble(TextBoxEinzelBrutto.Text)));
                TextBoxGesamtNetto.Text = String.Format("{0:0.00}", Convert.ToDouble(TextBoxEinzelNetto.Text) * Convert.ToDouble(TextBoxMenge.Text));
                TextBoxEinzelMwst.Text = String.Format("{0:0.00}", Convert.ToDouble(TextBoxEinzelBrutto.Text) - Convert.ToDouble(TextBoxEinzelNetto.Text));
                TextBoxGesamtMwst.Text = String.Format("{0:0.00}", Convert.ToDouble(TextBoxEinzelMwst.Text) * Convert.ToDouble(TextBoxMenge.Text));
            }
            catch{}
        }

        private void CheckBoxFreiposition_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckBoxFreiposition.IsChecked == true)
            {
                TextBoxMenge.IsEnabled = false;
                TextBoxMenge.Text = "0";
                TextBoxEinzelNetto.IsEnabled = false;
                TextBoxEinzelNetto.Text = "0";
                TextBoxEinzelBrutto.IsEnabled = false;
                TextBoxEinzelBrutto.Text = "0";
            }
            else
            {
                TextBoxMenge.IsEnabled = true;
                TextBoxEinzelNetto.IsEnabled = true;
                TextBoxEinzelBrutto.IsEnabled = true;
            }
        }
    }
}