using MyDive.Server.Log;
using MyDive.Server.Logic;
using MyDive.Server.Models;
using System;
using System.Web.Http;
using static MyDive.Server.Enums;

namespace MyDive.Server.Controllers
{
    [RoutePrefix("issue")]
    public class IssuesController : MainController
    {
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddNewIssue([FromBody] IssueModel i_Issue)
        {
            LogControllerEntring("add");
            IHttpActionResult result = Ok();
            if (IssueLogic.CheckIsValidInfo(i_Issue))
            {
                try
                {
                    using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                    {
                        int? issueId = -1;
                        issueId = MyDiveDB.stp_CreateIssue(i_Issue.Subject, i_Issue.Email, i_Issue.Description);
                        Logger.Instance.Notify(
                            string.Format("add issue: '{0}'", i_Issue.Subject),
                            eLogType.Info);
                        result = Ok(issueId != -1 ? issueId : null);
                    }
                }
                catch (Exception ex)
                {
                    result = LogException(ex);
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
