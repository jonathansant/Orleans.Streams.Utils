using Orleans.Hosting;
using Orleans.Runtime;

namespace Orleans.Streams.Utils.MessageTracking
{
	public static class MessageTrackingConfigExtensions
	{
		public static ISiloBuilder UseLoggingTracker(this ISiloBuilder siloHostBuilder, string providerName)
			=> siloHostBuilder.ConfigureServices(services => services.AddSingletonNamedService<ITraceWriter, LoggerTraceWriter>(providerName));
	}
}

