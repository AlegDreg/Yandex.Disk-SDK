using System.Threading.Tasks;

namespace YaDiskSdk
{
    internal interface ISdkOperaions
    {
        Task<bool> DownloadFile(string filePath, string saveAsPath);
        Task<bool> UploadUrlFile(string fileUrl, string newFileUrl);
        Task<bool> UploadFile(string localPath, string diskPath, bool overwrite = true);
        Task<bool> CreateFolder(string name);
        Task<bool> DeleteItem(string name, bool permanently = true);
        string Token { get; set; }
    }
}
