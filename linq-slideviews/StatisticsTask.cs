using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public class StatisticsTask
    {
        public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
        {
            var bigrams = visits
                .GroupBy(visit => visit.UserId)
                .Select(group => group
                    .OrderBy(visit => visit.DateTime)
                    .Bigrams()
                    .Where(tuple => tuple.Item1.SlideType == slideType)).ToList();
            return bigrams.Count == 0
                ? 0
                : bigrams.SelectMany(visitsBigrams => visitsBigrams.Select(
                            bigram => (bigram.Item2.DateTime - bigram.Item1.DateTime).TotalMinutes)
                        .Where(d => d >= 1 && d <= 120))
                    .OrderBy(d => d)
                    .Median();
        }
    }
}