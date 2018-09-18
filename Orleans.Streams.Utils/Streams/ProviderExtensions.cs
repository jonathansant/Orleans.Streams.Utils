

// ReSharper disable once CheckNamespace
namespace Orleans.Streams.Utils.Streams
{
	public static class ProviderExtensions
	{
		public static IAsyncStream<T> GetStream<T>(this IStreamProvider streamProvider, string streamId, string streamNamespace) 
			=> streamProvider.GetStream<T>(StreamProviderUtils.GenerateStreamGuid(streamId), streamNamespace);
	}
}
