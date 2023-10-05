using System;

namespace Orleans.Streams.Utils.Serialization
{
	public interface IExternalStreamDeserializer : IDisposable
	{
		object Deserialize(QueueProperties queueProps, Type type, byte[] data);
	}
}