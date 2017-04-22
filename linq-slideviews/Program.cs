using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZedGraph;

namespace linq_slideviews
{
    public class Program
    {
        private readonly Form form;
        private readonly ZedGraphControl chart;
        private readonly PointPairList points;

        public Program()
        {
            form = new Form
            {
                WindowState = FormWindowState.Maximized,
                Text = "Press any key to pause / resume"
            };
            chart = new ZedGraphControl()
            {
                Dock = DockStyle.Fill
            };
            chart.GraphPane.Title.Text = "Ежедневная активность";
            chart.GraphPane.YAxis.Title.Text = "Количество человек";
            chart.GraphPane.XAxis.Scale.MaxAuto = true;
            chart.GraphPane.XAxis.Scale.MinAuto = true;
            chart.GraphPane.YAxis.Scale.MaxAuto = true;
            chart.GraphPane.YAxis.Scale.MinAuto = true;
            chart.GraphPane.Legend.IsVisible = false;
            points = new PointPairList();
            GetPoints(points);

            chart.GraphPane.XAxis.Title.Text = "День с "+points.First().Tag;
            var original = chart.GraphPane.AddCurve("original", points, Color.Blue, SymbolType.None);
            original.Line.IsAntiAlias = true;
            form.Controls.Add(chart);

            chart.AxisChange();
            chart.Invalidate();
            chart.Refresh();
        }

        private void GetPoints(PointPairList pointPairList)
        {
            var encoding = Encoding.GetEncoding(1251);
            var slideRecords = ParsingTask.ParseSlideRecords(File.ReadAllLines("slides.txt", encoding));
            var visitRecords =ParsingTask.ParseVisitRecords(File.ReadAllLines("visits.txt", encoding), slideRecords).ToList();
            var pairs = CountActivity(visitRecords)
                .Select(da => Tuple.Create($"{da.Date.Year}-{da.Date.Month}-{da.Date.Day}", da.VisitsAmount))
                .OrderBy(t => t.Item1);
            var x = 0;
            foreach (var pair in pairs)
            {
                pointPairList.Add(x,pair.Item2,pair.Item1);
                x++;
            }

        }

        [STAThread]
        static void Main()
        {
            new Program().Run();
        }

        private void Run()
        {
            Application.Run(form);
        }

        public static IEnumerable<DayActivity> CountActivity(IEnumerable<VisitRecord> visits)
        {
            return visits.Select(visit => visit.DateTime.Date)
                .GroupBy(date => date)
                .Select(group => new DayActivity(group.Key, group.Count()));
        }

        public struct DayActivity
        {
            public readonly DateTime Date;
            public readonly int VisitsAmount;

            public DayActivity(DateTime date, int visitsAmount)
            {
                Date = date;
                VisitsAmount = visitsAmount;
            }
        }
    }
}