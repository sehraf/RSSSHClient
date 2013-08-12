using System;
using System.Threading;
using System.Collections.Generic;

using Meebey.SmartIrc4net;

namespace RetroShareSSHClient
{
    class IRCProcessor
    {
        Bridge _b;
        IrcClient _client;

        string _botName = "RSIRCB";
        string _botNameFull = "Retroshare - IRC Bridge";
        string _channel = "#retroshare";
        string _hostname = "irc.freenode.net";
        ushort _port = 6667;

        Thread _ircThread;

        public IRCProcessor()
        {
            _b = Bridge.GetBridge();
            _client = new IrcClient();
            _client.Encoding = System.Text.Encoding.UTF8;
            _client.SendDelay = 200;
            _client.ActiveChannelSyncing = true;

            _client.OnQueryMessage += _client_OnQueryMessage;
            _client.OnChannelMessage += _client_OnChannelMessage;
            _client.OnRawMessage += _client_OnRawMessage;

            _client.OnError += _client_OnError;
        }


        internal void Reset()
        {

        }

        internal void starte()
        {
            if (!connect())
                return;

            try
            {
                _client.Login(_botName, _botNameFull);
                _client.RfcJoin(_channel);

                _client.SendMessage(SendType.Notice, _channel, "Linking RS internal chat to this channel...");
                _b.GUI.SendToChatFromIRCFromThread("Linking " + _channel + " to this lobby...");

                _ircThread = new Thread(new ThreadStart(ircChatThread));
                _ircThread.Name = "IRC Thread";
                _ircThread.Start();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        internal void stop()
        {
            _ircThread.Abort();
            disconnect();
        }
        
        private bool connect()
        {
            try
            {
                _client.Connect(_hostname , _port);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        private void disconnect()
        {
            try
            {
                _client.Disconnect();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        private void ircChatThread()
        {
            _client.Listen();
        }

        public void WriteMsg(string msg)
        {
            _client.SendMessage(SendType.Message, _channel, msg);
        }

        void _client_OnError(object sender, ErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.ErrorMessage);
        }

        void _client_OnRawMessage(object sender, IrcEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("_client_OnRawMessage - " + e.Data.Message);
        }

        void _client_OnQueryMessage(object sender, IrcEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("_client_OnQueryMessage - " + e.Data.Message);
        }

        void _client_OnChannelMessage(object sender, IrcEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("_client_OnChannelMessage - " + e.Data.Message);
            _b.GUI.SendToChatFromIRCFromThread(e.Data.Nick + ": " + e.Data.Message);
        }

    }
}
