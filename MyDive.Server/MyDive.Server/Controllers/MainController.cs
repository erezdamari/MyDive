using MyDive.Server.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyDive.Server.Controllers
{
    public abstract class MainController : ApiController
    {
        public IHttpActionResult LogException(Exception ex)
        {
            Logger.Instance.Notify(ex.StackTrace, eLogType.Error);
            return InternalServerError();
        }

        public void LogControllerEntring()
        {
            
            Logger.Instance.Notify(
                string.Format("start {0} controller", this.GetType().Name),
                eLogType.Debug);
        }
    }
}
