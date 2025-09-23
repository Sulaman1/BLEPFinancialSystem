﻿using Microsoft.AspNetCore.Authorization;

namespace BLEPFinancialSystem.SecurityandCompliance.RoleBasedAccess
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }
        public PermissionRequirement(string permission) => Permission = permission;
    }
}
