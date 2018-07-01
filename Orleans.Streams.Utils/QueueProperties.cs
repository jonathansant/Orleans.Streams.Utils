namespace Orleans.Streams.Utils
{
	public class QueueProperties
	{
		public string QueueName { get; }
		public string Namespace { get; }
		public uint PartitionId { get; }
		public uint Hash { get; }

		public QueueProperties(string @namespace, uint partitionId = 0)
		{
			Namespace = @namespace;
			PartitionId = partitionId;
			QueueName = $"{@namespace}_{@partitionId}";
			Hash = JenkinsHash.ComputeHash(@namespace);
		}
	}
}