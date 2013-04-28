using System;

using rsctrl.core;
using rsctrl.stream;

namespace RetroShareSSHClient
{
    class StreamProcessor
    {
        Bridge _b;

        public StreamProcessor()
        {
            _b = Bridge.GetBridge();
        }

        internal void Reset()
        {
            //throw new NotImplementedException();
        }

        internal void StreamDetails(ResponseStreamDetail response)
        {
            throw new NotImplementedException();
        }

        internal void StreamData(ResponseStreamData response)
        {
            throw new NotImplementedException();
        }
    }
}
