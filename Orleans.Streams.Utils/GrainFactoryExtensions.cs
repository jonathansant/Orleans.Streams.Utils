using Orleans.Streams.Utils.MessageTracking;

namespace Orleans.Streams.Utils
{
	public static class GrainFactoryExtensions
	{
		public static IMessageTrackingGrain GetMessageTrackerGrain(this IGrainFactory grainFactory, string receiverId) 
			=> grainFactory.GetGrain<IMessageTrackingGrain>(receiverId);
	}
}