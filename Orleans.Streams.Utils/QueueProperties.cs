using System;
using System.IO.Hashing;
using System.Text;

namespace Orleans.Streams.Utils
{
	public class QueueProperties
	{
		public string QueueName { get; }
		public string Namespace { get; }
		public uint PartitionId { get; }
		public uint Hash { get; }
		public bool IsExternal { get; }
		public Type ExternalContractType { get; }

		public QueueProperties(
			string @namespace,
			uint partitionId = 0,
			bool isExternal = false,
			Type externalContractType = null
		)
		{
			Namespace = @namespace;
			PartitionId = partitionId;
			QueueName = $"{@namespace}_{partitionId.ToString()}";
			Hash = BitConverter.ToUInt32(XxHash64.Hash(Encoding.UTF8.GetBytes(@namespace)));
			IsExternal = isExternal;
			ExternalContractType = externalContractType;
		}
	}
}
