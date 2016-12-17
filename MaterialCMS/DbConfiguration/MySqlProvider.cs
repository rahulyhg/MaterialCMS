﻿using System.ComponentModel;
using FluentNHibernate.Cfg.Db;
using MaterialCMS.Settings;

namespace MaterialCMS.DbConfiguration
{
    [Description("MySQL")]
    public class MySqlProvider : IDatabaseProvider
    {
        private readonly DatabaseSettings _databaseSettings;

        public MySqlProvider(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public IPersistenceConfigurer GetPersistenceConfigurer()
        {
            return
                MySQLConfiguration.Standard.ConnectionString(builder => builder.Is(_databaseSettings.ConnectionString));
        }

        public void AddProviderSpecificConfiguration(NHibernate.Cfg.Configuration config)
        {
        }

        public string Type
        {
            get { return GetType().FullName; }
        }
    }
}