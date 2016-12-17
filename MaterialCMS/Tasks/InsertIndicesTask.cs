﻿using MaterialCMS.Entities;
using MaterialCMS.Indexing.Management;
using MaterialCMS.Services;
using NHibernate;

namespace MaterialCMS.Tasks
{
    internal class InsertIndicesTask<T> : IndexManagementTask<T> where T : SiteEntity
    {
        public InsertIndicesTask(ISession session, IIndexService indexService)
            : base(session, indexService)
        {
        }

        protected override void ExecuteLogic(IIndexManagerBase manager, T entity)
        {
            manager.Insert(entity);
        }

        protected override LuceneOperation Operation
        {
            get { return LuceneOperation.Insert; }
        }
    }
}