using System;
using System.Collections.Generic;
using System.Reflection;

namespace kunze_prüfer.Models
{
    public class InvoiceCreator
    {
        public List<InvoiceElement> InvoiceElements = new List<InvoiceElement>();
        public List<InvoiceBaseElement> InvoiceBaseElements = new List<InvoiceBaseElement>();
        
        public class InvoiceBaseElement
        {
            public string Artikelname { get; set; }
            public double Artikel_menge { get; set; }
            public double Artikel_einzel_preis { get; set; }
            public double Artikel_gesamt_preis { get; set; }
            public bool Artikel_IstFreiposition { get; set; }
        }

        public class InvoiceElement
        {
            public int Rechnungs_pos { get; set; }
            public string Artikelname { get; set; }
            public double Artikel_menge { get; set; }
            public double Artikel_einzel_preis { get; set; }
            public double Artikel_gesamt_preis { get; set; }
            public string Artikel_bemerkung { get; set; }
            public bool Artikel_IstFreiposition { get; set; }
        }

        public void AddBaseElement(string artikelname, double artikelmenge, double einzelpreis,
        bool istFreiposition = false)
        {
            InvoiceBaseElements.Add(new InvoiceBaseElement{Artikelname = artikelname, Artikel_menge = artikelmenge, Artikel_einzel_preis = einzelpreis, Artikel_IstFreiposition = istFreiposition, Artikel_gesamt_preis = einzelpreis * artikelmenge});
        }
        public void AddElement(int pos, int baseElementIndex,
            string Bemerkung = "")
        {
            string artikelname = InvoiceBaseElements[baseElementIndex].Artikelname;
            double artikelmenge = InvoiceBaseElements[baseElementIndex].Artikel_menge;
            double einzelpreis = InvoiceBaseElements[baseElementIndex].Artikel_einzel_preis;
            string bemerkung = Bemerkung;
            bool istFreiposition = InvoiceBaseElements[baseElementIndex].Artikel_IstFreiposition;
            InvoiceElements.Add(new InvoiceElement { Rechnungs_pos = pos, Artikelname = artikelname, Artikel_menge  = artikelmenge, Artikel_einzel_preis = einzelpreis, Artikel_IstFreiposition = istFreiposition, Artikel_gesamt_preis = einzelpreis * artikelmenge, Artikel_bemerkung = bemerkung});
        }
        
        public void EditElement(int index, int pos, string artikelname, double artikelmenge, double einzelpreis,
            bool istFreiposition = false, string Bemerkung = "")
        {
            InvoiceElements[index].Rechnungs_pos = pos;
            InvoiceElements[index].Artikelname = artikelname;
            InvoiceElements[index].Artikel_menge = artikelmenge;
            InvoiceElements[index].Artikel_einzel_preis = einzelpreis;
            InvoiceElements[index].Artikel_IstFreiposition = istFreiposition;
            InvoiceElements[index].Artikel_bemerkung = Bemerkung;
            InvoiceElements[index].Artikel_gesamt_preis = einzelpreis * artikelmenge;
        }
        
        public double GetSum()
        {
            double sum = 0;
            foreach (var element in InvoiceElements)
            {
                sum += element.Artikel_gesamt_preis;
            }

            return sum;
        }
        
        public void ReCount()
        {
            int pos = 1;
            foreach (var element in InvoiceElements)
            {
                element.Rechnungs_pos = pos;
                pos++;
            }
        }
        
        public void EditPos(int index, int pos)
        {
            InvoiceElements[index].Rechnungs_pos = pos;
        }
        
        public void DeleteElement(int index)
        {
            InvoiceElements.RemoveAt(index);
        }
    }
}