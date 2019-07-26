using Orleans.Hosting;
using Orleans.Runtime;

namespace Orleans.Streams.Utils.MessageTracking
{
	public static class MessageTrackingConfigExtensions
	{
		public static ISiloHostBuilder UseLoggingTracker(this ISiloHostBuilder siloHostBuilder, string providerName)
			=> siloHostBuilder.ConfigureServices(services => services.AddSingletonNamedService<ITraceWriter, LoggerTraceWriter>(providerName));

		public static ISiloBuilder UseLoggingTracker(this ISiloBuilder siloHostBuilder, string providerName)
			=> siloHostBuilder.ConfigureServices(services => services.AddSingletonNamedService<ITraceWriter, LoggerTraceWriter>(providerName));
	}
}

