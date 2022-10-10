<<<<<<< HEAD
﻿namespace YaDiskSdk.Models
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
=======
﻿namespace YaDiskSdk.Models
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
>>>>>>> f726f92d39c1a01454602106d5e7026661d8f9a9
}