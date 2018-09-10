namespace Orleans.Streams.Utils
{
	public class QueueProperties
	{
		public string QueueName { get; }
		public string Namespace { get; }
		public uint PartitionId { get; }
		public uint Hash { get; }

		public QueueProperties(string @namespace, uint? partitionId = null)
		{
			Namespace = @namespace;
			PartitionId = partitionId ?? 0;
			QueueName = $"{@namespace}_{partitionId}";
			Hash = partitionId != null ? PartitionId : JenkinsHash.ComputeHash(@namespace);
		}
	}
}