using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace printServer.actions
{
    class Action
    {
        protected static void WriteToResponseStream(HttpListenerResponse response, string data)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(data);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
        }

        public virtual void Run(HttpListenerRequest request, HttpListenerResponse response, Form1 frm) { throw new NotImplementedException(); }
    }
}
