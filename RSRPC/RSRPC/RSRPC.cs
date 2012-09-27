using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

using Renci.SshNet;

using ProtoBuf;

using rsctrl.chat;
using rsctrl.core;
using rsctrl.files;
//using rsctrl.gxs;
//using rsctrl.msgs;
using rsctrl.peers;
using rsctrl.search;
using rsctrl.system;

using File = rsctrl.core.File;

namespace Sehraf.RSRPC
{
    public class RSRPC
    {
        public enum EventType
        {
            Error,
            Reconnect
        }

        public delegate void ReceivedMsgEvent(RSProtoBuffSSHMsg msg);
        public delegate void EventOccurredEvent(EventType type, object obj); // nice name :D

        RSSSHConnector _rsConnector;
        RSProtoBuf _rsProtoBuf;
        bool _connected, _disconnecting;

        bool _useProperDisconnect; 

        event ReceivedMsgEvent _receivedMsg;
        event EventOccurredEvent _event;
        Queue<RSProtoBuffSSHMsg> _sendQueue;
        Queue<RSProtoBuffSSHMsg> _receiveQueue;

        Thread _t, _shutdownThread;
        bool _run;

        public RSSSHConnector RSConnector { get { return _rsConnector; } }
        public RSProtoBuf RSProtoBuf { get { return _rsProtoBuf; } }
        public bool IsConnected { get { return _connected; } }
        public ReceivedMsgEvent ReceivedMsg { get { return _receivedMsg; } set { _receivedMsg = value; } }
        public EventOccurredEvent EventOccurred { get { return _event; } set { _event = value; } }

        public RSRPC(bool diconnect = true)
        {
            _connected = false;
            _disconnecting = false;
            _useProperDisconnect = diconnect;
            _sendQueue = new Queue<RSProtoBuffSSHMsg>();
            _receiveQueue = new Queue<RSProtoBuffSSHMsg>();
        }

        public bool Connect(string host, ushort port, string user, string pw)
        {
            if (!_connected && !_disconnecting)
            {
                _sendQueue.Clear();
                _receiveQueue.Clear();
                _rsConnector = new RSSSHConnector(host, port, user, pw);
                if (_rsConnector.Connect())
                {
                    _rsProtoBuf = new RSProtoBuf(_rsConnector.Stream, _sendQueue, _receiveQueue, this);
                    _connected = true;
                    _run = true;

                    _t = new Thread(new ThreadStart(ProcessNewMsgLoop));
                    _t.Name = "Process new msg loop";
                    _t.Priority = ThreadPriority.Normal;
                    _t.Start();

                    return true;
                }
            }
            return false;
        }

        public void Disconnect(bool shutdown = false)
        {
            if (_connected && !_disconnecting)
            {
                _disconnecting = true;
                _run = false;
                if (_useProperDisconnect)
                {
                    if (shutdown)
                        SystemShutDown();
                    SystemCloseConnection();
                }
                _shutdownThread = new Thread(new ThreadStart(ShutDownThread));
                _shutdownThread.Name = "Shutdown Thread";
                _shutdownThread.Priority = ThreadPriority.Normal;
                _shutdownThread.Start();
            }
        }

        public bool Reconnect(out ShellStream stream)
        {
            _event(EventType.Reconnect, null);
            Thread.Sleep(500);
            _rsProtoBuf.BreakConnection();
            return _rsConnector.Reconnect(out stream);
        }

        internal void Error(Exception e)
        {
            _event(EventType.Error, e);
        }

        private void ProcessNewMsgLoop()
        {
            RSProtoBuffSSHMsg msg;
            while (_run)
            {
                if (_receiveQueue.Count > 0 && _connected)
                {
                    lock (_receiveQueue)
                        msg = _receiveQueue.Dequeue();
                    if(RSProtoBuf.IsRpcMsgIdResponse(msg.MsgID))
                        _receivedMsg(msg);
                }
                Thread.Sleep(125);
            }
        }

        private void ShutDownThread()
        {
            byte counter = 0;
            _rsProtoBuf.FinishQueue();
            while (_rsProtoBuf.ThreadRunning && counter < 10 * 2)
            {
                Thread.Sleep(500);
                counter++;
            }
            _rsProtoBuf.StopThread();
            Thread.Sleep(100);
            if (!_useProperDisconnect)
            {
                _rsProtoBuf.BreakConnection();
            }
            _rsProtoBuf = null;
            _rsConnector.Disconnect();
            _rsConnector = null;
            _connected = false;
            _disconnecting = false;
        }

        // ---------- send ----------
        // ---------- generic send<T> ----------
        public uint Send<T>(T pbMsg, uint inMsgID)
        {
            if (!_connected)
            {
                Error(new Exception("not connected"));
                return 0;
            }

            RSProtoBuffSSHMsg msg = new RSProtoBuffSSHMsg();
            msg.MsgID = inMsgID;
            msg.ReqID = _rsProtoBuf.GetReqID();
            msg.ProtoBuffMsg = new MemoryStream();
            Serializer.Serialize<T>(msg.ProtoBuffMsg, pbMsg);
            msg.ProtoBuffMsg.Position = 0;
            msg.BodySize = (uint)msg.ProtoBuffMsg.Length;

            lock (_sendQueue)
                _sendQueue.Enqueue(msg);

            return msg.ReqID;
        }

        #region chat
        public uint ChatGetLobbies(RequestChatLobbies.LobbySet type)
        {
            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.CHAT,
                    (byte)rsctrl.chat.RequestMsgIds.MsgId_RequestChatLobbies,
                    false
                );

            RequestChatLobbies request = new RequestChatLobbies();
            request.lobby_set = type;
            return Send<RequestChatLobbies>(request, msgID);
        }

        public uint ChatCreateLobby(string name, string topic, LobbyPrivacyLevel privacy)
        {
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
            return Send<RequestCreateLobby>(request, msgID);
        }

        public uint ChatJoinLeaveLobby(RequestJoinOrLeaveLobby.LobbyAction action, string lobbyID)
        {
            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.CHAT,
                    (byte)rsctrl.chat.RequestMsgIds.MsgId_RequestJoinOrLeaveLobby,
                    false
                );

            RequestJoinOrLeaveLobby request = new RequestJoinOrLeaveLobby();
            request.action = action;
            request.lobby_id = lobbyID;
            return Send<RequestJoinOrLeaveLobby>(request, msgID);
        }

        public uint ChatRegisterEvent(RequestRegisterEvents.RegisterAction action)
        {
            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.CHAT,
                    (byte)rsctrl.chat.RequestMsgIds.MsgId_RequestRegisterEvents,
                    false
                );

            RequestRegisterEvents request = new RequestRegisterEvents();
            request.action = action;
            return Send<RequestRegisterEvents>(request, msgID);
        }

        public uint ChatSendMsg(ChatMessage msg)
        {
            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.CHAT,
                    (byte)rsctrl.chat.RequestMsgIds.MsgId_RequestSendMessage,
                    false
                );

            RequestSendMessage request = new RequestSendMessage();
            request.msg = msg;
            return Send<RequestSendMessage>(request, msgID);
        }

        public uint ChatSetLobbyNickname(string name, List<string> lobbyIDs = null)
        {
            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.CHAT,
                    (byte)rsctrl.chat.RequestMsgIds.MsgId_RequestSetLobbyNickname,
                    false
                );

            RequestSetLobbyNickname request = new RequestSetLobbyNickname();
            if(lobbyIDs != null)
                request.lobby_ids.AddRange(lobbyIDs);
            request.nickname = name;
            return Send<RequestSetLobbyNickname>(request, msgID);
        }
        #endregion

        #region files

        public uint FilesControllDownload(RequestControlDownload.Action action, File file)
        {
            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.FILES,
                    (byte)rsctrl.files.RequestMsgIds.MsgId_RequestControlDownload,
                    false
                );

            RequestControlDownload request = new RequestControlDownload();
            request.action = action;
            request.file = file;
            return Send<RequestControlDownload>(request, msgID);
        }

        public uint FilesGetTransferList(Direction dir)
        {
            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.FILES,
                    (byte)rsctrl.files.RequestMsgIds.MsgId_RequestTransferList,
                    false
                );

            RequestTransferList request = new RequestTransferList();
            request.direction = dir;
            return Send<RequestTransferList>(request, msgID);
        }

        #endregion

        #region gxs
        #endregion

        #region msgs
        #endregion

        #region peers
        public uint PeersAddPeer(string cert, string gpgID)
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
            return Send<RequestAddPeer>(request, msgID);
        }

        public uint PeersModifyPeer(Person peer, RequestModifyPeer.ModCmd cmd)
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
            return Send<RequestModifyPeer>(request, msgID);
        }

        public uint PeersGetFriendList(RequestPeers.SetOption option)
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
            return Send<RequestPeers>(request, msgID);
        }
        #endregion

        #region search

        public uint SearchBasic(List<string> terms)
        {
            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.SEARCH,
                    (byte)rsctrl.search.RequestMsgIds.MsgId_RequestBasicSearch,
                    false
                );

            RequestBasicSearch request = new RequestBasicSearch();
            request.terms.AddRange(terms);
            return Send<RequestBasicSearch>(request, msgID);
        }

        public uint SearchClose(uint ID)
        {
            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.SEARCH,
                    (byte)rsctrl.search.RequestMsgIds.MsgId_RequestCloseSearch,
                    false
                );

            RequestCloseSearch request = new RequestCloseSearch();
            request.search_id = ID;
            return Send<RequestCloseSearch>(request, msgID);
        }

        public uint SearchList(uint ID)
        {
            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.SEARCH,
                    (byte)rsctrl.search.RequestMsgIds.MsgId_RequestListSearches,
                    false
                );

            RequestListSearches request = new RequestListSearches();
            return Send<RequestListSearches>(request, msgID);
        }

        public uint SearchResult(List<uint> IDs)
        {
            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.SEARCH,
                    (byte)rsctrl.search.RequestMsgIds.MsgId_RequestSearchResults,
                    false
                );

            RequestSearchResults request = new RequestSearchResults();
            request.search_ids.AddRange(IDs);
            return Send<RequestSearchResults>(request, msgID);
        }

        #endregion

        #region system
        public uint SystemGetStatus()
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

        private void SystemCloseConnection()
        {
            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.SYSTEM,
                    (byte)rsctrl.system.RequestMsgIds.MsgId_RequestSystemQuit,
                    false
                );

            RequestSystemQuit request = new RequestSystemQuit();
            request.quit_code = RequestSystemQuit.QuitCode.CLOSE_CHANNEL;

            //RSProtoBuffSSHMsg msg = new RSProtoBuffSSHMsg();
            //msg.MsgID = msgID;
            //msg.ReqID = _rsProtoBuf.GetReqID();
            //msg.ProtoBuffMsg = new MemoryStream();
            //Serializer.Serialize<RequestSystemQuit>(msg.ProtoBuffMsg, request);
            //msg.ProtoBuffMsg.Position = 0;
            //msg.BodySize = (uint)msg.ProtoBuffMsg.Length;

            //_rsProtoBuf.Send(msg);

            Send<RequestSystemQuit>(request, msgID);
        }

        private void SystemShutDown()
        {
            uint msgID = RSProtoBuf.ConstructMsgId(
                    (byte)ExtensionId.CORE,
                    (ushort)PackageId.SYSTEM,
                    (byte)rsctrl.system.RequestMsgIds.MsgId_RequestSystemQuit,
                    false
                );

            RequestSystemQuit request = new RequestSystemQuit();
            request.quit_code = RequestSystemQuit.QuitCode.SHUTDOWN_RS;

            //RSProtoBuffSSHMsg msg = new RSProtoBuffSSHMsg();
            //msg.MsgID = msgID;
            //msg.ReqID = _rsProtoBuf.GetReqID();
            //msg.ProtoBuffMsg = new MemoryStream();
            //Serializer.Serialize<RequestSystemQuit>(msg.ProtoBuffMsg, request);
            //msg.ProtoBuffMsg.Position = 0;
            //msg.BodySize = (uint)msg.ProtoBuffMsg.Length;

            //_rsProtoBuf.Send(msg);
            Send<RequestSystemQuit>(request, msgID);
        }
        #endregion
        
    }
}
