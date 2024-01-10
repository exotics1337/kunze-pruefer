using System;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace kunze_prüfer.Models
{
    public class InvoiceGenerator : IDocument
    {
        public InvoiceTemplate.AngebotModel AngebotModel { get; }
        public InvoiceTemplate.RechnungModel RechnungModel { get; }
        public InvoiceGenerator(InvoiceTemplate.AngebotModel model = null, InvoiceTemplate.RechnungModel model2 = null)
        {
            if (model != null)
            {
                AngebotModel = model;
            }
            else if (model2 != null)
            {
                RechnungModel = model2;
            }
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;
        
        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(50);
            
                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);

                    
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
        }

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Orange.Medium);
            
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text(AngebotModel != null ? $"Angebot #{AngebotModel.AngebotNr}" : $"Rechnung #{RechnungModel.RechnungNr}").Style(titleStyle);
                
                    column.Item().Text(text =>
                    {
                        text.Span("Datum: ").SemiBold();
                        text.Span($"{DateTime.Now.ToString("d")}");
                    });
                });

                row.ConstantItem(100).Image("./Media/kunze.png").FitArea();
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);

                column.Item().Element(ComposeTable);

                if (!string.IsNullOrWhiteSpace("Hier würden künftig Kommentare stehen...")) // austauschen für Kommentar Objekt => Prüfen ob Kommentar vorhanden
                    column.Item().PaddingTop(25).Element(ComposeComments);
            });
        }
        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);
                    columns.RelativeColumn(3);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });
            
                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Prüfung / Prüfverfahren");
                    header.Cell().Element(CellStyle).AlignRight().Text("Einzelpreis [€]");
                    header.Cell().Element(CellStyle).AlignRight().Text("Menge");
                    header.Cell().Element(CellStyle).AlignRight().Text("Gesamtpreis [€]");
                
                    IContainer CellStyle(IContainer styledContainer)
                    {
                        return styledContainer.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });
            
                // step 3
                foreach (var item in AngebotModel != null ? AngebotModel.ArtikelList : RechnungModel.ArtikelList)
                {
                    table.Cell().Element(CellStyle).Text(AngebotModel != null ? AngebotModel.ArtikelList.IndexOf(item) + 1 : RechnungModel.ArtikelList.IndexOf(item));
                    table.Cell().Element(CellStyle).Text(item.Name);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.Preis}€");
                    table.Cell().Element(CellStyle).AlignRight().Text(item.Menge);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.Preis * item.Menge}€");
                
                    IContainer CellStyle(IContainer styledContainer)
                    {
                        return styledContainer.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }

        void ComposeComments(IContainer container)
        {
            container.Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
            {
                column.Spacing(5);
                column.Item().Text("Kommentar 1").FontSize(14);
                column.Item().Text("Hier würden künftig Kommentare stehen..."); // austauschen für Kommentar Objekt
            });
        }
    }
}