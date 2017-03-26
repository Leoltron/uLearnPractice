using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace linq_slideviews
{
    public class ParsingTask
    {
        private static bool TryParseSlydeType(string s, out SlideType slideType)
        {
            switch (s.ToLower())
            {
                case "theory":
                    slideType = SlideType.Theory;
                    return true;
                case "exercise":
                    slideType = SlideType.Exercise;
                    return true;
                case "quiz":
                    slideType = SlideType.Quiz;
                    return true;
                default:
                    slideType = SlideType.Theory;
                    return false;
            }
        }

        public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
        {
            return lines.Skip(1).Dis.Select()
            var returnDict = new Dictionary<int, SlideRecord>();
            var firstPassed = false;
            foreach (var line in lines)
            {
                if (firstPassed)
                {
                    var data = line.Split(';');
                    if (data.Length != 3)
                        continue;

                    int id;
                    if (!int.TryParse(data[0], out id))
                        continue;

                    SlideType type;
                    if (!TryParseSlydeType(data[1], out type))
                        continue;

                    returnDict.Add(id, new SlideRecord(id, type, data[2]));
                }
                else
                    firstPassed = true;
            }
            return returnDict;
        }

        public static IEnumerable<VisitRecord> ParseVisitRecords(
            IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
        {
            return lines.Skip(1).Select(line => ParseVisitRecord(slides, line));
        }

        private static VisitRecord ParseVisitRecord(IDictionary<int, SlideRecord> slides, string line)
        {
            try
            {
                var data = line.Split(';');
                var slideId = int.Parse(data[1]);
                return new VisitRecord(
                    int.Parse(data[0]),
                    slideId,
                    DateTime.ParseExact(
                        data[2] + ' ' + data[3],
                        "yyyy-MM-dd HH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None), slides[slideId].SlideType);
            }
            catch (Exception)
            {
                throw new FormatException($"Wrong line [{line}]");
            }
        }
    }
}