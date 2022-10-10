# Yandex.Disk RestApi SDK
This is an unofficial project for Yandex.Disk management

At the moment it has the functions:
- Downloading files
- Uploading files by url
- Deleting file/directory
- Create directory

## Begin
To get started, you need to get a Yandex.OAuth token

## Initialize the Sdk class
```c#
var sdk = new Sdk(token);
```

## Now you can call any method in Sdk instance
```#c
bool success = await sdk.DownloadFile("images/image.png", "C:\newimage.png");
```
