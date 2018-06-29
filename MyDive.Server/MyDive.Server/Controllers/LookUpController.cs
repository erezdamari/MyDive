using MyDive.Server.Logic;
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
    [RoutePrefix("lookup")]
    public class LookUpController : MainController
    {
        private LookupLogic m_Logic = new LookupLogic();

        [HttpGet]
        [Route("bottom")]
        public IHttpActionResult GetBottomTypes()
        {
            LogControllerEntring("bottom");
            IHttpActionResult result = Ok();

            try
            {
                result = Ok(m_Logic.GetBottomTypes());
            }
            catch (Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }

        [HttpGet]
        [Route("salinity")]
        public IHttpActionResult GetSalinityTypes()
        {
            LogControllerEntring("salinity");
            IHttpActionResult result = Ok();

            try
            {
                result = Ok(m_Logic.GetSalinityTypes());
            }
            catch (Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }

        [HttpGet]
        [Route("water")]
        public IHttpActionResult GetWaterTypes()
        {
            LogControllerEntring("water");
            IHttpActionResult result = Ok();

            try
            {
                result = Ok(m_Logic.GetWaterTypes());
            }
            catch (Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }

        [HttpGet]
        [Route("association")]
        public IHttpActionResult GetAssociations()
        {
            LogControllerEntring("association");
            IHttpActionResult result = Ok();

            try
            {
                result = Ok(m_Logic.GetAssociation());
            }
            catch (Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }

        [HttpGet]
        [Route("license")]
        public IHttpActionResult GetLicenses()
        {
            LogControllerEntring("license");
            IHttpActionResult result = Ok();

            try
            {
                result = Ok(m_Logic.GetLicenseTypes());
            }
            catch (Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }

        [HttpGet]
        [Route("dive")]
        public IHttpActionResult GetDiveTypes()
        {
            LogControllerEntring("dive");
            IHttpActionResult result = Ok();

            try
            {
                result = Ok(m_Logic.GetDiveTypes());
            }
            catch (Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }

        [HttpGet]
        [Route("registration")]
        public IHttpActionResult GetRegistrationModel()
        {
            LogControllerEntring("registration");
            IHttpActionResult result = Ok();
            try
            {
                RegistrationModel model = new RegistrationModel
                {
                    AssociationsTypes = m_Logic.GetAssociation(),
                    LicenseTypes = m_Logic.GetLicenseTypes()
                };

                result = Ok(model);
            }
            catch (Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }

        [HttpGet]
        [Route("lognewdive")]
        public IHttpActionResult GetLogNewDiveModel()
        {
            LogControllerEntring("lognewdive");
            IHttpActionResult result = Ok();
            try
            {
                LogNewDiveModel model = new LogNewDiveModel
                {
                    BottomTypes = m_Logic.GetBottomTypes(),
                    DiveTypes = m_Logic.GetDiveTypes(),
                    SalinityTypes = m_Logic.GetSalinityTypes(),
                    WaterTypes = m_Logic.GetWaterTypes()
                };

                result = Ok(model);
            }
            catch(Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }
    }
}
