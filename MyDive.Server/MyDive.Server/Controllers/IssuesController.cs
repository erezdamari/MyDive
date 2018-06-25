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
            IHttpActionResult result = Ok();
            if (IssueLogic.CheckIsValidInfo(i_Issue))
            {
                MyDiveEntities MyDiveDB = null;
                //log info - method started
                try
                {
                    MyDiveDB = new MyDiveEntities();

                    var issueId = MyDiveDB.stp_CreateIssue(i_Issue.Subject, i_Issue.Email, i_Issue.Description);
                    //log debug - details
                    result = Ok(issueId);
                }
                catch (Exception ex)
                {
                    //log error 
                    result = InternalServerError();
                }
                finally
                {
                    if(MyDiveDB != null)
                    {
                        MyDiveDB.Dispose();
                    }
                }
            }
            else
            {
                //log error
                result = BadRequest();
            }

            return result;
        }
    }
}
