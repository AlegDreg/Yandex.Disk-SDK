using System.Threading.Tasks;
using YaDiskSdk.Models;

namespace YaDiskSdk.Actions
{
    internal class UploadFileFromUrl : ActionBase, IAction<UploadDataModel>
    {
        public async Task<MainResult<Res>> Uploadfile(string url, string fileUrl, string saveFileAs, string token)
        {
            return await base.Action<Res>(
                token,
                url + $"/upload?url={fileUrl.ReplaceCharsToUri()}&path={saveFileAs.ReplaceCharsToUri()}",
                ReqTypes.POST);
        }

        public async Task<bool> AwaitUploading(MainResult<Res> downloadResult, string token)
        {
            MainResult<UploadResult> uploadUrlResult = CheckStatus(token, downloadResult);

            if (uploadUrlResult.result.status == "error")
                return false;

            while (uploadUrlResult.result.status == "in-progress")
            {
                await Task.Delay(1000);

                uploadUrlResult = CheckStatus(token, downloadResult);

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

        private MainResult<UploadResult> CheckStatus(string token, MainResult<Res> downloadResult)
        {
            var e = (MainResult<UploadResult>)base.GetAction<UploadResult>(token, downloadResult.result.href);

            if (e.exception != null)
                e.result.status = "error";

            return e;
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