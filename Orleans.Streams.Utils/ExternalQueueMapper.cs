using Orleans.Concurrency;
using Orleans.Runtime;
using Orleans.Streams.Utils.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Orleans.Streams.Utils
{
	public class ExternalQueueMapper : IConsistentRingStreamQueueMapper
	{
		private readonly IReadOnlyDictionary<string, HashRing<InternalQueueId>> _queueMap;

		public ExternalQueueMapper(IEnumerable<QueueProperties> queues)
		{
			_queueMap = new ReadOnlyDictionary<string, HashRing<InternalQueueId>>(CreateQueueMap(queues));
		}

		public IEnumerable<QueueId> GetAllQueues()
			=> _queueMap.Values
				.SelectMany(queueMap => queueMap.GetAllRingMembers())
				.Select(identifiers =>
					QueueId.GetQueueId(identifiers.QueueNamePrefix, identifiers.QueueId, identifiers.UniformHashCache));

		public QueueId GetQueueForStream(Guid streamGuid, string streamNamespace)
		{
			if (!_queueMap.ContainsKey(streamNamespace))
				throw new ArgumentException("No queue for supplied namespace");

			var identifier = _queueMap[streamNamespace].CalculateResponsible(streamGuid); // todo: Error handling
			return QueueId.GetQueueId(identifier.QueueNamePrefix, identifier.QueueId, identifier.UniformHashCache);
		}

		public IEnumerable<QueueId> GetQueuesForRange(IRingRange range)
			=> from ring in _queueMap.Values
			from queueId in ring.GetAllRingMembers()
			where range.InRange(queueId.GetUniformHashCode())
			select QueueId.GetQueueId(queueId.QueueNamePrefix, queueId.QueueId, queueId.UniformHashCache);

		private static IDictionary<string, HashRing<InternalQueueId>> CreateQueueMap(
			IEnumerable<QueueProperties> queueProps)
		{
			return queueProps
				.GroupBy(props => props.Namespace)
				.ToDictionary(
					grouping => grouping.Key,
					grouping =>
					{
						var ringSize = grouping.Count();

						return new HashRing<InternalQueueId>(grouping.Select((props, iteration) =>
						{
							var uniformHashCode = (uint)0;

							if (ringSize == 1)
								return new InternalQueueId(props.QueueName, props.Hash, uniformHashCode);

							var portion = checked((uint)(RangeFactory.RING_SIZE / ringSize + 1));
							uniformHashCode = checked(portion * (uint)iteration);
							return new InternalQueueId(props.QueueName, props.Hash, uniformHashCode);
						}));
					});
		}

		/// <summary>
		/// Identifier of a durable queue. This is a Hack which can be avoided if we work in the Orleans workspace.
		/// </summary>
		[Serializable]
		[Immutable]
		private sealed class InternalQueueId : IRingIdentifier<InternalQueueId>
		{
			public string QueueNamePrefix { get; }

			public uint QueueId { get; }

			public uint UniformHashCache { get; }

			public InternalQueueId(string queuePrefix, uint id, uint hash)
			{
				QueueNamePrefix = queuePrefix;
				QueueId = id;
				UniformHashCache = hash;
			}

			public uint GetUniformHashCode()
				=> UniformHashCache;

			public bool Equals(InternalQueueId other)
				=> other != null
				&& QueueId == other.QueueId
				&& string.Equals(QueueNamePrefix, other.QueueNamePrefix, StringComparison.Ordinal)
				&& UniformHashCache == other.UniformHashCache;

			public override bool Equals(object obj) => Equals(obj as InternalQueueId);

			public override int GetHashCode()
				=> (int)QueueId
				   ^ (QueueNamePrefix != null ? QueueNamePrefix.GetHashCode() : 0)
				   ^ (int)UniformHashCache;
		}
	}
}