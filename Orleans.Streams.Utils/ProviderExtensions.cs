using System.Xml;
using Orleans.Streams.Utils;

// ReSharper disable once CheckNamespace
namespace Orleans.Streams
{
	public static class ProviderExtensions
	{
		public static IAsyncStream<T> GetStream<T>(this IStreamProvider streamProvider, string streamId) 
			=> streamProvider.GetStream<T>(StreamProviderUtils.GenerateStreamGuid(streamId));
	}
}
