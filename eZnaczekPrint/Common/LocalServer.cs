using NetFrameworkServer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace eZnaczekPrint.Common
{
    internal class LocalServer : HttpServer
    {
        private MainWindow instance;

        public LocalServer(MainWindow instance) : base("127.0.0.1", 31871)
        {
            this.instance = instance;
        }

        protected override TcpSession CreateSession()
        {
            return new Session(this);
        }


        class Session : HttpSession
        {
            public Session(HttpServer server) : base(server)
            {
            }

            protected override void OnReceivedRequest(HttpRequest request)
            {
                try
                {
                    var instance = ((LocalServer)this.Server).instance;

                    string[] parts = request.Url.Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                    string method = parts[0].Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0];

                    if (method == "envelo")
                    {
                        string data = parts[1];
                        string decoded = Encoding.UTF8.GetString(Convert.FromBase64String(data));

                        var obj = JObject.Parse(decoded);

                        instance.webHook(obj["address"].ToString(), obj["phone"].ToString());

                        SendResponseAsync(Response.MakeGetResponse
                            ("<h1>Dane wczytane. Możesz zamknąć tę kartę.</h1><script>window.close();</script>", "text/html; charset=UTF-8"));
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd podczas wczytywania danych:\n\n" + ex.ToString(), "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                SendResponseAsync(Response.MakeGetResponse("ERROR: Invalid request."));
            }
        }
    }
}
