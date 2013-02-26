using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

using Renci.SshNet;
using Renci.SshNet.Common;

namespace Sehraf.RSRPC
{
    public class RSSSHConnector
    {
        const bool DEBUG = false;
        ShellStream _stream;    
        SshClient _client;
        RSRPC _parent;

        string _host;
        ushort _port;
        string _user;
        string _pw;

        public string IP { get { return _host; } }
        public int Port { get { return _port; } }
        public string User { get { return _user; } }
        public string Password { get { return _pw; } }
        public ShellStream Stream { get { return _stream; } }

        public RSSSHConnector(RSRPC parent, string IP, ushort port, string user, string pw)
        {
            _parent = parent;
            _host = IP;
            _port = port;
            _user = user;
            _pw = pw;
        }

        public bool Connect()
        {
            System.Diagnostics.Debug.WriteLineIf(DEBUG, "ssh: connecting ....");
            try
            {
                ConnectionInfo info = new PasswordConnectionInfo(_host,_port, _user, _pw);
                _client = new SshClient(info);
                _client.ErrorOccurred += SSHError;
                _client.Connect();
                _stream = _client.CreateShellStream("xterm", 80, 24, 800, 600, 1024);
                System.Diagnostics.Debug.WriteLineIf(DEBUG, "ssh: connected");
                return true;
            }
            catch (System.Exception e)
            {
                System.Diagnostics.Debug.WriteLineIf(DEBUG, "ssh: error while connecting:");
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.InnerException);
                return false;
            }
        }

        private void SSHError(object sender, ExceptionEventArgs e)
        {
            _parent.Error(e.Exception, RSRPC.ErrorFrom.SSH);   
        }

        public void Disconnect()
        {
            System.Diagnostics.Debug.WriteLineIf(DEBUG, "ssh: disconnecting ....");
            _stream.Close();
            _stream.Dispose();
            _client.Disconnect();
            _client = null;
        }

        public bool Reconnect(out ShellStream stream)
        {
            System.Diagnostics.Debug.WriteLineIf(DEBUG, "ssh: reconnecting ....");
            Disconnect();
            System.Threading.Thread.Sleep(500);
            if (Connect())
            {
                stream = _stream;
                return true;
            }
            else
            {
                stream = null;
                return false;
            }
        }
    }
}
