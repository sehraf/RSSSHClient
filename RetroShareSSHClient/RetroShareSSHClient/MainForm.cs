using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Sehraf.RSRPC;

//using ProtoBuf;

using rsctrl.chat;
using rsctrl.core;
using rsctrl.files;
//using rsctrl.gxs;
//using rsctrl.msgs;
using rsctrl.peers;
using rsctrl.search;
using rsctrl.system;


namespace RetroShareSSHClient
{
    public partial class MainForm : Form
    {
        struct GuiChatLobby
        {
            ChatLobbyInfo _lobby;
            bool _joined;
            bool _unread;
            ushort _listIndex;

            public ChatLobbyInfo Lobby { get { return _lobby; } set { _lobby = value; } }
            public string ID { get { return _lobby.lobby_id; } }
            public bool Joined { get { return _joined; } set { _joined = value; } }
            public bool Unread { get { return _unread; } set { _unread = value; } }
            public ushort Index { get { return _listIndex; } set { _listIndex = value; } }
        }

        struct GuiFileTransfer
        {
            FileTransfer _fileTransfer;
            ushort _listIndex;
            bool @new;

            public FileTransfer FileTransfer { get { return _fileTransfer; } set { _fileTransfer = value; } }
            //public File File { get { return _fileTransfer.file; } set { _fileTransfer.file = value; } }
            //public Direction Direction { get { return _fileTransfer.direction; } set { _fileTransfer.direction = value; } }
            //public float Fraction { get { return _fileTransfer.fraction; } set { _fileTransfer.fraction = value; } }
            //public float Speed { get { return _fileTransfer.rate_kBs; } set { _fileTransfer.rate_kBs = value; } }
            public string Hash { get { return _fileTransfer.file.hash; } set { _fileTransfer.file.hash = value; } }
            public ushort Index { get { return _listIndex; } set { _listIndex = value; } }
            public bool @New { get { return @new; } set { @new = value; } }

            public bool Download { get { return (_fileTransfer.direction == rsctrl.files.Direction.DIRECTION_DOWNLOAD); } }
            public bool Upload { get { return (_fileTransfer.direction == rsctrl.files.Direction.DIRECTION_UPLOAD); } }
        }

        struct GuiSearch
        {
            uint _searchID;
            string _keyWords;
            List<SearchHit> _results;
            DateTime _requestTime;
            ushort _listIndex;

            public uint ID { get { return _searchID; } set { _searchID = value; } }
            public string KeyWords { get { return _keyWords; } set { _keyWords = value; } }
            public List<SearchHit> Results { get { return _results; } set { _results = value; } }
            public DateTime RequestTime { get { return _requestTime; } set { _requestTime = value; } }
            public uint Age { get { return (uint)(DateTime.Now - _requestTime).TotalSeconds; } }
            public ushort Index { get { return _listIndex; } set { _listIndex = value; } }
        }

        class GuiFileTransferComparer : IComparer<GuiFileTransfer>
        {
            public int Compare(GuiFileTransfer p1, GuiFileTransfer p2)
            {
                return p1.FileTransfer.file.name.CompareTo(p2.FileTransfer.file.name);
            }
        }

        class GuiSearchHitsComparer : IComparer<SearchHit>
        {
            public int Compare(SearchHit p1, SearchHit p2)
            {
                return p1.file.name.CompareTo(p2.file.name);
            }
        }

        const bool DEBUG_CHAT = false;
        const bool DEBUG_FILES = false;
        const bool DEBUG_SEARCH = true;

        const string GROUPCHAT = "%groupChat%";

        RSRPC _rpc;
        Processor _processor;
        Settings _settings;

        uint _tickCounter;
        // peers
        Person _owner;
        List<Person> _friendList;
        Person _selectedFriend;

        // chat
        Dictionary<string, GuiChatLobby> _chatLobbies;
        Dictionary<string, string> _chatText;
        Dictionary<string, List<string>> _chatUser;
        bool _isRegistered;
        string _nick;

        // files
        Dictionary<string, GuiFileTransfer> _fileTransfers;

        // search
        Dictionary<uint, string> _pendingSearchReq;
        Dictionary<uint, GuiSearch> _searches;

        public MainForm()
        {
            InitializeComponent();
            cb_con.CheckState = CheckState.Unchecked;

            _rpc = new RSRPC(false);
            _rpc.ErrorOccurred += ErrorFromThread;
            _rpc.ReceivedMsg += ProcessMsgFromThread;
            _processor = new Processor(this);
            _settings = new Settings();
            _tickCounter = 0;
            // peers
            _owner = new Person();
            _friendList = new List<Person>();
            _selectedFriend = new Person();
            // chat
            _chatLobbies = new Dictionary<string, GuiChatLobby>();
            _chatText = new Dictionary<string, string>();
            _chatUser = new Dictionary<string, List<string>>();
            _isRegistered = false;
            // files
            _fileTransfers = new Dictionary<string, GuiFileTransfer>();
            // search
            _pendingSearchReq = new Dictionary<uint, string>();
            _searches = new Dictionary<uint, GuiSearch>();

            LoadForms();
        }

        private void LoadForms()
        {
            Options opt;
            if(_settings.Load(out opt)) {
                if (opt.SaveSettings)
                {
                    tb_host.Text = opt.Host;
                    tb_port.Text = opt.Port;
                    tb_user.Text = opt.User;
                    tb_pw.Text = opt.Password;
                    cb_settingsSave.Checked = true;
                    cb_settingsSavePW.Checked = opt.SavePW;
                }
                if (opt.SaveChat)
                {
                    SetNick(opt.Nick);
                    tb_chatAutoRespAnswer.Text = opt.AutoRespAnswer;
                    tb_chatAutoRespSearch.Text = opt.AutoRespSearch;
                    cb_chatAutoRespEnable.Checked = opt.EnableAutoResp;
                    cb_settingsSaveChat.Checked = true;
                }
            }
        }

        private void SaveForms()
        {
            Options opt = new Options();
            opt.SaveSettings = false;
            opt.SaveChat = false;
            if (cb_settingsSave.Checked)
            {
                opt.Host = tb_host.Text;
                opt.Port = tb_port.Text;
                opt.User = tb_user.Text;
                opt.Password = cb_settingsSavePW.Checked ? tb_pw.Text : "";
                opt.SaveSettings = true;
                opt.SavePW = cb_settingsSavePW.Checked;
            }
            if (cb_settingsSaveChat.Checked)
            {
                opt.Nick = _nick;
                opt.AutoRespAnswer = tb_chatAutoRespAnswer.Text;
                opt.AutoRespSearch = tb_chatAutoRespSearch.Text;
                opt.EnableAutoResp = cb_chatAutoRespEnable.Checked;
                opt.SaveChat = true;
            }            
            _settings.Save(opt);
        }

        private void ErrorFromThread(Exception e)
        {
            this.Invoke((MethodInvoker)delegate { Error(e); });
        }

        private void Error(Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e.Message);
            tb_out.AppendText(e.Message + "\n");
        }

        private void ProcessMsgFromThread(RSProtoBuffSSHMsg msg)
        {           
            this.Invoke((MethodInvoker)delegate { _processor.ProcessMsg(msg); });
        }

        private void ConnectionEstablished()
        {
            _tickCounter = 0;
            t_tick.Start();
            uint regID;
            regID = _rpc.SystemGetStatus();
            regID = _rpc.PeersGetFriendList(RequestPeers.SetOption.OWNID);
            _processor.PendingPeerRequests.Add(regID, RequestPeers.SetOption.OWNID);
            SetNick(_nick);

            AddGroupChat();
        }

        private void Connect()
        {
            if (_rpc.IsConnected)
                return;
            cb_con.CheckState = CheckState.Indeterminate;
            _processor.PendingPeerRequests.Clear();
            if (_rpc.Connect(tb_host.Text, Convert.ToUInt16(tb_port.Text), tb_user.Text, tb_pw.Text))
            {
                tb_out.Text = "connected!" + "\n";
                cb_con.CheckState = CheckState.Checked;
                ConnectionEstablished();
            }
            else
            {
                tb_out.Text = "error while connecting!" + "\n";
                cb_con.CheckState = CheckState.Unchecked;
                t_tick.Stop();
            }
        }

        private void Shutdown()
        {
            tb_out.AppendText("shuting down RS server and disconnecting" + "\n");
            Disconnect(true);
        }

        private void Disconnect(bool shutdown = false)
        {
            if (!_rpc.IsConnected)
                return;
            t_tick.Stop();

            CloseAllSearches();
            System.Threading.Thread.Sleep(250);           

            _rpc.Disconnect(shutdown);
            //tb_out.Text = "disconnected!";
            cb_con.CheckState = CheckState.Unchecked;
        }

        private void CloseAllSearches()
        {
            GuiSearch[] values = new GuiSearch[_searches.Count];
            _searches.Values.CopyTo(values, 0);
            foreach (GuiSearch gs in values)
            {
                _rpc.SearchClose(gs.ID);
            }
        }

        #region chat
        // --------- chat ---------

        public void UpdateChatLobbies(ResponseChatLobbies msg)
        {
            foreach (ChatLobbyInfo lobby in msg.lobbies)
                ProcessLobby(lobby);

            CheckChatRegistration();
        }

        public void LobbyInvite(EventLobbyInvite msg)
        {
            ProcessLobby(msg.lobby);
            CheckChatRegistration();
        }

        private void ProcessLobby(ChatLobbyInfo lobby) {
            string ID = lobby.lobby_id;
            GuiChatLobby cl = new GuiChatLobby();
            if (_chatLobbies.ContainsKey(ID)) //update
            {
                System.Diagnostics.Debug.WriteLineIf(DEBUG_CHAT, "updating lobby " + lobby.lobby_name);
                System.Diagnostics.Debug.WriteLineIf(DEBUG_CHAT, "user: " + lobby.no_peers + " - names: " + lobby.nicknames.Count + " - friends: " + lobby.participating_friends.Count);
                cl = _chatLobbies[ID];
                cl.Lobby = lobby;
                _chatLobbies[ID] = cl;
                _chatUser[ID] = lobby.participating_friends;
                clb_chatLobbies.Items[cl.Index] = "(" + lobby.no_peers + ") " + lobby.lobby_name + " - " + lobby.lobby_topic;
            }
            else //new 
            {
                System.Diagnostics.Debug.WriteLineIf(DEBUG_CHAT, "adding lobby " + lobby.lobby_name);
                System.Diagnostics.Debug.WriteLineIf(DEBUG_CHAT, "user: " + lobby.no_peers + " - names: " + lobby.nicknames.Count + " - friends: " + lobby.participating_friends.Count);
                cl.Lobby = lobby;
                cl.Index = (ushort)clb_chatLobbies.Items.Count;
                cl.Joined = (lobby.lobby_state == ChatLobbyInfo.LobbyState.LOBBYSTATE_JOINED);
                {
                    clb_chatLobbies.Items.Add(cl.Lobby.lobby_name, cl.Joined);
                    cl.Index = (ushort)clb_chatLobbies.Items.IndexOf(cl.Lobby.lobby_name);
                    if (lobby.lobby_state == ChatLobbyInfo.LobbyState.LOBBYSTATE_INVITED)
                        clb_chatLobbies.SetItemCheckState(cl.Index, CheckState.Indeterminate);                    
                }
                cl.Unread = false;
                _chatLobbies.Add(cl.ID, cl);                
                _chatText.Add(cl.ID, "======= " + DateTime.Now.ToLongDateString() + " - " + lobby.lobby_name + " =======\n");
                _chatUser.Add(cl.ID, lobby.participating_friends);                
            }
        }

        public void SendChatMsg(string inMsg = "")
        {
            int index = clb_chatLobbies.SelectedIndex;
            if (index >= 0 && clb_chatLobbies.GetItemChecked(index))
            {
                GuiChatLobby cl = new GuiChatLobby();
                if (GetLobbyByListIndex(index, out cl))
                {
                    string text = (inMsg == "") ? tb_chatMsg.Text : inMsg;

                    ChatId id = new ChatId();
                    if (cl.ID == GROUPCHAT)
                    {
                        id.chat_id = "";
                        id.chat_type = ChatType.TYPE_GROUP;
                    }
                    else
                    {
                        id.chat_id = cl.ID;
                        id.chat_type = ChatType.TYPE_LOBBY;
                    }
                    ChatMessage msg = new ChatMessage();
                    msg.id = id;
                    msg.msg = text;
                    msg.peer_nickname = _nick;
                    msg.send_time = (uint)DateTime.Now.Second;

                    _rpc.ChatSendMsg(msg);
                    PrintMsgToLobby(cl.ID, DateTime.Now.ToLongTimeString() + " - " + _nick + " > " + text + "\n");
                    if (inMsg == "")
                        tb_chatMsg.Clear();
                }
            }
        }

        public void PrintMsgToLobby(string ID, EventChatMessage response)
        {
            if (!_chatLobbies.ContainsKey(ID))
                // we don't know this lobby :S
                return;

            GuiChatLobby cl = _chatLobbies[ID];
            string msg = Processor.RemoteTags(response.msg.msg);
            _chatText[ID] += DateTime.Now.ToLongTimeString() + " - " + response.msg.peer_nickname + " > " + msg + "\n";
            if (clb_chatLobbies.SelectedIndex == cl.Index)
                SetChatText(ID);
            else            
                if (!cl.Unread)
                {
                    cl.Unread = true;
                    clb_chatLobbies.SetItemCheckState(cl.Index, CheckState.Indeterminate);
                    _chatLobbies[ID] = cl;
                }

            AutoAnswer(Processor.RemoteTags(response.msg.msg));
        }

        public void PrintMsgToLobby(string ID, string msg)
        {
            GuiChatLobby cl = _chatLobbies[ID];
            _chatText[ID] += msg;
            if (clb_chatLobbies.SelectedIndex == cl.Index)
                SetChatText(ID);
        }

        public void PrintMsgToGroupChat(EventChatMessage response)
        {
            _chatText[GROUPCHAT] += DateTime.Now.ToShortTimeString() + " - " + response.msg.peer_nickname + " > " + Processor.RemoteTags(response.msg.msg) + "\n";
            if (clb_chatLobbies.SelectedIndex == 0)
                SetChatText(GROUPCHAT);
            else
            {
                GuiChatLobby cl = _chatLobbies[GROUPCHAT];
                if (!cl.Unread)
                {
                    cl.Unread = true;
                    clb_chatLobbies.SetItemCheckState(cl.Index, CheckState.Indeterminate);
                    _chatLobbies[GROUPCHAT] = cl;
                } 
            }
            AutoAnswer(Processor.RemoteTags(response.msg.msg));
        }

        private void SetChatText(string ID)
        {
            //rtb_chat.Clear();
            //rtb_chat.Rtf =@"{\rtf1\ansi " + _chatText[index] + "}";
            rtb_chat.Text = _chatText[ID];
            rtb_chat.SelectionStart = rtb_chat.Text.Length;
            rtb_chat.ScrollToCaret();

            GuiChatLobby cl = _chatLobbies[ID];
            cl.Unread = false;
            _chatLobbies[ID] = cl;
            if (cl.Joined && clb_chatLobbies.GetItemCheckState(cl.Index) == CheckState.Indeterminate)
                clb_chatLobbies.SetItemCheckState(cl.Index, CheckState.Checked);
        }

        private void AutoAnswer(string msg) 
        {
            if (cb_chatAutoRespEnable.Checked && tb_chatAutoRespSearch.Text != "" && tb_chatAutoRespAnswer.Text != "")
            {
               if(msg.ToLower().Contains(tb_chatAutoRespSearch.Text.ToLower()))
                   SendChatMsg(tb_chatAutoRespAnswer.Text);
            }                
        }

        private void SetNick(string nick)
        {
            _nick = nick;
            tb_chatNickname.Text = _nick;

            if (_rpc != null && _rpc.IsConnected)
            {
                List<string> ids = new List<string>();
                Dictionary<string, GuiChatLobby>.ValueCollection lobbies = _chatLobbies.Values;
                foreach (GuiChatLobby lobby in lobbies)
                    if (clb_chatLobbies.GetItemChecked(lobby.Index))
                        if(lobby.ID != GROUPCHAT)
                            ids.Add(lobby.ID);

                if (ids.Count == 0)
                    _rpc.ChatSetLobbyNickname(_nick);
                else
                    _rpc.ChatSetLobbyNickname(_nick, ids);
            }
        }

        private void AddGroupChat()
        {
            if (!_chatLobbies.ContainsKey(GROUPCHAT))
            {
                ChatLobbyInfo lobby = new ChatLobbyInfo();
                lobby.lobby_name = "Group chat";
                lobby.lobby_id = GROUPCHAT;

                GuiChatLobby cl = new GuiChatLobby();
                cl.Lobby = lobby;
                cl.Joined = true;
                cl.Unread = false;
                {
                    clb_chatLobbies.Items.Add(cl.Lobby.lobby_name, true);
                    cl.Index = (ushort)clb_chatLobbies.Items.IndexOf(cl.Lobby.lobby_name);
                }
                _chatLobbies.Add(cl.ID, cl);
                _chatText.Add(cl.ID, DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + "\n");
                _chatUser.Add(cl.ID, new List<string> { });
                CheckChatRegistration();
            }
        }
        
        private void CheckChatRegistration()
        {
            /*
             * if we register to early the server may send us messages from lobbies we don't know yet.
             * AddGroupChat() will call this function before we requested the lobby list.
             * after 2 minutes ( +5 secs for answer) we should have every reachable lobby
             */
            if (_chatLobbies.Count <= 1 && _tickCounter < 125)
                return;

            bool joined = false;
            Dictionary<string, GuiChatLobby>.ValueCollection Lobbies = _chatLobbies.Values;
            foreach (GuiChatLobby lobby in Lobbies)
            {
                if (lobby.Joined)
                {
                    joined = true;
                    break;
                }
            }

            if (joined && !_isRegistered)
            {
                _rpc.ChatRegisterEvent(RequestRegisterEvents.RegisterAction.REGISTER);
                tb_out.AppendText("Registered" + "\n");
                _isRegistered = true;
            }

            if (!joined && _isRegistered)
            {
                _rpc.ChatRegisterEvent(RequestRegisterEvents.RegisterAction.DEREGISTER);
                tb_out.AppendText("Unregistered" + "\n");
                _isRegistered = false;
            }
        }

        private bool GetLobbyByListIndex(int index, out GuiChatLobby lobby)
        {
            GuiChatLobby[] values = new GuiChatLobby[_chatLobbies.Values.Count];
            _chatLobbies.Values.CopyTo(values, 0);
            foreach (GuiChatLobby l in values)
            {
                if (l.Index == index)
                {
                    lobby = l;
                    return true;
                }
            }
            lobby = default(GuiChatLobby);
            return false;
        }

        private void ToggleCheckboxes()
        {
            //try
            //{
                //Dictionary<string, GuiChatLobby>.ValueCollection Lobbies = _chatLobbies.Values;
                //foreach (GuiChatLobby lobby in Lobbies)
            GuiChatLobby[] values = new GuiChatLobby[_chatLobbies.Count];
            foreach (GuiChatLobby lobby in values)
            {
                if (lobby.Lobby == null)
                    continue;
                CheckState state = clb_chatLobbies.GetItemCheckState(lobby.Index);
                if (lobby.Unread)
                {
                    if (state == CheckState.Indeterminate)
                        if (lobby.Joined)
                            state = CheckState.Checked;
                        else
                            state = CheckState.Unchecked;
                    else
                        state = CheckState.Indeterminate;
                    clb_chatLobbies.SetItemCheckState(lobby.Index, state);
                }
                else if (lobby.Lobby.lobby_state == ChatLobbyInfo.LobbyState.LOBBYSTATE_INVITED)
                    clb_chatLobbies.SetItemCheckState(lobby.Index, (state == CheckState.Indeterminate) ? CheckState.Unchecked : CheckState.Indeterminate);
            }
            //}
            //catch (Exception e)
            //{
            //    System.Diagnostics.Debug.WriteLineIf(DEBUG_CHAT, e.Message);
            //}
        }

        #endregion

        #region files
        // --------- files ---------
        public void UpdateFileTransfers(ResponseTransferList list)
        {
            if (list.transfers.Count == 0)
                return;
            List<string> hashes = new List<string>();
            Direction dir = list.transfers[0].direction;
            bool added = false;
            bool removed = false;

            // add new / update old
            foreach (FileTransfer ft in list.transfers)
            {
                if (ft == null)
                    continue;
                if (_fileTransfers.ContainsKey(ft.file.hash)) // update
                {
                    GuiFileTransfer gft = _fileTransfers[ft.file.hash];
                    gft.FileTransfer = ft;
                    _fileTransfers[ft.file.hash] = gft;
                    System.Diagnostics.Debug.WriteLineIf(DEBUG_FILES, "FileTransfer: updating " + ft.file.hash);
                }
                else //new
                {
                    GuiFileTransfer gft = new GuiFileTransfer();
                    gft.FileTransfer = ft;
                    gft.New = true;
                    _fileTransfers.Add(gft.Hash, gft);
                    added = true;
                    System.Diagnostics.Debug.WriteLineIf(DEBUG_FILES, "FileTransfer: adding " + ft.file.hash);
                }
                hashes.Add(ft.file.hash);
            }

            // remove
            GuiFileTransfer[] fileTransfers = new GuiFileTransfer[_fileTransfers.Values.Count];
            _fileTransfers.Values.CopyTo(fileTransfers, 0);
            foreach (GuiFileTransfer gft in fileTransfers)
            {
                if (!hashes.Contains(gft.Hash) && gft.FileTransfer.direction == dir)
                {
                    _fileTransfers.Remove(gft.Hash);
                    removed = true;
                }
            }

            UpdateFileLists(dir, added, removed);
        }

        public void UpdateFileLists(Direction dir, bool added, bool removed)
        {
            
            //if (added)
            //{
            //    GuiFileTransfer[] values = new GuiFileTransfer[_fileTransfers.Values.Count];
            //    _fileTransfers.Values.CopyTo(values, 0);

            //    // build an array to see which indices were removed
            //    foreach (GuiFileTransfer gft in values)
            //    {
            //        if (gft.New)
            //        {
            //            if (dir == Direction.DIRECTION_DOWNLOAD)
            //            {
            //                ushort index = (ushort)lb_filesDownloads.Items.Count;
            //                lb_filesDownloads.Items.Add(BuildFileTransferString(gft.FileTransfer));
            //            }
            //            else
            //            {
            //                ushort index = (ushort)lb_filesUploads.Items.Count;
            //                lb_filesUploads.Items.Add(BuildFileTransferString(gft.FileTransfer));
            //            }
            //            // update new flag
            //            GuiFileTransfer gft2 = _fileTransfers[gft.Hash];
            //            gft2.New = false;
            //            _fileTransfers[gft.Hash] = gft2;
            //        }
            //    }
            //}
            //if (!removed) //just update list 
            //{
            //    GuiFileTransfer[] values = new GuiFileTransfer[_fileTransfers.Values.Count];
            //    _fileTransfers.Values.CopyTo(values, 0);

            //    foreach (GuiFileTransfer gft in values)
            //    {
            //        if (gft.FileTransfer.direction == dir)
            //            if (dir == Direction.DIRECTION_DOWNLOAD)
            //            {                            
            //                lb_filesDownloads.Items[gft.Index] = BuildFileTransferString(gft.FileTransfer);
            //            }
            //            else
            //            {
            //                lb_filesUploads.Items[gft.Index] = BuildFileTransferString(gft.FileTransfer);
            //            }
            //    }
            //}
            //if (removed) // items were removed
            //{
            //    bool[] stillHere = new bool[_fileTransfers.Values.Count * 2]; // should be enough
            //    ushort max = 0, index = 0;

            //    GuiFileTransfer[] values = new GuiFileTransfer[_fileTransfers.Values.Count];
            //    _fileTransfers.Values.CopyTo(values, 0);

            //    // build an array to see which indices were removed
            //    foreach (GuiFileTransfer gft in values)
            //    {
            //        if (gft.FileTransfer.direction == dir)
            //        {
            //            index = gft.Index;
            //            max = (index > max) ? index : max;
            //            if (index > stillHere.Length)
            //            {
            //                bool[] tmp = new bool[stillHere.Length];
            //                stillHere.CopyTo(tmp, 0);
            //                stillHere = null;
            //                stillHere = new bool[tmp.Length * 2];
            //                tmp.CopyTo(stillHere, 0);
            //            }
            //            stillHere[index] = true;
            //        }                        
            //    }

            //    //now removed the removed items and fill the gapes
            //    for (ushort i = 0; i < max; i++)
            //    {
            //        if (stillHere[i]) // just update  
            //        {
            //            GuiFileTransfer gft = new GuiFileTransfer();
            //            if (GetFileTransferByListIndex(i, out gft))
            //                if (dir == Direction.DIRECTION_DOWNLOAD)
            //                    lb_filesDownloads.Items[gft.Index] = BuildFileTransferString(gft.FileTransfer);
            //                else
            //                    lb_filesUploads.Items[gft.Index] = BuildFileTransferString(gft.FileTransfer);
            //            //else - HOW COUDL THIS HAPPEN?
            //        }
            //        else // removed
            //        {
            //            for (ushort j = (ushort)(i + 1); j < max; j++)
            //            {
            //                GuiFileTransfer gft = new GuiFileTransfer();
            //                if (GetFileTransferByListIndex(j, out gft))
            //                {
            //                    // move item one field up 
            //                    gft.Index--;
            //                    _fileTransfers[gft.Hash] = gft;
            //                    if (dir == Direction.DIRECTION_DOWNLOAD)
            //                        lb_filesDownloads.Items[gft.Index] = BuildFileTransferString(gft.FileTransfer);
            //                    else
            //                        lb_filesUploads.Items[gft.Index] = BuildFileTransferString(gft.FileTransfer);
            //                } 
            //                //else - just skip removed filetransfers
            //            }
            //        }
            //    }
            //}


            List<GuiFileTransfer> tmpList = new List<GuiFileTransfer>();
            GuiFileTransfer[] values = new GuiFileTransfer[_fileTransfers.Values.Count];

            _fileTransfers.Values.CopyTo(values, 0);
            foreach (GuiFileTransfer gft in values)
            {
                if (gft.FileTransfer.direction == dir)
                    tmpList.Add(gft);
            }

            List<GuiFileTransfer> list = new List<GuiFileTransfer>();

            Dictionary<string, GuiFileTransfer>.ValueCollection fileTransfers = _fileTransfers.Values;
            foreach (GuiFileTransfer gft in fileTransfers)
                if (dir == gft.FileTransfer.direction)
                    list.Add(gft);

            list.Sort(new GuiFileTransferComparer());

            string hash = "";
            GuiFileTransfer tmpGft;
            if (dir == Direction.DIRECTION_DOWNLOAD)
            {
                if (lb_filesDownloads.SelectedIndex != -1)
                    if (GetFileTransferByListIndexDir(lb_filesDownloads.SelectedIndex, dir, out tmpGft))
                        hash = tmpGft.Hash;
            }
            else
            {
                if (lb_filesUploads.SelectedIndex != -1)
                    if (GetFileTransferByListIndexDir(lb_filesUploads.SelectedIndex, dir, out tmpGft))
                        hash = tmpGft.Hash;
            }

            string s;
            ushort index = 0;
            int selectedItem = -1;
            GuiFileTransfer gft2;
            string[] list2 = new string[list.Count];

            foreach (GuiFileTransfer gft in list)
            {
                s = BuildFileTransferString(gft.FileTransfer);
                list2[index] = s;

                gft2 = _fileTransfers[gft.Hash];
                gft2.Index = index;
                _fileTransfers[gft.Hash] = gft2;

                if (hash == gft.Hash)
                    selectedItem = index;

                index++;
            }

            if (dir == Direction.DIRECTION_DOWNLOAD)
            {
                lb_filesDownloads.Items.Clear();
                lb_filesDownloads.Items.AddRange(list2);
                lb_filesDownloads.SelectedIndex = selectedItem;
            }
            else
            {
                lb_filesUploads.Items.Clear();
                lb_filesUploads.Items.AddRange(list2);
                lb_filesUploads.SelectedIndex = selectedItem;
            }

        }

        private string BuildFileTransferString(FileTransfer ft)
        {
            return String.Format("{0:0,0.00}", ft.rate_kBs) + "kBs - " +
                ((ft.direction == Direction.DIRECTION_DOWNLOAD) ? (String.Format("{0:0,0.00}", ft.fraction * 100) + "% - ") : "") +
                BuildSizeString(ft.file.size) + " - " +
                ft.file.name;
        }

        private string BuildSizeString(ulong size)
        {
            byte counter = 0;
            float sizef = size;
            while (sizef > 1024)
            {
                counter++;
                sizef /= 1024;
            }
            string s = "";
            switch (counter)
            {
                case 0:
                    s = "B";
                    break;
                case 1:
                    s = "KiB";
                    break;
                case 2:
                    s = "MiB";
                    break;
                case 3:
                    s = "GiB";
                    break;
                case 4:
                    s = "TiB";
                    break;
                case 5:
                    s = "PiB";
                    break;
                default:
                    s = "too damn high";
                    break;
            }
            return String.Format("{0:0.00}", sizef) + s;
        }

        private bool GetFileTransferBySelection(out GuiFileTransfer gft)
        {
            gft = new GuiFileTransfer();
            if (lb_filesDownloads.SelectedIndex != -1)
                if (GetFileTransferByListIndexDir(lb_filesDownloads.SelectedIndex, Direction.DIRECTION_DOWNLOAD, out gft)) ;
                else return false;
            else if (lb_filesUploads.SelectedIndex != -1)
                if (GetFileTransferByListIndexDir(lb_filesUploads.SelectedIndex, Direction.DIRECTION_UPLOAD, out gft)) ;
                else return false;
            else return false;
            return true;
        }

        private bool GetFileTransferByListIndexDir(int index, Direction dir, out GuiFileTransfer gft)
        {
            GuiFileTransfer[] values = new GuiFileTransfer[_fileTransfers.Values.Count];
            _fileTransfers.Values.CopyTo(values, 0);
            foreach (GuiFileTransfer ft in values)
            {
                if (ft.Index == index && ft.FileTransfer.direction == dir)
                {
                    gft = ft;
                    return true;
                }
            }
            gft = default(GuiFileTransfer);
            return false;
        }

        #endregion

        #region peers
        // ---------- peers ----------

        public void UpdatePeerList(ResponsePeerList msg)
        {
            int index1 = lb_friends.SelectedIndex, index2 = lb_locations.SelectedIndex;
            lb_friends.Items.Clear();
            ClearPeerForm();
            _friendList.Clear();
            //List<Person> persons = new List<Person>();
            List<Location> locs = new List<rsctrl.core.Location>();
            _friendList = msg.peers;
            if (_owner.locations.Count > 0)
                _friendList.Add(_owner);
            _friendList.Sort(new PersonComparer());
            foreach (Person p in _friendList)
            {
                locs = p.locations;
                byte online = 0, total = 0;
                foreach (Location l in locs)
                {
                    total++;
                    switch (l.state)
                    {
                        case ((uint)rsctrl.core.Location.StateFlags.CONNECTED):
                        case ((uint)rsctrl.core.Location.StateFlags.ONLINE):
                        case ((uint)(rsctrl.core.Location.StateFlags.CONNECTED | rsctrl.core.Location.StateFlags.ONLINE)):
                            online++;
                            break;
                        default: // offline
                            break;
                    }
                }
                lb_friends.Items.Add("(" + online + "/" + total + ") " + p.name);
            }
            lb_friends.SelectedIndex = index1;
            lb_locations.SelectedIndex = index2;
            //lb_locations_SelectedIndexChanged(null, null);
        }

        public void SetOwnID(ResponsePeerList msg)
        {
            _owner = msg.peers[0]; // i gues we just have one owner           
            this.Text = "RetroShare SSH Client by sehraf - " + _owner.name + "(" + _owner.gpg_id + ") ";// +l.location + " (" + l.ssl_id + ")";

            if (_nick == "" || _nick == null)
            {
                string name = _owner.name.Trim();
                SetNick(name + " (nogui/ssh)");
            }
        }
        
        private void ClearPeerForm()
        {
            lb_locations.Items.Clear();
            tb_peerName.Clear();
            tb_peerID.Clear();
            tb_peerLocation.Clear();
            tb_peerLocationID.Clear();
            tb_peerIPExt.Clear();
            tb_peerIPInt.Clear();
            nud_peerPortExt.Value = nud_peerPortExt.Minimum;
            nud_peerPortInt.Value = nud_peerPortInt.Minimum;
        }

        #endregion

        #region search
        // ---------- search ----------

        public void RegisterSearchIDs(uint ReqID, ResponseSearchIds response)
        {
            if (_pendingSearchReq.ContainsKey(ReqID))
            {                
                GuiSearch gs = new GuiSearch();
                gs.KeyWords = _pendingSearchReq[ReqID];                
                gs.RequestTime = DateTime.Now;
                gs.ID = response.search_id[0]; // for now we only support one ID

                System.Diagnostics.Debug.WriteLineIf(DEBUG_SEARCH, "Search: Adding ID " + gs.ID);
                _searches.Add(gs.ID, gs);
                _pendingSearchReq.Remove(ReqID);

                UpdateSearches();
            }
        }

        public void ProcessSearchResults(ResponseSearchResults response)
        {
            GuiSearch gs = new GuiSearch();
            bool updated = false;
            System.Diagnostics.Debug.WriteLineIf(DEBUG_SEARCH, "Search: Processing " + response.searches.Count + " search results");
            foreach (SearchSet ss in response.searches)
            {
                System.Diagnostics.Debug.WriteLineIf(DEBUG_SEARCH, "Search: Processing ID" + ss.search_id + " with " + ss.hits.Count + " hits");
                if (_searches.ContainsKey(ss.search_id))
                {                    
                    gs = _searches[ss.search_id];
                    System.Diagnostics.Debug.WriteLineIf(DEBUG_SEARCH, "Search: Updating results (ID " + gs.ID + ")");
                    if (ss.hits.Count > 0)
                        gs.Results = new List<SearchHit>();
                    foreach (SearchHit sh in ss.hits)
                    {              
                        updated = false;
                        // update 
                        for (int i = 0; i < gs.Results.Count; i++)
                        {
                            if (gs.Results[i].file.hash == sh.file.hash)
                            {
                                gs.Results[i].no_hits += sh.no_hits;
                                gs.Results[i].file.name = (gs.Results[i].file.name.Length > sh.file.name.Length) ? gs.Results[i].file.name : sh.file.name;
                                updated = true;
                            }
                        }
                        // adding new
                        if (!updated)
                            gs.Results.Add(sh);
                    }
                    _searches[ss.search_id] = gs;
                }                
            }
            UpdateSearches();
            UpdateSearchResults(lb_searches.SelectedIndex);
        }

        private void Search(string keyWords)
        {
            string[] strings = keyWords.Split(' ');
            List<string> list = new List<string>();
            uint reqID;

            list.AddRange(strings);
            reqID = _rpc.SearchBasic(list);

            _pendingSearchReq.Add(reqID, keyWords);
        }

        private void GetSearchResults()
        {
            List<uint> searchIDs = new List<uint>();
            GuiSearch[] values = new GuiSearch[_searches.Values.Count];
            _searches.Values.CopyTo(values, 0);
            foreach (GuiSearch gs in values)
            {
                if (gs.Age > 10 && gs.Age < 60)
                {
                    System.Diagnostics.Debug.WriteLineIf(DEBUG_SEARCH, "Search: requesting results for " + gs.ID);
                    searchIDs.Add(gs.ID);
                }
            }
            if (searchIDs.Count > 0)
                //_rpc.SearchResult(searchIDs);
                _rpc.SearchResult(new List<uint>() { });
        }

        public void UpdateSearches()
        {
            GuiSearch gs2;
            uint selectedID = 0;
            ushort selectedIndex = ushort.MaxValue;

            if (lb_searches.SelectedIndex != -1)
                if (GetSearchByIndex((ushort)lb_searches.SelectedIndex, out gs2))
                    selectedID = gs2.ID;


            GuiSearch[] values = new GuiSearch[_searches.Values.Count];
            _searches.Values.CopyTo(values, 0);
            lb_searches.Items.Clear();
            foreach (GuiSearch gs in values)
            {
                gs2 = _searches[gs.ID];
                gs2.Index = (ushort)lb_searches.Items.Count;
                _searches[gs.ID] = gs2;
                lb_searches.Items.Add(((gs.Results != null) ? gs.Results.Count : 0) + " - " + gs.KeyWords);

                if (gs.ID == selectedID)
                    selectedIndex = gs.Index;
            }

            if (selectedIndex < ushort.MaxValue)
                lb_searches.SelectedIndex = selectedIndex;
        }

        public void UpdateSearchResults(int index)
        {
            if (index == -1)
            {
                lb_searchResults.Items.Clear();
                return;
            }

            GuiSearch gs = new GuiSearch();
            if (!GetSearchByIndex((ushort)index, out gs))
                return;
            if (gs.Results == null)
                return;

            lb_searchResults.Items.Clear();
            foreach (SearchHit sh in gs.Results)
            {
                lb_searchResults.Items.Add(sh.no_hits + " hits - " + sh.file.name);
            }

            if(index < lb_searchResults.Items.Count)
                lb_searchResults.SelectedIndex = index;
        }

        private bool GetSearchByIndex(ushort index, out GuiSearch gs)
        {
            gs = new GuiSearch();
            GuiSearch[] values = new GuiSearch[_searches.Values.Count];
            _searches.Values.CopyTo(values, 0);
            foreach (GuiSearch gs2 in values)
            {
                if (gs2.Index == index)
                {
                    gs = gs2;
                    return true;
                }
            }
            return false;
        }

        #endregion

        // ---------- system ----------

        public void UpdateSystemStatus(ResponseSystemStatus msg)
        {
            if (msg.status != null && msg.status.code == Status.StatusCode.SUCCESS)
            {
                gb_systemStatus.Text = "System Status: Running!";

                switch (msg.net_status)
                {
                    case ResponseSystemStatus.NetCode.BAD_NATSYM:
                        l_network.Text = "BAD: symmetric NAT";
                        break;
                    case ResponseSystemStatus.NetCode.BAD_NODHT_NAT:
                        l_network.Text = "BAD: natted/no DHT";
                        break;
                    case ResponseSystemStatus.NetCode.BAD_OFFLINE:
                        l_network.Text = "BAD: offline";
                        break;
                    case ResponseSystemStatus.NetCode.BAD_UNKNOWN:
                        l_network.Text = "BAD: unknown";
                        break;
                    case ResponseSystemStatus.NetCode.WARNING_NATTED:
                        l_network.Text = "Warning: natted";
                        break;
                    case ResponseSystemStatus.NetCode.WARNING_NODHT:
                        l_network.Text = "Warning: no DHT";
                        break;
                    case ResponseSystemStatus.NetCode.WARNING_RESTART:
                        l_network.Text = "Warning: restarting";
                        break;
                    case ResponseSystemStatus.NetCode.ADV_FORWARD:
                        l_network.Text = "GOD: adv. forward";
                        break;
                    case ResponseSystemStatus.NetCode.GOOD:
                        l_network.Text = "GOD :)";
                        break;
                }

                l_connected.Text = "Connected: " + Convert.ToString(msg.no_connected);
                l_peers.Text = "Peers: " + Convert.ToString(msg.no_peers);

                l_bwUp.Text = "Up: " + String.Format("{0:0,0.00}", msg.bw_total.up) + "kb/s";
                l_bwDown.Text = "Down: " + String.Format("{0:0,0.00}", msg.bw_total.down) + "kb/s";
            }
            else if (msg.status != null && msg.status.code == Status.StatusCode.READMSG)
                gb_systemStatus.Text = "System Status: " + msg.status.msg;
            else
                gb_systemStatus.Text = "System Status: Error";
        }
        
        // --------- ---------

        private bool PortInRange(uint port)
        {
            return (port >= 1024 && port <= UInt16.MaxValue);
        }

        private void Tick()
        {
            if (_rpc.IsConnected)
            {
                _rpc.SystemGetStatus();
                ToggleCheckboxes();

                if (_tickCounter % 2 == 0)
                {
                    _rpc.FilesGetTransferList(rsctrl.files.Direction.DIRECTION_DOWNLOAD);
                    _rpc.FilesGetTransferList(rsctrl.files.Direction.DIRECTION_UPLOAD);                    
                }

                if (_tickCounter % 10 == 0)
                {
                    GetSearchResults();
                }

                if (_tickCounter % 30 == 0)
                {
                    uint regID;
                    regID = _rpc.ChatGetLobbies(rsctrl.chat.RequestChatLobbies.LobbySet.LOBBYSET_JOINED);
                    regID = _rpc.ChatGetLobbies(rsctrl.chat.RequestChatLobbies.LobbySet.LOBBYSET_INVITED);
                    regID = _rpc.ChatGetLobbies(rsctrl.chat.RequestChatLobbies.LobbySet.LOBBYSET_PUBLIC);
                    regID = _rpc.PeersGetFriendList(RequestPeers.SetOption.FRIENDS);
                    _processor.PendingPeerRequests.Add(regID, RequestPeers.SetOption.FRIENDS);
                }

                if (_tickCounter == uint.MaxValue)
                    _tickCounter = 0;
                else
                    _tickCounter++;
            }
        }

        // --------- form actions ----------

        private void bt_connect_Click(object sender, EventArgs e)
        {
            this.Connect();
        }

        private void bt_disconnect_Click(object sender, EventArgs e)
        {
            this.Disconnect();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Disconnect();
            this.SaveForms();
        }

        private void tb_pw_Enter(object sender, EventArgs e)
        {
            tb_pw.Clear();
        }

        private void t_tick_Tick(object sender, EventArgs e)
        {
            Tick();
        }

        private void bt_test_Click(object sender, EventArgs e)
        {
            uint regID;
            //regID = _rpc.GetSystemStatus();
            //regID = _rpc.GetFriendList(RequestPeers.SetOption.OWNID);
            //_processor.PendingPeerRequests.Add(regID, RequestPeers.SetOption.OWNID);
            //regID = _rpc.GetFriendList(RequestPeers.SetOption.FRIENDS);
            //_processor.PendingPeerRequests.Add(regID, RequestPeers.SetOption.FRIENDS);
            regID = _rpc.ChatGetLobbies(rsctrl.chat.RequestChatLobbies.LobbySet.LOBBYSET_PUBLIC);
        }

        #region friends
        // Friends

        private void lb_friends_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_friends.SelectedIndex >= 0)
            {
                ClearPeerForm();
                Person p = _friendList[lb_friends.SelectedIndex];
                _selectedFriend = p;
                foreach (Location l in p.locations)
                {
                    string state = "";
                    switch (l.state)
                    {
                        case ((uint)rsctrl.core.Location.StateFlags.CONNECTED):
                            state = "connected";
                            break;
                        case ((uint)rsctrl.core.Location.StateFlags.ONLINE):
                            state = "online";
                            break;
                        case ((uint)(rsctrl.core.Location.StateFlags.CONNECTED | rsctrl.core.Location.StateFlags.ONLINE)):
                            state = "connected online";
                            break;
                        case ((uint)rsctrl.core.Location.StateFlags.UNREACHABLE):
                            state = "unreachable";
                            break;
                        default:
                            state = "offline";
                            break;
                    }
                    lb_locations.Items.Add(l.location + " - " + state);
                }
                tb_peerName.Text = p.name;
                tb_peerID.Text = p.gpg_id;
            }
        }

        private void lb_locations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_locations.SelectedIndex >= 0)
            {
                Location l = _selectedFriend.locations[lb_locations.SelectedIndex];
                tb_peerLocation.Text = l.location;
                tb_peerLocationID.Text = l.ssl_id;
                tb_peerIPExt.Text = l.extaddr.addr;
                if (PortInRange(l.extaddr.port))
                    nud_peerPortExt.Value = l.extaddr.port;
                tb_peerIPInt.Text = l.localaddr.addr;
                if (PortInRange(l.localaddr.port))
                    nud_peerPortInt.Value = l.localaddr.port;
                tb_dyndns.Text = "NOT IMPLEMENTED";
            }
        }

        private void bt_peerSave_Click(object sender, EventArgs e)
        {
            Person p = _selectedFriend;
            Location l = p.locations[lb_locations.SelectedIndex];
            l.extaddr.addr = tb_peerIPExt.Text;
            l.extaddr.port = Convert.ToUInt16(nud_peerPortExt.Value);
            l.localaddr.addr = tb_peerIPInt.Text;
            l.localaddr.port = Convert.ToUInt16(nud_peerPortInt.Value);
            //dyndns
            p.locations[lb_locations.SelectedIndex] = l;
            uint reqID = _rpc.PeersModifyPeer(p, RequestModifyPeer.ModCmd.ADDRESS);
            // need to save request
        }

        private void bt_peerRemove_Click(object sender, EventArgs e)
        {

        }

        private void bt_peerNew_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region chat
        // chat

        private void bt_chatSend_Click(object sender, EventArgs e)
        {
            if (tb_chatMsg.Text != "")
            {
                SendChatMsg();
            }
            tb_chatMsg.Focus();
        }

        private void clb_chatLobbies_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            GuiChatLobby cl = new GuiChatLobby();
            if (GetLobbyByListIndex(e.Index, out cl))
            {
                if (e.NewValue != CheckState.Indeterminate)
                {
                    if (cl.Joined && e.NewValue == CheckState.Unchecked) // leave lobby
                    {
                        _rpc.ChatJoinLeaveLobby(RequestJoinOrLeaveLobby.LobbyAction.LEAVE_OR_DENY, cl.ID);
                        cl.Joined = false;
                    }
                    else if (!cl.Joined && e.NewValue == CheckState.Checked) // join lobby
                    {
                        _rpc.ChatJoinLeaveLobby(RequestJoinOrLeaveLobby.LobbyAction.JOIN_OR_ACCEPT, cl.ID);
                        cl.Joined = true;
                    }
                    _chatLobbies[cl.ID] = cl;
                    CheckChatRegistration();
                }
            }
        }

        private void clb_chatLobbies_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = clb_chatLobbies.SelectedIndex;
            if (index >= 0)
            {
                GuiChatLobby cl = new GuiChatLobby();
                if (GetLobbyByListIndex(index, out cl))
                {
                    SetChatText(cl.ID);
                    clb_chatUser.Items.Clear();
                    foreach (string s in _chatUser[cl.ID])
                        clb_chatUser.Items.Add(s);
                }
            }
        }

        private void tb_chatMsg_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                bt_chatSend_Click(null, null);
        }

        private void bt_setNickname_Click(object sender, EventArgs e)
        {
            if (tb_chatNickname.Text != "")
                SetNick(tb_chatNickname.Text);
        }

        private void cb_settingsSave_CheckedChanged(object sender, EventArgs e)
        {
            cb_settingsSavePW.Enabled = cb_settingsSave.Checked;
        }

        private void bt_shutdown_Click(object sender, EventArgs e)
        {
            Shutdown();
        }

        #endregion

        #region files

        private void lb_filesDownloads_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lb_filesDownloads.SelectedIndex != -1)
                lb_filesUploads.SelectedIndex = -1;
        }

        private void lb_filesUploads_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lb_filesUploads.SelectedIndex != -1)
                lb_filesDownloads.SelectedIndex = -1;
        }

        private void bt_filesPause_Click(object sender, EventArgs e)
        {
            GuiFileTransfer gft = new GuiFileTransfer();
            if (!GetFileTransferBySelection(out gft))
                return;

            _rpc.FilesControllDownload(RequestControlDownload.Action.ACTION_PAUSE, gft.FileTransfer.file);
        }

        private void bt_filesWait_Click(object sender, EventArgs e)
        {
            GuiFileTransfer gft = new GuiFileTransfer();
            if (!GetFileTransferBySelection(out gft))
                return;

            _rpc.FilesControllDownload(RequestControlDownload.Action.ACTION_WAIT, gft.FileTransfer.file);
        }

        private void bt_filesContinue_Click(object sender, EventArgs e)
        {
            GuiFileTransfer gft = new GuiFileTransfer();
            if (!GetFileTransferBySelection(out gft))
                return;

            _rpc.FilesControllDownload(RequestControlDownload.Action.ACTION_CONTINUE, gft.FileTransfer.file);
        }
        
        private void bt_filesForceCheck_Click(object sender, EventArgs e)
        {
            GuiFileTransfer gft = new GuiFileTransfer();
            if (!GetFileTransferBySelection(out gft))
                return;

            _rpc.FilesControllDownload(RequestControlDownload.Action.ACTION_CHECK, gft.FileTransfer.file);
        }

        private void bt_filesRestart_Click(object sender, EventArgs e)
        {
            GuiFileTransfer gft = new GuiFileTransfer();
            if (!GetFileTransferBySelection(out gft))
                return;

            _rpc.FilesControllDownload(RequestControlDownload.Action.ACTION_RESTART, gft.FileTransfer.file);
        }

        private void bt_filesCancel_Click(object sender, EventArgs e)
        {
            GuiFileTransfer gft = new GuiFileTransfer();
            if (!GetFileTransferBySelection(out gft))
                return;

            _rpc.FilesControllDownload(RequestControlDownload.Action.ACTION_CANCEL, gft.FileTransfer.file);
        }

        #endregion

        #region search

        private void bt_searchSearch_Click(object sender, EventArgs e)
        {
            Search(tb_searchKeyWords.Text);
            tb_searchKeyWords.Clear();
        }

        private void lb_searches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_searches.SelectedIndex != -1)
                UpdateSearchResults(lb_searches.SelectedIndex);
        }

        private void tb_searchKeyWords_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                bt_searchSearch_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lb_searches.SelectedIndex == -1)
                return;

            GuiSearch gs = new GuiSearch();
            if (GetSearchByIndex((ushort)lb_searches.SelectedIndex, out gs))
            {
                _rpc.SearchClose(gs.ID);
                _searches.Remove(gs.ID);
                UpdateSearches();
                UpdateSearchResults(-1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lb_searchResults.SelectedIndex == -1)
                return;

            GuiSearch gs = new GuiSearch();
            if (GetSearchByIndex((ushort)lb_searches.SelectedIndex, out gs))
                _rpc.FilesControllDownload(RequestControlDownload.Action.ACTION_START, gs.Results[lb_searchResults.SelectedIndex].file);
        }

        #endregion

    }
}