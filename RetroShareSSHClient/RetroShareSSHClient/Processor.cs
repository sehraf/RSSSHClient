using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using Sehraf.RSRPC;

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
    class Processor
    {
        MainForm _gui;

        const bool DEBUG = false;
        const string pattern = "<.*?>";

        Dictionary<uint, RequestPeers.SetOption> _pendingPeerRequests;

        public Dictionary<uint, RequestPeers.SetOption> PendingPeerRequests { get { return _pendingPeerRequests; } set { _pendingPeerRequests = value; } }

        public Processor(MainForm gui)
        {
            _gui = gui;

            _pendingPeerRequests = new Dictionary<uint, RequestPeers.SetOption>();
        }

        public static string RemoteTags(string inputString)
        {
            return Regex.Replace
              (inputString, pattern, string.Empty);
        }

        public void ProcessMsg(RSProtoBuffSSHMsg msg)
        {
            byte extension = RSProtoBuf.GetRpcMsgIdExtension(msg.MsgID);
            ushort service = RSProtoBuf.GetRpcMsgIdService(msg.MsgID);
            System.Diagnostics.Debug.WriteLineIf(DEBUG, "Processing Msg " + msg.ReqID + " .....");
            System.Diagnostics.Debug.WriteLineIf(DEBUG, "ext: " + extension + " - service: " + service + " - submsg: " + RSProtoBuf.GetRpcMsgIdSubMsg(msg.MsgID));
            //_gui.tb_out.AppendText(" -> " + msg.ReqID + "\n");
            switch (extension)
            {
                case (byte)rsctrl.core.ExtensionId.CORE:
                    switch (service)
                    {
                        case (ushort)PackageId.CHAT:
                            ProcessChat(msg);
                            break;
                        case (ushort)PackageId.FILES:
                            ProcessFiles(msg);
                            break;
                        case (ushort)PackageId.PEERS:
                            ProcessPeer(msg);
                            break;
                        case (ushort)PackageId.SEARCH:
                            ProcessSearch(msg);
                            break;
                        case (ushort)PackageId.SYSTEM:
                            ProcessSystem(msg);
                            break;

                        default:
                            System.Diagnostics.Debug.WriteLineIf(DEBUG, "ProcessMsg: unknown service " + service);
                            break;
                    } // service
                    break;
                default:
                    System.Diagnostics.Debug.WriteLineIf(DEBUG, "ProcessMsg: unknown extension " + extension);
                    break;
            } // extension
            System.Diagnostics.Debug.WriteLineIf(DEBUG, "##########################################");
        }

        // ---------- chat ----------
        private void ProcessChat(RSProtoBuffSSHMsg msg)
        {
            byte submsg = RSProtoBuf.GetRpcMsgIdSubMsg(msg.MsgID);
            switch (submsg)
            {
                case (byte)rsctrl.chat.ResponseMsgIds.MsgId_EventChatMessage:
                    ProcessChatMsg(msg);
                    break;
                case (byte)rsctrl.chat.ResponseMsgIds.MsgId_EventLobbyInvite:
                    ProcessLobbyInvite(msg);
                    break;
                case (byte)rsctrl.chat.ResponseMsgIds.MsgId_ResponseChatLobbies:
                    ProcessChatLobbies(msg);
                    break;
                case (byte)rsctrl.chat.ResponseMsgIds.MsgId_ResponseRegisterEvents:
                    ProcessRegisterEvents(msg);
                    break;
                case (byte)rsctrl.chat.ResponseMsgIds.MsgId_ResponseSendMessage:
                    ProcessSendMsg(msg);
                    break;
                default:
                    System.Diagnostics.Debug.WriteLineIf(DEBUG, "ProcessChat: unknown submsg " + submsg);
                    break;
            }
        }

        private void ProcessChatMsg(RSProtoBuffSSHMsg msg)
        {
            EventChatMessage response = new EventChatMessage();
            Exception e;
            if (RSProtoBuf.Deserialize<EventChatMessage>(msg.ProtoBuffMsg, out response, out e))
            {
                switch (response.msg.id.chat_type)
                {
                    case ChatType.TYPE_GROUP:
                        _gui.PrintMsgToGroupChat(response);
                        break;
                    case ChatType.TYPE_LOBBY:
                        _gui.PrintMsgToLobby(response.msg.id.chat_id, response);
                        break;
                    case ChatType.TYPE_PRIVATE:
                        break;
                    default:
                        // :S
                        break;
                }

            }
            else
                System.Diagnostics.Debug.WriteLine("ProcessChatMsg: error deserializing");
        }

        private void ProcessLobbyInvite(RSProtoBuffSSHMsg msg)
        {
            EventLobbyInvite response = new EventLobbyInvite();
            Exception e; 
            if (RSProtoBuf.Deserialize<EventLobbyInvite>(msg.ProtoBuffMsg, out response, out e))
                _gui.LobbyInvite(response);
            else
                System.Diagnostics.Debug.WriteLine("ProcessChatMsg: error deserializing");
        }

        private void ProcessChatLobbies(RSProtoBuffSSHMsg msg)
        {
            ResponseChatLobbies response = new ResponseChatLobbies();
            Exception e; 
            if (RSProtoBuf.Deserialize<ResponseChatLobbies>(msg.ProtoBuffMsg, out response, out e))
            {
                if (response.status.code == Status.StatusCode.SUCCESS)
                    _gui.UpdateChatLobbies(response);
            }
            else
                System.Diagnostics.Debug.WriteLine("ProcessChatMsg: error deserializing");
        }

        private void ProcessRegisterEvents(RSProtoBuffSSHMsg msg)
        {
            ResponseRegisterEvents response = new ResponseRegisterEvents();
            Exception e; 
            if (RSProtoBuf.Deserialize<ResponseRegisterEvents>(msg.ProtoBuffMsg, out response, out e))
            {
                _gui.tb_out.AppendText("ReqRegisterEvents response: " + response.status.code + "\n");
            }
            else
                System.Diagnostics.Debug.WriteLine("ProcessChatMsg: error deserializing");
        }

        private void ProcessSendMsg(RSProtoBuffSSHMsg msg)
        {
            ResponseSendMessage response = new ResponseSendMessage();
            Exception e; 
            if (RSProtoBuf.Deserialize<ResponseSendMessage>(msg.ProtoBuffMsg, out response, out e))
            {
                _gui.tb_out.AppendText("ReqSendMsg response:" + response.status.code + "\n");
            }
            else
                System.Diagnostics.Debug.WriteLine("ProcessChatMsg: error deserializing");
        }

        // ---------- files ----------
        private void ProcessFiles(RSProtoBuffSSHMsg msg)
        {
            byte submsg = RSProtoBuf.GetRpcMsgIdSubMsg(msg.MsgID);
            switch (submsg)
            {
                case (byte)rsctrl.files.ResponseMsgIds.MsgId_ResponseControlDownload:
                    ProcessControllDownload(msg);
                    break;
                case (byte)rsctrl.files.ResponseMsgIds.MsgId_ResponseTransferList:
                    ProcessTransferlist(msg);
                    break;
                default:
                    System.Diagnostics.Debug.WriteLineIf(DEBUG, "ProcessFiles: unknown submsg " + submsg);
                    break;
            }
        }

        private void ProcessControllDownload(RSProtoBuffSSHMsg msg)
        {
            ResponseControlDownload response = new ResponseControlDownload();
            Exception e; 
            if (RSProtoBuf.Deserialize<ResponseControlDownload>(msg.ProtoBuffMsg, out response, out e))
            {
                _gui.tb_out.AppendText("ReqContrDL response:" + response.status.code + "\n");
            }
            else
                System.Diagnostics.Debug.WriteLine("ProcessControllDownload: error deserializing");
        }

        private void ProcessTransferlist(RSProtoBuffSSHMsg msg)
        {
            ResponseTransferList response = new ResponseTransferList();
            Exception e; 
            if (RSProtoBuf.Deserialize<ResponseTransferList>(msg.ProtoBuffMsg, out response, out e))
            {
                if (response.status.code == Status.StatusCode.SUCCESS)
                    _gui.UpdateFileTransfers(response);
            }
            else
                System.Diagnostics.Debug.WriteLine("ProcessTransferlist: error deserializing");
        }

        // ---------- peers ----------
        private void ProcessPeer(RSProtoBuffSSHMsg msg)
        {
            byte submsg = RSProtoBuf.GetRpcMsgIdSubMsg(msg.MsgID);
            switch (submsg)
            {
                case (byte)rsctrl.peers.RequestMsgIds.MsgId_RequestAddPeer:
                    ProcessAddPeer(msg);
                    break;
                case (byte)rsctrl.peers.RequestMsgIds.MsgId_RequestModifyPeer:
                    ProcessModifyPeer(msg);
                    break;
                case (byte)rsctrl.peers.RequestMsgIds.MsgId_RequestPeers:
                    ProcessPeerList(msg);
                    break;
                default:
                    System.Diagnostics.Debug.WriteLineIf(DEBUG, "ProcessPeer: unknown submsg " + submsg);
                    break;
            }
        }

        private void ProcessAddPeer(RSProtoBuffSSHMsg msg)
        {
            ResponseAddPeer response = new ResponseAddPeer();
            Exception e; 
            if (RSProtoBuf.Deserialize<ResponseAddPeer>(msg.ProtoBuffMsg, out response, out e))
                _gui.tb_out.AppendText("AddPeer response: " + response.status.code + "\n");
            else
                System.Diagnostics.Debug.WriteLine("ProcessAddPeer: error deserializing");
        }

        private void ProcessModifyPeer(RSProtoBuffSSHMsg msg)
        {
            ResponseModifyPeer response = new ResponseModifyPeer();
            Exception e; 
            if (RSProtoBuf.Deserialize<ResponseModifyPeer>(msg.ProtoBuffMsg, out response, out e))
                _gui.tb_out.AppendText("AddPeer response: " + response.status.code + "\n");
            else
                System.Diagnostics.Debug.WriteLine("ProcessModifyPeer: error deserializing");
        }

        private void ProcessPeerList(RSProtoBuffSSHMsg msg)
        {
            ResponsePeerList response = new ResponsePeerList();
            Exception e; 
            if (RSProtoBuf.Deserialize<ResponsePeerList>(msg.ProtoBuffMsg, out response, out e))
            {
                RequestPeers.SetOption opt;
                if (response.status.code == Status.StatusCode.SUCCESS)
                {
                    if (_pendingPeerRequests.TryGetValue(msg.ReqID, out opt))
                        switch (opt)
                        {
                            case RequestPeers.SetOption.OWNID:
                                _gui.SetOwnID(response);
                                break;
                            case RequestPeers.SetOption.FRIENDS:
                                _gui.UpdatePeerList(response);
                                break;
                            default:
                                // all other cases 
                                break;
                        }
                    else
                    {
                        // peer list but no clue what is in it
                    }
                }
            }
            else
                System.Diagnostics.Debug.WriteLine("ProcessPeerList: error deserializing");
        }

        // ---------- search ----------
        private void ProcessSearch(RSProtoBuffSSHMsg msg)
        {
            byte submsg = RSProtoBuf.GetRpcMsgIdSubMsg(msg.MsgID);
            switch (submsg)
            {
                case (byte)rsctrl.search.ResponseMsgIds.MsgId_ResponseSearchIds:
                    ProcessSearchIDs(msg);
                    break;
                case (byte)rsctrl.search.ResponseMsgIds.MsgId_ResponseSearchResults:
                    ProcessSearchResults(msg);
                    break;
                default:
                    System.Diagnostics.Debug.WriteLineIf(DEBUG, "ProcessSearch: unknown submsg " + submsg);
                    break;
            }
        }

        private void ProcessSearchIDs(RSProtoBuffSSHMsg msg)
        {
            ResponseSearchIds response = new ResponseSearchIds();
            Exception e; 
            if (RSProtoBuf.Deserialize<ResponseSearchIds>(msg.ProtoBuffMsg, out response, out e))
                if (response.status.code == Status.StatusCode.SUCCESS)
                    _gui.RegisterSearchIDs(msg.ReqID, response);
                else
                    _gui.tb_out.AppendText("SearchIDs response: " + response.status.code + "\n");
            else
                System.Diagnostics.Debug.WriteLine("ProcessSearchIDs: error deserializing");
        }

        private void ProcessSearchResults(RSProtoBuffSSHMsg msg)
        {
            ResponseSearchResults response = new ResponseSearchResults();
            Exception e; 
            if (RSProtoBuf.Deserialize<ResponseSearchResults>(msg.ProtoBuffMsg, out response, out e))
                if (response.status.code == Status.StatusCode.SUCCESS)
                    _gui.ProcessSearchResults(response);
                else
                    _gui.tb_out.AppendText("SearchResults response: " + response.status.code + "\n");
            else
                System.Diagnostics.Debug.WriteLine("ProcessSearchResults: error deserializing");
        }

        // ---------- system ----------
        private void ProcessSystem(RSProtoBuffSSHMsg msg)
        {
            byte submsg = RSProtoBuf.GetRpcMsgIdSubMsg(msg.MsgID);
            switch (submsg)
            {
                case (byte)rsctrl.system.ResponseMsgIds.MsgId_ResponseSystemStatus:
                    ProcessSystemstatus(msg);
                    break;
                default:
                    System.Diagnostics.Debug.WriteLineIf(DEBUG, "ProcessSystem: unknown submsg " + submsg);
                    break;
            }
        }

        private void ProcessSystemstatus(RSProtoBuffSSHMsg msg)
        {
            ResponseSystemStatus response = new ResponseSystemStatus();
            Exception e; 
            if (RSProtoBuf.Deserialize<ResponseSystemStatus>(msg.ProtoBuffMsg, out response, out e))
                _gui.UpdateSystemStatus(response);
            else
                System.Diagnostics.Debug.WriteLine("ProcessSystemstatus: error deserializing");
        }
    }
}
