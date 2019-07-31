using System.Threading.Tasks;

namespace Orleans.Streams.Utils.MessageTracking
{
	public interface ITraceWriter
	{
		Task Write(IBatchContainer batchContainer);
	}
}