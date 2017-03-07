using System.Collections.Generic;

namespace yield
{
    public static class MovingAverageTask
    {
        public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
        {
            var valuesQueue = new Queue<double>();
            var prevMovingAvg = 0d;
            var queueHasBeenFulled = false;
            foreach (var dataPoint in data)
            {
            
                valuesQueue.Enqueue(dataPoint.OriginalY);

                var prevQueueSize = queueHasBeenFulled ? valuesQueue.Count : valuesQueue.Count - 1;
                prevMovingAvg = dataPoint.AvgSmoothedY =
                    SimpleMovingAverage(
                        prevMovingAvg,
                        dataPoint.OriginalY,
                        prevQueueSize,
                        valuesQueue.Count,
                        queueHasBeenFulled ? valuesQueue.Dequeue() / windowWidth : 0,
                        queueHasBeenFulled);
                if (!queueHasBeenFulled && valuesQueue.Count == windowWidth)
                    queueHasBeenFulled = true;
                yield return dataPoint;
            }
        }

        private static double SimpleMovingAverage(double prevSMA, double originalValue, int prevQueueSize, int curQueueSize,
            double outlier, bool queueHasBeenFulled)
        {
            return prevSMA
                   * ((double) prevQueueSize / curQueueSize)
                   + originalValue / (queueHasBeenFulled ? curQueueSize - 1 : curQueueSize)
                   - outlier;
        }
    }
}