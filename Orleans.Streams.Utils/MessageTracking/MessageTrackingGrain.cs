using Orleans.Concurrency;
using Orleans.Placement;
using System.Threading.Tasks;

namespace Orleans.Streams.Utils.MessageTracking
{
	public interface IMessageTrackingGrain : IGrainWithStringKey
	{
		[OneWay]
		Task Track(Immutable<TrackingUnit> batchContainer);
	}

	[PreferLocalPlacement]
	public class MessageTrackingGrain : Grain, IMessageTrackingGrain
	{
		private readonly ITraceWriter _traceWriter;

		public MessageTrackingGrain(ITraceWriter traceWriter)
		{
			_traceWriter = traceWriter;
		}

		public Task Track(Immutable<TrackingUnit> trackingUnit)
			=> _traceWriter.Write(trackingUnit.Value);
	}
}