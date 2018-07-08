using MyDive.Server.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static MyDive.Server.Enums;
using Newtonsoft;

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
    }
}
