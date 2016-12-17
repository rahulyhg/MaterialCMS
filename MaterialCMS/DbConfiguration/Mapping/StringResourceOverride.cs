﻿using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using MaterialCMS.Entities.Resources;

namespace MaterialCMS.DbConfiguration.Mapping
{
    public class StringResourceOverride : IAutoMappingOverride<StringResource>
    {
        public void Override(AutoMapping<StringResource> mapping)
        {
            mapping.Map(resource => resource.Key).MakeVarCharMax();
            mapping.Map(resource => resource.Value).MakeVarCharMax();
        }
    }
}