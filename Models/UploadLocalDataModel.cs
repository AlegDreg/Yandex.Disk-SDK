namespace YaDiskSdk.Models
{
    public class UploadLocalDataModel
    {
        public string filepathOndisk { get; set; }
        public string fileLocalPath { get; set; }
        public bool overwrite { get; set; } = true;
    }
}