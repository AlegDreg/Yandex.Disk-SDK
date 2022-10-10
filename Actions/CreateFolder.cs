using System.Threading.Tasks;
using YaDiskSdk.Models;

namespace YaDiskSdk.Actions
{
    public class CreateFolder : ActionBase, IAction<CreateDataModel>
    {
        public async Task<MainResult<Res>> Create(string url, string token, string foldername)
        {
            return (MainResult<Res>)await base.Action<Res>(
                token,
                url + $"?path={foldername.ReplaceCharsToUri()}",
                ReqTypes.PUT
                );
        }

        public async Task<bool> DoAction(CreateDataModel t, BaseInfoModel baseInfo)
        {
            if ((await Create(baseInfo.url, baseInfo.token, t.foldername)).exception == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}