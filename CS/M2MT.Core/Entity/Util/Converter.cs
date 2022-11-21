using System;
using System.Collections.Generic;
using System.Text;

namespace M2MT.Shared.Entity.Util
{
    public partial class Converter
    {
        public static List<T> ConvertList<T, I>(IEnumerable<I> input) where I : Convertable<I, T>
        {
            var convertedRules = new List<T>();
            foreach (I rule in input)
            {
                convertedRules.Add(rule.Convert());
            }
            return convertedRules;
        }
    }
}
