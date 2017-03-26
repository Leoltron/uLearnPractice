using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public class StatisticsTask
    {
        public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
        {
            return visits
                .GroupBy(visit => visit.UserId)
                .Select(group => group
                    .Bigramms()
                    .Where(tuple => tuple.Item1.SlideType == slideType))
                .SelectMany(GetTime).OrderBy(d => d).Median();
        }

        private static IEnumerable<double> GetTime(IEnumerable<Tuple<VisitRecord, VisitRecord>> visitsBigrams)
        {
            var timeBuffer = 0d;
            return visitsBigrams.Select(bigram => GetTime(bigram, ref timeBuffer)).Where(d => d >= 0);
        }

        private static double GetTime(Tuple<VisitRecord, VisitRecord> bigram, ref double timeBuffer)
        {
            var time = (double) (bigram.Item2.DateTime - bigram.Item1.DateTime).Seconds / 60;

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