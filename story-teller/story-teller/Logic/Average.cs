using System;
using System.Collections.Generic;
using System.Linq;

namespace story_teller.Logic
{
    static class Average
    {
        public static int AverageOf<T>(this IEnumerable<T> data) where T:ICountable
        {
            var enumerable = data as T[] ?? data.ToArray();
            return enumerable.Sum(x=>x.Count()) / enumerable.Length;
        }
    }
}
