using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace printServer
{
    class SilentPrint
    {
        public static void Print(string printerName, string html)
        {
            var th = new Thread(() =>
            {
                var br = new WebBrowser();
                br.DocumentCompleted += PrintDocument;
                br.ScriptErrorsSuppressed = true;
                //  br.Navigate(url);
                br.Tag = printerName;
                br.DocumentText = html;
                Application.Run();
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        static void PrintDocument(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var browser = sender as WebBrowser;
            // Print the document now that it is fully loaded.
            string printerName = browser.Tag as string;


            PrinterSettings settings = new PrinterSettings();
            string currentDefault = settings.PrinterName;
            string footer = SilentPrint.IESetPageSetup("footer", "");
            string header = SilentPrint.IESetPageSetup("header", "");
            string margin_top = SilentPrint.IESetPageSetup("margin_top", "0");
            string margin_bottom = SilentPrint.IESetPageSetup("margin_bottom", "0");
            string margin_right = SilentPrint.IESetPageSetup("margin_right", "0");
            string margin_left = SilentPrint.IESetPageSetup("margin_left", "0");

            SilentPrint.SetDefaultPrinter(printerName);

            browser.Print();
            SilentPrint.SetDefaultPrinter(currentDefault);
            SilentPrint.IESetPageSetup("footer", footer);
            SilentPrint.IESetPageSetup("header", header);
            SilentPrint.IESetPageSetup("margin_top", margin_top);
            SilentPrint.IESetPageSetup("margin_bottom", margin_bottom);
            SilentPrint.IESetPageSetup("margin_right", margin_right);
            SilentPrint.IESetPageSetup("margin_left", margin_left);

            // Dispose the WebBrowser now that the task is complete. 
            browser.Dispose();
        }


        public static bool SetDefaultPrinter(string defaultPrinter)
        {
            using (ManagementObjectSearcher objectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer"))
            {
                using (ManagementObjectCollection objectCollection = objectSearcher.Get())
                {
                    foreach (ManagementObject mo in objectCollection)
                    {
                        if (string.Compare(mo["Name"].ToString(), defaultPrinter, true) == 0)
                        {
                            mo.InvokeMethod("SetDefaultPrinter", null, null);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private static string IESetPageSetup(string key, string value)
        {
            string strKey = "Software\\Microsoft\\Internet Explorer\\PageSetup";
            RegistryKey oKey = Registry.CurrentUser.OpenSubKey(strKey, true);
            string retValue = oKey.GetValue(key) as string;
            oKey.SetValue(key, value);
            oKey.Close();

            return retValue;
        }
    }
}
