using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Orleans.Streams.Utils.Serialization
{
	public class JsonExternalStreamSerializer : IExternalStreamSerDes
	{
		private readonly JsonSerializer _serializer;

		public JsonExternalStreamSerializer()
		{
			_serializer = JsonSerializer.Create();
		}

		public object Deserialize(QueueProperties queueProps, Type type, byte[] data)
		{
			using var stream = new MemoryStream(data);
			using var reader = new StreamReader(stream, Encoding.UTF8);
			return _serializer.Deserialize(reader, type);
		}

		public byte[] Serialize(QueueProperties queueProps, Type type, object data)
		{
			using var stream = new MemoryStream();
			using var reader = new StreamWriter(stream, Encoding.UTF8);
			_serializer.Serialize(reader, data);

			return stream.ToArray();
		}

		public void Dispose() { }
	};
}
