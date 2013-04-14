using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using IrcBot;
using System.Threading;

namespace BotIRC
{

    public partial class Form1 : Form
    {
        // StreamWriter is declared here so that PingSender can access it
        private StreamWriter writer;

        // Irc server to connect 
        static string SERVER = "irc.brlink.org";

        public Form1()
        {
            InitializeComponent();
        }

        private void Teste(object sender, EventArgs e)
        {
            // Irc server's port (6667 is default port)
            int PORT = 6667;
            // User information defined in RFC 2812 (Internet Relay Chat: Client Protocol) is sent to irc server 
            string USER = "USER " + txNick.Text + " 8 * :Bem-vindos";
            //I'm a C# irc bot
            // Bot's nickname
            string NICK = txNick.Text;
            // Channel to join
            string CHANNEL = "#fortaleza";

            NetworkStream stream;
            TcpClient irc;
            string inputLine;
            StreamReader reader;
            string nickname;
            try
            {
                irc = new TcpClient(SERVER, PORT);
                stream = irc.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                // Start PingSender thread
                PingSender ping = new PingSender();
                ping.PingSenderEvent += new PingSenderDelegate(ping_PingSenderEvent);
                ping.Start();
                writer.WriteLine(USER);
                writer.Flush();
                writer.WriteLine("NICK " + NICK);
                writer.Flush();
                writer.WriteLine("JOIN " + CHANNEL);
                writer.Flush();
                writer.WriteLine("/LIST");
                writer.Flush();
                while (true)
                {
                    while ((inputLine = reader.ReadLine()) != null)
                    {
                        //if (inputLine.EndsWith("JOIN :" + CHANNEL))
                        //{
                        //    // Parse nickname of person who joined the channel
                        //    nickname = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
                        //    // Welcome the nickname to channel by sending a notice
                        //    writer.WriteLine("NOTICE " + nickname + " :Oi " + nickname +
                        //    " esse canal " + CHANNEL + " é legal!");
                        //    writer.Flush();
                        //    // Sleep to prevent excess flood
                        //    Thread.Sleep(2000);
                        //}
                    }
                    // Close all streams
                    ping.PingSenderEvent -= ping_PingSenderEvent;
                    writer.Close();
                    reader.Close();
                    irc.Close();
                }
            }
            catch (Exception ex)
            {
                textBox1.AppendText(ex.ToString() + "\n");
                // Show the exception, sleep for a while and try to establish a new connection to irc server
            }
        }

        void ping_PingSenderEvent()
        {
            //string PING = "PING :";
            //    writer.WriteLine(PING + SERVER);
            //    writer.Flush();
                //textBox1.AppendText(PING + SERVER + "\n");
        }

        private void bOk_Click(object sender, EventArgs e)
        {

        }
    }
}
