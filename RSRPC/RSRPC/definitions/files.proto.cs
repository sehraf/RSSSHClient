//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: files.proto
// Note: requires additional types generated from: core.proto
namespace rsctrl.files
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"FileTransfer")]
  public partial class FileTransfer : global::ProtoBuf.IExtensible
  {
    public FileTransfer() {}
    
    private rsctrl.core.File _file;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"file", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public rsctrl.core.File file
    {
      get { return _file; }
      set { _file = value; }
    }
    private rsctrl.files.Direction _direction;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"direction", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public rsctrl.files.Direction direction
    {
      get { return _direction; }
      set { _direction = value; }
    }
    private float _fraction;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"fraction", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public float fraction
    {
      get { return _fraction; }
      set { _fraction = value; }
    }
    private float _rate_kBs;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"rate_kBs", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public float rate_kBs
    {
      get { return _rate_kBs; }
      set { _rate_kBs = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RequestTransferList")]
  public partial class RequestTransferList : global::ProtoBuf.IExtensible
  {
    public RequestTransferList() {}
    
    private rsctrl.files.Direction _direction;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"direction", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public rsctrl.files.Direction direction
    {
      get { return _direction; }
      set { _direction = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ResponseTransferList")]
  public partial class ResponseTransferList : global::ProtoBuf.IExtensible
  {
    public ResponseTransferList() {}
    
    private rsctrl.core.Status _status;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public rsctrl.core.Status status
    {
      get { return _status; }
      set { _status = value; }
    }
    private readonly global::System.Collections.Generic.List<rsctrl.files.FileTransfer> _transfers = new global::System.Collections.Generic.List<rsctrl.files.FileTransfer>();
    [global::ProtoBuf.ProtoMember(2, Name=@"transfers", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<rsctrl.files.FileTransfer> transfers
    {
      get { return _transfers; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RequestControlDownload")]
  public partial class RequestControlDownload : global::ProtoBuf.IExtensible
  {
    public RequestControlDownload() {}
    
    private rsctrl.core.File _file;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"file", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public rsctrl.core.File file
    {
      get { return _file; }
      set { _file = value; }
    }
    private rsctrl.files.RequestControlDownload.Action _action;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"action", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public rsctrl.files.RequestControlDownload.Action action
    {
      get { return _action; }
      set { _action = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"Action")]
    public enum Action
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"ACTION_START", Value=1)]
      ACTION_START = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ACTION_CONTINUE", Value=2)]
      ACTION_CONTINUE = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ACTION_WAIT", Value=3)]
      ACTION_WAIT = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ACTION_PAUSE", Value=4)]
      ACTION_PAUSE = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ACTION_RESTART", Value=5)]
      ACTION_RESTART = 5,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ACTION_CHECK", Value=6)]
      ACTION_CHECK = 6,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ACTION_CANCEL", Value=7)]
      ACTION_CANCEL = 7
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ResponseControlDownload")]
  public partial class ResponseControlDownload : global::ProtoBuf.IExtensible
  {
    public ResponseControlDownload() {}
    
    private rsctrl.core.Status _status;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public rsctrl.core.Status status
    {
      get { return _status; }
      set { _status = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"RequestMsgIds")]
    public enum RequestMsgIds
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"MsgId_RequestTransferList", Value=1)]
      MsgId_RequestTransferList = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MsgId_RequestControlDownload", Value=2)]
      MsgId_RequestControlDownload = 2
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"ResponseMsgIds")]
    public enum ResponseMsgIds
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"MsgId_ResponseTransferList", Value=1)]
      MsgId_ResponseTransferList = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MsgId_ResponseControlDownload", Value=2)]
      MsgId_ResponseControlDownload = 2
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"Direction")]
    public enum Direction
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"DIRECTION_UPLOAD", Value=1)]
      DIRECTION_UPLOAD = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"DIRECTION_DOWNLOAD", Value=2)]
      DIRECTION_DOWNLOAD = 2
    }
  
}