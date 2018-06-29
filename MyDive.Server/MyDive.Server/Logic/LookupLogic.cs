using MyDive.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace MyDive.Server.Logic
{
    public class LookupLogic
    {
        public List<BottomTypeModel> GetBottomTypes()
        {
            List<BottomTypeModel> bottomTypes = null;
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetBottomType_Result> serverResult = MyDiveDB.stp_GetBottomType();
                    bottomTypes = new List<BottomTypeModel>();

                    foreach (stp_GetBottomType_Result res in serverResult)
                    {
                        bottomTypes.Add(new BottomTypeModel
                        {
                            BottomTypeID = res.BottomTypeID,
                            BottomTypeName = res.BottomType
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                bottomTypes = null;
            }

            return bottomTypes;
        }

        public List<SalinityTypeModel> GetSalinityTypes()
        {
            List<SalinityTypeModel> salinityTypes = null;
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetSalinityTypes_Result> serverResult = MyDiveDB.stp_GetSalinityTypes();
                    salinityTypes = new List<SalinityTypeModel>();

                    foreach (stp_GetSalinityTypes_Result res in serverResult)
                    {
                        salinityTypes.Add(new SalinityTypeModel
                        {
                            SalinityID = res.SalinityID,
                            Salinity = res.Salinity
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                salinityTypes = null;
            }

            return salinityTypes;
        }

        public List<WaterTypeModel> GetWaterTypes()
        {
            List<WaterTypeModel> waterTypes = null;
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetWaterTypes_Result> serverResult = MyDiveDB.stp_GetWaterTypes();
                    waterTypes = new List<WaterTypeModel>();

                    foreach (stp_GetWaterTypes_Result res in serverResult)
                    {
                        waterTypes.Add(new WaterTypeModel
                        {
                            WaterTypeID = res.WaterTypeID,
                            WaterTypeName = res.WaterType
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                waterTypes = null;
            }

            return waterTypes;
        }

        public List<AssociationModel> GetAssociation()
        {
            List<AssociationModel> assosiations = null;
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetAssociations_Result> serverResult = MyDiveDB.stp_GetAssociations();
                    assosiations = new List<AssociationModel>();

                    foreach (stp_GetAssociations_Result res in serverResult)
                    {
                        assosiations.Add(new AssociationModel
                        {
                            AssociationID = res.AssociationID,
                            AssociationName = res.AssociationName
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                assosiations = null;
            }

            return assosiations;
        }

        public List<LicenseTypeModel> GetLicenseTypes()
        {
            List<LicenseTypeModel> licenseTypes = null;
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetLicenseTypes_Result> serverResult = MyDiveDB.stp_GetLicenseTypes();
                    licenseTypes = new List<LicenseTypeModel>();

                    foreach (stp_GetLicenseTypes_Result res in serverResult)
                    {
                        licenseTypes.Add(new LicenseTypeModel
                        {
                            Id = res.LicenseTypeID,
                            Type = res.LicenseType
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                licenseTypes = null;
            }

            return licenseTypes;
        }

        public List<DiveTypeModel> GetDiveTypes()
        {
            List<DiveTypeModel> DiveTypes = null;
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetDiveTypes_Result> serverResult = MyDiveDB.stp_GetDiveTypes();
                    DiveTypes = new List<DiveTypeModel>();

                    foreach (stp_GetDiveTypes_Result res in serverResult)
                    {
                        DiveTypes.Add(new DiveTypeModel
                        {
                            DiveTypeID = res.DiveTypeID,
                            Type = res.Type
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                DiveTypes = null;
            }

            return DiveTypes;
        }
    }
}