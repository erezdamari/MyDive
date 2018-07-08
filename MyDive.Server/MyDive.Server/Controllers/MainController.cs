using MyDive.Server.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static MyDive.Server.Enums;

namespace MyDive.Server.Controllers
{
    public abstract class MainController : ApiController
    {
        public IHttpActionResult LogException(Exception ex)
        {
            Logger.Instance.Notify(ex.StackTrace, eLogType.Error);
            return InternalServerError();
        }

        public void LogControllerEntring(string i_FunctionName)
        {
            
            Logger.Instance.Notify(
                string.Format("start {0}/{1} controller", this.GetType().Name, i_FunctionName),
                eLogType.Debug);
        }

        public void LogError(string i_Error)
        {
            Logger.Instance.Notify(i_Error, eLogType.Error);
        }

        public IHttpActionResult LogInternalError(eErrors i_Error)
        {
            LogError(i_Error.ToString());
            return BadRequest(((int)i_Error).ToString());
        }
    }
}
