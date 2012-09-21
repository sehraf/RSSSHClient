using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

using Renci.SshNet;

namespace Sehraf.RSRPC
{
    public class RSSSHConnector
    {
        ShellStream _stream;    
        SshClient _client;

        string _host;
        ushort _port;
        string _user;
        string _pw;

        public string IP { get { return _host; } }
        public int Port { get { return _port; } }
        public string User { get { return _user; } }
        public string Password { get { return _pw; } }
        public ShellStream Stream { get { return _stream; } }

        public RSSSHConnector(string IP, ushort port, string user, string pw)
        {
            _host = IP;
            _port = port;
            _user = user;
            _pw = pw;
        }

        public bool Connect()
        {
            System.Diagnostics.Debug.WriteLine("connecting...");
            try
            {
                ConnectionInfo info = new PasswordConnectionInfo(_host,_port, _user, _pw);
                _client = new SshClient(info);
                _client.Connect();
                _stream = _client.CreateShellStream("xterm", 80, 24, 800, 600, 1024);
                return true;
            }
            catch (System.Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.InnerException);
                return false;
            }
        }

        public void Disconnect()
        {
            _stream.Close();
            _stream.Dispose();
            _client.Disconnect();
            _client = null;
        }
    }
}
