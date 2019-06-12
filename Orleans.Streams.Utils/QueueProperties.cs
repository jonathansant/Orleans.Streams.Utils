namespace Orleans.Streams.Utils
{
	public class QueueProperties
	{
		public string QueueName { get; }
		public string Namespace { get; }
		public uint PartitionId { get; }
		public uint Hash { get; }
		public bool IsExternal { get; set; }

		public QueueProperties(string @namespace, uint partitionId = 0, bool isExternal = false)
		{
			Namespace = @namespace;
			PartitionId = partitionId;
			QueueName = $"{@namespace}_{partitionId.ToString()}";
			Hash = JenkinsHash.ComputeHash(@namespace);
			IsExternal = isExternal;
		}
	}
}