using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sehraf.RSRPC;

namespace RSRPC_minimal_implementation
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * First thing to do is to instance the RSPRC class.
             * For now the parameter must be "false" (the server side disconnect function is disabled) 
             * 
             * Some backgroud infos:
             * For some reason the SSH.NET lib does not disconnect well from the server. 
             * When i call the ssh disconnect function the connection is closed but only on client side.
             * The server still thinks he has an open ssh connection (which blocks every future connection attempt).
             * (If anybody knows how to fix this let me know!)
             * 
             * With the parameter being "true" the lib uses a server side disconnect function to close the connection.
             * With the parameter being "false" the lib sends some bytes, the server doesn't understand, causing the RPC and SSH system to reset.
             * (The best thing would be a 100% wroking disconnect function :P )
             */
            RSRPC _rpc = new RSRPC(false);

            /*
             * Now you have to set the functions that should be called when events occur.
             * 
             * There are two events:
             * 1) generic Event: It has two sub types:
             *      a) Error: something when wrong. The object is a "Exception" in this case.
             *      b) Reconnect: when the lib detects transmitting problems it reconnects (which is the easiest way to fix problems ;) )
             *          Why is the a event for it? Because things like chat registration and search results are cleared on the server side!
             *          So you'll have to take care of this.
             * 2) message received: This message is obviously a RPC message. Do what ever you want to do with it :P
             *      I suggest to process the message. How? See below!
             * 
             * Note:
             * Both events are called from a thread and you can NOT use the events to change GUI stuff. 
             * Instead you have to invoke the GUI (main) thread!
             */
            _rpc.EventOccurred += EventFromThread;
            _rpc.ReceivedMsg += ProcessMsgFromThread;

            /*
             * Next step: connect \o/ 
             * 
             * You can check the return value of the function or use _rpc.IsConnected to see if you are successfully connected
             * (I chose the second way)
             */
            _rpc.Connect(
                    "127.0.0.1",    // IP
                    7022,           // port
                    "user",         // user name 
                    "passwd"        // password
                );

            if (!_rpc.IsConnected)
            {
                System.Console.WriteLine("can't connect");
                return;
            }

            /*
             * At this point you can do what ever you want to do.
             * When you want to send a request to the server just call the proper function!
             * (In this example it is SystemGetStatus() )
             * 
             * Note:
             * The function name schema is:
             *      package + call
             * So chat function calls are always like this "Chat...." (e.g. ChatGetLobbies, ChatCreateLobby, ...)
             */

            for (byte i = 0; i < 10; i++)
            {
                System.Console.WriteLine("requestion system status");
                _rpc.SystemGetStatus();
                System.Threading.Thread.Sleep(1000);
            }

            /*
             * Don't forget to disconnect when you are done.
             * Optional you can shutdown the server (this is disabled right now)
             */
            _rpc.Disconnect();
        }

        static private void EventFromThread(RSRPC.EventType type, object obj)
        {
            switch (type)
            {
                case RSRPC.EventType.Error:
                    System.Diagnostics.Debug.WriteLine("error");
                    break;
                case RSRPC.EventType.Reconnect:
                    System.Diagnostics.Debug.WriteLine("reconnect");
                    break;
            }
        }


        static private void ProcessMsgFromThread(RSProtoBuffSSHMsg msg)
        {
            System.Console.WriteLine("got msg");
            /*
             * So we received a RPC message. What do we know at this point?
             * We know that the magic code of the message is ok which means that the chance is high that the rest is ok, too.
             * 
             * 
             * First of all we should check if the message is a response. 
             * Since the server only sends responses we do not expect anything else.
             * 
             * Note: actually the lib is doing this on its own and you can skip this test.
             * (I keep it for educational purpose :P)
             */
            if (!RSProtoBuf.IsRpcMsgIdResponse(msg.MsgID))
                return;

            /*
             * Next step is to get the informations about the message:
             *      extension, service and submsg
             *      
             * These 3 things define the content of the message.
             * (In this example i only requested the system status so i only expect a response to this call)
             */
            byte extension  = RSProtoBuf.GetRpcMsgIdExtension(msg.MsgID);
            ushort service  = RSProtoBuf.GetRpcMsgIdService(msg.MsgID);
            byte submsg     = RSProtoBuf.GetRpcMsgIdSubMsg(msg.MsgID);

            // check extension
            if(extension != (byte) rsctrl.core.ExtensionId.CORE)
                return;

            // check service
            if (service != (ushort)rsctrl.core.PackageId.SYSTEM)
                return;

            // check submsg
            if (submsg != (byte)rsctrl.system.ResponseMsgIds.MsgId_ResponseSystemStatus)
                return;

            /*
             * Now we know the content of the message and can deserialize it
             * (The example message is a response to our system status request!)
             */

            rsctrl.system.ResponseSystemStatus response = new rsctrl.system.ResponseSystemStatus();
            Exception e;
            if (!RSProtoBuf.Deserialize<rsctrl.system.ResponseSystemStatus>(msg.ProtoBuffMsg, out response, out e))
                System.Console.WriteLine("ProcessSystemstatus: error deserializing " + e.Message);

            /*
             * Last thing you should do is to check if the response.status is SUCCESS.
             * In any other case something went wrong.
             * 
             * After this you can continue processing the message in the proper way e.g. update system status
             */
            if (response.status == null || response.status.code != rsctrl.core.Status.StatusCode.SUCCESS)
                return;

            System.Console.WriteLine("Systemstatus: ");
            System.Console.WriteLine(" -- peers: " + response.no_peers);
            System.Console.WriteLine(" -- online: " + response.no_connected);
        }
    }
}
