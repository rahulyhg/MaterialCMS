﻿using MaterialCMS.Batching.Entities;
using MaterialCMS.Events;

namespace MaterialCMS.Batching.Events
{
    public interface IOnBatchRunPause : IEvent<BatchRunPauseArgs>
    {
         
    }

    public class BatchRunPauseArgs
    {
        public BatchRun BatchRun { get; set; }
    }
}