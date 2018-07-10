using MyDive.Server.Log;
using Newtonsoft.Json;
using System;
using System.Web.Http;
using static MyDive.Server.Enums;

namespace MyDive.Server.Controllers
{
    public abstract class MainController : ApiController
    {
        public IHttpActionResult LogException(Exception ex, string i_Data)
        {
            Logger.Instance.Notify(ex.StackTrace, eLogType.Error, i_Data);
            return InternalServerError();
        }

        public void LogControllerEntring(string i_FunctionName)
        {
            
            Logger.Instance.Notify(
                string.Format("start {0}/{1} controller", this.GetType().Name, i_FunctionName),
                eLogType.Debug,
                null);
        }

        public void LogError(string i_Error, string i_Data)
        {
            Logger.Instance.Notify(i_Error, eLogType.Error, i_Data);
        }

        public IHttpActionResult LogInternalError(eErrors i_Error, string i_Data)
        {
            LogError(i_Error.ToString(), i_Data);
            return BadRequest(((int)i_Error).ToString());
        }

        public void LogData(string i_Msg, object i_Model)
        {
            LogData(i_Msg, i_Model);
        }
    }
}
