using Orleans.Concurrency;
using Orleans.Placement;
using Orleans.Runtime;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Orleans.Streams.Utils.MessageTracking
{
	public interface IMessageTrackingGrain : IGrainWithStringKey
	{
		[OneWay]
		Task Track(Immutable<IBatchContainer> batchContainer);
	}

	[PreferLocalPlacement]
	public class MessageTrackingGrain : Grain, IMessageTrackingGrain
	{
		private readonly IServiceProvider _serviceProvider;
		private ITraceWriter _traceWriter;

		public MessageTrackingGrain(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public override Task OnActivateAsync(CancellationToken cancellationToken)
		{
			var keyParts = this.GetPrimaryKeyString().Split(
				new[] { "::" },
				StringSplitOptions.RemoveEmptyEntries
			);

			_traceWriter = _serviceProvider.GetRequiredServiceByName<ITraceWriter>(keyParts.First());
			return Task.CompletedTask;
		}

		public Task Track(Immutable<IBatchContainer> batchContainer)
			=> _traceWriter.Write(batchContainer.Value);
	}
}