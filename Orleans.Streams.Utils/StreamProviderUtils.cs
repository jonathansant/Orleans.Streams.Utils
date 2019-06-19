﻿using Orleans.Streams.Utils.Tools;
using System;

namespace Orleans.Streams.Utils
{
	/// <summary>
	/// Streaming utility functions
	/// </summary>
	public static class StreamProviderUtils
	{
		private static readonly Guid OrleansNamespace = Guid.Parse("5bca7b08-9b10-41dc-aff1-b61499fae79e");

		/// <summary>
		/// Generates a deterministic GUID for a string. 
		/// Useful so that there is no restriction on GUID only stream Ids.
		/// </summary>
		/// <returns>The GUID representing the StreamId.</returns>
		public static Guid GenerateStreamGuid(string streamId) => GuidUtility.Create(OrleansNamespace, streamId);
	}
}