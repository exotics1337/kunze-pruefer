﻿using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using kunze_prüfer.Models;
using QuestPDF.Fluent;
using QuestPDF.Previewer;
using Colors = QuestPDF.Helpers.Colors;

namespace kunze_prüfer.Views.QuickPDF
{
    public partial class PDFCreator : Window
    {
        private Models.InvoiceDataSource invoiceInstance = new Models.InvoiceDataSource();
        public PDFCreator(Models.InvoiceDataSource invoiceInstance)
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
              ListView.Items.Add(invoiceInstance.InvoiceElements[ListView.Items.Count]);
              UpdateSum();
              RebuildListView();
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
                foreach(var element in ListBox_Rechnungspos.Items)
                {
                    invoiceInstance.AddElement(ListView.Items.Count + 1, ListBox_Rechnungspos.Items.IndexOf(element));
                    ListView.Items.Add(invoiceInstance.InvoiceElements[ListView.Items.Count]);
                }
                UpdateSum();              
                RebuildListView();
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
            if (ListView.SelectedIndex != -1)
            {
                invoiceInstance.DeleteElement(ListView.SelectedIndex);
                invoiceInstance.ReCount();
                RebuildListView();
                UpdateSum();
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie eine Position aus.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void RebuildListView()
        {
            ListView.Items.Clear();
            foreach (var element in invoiceInstance.InvoiceElements)
            {
                ListView.Items.Add(element);
            }

            for (int i = 0; i < ListView.Items.Count; i++)
            {
                if (invoiceInstance.InvoiceElements[i].Artikel_IstFreiposition)
                {
                    // change to bold
                }
            }
        }
        
        private void RebuildListBox()
        {
            ListBox_Rechnungspos.Items.Clear();
            foreach (var element in invoiceInstance.InvoiceBaseElements)
            {
                ListBox_Rechnungspos.Items.Add(element.Artikelname);
            }
        }

        private void BtnAddPos_OnClick(object sender, RoutedEventArgs e)
        {
            PDFCreator_ElementEditor addElementForm = new PDFCreator_ElementEditor(false, invoiceInstance, newPosition: ListView.Items.Count + 1);
            addElementForm.ShowDialog();
            bool? dialogResult = addElementForm.DialogResult;
            if (dialogResult == true)
            {
                this.invoiceInstance = addElementForm.ResultInstance;
                RebuildListView();
                RebuildListBox();
                UpdateSum();
            }
        }

        private void BtnEditPos_OnClick(object sender, RoutedEventArgs e)
        {
            PDFCreator_ElementEditor editElementForm = new PDFCreator_ElementEditor(true, invoiceInstance, invoiceElementIndex: ListView.SelectedIndex, newPosition: ListView.SelectedIndex + 1);
            editElementForm.ShowDialog();
            bool? dialogResult = editElementForm.DialogResult;
            if (dialogResult == true)
            {
                this.invoiceInstance = editElementForm.ResultInstance;
                RebuildListView();
                RebuildListBox();
                UpdateSum();
            }
        }

        private void BtnTabellenansicht_OnClick(object sender, RoutedEventArgs e)
        {

        }
        
        
        private void UpdateSum()
        {
            TextBlockRechnungssumme.Text = "Rechnungsumme: " + invoiceInstance.GetSum().ToString("C");
        }
    }
}