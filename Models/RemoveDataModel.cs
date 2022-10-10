namespace YaDiskSdk.Models
{
    public class RemoveDataModel
    {
        public string path { get; set; }
        public bool permanently { get; set; } = true;
    }
}