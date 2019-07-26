using System;
using System.Collections.Generic;

namespace Orleans.Streams.Utils.MessageTracking
{
	public class TrackingUnit
	{
		public Guid StreamGuid { get; }
		public string StreamNamespace { get; }
		public StreamSequenceToken SequenceToken { get; }
		public List<object> Events { get; }

		public TrackingUnit(
			Guid streamGuid,
			string streamNamespace,
			StreamSequenceToken sequenceToken,
			List<object> events
		)
		{
			StreamGuid = streamGuid;
			StreamNamespace = streamNamespace;
			SequenceToken = sequenceToken;
			Events = events;
		}
	}
}
