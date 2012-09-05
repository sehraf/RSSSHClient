using System;
using System.Collections.Generic;
using System.IO;

namespace RetroShareSSHClient
{
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

        bool _enableAutoResp;

        public bool EnableAutoResp { get { return _enableAutoResp; } set { _enableAutoResp = value; } }
    }

    class Settings
    {
        string _filename = "settings.txt";

        public Settings()
        {

        }

        public bool Load(out Options opt)
        {
            opt = new Options();
            if (File.Exists(_filename))
            {
                try
                {
                    FileStream fs = new FileStream(_filename, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);

                    opt.Host = sr.ReadLine();
                    opt.Port = sr.ReadLine();
                    opt.User = sr.ReadLine();
                    opt.Password = DecodeFrom64(sr.ReadLine());
                    opt.SaveSettings = (sr.ReadLine() == "1") ? true : false;
                    opt.SavePW = (sr.ReadLine() == "1") ? true : false;

                    opt.EnableAutoResp = (sr.ReadLine() == "1") ? true : false;

                    sr.Close();
                    sr.Dispose();
                    fs.Close();
                    fs.Dispose();
                    return true;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    return false;
                }
            }
            else
                return false;
        }

        public void Save(Options opt)
        {
            if (!File.Exists(_filename))
                File.Create(_filename);

            try
            {
                FileStream fs = new FileStream(_filename, FileMode.Truncate);
                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine(opt.Host);
                sw.WriteLine(opt.Port);
                sw.WriteLine(opt.User);
                sw.WriteLine(EncodeTo64(opt.Password));
                sw.WriteLine(opt.SaveSettings ? "1" : "0");
                sw.WriteLine(opt.SavePW ? "1" : "0");

                sw.WriteLine(opt.EnableAutoResp ? "1" : "0");

                sw.Flush();
                sw.Close();
                sw.Dispose();

                fs.Flush();
                fs.Close();
                fs.Dispose();
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
