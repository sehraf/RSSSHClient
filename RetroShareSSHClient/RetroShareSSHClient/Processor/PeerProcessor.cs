using System;
using System.Linq;
using System.Collections.Generic;

using rsctrl.core;
using rsctrl.peers;

using Sehraf.RSRPC;

namespace RetroShareSSHClient
{
    class PeerProcessor
    {
        Person _owner;
        List<Person> _peerList;
        Person _selectedPeer;

        Bridge _b;

        public PeerProcessor(Bridge bridge)
        {
            _b = bridge;

            _owner = new Person();
            _peerList = new List<Person>();
            _selectedPeer = new Person();
        }

        public void Reset()
        {
            //_owner = null; needed?
            _peerList.Clear();
            _b.GUI.lb_friends.Items.Clear();
            _b.GUI.lb_locations.Items.Clear();
            _selectedPeer = null;
        }

        public void UpdatePeerList(ResponsePeerList msg)
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
            if (_owner.locations.Count > 0)
                _peerList.Add(_owner);
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

        public void SetOwnID(ResponsePeerList msg)
        {
            _owner = msg.peers[0]; // i gues we just have one owner           
            _b.GUI.Text = "RetroShare SSH Client by sehraf - " + _owner.name + "(" + _owner.gpg_id + ") ";// +l.location + " (" + l.ssl_id + ")";

            if (_b.ChatProcessor.Nick == "" || _b.ChatProcessor.Nick == null)
            {
                string name = _owner.name.Trim();
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

        public void FriendsSelectedIndexChanged(int index)
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

        public void LocationSelevtedIndexChanged(int index)
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

        public void SavePeer(int locationIndex)
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
