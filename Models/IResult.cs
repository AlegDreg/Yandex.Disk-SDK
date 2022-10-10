using System;

namespace YaDiskSdk.Models
{
    public interface IResult<T>
    {
        T result { get; set; }
        Exception exception { get; set; }
    }
}