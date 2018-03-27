using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace SimSigTrust
{
    public sealed partial class TcpClient : IDisposable
    {
        // Called by producers to send data over the socket.
        public void SendData(string data)
        {
            _sender.SendData(data);
        }

        // Consumers register to receive data.
        public event EventHandler<MsgEventArgs> DataReceived;

        public TcpClient()
        {

        }

        private void OnDataReceived(object sender, MsgEventArgs e)
        {
            var handler = DataReceived;
            if (handler != null) DataReceived?.Invoke(this, e); // re-raise event
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Connect(string ipAddress, int port) //, string nick, string simulation, string simVersion, string loaderVersion)
        {
            try
            {
                _client = new System.Net.Sockets.TcpClient();

                Console.WriteLine(@"Connecting ...");
                _client.Connect(ipAddress, port);
                Console.WriteLine(@"Connected");


                _stream = _client.GetStream();

                _receiver = new Receiver(_stream);
                _sender = new Sender(_stream);

                _receiver.DataReceived += OnDataReceived;

                SendData("iA" +
                         //Settings.Default.clientName + "C" +
                         //Settings.Default.simVersion + "/" +
                         //Settings.Default.loadverVersion + "/" +
                         //Settings.Default.simulation + "|");
                         "TRST" + "C" + "1.0.17" + "/" + "4.6.12.0" + "/" + "cardiff" + "|");
            }
            catch (Exception f) { Console.WriteLine(f); }



        }

        public void Disconnect()
        {
            _receiver.Close();
            _stream.Close();
            _client.Close();
        }

        private System.Net.Sockets.TcpClient _client;
        private NetworkStream _stream;
        private Receiver _receiver;
        private Sender _sender;
    }

    public class MsgEventArgs : EventArgs
    {
        public string Msg { get; set; }
    }





}
