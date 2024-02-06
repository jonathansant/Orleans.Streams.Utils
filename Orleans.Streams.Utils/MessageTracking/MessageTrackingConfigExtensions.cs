using Microsoft.Extensions.DependencyInjection;
using Orleans.Hosting;

namespace Orleans.Streams.Utils.MessageTracking
{
	public static class MessageTrackingConfigExtensions
	{
		public static ISiloBuilder UseLoggingTracker(this ISiloBuilder siloHostBuilder, string providerName)
			=> siloHostBuilder.ConfigureServices(services => services.AddKeyedSingleton<ITraceWriter, LoggerTraceWriter>(providerName));
	}
}

