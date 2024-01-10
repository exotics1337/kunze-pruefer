using System;
using System.Collections.Generic;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using kunze_prüfer.Models;
using kunze_prüfer.Views.Stammdaten;
using QuestPDF.Fluent;
using QuestPDF.Previewer;
using Angebotsposition = kunze_prüfer.DataBase.Angebotsposition;
using Colors = QuestPDF.Helpers.Colors;
using Rechnungsposition = kunze_prüfer.DataBase.Rechnungsposition;

namespace kunze_prüfer.Views.QuickPDF
{
    public partial class PDFCreator : Window
    {
        private Models.InvoiceDataSource invoiceInstance = new Models.InvoiceDataSource();
        private bool _isAngebot;
        private int _angebotNr;
        private InvoiceDataSource model;
        private InvoiceTemplate.InvoiceKunde _IK;
        private InvoiceTemplate.AngebotModel _AM;
        private InvoiceTemplate.RechnungModel _RM;
        public PDFCreator(Models.InvoiceDataSource invoiceInstance, InvoiceTemplate.InvoiceKunde IK, int angebotNr, InvoiceTemplate.AngebotModel AM = null, InvoiceTemplate.RechnungModel RM = null, bool isAngebot = true)
        {
            InitializeComponent();
            foreach (var element in invoiceInstance.InvoiceBaseElements)
            {
                ListBox_Rechnungspos.Items.Add(element.Artikelname);
            }
            this._isAngebot = isAngebot;
            this.invoiceInstance = invoiceInstance;
            this._IK = IK;
            this._angebotNr = angebotNr;
            this._AM = AM;
            this._RM = RM;
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
            //
        }
        
        
        private void UpdateSum()
        {
            TextBlockRechnungssumme.Text = "Rechnungsumme: " + invoiceInstance.GetSum().ToString("C");
        }
        

        private void ButtonSpeichern_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isAngebot)
            {
                InvoiceTemplate.AngebotModel AM = new InvoiceTemplate.AngebotModel();
                AM.ArtikelList = new List<InvoiceTemplate.Artikel>();
                
                foreach (var el in invoiceInstance.InvoiceElements)
                {
                    InvoiceTemplate.Artikel artikel = new InvoiceTemplate.Artikel();
                    Angebotsposition pos = new Angebotsposition();
                    pos.Ang_nr = _angebotNr;
                    pos.Ang2_nr = el.Rechnungs_pos;
                    pos.Rp_name = el.Artikelname;
                    pos.Rp_menge = Convert.ToInt32(el.Artikel_menge);
                    pos.Rp_preis = Convert.ToDouble(el.Artikel_einzel_preis);
                    pos.Rp_bemerkung = el.Artikel_bemerkung;

                    artikel.Name = el.Artikelname;
                    artikel.Menge = Convert.ToInt32(el.Artikel_menge);
                    artikel.Preis = Convert.ToDouble(el.Artikel_einzel_preis);
                    artikel.Bemerkung = el.Artikel_bemerkung;
                    AM.ArtikelList.Add(artikel);
                    if (el.Artikel_IstFreiposition)
                    {
                        pos.Rp_menge = 0;
                        pos.Rp_preis = 0;
                    }
                    Auftragsverwaltung.Auftragsverwaltung.SharedResources.CurrentAngebotspositionList.Add(pos);
                }
                
                AM.AngebotNr = _angebotNr;
                AM.Kunde = _IK;
                
                
                var document = new InvoiceGenerator(AM);
                
                string path = "\\Dokumente\\Angebote\\";
                string path2 = "/Dokumente/Angebote/";
                if (!System.IO.Directory.Exists("." + path2))
                {
                    System.IO.Directory.CreateDirectory("." + path2);
                }
                if (!File.Exists(path + "Angebot-" + _angebotNr + ".pdf"))
                {
                    document.GeneratePdf("." + path2 + "Angebot-" + _angebotNr + ".pdf");
                }
            }
            else
            {
                InvoiceTemplate.RechnungModel RM = new InvoiceTemplate.RechnungModel();
                RM.ArtikelList = new List<InvoiceTemplate.Artikel>();
                
                foreach (var el in invoiceInstance.InvoiceElements)
                {
                    InvoiceTemplate.Artikel artikel = new InvoiceTemplate.Artikel();
                    Rechnungsposition pos = new Rechnungsposition();
                    pos.r_nr = _angebotNr;
                    pos.r2_nr = el.Rechnungs_pos;
                    pos.Rp_name = el.Artikelname;
                    pos.Rp_menge = Convert.ToInt32(el.Artikel_menge);
                    pos.Rp_preis = Convert.ToDouble(el.Artikel_einzel_preis);
                    pos.Rp_bemerkung = el.Artikel_bemerkung;

                    artikel.Name = el.Artikelname;
                    artikel.Menge = Convert.ToInt32(el.Artikel_menge);
                    artikel.Preis = Convert.ToDouble(el.Artikel_einzel_preis);
                    artikel.Bemerkung = el.Artikel_bemerkung;
                    RM.ArtikelList.Add(artikel);
                    if (el.Artikel_IstFreiposition)
                    {
                        pos.Rp_menge = 0;
                        pos.Rp_preis = 0;
                    }
                    Auftragsverwaltung.Auftragsverwaltung.SharedResources.CurrentRechnungspositionList.Add(pos);
                }

                RM.RechnungNr = _angebotNr;
                RM.Kunde = _IK;

                var document = new InvoiceGenerator(null, RM);

                string path = "\\Dokumente\\Rechnungen\\";
                string path2 = "/Dokumente/Rechnungen/";
                if (!System.IO.Directory.Exists("." + path2))
                {
                    System.IO.Directory.CreateDirectory("." + path2);
                }
                if (!File.Exists(path + "Rechnung-" + _angebotNr + ".pdf"))
                {
                    document.GeneratePdf("." + path2 + "Rechnung-" + _angebotNr + ".pdf");
                }
            }
            this.Close();
        }

        private void ButtonPrint_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isAngebot)
            {
                InvoiceTemplate.AngebotModel AM = new InvoiceTemplate.AngebotModel();
                AM.ArtikelList = new List<InvoiceTemplate.Artikel>();
                
                foreach (var el in invoiceInstance.InvoiceElements)
                {
                    InvoiceTemplate.Artikel artikel = new InvoiceTemplate.Artikel();
                    Angebotsposition pos = new Angebotsposition();
                    pos.Ang_nr = _angebotNr;
                    pos.Ang2_nr = el.Rechnungs_pos;
                    pos.Rp_name = el.Artikelname;
                    pos.Rp_menge = Convert.ToInt32(el.Artikel_menge);
                    pos.Rp_preis = Convert.ToDouble(el.Artikel_einzel_preis);
                    pos.Rp_bemerkung = el.Artikel_bemerkung;

                    artikel.Name = el.Artikelname;
                    artikel.Menge = Convert.ToInt32(el.Artikel_menge);
                    artikel.Preis = Convert.ToDouble(el.Artikel_einzel_preis);
                    artikel.Bemerkung = el.Artikel_bemerkung;
                    AM.ArtikelList.Add(artikel);
                    if (el.Artikel_IstFreiposition)
                    {
                        pos.Rp_menge = 0;
                        pos.Rp_preis = 0;
                    }
                    Auftragsverwaltung.Auftragsverwaltung.SharedResources.CurrentAngebotspositionList.Add(pos);
                }
                
                AM.AngebotNr = _angebotNr;
                AM.Kunde = _IK;
                
                
                var document = new InvoiceGenerator(AM);
                
                string path = "\\Dokumente\\Angebote\\";
                string path2 = "/Dokumente/Angebote/";
                if (!System.IO.Directory.Exists("." + path2))
                {
                    System.IO.Directory.CreateDirectory("." + path2);
                }

                if (!File.Exists(path + "Angebot-" + _angebotNr + ".pdf"))
                {
                    document.GeneratePdf("." + path2 + "Angebot-" + _angebotNr + ".pdf");
                    System.Diagnostics.Process.Start(Environment.CurrentDirectory + path + "Angebot-" + _angebotNr + ".pdf");
                }
                else
                {
                    System.Diagnostics.Process.Start(Environment.CurrentDirectory + path + "Angebot-" + _angebotNr + ".pdf");
                }
            }
            else
            {
                InvoiceTemplate.RechnungModel RM = new InvoiceTemplate.RechnungModel();
                RM.ArtikelList = new List<InvoiceTemplate.Artikel>();
                
                foreach (var el in invoiceInstance.InvoiceElements)
                {
                    InvoiceTemplate.Artikel artikel = new InvoiceTemplate.Artikel();
                    Rechnungsposition pos = new Rechnungsposition();
                    pos.r_nr = _angebotNr;
                    pos.r2_nr = el.Rechnungs_pos;
                    pos.Rp_name = el.Artikelname;
                    pos.Rp_menge = Convert.ToInt32(el.Artikel_menge);
                    pos.Rp_preis = Convert.ToDouble(el.Artikel_einzel_preis);
                    pos.Rp_bemerkung = el.Artikel_bemerkung;

                    artikel.Name = el.Artikelname;
                    artikel.Menge = Convert.ToInt32(el.Artikel_menge);
                    artikel.Preis = Convert.ToDouble(el.Artikel_einzel_preis);
                    artikel.Bemerkung = el.Artikel_bemerkung;
                    RM.ArtikelList.Add(artikel);
                    if (el.Artikel_IstFreiposition)
                    {
                        pos.Rp_menge = 0;
                        pos.Rp_preis = 0;
                    }
                    Auftragsverwaltung.Auftragsverwaltung.SharedResources.CurrentRechnungspositionList.Add(pos);
                }

                RM.RechnungNr = _angebotNr;
                RM.Kunde = _IK;

                var document = new InvoiceGenerator(null, RM);

                string path = "\\Dokumente\\Rechnungen\\";
                string path2 = "/Dokumente/Rechnungen/";
                if (!System.IO.Directory.Exists("." + path2))
                {
                    System.IO.Directory.CreateDirectory("." + path2);
                }

                if (!File.Exists(path + "Rechnung-" + _angebotNr + ".pdf"))
                {
                    document.GeneratePdf("." + path2 + "Rechnung-" + _angebotNr + ".pdf");
                    System.Diagnostics.Process.Start(Environment.CurrentDirectory + path + "Rechnung-" + _angebotNr + ".pdf");
                }
                else
                {
                    System.Diagnostics.Process.Start(Environment.CurrentDirectory + path + "Rechnung-" + _angebotNr + ".pdf"); 
                }
            }
        }
    }
}