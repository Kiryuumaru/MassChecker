using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveSockets;

namespace MassChecker.Services
{
    public static partial class Scanner
    {
        #region TCP

        private static ReactiveClient client;

        public static Action<string> OnReceived;

        public static void Start()
        {
            client = new ReactiveClient("192.168.254.173", 8000);

            string buffer = "";
            client.Receiver.Subscribe(data =>
            {
                if (data == 10 || data == 13)
                {
                    if (!string.IsNullOrEmpty(buffer))
                    {
                        OnReceived?.Invoke(buffer);
                        Console.WriteLine(buffer);
                    }
                    buffer = "";
                }
                else
                {
                    buffer += Convert.ToChar(data);
                }
            });

            client.ConnectAsync();
            client.Connected += delegate
            {
                Send("Hellqqsqso");
            };
        }

        private static void Send(string msg)
        {
            if (client == null) return;
            byte[] bytes = Encoding.UTF8.GetBytes(msg);
            client.SendAsync(bytes);
        }

        #endregion


    }
}
