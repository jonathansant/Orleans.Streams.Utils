using System;

namespace Orleans.Streams.Utils.Serialization
{
	public interface IExternalStreamDeserializer : IDisposable
	{
		T Deserialize<T>(QueueProperties queueProps, byte[] data);
	}
}
