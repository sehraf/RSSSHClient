using System;
using System.Linq;
using System.Collections.Generic;

using rsctrl.core;
using rsctrl.system;
using rsctrl.peers;

using Sehraf.RSRPC;

namespace RetroShareSSHClient
{
    class PeerProcessor
    {
        ResponseSystemAccount _owner; // sounds bad but seems to be the best/easiest way
        List<Person> _peerList;
        Person _selectedPeer;

        //Dictionary<uint, RequestPeers.SetOption> _pendingPeerRequests;
        //public Dictionary<uint, RequestPeers.SetOption> PendingPeerRequests { get { return _pendingPeerRequests; } set { _pendingPeerRequests = value; } }

        Bridge _b;

        public PeerProcessor()
        {
            _b = Bridge.GetBridge();

            _owner = new ResponseSystemAccount();
            _peerList = new List<Person>();
            _selectedPeer = new Person();
            //_pendingPeerRequests = new Dictionary<uint, RequestPeers.SetOption>();
        }

        internal void Reset()
        {
            //_owner = null; needed?
            _peerList.Clear();
            _b.GUI.lb_friends.Items.Clear();
            _b.GUI.lb_locations.Items.Clear();
            _selectedPeer = null;

            //_pendingPeerRequests.Clear();
        }

        //internal void RequestPeerList(bool requestOwner = false)
        //{
        //    uint regID;
        //    if (requestOwner)
        //    {
        //        regID = _b.RPC.PeersGetFriendList(RequestPeers.SetOption.OWNID);
        //        _pendingPeerRequests.Add(regID, RequestPeers.SetOption.OWNID);
        //    }
        //    else
        //    {
        //        regID = _b.RPC.PeersGetFriendList(RequestPeers.SetOption.FRIENDS);
        //        _pendingPeerRequests.Add(regID, RequestPeers.SetOption.FRIENDS);
        //    }
        //}

        //internal void ProcessResponsePeerList(uint reqID, ResponsePeerList response)
        //{
        //    RequestPeers.SetOption opt;
        //    if (_pendingPeerRequests.TryGetValue(reqID, out opt))
        //    {
        //        _pendingPeerRequests.Remove(reqID);
        //        switch (opt)
        //        {
        //            case RequestPeers.SetOption.OWNID:
        //                SetOwnID(response);
        //                break;
        //            case RequestPeers.SetOption.FRIENDS:
        //                UpdatePeerList(response);
        //                break;
        //            default:
        //                // all other cases 
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        // peer list - but no clue what is in it
        //    };
        //}

        internal void UpdatePeerList(ResponsePeerList msg)
        {
            // get new peerlist
            List<Person> _newPeerList = msg.peers;
            _newPeerList.Sort(new PersonComparer());

            // check if anything had changed
            if (EqualPeerLists(_peerList, _newPeerList))
                return;
            
            //save selected peer/location
            int index1 = _b.GUI.lb_friends.SelectedIndex, index2 = _b.GUI.lb_locations.SelectedIndex;
            _b.GUI.lb_friends.Items.Clear();
            ClearPeerForm();
            _peerList.Clear();

            _peerList = msg.peers;
            // i hope i don't need this anymore
            //if (_owner.locations.Count > 0)
            //    _peerList.Add(_owner);
            _peerList.Sort(new PersonComparer());

            foreach (Person p in _peerList)
            {
                byte online = 0, total = 0;
                foreach (Location l in p.locations)
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
                _b.GUI.lb_friends.Items.Add("(" + online + "/" + total + ") " + p.name);
            }

            _b.GUI.lb_friends.SelectedIndex = index1;
            _b.GUI.lb_locations.SelectedIndex = index2;
        }

        private bool EqualPeerLists(List<Person> ai, List<Person> bi)
        {
            if (ai.Count != bi.Count) return false;

            Person[] a = ai.ToArray(), b = bi.ToArray();
            Person p1, p2;

            for (int i = 0; i < a.Length; i++)
            {
                p1 = a[i];
                p2 = b[i];

                if (p1.gpg_id != p2.gpg_id || p1.name != p2.name || p1.locations.Count != p2.locations.Count) return false;                
            }

            return true;
        }

        internal void SetOwnID(ResponseSystemAccount msg)
        {
            //_owner.gpg_id = msg.pgp_id;
            //_owner.locations[0] = msg.location;
            //_owner.name = msg.pgp_name;
            //_owner.relation = Person.Relationship.YOURSELF;
            _owner = msg;

            _b.GUI.Text = "RetroShare SSH Client by sehraf - " + _owner.pgp_name + "(" + _owner.pgp_id + ") ";// +l.location + " (" + l.ssl_id + ")";

            if (_b.ChatProcessor.Nick == "" || _b.ChatProcessor.Nick == null)
            {
                string name = _owner.pgp_name.Trim();
                _b.ChatProcessor.SetNick(name + " (nogui/ssh)");
            }
        }

        private void ClearPeerForm()
        {
            _b.GUI.lb_locations.Items.Clear();
            _b.GUI.tb_peerName.Clear();
            _b.GUI.tb_peerID.Clear();
            _b.GUI.tb_peerLocation.Clear();
            _b.GUI.tb_peerLocationID.Clear();
            _b.GUI.tb_peerIPExt.Clear();
            _b.GUI.tb_peerIPInt.Clear();
            _b.GUI.nud_peerPortExt.Value = _b.GUI.nud_peerPortExt.Minimum;
            _b.GUI.nud_peerPortInt.Value = _b.GUI.nud_peerPortInt.Minimum;
        }

        private bool PortInRange(uint port)
        {
            return (port >= 1024 && port <= UInt16.MaxValue);
        }

        internal void FriendsSelectedIndexChanged(int index)
        {
            ClearPeerForm();
            Person p = _peerList[index];
            _selectedPeer = p;
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
                _b.GUI.lb_locations.Items.Add(l.location + " - " + state);
            }
            _b.GUI.tb_peerName.Text = p.name;
            _b.GUI.tb_peerID.Text = p.gpg_id;
        }

        internal void LocationSelevtedIndexChanged(int index)
        {
            Location l = _selectedPeer.locations[index];
            _b.GUI.tb_peerLocation.Text = l.location;
            _b.GUI.tb_peerLocationID.Text = l.ssl_id;
            _b.GUI.tb_peerIPExt.Text = l.extaddr.addr;
            if (PortInRange(l.extaddr.port))
                _b.GUI.nud_peerPortExt.Value = l.extaddr.port;
            _b.GUI.tb_peerIPInt.Text = l.localaddr.addr;
            if (PortInRange(l.localaddr.port))
                _b.GUI.nud_peerPortInt.Value = l.localaddr.port;
            //_b.GUI.tb_dyndns.Text = "NOT IMPLEMENTED";
        }

        internal void SavePeer(int locationIndex)
        {
            Person p = _selectedPeer;
            Location l = p.locations[locationIndex];
            l.extaddr.addr = _b.GUI.tb_peerIPExt.Text;
            l.extaddr.port = Convert.ToUInt16(_b.GUI.nud_peerPortExt.Value);
            l.localaddr.addr = _b.GUI.tb_peerIPInt.Text;
            l.localaddr.port = Convert.ToUInt16(_b.GUI.nud_peerPortInt.Value);
            //dyndns
            p.locations[locationIndex] = l;
            _selectedPeer = p;
            uint reqID = _b.RPC.PeersModifyPeer(p, RequestModifyPeer.ModCmd.ADDRESS);
            // save request ???
        }
    }
}
