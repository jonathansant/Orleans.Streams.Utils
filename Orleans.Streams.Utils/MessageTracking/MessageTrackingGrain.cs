using System.Threading.Tasks;
using Orleans.Concurrency;

namespace Orleans.Streams.Utils.MessageTracking
{
	public interface IMessageTrackingGrain : IGrainWithStringKey
	{
		[OneWay]
		Task Track(IBatchContainer batchContainer);
	}

	[StatelessWorker(1)]
	public class MessageTrackingGrain : Grain, IMessageTrackingGrain
	{
		private readonly ITraceWriter _traceWriter;

		public MessageTrackingGrain(ITraceWriter traceWriter)
		{
			_traceWriter = traceWriter;
		}

		public Task Track(IBatchContainer batchContainer)
			=> _traceWriter.Write(batchContainer);
	}
}