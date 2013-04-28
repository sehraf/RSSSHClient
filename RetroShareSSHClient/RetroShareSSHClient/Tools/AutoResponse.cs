using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using rsctrl.core;
using rsctrl.chat;

namespace RetroShareSSHClient
{
    class AutoResponse
    {
        Dictionary<string, AutoResponseItem> _items;
        Bridge _b;
        const bool DEBUG = false;

        //public delegate string ResponseFunctionDelegate();
        public delegate string ResponseFunctionDelegate(string s);

        public AutoResponseItem[] AutoResponseList
        {
            get { return _items.Values.ToArray<AutoResponseItem>(); }
            set
            {
                if (value == null) 
                    return;

                Array.Sort(value, new AutoResponseItemComparer());

                foreach (AutoResponseItem item in value)
                {
                    if (!_items.ContainsKey(item.Name))
                        _items.Add(item.Name, item);
                }

                UpdateARList();
            }
        }

        public AutoResponse()
        {
            _items = new Dictionary<string, AutoResponseItem>();
            _b = Bridge.GetBridge();
        }

        /// <summary>
        /// Adds /time, /date and /manual command
        /// </summary>
        private void SetupBasicItems()
        {
            string name;
            // add /time command
            name = "time";
            if (!_items.Keys.Contains<string>(name))
            {
                ResponseFunctionDelegate timeDelegate = delegate(string s)
                {
                    return System.DateTime.UtcNow.ToLongTimeString() + " (UTC)";
                };
                AutoResponseItem time = new AutoResponseItem(name, "time", "", '/', true, false, false, timeDelegate);
                //time.Enable();
                _items.Add(name, time);
            }

            // add /date command
            name = "date";
            if (!_items.Keys.Contains<string>(name))
            {
                ResponseFunctionDelegate dateDelegate = delegate(string s)
                {
                    return System.DateTime.UtcNow.ToShortDateString() + " (UTC)";
                };
                AutoResponseItem date = new AutoResponseItem(name, "date", "", '/', true, false, false, dateDelegate);
                //date.Enable();
                _items.Add(name, date);
            }
            
            // add /manual command
            name = "manual EN";
            if (!_items.Keys.Contains<string>(name))
            {
                string s = "";
                //s += "German: <a href=\"retroshare://file?name=RetroShare_Manual_German_2012_12_12.pdf&size=221437&hash=271cc46798434ffbc6163daae86cce475621c952\">RetroShare_Manual_German_2012_12_12.pdf</a> \r";
                s += "English: <a href=\"retroshare://file?name=RetroShare_Manual_English_2012_12_12_UNFINISHED.pdf&size=183421&hash=b7196b802271946e979279f56e4dea4c1cdac9d0\">RetroShare_Manual_English_2012_12_12_UNFINISHED.pdf</a>";
                AutoResponseItem manual = new AutoResponseItem(name, "manual", s, '/', true, false, false);
                //manual.Enable();
                _items.Add(name, manual);
            }
            name = "manual DE";
            if (!_items.Keys.Contains<string>(name))
            {
                string s = "";
                s += "German: <a href=\"retroshare://file?name=RetroShare_Manual_German_2012_12_12.pdf&size=221437&hash=271cc46798434ffbc6163daae86cce475621c952\">RetroShare_Manual_German_2012_12_12.pdf</a> \r";
                //s += "English: <a href=\"retroshare://file?name=RetroShare_Manual_English_2012_12_12_UNFINISHED.pdf&size=183421&hash=b7196b802271946e979279f56e4dea4c1cdac9d0\">RetroShare_Manual_English_2012_12_12_UNFINISHED.pdf</a>";
                AutoResponseItem manual = new AutoResponseItem(name, "manual", s, '/', true, false, false);
                //manual.Enable();
                _items.Add(name, manual);
            }

            // add /insult command
            //name = "insult";
            //if (!_items.Keys.Contains<string>(name))
            //{
            //    ResponseFunctionDelegate insultDelegate = delegate(string msg)
            //    {
            //        string[] a = msg.Split(' ');
            //        if (a == null || a.Length < 2)
            //            return "";
            //        return ("@" + a[1] + ": " + Processor.GetRandomInsult());
            //    };
            //    AutoResponseItem insult = new AutoResponseItem(name, "insult", "", '/', false, true, false, insultDelegate);
            //    //insult.Enable();
            //    _items.Add(name, insult);
            //}
        }

        /// <summary>
        /// Checks all auto response items if any matches.
        /// </summary>
        /// <param name="msgIN">message to check</param>
        /// <param name="cl"></param>
        /// <returns>message(s)</returns>
        internal string[] Process(ChatMessage msgIN, GuiChatLobby cl)
        {
            // check if auto response is activated
            if (!_b.GUI.cb_chat_arEnable.Checked)
                return null;

            // check if msg is too old
            System.Diagnostics.Debug.WriteLineIf(DEBUG, "rec msg: " + msgIN.send_time + " - now: " + Processor.conv_Date2Timestam(DateTime.UtcNow));
            if (msgIN.send_time < Processor.conv_Date2Timestam(DateTime.UtcNow) - 60)
                return null;

            // check if there are any items
            if (_items.Count == 0)
                this.SetupBasicItems();

            List<string> msgOUT = new List<string>();
            string s = "";

            AutoResponseItem[] items = new AutoResponseItem[_items.Count];
            _items.Values.CopyTo(items, 0);

            foreach (AutoResponseItem item in items)
            {
                if (item.IsActive() && !item.IsLobbyBlacklisted(cl.ID))
                    s = item.Process(msgIN);

                if (s != "")
                {
                    msgOUT.Add(s);
                    s = "";
                }
            }
            return msgOUT.ToArray();
        }

        /// <summary>
        /// Updates clb_chat_arList
        /// </summary>
        private void UpdateARList()
        {
            int i = 0;

            AutoResponseItem[] items = new AutoResponseItem[_items.Count];
            _items.Values.CopyTo(items, 0);

            // clear lis
            _b.GUI.clb_chat_arList.Items.Clear();

            foreach (AutoResponseItem item in items)
            {
                _b.GUI.clb_chat_arList.Items.Add(item.Name);
                _b.GUI.clb_chat_arList.SetItemChecked(i, item.IsActive());
                i++;
            }
        }

        /// <summary>
        /// Shows detailed infos for an item
        /// </summary>
        /// <param name="name">name of item to show</param>
        internal void ShowItem(string name)
        {
            AutoResponseItem item;
            if (!_items.TryGetValue(name, out item))
                return;

            _b.GUI.tb_chat_arName.Text = item.Name;
            _b.GUI.tb_chat_arSearchFor.Text = item.SearchFor;
            _b.GUI.tb_chat_arAnswer.Text = item.Answer;
            _b.GUI.tb_chat_arPrefix.Text = item.Prefix;

            _b.GUI.cb_chat_arCaseSensitive.Checked = item.CaseSensitive;
            _b.GUI.cb_chat_arOnly.Checked = item.Only;
            _b.GUI.cb_chat_arUsesFunction.Checked = item.UsesFunction();
            _b.GUI.cb_chat_arWithSpaces.Checked = item.WithSpaces;
        }

        /// <summary>
        /// Saves changes made 
        /// </summary>
        /// <param name="name">name of item to safe changes to</param>
        internal void SaveItem(string name)
        {
            AutoResponseItem item;
            if (!_items.TryGetValue(name, out item))
                return;

            item.CaseSensitive = _b.GUI.cb_chat_arCaseSensitive.Checked;
            item.Only = _b.GUI.cb_chat_arOnly.Checked;
            item.WithSpaces = _b.GUI.cb_chat_arWithSpaces.Checked;

            item.Name = _b.GUI.tb_chat_arName.Text;
            item.SearchFor = _b.GUI.tb_chat_arSearchFor.Text;
            if(item.CaseSensitive)
                item.SearchFor.ToLower();
            item.Answer = _b.GUI.tb_chat_arAnswer.Text;
            item.Prefix = _b.GUI.tb_chat_arPrefix.Text;

            _items[name] = item;

            this.UpdateARList();
        }

        /// <summary>
        /// Adds the given name to _items and calls SaveItem() and UpdateARList()
        /// </summary>
        /// <param name="name">name of the new item</param>
        internal void AddNew(string name)
        {
            if (_items.ContainsKey(name))
            {
                SaveItem(name);
                return;
            }

            AutoResponseItem item = new AutoResponseItem();
            _items.Add(name, item);
            this.SaveItem(name);
            this.UpdateARList();
        }

        /// <summary>
        /// Removes the item with the given name and calls UpdateARList()
        /// </summary>
        /// <param name="name">name to remove</param>
        internal void RemoveItem(string name)
        {
            if (_items.ContainsKey(name))
                _items.Remove(name);
            this.UpdateARList();
        }

        /// <summary>
        /// Calls Enable() / Disable
        /// </summary>
        /// <param name="name">name of the item to change</param>
        /// <param name="active">true = active</param>
        internal void SetActive(string name, bool active)
        {
            AutoResponseItem item;
            if (!_items.TryGetValue(name, out item))
                return;

            if (active)
                item.Enable();
            else
                item.Disable();

            _items[item.Name] = item;
        }

        /// <summary>
        /// Resets everything
        /// </summary>
        internal void Reset()
        {
            
        }
    }

    /// <summary>
    /// This class represents an individual configurable auto response
    /// </summary>
    [Serializable()]
    internal class AutoResponseItem
    {
        string _searchFor, _answer, _name;
        char _prefix;
        bool _only, _withSpaces, _active, _caseSensitive;
        List<string> _lobbyBlacklist;
        AutoResponse.ResponseFunctionDelegate _func;

        public string Name { get { return _name; } set { _name = value; } }
        public string Answer { get { return _answer; } set { _answer = value; } }
        public string SearchFor { get { return _searchFor; } set { _searchFor = value; } }
        public string Prefix { get { return _prefix != '\0' ? Convert.ToString(_prefix) : ""; } set { _prefix = value != "" ? Convert.ToChar(value) : '\0'; } }
        public bool Only { get { return _only; } set { _only = value; } }
        public bool WithSpaces { get { return _withSpaces; } set { _withSpaces = value; } }
        public bool CaseSensitive { get { return _caseSensitive; } set { _caseSensitive = value; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="searchFor"></param>
        /// <param name="answer"></param>
        /// <param name="prefix">'\0' for null/empty</param>
        /// <param name="only"></param>
        /// <param name="withSpaces"></param>
        /// <param name="func"></param>
        public AutoResponseItem(string name, string searchFor, string answer, char prefix, bool only, bool withSpaces,bool caseSensitive, AutoResponse.ResponseFunctionDelegate func = null)
        {
            _name = name;
            _searchFor = searchFor;
            _answer = answer;
            _prefix = prefix;   // like '/' or '@'
            _only = only;
            _withSpaces = withSpaces;
            _active = false;
            _caseSensitive = caseSensitive;
            _lobbyBlacklist = new List<string>();
            _func = func;
        }

        public AutoResponseItem()
        {
            _name = "";
            _searchFor = "";
            _answer = "";
            _prefix = '\0';   
            _only = false;
            _withSpaces = false;
            _active = false;
            _caseSensitive = false;
            _lobbyBlacklist = new List<string>();
            _func = null;
        }

        /// <summary>
        /// This function removes a lobby from the backlist
        /// </summary>
        /// <param name="lobbyID">lobby ID to remove</param>
        public void EnableLobby(string lobbyID)
        {
            if (_lobbyBlacklist.Contains(lobbyID))
                _lobbyBlacklist.Remove(lobbyID);
        }

        /// <summary>
        /// This function adds a lobby to the blacklist
        /// </summary>
        /// <param name="lobbyID">lobby ID to add</param>
        public void DisableLobby(string lobbyID)
        {
            if (!_lobbyBlacklist.Contains(lobbyID))
                _lobbyBlacklist.Add(lobbyID);
        }

        /// <summary>
        /// Activates the item
        /// </summary>
        public void Enable()
        {
            _active = true;
        }

        /// <summary>
        /// Deactivates the item
        /// </summary>
        public void Disable()
        {
            _active = false;
        }

        /// <summary>
        /// Returns if the item is active
        /// </summary>
        /// <returns>true = active</returns>
        public bool IsActive()
        {
            return _active;
        }

        /// <summary>
        /// Returns if the item uses a function to generate an auto response
        /// </summary>
        /// <returns>true = function is used</returns>
        public bool UsesFunction()
        {
            return _func != null;
        }

        /// <summary>
        /// Returns an (example) answer 
        /// </summary>
        /// <returns></returns>
        //public string GetAnswer()
        //{
        //    if (UsesFunction())
        //        return _func("");
        //    else
        //        return _answer;
        //}

        /// <summary>
        /// This function checks if the given lobby ID is on the blacklist
        /// </summary>
        /// <param name="lobbyID">lobby ID to check</param>
        /// <returns>true = blacklisted</returns>
        public bool IsLobbyBlacklisted(string lobbyID)
        {
            return _lobbyBlacklist.Contains(lobbyID) || lobbyID == ChatProcessor.BROADCAST;
        }

        /// <summary>
        /// This function will scan a given message and create a response
        /// </summary>
        /// <param name="msg">message to scan</param>
        /// <returns>message to return</returns>
        public string Process(ChatMessage msgIN)
        {
            // check if item is active
            // if not return
            if (!_active)
                return "";

            string msg = Processor.RemoteTags(msgIN.msg);

            if (!_caseSensitive)
                msg = msg.ToLower();

            // check for prefix
            // if matching prefix is found cut it off and continue
            // if no matching prefix is found return 
            if (_prefix != '\0')
                if (msg.StartsWith(Convert.ToString(_prefix)))               
                    msg = msg.Substring(1);
                else return "";

            // check if the sought phrase should occur alone
            // if it's found return the answer
            // if not return
            if (_only)
                if (msg == _searchFor)
                    return Process2(msgIN, msg);
                else
                    return "";

            if (_withSpaces && _prefix == '\0')
                if (Regex.IsMatch(msg, "\\b" + _searchFor + "\\b"))
                    return Process2(msgIN, msg);
                else
                    return "";            
            else if (_withSpaces && _prefix != '\0')
                // the problem with the prefix is, that the matching word must appear after the prefix -> StartsWith()
                // the + " " is requiered because of _withSpaces
                // in case word == _searchFor it does NOT match _searchFor + " "
                if (msg.StartsWith(_searchFor + " ") || msg == _searchFor)
                    return Process2(msgIN, msg);
                else return "";
            else
                if (msg.Contains(_searchFor))
                    return Process2(msgIN, msg);
                else
                    return "";
        }

        /// <summary>
        /// Creates the response using the predefined answer or calling the given function if there is any
        /// </summary>
        /// <returns>answer</returns>
        private string Process2(ChatMessage msgIN, string processedMsg)
        {
            string msg = "";
            if (_func == null)
                msg = _answer;
            else
                msg = _func(processedMsg);

            // replace %..% patterns
            msg = msg.Replace("%nick%", msgIN.peer_nickname);

            return msg;
        }
    }

    /// <summary>
    /// Class to compare AutoResponseItems by name
    /// </summary>
    class AutoResponseItemComparer : IComparer<AutoResponseItem>
    {
        public AutoResponseItemComparer() { }

        public int Compare(AutoResponseItem i1, AutoResponseItem i2)
        {
            return i1.Name.CompareTo(i2.Name);
        }
    }

}
