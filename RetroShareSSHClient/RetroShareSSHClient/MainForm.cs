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
        Settings _settings;
        uint _tickCounter;

        Bridge _b;

        public uint TickCounter { get { return _tickCounter; } }

        public MainForm()
        {
            InitializeComponent();
            cb_con.CheckState = CheckState.Unchecked;

            _b = new Bridge(this);
            _b.RPC.ErrorOccurred += ErrorFromThread;
            _b.RPC.ReceivedMsg += ProcessMsgFromThread;

            _settings = new Settings();
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
                    _b.ChatProcessor.SetNick(opt.Nick);
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
                opt.Nick = _b.ChatProcessor.Nick;
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
            this.Invoke((MethodInvoker)delegate { _b.Processor.ProcessMsg(msg); });
        }

        private void ConnectionEstablished()
        {
            _tickCounter = 0;
            t_tick.Start();
            uint regID;
            regID = _b.RPC.SystemGetStatus();
            regID = _b.RPC.PeersGetFriendList(RequestPeers.SetOption.OWNID);
            _b.Processor.PendingPeerRequests.Add(regID, RequestPeers.SetOption.OWNID);
            _b.ChatProcessor.SetNick(_b.ChatProcessor.Nick);

            _b.ChatProcessor.AddGroupChat();
        }

        private void Connect()
        {
            if (_b.RPC.IsConnected || tb_host.Text == "" || tb_port.Text == "")
                return;
            cb_con.CheckState = CheckState.Indeterminate;
            _b.Processor.PendingPeerRequests.Clear();
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
            if (_b.RPC.IsConnected)
            {
                // tick
                _b.RPC.SystemGetStatus();
                _b.ChatProcessor.ToggleCheckboxes();
                
                // DL/UL
                if (_tickCounter % 30 == 0 || (tc_main.SelectedTab == tp_files && _tickCounter % 2 == 0))
                {
                    _b.RPC.FilesGetTransferList(rsctrl.files.Direction.DIRECTION_DOWNLOAD);
                    _b.RPC.FilesGetTransferList(rsctrl.files.Direction.DIRECTION_UPLOAD);
                }

                // every 10 sec - not to often!
                if (_tickCounter % 10 == 0)
                {
                    _b.SearchProcessor.GetSearchResults();
                }

                // friends
                if (_tickCounter % 30 == 0 || (tc_main.SelectedTab == tp_friends && _tickCounter % 2 == 0))
                {
                    uint regID;
                    regID = _b.RPC.PeersGetFriendList(RequestPeers.SetOption.FRIENDS);
                    _b.Processor.PendingPeerRequests.Add(regID, RequestPeers.SetOption.FRIENDS);
                }

                //chat lobbies
                if (_tickCounter % 30 == 0 || (_tickCounter < 30 && _tickCounter % 10 == 0))
                {
                    //_bridge.RPC.ChatGetLobbies(rsctrl.chat.RequestChatLobbies.LobbySet.LOBBYSET_JOINED);
                    //_bridge.RPC.ChatGetLobbies(rsctrl.chat.RequestChatLobbies.LobbySet.LOBBYSET_INVITED);
                    //_bridge.RPC.ChatGetLobbies(rsctrl.chat.RequestChatLobbies.LobbySet.LOBBYSET_PUBLIC);
                    _b.RPC.ChatGetLobbies(rsctrl.chat.RequestChatLobbies.LobbySet.LOBBYSET_ALL);
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
            //regID = _bridge.RPC.GetSystemStatus();
            //regID = _bridge.RPC.GetFriendList(RequestPeers.SetOption.OWNID);
            //_processor.PendingPeerRequests.Add(regID, RequestPeers.SetOption.OWNID);
            //regID = _bridge.RPC.GetFriendList(RequestPeers.SetOption.FRIENDS);
            //_processor.PendingPeerRequests.Add(regID, RequestPeers.SetOption.FRIENDS);
            regID = _b.RPC.ChatGetLobbies(rsctrl.chat.RequestChatLobbies.LobbySet.LOBBYSET_PUBLIC);
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
        // chat

        private void bt_chatSend_Click(object sender, EventArgs e)
        {
            if (tb_chatMsg.Text != "")
            {
                _b.ChatProcessor.SendChatMsg();
            }
            tb_chatMsg.Focus();
        }

        private void clb_chatLobbies_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            _b.ChatProcessor.ChatLobbyItemChecked(e);
        }

        private void clb_chatLobbies_SelectedIndexChanged(object sender, EventArgs e)
        {
            _b.ChatProcessor.ChatLobbyIndexChange(clb_chatLobbies.SelectedIndex);
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
        MainForm _gui;
        //RSProtoBuf _protobuf;
        RSRPC _rpc;
        Processor _processor;
        ChatProcessor _chat;
        PeerProcessor _peer;
        SearchProcessor _search;
        FileProcessor _file;

        internal MainForm GUI { get { return _gui; } set { _gui = value; } }
        //public RSProtoBuf ProtoBuf { get { return _protobuf; } set { _protobuf = value; } }
        public RSRPC RPC { get { return _rpc; } set { _rpc = value; } }        
        internal Processor Processor { get { return _processor; } set { _processor = value; } }
        internal ChatProcessor ChatProcessor { get { return _chat; } set { _chat = value; } }
        internal PeerProcessor PeerProcessor { get { return _peer; } set { _peer = value; } }
        internal SearchProcessor SearchProcessor { get { return _search; } set { _search = value; } }
        internal FileProcessor FileProcessor { get { return _file; } set { _file = value; } }

        public Bridge(MainForm gui)
        {
            _gui = gui;
            _rpc = new RSRPC(false);
            _processor = new Processor(this);
            _chat = new ChatProcessor(this);
            _peer = new PeerProcessor(this);
            _search = new SearchProcessor(this);
            _file = new FileProcessor(this);
        }

        public void Reset()
        {
            _processor.Reset();
            _chat.Reset();
            _peer.Reset();
            _search.Reset();
            _file.Reset();
        }
    }
}