using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Sehraf.RSRPC;

//using ProtoBuf;

using rsctrl.chat;
using rsctrl.core;
using rsctrl.system;
using rsctrl.peers;

namespace RetroShareSSHClient
{

    public partial class MainForm : Form
    {
        const bool DEBUG = true;
        const string GROUPCHAT = "%groupChat%";

        RSRPC _rpc;
        Processor _processor;
        Settings _settings;
        Person _owner;
        List<Person> _friendList;
        List<ChatLobbyInfo> _chatLobbies;
        List<string> _chatText;
        List<List<string>> _chatUser;
        ushort _chatsJoined;
        string _nick;
        Person _selectedFriend;
        uint _tickCounter;

        public MainForm()
        {
            InitializeComponent();
            cb_con.CheckState = CheckState.Unchecked;

            _rpc = new RSRPC();
            _rpc.ErrorOccurred += ErrorFromThread;
            _rpc.ReceivedMsg += ProcessMsgFromThread;
            _processor = new Processor(this);
            _settings = new Settings();
            _owner = new Person();
            _friendList = new List<Person>();
            _chatLobbies = new List<ChatLobbyInfo>();
            _chatText = new List<string>();
            _chatUser = new List<List<string>>();
            _chatsJoined = 0;
            _selectedFriend = new Person();
            _tickCounter = 0;

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
        }

        private void ProcessMsgFromThread(RSProtoBuffSSHMsg msg)
        {           
            this.Invoke((MethodInvoker)delegate { _processor.ProcessMsg(msg); });
        }

        private void ConnectionEstablished()
        {
            t_tick.Start();
            uint regID;
            regID = _rpc.GetSystemStatus();
            regID = _rpc.GetFriendList(RequestPeers.SetOption.OWNID);
            _processor.PendingPeerRequests.Add(regID, RequestPeers.SetOption.OWNID);
            SetNick(_nick);

            if (_chatLobbies.Count == 0)
            {
                ChatLobbyInfo lobby = new ChatLobbyInfo();
                lobby.lobby_name = "Group chat";
                lobby.lobby_id = GROUPCHAT;

                _chatLobbies.Add(lobby);
                _chatText.Add(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + "\n");
                _chatUser.Add(new List<string> { });
                clb_chatLobbies.Items.Add("Group chat", true);
                CheckChatRegistration(true);
            }
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

        private void Disconnect()
        {
            if (!_rpc.IsConnected)
                return;
            t_tick.Stop();
            _rpc.Disconnect();
            tb_out.Text = "disconnected!";
            cb_con.CheckState = CheckState.Unchecked;
        }

        // --------- chat ---------

        public void UpdateChatLobbies(ResponseChatLobbies msg)
        {
            if (msg.status != null && msg.status.code == Status.StatusCode.SUCCESS)            
                foreach (ChatLobbyInfo lobby in msg.lobbies)                
                    ProcessLobby(lobby);            
        }

        public void LobbyInvite(EventLobbyInvite msg)
        {
            ProcessLobby(msg.lobby);
        }

        private void ProcessLobby(ChatLobbyInfo lobby) {
            ChatLobbyInfo oldLobby;
            int index;
            if (GetLobbyByID(lobby.lobby_id, out oldLobby, out index)) //update
            {
                System.Diagnostics.Debug.WriteLineIf(DEBUG, "updating lobby " + index + " - " + lobby.lobby_name);
                System.Diagnostics.Debug.WriteLineIf(DEBUG, "user: " + lobby.no_peers + " - names: " + lobby.nicknames.Count + " - friends: " + lobby.participating_friends.Count);
                _chatLobbies[index] = lobby;
                //_chatUser[index] = lobby.nicknames;
                _chatUser[index] = lobby.participating_friends;
                clb_chatLobbies.Items[index] = "(" + lobby.no_peers + ") " + lobby.lobby_name + " - " + lobby.lobby_topic;
            }
            else //new 
            {
                System.Diagnostics.Debug.WriteLineIf(DEBUG, "adding lobby " + index + " - " + lobby.lobby_name);
                System.Diagnostics.Debug.WriteLineIf(DEBUG, "user: " + lobby.no_peers + " - names: " + lobby.nicknames.Count + " - friends: " + lobby.participating_friends.Count);
                _chatLobbies.Add(lobby);
                _chatText.Add("======= " + DateTime.Now.ToLongDateString() + " - " + lobby.lobby_name + " =======\n");
                //_chatUser.Add(lobby.nicknames);
                _chatUser.Add(lobby.participating_friends);

                // seems not to work at the monent 
                if (lobby.lobby_state == ChatLobbyInfo.LobbyState.LOBBYSTATE_JOINED)
                {
                    clb_chatLobbies.Items.Add("(" + lobby.no_peers + ") " + lobby.lobby_name + " - " + lobby.lobby_topic, true);
                    //_rpc.JoinLeaveLobby(RequestJoinOrLeaveLobby.LobbyAction.JOIN_OR_ACCEPT, lobby.lobby_id);
                    CheckChatRegistration(true);
                }
                else
                    clb_chatLobbies.Items.Add("(" + lobby.no_peers + ") " + lobby.lobby_name + " - " + lobby.lobby_topic, false);

                if (lobby.lobby_state == ChatLobbyInfo.LobbyState.LOBBYSTATE_INVITED)
                    clb_chatLobbies.SetItemCheckState(index, CheckState.Indeterminate);
            }
        }

        public void SendChatMsg(string inMsg = "")
        {
            int index = clb_chatLobbies.SelectedIndex;
            if (index >= 0 && clb_chatLobbies.GetItemChecked(index))
            {
                string text = (inMsg == "") ? tb_chatMsg.Text : inMsg;
                ChatId id = new ChatId();
                if (_chatLobbies[index].lobby_id == GROUPCHAT)
                {
                    id.chat_id = "";
                    id.chat_type = ChatType.TYPE_GROUP;
                }
                else
                {
                    id.chat_id = _chatLobbies[index].lobby_id;
                    id.chat_type = ChatType.TYPE_LOBBY;                    
                }
                ChatMessage msg = new ChatMessage();
                msg.id = id;
                msg.msg = text;
                msg.peer_nickname = _nick;
                msg.send_time = (uint)DateTime.Now.Second;

                _rpc.SendMsg(msg);
                PrintMsgToLobby(_chatLobbies[index].lobby_id, DateTime.Now.ToLongTimeString() + " - " + _nick + " > " + text + "\n");
                if (inMsg == "")                
                    tb_chatMsg.Clear();                
            }
        }

        public void PrintMsgToLobby(string ID, EventChatMessage response)
        {
            int index;
            ChatLobbyInfo lobby;
            if (GetLobbyByID(ID, out lobby, out index))
            {
                string msg = Processor.RemoteTags(response.msg.msg);
                _chatText[index] += DateTime.Now.ToLongTimeString() + " - " + response.msg.peer_nickname + " > " + msg + "\n";
                if (clb_chatLobbies.GetItemChecked(index))
                {
                    if (clb_chatLobbies.SelectedIndex == index)
                        SetChatText(index);
                    else
                        clb_chatLobbies.SetItemCheckState(index, CheckState.Indeterminate);
                    AutoAnswer(Processor.RemoteTags(response.msg.msg));
                }
                else
                    clb_chatLobbies.SetItemCheckState(index, CheckState.Indeterminate);
            }
            // else drop msg
        }

        public void PrintMsgToLobby(string ID, string msg)
        {
            int index;
            ChatLobbyInfo lobby;
            if (GetLobbyByID(ID, out lobby, out index))
            {
                _chatText[index] += msg;
                if (clb_chatLobbies.SelectedIndex == index)
                    SetChatText(index);
            }
            // else drop msg
        }

        public void PrintMsgToGroupChat(EventChatMessage response)
        {
            _chatText[0] += DateTime.Now.ToShortTimeString() + " - " + response.msg.peer_nickname + " > " + Processor.RemoteTags(response.msg.msg) + "\n";
            if (clb_chatLobbies.SelectedIndex == 0)
                SetChatText(0);
            AutoAnswer(Processor.RemoteTags(response.msg.msg));
        }

        private void SetChatText(int index)
        {
            rtb_chat.Clear();
            //rtb_chat.Rtf =@"{\rtf1\ansi " + _chatText[index] + "}";
            rtb_chat.Text = _chatText[index];
            rtb_chat.SelectionStart = rtb_chat.Text.Length;
            rtb_chat.ScrollToCaret();
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
                for (ushort i = 1; i < _chatLobbies.Count; i++)
                    if (clb_chatLobbies.GetItemChecked(i))
                        ids.Add(_chatLobbies[i].lobby_id);

                if (ids.Count == 0)
                    _rpc.SetLobbyNickname(_nick);
                else
                    _rpc.SetLobbyNickname(_nick, ids);
            }
        }

        // ---------- peers ----------

        public void UpdateSystemStatus(ResponseSystemStatus msg)
        {
            if (msg.status != null && msg.status.code == Status.StatusCode.SUCCESS)
            {
                l_systemstatus.Text = "Running!";

                switch (msg.net_status)
                {
                    case ResponseSystemStatus.NetCode.BAD_NATSYM:
                    case ResponseSystemStatus.NetCode.BAD_NODHT_NAT:
                    case ResponseSystemStatus.NetCode.BAD_OFFLINE:
                    case ResponseSystemStatus.NetCode.BAD_UNKNOWN:
                        l_network.Text = "BAD :(";
                        break;
                    case ResponseSystemStatus.NetCode.WARNING_NATTED:
                    case ResponseSystemStatus.NetCode.WARNING_NODHT:
                    case ResponseSystemStatus.NetCode.WARNING_RESTART:
                        l_network.Text = "Warning :|";
                        break;
                    case ResponseSystemStatus.NetCode.ADV_FORWARD:
                    case ResponseSystemStatus.NetCode.GOOD:
                        l_network.Text = "GOD :)";
                        break;
                }

                l_connected.Text = "Connected: " + Convert.ToString(msg.no_connected);
                l_peers.Text = "Peers: " + Convert.ToString(msg.no_peers);

                l_bwUp.Text = "Up: " + String.Format("{0:0,0.00}", msg.bw_total.up) + "kb/s";
                l_bwDown.Text = "Down: " + String.Format("{0:0,0.00}", msg.bw_total.down) + "kb/s";
            }
            else
            {
                l_systemstatus.Text = "Error!";
            }
        }

        public void UpdatePeerList(ResponsePeerList msg)
        {
            if (msg.status != null && msg.status.code == Status.StatusCode.SUCCESS)
            {
                int index1 = lb_friends.SelectedIndex, index2 = lb_locations.SelectedIndex;
                lb_friends.Items.Clear();
                ClearPeerForm();
                _friendList.Clear();
                //List<Person> persons = new List<Person>();
                List<Location> locs = new List<rsctrl.core.Location>();
                _friendList = msg.peers;
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
            else
            {
                lb_friends.Items.Clear();
                lb_friends.Items.Add("Error");
            }
        }

        public void SetOwnID(ResponsePeerList msg)
        {
            _owner = msg.peers[0]; // i gues we just have one owner           
            this.Text = "RetroShare SSH Client - " + _owner.name + "(" + _owner.gpg_id + ") ";// +l.location + " (" + l.ssl_id + ")";

            if (_nick == "" || _nick == null)
            {
                string name = _owner.name.Trim();
                SetNick(name + " (nogui/ssh)");
            }
        }
        
        // --------- ---------

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

        private void CheckChatRegistration(bool joined)
        {
            if (joined)
            {
                if (_chatsJoined == 0)
                {
                    _rpc.RegisterEvent(RequestRegisterEvents.RegisterAction.REGISTER);
                    tb_out.AppendText("Registered" + "\n");
                }
                _chatsJoined++;
            }
            else
            {
                if (_chatsJoined == 1)
                {
                    _rpc.RegisterEvent(RequestRegisterEvents.RegisterAction.DEREGISTER);
                    tb_out.AppendText("Unregistered" + "\n");
                }
                _chatsJoined--;
            }
        }
        
        private bool GetLobbyByID(string ID, out ChatLobbyInfo lobby, out int index)
        {
            foreach (ChatLobbyInfo l in _chatLobbies)
            {
                if (l.lobby_id == ID)
                {
                    lobby = l;
                    index = _chatLobbies.IndexOf(l);
                    return true;
                }
            }
            lobby = null;
            index = 0;
            return false;
        }

        private bool PortInRange(uint port)
        {
            return (port >= 1024 && port <= UInt16.MaxValue);
        }

        private void Tick()
        {
            if (_rpc.IsConnected)
            {
                uint regID;

                regID = _rpc.GetSystemStatus();

                if (_tickCounter % 60 == 0)
                {
                    regID = _rpc.GetChatLobbies(rsctrl.chat.RequestChatLobbies.LobbySet.LOBBYSET_JOINED);
                    regID = _rpc.GetChatLobbies(rsctrl.chat.RequestChatLobbies.LobbySet.LOBBYSET_PUBLIC);
                    regID = _rpc.GetChatLobbies(rsctrl.chat.RequestChatLobbies.LobbySet.LOBBYSET_INVITED);
                    regID = _rpc.GetFriendList(RequestPeers.SetOption.FRIENDS);
                    _processor.PendingPeerRequests.Add(regID, RequestPeers.SetOption.FRIENDS);
                }
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
            regID = _rpc.GetChatLobbies(rsctrl.chat.RequestChatLobbies.LobbySet.LOBBYSET_PUBLIC);
        }

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
            uint reqID = _rpc.ModifyPeer(p, RequestModifyPeer.ModCmd.ADDRESS);
            // need to save request
        }

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
            ChatLobbyInfo lobby = _chatLobbies[e.Index];
            bool joined = e.NewValue == CheckState.Checked;
            if (joined)            
                _rpc.JoinLeaveLobby(RequestJoinOrLeaveLobby.LobbyAction.JOIN_OR_ACCEPT, lobby.lobby_id);            
            else         
                _rpc.JoinLeaveLobby(RequestJoinOrLeaveLobby.LobbyAction.LEAVE_OR_DENY, lobby.lobby_id);

            CheckChatRegistration(joined);
        }

        private void clb_chatLobbies_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = clb_chatLobbies.SelectedIndex;
            if (index >= 0)
            {                
                SetChatText(index);
                clb_chatUser.Items.Clear();
                foreach (string s in _chatUser[index])                
                    clb_chatUser.Items.Add(s);                
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
    }
}