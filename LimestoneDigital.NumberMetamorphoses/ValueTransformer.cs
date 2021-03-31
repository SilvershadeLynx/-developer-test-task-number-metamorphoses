using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LimestoneDigital.NumberMetamorphoses.Contracts;

namespace LimestoneDigital.NumberMetamorphoses
{
    public class ValueTransformer : IValueTransformer
    {
        public string Transform(string value)
        {
            int maxValueLength = 7;

            if (value == null)
                throw new ArgumentNullException();

            if (value.Length > maxValueLength || Regex.IsMatch(value, "[089]"))
                throw new ArgumentOutOfRangeException();

            if (!Regex.IsMatch(value, @"^\d+$"))
                throw new ArgumentException();
                
            var set = new SortedSet<int>();
            foreach (var s in value)
                set.Add((int)char.GetNumericValue(s));

            return GetIntervals(set);
        }

        static string GetIntervals(SortedSet<int> set)
        {
            int intervalStart, intervalEnd;
            string intervalsStr = default;

            intervalStart = set.Max;
            intervalEnd = set.Max;

            while (set.Count > 0)
            {
                intervalEnd = set.Max;
                set.Remove(intervalEnd);

                if (set.Max == intervalEnd - 1)
                    continue;
                else
                    intervalsStr += GetIntervals(set) + ", ";
            }

            if (intervalEnd == intervalStart)
                intervalsStr += $"{intervalEnd}";
            else
                intervalsStr += $"{intervalEnd}-{intervalStart}";

            return intervalsStr;
        }
    }
}
