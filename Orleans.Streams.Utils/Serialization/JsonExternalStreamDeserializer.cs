using Newtonsoft.Json;
using System;

namespace Orleans.Streams.Utils.Serialization
{
	public class JsonExternalStreamDeserializer : IExternalStreamDeserializer
	{
		public T Deserialize<T>(object obj)
			=> JsonConvert.DeserializeObject<T>((string)obj);

		public object Deserialize(Type type, object obj)
			=> JsonConvert.DeserializeObject((string)obj, type);

		public void Dispose() { }
	};
}
