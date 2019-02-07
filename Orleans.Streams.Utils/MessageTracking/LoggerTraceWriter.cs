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
			var messageIndex = 0;

			var events = batchContainer
				.GetEvents<string>()
				.ToDictionary(@event => $"event{messageIndex++}", @event => @event.Item1);

			using (_logger.BeginScope(events))
			{
				_logger.LogInformation("Received message: Batch container created {@messageBatch}", batchContainer);
			}

			return Task.CompletedTask;
		}
	}
}