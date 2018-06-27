using MyDive.Server.Log;
using MyDive.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyDive.Server.Controllers
{
    [RoutePrefix("logger")]
    public class LoggerController : MainController
    {
        [HttpGet]
        [Route("error")]
        public IHttpActionResult GetErrorLog()
        {
            return Get((int)eLogType.Error);
        }

        [HttpGet]
        [Route("info")]
        public IHttpActionResult GetInfoLog()
        {
            return Get((int)eLogType.Info);
        }

        [HttpGet]
        [Route("debug")]
        public IHttpActionResult GetDeugeLog()
        {
            return Get((int)eLogType.Debug);
        }

        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAllLogs()
        {
            return Get(0);
        }

        private IHttpActionResult Get(int i_Type)
        {
            LogControllerEntring();
            IHttpActionResult result = Ok();

            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    var Log = MyDiveDB.stp_GetLog(i_Type);
                    Logger.Instance.Notify(string.Format("Retrive {0} log", i_Type.ToString()), eLogType.Info);
                    result = Ok(convertToList(Log));
                }
            }
            catch (Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }

        private List<LogModel> convertToList(ObjectResult<stp_GetLog_Result> i_Res)
        {
            List<LogModel> result = new List<LogModel>();
            foreach (stp_GetLog_Result res in i_Res)
            {
                result.Add(new LogModel
                {
                    Msg = res.Msg,
                    Date = res.LogDate
                });
            }

            return result;
        }
    }
}
