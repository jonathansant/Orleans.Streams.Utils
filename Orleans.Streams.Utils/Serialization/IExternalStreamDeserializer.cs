using System;

namespace Orleans.Streams.Utils.Serialization
{
	public interface IExternalStreamDeserializer : IDisposable
	{ }

	public interface IExternalStreamDeserializer<out T> : IExternalStreamDeserializer
	{
		T Deserialize(byte[] data);
	}
}
