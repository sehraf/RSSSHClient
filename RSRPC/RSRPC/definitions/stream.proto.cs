//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: stream.proto
// Note: requires additional types generated from: core.proto
namespace rsctrl.stream
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"StreamFileDetail")]
  public partial class StreamFileDetail : global::ProtoBuf.IExtensible
  {
    public StreamFileDetail() {}
    
    private rsctrl.core.File _file;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"file", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public rsctrl.core.File file
    {
      get { return _file; }
      set { _file = value; }
    }
    private ulong _offset;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"offset", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong offset
    {
      get { return _offset; }
      set { _offset = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"StreamVoipDetail")]
  public partial class StreamVoipDetail : global::ProtoBuf.IExtensible
  {
    public StreamVoipDetail() {}
    
    private string _peer_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"peer_id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string peer_id
    {
      get { return _peer_id; }
      set { _peer_id = value; }
    }
    private ulong _duration;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"duration", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong duration
    {
      get { return _duration; }
      set { _duration = value; }
    }
    private ulong _offset;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"offset", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong offset
    {
      get { return _offset; }
      set { _offset = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"StreamDesc")]
  public partial class StreamDesc : global::ProtoBuf.IExtensible
  {
    public StreamDesc() {}
    
    private uint _stream_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"stream_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint stream_id
    {
      get { return _stream_id; }
      set { _stream_id = value; }
    }
    private rsctrl.stream.StreamType _stream_type;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"stream_type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public rsctrl.stream.StreamType stream_type
    {
      get { return _stream_type; }
      set { _stream_type = value; }
    }
    private rsctrl.stream.StreamState _stream_state;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"stream_state", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public rsctrl.stream.StreamState stream_state
    {
      get { return _stream_state; }
      set { _stream_state = value; }
    }
    private float _rate_kbs;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"rate_kbs", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public float rate_kbs
    {
      get { return _rate_kbs; }
      set { _rate_kbs = value; }
    }

    private rsctrl.stream.StreamFileDetail _file = null;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"file", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public rsctrl.stream.StreamFileDetail file
    {
      get { return _file; }
      set { _file = value; }
    }

    private rsctrl.stream.StreamVoipDetail _voip = null;
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"voip", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public rsctrl.stream.StreamVoipDetail voip
    {
      get { return _voip; }
      set { _voip = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"StreamData")]
  public partial class StreamData : global::ProtoBuf.IExtensible
  {
    public StreamData() {}
    
    private uint _stream_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"stream_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint stream_id
    {
      get { return _stream_id; }
      set { _stream_id = value; }
    }
    private rsctrl.stream.StreamState _stream_state;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"stream_state", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public rsctrl.stream.StreamState stream_state
    {
      get { return _stream_state; }
      set { _stream_state = value; }
    }
    private rsctrl.core.Timestamp _send_time;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"send_time", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public rsctrl.core.Timestamp send_time
    {
      get { return _send_time; }
      set { _send_time = value; }
    }
    private ulong _offset;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"offset", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong offset
    {
      get { return _offset; }
      set { _offset = value; }
    }
    private uint _size;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"size", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint size
    {
      get { return _size; }
      set { _size = value; }
    }
    private byte[] _stream_data;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, Name=@"stream_data", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] stream_data
    {
      get { return _stream_data; }
      set { _stream_data = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RequestStartFileStream")]
  public partial class RequestStartFileStream : global::ProtoBuf.IExtensible
  {
    public RequestStartFileStream() {}
    
    private rsctrl.core.File _file;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"file", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public rsctrl.core.File file
    {
      get { return _file; }
      set { _file = value; }
    }
    private float _rate_kbs;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"rate_kbs", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public float rate_kbs
    {
      get { return _rate_kbs; }
      set { _rate_kbs = value; }
    }

    private ulong _start_byte = default(ulong);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"start_byte", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(ulong))]
    public ulong start_byte
    {
      get { return _start_byte; }
      set { _start_byte = value; }
    }

    private ulong _end_byte = default(ulong);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"end_byte", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(ulong))]
    public ulong end_byte
    {
      get { return _end_byte; }
      set { _end_byte = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ResponseStreamDetail")]
  public partial class ResponseStreamDetail : global::ProtoBuf.IExtensible
  {
    public ResponseStreamDetail() {}
    
    private rsctrl.core.Status _status;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public rsctrl.core.Status status
    {
      get { return _status; }
      set { _status = value; }
    }
    private readonly global::System.Collections.Generic.List<rsctrl.stream.StreamDesc> _streams = new global::System.Collections.Generic.List<rsctrl.stream.StreamDesc>();
    [global::ProtoBuf.ProtoMember(2, Name=@"streams", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<rsctrl.stream.StreamDesc> streams
    {
      get { return _streams; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RequestControlStream")]
  public partial class RequestControlStream : global::ProtoBuf.IExtensible
  {
    public RequestControlStream() {}
    
    private uint _stream_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"stream_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint stream_id
    {
      get { return _stream_id; }
      set { _stream_id = value; }
    }
    private rsctrl.stream.RequestControlStream.StreamAction _action;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"action", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public rsctrl.stream.RequestControlStream.StreamAction action
    {
      get { return _action; }
      set { _action = value; }
    }

    private float _rate_kbs = default(float);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"rate_kbs", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float rate_kbs
    {
      get { return _rate_kbs; }
      set { _rate_kbs = value; }
    }

    private ulong _seek_byte = default(ulong);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"seek_byte", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(ulong))]
    public ulong seek_byte
    {
      get { return _seek_byte; }
      set { _seek_byte = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"StreamAction")]
    public enum StreamAction
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"STREAM_START", Value=1)]
      STREAM_START = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"STREAM_STOP", Value=2)]
      STREAM_STOP = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"STREAM_PAUSE", Value=3)]
      STREAM_PAUSE = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"STREAM_CHANGE_RATE", Value=4)]
      STREAM_CHANGE_RATE = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"STREAM_SEEK", Value=5)]
      STREAM_SEEK = 5
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RequestListStreams")]
  public partial class RequestListStreams : global::ProtoBuf.IExtensible
  {
    public RequestListStreams() {}
    
    private rsctrl.stream.StreamType _request_type;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"request_type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public rsctrl.stream.StreamType request_type
    {
      get { return _request_type; }
      set { _request_type = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ResponseStreamData")]
  public partial class ResponseStreamData : global::ProtoBuf.IExtensible
  {
    public ResponseStreamData() {}
    
    private rsctrl.core.Status _status;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public rsctrl.core.Status status
    {
      get { return _status; }
      set { _status = value; }
    }
    private rsctrl.stream.StreamData _data;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"data", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public rsctrl.stream.StreamData data
    {
      get { return _data; }
      set { _data = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"RequestMsgIds")]
    public enum RequestMsgIds
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"MsgId_RequestStartFileStream", Value=1)]
      MsgId_RequestStartFileStream = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MsgId_RequestControlStream", Value=2)]
      MsgId_RequestControlStream = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MsgId_RequestListStreams", Value=3)]
      MsgId_RequestListStreams = 3
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"ResponseMsgIds")]
    public enum ResponseMsgIds
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"MsgId_ResponseStreamDetail", Value=1)]
      MsgId_ResponseStreamDetail = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MsgId_ResponseStreamData", Value=101)]
      MsgId_ResponseStreamData = 101
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"StreamType")]
    public enum StreamType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"STREAM_TYPE_ALL", Value=1)]
      STREAM_TYPE_ALL = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"STREAM_TYPE_FILES", Value=2)]
      STREAM_TYPE_FILES = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"STREAM_TYPE_VOIP", Value=3)]
      STREAM_TYPE_VOIP = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"STREAM_TYPE_OTHER", Value=4)]
      STREAM_TYPE_OTHER = 4
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"StreamState")]
    public enum StreamState
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"STREAM_STATE_ERROR", Value=0)]
      STREAM_STATE_ERROR = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"STREAM_STATE_RUN", Value=1)]
      STREAM_STATE_RUN = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"STREAM_STATE_PAUSED", Value=2)]
      STREAM_STATE_PAUSED = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"STREAM_STATE_FINISHED", Value=3)]
      STREAM_STATE_FINISHED = 3
    }
  
}