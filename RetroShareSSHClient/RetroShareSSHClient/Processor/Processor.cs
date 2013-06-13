using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

using Sehraf.RSRPC;

using rsctrl.chat;
using rsctrl.core;
using rsctrl.files;
//using rsctrl.gxs;
//using rsctrl.msgs;
using rsctrl.peers;
using rsctrl.search;
using rsctrl.stream;
using rsctrl.system;

namespace RetroShareSSHClient
{
    internal class Processor
    {
        Bridge _b;

        const bool DEBUG = false;
        const string pattern = "<.*?>";

        public Processor()
        {
            _b = Bridge.GetBridge();
        }

        internal void Reset()
        {
            
        }

        public static string RemoteTags(string inputString)
        {
            return Regex.Replace
              (inputString, pattern, string.Empty);
        }

        public static double conv_Date2Timestam(DateTime date2)
        {
            DateTime date1 = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan ts = new TimeSpan(date2.Ticks - date1.Ticks); 
            return ts.TotalSeconds;
        }

        public static DateTime conv_Timestamp2Date(double Timestamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);         
            dateTime = dateTime.AddSeconds(Timestamp);
            return dateTime;
        }

        public static string BuildSizeString(ulong size)
        {
            byte counter = 0;
            float sizef = size;
            while (sizef > 1024)
            {
                counter++;
                sizef /= 1024;
            }
            string s = "";
            switch (counter)
            {
                case 0:
                    s = "B";
                    break;
                case 1:
                    s = "KiB";
                    break;
                case 2:
                    s = "MiB";
                    break;
                case 3:
                    s = "GiB";
                    break;
                case 4:
                    s = "TiB";
                    break;
                case 5:
                    s = "PiB";
                    break;
                default:
                    s = "too damn high";
                    break;
            }
            return String.Format("{0:0.00}", sizef) + s;
        }

        //public static string GetRandomInsult()
        //{
        //    string fileName = Settings.InsultListFileName;
        //    string[] list = System.IO.File.ReadAllLines(fileName);

        //    Random rnd = new Random(DateTime.UtcNow.Millisecond);
        //    int random = rnd.Next(list.Length - 1);

        //    return list[random];
        //}

        internal void ProcessMsg(RSProtoBuffSSHMsg msg)
        {
            byte extension = RSProtoBuf.GetRpcMsgIdExtension(msg.MsgID);
            ushort service = RSProtoBuf.GetRpcMsgIdService(msg.MsgID);
            System.Diagnostics.Debug.WriteLineIf(DEBUG, "Processing Msg " + msg.ReqID + " .....");
            System.Diagnostics.Debug.WriteLineIf(DEBUG, "ext: " + extension + " - service: " + service + " - submsg: " + RSProtoBuf.GetRpcMsgIdSubMsg(msg.MsgID));
            //System.Diagnostics.Debug.WriteLineIf(true, "Processing Msg " + msg.ReqID + " .....");
            //System.Diagnostics.Debug.WriteLineIf(true, "ext: " + extension + " - service: " + service + " - submsg: " + RSProtoBuf.GetRpcMsgIdSubMsg(msg.MsgID));
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
            //System.Diagnostics.Debug.WriteLine(" ----------- processing chat ----------");
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
                case (byte)rsctrl.chat.RequestMsgIds.MsgId_RequestChatHistory:
                    // don't need this -> drop it
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
                        //_b.ChatProcessor.AddMsgToGroupChat(response);
                        _b.ChatProcessor.AddMsgToLobby(ChatProcessor.BROADCAST, response.msg);
                        break;
                    case ChatType.TYPE_LOBBY:
                        _b.ChatProcessor.AddMsgToLobby(response.msg.id.chat_id, response.msg);
                        break;
                    case ChatType.TYPE_PRIVATE:
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLineIf(DEBUG, "ProcessChatMsg: unknown chat type");
                        break;
                }

            }
            else
                System.Diagnostics.Debug.WriteLine("ProcessChatMsg: error deserializing " + e.Message);
        }

        private void ProcessLobbyInvite(RSProtoBuffSSHMsg msg)
        {
            EventLobbyInvite response = new EventLobbyInvite();
            Exception e; 
            if (RSProtoBuf.Deserialize<EventLobbyInvite>(msg.ProtoBuffMsg, out response, out e))
                _b.ChatProcessor.LobbyInvite(response);
            else
                System.Diagnostics.Debug.WriteLine("ProcessChatMsg: error deserializing " + e.Message);
        }

        private void ProcessChatLobbies(RSProtoBuffSSHMsg msg)
        {
            ResponseChatLobbies response = new ResponseChatLobbies();
            Exception e; 
            if (RSProtoBuf.Deserialize<ResponseChatLobbies>(msg.ProtoBuffMsg, out response, out e))
            {
                if (response.status.code == Status.StatusCode.SUCCESS)
                    _b.ChatProcessor.UpdateChatLobbies(response);
            }
            else
                System.Diagnostics.Debug.WriteLine("ProcessChatMsg: error deserializing " + e.Message);
        }

        private void ProcessRegisterEvents(RSProtoBuffSSHMsg msg)
        {
            ResponseRegisterEvents response = new ResponseRegisterEvents();
            Exception e; 
            if (RSProtoBuf.Deserialize<ResponseRegisterEvents>(msg.ProtoBuffMsg, out response, out e))
            {
                _b.GUI.tb_out.AppendText("ReqRegisterEvents response: " + response.status.code + "\n");
            }
            else
                System.Diagnostics.Debug.WriteLine("ProcessChatMsg: error deserializing " + e.Message);
        }

        private void ProcessSendMsg(RSProtoBuffSSHMsg msg)
        {
            ResponseSendMessage response = new ResponseSendMessage();
            Exception e; 
            if (RSProtoBuf.Deserialize<ResponseSendMessage>(msg.ProtoBuffMsg, out response, out e))
            {
                _b.GUI.tb_out.AppendText("ReqSendMsg response:" + response.status.code + "\n");
            }
            else
                System.Diagnostics.Debug.WriteLine("ProcessChatMsg: error deserializing " + e.Message);
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
                case (byte)rsctrl.files.ResponseMsgIds.MsgId_ResponseShareDirList:
                    ProcessShareDirList(msg);
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
                _b.GUI.tb_out.AppendText("ReqContrDL response:" + response.status.code + "\n");
            }
            else
                System.Diagnostics.Debug.WriteLine("ProcessControllDownload: error deserializing " + e.Message);
        }

        private void ProcessTransferlist(RSProtoBuffSSHMsg msg)
        {
            ResponseTransferList response = new ResponseTransferList();
            Exception e; 
            if (RSProtoBuf.Deserialize<ResponseTransferList>(msg.ProtoBuffMsg, out response, out e))
            {
                if (response.status.code == Status.StatusCode.SUCCESS)
                {
                    _b.FileProcessor.UpdateFileTransfers(response, msg.ReqID);
                    //_b.FileProcessor.UpdateFileTransfersNEW(response, msg.ReqID);
                }
            }
            else
                System.Diagnostics.Debug.WriteLine("ProcessTransferlist: error deserializing " + e.Message);
        }

        private void ProcessShareDirList(RSProtoBuffSSHMsg msg)
        {
            ResponseShareDirList response = new ResponseShareDirList();
            Exception e;
            if (RSProtoBuf.Deserialize<ResponseShareDirList>(msg.ProtoBuffMsg, out response, out e))
            {
                if (response.status.code == Status.StatusCode.SUCCESS)
                    throw new NotImplementedException();
            }
            else
                System.Diagnostics.Debug.WriteLine("ProcessShareDirList: error deserializing " + e.Message);
        }

        // ---------- peers ----------
        private void ProcessPeer(RSProtoBuffSSHMsg msg)
        {
            byte submsg = RSProtoBuf.GetRpcMsgIdSubMsg(msg.MsgID);
            switch (submsg)
            {
                //case (byte)rsctrl.peers.ResponseMsgIds.MsgId_RequestAddPeer:
                //    ProcessAddPeer(msg);
                //    break;
                //case (byte)rsctrl.peers.ResponseMsgIds.MsgId_RequestModifyPeer:
                //    ProcessModifyPeer(msg);
                //    break;
                case (byte)rsctrl.peers.ResponseMsgIds.MsgId_ResponsePeerList:
                    ProcessPeerList(msg);
                    break;
                default:
                    System.Diagnostics.Debug.WriteLineIf(DEBUG, "ProcessPeer: unknown submsg " + submsg);
                    break;
            }
        }

        //private void ProcessAddPeer(RSProtoBuffSSHMsg msg)
        //{
        //    Respon response = new ResponseAddPeer();
        //    Exception e; 
        //    if (RSProtoBuf.Deserialize<ResponseAddPeer>(msg.ProtoBuffMsg, out response, out e))
        //        _b.GUI.tb_out.AppendText("AddPeer response: " + response.status.code + "\n");
        //    else
        //        System.Diagnostics.Debug.WriteLine("ProcessAddPeer: error deserializing " + e.Message);
        //}

        //private void ProcessModifyPeer(RSProtoBuffSSHMsg msg)
        //{
        //    ResponseModifyPeer response = new ResponseModifyPeer();
        //    Exception e; 
        //    if (RSProtoBuf.Deserialize<ResponseModifyPeer>(msg.ProtoBuffMsg, out response, out e))
        //        _b.GUI.tb_out.AppendText("AddPeer response: " + response.status.code + "\n");
        //    else
        //        System.Diagnostics.Debug.WriteLine("ProcessModifyPeer: error deserializing " + e.Message);
        //}

        private void ProcessPeerList(RSProtoBuffSSHMsg msg)
        {
            ResponsePeerList response = new ResponsePeerList();
            Exception e;
            if (RSProtoBuf.Deserialize<ResponsePeerList>(msg.ProtoBuffMsg, out response, out e))
                if (response.status.code == Status.StatusCode.SUCCESS)
                    //_b.PeerProcessor.ProcessResponsePeerList(msg.ReqID, response);
                    _b.PeerProcessor.UpdatePeerList(response);
                else
                    System.Diagnostics.Debug.WriteLine("ProcessPeerList: error deserializing " + e.Message);
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
                    _b.SearchProcessor.RegisterSearchIDs(msg.ReqID, response);
                else
                    _b.GUI.tb_out.AppendText("SearchIDs response: " + response.status.code + "\n");
            else
                System.Diagnostics.Debug.WriteLine("ProcessSearchIDs: error deserializing " + e.Message);
        }

        private void ProcessSearchResults(RSProtoBuffSSHMsg msg)
        {
            ResponseSearchResults response = new ResponseSearchResults();
            Exception e; 
            if (RSProtoBuf.Deserialize<ResponseSearchResults>(msg.ProtoBuffMsg, out response, out e))
                if (response.status.code == Status.StatusCode.SUCCESS)
                    _b.SearchProcessor.ProcessSearchResults(response);
                else
                    _b.GUI.tb_out.AppendText("SearchResults response: " + response.status.code + "\n");
            else
                System.Diagnostics.Debug.WriteLine("ProcessSearchResults: error deserializing " + e.Message);
        }

        // ---------- stream ----------

        private void ProcessStream(RSProtoBuffSSHMsg msg)
        {
            byte submsg = RSProtoBuf.GetRpcMsgIdSubMsg(msg.MsgID);
            switch (submsg)
            {
                case (byte)rsctrl.stream.ResponseMsgIds.MsgId_ResponseStreamDetail:
                    ProcessStreamDetails(msg);
                    break;
                case (byte)rsctrl.stream.ResponseMsgIds.MsgId_ResponseStreamData:
                    ProcessStreamData(msg);
                    break;
                default:
                    System.Diagnostics.Debug.WriteLineIf(DEBUG, "ProcessStream: unknown submsg " + submsg);
                    break;
            }
        }

        private void ProcessStreamDetails(RSProtoBuffSSHMsg msg)
        {
            ResponseStreamDetail response = new ResponseStreamDetail();
            Exception e;
            if (RSProtoBuf.Deserialize<ResponseStreamDetail>(msg.ProtoBuffMsg, out response, out e))
                if(response.status.code == Status.StatusCode.SUCCESS)
                    _b.StreamProcessor.StreamDetails(response);
                else _b.GUI.tb_out.AppendText("StreamDetails response: " + response.status.code + "\n");
            else
                System.Diagnostics.Debug.WriteLine("ProcessStreamDetails: error deserializing " + e.Message);
        }

        private void ProcessStreamData(RSProtoBuffSSHMsg msg)
        {
            ResponseStreamData response = new ResponseStreamData();
            Exception e;
            if (RSProtoBuf.Deserialize<ResponseStreamData>(msg.ProtoBuffMsg, out response, out e))
                if (response.status.code == Status.StatusCode.SUCCESS)
                    _b.StreamProcessor.StreamData(response);
                else _b.GUI.tb_out.AppendText("StreamData response: " + response.status.code + "\n");
            else
                System.Diagnostics.Debug.WriteLine("ProcessStreamData: error deserializing " + e.Message);
        }

        // ---------- system ----------
        private void ProcessSystem(RSProtoBuffSSHMsg msg)
        {
            byte submsg = RSProtoBuf.GetRpcMsgIdSubMsg(msg.MsgID);
            switch (submsg)
            {
                case (byte)rsctrl.system.ResponseMsgIds.MsgId_ResponseSystemStatus:
                    ProcessSystemStatus(msg);
                    break;
                case (byte)rsctrl.system.ResponseMsgIds.MsgId_ResponseSystemAccount:
                    ProcessSystemAccount(msg);
                    break;
                default:
                    System.Diagnostics.Debug.WriteLineIf(DEBUG, "ProcessSystem: unknown submsg " + submsg);
                    break;
            }
        }

        private void ProcessSystemStatus(RSProtoBuffSSHMsg msg)
        {
            ResponseSystemStatus response = new ResponseSystemStatus();
            Exception e; 
            if (RSProtoBuf.Deserialize<ResponseSystemStatus>(msg.ProtoBuffMsg, out response, out e))
                _b.GUI.UpdateSystemStatus(response);
            else
                System.Diagnostics.Debug.WriteLine("ProcessSystemstatus: error deserializing " + e.Message);
        }

        private void ProcessSystemAccount(RSProtoBuffSSHMsg msg)
        {
            ResponseSystemAccount response = new ResponseSystemAccount();
            Exception e;
            if (RSProtoBuf.Deserialize<ResponseSystemAccount>(msg.ProtoBuffMsg, out response, out e))
                _b.PeerProcessor.SetOwnID(response);
            else
                System.Diagnostics.Debug.WriteLine("ProcessSystemstatus: error deserializing " + e.Message);
        }
    }
}
