using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace printServer.actions
{
    class Ping : Action
    {
        public override void Run(HttpListenerRequest request, HttpListenerResponse response, Form1 frm)
        {
            WriteToResponseStream(response, "PONG");
        }
    }
}
