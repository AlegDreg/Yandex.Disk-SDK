using System.Threading.Tasks;

namespace YaDiskSdk
{
    public interface ISdkOperaions
    {
        /// <summary>
        /// Download file from Yadisk
        /// </summary>
        /// <param name="filePath">File path on YaDisk</param>
        /// <param name="saveAsPath">Path to save downloaded file</param>
        /// <returns>True if file success downloaded</returns>
        Task<bool> DownloadFile(string filePath, string saveAsPath);

        /// <summary>
        /// Upload file to YaDisk from url
        /// </summary>
        /// <param name="fileUrl">File url that uploading to YaDisk</param>
        /// <param name="newFileUrl">New file path on disk</param>
        /// <returns></returns>
        Task<bool> UploadUrlFile(string fileUrl, string newFileUrl);

        /// <summary>
        /// Upload local file to YaDisk
        /// </summary>
        /// <param name="localPath">Local file path</param>
        /// <param name="diskPath">YaDisk file path</param>
        /// <param name="overwrite">True if overwrite file if it exist</param>
        /// <returns></returns>
        Task<bool> UploadFile(string localPath, string diskPath, bool overwrite = true);

        /// <summary>
        /// Create folder
        /// </summary>
        /// <param name="name">New folder name</param>
        /// <returns></returns>
        Task<bool> CreateFolder(string name);

        /// <summary>
        /// Delete file or directory
        /// </summary>
        /// <param name="name">Item name</param>
        /// <param name="permanently">True if delete without moving to trash</param>
        /// <returns></returns>
        Task<bool> DeleteItem(string name, bool permanently = true);

        string Token { get; set; }
    }
}