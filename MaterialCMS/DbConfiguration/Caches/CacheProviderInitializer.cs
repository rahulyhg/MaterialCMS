﻿using FluentNHibernate.Cfg.Db;
using NHibernate.Cache;

namespace MaterialCMS.DbConfiguration.Caches
{
    public abstract class CacheProviderInitializer
    {
        public abstract void Initialize(CacheSettingsBuilder builder);
    }

    public abstract class CacheProviderInitializer<T> : CacheProviderInitializer where T : ICacheProvider
    {
    }
}