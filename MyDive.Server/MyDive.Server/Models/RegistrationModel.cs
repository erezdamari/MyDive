﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDive.Server.Models
{
    public class RegistrationModel
    {
        public List<AssociationModel> AssociationsTypes { get; set; }

        public List<LicenseTypeModel> LicenseTypes { get; set; }
    }
}