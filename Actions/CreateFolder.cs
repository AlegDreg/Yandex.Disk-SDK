using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YaDiskSdk.Models;
using Json = Newtonsoft.Json.JsonConvert;

namespace YaDiskSdk.Actions
{
    public class CreateFolder : ActionBase, IAction<CreateDataModel>
    {
        public async Task<MainResult> Create(string url, string token, string foldername)
        {
            return (MainResult)await base.Action<MainResult>(
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