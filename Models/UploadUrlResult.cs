using System;

namespace YaDiskSdk.Models
{
    public class UploadUrlResult : IResult<UploadRes>
    {
        public Exception exception { get; set; }
        public UploadRes result { get; set; }
    }

    public class UploadRes
    {
        public string status { get; set; }
    }
}