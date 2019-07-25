using Orleans.Concurrency;
using System.Threading.Tasks;

namespace Orleans.Streams.Utils.MessageTracking
{
	public interface IMessageTrackingGrain : IGrainWithStringKey
	{
		[OneWay]
		Task Track(Immutable<IBatchContainer> batchContainer);
	}

	[StatelessWorker(1)]
	public class MessageTrackingGrain : Grain, IMessageTrackingGrain
	{
		private readonly ITraceWriter _traceWriter;

		public MessageTrackingGrain(ITraceWriter traceWriter)
		{
			_traceWriter = traceWriter;
		}

		public Task Track(Immutable<IBatchContainer> batchContainer)
			=> _traceWriter.Write(batchContainer.Value);
	}
}