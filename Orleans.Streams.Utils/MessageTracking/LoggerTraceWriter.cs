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

		public Task Write(IBatchContainer batchContainer)
		{
			var count = batchContainer
				.GetEvents<object>()
				.Count();

			_logger.LogInformation(
				"Received message: Batch container created {@trackingUnit} with {count} event(s)",
				batchContainer,
				count
			);

			return Task.CompletedTask;
		}
	}
}