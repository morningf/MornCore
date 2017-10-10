using System;
using System.Collections.Generic;
using System.Text;

namespace MornCore
{
    class RemoteMethodHandler : IProtocolHandler
    {
        public RemoteMethodHandler(string url)
        {
            _url = url;
        }

        private static MornHttpClient _client = new MornHttpClient();
        private string _url;

        public string Process(MornRequestData data)
        {
            try
            {
                return _client.DoPost(_url, data.Context);
            }
            catch (Exception ex)
            {
                return MornErrorUtil.GenError(MornErrorType.SystemError, ex.Message);
            }
        }
    }
}
