using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YaDiskSdk.Models;
using Json = Newtonsoft.Json.JsonConvert;

namespace YaDiskSdk.Actions
{
    internal class DownloadFile : IAction<DownloadDataModel>
    {
        /// <summary>
        /// Download file from url
        /// </summary>
        /// <param name="downloadResult"></param>
        /// <param name="saveAsPath"></param>
        /// <returns></returns>
        public async Task<bool> GetFile(MainResult downloadResult, string saveAsPath)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    await webClient.DownloadFileTaskAsync(downloadResult.result.href, saveAsPath);

                    FileInfo file = new FileInfo(saveAsPath);
                    return file.Exists;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Get url to download file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<MainResult> GetDownloadUrl(string filePath, string url, string token)
        {
            using (WebClient web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;
                web.Headers.Add("Accept: application/json");
                web.Headers.Add($"Authorization: OAuth {token}");
                web.Headers.Add("Content-Type: application/json");

                try
                {
                    var r = await web.DownloadStringTaskAsync(url + $"/download?path={filePath.ReplaceCharsToUri()}");

                    return Json.DeserializeObject<MainResult>(r);
                }
                catch (Exception ex)
                {
                    return new MainResult { exception = ex };
                }
            }
        }

        public async Task<bool> DoAction(DownloadDataModel t, BaseInfoModel baseInfo)
        {
            var result = await GetDownloadUrl(t.filePath, baseInfo.url, baseInfo.token);
            if (result.exception == null)
                return false;
            else
            {
                return await GetFile(result, t.saveAsPath);
            }
        }
    }
}
