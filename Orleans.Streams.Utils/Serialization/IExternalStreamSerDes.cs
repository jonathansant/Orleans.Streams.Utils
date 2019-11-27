using System;

namespace Orleans.Streams.Utils.Serialization
{
	public interface IExternalStreamSerDes : IDisposable
	{
		object Deserialize(QueueProperties queueProps, Type type, byte[] data);
		byte[] Serialize(QueueProperties queueProps, Type type, object data);
	}
}
