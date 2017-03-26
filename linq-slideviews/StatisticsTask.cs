using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public class StatisticsTask
    {
        public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
        {
            return visits.Count == 0
                ? 0
                : visits
                    .OrderBy(visit => visit.DateTime)
                    .GroupBy(visit => visit.UserId)
                    .Select(group => group
                        .Bigramms()
                        .Where(tuple => tuple.Item1.SlideType == slideType))
                    .SelectMany(GetVisitsTime)
                    .OrderBy(d => d)
                    .Median();
        }

        private static IEnumerable<double> GetVisitsTime(IEnumerable<Tuple<VisitRecord, VisitRecord>> visitsBigrams)
        {
            var timeBuffer = 0d;
            return visitsBigrams.Select(bigram => GetVisitTime(bigram, ref timeBuffer)).Where(d => d >= 1 && d <= 120);
        }

        private static double GetVisitTime(Tuple<VisitRecord, VisitRecord> bigram, ref double timeBuffer)
        {
            var time = bigram.Item2.DateTime.Subtract(bigram.Item1.DateTime).TotalMinutes;

            if (bigram.Item1.SlideId == bigram.Item2.SlideId)
            {
                timeBuffer = time;
                return -1;
            }

            time += timeBuffer;
            timeBuffer = 0;
            return time;
        }
    }
}