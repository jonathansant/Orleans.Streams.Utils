using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;
using Orleans.Hosting;

namespace Orleans.Streams.Utils.MessageTracking
{
	public static class MessageTrackingConfigExtensions
	{
		public static ISiloHostBuilder UseLoggingTracker(this ISiloHostBuilder siloHostBuilder)
		{
			return siloHostBuilder
				.EnableDirectClient()
				.ConfigureServices(services => services.AddSingleton<ITraceWriter, LoggerTraceWriter>());
		}
	}
}