using System.Threading.Tasks;
using YaDiskSdk.Actions;
using YaDiskSdk.Models;

namespace YaDiskSdk
{
    public class Sdk : ISdkOperaions
    {
        public string Token { get; set; }
        public const string url = "https://cloud-api.yandex.net/v1/disk/resources";
        private BaseInfoModel _baseInfoModel;

        public Sdk(string token)
        {
            Token = token;
            _baseInfoModel = new BaseInfoModel() { token = Token, url = url };
        }

        //https://oauth.yandex.ru/client/f6fe1ecabe9740519e8cd2638bbb38ed
        //https://yandex.ru/dev/disk/api/reference/content.html
        //https://oauth.yandex.ru/authorize?response_type=token&client_id=f6fe1ecabe9740519e8cd2638bbb38ed

        public async Task<bool> DownloadFile(string filePath, string saveAsPath)
        {
            IAction<DownloadDataModel> action = new DownloadFile();
            return await action.DoAction(new DownloadDataModel { filePath = filePath, saveAsPath = saveAsPath },
                _baseInfoModel);
        }

        public async Task<bool> UploadUrlFile(string fileUrl, string newFileUrl)
        {
            IAction<UploadDataModel> action = new UploadFileFromUrl();
            return await action.DoAction(new UploadDataModel { fileUrl = fileUrl, newFileUrl = newFileUrl },
                _baseInfoModel);
        }

        public async Task<bool> CreateFolder(string name)
        {
            IAction<CreateDataModel> action = new CreateFolder();
            return await action.DoAction(new CreateDataModel { foldername = name },
                _baseInfoModel);
        }

        public async Task<bool> DeleteItem(string name, bool permanently = true)
        {
            IAction<RemoveDataModel> action = new RemoveItem();
            return await action.DoAction(new RemoveDataModel { path = name, permanently = permanently },
                _baseInfoModel);
        }

        public async Task<bool> UploadFile(string localPath, string diskPath, bool overwrite = true)
        {
            IAction<UploadLocalDataModel> action = new UploadFile();
            return await action.DoAction(new UploadLocalDataModel
            {
                fileLocalPath = localPath,
                filepathOndisk = diskPath,
                overwrite = overwrite
            },
                _baseInfoModel);
        }
    }
}