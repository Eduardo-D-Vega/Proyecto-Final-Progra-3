using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PlataformaEmpleo.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using IContainer = QuestPDF.Infrastructure.IContainer; //alias para evitar conflictos con System.ComponentModel.IContainer
using System.ComponentModel;
using System.Data.Common;

namespace PlataformaEmpleo.Documents
{
    public class CvDocument : IDocument
    {
        private readonly CV _cv;

        public CvDocument(CV cv)
        {
            _cv = cv;
        }

        public DocumentMetadata GetMetadata() => new DocumentMetadata();

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
            });
        }

        //encabezado
        private void ComposeHeader(IContainer container)
        {
            container.Column(col =>
            {
                //formato del nombre del candidato
                col.Item().Text(_cv.Candidato.NombreCompleto ?? string.Empty)
                    .FontSize(17)
                    .SemiBold() 
                    .AlignLeft();

                col.Item().Height(6); // Espacio pequeño entre nombre y contacto

                //la información de contacto y localidad centrado 
                col.Item().AlignCenter().Text(t =>
                {
                    t.DefaultTextStyle(x => x.FontSize(11));
                    t.Span($"Teléfono: {_cv.Candidato.Telefono}   |   ");
                    t.Span($"Localidad: {_cv.Candidato.Ciudad}");
                });
            });
        }

        //cuerpo del cv
        private void ComposeContent(IContainer container)
        {
            //espacio que separa el encabezado con el contenido
            container.PaddingTop(28).PaddingBottom(14).Column(colum =>
            {
                if (!string.IsNullOrWhiteSpace(_cv.PerfilProfesional))
                {
                    colum.Item().Text("PERFIL PROFESIONAL")
                        .FontSize(13)
                        .SemiBold();

                    //separador
                    colum.Item()
                        .PaddingTop(2)
                        .LineHorizontal(1)
                        .LineColor(Colors.Grey.Lighten2);
                    //espacio después del separador
                    colum.Item().PaddingTop(6)
                        .Text(_cv.PerfilProfesional)
                        .FontSize(11); //contenido del perfil profesional
                    colum.Item()
                        .PaddingTop(14); //separación antes de la siguiente sección
                }

                //secciones en viñetas, se usa BuildSection que acepta column descriptor
                BuildSection(colum, "HABILIDADES", _cv.Habilidades);
                BuildSection(colum, "FORMACIÓN ACADÉMICA", _cv.FormacionAcademica);
                BuildSection(colum, "CERTIFICACIONES", _cv.Certificaciones);
                BuildSection(colum, "EXPERIENCIA LABORAL", _cv.ExperienciaLaboral);
                BuildSection(colum, "IDIOMAS", _cv.Idiomas);
            });
        }

        //parte del BUILDER que 
        private void BuildSection(ColumnDescriptor col, string title, string? content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return;
            }

            //titulos de seccion
            col.Item().Text(title).FontSize(13).SemiBold();

            //linea gris
            col.Item().Height(2);
            col.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
            col.Item().Height(3);

            //se convierte el texto plano en items (aguanta las comas, saltos de línea o puntos)
            var items = content
                .Split(new[] { ',', '\n', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => x.Length > 0)
                .ToList();

            // se agregan los items con viñetas
            foreach (var item in items)
            {
                col.Item().Row(row =>
                {
                    row.ConstantItem(11)
                        .Text("•")
                        .FontSize(11);

                    row.RelativeItem(11)
                        .Text(item)
                        .FontSize(11);
                });
            }

            //es el espacio entre cada seccion
            col.Item().PaddingTop(15);
        }

        //pie de pagina
        private void ComposeFooter(IContainer container)
        {
            container.AlignCenter().Text(t =>
            {
                t.DefaultTextStyle(x => x
                    .FontSize(9)
                    .FontColor(Colors.Grey.Darken2));
                t.Span("Plataforma de Empleo - 2025");
            });
        }
    }
}

