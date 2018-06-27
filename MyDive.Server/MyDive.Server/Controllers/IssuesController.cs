using MyDive.Server.Log;
using MyDive.Server.Logic;
using MyDive.Server.Models;
using System;
using System.Web.Http;

namespace MyDive.Server.Controllers
{
    [RoutePrefix("issue")]
    public class IssuesController : ApiController
    {
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddNewIssue([FromBody] IssueModel i_Issue)
        {
            Logger.Instance.Notify("Start issues controller", eLogType.Debug);
            IHttpActionResult result = Ok();
            if (IssueLogic.CheckIsValidInfo(i_Issue))
            {
                MyDiveEntities MyDiveDB = null;
                try
                {
                    MyDiveDB = new MyDiveEntities();
                    var issueId = MyDiveDB.stp_CreateIssue(i_Issue.Subject, i_Issue.Email, i_Issue.Description);
                    Logger.Instance.Notify(
                        string.Format("add issue: '{0}'", i_Issue.Subject),
                        eLogType.Info);
                    result = Ok(issueId);
                }
                catch (Exception ex)
                {
                    Logger.Instance.Notify(ex.StackTrace, eLogType.Error);
                    result = InternalServerError();
                }
                finally
                {
                    if(MyDiveDB != null)
                    {
                        MyDiveDB.Dispose();
                        Logger.Instance.Notify("Connection was closed", eLogType.Info);
                    }
                }
            }
            else
            {
                Logger.Instance.Notify("issue info in insufficient", eLogType.Error);
                result = BadRequest();
            }

            return result;
        }
    }
}
