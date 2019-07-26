using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Orleans.Streams.Utils.MessageTracking
{
	public class LoggerTraceWriter : ITraceWriter
	{
		private readonly ILogger _logger;

		public LoggerTraceWriter(ILogger<LoggerTraceWriter> logger)
		{
			_logger = logger;
		}

		public Task Write(TrackingUnit trackingUnit)
		{
			var count = trackingUnit
				.Events
				.Count();

			_logger.LogInformation(
				"Received message: Batch container created {@trackingUnit} with {count} event(s)",
				trackingUnit,
				count
			);

			return Task.CompletedTask;
		}
	}
}