using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Orleans.Streams.Utils.Serialization
{
	public class JsonExternalStreamDeserializer : IExternalStreamDeserializer
	{
		private readonly JsonSerializer _serializer;

		public JsonExternalStreamDeserializer()
		{
			_serializer = JsonSerializer.Create();
		}

		public object Deserialize(QueueProperties queueProps, Type type, byte[] data)
		{
			using (var stream = new MemoryStream(data))
			using (var reader = new StreamReader(stream, Encoding.UTF8))
				return _serializer.Deserialize(reader, type);
		}

		public void Dispose() { }
	};
}
