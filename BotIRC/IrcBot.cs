using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading; 
/* 
* This program establishes a connection to irc server, joins a channel and greets every nickname that
* joins the channel.
*
* Coded by Pasi Havia 17.11.2001 http://koti.mbnet.fi/~curupted
*/
namespace IrcBot
{

    /*
    * Class that sends PING to irc server every 15 seconds
    */
    delegate void PingSenderDelegate();
    class PingSender
    {
        public event PingSenderDelegate PingSenderEvent;
        private Thread pingSender;
        // Empty constructor makes instance of Thread
        public PingSender()
        {
            pingSender = new Thread(new ThreadStart(this.Run));
        }
        // Starts the thread
        public void Start()
        {
            pingSender.Start();
        }
        // Send PING to irc server every 15 seconds
        public void Run()
        {
            while (true)
            {
                if (PingSenderEvent != null) PingSenderEvent();
                Thread.Sleep(15000);
            }
        }
    }
}
