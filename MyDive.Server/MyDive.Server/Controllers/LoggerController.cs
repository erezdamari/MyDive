using MyDive.Server.Log;
using MyDive.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static MyDive.Server.Enums;

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
        public IHttpActionResult GetDeugLog()
        {
            return Get((int)eLogType.Debug);
        }

        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAllLogs()
        {
            return Get(0);
        }

        [HttpGet]
        [Route("clearlog")]
        public IHttpActionResult ClearLog()
        {
            IHttpActionResult result = Ok();
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    MyDiveDB.stp_ClearLog();
                }
            }
            catch (Exception ex)
            {
                result = LogException(ex, null);
            }

            return result;
        }

        private IHttpActionResult Get(int i_Type)
        {
            LogControllerEntring(i_Type.ToString());
            IHttpActionResult result = Ok();

            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    var Log = MyDiveDB.stp_GetLog(i_Type);
                    Logger.Instance.Notify(string.Format("Retrive {0} log", i_Type.ToString()), eLogType.Info, null);
                    result = Ok(convertToList(Log));
                }
            }
            catch (Exception ex)
            {
                result = LogException(ex, null);
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
                    Date = res.LogDate,
                    Data = res.Data
                });
            }

            return result.Count > 0 ? result : null;
        }
    }
}
