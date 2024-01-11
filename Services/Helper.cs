using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using Document = DocumentFormat.OpenXml.Wordprocessing.Document;

namespace Services;

public class Helper
{
    static void ConvertHtmlToPlainTextDocx(string html, string filePath)
    {
        // Parse HTML and extract plain text
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
        string plainText = htmlDoc.DocumentNode.InnerText;

        // Create a Wordprocessing document
        using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
        {
            MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
            mainPart.Document = new Document(new Body());
            Body body = mainPart.Document.Body;

            // Add the text to the DOCX file
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text(plainText));
        }
    }

    public static string ConvertToUrl(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            return "";
        }

        // Replace spaces with hyphens
        string urlFriendlyTitle = title.Replace(" ", "-");

        // Remove invalid characters
        urlFriendlyTitle = Regex.Replace(urlFriendlyTitle, @"[^a-zA-Z0-9\-]", "");

        // Convert to lowercase
        urlFriendlyTitle = urlFriendlyTitle.ToLower();

        return urlFriendlyTitle;
    }
}
