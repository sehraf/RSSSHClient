using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Sehraf.RetroShareSSH;

using ProtoBuf;

using rsctrl;
using rsctrl.core;
using rsctrl.system;
using rsctrl.peers;

namespace RetroShareSSHClient
{
    public partial class MainForm : Form
    {
        RSRPC _rpc;
        Dictionary<uint, RequestPeers.SetOption> _pendingPeerRequests;
        List<Person> _friendList;
        Person _selectedFriend;

        uint _tickCounter;

        public MainForm()
        {
            InitializeComponent();
            cb_con.CheckState = CheckState.Unchecked;

            _rpc = new RSRPC();
            _rpc.GenericCallback += Callback;
            _rpc.ReceivedMsg += ProcessMsgFromThread;
            _pendingPeerRequests = new Dictionary<uint, RequestPeers.SetOption>();
            _friendList = new List<Person>();
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

        private void Callback(CallbackType type)
        {
            switch (type)
            {
                case CallbackType.Disconnect:
                    this.Invoke((MethodInvoker)delegate { bt_disconnect_Click(null, null); });                    
                    break;
                case CallbackType.ProcessMsg:
                    //ProcessMsg(msg);
                    break;
                default:
                    // :S
                    break;
            }
        }

        // ---------- process incoming messages ----------
        private void ProcessMsgFromThread(RSProtoBuffSSHMsg msg)
        {
            this.Invoke((MethodInvoker)delegate { ProcessMsg(msg); });
        }

        private void ProcessMsg(RSProtoBuffSSHMsg msg)
        {
            byte extension = RSProtoBuf.GetRpcMsgIdExtension(msg.MsgID);
            ushort service = RSProtoBuf.GetRpcMsgIdService(msg.MsgID);
            byte submsg = RSProtoBuf.GetRpcMsgIdSubMsg(msg.MsgID);
            System.Diagnostics.Debug.WriteLine("Processing Msg " + msg.ReqID + " .....");
            System.Diagnostics.Debug.WriteLine("ext: "  + extension + " - service: " + service + " - submsg: " + submsg);
            tb_out.AppendText(" -> " + msg.ReqID + "\n");
            switch (extension)
            {
                case (byte)rsctrl.core.ExtensionId.CORE:
                    switch (service)
                    {
                        case (ushort)rsctrl.core.PackageId.PEERS:
                            switch (submsg)
                            {
                                case (byte)rsctrl.peers.RequestMsgIds.MsgId_RequestAddPeer:
                                    ResponseAddPeer rap = new ResponseAddPeer();
                                    if (RSProtoBuf.Deserialize<ResponseAddPeer>(out rap, msg.ProtoBuffMsg))
                                        tb_out.AppendText("AddPeer response: " + rap.status.code + "\n");
                                    else
                                        System.Diagnostics.Debug.WriteLine("error deserializing");
                                    break;
                                case (byte)rsctrl.peers.RequestMsgIds.MsgId_RequestModifyPeer:
                                    ResponseModifyPeer rmp = new ResponseModifyPeer();
                                    if (RSProtoBuf.Deserialize<ResponseModifyPeer>(out rmp, msg.ProtoBuffMsg))
                                        tb_out.AppendText("AddPeer response: " + rmp.status.code + "\n");
                                    else
                                        System.Diagnostics.Debug.WriteLine("error deserializing");
                                    break;
                                case (byte)rsctrl.peers.RequestMsgIds.MsgId_RequestPeers:
                                    ResponsePeerList rpl = new ResponsePeerList();
                                    if (RSProtoBuf.Deserialize<ResponsePeerList>(out rpl, msg.ProtoBuffMsg))
                                    {
                                        RequestPeers.SetOption opt;
                                        if (_pendingPeerRequests.TryGetValue(msg.ReqID, out opt))
                                            switch (opt)
                                            {
                                                case RequestPeers.SetOption.OWNID:
                                                    SetOwnID(rpl);
                                                    break;
                                                //case RequestPeers.SetOption.LISTED:                                                    
                                                default:
                                                    UpdatePeerList(rpl);
                                                    // all other cases 
                                                    break;
                                            }
                                        else
                                        {
                                            // peer list but no clue what is in it
                                        }
                                    }
                                    else
                                        System.Diagnostics.Debug.WriteLine("error deserializing");
                                    break;
                                default:
                                    //...
                                    break;
                            }
                            break;
                        case (ushort)rsctrl.core.PackageId.SYSTEM:
                            switch (submsg)
                            {
                                case (byte)rsctrl.system.ResponseMsgIds.MsgId_ResponseSystemStatus:
                                    ResponseSystemStatus rss = new ResponseSystemStatus();
                                    if (RSProtoBuf.Deserialize<ResponseSystemStatus>(out rss, msg.ProtoBuffMsg))
                                        UpdateSystemStatus(rss);
                                    else
                                        System.Diagnostics.Debug.WriteLine("error deserializing");
                                    break;
                                default:
                                    //...
                                    break;
                            }
                            break;
                        default: 
                            // HOW COULD THIS HAPPEN? - ok now that bad 
                            break;
                    } // service
                    break;
                default: 
                    // HOW COULD THIS HAPPEN?
                    break;
            } // extension
            System.Diagnostics.Debug.WriteLine("##########################################");
        }

        private void UpdateSystemStatus(ResponseSystemStatus msg)
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

        private void UpdatePeerList(ResponsePeerList msg)
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

        private void SetOwnID(ResponsePeerList msg)
        {
            Person p = msg.peers[0]; // i gues we just have one owner
            //Location l = p.locations[0];
            this.Text = "RetroShare SSH Client - " + p.name + "(" + p.gpg_id + ") ";// +l.location + " (" + l.ssl_id + ")";
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

        private bool PortInRange(uint port)
        {
            return (port >= 1024 && port <= UInt16.MaxValue);
        }

        private void Connect()
        {
            cb_con.CheckState = CheckState.Indeterminate;
            _pendingPeerRequests.Clear();
            if (_rpc.Connect(tb_host.Text, Convert.ToUInt16(tb_port.Text), tb_user.Text, tb_pw.Text))
            {
                tb_out.Text = "connected!" + "\n";
                cb_con.CheckState = CheckState.Checked;
                t_tick.Start();
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
            _rpc.Disconnect();
            tb_out.Text = "disconnected!";
            cb_con.CheckState = CheckState.Unchecked;
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
                regID = _rpc.GetFriendList(RequestPeers.SetOption.FRIENDS);
                _pendingPeerRequests.Add(regID, RequestPeers.SetOption.FRIENDS);
            }
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

        private void bt_test_Click(object sender, EventArgs e)
        {
            uint regID;
            regID = _rpc.GetSystemStatus();
            regID = _rpc.GetFriendList(RequestPeers.SetOption.OWNID);
            _pendingPeerRequests.Add(regID, RequestPeers.SetOption.OWNID);
            regID = _rpc.GetFriendList(RequestPeers.SetOption.FRIENDS);
            _pendingPeerRequests.Add(regID, RequestPeers.SetOption.FRIENDS);
        }
    }
}