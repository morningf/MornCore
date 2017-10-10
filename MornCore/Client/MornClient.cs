using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MornCore
{
    public class MornClient
    {
        public string ServerUrl { get; set; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }

        private MornHttpClient _client = new MornHttpClient();

        public T Execute<T>(MornRequest<T> request) where T : MornResponse
        {
            try
            {
                request.Validate();
            }
            catch (MornException ex)
            {
                return CreateErrorResponse<T>(ex.ErrorType, ex.Message);
            }

            // 添加协议级请求参数
            Dictionary<string, string> txtParams = new Dictionary<string, string>();
            txtParams.Add(MornConstants.METHOD, request.GetApiName());
            txtParams.Add(MornConstants.TIMESTAMP, DateTime.Now.ToString(MornConstants.DATE_TIME_FORMAT));
            txtParams.Add(MornConstants.DATA, request.ToJson());
            if (!string.IsNullOrEmpty(AppKey))
            {
                txtParams.Add(MornConstants.APP_KEY, AppKey);
                // 添加签名参数
                if (!string.IsNullOrEmpty(AppSecret))
                    txtParams.Add(MornConstants.SIGN, Util.SignRequestByMd5Method(txtParams, AppSecret));
            }
 
            try
            {
                var data = JsonConvert.SerializeObject(txtParams);
                var body = _client.DoPost(ServerUrl, data);
                return JsonConvert.DeserializeObject<T>(body);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse<T>(MornErrorType.SystemError, ex.Message);
            }
        }

        public async Task<T> ExecuteAsync<T>(MornRequest<T> request) where T : MornResponse => await Task.Factory.StartNew(() => { return Execute(request); });

        private T CreateErrorResponse<T>(MornErrorType type, string addition) where T : MornResponse
        {
            var rsp = Activator.CreateInstance<T>();
            rsp.ErrorCode = MornErrorUtil.GetErrorCode(type);
            if (string.IsNullOrEmpty(addition))
            {
                rsp.ErrorMessage = MornErrorUtil.GetDescription(type);
            }
            else
            {
                rsp.ErrorMessage = string.Format("{0} : {1}", MornErrorUtil.GetDescription(type), addition);
            }
            return rsp;
        }
    }
}
