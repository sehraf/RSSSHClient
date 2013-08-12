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
        LoadSaveHandler _loadSaveHandler;
        Log _log;
        uint _tickCounter;
        int selectedIndex = -1;

        bool _flip = false;

        Bridge _b;

        public uint TickCounter { get { return _tickCounter; } }

        public MainForm()
        {
            InitializeComponent();
            cb_con.CheckState = CheckState.Unchecked;

            _b = Bridge.GetBridge(this); // first initialisation of Bridge -> need gui
            _b.RPC.EventOccurred += EventFromThread;
            _b.RPC.ReceivedMsg += ProcessMsgFromThread;

            _loadSaveHandler = new LoadSaveHandler();
            _log = new Log();
            _log.NewSession();
            _tickCounter = 0;

            AddSpeedOptions(); // add options _before_ loading settings!
            LoadSettings();
        }

        private void AddSpeedOptions()
        {
            cb_settingsReadSpeed.Items.Add(10);
            cb_settingsReadSpeed.Items.Add(20);
            cb_settingsReadSpeed.Items.Add(50);
            cb_settingsReadSpeed.Items.Add(100);
            cb_settingsReadSpeed.Items.Add(500);
        }

        private void LoadSettings()
        {
            Options opt;
            if(_loadSaveHandler.Load(out opt)) {
                if (opt.SaveSettings)
                {
                    tb_host.Text = opt.Host;
                    tb_port.Text = opt.Port;
                    tb_user.Text = opt.User;
                    tb_pw.Text = opt.Password;
                    cb_settingsSave.Checked = true;
                    cb_settingsSavePW.Checked = opt.SavePW;
                    cb_settingsReadSpeed.SelectedIndex = opt.ReadSpeedIndex;
                }
                if (opt.SaveChat) // chat includes AutoResponse
                {
                    _b.ChatProcessor.SetNick(opt.Nick);
                    cb_settingsSaveChat.Checked = true;

                    cb_chat_arEnable.Checked = opt.EnableAutoResp;
                    _b.AutoResponse.AutoResponseList = opt.AutoResponseList;
                }
            }
        }

        private void SaveSettings()
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
                opt.ReadSpeedIndex = (byte)cb_settingsReadSpeed.SelectedIndex;
            }
            if (cb_settingsSaveChat.Checked) // chat includes AutoResponse
            {
                opt.Nick = _b.ChatProcessor.Nick;
                opt.SaveChat = true;

                opt.EnableAutoResp = cb_chat_arEnable.Checked;
                opt.AutoResponseList = _b.AutoResponse.AutoResponseList;
            }            
            _loadSaveHandler.Save(opt);
        }

        private void EventFromThread(RSRPC.EventType type, object obj)
        {
            switch (type)
            {
                case RSRPC.EventType.Error:
                    Exception e = (Exception)obj;
                    this.Invoke((MethodInvoker)delegate { Error(e); });
                    break;
                case RSRPC.EventType.Reconnect:
                    this.Invoke((MethodInvoker)delegate { ReconnectOccurred(); });
                    break;
            }
        }

        private void Error(Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e.Message);
            tb_out.AppendText(e.Message + "\n");
            _log.AddError(e);
        }

        private void ReconnectOccurred()
        {
            /*
             * Reconnecting causes a server-side clean-up
             * -> Searches are removed
             * -> Events are removed
             */

            _b.SearchProcessor.Reset();     // should work for now (you have to add a search request again if needed)
            _b.ChatProcessor.Reset(true);   // will register again with next LobbyRequest
        }

        private void ProcessMsgFromThread(RSProtoBuffSSHMsg msg)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate { _b.Processor.ProcessMsg(msg); });
            }
            catch { }
        }

        public void SendToChatFromIRCFromThread(string msg)
        {
            this.Invoke((MethodInvoker)delegate { _b.ChatProcessor.sendToIRC(msg); });
        }

        private void ConnectionEstablished()
        {
            _tickCounter = 0;
            t_tick.Start();

            _b.RPC.SystemGetStatus();

            //_b.PeerProcessor.RequestPeerList(true); 
            _b.RPC.SystemRequestSystemAccount();

            _b.ChatProcessor.SetNick(_b.ChatProcessor.Nick);
            _b.ChatProcessor.AddBroadcast();
            //_b.ChatProcessor.AddIRC();
        }

        private void Connect()
        {
            if (_b.RPC.IsConnected || tb_host.Text == "" || tb_port.Text == "")
                return;

            cb_con.CheckState = CheckState.Indeterminate;

            if (_b.RPC.Connect(tb_host.Text, Convert.ToUInt16(tb_port.Text), tb_user.Text, tb_pw.Text))
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
            if (!_b.RPC.IsConnected)
                return;
            t_tick.Stop();

            _b.SearchProcessor.CloseAllSearches();
            _b.RPC.Disconnect(shutdown);
            _b.Reset();
            cb_con.CheckState = CheckState.Unchecked;
        }

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

        private void Tick()
        {
            // 1 tick = 1 second
            if (_b.RPC.IsConnected)
            {
                // update system status (every tick/second)
                if (_tickCounter % 30 == 0 || tc_main.SelectedTab == tp_setup)
                {
                    _b.RPC.SystemGetStatus();
                }
                
                // DL/UL (every second tick)
                if (_tickCounter % 30 == 0 || (tc_main.SelectedTab == tp_files && _tickCounter % 2 == 0))
                {
                    _b.FileProcessor.RequestFileLists();
                }

                // get search results ( every 10 seconds )
                if (_tickCounter % 10 == 0)
                {
                    _b.SearchProcessor.GetSearchResults();
                }

                // update friends (every second tick)
                if (_tickCounter % 30 == 0 || (tc_main.SelectedTab == tp_friends && _tickCounter % 2 == 0))
                {
                    _b.RPC.PeersGetFriendList(RequestPeers.SetOption.FRIENDS);
                }

                //chat lobbies
                _b.ChatProcessor.Tick(_tickCounter);


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
            this.SaveSettings();
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
            if (_flip)
                _b.IRCProcessor.stop();
            else
                _b.IRCProcessor.starte();
            _flip = !_flip;
        }

        private void cb_settingsReadSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cb_settingsReadSpeed.SelectedText;
            if (selected != "")
                _b.RPC.SetReadSpeed(Convert.ToUInt16(selected));
        }

        private void bt_settingsClearLog_Click(object sender, EventArgs e)
        {
            _log.ClearLog();
        }

        #region friends
        // Friends

        private void lb_friends_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_friends.SelectedIndex >= 0)
                _b.PeerProcessor.FriendsSelectedIndexChanged(lb_friends.SelectedIndex);
        }

        private void lb_locations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_locations.SelectedIndex >= 0)
                _b.PeerProcessor.LocationSelevtedIndexChanged(lb_locations.SelectedIndex);
        }

        private void bt_peerSave_Click(object sender, EventArgs e)
        {
            _b.PeerProcessor.SavePeer(lb_locations.SelectedIndex);
        }

        private void bt_peerRemove_Click(object sender, EventArgs e)
        {

        }

        private void bt_peerNew_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region chat

        private void bt_chatSend_Click(object sender, EventArgs e)
        {
            if (tb_chatMsg.Text != "")
            {
                // get chat lobby
                int index = _b.GUI.clb_chatLobbies.SelectedIndex;
                if (index >= 0 && _b.GUI.clb_chatLobbies.GetItemChecked(index))
                {
                    GuiChatLobby cl = new GuiChatLobby();
                    if (_b.ChatProcessor.GetLobbyByListIndex(index, out cl))
                        _b.ChatProcessor.SendChatMsg(cl);
                }
            }
            tb_chatMsg.Focus();
        }

        private void clb_chatLobbies_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //clb_chatLobbies.SetItemCheckState(e.Index, e.CurrentValue);
        }

        private void clb_chatLobbies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedIndex != clb_chatLobbies.SelectedIndex)
            {
                selectedIndex = clb_chatLobbies.SelectedIndex;
                _b.ChatProcessor.ChatLobbyIndexChange(clb_chatLobbies.SelectedIndex);
            }
        }

        private void clb_chatLobbies_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = clb_chatLobbies.SelectedIndex;
            if (index > 0)  // 0 is group chat            
                clb_chatLobbies.SetItemCheckState(index, _b.ChatProcessor.JoinLeaveChatLobby(JoinLeaveAction.toggle, clb_chatLobbies.SelectedIndex));     
        }

        private void tb_chatMsg_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                bt_chatSend_Click(null, null);
        }

        private void bt_setNickname_Click(object sender, EventArgs e)
        {
            if (tb_chatNickname.Text != "")
                _b.ChatProcessor.SetNick(tb_chatNickname.Text);
        }

        private void cb_settingsSave_CheckedChanged(object sender, EventArgs e)
        {
            cb_settingsSavePW.Enabled = cb_settingsSave.Checked;
        }

        private void bt_shutdown_Click(object sender, EventArgs e)
        {
            Shutdown();
        }

        private void bt_joinChatLobby_Click(object sender, EventArgs e)
        {
            int index = clb_chatLobbies.SelectedIndex;
            if (index > 0)  // 0 is group chat
                clb_chatLobbies.SetItemCheckState(index, _b.ChatProcessor.JoinLeaveChatLobby(JoinLeaveAction.join, clb_chatLobbies.SelectedIndex));
        }

        private void bt_leaveChatLobby_Click(object sender, EventArgs e)
        {
            int index = clb_chatLobbies.SelectedIndex;
            if (index > 0)  // 0 is group chat
                clb_chatLobbies.SetItemCheckState(index, _b.ChatProcessor.JoinLeaveChatLobby(JoinLeaveAction.leave, clb_chatLobbies.SelectedIndex));
        }

        #region automatic response

        private void clb_chat_arList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = clb_chat_arList.SelectedIndex;
            if (index >= 0)
                _b.AutoResponse.ShowItem(clb_chat_arList.Items[index].ToString());
        }

        private void bt_chat_arSave_Click(object sender, EventArgs e)
        {
            int index = clb_chat_arList.SelectedIndex;
            if (index >= 0)
                _b.AutoResponse.SaveItem(clb_chat_arList.Items[index].ToString());
        }

        private void bt_chat_arNew_Click(object sender, EventArgs e)
        {
            _b.AutoResponse.AddNew(tb_chat_arName.Text);
        }

        private void clb_chat_arList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int index = clb_chat_arList.SelectedIndex;
            if (index >= 0)
                _b.AutoResponse.SetActive(clb_chat_arList.Items[index].ToString(), e.NewValue == CheckState.Checked);
        }

        private void bt_chat_arRemove_Click(object sender, EventArgs e)
        {
            int index = clb_chat_arList.SelectedIndex;
            if (index >= 0)
                _b.AutoResponse.RemoveItem(clb_chat_arList.Items[index].ToString());
        }

        #endregion
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
            if (!_b.FileProcessor.GetFileTransferBySelection(out gft))
                return;

            _b.RPC.FilesControllDownload(RequestControlDownload.Action.ACTION_PAUSE, gft.FileTransfer.file);
        }

        private void bt_filesWait_Click(object sender, EventArgs e)
        {
            GuiFileTransfer gft = new GuiFileTransfer();
            if (!_b.FileProcessor.GetFileTransferBySelection(out gft))
                return;

            _b.RPC.FilesControllDownload(RequestControlDownload.Action.ACTION_WAIT, gft.FileTransfer.file);
        }

        private void bt_filesContinue_Click(object sender, EventArgs e)
        {
            GuiFileTransfer gft = new GuiFileTransfer();
            if (!_b.FileProcessor.GetFileTransferBySelection(out gft))
                return;

            _b.RPC.FilesControllDownload(RequestControlDownload.Action.ACTION_CONTINUE, gft.FileTransfer.file);
        }
        
        private void bt_filesForceCheck_Click(object sender, EventArgs e)
        {
            GuiFileTransfer gft = new GuiFileTransfer();
            if (!_b.FileProcessor.GetFileTransferBySelection(out gft))
                return;

            _b.RPC.FilesControllDownload(RequestControlDownload.Action.ACTION_CHECK, gft.FileTransfer.file);
        }

        private void bt_filesRestart_Click(object sender, EventArgs e)
        {
            GuiFileTransfer gft = new GuiFileTransfer();
            if (!_b.FileProcessor.GetFileTransferBySelection(out gft))
                return;

            _b.RPC.FilesControllDownload(RequestControlDownload.Action.ACTION_RESTART, gft.FileTransfer.file);
        }

        private void bt_filesCancel_Click(object sender, EventArgs e)
        {
            GuiFileTransfer gft = new GuiFileTransfer();
            if (!_b.FileProcessor.GetFileTransferBySelection(out gft))
                return;

            _b.RPC.FilesControllDownload(RequestControlDownload.Action.ACTION_CANCEL, gft.FileTransfer.file);
        }

        private void bt_filesAddCollection_Click(object sender, EventArgs e)
        {
            if (ofd_collection.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                tb_out.AppendText("Error adding collection" + "\n");
                return;
            }

            List<File> fileList;
            if (!RsCollection.ReadCollection(ofd_collection.FileName, out fileList) || fileList == null)
            {
                tb_out.AppendText("Error adding collection" + "\n");
            }

            foreach (File f in fileList)
            {
                // add all the files 
                System.Diagnostics.Debug.WriteLine(f.name + " - " + f.size + " - " + f.hash);
            }
            tb_out.AppendText("adding " + fileList.Count + " files" + "\n");
        }

        #endregion

        #region search

        private void bt_searchSearch_Click(object sender, EventArgs e)
        {
            _b.SearchProcessor.Search(tb_searchKeyWords.Text);
            tb_searchKeyWords.Clear();
        }

        private void lb_searches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_searches.SelectedIndex != -1)
                _b.SearchProcessor.UpdateSearchResults(lb_searches.SelectedIndex);
        }

        private void tb_searchKeyWords_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                bt_searchSearch_Click(null, null);
        }

        private void bt_searchRemove_Click(object sender, EventArgs e)
        {
            if (lb_searches.SelectedIndex == -1)
                return;
            _b.SearchProcessor.RemoveSearch(lb_searches.SelectedIndex);
        }

        private void bt_searchAddToDL_Click(object sender, EventArgs e)
        {
            if (lb_searchResults.SelectedIndex == -1)
                return;

            GuiSearch gs = new GuiSearch();
            if (_b.SearchProcessor.GetSearchByIndex((ushort)lb_searches.SelectedIndex, out gs))
                _b.RPC.FilesControllDownload(RequestControlDownload.Action.ACTION_START, gs.Results[lb_searchResults.SelectedIndex].file);
        }

        #endregion


    }

    public class Bridge
    {
        static Bridge _b;
        MainForm _gui;
        RSRPC _rpc;
        Processor _processor;
        ChatProcessor _chat;
        AutoResponse _autoResponse;
        PeerProcessor _peer;
        SearchProcessor _search;
        StreamProcessor _stream;
        FileProcessor _file;
        IRCProcessor _irc;

        internal MainForm GUI { get { return _gui; } } //set { _gui = value; } }
        public RSRPC RPC { get { return _rpc; } } //set { _rpc = value; } }        
        internal Processor Processor { get { return _processor; } } //set { _processor = value; } }
        internal ChatProcessor ChatProcessor { get { return _chat; } } //set { _chat = value; } }
        internal AutoResponse AutoResponse { get { return _autoResponse; } } //set { _autoResponse = value; } }
        internal PeerProcessor PeerProcessor { get { return _peer; } } //set { _peer = value; } }
        internal SearchProcessor SearchProcessor { get { return _search; } } //set { _search = value; } }
        internal StreamProcessor StreamProcessor { get { return _stream; } } //set { _stream = value; } }
        internal FileProcessor FileProcessor { get { return _file; } } //set { _file = value; } }
        internal IRCProcessor IRCProcessor { get { return _irc; } }

        private Bridge(MainForm gui)
        {
            _gui = gui;
        }

        /// <summary>
        /// This need to be called after the bridge is instanced!
        /// Most classes call GetBridge() on initialisation and if there is no bridge (yet)
        /// this will end up in an endless loop.
        /// </summary>
        private static void InitBridge()
        {
            _b._rpc = new RSRPC(false);
            _b._processor = new Processor();
            _b._chat = new ChatProcessor();
            _b._peer = new PeerProcessor();
            _b._search = new SearchProcessor();
            _b._stream = new StreamProcessor();
            _b._file = new FileProcessor();
            _b._autoResponse = new AutoResponse();
            _b._irc = new IRCProcessor();
        }

        /// <summary>
        /// This function will return always the same bridge.
        /// </summary>
        /// <param name="gui">gui only needs to be set on the first call!</param>
        /// <returns>returns (the one and only) bridge</returns>
        public static Bridge GetBridge(MainForm gui = null)
        {
            if (_b == null)
                if (gui != null)
                {
                    _b = new Bridge(gui);
                    InitBridge();
                }
                else
                    throw new Exception("need GUI to initialize bridge");

            return _b;
        }

        /// <summary>
        /// Resets all components
        /// </summary>
        public void Reset()
        {
            _processor.Reset();
            _chat.Reset();
            _autoResponse.Reset();
            _peer.Reset();
            _search.Reset();
            _stream.Reset();
            _file.Reset();
            _irc.Reset();
        }
    }
}