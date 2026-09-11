using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.Drawing;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Presentation;
using Syncfusion.PresentationRenderer;
using Syncfusion.XlsIO;
using Syncfusion.XlsIORenderer;
using Syncfusion.XPS;


using Path = System.IO.Path;
using Size = Syncfusion.Drawing.Size;
using FormatType = Syncfusion.DocIO.FormatType;

namespace ConvertAndMergeDocumentsToPdf
{
    public partial class MainForm : Form
    {

        private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".pdf",
            ".doc", ".docx", ".dot", ".dotx", ".rtf",
            ".xls", ".xlsx", ".csv",
            ".ppt", ".pptx",
            ".jpg", ".jpeg", ".png", ".bmp", ".tif", ".tiff",
            ".html", ".htm",
            ".md",
            ".xps"
        };


        private readonly List<string> _selectedFiles = new();

        public MainForm()
        {
            InitializeComponent();
        }

        private void ButtonBrowse_Click(object? sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                AddFiles(openFileDialog.FileNames);
            }
        }

        private void PanelDropArea_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void PanelDropArea_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data == null) return;

            if (e.Data.GetData(DataFormats.FileDrop) is string[] droppedFiles)
            {
                AddFiles(droppedFiles);
            }
        }

        private void AddFiles(IEnumerable<string> filePaths)
        {
            foreach (var path in filePaths)
            {
                var extension = Path.GetExtension(path);

                if (!AllowedExtensions.Contains(extension))
                {
                    MessageBox.Show(
                        $"{Path.GetFileName(path)} is not a supported file type.",
                        "Unsupported File",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    continue;
                }


                if (!_selectedFiles.Contains(path, StringComparer.OrdinalIgnoreCase))
                {
                    _selectedFiles.Add(path);
                }
            }

            RenderFileList();
        }

        private void RenderFileList()
        {
            listBoxFiles.BeginUpdate();
            listBoxFiles.Items.Clear();

            foreach (var path in _selectedFiles)
            {
                var info = new FileInfo(path);
                listBoxFiles.Items.Add($"{info.Name}  ({info.Length / 1024.0:F2} KB)");
            }

            listBoxFiles.EndUpdate();
            UpdateStatus($"{_selectedFiles.Count} file(s) selected.");
        }

        private void ButtonRemove_Click(object? sender, EventArgs e)
        {
            if (listBoxFiles.SelectedIndices.Count == 0)
            {
                MessageBox.Show(
                    "Select a file to remove.",
                    "No File Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var indices = listBoxFiles.SelectedIndices
                .Cast<int>()
                .OrderByDescending(i => i)
                .ToList();

            foreach (var index in indices)
            {
                _selectedFiles.RemoveAt(index);
            }

            RenderFileList();
        }

        private async void ButtonGenerate_Click(object? sender, EventArgs e)
        {
            if (_selectedFiles.Count == 0)
            {
                MessageBox.Show(
                    "Please select at least one file.",
                    "No Files",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var outputPath = saveFileDialog.FileName;

            buttonBrowse.Enabled = false;
            buttonGenerate.Enabled = false;
            buttonRemove.Enabled = false;
            UpdateStatus("Generating merged PDF...");

            try
            {
                await Task.Run(() => MergeDocuments(_selectedFiles, outputPath));

                UpdateStatus("Done. PDF saved to " + outputPath);
                MessageBox.Show(
                    $"Merged PDF saved to:\n{outputPath}",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                UpdateStatus("Failed: " + ex.Message);
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                buttonBrowse.Enabled = true;
                buttonGenerate.Enabled = true;
                buttonRemove.Enabled = true;
            }
        }

        private void UpdateStatus(string text)
        {
            if (statusStrip.InvokeRequired)
            {
                statusStrip.BeginInvoke(() => statusLabel.Text = text);
            }
            else
            {
                statusLabel.Text = text;
            }
        }

        private void MergeDocuments(List<string> files, string outputPath)
        {

            var mergedDocument = new PdfDocument();
            var sources = new List<(Stream Stream, PdfLoadedDocument Document)>();

            try
            {
                foreach (var file in files)
                {
                    var pdfStream = ConvertToPdf(file);
                    var loadedDocument = new PdfLoadedDocument(pdfStream);
                    sources.Add((pdfStream, loadedDocument));
                    PdfDocumentBase.Merge(mergedDocument, loadedDocument);
                }

                using var outputStream = new FileStream(
                    outputPath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None);

                mergedDocument.Save(outputStream);
            }
            finally
            {
                foreach (var (stream, document) in sources)
                {
                    document.Close(true);
                    stream.Dispose();
                }
                mergedDocument.Close(true);
            }
        }

        private static Stream ConvertToPdf(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();

            return extension switch
            {
                ".pdf" => GetPdfStream(filePath),
                ".csv" => ConvertCsvToPdf(filePath),
                ".doc" or ".docx" or ".dot" or ".dotx" or ".rtf"
                    => ConvertWordToPdf(filePath),
                ".xlsx" or ".xls" => ConvertExcelToPdf(filePath),
                ".pptx" or ".ppt" => ConvertPowerPointToPdf(filePath),
                ".jpg" or ".jpeg" or ".png" or ".bmp" or ".tif" or ".tiff"
                    => ConvertImageToPdf(filePath),
                ".html" or ".htm" => ConvertHtmlToPdf(filePath),
                ".xps" => ConvertXpsToPdf(filePath),
                ".md" => ConvertMarkdownToPdf(filePath),
                _ => throw new NotSupportedException(
                    $"Unsupported file type: {extension}")
            };
        }

        private static Stream GetPdfStream(string filePath)
        {

            var ms = new MemoryStream(File.ReadAllBytes(filePath));
            ms.Position = 0;
            return ms;
        }

        private static Stream ConvertWordToPdf(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            var wordDocument = new WordDocument(stream, FormatType.Automatic);

            var renderer = new DocIORenderer();
            var pdfDocument = renderer.ConvertToPDF(wordDocument);

            var pdfStream = new MemoryStream();
            pdfDocument.Save(pdfStream);

            pdfDocument.Close(true);
            renderer.Dispose();
            wordDocument.Close();

            pdfStream.Position = 0;
            return pdfStream;
        }

        private static Stream ConvertExcelToPdf(string filePath)
        {
            var pdfStream = new MemoryStream();

            using var excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            application.DefaultVersion = ExcelVersion.Xlsx;

            using var excelStream = File.OpenRead(filePath);
            IWorkbook workbook = application.Workbooks.Open(excelStream);

            var renderer = new XlsIORenderer();
            var pdfDocument = renderer.ConvertToPDF(workbook);

            pdfDocument.Save(pdfStream);

            workbook.Close();
            pdfDocument.Close(true);

            pdfStream.Position = 0;
            return pdfStream;
        }

        private static Stream ConvertCsvToPdf(string filePath)
        {

            var pdfStream = new MemoryStream();

            using var excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            application.DefaultVersion = ExcelVersion.Xlsx;

            using var csvStream = File.OpenRead(filePath);
            IWorkbook workbook = application.Workbooks.Open(csvStream, ",");

            var renderer = new XlsIORenderer();
            var pdfDocument = renderer.ConvertToPDF(workbook);

            pdfDocument.Save(pdfStream);

            workbook.Close();
            pdfDocument.Close(true);

            pdfStream.Position = 0;
            return pdfStream;
        }

        private static Stream ConvertPowerPointToPdf(string filePath)
        {
            using var pptStream = File.OpenRead(filePath);

            IPresentation presentation = Presentation.Open(pptStream);

            var renderer = new PresentationRenderer();
            PdfDocument pdfDocument = PresentationToPdfConverter.Convert(presentation);

            var pdfStream = new MemoryStream();
            pdfDocument.Save(pdfStream);

            presentation.Close();
            pdfDocument.Close(true);

            pdfStream.Position = 0;
            return pdfStream;
        }

        private static Stream ConvertImageToPdf(string filePath)
        {

            var imageToPdfConverter = new ImageToPdfConverter
            {
                PageSize = PdfPageSize.A4,
                ImagePosition = PdfImagePosition.TopLeftCornerOfPage
            };

            using var imageStream = File.OpenRead(filePath);
            using var pdfDocument = imageToPdfConverter.Convert(imageStream);

            var pdfStream = new MemoryStream();
            pdfDocument.Save(pdfStream);
            pdfDocument.Close(true);

            pdfStream.Position = 0;
            return pdfStream;
        }

        private static Stream ConvertHtmlToPdf(string filePath)
        {
            string htmlContent = File.ReadAllText(filePath);

            var htmlConverter = new HtmlToPdfConverter();
            var settings = new BlinkConverterSettings
            {
                ViewPortSize = new Size(1280, 0)
            };
            htmlConverter.ConverterSettings = settings;

            PdfDocument document = htmlConverter.Convert(htmlContent, string.Empty);

            using var tempStream = new MemoryStream();
            document.Save(tempStream);
            byte[] pdfBytes = tempStream.ToArray();
            document.Close();

            return new MemoryStream(pdfBytes);
        }

        private static Stream ConvertXpsToPdf(string filePath)
        {
            using var xpsStream = File.OpenRead(filePath);

            var converter = new XPSToPdfConverter();
            PdfDocument document = converter.Convert(xpsStream);

            using var tempStream = new MemoryStream();
            document.Save(tempStream);
            byte[] pdfBytes = tempStream.ToArray();
            document.Close(true);

            return new MemoryStream(pdfBytes);
        }

        private static Stream ConvertMarkdownToPdf(string filePath)
        {
            using var markdownStream = File.OpenRead(filePath);

            using var wordDocument = new WordDocument(markdownStream, FormatType.Markdown);
            using var renderer = new DocIORenderer();
            using var pdfDocument = renderer.ConvertToPDF(wordDocument);

            using var tempStream = new MemoryStream();
            pdfDocument.Save(tempStream);
            byte[] pdfBytes = tempStream.ToArray();

            return new MemoryStream(pdfBytes);
        }
    }
}
