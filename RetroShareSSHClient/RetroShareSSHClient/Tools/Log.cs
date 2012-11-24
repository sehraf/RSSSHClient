using System;
using System.IO;

namespace RetroShareSSHClient
{
    class Log
    {
        string _filename = "log.txt";

        public Log() { }

        public void ClearLog()
        {
            if (File.Exists(_filename))
                File.Delete(_filename);
        }

        public void NewSession()
        {
            if (!File.Exists(_filename))
                File.Create(_filename);

            try
            {
                FileStream fs = new FileStream(_filename, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine("+++++++++++++++++++++++++++++++++++ " + System.DateTime.Now.ToShortDateString() + " +++++++++++++++++++++++++++++++++++");
                sw.WriteLine("new log");

                sw.Flush();
                sw.Close();
                sw.Dispose();

                //fs.Flush();
                fs.Close();
                fs.Dispose();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        public void AddError(Exception e)
        {
            if (!File.Exists(_filename))
                File.Create(_filename);

            try
            {
                FileStream fs = new FileStream(_filename, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine("----------------------------------- " + System.DateTime.Now.ToShortTimeString() + " -----------------------------------");
                sw.WriteLine(e.Message);
                sw.WriteLine(e.StackTrace);

                sw.Flush();
                sw.Close();
                sw.Dispose();

                fs.Flush();
                fs.Close();
                fs.Dispose();
            }
            catch (Exception e2)
            {
                System.Diagnostics.Debug.WriteLine(e2.Message);
            }
        }
    }
}
