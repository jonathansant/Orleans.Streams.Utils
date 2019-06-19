using Microsoft.Extensions.DependencyInjection;
using Orleans.Hosting;

namespace Orleans.Streams.Utils.MessageTracking
{
	public static class MessageTrackingConfigExtensions
	{
		public static ISiloHostBuilder UseLoggingTracker(this ISiloHostBuilder siloHostBuilder)
			=> siloHostBuilder.ConfigureServices(services => services.AddSingleton<ITraceWriter, LoggerTraceWriter>());
	}
}