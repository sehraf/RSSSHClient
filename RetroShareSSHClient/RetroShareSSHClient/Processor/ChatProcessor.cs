using System;
using System.Collections.Generic;
using System.Windows.Forms;

using rsctrl.chat;
using rsctrl.core;

namespace RetroShareSSHClient
{
    struct GuiChatLobby
    {
        ChatLobbyInfo _lobby;
        bool _joined;
        bool _unread;
        ushort _listIndex;
        string _chatText;
        List<string> _chatUser;

        public ChatLobbyInfo Lobby { get { return _lobby; } set { _lobby = value; } }
        public string ID { get { return _lobby.lobby_id; } }
        public bool Joined { get { return _joined; } set { _joined = value; } }
        public bool Unread { get { return _unread; } set { _unread = value; } }
        public ushort Index { get { return _listIndex; } set { _listIndex = value; } }
        public string ChatText { get { return _chatText; } set { _chatText = value; } }
        public List<string> ChatUser { get { return _chatUser; } set { _chatUser = value; } }
    }

    internal class ChatProcessor
    {
        const bool DEBUG = false;
        const string GROUPCHAT = "%groupChat%";

        Dictionary<string, GuiChatLobby> _chatLobbies;
        bool _isRegistered;
        string _nick;

        Bridge _b;

        public string Nick { get { return _nick; } }

        public ChatProcessor(Bridge bridge)
        {
            _b = bridge;

            _chatLobbies = new Dictionary<string, GuiChatLobby>();
            _isRegistered = false;
        }

        public void Reset(bool justResetChatRegistration = false)
        {
            if (!justResetChatRegistration)
            {
                _chatLobbies.Clear();
                _b.GUI.clb_chatLobbies.Items.Clear();
                _b.GUI.clb_chatUser.Items.Clear();
            }
            _isRegistered = false;
        }
        
        public void UpdateChatLobbies(ResponseChatLobbies msg)
        {
            System.Diagnostics.Debug.WriteLineIf(DEBUG, "lobbyList "+ msg.lobbies.Count);
            foreach (ChatLobbyInfo lobby in msg.lobbies)
                ProcessLobby(lobby);

            CheckChatRegistration();
        }

        public void LobbyInvite(EventLobbyInvite msg)
        {
            System.Diagnostics.Debug.WriteLineIf(DEBUG, "-> invited");
            ProcessLobby(msg.lobby);
            CheckChatRegistration();
        }

        private void ProcessLobby(ChatLobbyInfo lobby)
        {
            string ID = lobby.lobby_id;
            GuiChatLobby cl = new GuiChatLobby();
            if (_chatLobbies.ContainsKey(ID)) //update
            {
                System.Diagnostics.Debug.WriteLineIf(DEBUG, "updating lobby " + lobby.lobby_name + " - state " + lobby.lobby_state);
                System.Diagnostics.Debug.WriteLineIf(DEBUG, "user: " + lobby.no_peers + " - names: " + lobby.nicknames.Count + " - friends: " + lobby.participating_friends.Count);
                cl = _chatLobbies[ID];
                cl.Lobby = lobby;
                cl.ChatUser = lobby.participating_friends;
                _chatLobbies[ID] = cl;
                _b.GUI.clb_chatLobbies.Items[cl.Index] = "(" + lobby.no_peers + ") " + lobby.lobby_name + " - " + lobby.lobby_topic;

                if (lobby.lobby_state == ChatLobbyInfo.LobbyState.LOBBYSTATE_INVITED && !cl.Joined)
                    _b.GUI.clb_chatLobbies.SetItemCheckState(cl.Index, CheckState.Indeterminate);
            }
            else //new 
            {
                System.Diagnostics.Debug.WriteLineIf(DEBUG, "adding lobby " + lobby.lobby_name + " - state " + lobby.lobby_state);
                System.Diagnostics.Debug.WriteLineIf(DEBUG, "user: " + lobby.no_peers + " - names: " + lobby.nicknames.Count + " - friends: " + lobby.participating_friends.Count);
                cl.Lobby = lobby;
                cl.Index = (ushort)_b.GUI.clb_chatLobbies.Items.Count;
                cl.Joined = (lobby.lobby_state == ChatLobbyInfo.LobbyState.LOBBYSTATE_JOINED);
                {
                    _b.GUI.clb_chatLobbies.Items.Add(cl.Lobby.lobby_name, cl.Joined);
                    cl.Index = (ushort)_b.GUI.clb_chatLobbies.Items.IndexOf(cl.Lobby.lobby_name);
                    if (lobby.lobby_state == ChatLobbyInfo.LobbyState.LOBBYSTATE_INVITED)
                        _b.GUI.clb_chatLobbies.SetItemCheckState(cl.Index, CheckState.Indeterminate);
                }
                cl.Unread = false;
                cl.ChatText = "======= " + DateTime.Now.ToLongDateString() + " - " + lobby.lobby_name + " =======\n";
                cl.ChatUser = lobby.participating_friends;
                _chatLobbies.Add(cl.ID, cl);
            }
        }

        private void CheckChatRegistration()
        {
            /*
             * If we register to early the server may send us messages from lobbies we don't know yet.
             * AddGroupChat() will call this function before we requested the lobby list.
             * If we don't get any lobby after 2 minutes ( +5 secs for answer) then we don't reach any other lobby -> we can register
             * otherwise the server will send the lobbies after the first request ( if participating ) or a bit later
             */

            if (_chatLobbies.Count <= 1 && _b.GUI.TickCounter < 125)
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
                _b.RPC.ChatRegisterEvent(RequestRegisterEvents.RegisterAction.REGISTER);
                _b.GUI.tb_out.AppendText("Registered" + "\n");
                _isRegistered = true;
            }

            if (!joined && _isRegistered)
            {
                _b.RPC.ChatRegisterEvent(RequestRegisterEvents.RegisterAction.DEREGISTER);
                _b.GUI.tb_out.AppendText("Unregistered" + "\n");
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

        public void SendChatMsg(string inMsg = "")
        {
            int index = _b.GUI.clb_chatLobbies.SelectedIndex;
            if (index >= 0 && _b.GUI.clb_chatLobbies.GetItemChecked(index))
            {
                GuiChatLobby cl = new GuiChatLobby();
                if (GetLobbyByListIndex(index, out cl))
                {
                    string text = (inMsg == "") ? _b.GUI.tb_chatMsg.Text : inMsg;

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

                    _b.RPC.ChatSendMsg(msg);
                    if(cl.ID != GROUPCHAT) // needed ?!
                        PrintMsgToLobby(cl.ID, DateTime.Now.ToLongTimeString() + " - " + _nick + " > " + text + "\n");
                    if (inMsg == "")
                        _b.GUI.tb_chatMsg.Clear();
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
            cl.ChatText += DateTime.Now.ToLongTimeString() + " - " + response.msg.peer_nickname + " > " + msg + "\n";
            if (_b.GUI.clb_chatLobbies.SelectedIndex == cl.Index)
            {
                _chatLobbies[ID] = cl;
                SetChatText(ID);
            }
            else
                if (!cl.Unread)
                {
                    cl.Unread = true;
                    _b.GUI.clb_chatLobbies.SetItemCheckState(cl.Index, CheckState.Indeterminate);
                    _chatLobbies[ID] = cl;
                }

            AutoAnswer(Processor.RemoteTags(response.msg.msg));
        }

        public void PrintMsgToLobby(string ID, string msg)
        {
            GuiChatLobby cl = _chatLobbies[ID];
            cl.ChatText += msg;
            _chatLobbies[ID] = cl;
            if (_b.GUI.clb_chatLobbies.SelectedIndex == cl.Index)
                SetChatText(ID);
        }

        public void PrintMsgToGroupChat(EventChatMessage response)
        {
            GuiChatLobby cl = _chatLobbies[GROUPCHAT];
            cl.ChatText += DateTime.Now.ToShortTimeString() + " - " + response.msg.peer_nickname + " > " + Processor.RemoteTags(response.msg.msg) + "\n";

            if (_b.GUI.clb_chatLobbies.SelectedIndex == 0)
            {
                _chatLobbies[GROUPCHAT] = cl;
                SetChatText(GROUPCHAT);
            }
            else
            {
                if (!cl.Unread)
                {
                    cl.Unread = true;
                    _b.GUI.clb_chatLobbies.SetItemCheckState(cl.Index, CheckState.Indeterminate);
                }
                _chatLobbies[GROUPCHAT] = cl;
            }
            AutoAnswer(Processor.RemoteTags(response.msg.msg));
        }

        private void SetChatText(string ID)
        {
            //rtb_chat.Clear();
            //rtb_chat.Rtf =@"{\rtf1\ansi " + _chatText[index] + "}";
            _b.GUI.rtb_chat.Text = _chatLobbies[ID].ChatText;
            _b.GUI.rtb_chat.SelectionStart = _b.GUI.rtb_chat.Text.Length;
            _b.GUI.rtb_chat.ScrollToCaret();

            GuiChatLobby cl = _chatLobbies[ID];
            cl.Unread = false;
            _chatLobbies[ID] = cl;
            if (cl.Joined && _b.GUI.clb_chatLobbies.GetItemCheckState(cl.Index) == CheckState.Indeterminate)
                _b.GUI.clb_chatLobbies.SetItemCheckState(cl.Index, CheckState.Checked);
        }

        private void AutoAnswer(string msg)
        {
            if (_b.GUI.cb_chatAutoRespEnable.Checked && _b.GUI.tb_chatAutoRespSearch.Text != "" && _b.GUI.tb_chatAutoRespAnswer.Text != "")
            {
                if (msg.ToLower().Contains(_b.GUI.tb_chatAutoRespSearch.Text.ToLower()))
                    SendChatMsg(_b.GUI.tb_chatAutoRespAnswer.Text);
            }
        }

        public void SetNick(string nick)
        {
                _nick = nick;
            _b.GUI.tb_chatNickname.Text = _nick;

            if (_b.RPC.IsConnected)
            {
                List<string> ids = new List<string>();
                Dictionary<string, GuiChatLobby>.ValueCollection lobbies = _chatLobbies.Values;
                foreach (GuiChatLobby lobby in lobbies)
                    if (_b.GUI.clb_chatLobbies.GetItemChecked(lobby.Index))
                        if (lobby.ID != GROUPCHAT)
                            ids.Add(lobby.ID);

                if (ids.Count == 0)
                    _b.RPC.ChatSetLobbyNickname(_nick);
                else
                    _b.RPC.ChatSetLobbyNickname(_nick, ids);
            }
        }

        public void AddGroupChat()
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
                    _b.GUI.clb_chatLobbies.Items.Add(cl.Lobby.lobby_name, true);
                    cl.Index = (ushort)_b.GUI.clb_chatLobbies.Items.IndexOf(cl.Lobby.lobby_name);
                }
                cl.ChatText= DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + "\n";
                cl.ChatUser = new List<string> { };
                _chatLobbies.Add(cl.ID, cl);
                CheckChatRegistration();
            }
        }

        public void ToggleCheckboxes()
        {
            GuiChatLobby[] values = new GuiChatLobby[_chatLobbies.Count];
            _chatLobbies.Values.CopyTo(values, 0);
            foreach (GuiChatLobby lobby in values)
            {
                if (lobby.Lobby == null)
                    continue;
                CheckState state = _b.GUI.clb_chatLobbies.GetItemCheckState(lobby.Index);
                if (lobby.Unread)
                {
                    if (state == CheckState.Indeterminate)
                        if (lobby.Joined)
                            state = CheckState.Checked;
                        else
                            state = CheckState.Unchecked;
                    else
                        state = CheckState.Indeterminate;
                    _b.GUI.clb_chatLobbies.SetItemCheckState(lobby.Index, state);
                }
                else if (lobby.Lobby.lobby_state == ChatLobbyInfo.LobbyState.LOBBYSTATE_INVITED)
                    _b.GUI.clb_chatLobbies.SetItemCheckState(lobby.Index, (state == CheckState.Indeterminate) ? CheckState.Unchecked : CheckState.Indeterminate);
            }
        }

        public void ChatLobbyIndexChange(int index)
        {
            if (index >= 0)
            {
                GuiChatLobby cl = new GuiChatLobby();
                if (GetLobbyByListIndex(index, out cl))
                {
                    SetChatText(cl.ID);
                    _b.GUI.clb_chatUser.Items.Clear();
                    foreach (string s in cl.ChatUser)
                        _b.GUI.clb_chatUser.Items.Add(s);
                }
            }
        }

        public void ChatLobbyItemChecked(ItemCheckEventArgs e)
        {
            GuiChatLobby cl = new GuiChatLobby();
            if (GetLobbyByListIndex(e.Index, out cl))
            {
                if (e.NewValue != CheckState.Indeterminate)
                {
                    if (cl.Joined && e.NewValue == CheckState.Unchecked) // leave lobby
                    {
                        _b.RPC.ChatJoinLeaveLobby(RequestJoinOrLeaveLobby.LobbyAction.LEAVE_OR_DENY, cl.ID);
                        cl.Joined = false;
                    }
                    else if (!cl.Joined && e.NewValue == CheckState.Checked) // join lobby
                    {
                        _b.RPC.ChatJoinLeaveLobby(RequestJoinOrLeaveLobby.LobbyAction.JOIN_OR_ACCEPT, cl.ID);
                        cl.Joined = true;
                        //if(cl.Lobby.lobby_state == ChatLobbyInfo.LobbyState.LOBBYSTATE_INVITED)
                        cl.Lobby.lobby_state = ChatLobbyInfo.LobbyState.LOBBYSTATE_JOINED;
                    }
                    _chatLobbies[cl.ID] = cl;
                    CheckChatRegistration();
                }
            }
        }
    }
}
