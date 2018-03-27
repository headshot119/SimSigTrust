using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace SimSigTrust
{
    public sealed partial class TcpClient
    {
        private sealed class Receiver
        {
            internal event EventHandler<MsgEventArgs> DataReceived;

            internal Receiver(NetworkStream stream)
            {
                _stream = stream;
                thread = new Thread(Run);
                thread.Start();
            }

            private void Run()
            {
                bool running = true;

                try
                {

                    var buffer = new byte[2048];

                    int bytesRead;

                    var charBuffer = new char[2048];

                    string temp = "";

                    while (running)
                    {
                        if ((bytesRead = _stream.Read(buffer, 0, buffer.Length)) == 0)
                        { Thread.Sleep(20); }
                        else
                        {


                            var charsRead = Encoding.ASCII.GetChars(buffer, 0, bytesRead, charBuffer, 0);

                            string msg = new string(charBuffer, 0, charsRead);
                            if (temp != "")
                            {
                                msg = temp + msg;
                                temp = "";
                            }

                            string[] receivedStrings = msg.Split('|');
                            if (charBuffer[charsRead - 1] != '|')
                            {
                                temp = receivedStrings[receivedStrings.Length - 1];
                                receivedStrings[receivedStrings.Length - 1] = null;
                            }
                            foreach (string element in receivedStrings)
                            {

                                if (element != null)
                                {
                                    MsgEventArgs m = new MsgEventArgs() { Msg = element };
                                    OnDataReceived(this, m);

                                }
                            }

                            Thread.Sleep(20);
                        }
                    }

                }
                catch (ThreadInterruptedException)
                {
                    running = false;
                }
                catch (ThreadAbortException)
                {
                    running = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
            private void OnDataReceived(object sender, MsgEventArgs e)
            {
                var handler = DataReceived;

                if (handler != null) DataReceived?.Invoke(this, e); // re-raise event



            }
            private NetworkStream _stream;
            private Thread thread;
            public void Close()
            {
                thread.Interrupt();
                if (thread.Join(TimeSpan.FromSeconds(5)))
                {
                    Console.WriteLine("Closing correctly");
                }
                else
                {
                    thread.Abort();
                    Console.WriteLine("Force Closed");
                }
            }
        }


    }


}
