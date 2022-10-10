using System.Threading.Tasks;
using YaDiskSdk.Models;

namespace YaDiskSdk.Actions
{
    internal interface IAction<T>
    {
        Task<bool> DoAction(T t, BaseInfoModel baseInfo);
    }
}
