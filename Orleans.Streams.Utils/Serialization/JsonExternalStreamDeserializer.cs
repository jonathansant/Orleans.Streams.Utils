﻿using Newtonsoft.Json;
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

		public T Deserialize<T>(QueueProperties queueProps, byte[] data)
		{
			using (var stream = new MemoryStream(data))
			using (var reader = new StreamReader(stream, Encoding.UTF8))
				return (T)_serializer.Deserialize(reader, typeof(T));
		}

		public void Dispose() { }
	};
}
