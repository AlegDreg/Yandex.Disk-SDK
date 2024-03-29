# Yandex.Disk RestApi SDK
This is an unofficial project for Yandex.Disk management

At the moment it has the functions:
- Download files
- Upload files from url
- Upload file from local storage
- Delete file/directory
- Create directory

## Start
To get started, you need to get a Yandex.OAuth token

## Initialize the Sdk class
```c#
var sdk = new Sdk(token);
```

Now you can call any method in Sdk instance
```c#
bool success = await sdk.DownloadFile("images/image.png", "C:\\newimage.png");
```

## Another way
You can call all methods using the IAction<T> interface
```c#
string url = "https://cloud-api.yandex.net/v1/disk/resources";
IAction<DownloadDataModel> action = new DownloadFile();
  
bool success = await action.DoAction(
    new DownloadDataModel { filePath = "images/image.png", saveAsPath = "C:\\newimage.png" },  
    new BaseInfoModel() { token = Token, url = url });
```

### Interface ISdkOperaions
```c#
    public interface ISdkOperaions
    {
        Task<bool> DownloadFile(string filePath, string saveAsPath);
        Task<bool> UploadUrlFile(string fileUrl, string newFileUrl);
        Task<bool> UploadFile(string localPath, string diskPath, bool overwrite = true);
        Task<bool> CreateFolder(string name);
        Task<bool> DeleteItem(string name, bool instantly = true);
        string Token { get; set; }
    }
```
