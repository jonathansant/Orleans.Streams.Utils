using System;

namespace Orleans.Streams.Utils.Serialization
{
	public interface IExternalStreamSerializer
	{
		T Deserialize<T>(object obj);
		object Deserialize(Type type, object obj);
	}
}
