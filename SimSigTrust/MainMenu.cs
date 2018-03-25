using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SimSigTrust
{
    public partial class MainMenu : Form
    {

        public static TcpClient Connection = new TcpClient();
        public string trustString;

        public static event EventHandler<MsgEventArgs> DebugTcpDataReceived;
        public static event EventHandler<MsgEventArgs> KeyboardTcpDataReceived;



        public MainMenu()
        {
            InitializeComponent();
            
           Connection.DataReceived += TcpDataUpdate;
        }

        private void TcpDataUpdate(Object sender, MsgEventArgs e)
        {
            string element = e.Msg;


            if (element != null && InvokeRequired)
                try
                {
                    {
                        MsgEventArgs m = new MsgEventArgs() { Msg = element };

                        var handler = DebugTcpDataReceived;
                        if (handler != null) DebugTcpDataReceived?.Invoke(this, m);
                    }
                    if (element.Length > 3)
                    {
                        element = element.Remove(0, 3);
                    }
                    if (element.Contains("<platformDataResponse"))
                    {

                        string headcode = null;
                        string platform = "   ";
                        string line = "   ";
                        string path = "   ";
                        string description = null;
                        string arrival = "--:-- ";
                        string departure = "--:-- ";
                        string delay = "       ";
                        string stock = null;

                        List<string> simplfierList = new List<string>();

                        XmlDocument simplifier = new XmlDocument();

                        simplifier.LoadXml(element.ToString());

                        XmlNodeList listOfHeadcodes = simplifier.SelectNodes("/SimSig/platformDataResponse/headcode");

                        foreach (XmlNode trainInSimplfier in listOfHeadcodes)
                        {
                            if (InvokeRequired)
                                Invoke(new MethodInvoker(delegate
                                {
                                    screenTrust.Items.Clear();
                                    screenTrust.Items.Add(trustString);
                                    screenTrust.Items.Add(" ");
                                    screenTrust.Items.Add("TRAIN ARR    DEP  PLT LIN PTH  DELAY");
                                }));

                            headcode = null;
                            platform = "   ";
                            line = "   ";
                            path = "   ";
                            description = null;
                            arrival = "--:-- ";
                            departure = "--:-- ";
                            delay = "       ";
                            stock = null;

                            headcode = trainInSimplfier.Attributes["id"].Value;
                            platform = trainInSimplfier.SelectSingleNode("platform").InnerText;
                            do
                            {
                                platform = platform + " ";
                            } while (platform.Length != 3);
                            line = trainInSimplfier.SelectSingleNode("line").InnerText;
                            do
                            {
                                line = line + " ";
                            } while (line.Length != 3);
                            path = trainInSimplfier.SelectSingleNode("path").InnerText;
                            do
                            {
                                path = path + " ";
                            } while (path.Length != 3);
                            description = trainInSimplfier.SelectSingleNode("description").InnerText;
                            if (trainInSimplfier.SelectSingleNode("delay") != null)
                            {
                                if (trainInSimplfier.SelectSingleNode("delay").InnerText != "RT")
                                {
                                    delay = trainInSimplfier.SelectSingleNode("delay").InnerText.Replace("L", "") + " LATE";
                                }
                                else
                                {
                                    delay = "RT TIME";
                                }
                            }
                            stock = trainInSimplfier.SelectSingleNode("stock").InnerText;

                            XmlNodeList listOfTimes = trainInSimplfier.SelectNodes("time");

                            foreach (XmlNode time in listOfTimes)
                            {
                                if (time.Attributes != null)
                                {
                                    if (time.Attributes["timeType"] != null)
                                    {
                                        if (time.Attributes["timeType"].Value == "arrival")
                                        {
                                            arrival = time.InnerText;
                                            if (arrival.Length != 6)
                                            {
                                                arrival = arrival + " ";
                                            }
                                        }
                                        else if (time.Attributes["timeType"].Value == "departure")
                                        {
                                            departure = time.InnerText;
                                            if (departure.Length != 6)
                                            {
                                                departure = departure + " ";
                                            }
                                        }
                                        else if (time.Attributes["timeType"].Value == "passing")
                                        {
                                            departure = time.InnerText;
                                            arrival = "PASS  ";
                                            departure = time.InnerText;
                                            if (departure.Length != 6)
                                            {
                                                departure = departure + " ";
                                            }
                                        }
                                    }
                                }
                            }

                            if (departure == "--:-- ")
                            {
                                departure = arrival;
                            }

                            String simplifierString = departure + " " + arrival + " " + headcode + " " + platform + " " + line + " " + path + " " + delay;
                            Console.WriteLine(simplifierString);
                            simplfierList.Add(simplifierString);

                        }

                        //The simplfier needs to be sorted
                        Console.WriteLine("*************************");

                        simplfierList.Sort();
                        foreach (string train in simplfierList)
                        {
                            //Console.WriteLine(train.ToString());

                            Console.WriteLine(train.ToString().Substring(14, 4) + train.ToString().Substring(6, 8) + train.ToString().Substring(0, 6) + train.ToString().Substring(18));
                            if (InvokeRequired)
                                Invoke(new MethodInvoker(delegate
                                {
                                    screenTrust.Items.Add(train.ToString().Substring(14, 4) + train.ToString().Substring(6, 8) + train.ToString().Substring(0, 6) + train.ToString().Substring(18));
                                }));

                        }



                    }
                }
                catch
                {
                    Console.WriteLine(@"A Unhandled String was Received - " + element);
                }
        }

            //input

        private void txtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtUserInput.Text.StartsWith("TRJA"))
                {
                    string[] z = txtUserInput.Text.Split(' ');
                    trustString = ("TRUST LineUP for " + z[1] + " at " + z[2]);


                    Connection.SendData("<?xml version=\"1.0\" encoding=\"utf-8\"?><SimSig><platformDataRequest userTag=\"1\"><id>" + z[1] + "</id><platformCodes>(all)</platformCodes><time>" + z[2] + "</time></platformDataRequest></SimSig>|");//WORK HERE
                    txtUserInput.Text = "";
                }
                else
                {
                    //do nothing
                }

                txtUserInput.Text = "";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connection.Connect("127.0.0.1", 50505);
        }
    }
}

