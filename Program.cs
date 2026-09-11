using System;
using System.Windows.Forms;
using Syncfusion.Licensing;

namespace ConvertAndMergeDocumentsToPdf
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY_1,YOUR LICENSE KEY_2,...");
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
