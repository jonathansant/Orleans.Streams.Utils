using Orleans.Streams.Utils.Serialization;
using System;

namespace Orleans.Streams.Utils
{
	public class QueueProperties
	{
		public string QueueName { get; }
		public string Namespace { get; }
		public uint PartitionId { get; }
		public uint Hash { get; }
		public bool IsExternal { get; set; }
		public IExternalStreamDeserializer ExternalStreamDeserializer { get; }

		public QueueProperties(
			string @namespace,
			uint partitionId = 0,
			bool isExternal = false,
			IExternalStreamDeserializer externalStreamDeserializer = null
		)
		{
			if (isExternal && externalStreamDeserializer == null)
				throw new ArgumentNullException(
					nameof(externalStreamDeserializer),
					"External topics should specify an external deserializer"
				);

			ExternalStreamDeserializer = externalStreamDeserializer;
			Namespace = @namespace;
			PartitionId = partitionId;
			QueueName = $"{@namespace}_{partitionId.ToString()}";
			Hash = JenkinsHash.ComputeHash(@namespace);
			IsExternal = isExternal;
		}
	}
}