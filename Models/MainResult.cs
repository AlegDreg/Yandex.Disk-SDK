namespace YaDiskSdk.Models
{
    public class MainResult<T>
    {
        public System.Exception exception { get; set; }
        public T result { get; set; }
    }

    public class Res
    {
        public string href { get; set; }
        public string method { get; set; }
        public bool templated { get; set; }
    }
}
