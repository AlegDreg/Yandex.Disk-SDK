using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YaDiskSdk.Models;
using Json = Newtonsoft.Json.JsonConvert;

namespace YaDiskSdk.Actions
{
    public abstract class ActionBase
    {
        public virtual async Task<IResult<T>> Action<T>(string token, string url, ReqTypes reqType)
        {
            using (var wb = new WebClient())
            {
                wb.Headers.Add($"Authorization: OAuth {token}");
                try
                {
                    var response = await wb.UploadDataTaskAsync(
                        url,
                        reqType.ToString(url),
                        new byte[0]);

                    string responseInString = Encoding.UTF8.GetString(response);

                    var result = Json.DeserializeObject<T>(responseInString);

                    var e = default(IResult<T>);

                    if (result == null)
                    {
                        result = default(T);
                    }

                    e.result = result;

                    return e;
                }
                catch (Exception ex)
                {
                    var e = default(IResult<T>);
                    e.exception = ex;
                    return e;
                }
            }
        }
    }

    public enum ReqTypes
    {
        POST,
        GET,
        PUT,
        DELETE
    }
}