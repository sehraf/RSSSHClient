using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Sehraf.RetroShareSSH;

//using ProtoBuf;

using rsctrl.chat;
using rsctrl.core;
using rsctrl.system;
using rsctrl.peers;

namespace RetroShareSSHClient
{

    public partial class MainForm : Form
    {
        const bool PINGSERVER = true;
        const bool DEBUG = true;
        const string GROUPCHAT = "%groupChat%";

        RSRPC _rpc;
        Processor _processor;
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
            _rpc.GenericCallback += Callback;
            _rpc.ReceivedMsg += ProcessMsgFromThread;
            _processor = new Processor(this);
            _owner = new Person();
            _friendList = new List<Person>();
            _chatLobbies = new List<ChatLobbyInfo>();
            _chatText = new List<string>();
            _chatUser = new List<List<string>>();
            _chatsJoined = 0;
            _nick = "sehraf nogui (ssh)";
            _selectedFriend = new Person();
            _tickCounter = 0;
        }

        //private void TestMsgID()
        //{
        //    uint msgID = RSProtoBuf.ConstructMsgId(14, 1337, 11, true);
        //    tb_out.Text = RSProtoBuf.GetHex(msgID) + "\n";
        //    tb_out.AppendText("Ext : " + RSProtoBuf.GetRpcMsgIdExtension(msgID) + "\n");
        //    tb_out.AppendText("Service : " + RSProtoBuf.GetRpcMsgIdService(msgID) + "\n");
        //    tb_out.AppendText("subMsg : " + RSProtoBuf.GetRpcMsgIdSubMsg(msgID) + "\n");
        //    tb_out.AppendText("respond : " + RSProtoBuf.IsRpcMsgIdResponse(msgID).ToString() + "\n");
        //}
        
        //private void TestProcessPBMsg(RSProtoBuffSSHMsg msg)
        //{
        //    byte extension = RSProtoBuf.GetRpcMsgIdExtension(msg.MsgID);
        //    ushort service = RSProtoBuf.GetRpcMsgIdService(msg.MsgID);
        //    byte submsg = RSProtoBuf.GetRpcMsgIdSubMsg(msg.MsgID);

        //    Status status = null;

        //    switch (extension)
        //    {
        //        case (byte)rsctrl.core.ExtensionId.CORE:
        //            switch (service)
        //            {
        //                case (ushort)rsctrl.core.PackageId.PEERS:
        //                    List<Person> peers = new List<Person>();
        //                    switch (submsg)
        //                    {
        //                        case (byte)rsctrl.peers.RequestMsgIds.MsgId_RequestAddPeer:
        //                            tb_out.AppendText("AddPeer:" + "\n");
        //                            ResponseAddPeer responseAdd = Serializer.Deserialize<ResponseAddPeer>(msg.ProtoBuffMsg);
        //                            peers = responseAdd.peers;
        //                            status = responseAdd.status;
        //                            break;
        //                        case (byte)rsctrl.peers.RequestMsgIds.MsgId_RequestModifyPeer:
        //                            tb_out.AppendText("ModifyPeer:" + "\n");
        //                            ResponseModifyPeer responseModify = Serializer.Deserialize<ResponseModifyPeer>(msg.ProtoBuffMsg);
        //                            peers = responseModify.peers;
        //                            status = responseModify.status;
        //                            break;
        //                        case (byte)rsctrl.peers.RequestMsgIds.MsgId_RequestPeers:
        //                            tb_out.AppendText("Peers:" + "\n");
        //                            ResponsePeerList responsePeers = Serializer.Deserialize<ResponsePeerList>(msg.ProtoBuffMsg);
        //                            peers = responsePeers.peers;
        //                            status = responsePeers.status;
        //                            break;
        //                        default:
        //                            //...
        //                            break;
        //                    }

        //                    foreach (Person p in peers)
        //                    {
        //                        tb_out.AppendText(p.name + ":" + "\n");
        //                        List<Location> locs = p.locations;
        //                        foreach (Location l in locs)
        //                            tb_out.AppendText(" - " + l.location + ": SSLID: " + l.ssl_id + "\n");
        //                    }

        //                    break;
        //                case (ushort)rsctrl.core.PackageId.SYSTEM:
        //                    switch (submsg)
        //                    {
        //                        case (byte)rsctrl.system.ResponseMsgIds.MsgId_ResponseSystemStatus:
        //                            ResponseSystemStatus responseSystem = Serializer.Deserialize<ResponseSystemStatus>(msg.ProtoBuffMsg);
        //                            tb_out.AppendText("SystemStatus:" + "\n");
        //                            tb_out.AppendText(responseSystem.bw_total.name + " -> up: " + responseSystem.bw_total.up + " - down: " + responseSystem.bw_total.down + "\n");
        //                            tb_out.AppendText("net status: " + responseSystem.net_status + "\n");
        //                            tb_out.AppendText("connected: " + responseSystem.no_connected + "\n");
        //                            tb_out.AppendText("peers: " + responseSystem.no_peers + "\n");
        //                            status = responseSystem.status;
        //                            break;
        //                        default:
        //                            //...
        //                            break;
        //                    }
        //                    break;
        //                default:
        //                    // HOW COULD THIS HAPPEN? - ok now that bad 
        //                    break;
        //            }
        //            break;
        //        default:
        //            // HOW COULD THIS HAPPEN?
        //            break;
        //    }
        //    if (status != null)
        //    {
        //        tb_out.AppendText("----- Satus ----- " + "\n");
        //        tb_out.AppendText("- code: " + status.code + "\n");
        //        tb_out.AppendText("- msg: " + status.msg + "\n");
        //    }
        //}

        private void CallbackFromThread(CallbackType type)
        {
            this.Invoke((MethodInvoker)delegate { Callback(type); });
        }

        private void Callback(CallbackType type)
        {
            switch (type)
            {
                case CallbackType.Disconnect:
                    this.Invoke((MethodInvoker)delegate { Disconnect(); });
                    break;
                default:
                    System.Diagnostics.Debug.WriteLineIf(DEBUG, "Callback: unknown type " + type);
                    break;
            }
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

            ChatLobbyInfo lobby = new ChatLobbyInfo();
            lobby.lobby_name = "Group chat";
            lobby.lobby_id = GROUPCHAT;

            _chatLobbies.Add(lobby);
            _chatText.Add(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + "\n");
            _chatUser.Add(new List<string> { });
            clb_chatLobbies.Items.Add("Group chat");
        }

        private void Connect()
        {
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
            t_tick.Stop();
            _rpc.Disconnect();
            tb_out.Text = "disconnected!";
            cb_con.CheckState = CheckState.Unchecked;
        }

        // --------- chat ---------

        public void UpdateChatLobbies(ResponseChatLobbies msg)
        {
            if (msg.status != null && msg.status.code == Status.StatusCode.SUCCESS)
            {
                ChatLobbyInfo oldLobby;
                int index;
                foreach (ChatLobbyInfo lobby in msg.lobbies)
                {
                    if (GetLobbyByID(lobby.lobby_id, out oldLobby, out index)) //update
                    {
                        System.Diagnostics.Debug.WriteLineIf(DEBUG, "updating lobby " + index + " - " + lobby.lobby_name);
                        System.Diagnostics.Debug.WriteLineIf(DEBUG, "user: " + lobby.no_peers + " - names: " + lobby.nicknames.Count);
                        _chatLobbies[index] = lobby;
                        _chatUser[index] = lobby.nicknames;
                        clb_chatLobbies.Items[index] = "(" + lobby.no_peers + ") " + lobby.lobby_name + " - " + lobby.lobby_topic;
                    }
                    else //new 
                    {
                        System.Diagnostics.Debug.WriteLineIf(DEBUG, "adding lobby " + index + " - " + lobby.lobby_name);
                        System.Diagnostics.Debug.WriteLineIf(DEBUG, "user: " + lobby.no_peers + " - names: " + lobby.nicknames.Count);
                        _chatLobbies.Add(lobby);
                        _chatText.Add("======= " + DateTime.Now.ToLongDateString() + " - " + lobby.lobby_name + " =======\n");
                        _chatUser.Add(lobby.nicknames);

                        // seems not to work at the monent 
                        //if (lobby.lobby_state == ChatLobbyInfo.LobbyState.LOBBYSTATE_JOINED)
                        //{
                        //    clb_chatLobbies.Items.Add("(" + lobby.no_peers + ") " + lobby.lobby_name + " - " + lobby.lobby_topic, true);
                        //    //_rpc.JoinLeaveLobby(RequestJoinOrLeaveLobby.LobbyAction.JOIN_OR_ACCEPT, lobby.lobby_id);
                        //    CheckChatRegistration(true);
                        //}
                        //else
                        clb_chatLobbies.Items.Add("(" + lobby.no_peers + ") " + lobby.lobby_name + " - " + lobby.lobby_topic, false);
                    }
                }                
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
                if (clb_chatLobbies.GetItemChecked(index))
                {
                    string msg = Processor.RemoteTags(response.msg.msg);
                    _chatText[index] += DateTime.Now.ToLongTimeString() + " - " + response.msg.peer_nickname + " > " + msg + "\n";
                    if (clb_chatLobbies.SelectedIndex == index)
                        SetChatText(index);

                    if (msg == "ping" && PINGSERVER)
                        SendChatMsg("pong");
                }
                // else drop msg
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
        }

        private void SetChatText(int index)
        {
            rtb_chat.Clear();
            //rtb_chat.Rtf =@"{\rtf1\ansi " + _chatText[index] + "}";
            rtb_chat.Text = _chatText[index];
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

            _nick = _owner.name + "(nogui/ssh)";
            tb_chatNickname.Text = _nick;
            bt_setNickname_Click(null, null);
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
        }

        private void tb_pw_Enter(object sender, EventArgs e)
        {
            tb_pw.Clear();
        }

        private void t_tick_Tick(object sender, EventArgs e)
        {
            if (_rpc.IsConnected)
            {
                uint regID;

                regID = _rpc.GetSystemStatus();

                if (_tickCounter % 60 == 0)
                {
                    regID = _rpc.GetChatLobbies(rsctrl.chat.RequestChatLobbies.LobbySet.LOBBYSET_PUBLIC);
                    regID = _rpc.GetFriendList(RequestPeers.SetOption.FRIENDS);
                    _processor.PendingPeerRequests.Add(regID, RequestPeers.SetOption.FRIENDS);
                }
                //if (_tickCounter % 15 == 0)
                //{
                //    regID = _rpc.GetChatLobbies(rsctrl.chat.RequestChatLobbies.LobbySet.LOBBYSET_PUBLIC);
                //}
                _tickCounter++;
            }
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
                lb_chatUser.Items.Clear();
                foreach (string s in _chatUser[index])                
                    lb_chatUser.Items.Add(s);                
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
            {
                _nick = tb_chatNickname.Text;

                List<string> ids = new List<string>();
                for (ushort i = 0; i < _chatLobbies.Count; i++)
                    if (clb_chatLobbies.GetItemChecked(i))
                        ids.Add(_chatLobbies[i].lobby_id);

                _rpc.SetLobbyNickname(_nick, ids);
            }
        }
    }
}