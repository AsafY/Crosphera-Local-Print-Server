using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace printServer.actions
{
    class Print : Action
    {
        public override void Run(HttpListenerRequest request, HttpListenerResponse response, Form1 frm)
        {
            string data;
            using (Stream receiveStream = request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    data = readStream.ReadToEnd();
                }
            }

            string printerName = request.QueryString["printerName"];
            Console.WriteLine("Print requested from {0}", printerName);
            frm.ShowNotification(String.Format("Print requested from {0}", printerName), "print");


            SilentPrint.Print(printerName, data);
        }
    }
}
