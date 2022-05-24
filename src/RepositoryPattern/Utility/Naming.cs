﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Utility
{
    internal static class Naming
    {
        internal static string Settings<T, TKey>()
            where TKey : notnull
            => typeof(IRepositoryPattern<T, TKey>).FullName!;
    }
}
