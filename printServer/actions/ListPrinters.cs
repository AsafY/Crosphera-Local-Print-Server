using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace printServer.actions
{
    class ListPrinters : Action
    {
        public override void Run(HttpListenerRequest request, HttpListenerResponse response, Form1 frm)
        {
            List<string> printers = new List<string>();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                printers.Add(printer);
            }

            string responseString = JsonConvert.SerializeObject(printers);
            response.ContentType = "Application/json";
            WriteToResponseStream(response, responseString);
        }
    }
}
