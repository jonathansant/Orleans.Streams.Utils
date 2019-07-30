using Orleans.Streams.Utils.MessageTracking;

namespace Orleans.Streams.Utils
{
	public static class GrainFactoryExtensions
	{
		public static IMessageTrackingGrain GetMessageTrackerGrain(
			this IGrainFactory grainFactory,
			string providerName,
			string receiverId
		) => grainFactory.GetGrain<IMessageTrackingGrain>($"{providerName}::{receiverId}");
	}
}