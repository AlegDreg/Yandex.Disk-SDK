using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YaDiskSdk.Models;
using Json = Newtonsoft.Json.JsonConvert;

namespace YaDiskSdk.Actions
{
    internal class UploadFileFromUrl : ActionBase, IAction<UploadDataModel>
    {
        public async Task<MainResult> Uploadfile(string url, string fileUrl, string saveFileAs, string token)
        {
            return (MainResult)await base.Action<MainResult>(
                token,
                url + $"/upload?url={fileUrl.ReplaceCharsToUri()}&path={saveFileAs.ReplaceCharsToUri()}",
                ReqTypes.POST);
        }

        public async Task<bool> AwaitUploading(MainResult downloadResult, string token)
        {
            UploadUrlResult uploadUrlResult = CheckStatus(token, downloadResult);

            if (uploadUrlResult.result.status == "error")
                return false;

            while (uploadUrlResult.result.status == "in-progress")
            {
                await Task.Delay(1000);

                CheckStatus(token, downloadResult);

                if (uploadUrlResult.result.status == "error")
                    return false;
            }

            if (uploadUrlResult.result.status == "success")
                return true;
            else
            {
                return false;
            }
        }

        private UploadUrlResult CheckStatus(string token, MainResult downloadResult)
        {
            using (WebClient wb = new WebClient())
            {
                wb.Encoding = Encoding.UTF8;

                wb.Headers.Add($"Authorization: OAuth {token}");

                try
                {
                    var r = wb.DownloadString(downloadResult.result.href);

                    return Json.DeserializeObject<UploadUrlResult>(r);
                }
                catch (Exception ex)
                {
                    return new UploadUrlResult { result = new UploadRes { status = "error" }, exception = ex };
                }
            }
        }

        public async Task<bool> DoAction(UploadDataModel model, BaseInfoModel baseInfo)
        {
            var resUpload = await Uploadfile(baseInfo.url, model.fileUrl, model.newFileUrl, baseInfo.token);

            if (resUpload.exception != null)
                return false;
            else
            {
                return await AwaitUploading(resUpload, baseInfo.token);
            }
        }
    }
}