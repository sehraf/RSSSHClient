using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;


namespace RetroShareSSHClient
{
    [Serializable()]
    struct Options
    {
        string _host;
        string _port;
        string _user;
        string _pw;
        bool _saveSettings;
        bool _savePW;

        public string Host { get { return _host; } set { _host = value; } }
        public string Port { get { return _port; } set { _port = value; } }
        public string User { get { return _user; } set { _user = value; } }
        public string Password { get { return _pw; } set { _pw = value; } }
        public bool SaveSettings { get { return _saveSettings; } set { _saveSettings = value; } }
        public bool SavePW { get { return _savePW; } set { _savePW = value; } }

        string _nickname;
        //string _chatAutoRespSearch;
        //string _chatAutoRespAnswer;
        bool _enableAutoResp;
        bool _saveChat;
        byte _readSpeedIndex;

        public string Nick { get { return _nickname; } set { _nickname = value; } }
        //public string AutoRespSearch { get { return _chatAutoRespSearch; } set { _chatAutoRespSearch = value; } }
        //public string AutoRespAnswer { get { return _chatAutoRespAnswer; } set { _chatAutoRespAnswer = value; } }
        public bool EnableAutoResp { get { return _enableAutoResp; } set { _enableAutoResp = value; } }
        public bool SaveChat { get { return _saveChat; } set { _saveChat = value; } }
        public byte ReadSpeedIndex { get { return _readSpeedIndex; } set { _readSpeedIndex = value; } }

        AutoResponseItem[] _autoResponseList;

        public AutoResponseItem[] AutoResponseList { get { return _autoResponseList; } set { _autoResponseList = value; } }
    }

    class LoadSaveHandler
    {
        string _filename = Settings.SettingsFileName;

        public LoadSaveHandler(string filename = null)
        {
            if (filename != null)
                _filename = filename;
        }

        public bool Load(out Options opt)
        {
            opt = new Options();
            if (!File.Exists(_filename)) return false;

            try
            {
                MemoryStream memory = new MemoryStream();
                using (FileStream fStream = new FileStream(_filename, FileMode.Open))
                using (GZipStream gStream = new GZipStream(fStream, CompressionMode.Decompress))
                    gStream.CopyTo(memory);

                memory.Position = 0;
                BinaryFormatter binary = new BinaryFormatter();
                opt = (Options)binary.Deserialize(memory);

                // tmp fix to avoid answering to old msgs (which were saved by the server)
                //opt.EnableAutoResp = false;

                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }

        }

        public void Save(Options opt)
        {
            if (!File.Exists(_filename))
                File.Create(_filename);

            try
            {
                MemoryStream memory = new MemoryStream();
                BinaryFormatter binary = new BinaryFormatter();

                binary.Serialize(memory, opt);
                memory.Position = 0;

                using (FileStream fStream = new FileStream(_filename, FileMode.Create))
                using (GZipStream gStream = new GZipStream(fStream, CompressionMode.Compress))
                    memory.CopyTo(gStream);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        static public string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            return System.Convert.ToBase64String(toEncodeAsBytes);
        }

        static public string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            return System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
        }
    }
}
