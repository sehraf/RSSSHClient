using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

using ProtoBuf;

using rsctrl.core;
using rsctrl.peers;
using rsctrl.system;

namespace Sehraf.RetroShareSSH
{
    //delegate void RSCallback(CallbackType type, RSProtoBuffSSHMsg msg);
    enum CallbackType
    {
        Disconnect = 0,
        ProcessMsg = 1
    }

    class RSRPC
    {
        public delegate void ReceivedMsgEvent(RSProtoBuffSSHMsg msg);
        public delegate void GenericCallbackEvent(CallbackType type);

        RSSSHConnector _rsConnector;
        RSProtoBuf _rsProtoBuf;
        bool _connected;

        event ReceivedMsgEvent _receivedMsg;
        event GenericCallbackEvent _callback;
        Queue<RSProtoBuffSSHMsg> _sendQueue;

        public RSSSHConnector RSConnector { get { return _rsConnector; } }
        public RSProtoBuf RSProtoBuf { get { return _rsProtoBuf; } }
        public bool IsConnected { get { return _connected; } }
        public ReceivedMsgEvent ReceivedMsg { get { return _receivedMsg; } set { _receivedMsg = value; } }
        public GenericCallbackEvent GenericCallback { get { return _callback; } set { _callback = value; } }

        public RSRPC()
        {
            _connected = false;
            _sendQueue = new Queue<RSProtoBuffSSHMsg>();
        }

        public bool Connect(string host, ushort port, string user, string pw)
        {
            if (!_connected)
            {
                _sendQueue.Clear();
                _rsConnector = new RSSSHConnector(host, port, user, pw);
                if (_rsConnector.Connect())
                {
                    _rsProtoBuf = new RSProtoBuf(_rsConnector.StreamIn, _rsConnector.StreamOut, _sendQueue, this);
                    //_rsProtoBuf = new RSProtoBuf(_rsConnector.Stream, _sendQueue, this);
                    _connected = true;
                    return true;
                }
            }
            return false;
        }

        public void Disconnect(bool error = false)
        {
            if (error)            
                _callback(CallbackType.Disconnect);            
            else
                if (_connected)
                {
                    _rsProtoBuf.Stop();
                    _connected = false;
                    _rsConnector.Disconnect();
                    _rsConnector = null;
                    _rsProtoBuf = null;
                }
        }

        internal void Reconnect()
        {
            _rsConnector.Reconnect();
            _rsProtoBuf.Reset();
            System.Threading.Thread.Sleep(500);
        }

        internal bool ProcessMsg(RSProtoBuffSSHMsg msg)
        {
            _receivedMsg(msg);
            return true;
        }

        // ---------- Sender ----------
        // ---------- generic send<T> ----------
        public uint Send<T>(T pbMsg, uint inMsgID, bool important = false)
        {
            RSProtoBuffSSHMsg msg = new RSProtoBuffSSHMsg();
            msg.MsgID = inMsgID;
            msg.ReqID = _rsProtoBuf.ReqID();
            msg.IsImportant = important;
            msg.ProtoBuffMsg = new MemoryStream();
            Serializer.Serialize<T>(msg.ProtoBuffMsg, pbMsg);
            msg.ProtoBuffMsg.Position = 0;
            msg.BodySize = (uint)msg.ProtoBuffMsg.Length;

            lock (_sendQueue)
                _sendQueue.Enqueue(msg);

            return msg.ReqID;
        }

        // ---------- peers ----------

        public uint AddPeer(string cert, string gpgID)
        {
            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.PEERS,
                    (byte)rsctrl.peers.RequestMsgIds.MsgId_RequestAddPeer,
                    false
                );

            RequestAddPeer request = new RequestAddPeer();
            request.cert = cert;
            request.gpg_id = gpgID;
            request.cmd = RequestAddPeer.AddCmd.ADD;
            return Send<RequestAddPeer>(request, msgID, true);
        }

        public uint ModifyPeer(Person peer, RequestModifyPeer.ModCmd cmd)
        {
            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.PEERS,
                    (byte)rsctrl.peers.RequestMsgIds.MsgId_RequestModifyPeer,
                    false
                );

            RequestModifyPeer request = new RequestModifyPeer();
            request.peers.Add(peer);
            request.cmd = cmd;
            return Send<RequestModifyPeer>(request, msgID, true);
        }

        public uint GetFriendList(RequestPeers.SetOption option)
        {
            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.PEERS,
                    (byte)rsctrl.peers.RequestMsgIds.MsgId_RequestPeers,
                    false
                );

            RequestPeers request = new RequestPeers();
            request.info = RequestPeers.InfoOption.ALLINFO;
            request.set = option;
            return Send<RequestPeers>(request, msgID, true);
        }

        // ---------- system ----------
        public uint GetSystemStatus()
        {
            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.SYSTEM,
                    (byte)rsctrl.system.RequestMsgIds.MsgId_RequestSystemStatus,
                    false
                );

            RequestSystemStatus request = new RequestSystemStatus();
            return Send<RequestSystemStatus>(request, msgID);
        }

    }
}
