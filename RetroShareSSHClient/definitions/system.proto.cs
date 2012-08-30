//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: system.proto
// Note: requires additional types generated from: core.proto
namespace rsctrl.system
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RequestSystemStatus")]
  public partial class RequestSystemStatus : global::ProtoBuf.IExtensible
  {
    public RequestSystemStatus() {}
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ResponseSystemStatus")]
  public partial class ResponseSystemStatus : global::ProtoBuf.IExtensible
  {
    public ResponseSystemStatus() {}
    
    private rsctrl.core.Status _status;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public rsctrl.core.Status status
    {
      get { return _status; }
      set { _status = value; }
    }
    private uint _no_peers;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"no_peers", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint no_peers
    {
      get { return _no_peers; }
      set { _no_peers = value; }
    }
    private uint _no_connected;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"no_connected", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint no_connected
    {
      get { return _no_connected; }
      set { _no_connected = value; }
    }
    private rsctrl.system.ResponseSystemStatus.NetCode _net_status;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"net_status", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public rsctrl.system.ResponseSystemStatus.NetCode net_status
    {
      get { return _net_status; }
      set { _net_status = value; }
    }
    private rsctrl.core.Bandwidth _bw_total;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"bw_total", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public rsctrl.core.Bandwidth bw_total
    {
      get { return _bw_total; }
      set { _bw_total = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"NetCode")]
    public enum NetCode
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"BAD_UNKNOWN", Value=0)]
      BAD_UNKNOWN = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"BAD_OFFLINE", Value=1)]
      BAD_OFFLINE = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"BAD_NATSYM", Value=2)]
      BAD_NATSYM = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"BAD_NODHT_NAT", Value=3)]
      BAD_NODHT_NAT = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"WARNING_RESTART", Value=4)]
      WARNING_RESTART = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"WARNING_NATTED", Value=5)]
      WARNING_NATTED = 5,
            
      [global::ProtoBuf.ProtoEnum(Name=@"WARNING_NODHT", Value=6)]
      WARNING_NODHT = 6,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GOOD", Value=7)]
      GOOD = 7,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ADV_FORWARD", Value=8)]
      ADV_FORWARD = 8
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"RequestMsgIds")]
    public enum RequestMsgIds
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"MsgId_RequestSystemStatus", Value=1)]
      MsgId_RequestSystemStatus = 1
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"ResponseMsgIds")]
    public enum ResponseMsgIds
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"MsgId_ResponseSystemStatus", Value=1)]
      MsgId_ResponseSystemStatus = 1
    }
  
}