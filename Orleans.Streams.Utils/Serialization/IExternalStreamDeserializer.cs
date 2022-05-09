using Confluent.Kafka;
using System;

namespace Orleans.Streams.Utils.Serialization
{
	public interface IExternalStreamDeserializer : IDisposable
	{
		object Deserialize(QueueProperties queueProps, Type type, byte[] data);

		object Deserialize(QueueProperties queueProps, Type type, ConsumeResult<byte[], byte[]> data)
			=> Deserialize(queueProps, type, data.Message.Value);
	}
}
