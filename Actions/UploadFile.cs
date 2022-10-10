using System.Net;
using System.Text;
using System.Threading.Tasks;
using YaDiskSdk.Models;

namespace YaDiskSdk.Actions
{
    public class UploadFile : ActionBase, IAction<UploadLocalDataModel>
    {
        public MainResult<UploadLocalFileResult> GetUrlToUpload(string token, string url, string filepath, bool overwrite = true)
        {
            return (MainResult<UploadLocalFileResult>)base.GetAction<UploadLocalFileResult>(token,
                url + $@"/upload?path={filepath.ReplaceCharsToUri()}&overwrite={overwrite}");
        }

        public async Task<bool> Upload(UploadLocalFileResult mainResult, string token, string filename)
        {
            using (var wb = new WebClient())
            {
                wb.Headers.Add($"Authorization: OAuth {token}");
                try
                {
                    var response = await wb.UploadFileTaskAsync(
                        mainResult.href,
                        "PUT",
                        filename);

                    string responseInString = Encoding.UTF8.GetString(response);

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task<bool> DoAction(UploadLocalDataModel t, BaseInfoModel baseInfo)
        {
            var r = GetUrlToUpload(baseInfo.token, baseInfo.url, t.filepathOndisk, t.overwrite);

            if (r.exception == null)
            {
                return await Upload(r.result, baseInfo.token, t.fileLocalPath);
            }
            else
            {
                return false;
            }
        }
    }
}
