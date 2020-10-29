using Microsoft.Win32;
using Newtonsoft.Json;
using printServer.actions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace printServer
{
    class Program
    {
        static Form1 _frm;
        static Dictionary<string, actions.Action> factory = new Dictionary<string, actions.Action>();

        [STAThread]
        static void Main(string[] args)
        {
            factory.Add("print", new Print());
            factory.Add("ping", new Ping());
            factory.Add("list", new ListPrinters());



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Program._frm = new Form1();

            var th = new Thread(() =>
            {
                Program.SimpleListenerExample(new string[] { "http://+:17080/" }, Program._frm);
                Application.Run();
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

            Application.Run(Program._frm);
        }

        public static void SimpleListenerExample(string[] prefixes, Form1 frm)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            // URI prefixes are required,
            // for example "http://contoso.com:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            Console.WriteLine("Crosphera local print server is Listening...");
            // Note: The GetContext method blocks while waiting for a request.
            while (true)
            {
                //  ThreadPool.QueueUserWorkItem(Process, listener.GetContext());
                object ctx = listener.GetContext();
                Process(ctx);
            }

            //  listener.Stop();

            void Process(object o)
            {
                HttpListenerContext context = o as HttpListenerContext;
                HttpListenerRequest request = context.Request;
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                // Construct a response.

                response.Headers.Add("Access-Control-Allow-Origin", "*");
                response.Headers.Add("Access-Control-Allow-Methods", "POST, GET");

                string op = request.QueryString["op"];
                if (!String.IsNullOrEmpty(op))
                {
                    if (factory.TryGetValue(op, out actions.Action action))
                    {
                        action.Run(request, response, Program._frm);
                    }
                }

                response.OutputStream.Close();
            }

        }
    }
}