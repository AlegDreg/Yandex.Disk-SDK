namespace YaDiskSdk.Models
{
    public class MainResult : IResult<Res>
    {
        public System.Exception exception { get; set; }
        public Res result { get; set; }
    }

    public class Res
    {
        public string href { get; set; }
        public string method { get; set; }
        public bool templated { get; set; }
    }
}