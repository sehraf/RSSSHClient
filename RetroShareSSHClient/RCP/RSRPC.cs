using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

using ProtoBuf;

using rsctrl.chat;
using rsctrl.core;
//using rsctrl.files;
//using rsctrl.gxs;
//using rsctrl.msgs;
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
            //_rsProtoBuf.Reset();
            System.Threading.Thread.Sleep(500);
        }

        internal bool ProcessMsg(RSProtoBuffSSHMsg msg)
        {
            _receivedMsg(msg);
            return true;
        }

        // ---------- sende ----------
        // ---------- generic send<T> ----------
        public uint Send<T>(T pbMsg, uint inMsgID, bool important = false)
        {
            RSProtoBuffSSHMsg msg = new RSProtoBuffSSHMsg();
            msg.MsgID = inMsgID;
            msg.ReqID = _rsProtoBuf.GetReqID();
            msg.IsImportant = important;
            msg.ProtoBuffMsg = new MemoryStream();
            Serializer.Serialize<T>(msg.ProtoBuffMsg, pbMsg);
            msg.ProtoBuffMsg.Position = 0;
            msg.BodySize = (uint)msg.ProtoBuffMsg.Length;

            lock (_sendQueue)
                _sendQueue.Enqueue(msg);

            return msg.ReqID;
        }

        #region chat
        public uint GetChatLobbies(RequestChatLobbies.LobbyType type)
        {
            throw new NotImplementedException();

            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.CHAT,
                    (byte)rsctrl.chat.RequestMsgIds.MsgId_RequestChatLobbies,
                    false
                );

            RequestChatLobbies request = new RequestChatLobbies();
            request.lobby_type = type;
            return Send<RequestChatLobbies>(request, msgID, true);
        }

        public uint CreateLobby(string name, string topic, LobbyPrivacyLevel privacy)
        {
            throw new NotImplementedException();

            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.CHAT,
                    (byte)rsctrl.chat.RequestMsgIds.MsgId_RequestCreateLobby,
                    false
                );

            RequestCreateLobby request = new RequestCreateLobby();
            //request.invited_friends = ... // what strings?
            request.lobby_name = name;
            request.lobby_topic = topic;
            request.privacy_level = privacy;
            return Send<RequestCreateLobby>(request, msgID, true);
        }

        public uint CreateLobby(RequestJoinOrLeaveLobby.LobbyAction action, string lobbyID)
        {
            throw new NotImplementedException();

            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.CHAT,
                    (byte)rsctrl.chat.RequestMsgIds.MsgId_RequestJoinOrLeaveLobby,
                    false
                );

            RequestJoinOrLeaveLobby request = new RequestJoinOrLeaveLobby();
            request.action = action;
            request.lobby_id = lobbyID;
            return Send<RequestJoinOrLeaveLobby>(request, msgID, true);
        }

        public uint CreateLobby(RequestRegisterEvents.RegisterAction action)
        {
            throw new NotImplementedException();

            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.CHAT,
                    (byte)rsctrl.chat.RequestMsgIds.MsgId_RequestRegisterEvents,
                    false
                );

            RequestRegisterEvents request = new RequestRegisterEvents();
            request.action = action;
            return Send<RequestRegisterEvents>(request, msgID, true);
        }

        public uint SendMsg(ChatMessage msg)
        {
            throw new NotImplementedException();

            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.CHAT,
                    (byte)rsctrl.chat.RequestMsgIds.MsgId_RequestSendMessage,
                    false
                );

            RequestSendMessage request = new RequestSendMessage();
            request.msg = msg;
            return Send<RequestSendMessage>(request, msgID, true);
        }

        public uint SetLobbyNickname(string name, List<string> lobbyIDs = null)
        {
            throw new NotImplementedException();

            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.CHAT,
                    (byte)rsctrl.chat.RequestMsgIds.MsgId_RequestSetLobbyNickname,
                    false
                );

            RequestSetLobbyNickname request = new RequestSetLobbyNickname();
            //request.lobby_ids = lobbyIDs; - read only
            request.nickname = name;
            return Send<RequestSetLobbyNickname>(request, msgID, true);
        }
        #endregion

        #region files
        #endregion

        #region gxs
        #endregion

        #region msgs
        #endregion

        #region peers
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
        #endregion

        #region system
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
        #endregion
        
    }
}
