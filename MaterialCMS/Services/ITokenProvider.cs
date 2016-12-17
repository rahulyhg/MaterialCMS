﻿using System;
using System.Collections.Generic;

namespace MaterialCMS.Services
{
    public interface ITokenProvider<T>
    {
        IDictionary<string, Func<T, string>> Tokens { get; }
    }
    public interface ITokenProvider
    {
        IDictionary<string, Func<string>> Tokens { get; }
    }
}