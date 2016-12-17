﻿using MaterialCMS.Helpers;

namespace MaterialCMS.HealthChecks
{
    public abstract class HealthCheck : IHealthCheck
    {
        public virtual string DisplayName { get { return GetType().Name.BreakUpString(); } }
        public string TypeName { get { return GetType().FullName; } }
        public abstract HealthCheckResult PerformCheck();
    }
}