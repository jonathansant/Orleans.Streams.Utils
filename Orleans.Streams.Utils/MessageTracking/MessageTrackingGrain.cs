using Orleans.Concurrency;
using Orleans.Placement;
using Orleans.Runtime;
using System;
using System.Linq;
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
		private readonly IServiceProvider _serviceProvider;
		private ITraceWriter _traceWriter;

		public MessageTrackingGrain(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public override Task OnActivateAsync()
		{
			var keyParts = this.GetPrimaryKeyString().Split(
				new[] { "::" },
				StringSplitOptions.RemoveEmptyEntries
			);

			_traceWriter = _serviceProvider.GetRequiredServiceByName<ITraceWriter>(keyParts.First());
			return Task.CompletedTask;
		}

		public Task Track(Immutable<TrackingUnit> trackingUnit)
			=> _traceWriter.Write(trackingUnit.Value);
	}
}