using System;
using System.Collections.Generic;
using System.Reflection;

namespace kunze_prüfer.Models
{
    public class QuickPDF
    {
        public List<RechnungsElement> RechnungsElemente = new List<RechnungsElement>();
        

        public class RechnungsElement
        {
            public int Rechnungs_pos { get; set; }
            public string Artikelname { get; set; }
            public double Artikel_menge { get; set; }
            public double Artikel_einzel_preis { get; set; }
            public double Artikel_gesamt_preis { get; set; }
        }
        
        public void AddElement(int pos, string artikelname, double artikelmenge, double einzelpreis)
        {
            RechnungsElemente.Add(new RechnungsElement { Rechnungs_pos = pos, Artikelname = artikelname, Artikel_menge  = artikelmenge, Artikel_einzel_preis = einzelpreis, Artikel_gesamt_preis = einzelpreis * artikelmenge});
        }

        public void EditElement(int index, int pos, string artikelname, double artikelmenge, double einzelpreis)
        {
            RechnungsElemente[index].Rechnungs_pos = pos;
            RechnungsElemente[index].Artikelname = artikelname;
            RechnungsElemente[index].Artikel_menge = artikelmenge;
            RechnungsElemente[index].Artikel_einzel_preis = einzelpreis;
            RechnungsElemente[index].Artikel_gesamt_preis = einzelpreis * artikelmenge;
        }
        
    }
}