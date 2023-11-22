using QuestPDF.Infrastructure;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace kunze_prüfer.Models
{
    public interface IDocument
    {
        DocumentMetadata GetMetadata();
        DocumentSettings GetSettings();
        void Compose(IDocumentContainer container);
    }

    public class InvoiceGenerator : IDocument
    {
        public InvoiceTemplate.AngebotModel Model { get; }
        public InvoiceGenerator(InvoiceTemplate.AngebotModel model)
        {
            Model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(50);
                    page.Header().Height(100).Background(Colors.Grey.Lighten1);
                    page.Content().Background(Colors.Grey.Lighten3);
                    page.Footer().Height(50).Background(Colors.Grey.Lighten1);
                });
        }
    }
}