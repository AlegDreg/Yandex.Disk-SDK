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
        public virtual async Task<MainResult<T>> Action<T>(string token, string url, ReqTypes reqType)
        {
            using (var wb = new WebClient())
            {
                wb.Headers.Add($"Authorization: OAuth {token}");
                try
                {
                    var response = await wb.UploadDataTaskAsync(
                        url,
                        reqType.ToString(),
                        new byte[0]);

                    string responseInString = Encoding.UTF8.GetString(response);

                    var result = Json.DeserializeObject<T>(responseInString);

                    var e = new MainResult<T>();

                    if (result == null)
                    {
                        result = default(T);
                    }

                    e.result = result;

                    return e;
                }
                catch (Exception ex)
                {
                    var e = new MainResult<T>();
                    e.exception = ex;
                    return e;
                }
            }
        }

        public virtual MainResult<T> GetAction<T>(string token, string url)
        {
            using (var wb = new WebClient())
            {
                wb.Headers.Add("Accept: application/json");
                wb.Headers.Add($"Authorization: OAuth {token}");
                wb.Headers.Add("Content-Type: application/json");

                try
                {
                    var responseInString = wb.DownloadString(url);

                    var result = Json.DeserializeObject<T>(responseInString);

                    var e = new MainResult<T>();

                    if (result == null)
                    {
                        result = default(T);
                    }

                    e.result = result;

                    return e;
                }
                catch (Exception ex)
                {
                    var e = new MainResult<T>();
                    e.exception = ex;
                    return e;
                }
            }
        }
    }

    public enum ReqTypes
    {
        POST,
        PUT,
        DELETE
    }
}