﻿using MaterialCMS.DbConfiguration;
using MaterialCMS.Entities;
using NHibernate;

namespace MaterialCMS.Events
{
    public abstract class OnDeletedArgs
    {
        public abstract SystemEntity ItemBase { get; }
        public ISession Session { get; protected set; }
    }

    public class OnDeletedArgs<T> : OnDeletedArgs where T : SystemEntity
    {
        public OnDeletedArgs(EventInfo<T> info, ISession session)
        {
            Item = info.Object;
            Session = session;
        }

        public T Item { get; private set; }

        public override SystemEntity ItemBase
        {
            get { return Item; }
        }
    }
}