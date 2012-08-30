using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

//using rsctrl.rpcbase;
//using rsctrl.peers;

using Tamir.SharpSsh;

namespace Sehraf.RetroShareSSH
{
    class RSSSHConnector
    {
        const bool DEBUG = false;
        MemoryStream _streamOut, _streamIn, _stream;    
        SshShell _shell;

        string _host;
        ushort _port;
        string _user;
        string _pw;

        public string IP { get { return _host; } }
        public int Port { get { return _port; } }
        public string User { get { return _user; } }
        public string Password { get { return _pw; } }

        public SshShell Shell { get { return _shell; } }
        public MemoryStream StreamOut { get { return _streamOut; } }
        public MemoryStream StreamIn { get { return _streamIn; } }
        public MemoryStream Stream { get { return _streamOut; } }

        //public RSConnector() { }
        public RSSSHConnector(string IP, ushort port, string user, string pw)
        {
            _host = IP;
            _port = port;
            _user = user;
            _pw = pw;

            _shell = new SshShell(_host, _user, _pw);

            _stream = new MemoryStream();
            _streamIn = new MemoryStream();
            _streamOut = new MemoryStream();

            //_protobuf = new RSProtoBuf(null, null, null);
        }

        public bool Connect()
        {
            System.Diagnostics.Debug.WriteLineIf(DEBUG, "connecting...");
            try
            {
                _shell.Connect(_port);
                _shell.SetStream(_streamIn, _streamOut);
                //_shell.SetStream(_stream);

                return true;
            }
            catch (System.Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }
        }

        public void Disconnect()
        {
            if(_shell != null &&_shell.ShellConnected && _shell.ShellOpened)
                _shell.Close();
            _streamIn.Close();
            _streamIn.Dispose();
            _streamOut.Close();
            _streamOut.Dispose();
        }

        public void Reconnect()
        {
            Disconnect();
            Connect();
        }
    }
}
