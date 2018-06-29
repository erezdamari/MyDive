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
            return Get((int)eLogType.Error, "error");
        }

        [HttpGet]
        [Route("info")]
        public IHttpActionResult GetInfoLog()
        {
            return Get((int)eLogType.Info, "info");
        }

        [HttpGet]
        [Route("debug")]
        public IHttpActionResult GetDeugeLog()
        {
            return Get((int)eLogType.Debug, "debug");
        }

        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAllLogs()
        {
            return Get(0, "all");
        }

        [HttpGet]
        [Route("delete/{i_LogId")]
        public IHttpActionResult DeleteLog(int i_LogId)
        {
            LogControllerEntring("delete");
            IHttpActionResult result = Ok();
            try
            {
                using(MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    int? logId = -1;
                    logId = MyDiveDB.stp_DeleteLogById(i_LogId);
                    result = Ok(logId != -1 ? logId : null);
                }
            }
            catch(Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }

        private IHttpActionResult Get(int i_Type, string i_FunctionName)
        {
            LogControllerEntring(i_FunctionName);
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
                    Type = res.Type,
                    Msg = res.Msg,
                    Date = res.LogDate
                });
            }

            return result.Count > 0 ? result : null;
        }
    }
}
