using System.Collections.Generic;

namespace Orleans.Streams.Utils.MessageTracking
{
	public static class BatchContainerExtensions
	{
		public static TrackingUnit ToTrackingUnit(this ITraceablebleBatch traceableBatch)
			=> new TrackingUnit(
				traceableBatch.StreamGuid,
				traceableBatch.StreamNamespace,
				traceableBatch.SequenceToken,
				traceableBatch.RawEvents
			);
	}

	public interface ITraceablebleBatch : IBatchContainer
	{
		List<object> RawEvents { get; }
	}
}
