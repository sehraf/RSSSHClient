using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

using Renci.SshNet;

namespace Sehraf.RetroShareSSH
{
    class RSSSHConnectorNEW
    {
        MemoryStream _streamOut, _streamIn;
        ShellStream _stream;    
        Shell _shell;
        SshClient _client;

        string _host;
        ushort _port;
        string _user;
        string _pw;

        public string IP { get { return _host; } }
        public int Port { get { return _port; } }
        public string User { get { return _user; } }
        public string Password { get { return _pw; } }

        public Shell Shell { get { return _shell; } }
        public MemoryStream StreamOut { get { return _streamOut; } }
        public MemoryStream StreamIn { get { return _streamIn; } }
        public ShellStream Stream { get { return _stream; } }

        //public RSConnector() { }
        public RSSSHConnectorNEW(string IP, ushort port, string user, string pw)
        {
            _host = IP;
            _port = port;
            _user = user;
            _pw = pw;

            //_shell = new Shell();

            //_stream = new ShellStream();
            //_streamIn = new ShellStream();
            //_streamOut = new ShellStream();

            //_protobuf = new RSProtoBuf(null, null, null);
        }

        public bool Connect()
        {
            System.Diagnostics.Debug.WriteLine("connecting...");
            try
            {
                ConnectionInfo info = new PasswordConnectionInfo(_host,_port, _user, _pw);
                _client = new SshClient(info);
                _client.Connect();
                //_client.ErrorOccurred += new EventHandler<Renci.SshNet.Common.ExceptionEventArgs>(_client_ErrorOccurred);
                //_stream = _client.CreateShellStream("xterm", 80, 24, 800, 600, 1024);
                _shell = _client.CreateShell(_streamIn, _streamOut, _streamOut);
                _shell.ErrorOccurred += new EventHandler<Renci.SshNet.Common.ExceptionEventArgs>(_shell_ErrorOccurred);
                _shell.Start();
                return true;
            }
            catch (System.Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.InnerException);
                return false;
            }
        }

        void _shell_ErrorOccurred(object sender, Renci.SshNet.Common.ExceptionEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Exception.Message);
            System.Diagnostics.Debug.WriteLine(e.Exception.StackTrace);
        }

        public void Disconnect()
        {
            //_stream.Close();
            if(_shell.IsStarted)
                _shell.Stop();
            _client.Disconnect();
            
            //if (_shell != null && _shell.IsStarted)
            //    _shell.Stop();
            //_streamIn.Close();
            //_streamIn.Dispose();
            //_streamOut.Close();
            //_streamOut.Dispose();
        }

        public void Reconnect()
        {
            Disconnect();
            Connect();
        }
    }
}
