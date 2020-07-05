using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using MassChecker.Models;
using ReactiveSockets;
using System.Net;
using System.Threading;

namespace MassChecker.Services
{
    public static partial class Scanner
    {
        #region TCP

        private readonly static int PortServer = 8000;
        private readonly static int PortPinger = 8001;

        private static Action<string> imgDirHandler;
        private static Action<bool> connectionHandler;
        private static bool scanning = false;
        private static ReactiveClient client;

        public static void Start(string ip = "", Action<string> logger = null)
        {
            bool isTimeout = false;
            bool isSuccess = false;
            Task.Run(async delegate
            {
                await Task.Delay(10000);
                isTimeout = true;
                if (!isSuccess) connectionHandler?.Invoke(false);
            });
            Task.Run(delegate
            {
                if (string.IsNullOrEmpty(ip))
                {
                    UdpClient udpClient = new UdpClient { EnableBroadcast = true };
                    string HostName = Dns.GetHostName();
                    IPAddress[] ipaddress = Dns.GetHostAddresses(HostName);
                    foreach (IPAddress ip4 in ipaddress.Where(ipv4 => ipv4.AddressFamily == AddressFamily.InterNetwork))
                    {
                        Console.WriteLine(ip4.ToString());
                        var data = Encoding.UTF8.GetBytes("PING");
                        logger?.Invoke("Pinging broadcast: " + ip4.ToString().Substring(0, ip4.ToString().LastIndexOf('.')) + ".255");
                        udpClient.Send(data, data.Length, ip4.ToString().Substring(0, ip4.ToString().LastIndexOf('.')) + ".255", PortPinger);
                    }

                    var from = new IPEndPoint(0, 0);
                    while (true)
                    {
                        var recvBuffer = udpClient.Receive(ref from);
                        string msg = Encoding.UTF8.GetString(recvBuffer);
                        Console.WriteLine(msg);
                        if (msg.Equals("PONG"))
                        {
                            logger?.Invoke("Scanner Found");
                            break;
                        }
                    }

                    if (isTimeout) return;

                    client = new ReactiveClient(from.Address.ToString(), PortServer);
                }
                else
                {
                    client = new ReactiveClient(ip, PortServer);
                }
                
                List<byte> buffer = new List<byte>();
                client.Receiver.Subscribe(sdata =>
                {
                    buffer.Add(sdata);
                    if (buffer.Count > 7)
                    {
                        if (buffer[buffer.Count - 8] == 69 &&
                            buffer[buffer.Count - 7] == 78 &&
                            buffer[buffer.Count - 6] == 68 &&
                            buffer[buffer.Count - 5] == 79 &&
                            buffer[buffer.Count - 4] == 70 &&
                            buffer[buffer.Count - 3] == 77 &&
                            buffer[buffer.Count - 2] == 83 &&
                            buffer[buffer.Count - 1] == 71)
                        {
                            Recieved(buffer.GetRange(0, buffer.Count - 8).ToArray());
                            buffer.Clear();
                        }
                    }
                });

                client.ConnectAsync();
                client.Connected += delegate
                {
                    Send("PING");
                    isSuccess = true;
                    logger?.Invoke("Scanner Connected");
                };
                client.Disconnected += delegate { connectionHandler?.Invoke(false); };
            });
        }

        private static void Send(string msg)
        {
            if (client == null) return;
            byte[] bytes = Encoding.UTF8.GetBytes(msg + "ENDOFMSG");
            client.SendAsync(bytes);
        }

        private static void Recieved(byte[] data)
        {
            if (scanning)
            {
                scanning = false;
                string filename = GenerateTempImageName();
                File.WriteAllBytes(filename, data);
                imgDirHandler?.Invoke(filename);
            }
            connectionHandler?.Invoke(true);
        }

        private static string GenerateTempImageName()
        {
            if (!Directory.Exists(Extension.TempDir)) Directory.CreateDirectory(Extension.TempDir);
            int index = 1;
            while (File.Exists(Path.Combine(Extension.TempDir, "temp" + index.ToString("0000") + ".jpg")))
            {
                try
                {
                    Directory.Delete(Path.Combine(Extension.TempDir), true);
                    Directory.CreateDirectory(Extension.TempDir);
                    break;
                }
                catch
                {
                    index++;
                }
            }
            return Path.Combine(Extension.TempDir, "temp" + index.ToString("0000") + ".jpg");
        }

        #endregion

        public static void SetConnectionChangeHandler(Action<bool> handler)
        {
            connectionHandler = handler;
        }

        public static void SetImageScanHandler(Action<string> handler)
        {
            imgDirHandler = handler;
        }

        public static void NextPaper()
        {
            Send("NEXT");
        }

        public static void ScanPaper()
        {
            Send("SCAN");
            scanning = true;
        }
    }
}
