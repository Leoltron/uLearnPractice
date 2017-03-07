using System.Collections.Generic;

namespace yield
{
	public static class ExpSmoothingTask
	{
		public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
		{
		    DataPoint prevDataPoint = null;
		    foreach (var dataPoint in data)
		    {
		        dataPoint.ExpSmoothedY =
                    alpha * dataPoint.OriginalY + (1 - alpha) * prevDataPoint?.ExpSmoothedY ??
		                                 dataPoint.OriginalY;

		        prevDataPoint = dataPoint;
		        yield return dataPoint;
		    }
		}
	}
}