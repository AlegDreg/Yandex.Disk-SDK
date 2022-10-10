using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YaDiskSdk.Models;
using Json = Newtonsoft.Json.JsonConvert;

namespace YaDiskSdk.Actions
{
    public class RemoveItem : ActionBase, IAction<RemoveDataModel>
    {
        public async Task<bool> DoAction(RemoveDataModel t, BaseInfoModel baseInfo)
        {
            if ((await Remove(baseInfo.url, baseInfo.token, t.path, t.permanently)).exception == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<MainResult> Remove(string url, string token, string name, bool permanently)
        {
            return (MainResult)await base.Action<MainResult>(
                token,
                url + $"?path={name.ReplaceCharsToUri()}&permanently={permanently}",
                ReqTypes.DELETE);
        }
    }
}