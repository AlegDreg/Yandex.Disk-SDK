using System.IO;
using System.Net;
using System.Threading.Tasks;
using YaDiskSdk.Models;

namespace YaDiskSdk.Actions
{
    internal class DownloadFile : ActionBase, IAction<DownloadDataModel>
    {
        /// <summary>
        /// Download file from url
        /// </summary>
        /// <param name="downloadResult"></param>
        /// <param name="saveAsPath"></param>
        /// <returns></returns>
        public async Task<bool> GetFile(Res downloadResult, string saveAsPath)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    await webClient.DownloadFileTaskAsync(downloadResult.href, saveAsPath);

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
        public MainResult<Res> GetDownloadUrl(string filePath, string url, string token)
        {
            return base.GetAction<Res>(token,
                url + $"/download?path={filePath.ReplaceCharsToUri()}");
        }

        public async Task<bool> DoAction(DownloadDataModel t, BaseInfoModel baseInfo)
        {
            var result = GetDownloadUrl(t.filePath, baseInfo.url, baseInfo.token);
            if (result.exception != null)
                return false;
            else
            {
                return await GetFile(result.result, t.saveAsPath);
            }
        }
    }
}