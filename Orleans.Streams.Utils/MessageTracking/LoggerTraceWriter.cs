using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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
			var events = batchContainer
				.GetEvents<object>()
				.ToDictionary(@event => @event.Item2.ToString(), @event => @event.Item1);
			
			using (_logger.BeginScope(events))
			{
				_logger.LogInformation("Received message: Batch container created {@messageBatch}", batchContainer);
			}

			return Task.CompletedTask;
		}
	}
}