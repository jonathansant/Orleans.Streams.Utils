// Copied from: https://github.com/dotnet/orleans/blob/master/src/Orleans.Core/Runtime/HashRing.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orleans.Streams.Utils.Streams
{
	internal interface IRingIdentifier<T> : IEquatable<T>
	{
		uint GetUniformHashCode();
	}

	internal class HashRing<T>
	{
		private readonly List<IRingIdentifier<T>> _sortedRingList;
		private readonly object _lockable = new object();

		internal HashRing()
		{
			_sortedRingList = new List<IRingIdentifier<T>>();
		}

		public HashRing(IEnumerable<IRingIdentifier<T>> ring)
		{
			var tmpList = new List<IRingIdentifier<T>>(ring);
			tmpList.Sort((x, y) => x.GetUniformHashCode().CompareTo(y.GetUniformHashCode()));
			_sortedRingList = tmpList; // make it read only, so can't add any more elements if created via this constructor.
		}

		public IEnumerable<T> GetAllRingMembers()
		{
			IEnumerable<T> copy;
			lock (_lockable)
			{
				copy = _sortedRingList.Cast<T>().ToArray();
			}
			return copy;
		}

		internal void AddElement(IRingIdentifier<T> element)
		{
			lock (_lockable)
			{
				if (_sortedRingList.Contains(element))
				{
					// we already have this element
					return;
				}

				var hash = element.GetUniformHashCode();

				// insert new element in the sorted order
				// Find the last element with hash smaller than the new element, and insert the latter after (this is why we have +1 here) the former.
				// Notice that FindLastIndex might return -1 if this should be the first element in the list, but then
				// 'index' will get 0, as needed.
				var index = _sortedRingList.FindLastIndex(elem => elem.GetUniformHashCode() < hash) + 1;

				_sortedRingList.Insert(index, element);
			}
		}

		internal void RemoveElement(IRingIdentifier<T> element)
		{
			throw new NotImplementedException();
		}

		public T CalculateResponsible<TR>(IRingIdentifier<TR> element)
		{
			return CalculateResponsible(element.GetUniformHashCode());
		}

		public T CalculateResponsible(Guid guid)
		{
			var guidBytes = guid.ToByteArray();
			var uniformHashCode = JenkinsHash.ComputeHash(guidBytes);
			return CalculateResponsible(uniformHashCode);
		}

		private T CalculateResponsible(uint uniformHashCode)
		{
			lock (_lockable)
			{
				if (_sortedRingList.Count == 0)
				{
					// empty ring.
					return default(T);
				}

				// use clockwise ... current code in membershipOracle.CalculateTargetSilo() does counter-clockwise ...
				// need to implement a binary search, but for now simply traverse the list of silos sorted by their hashes
				var index = _sortedRingList.FindIndex(elem => (elem.GetUniformHashCode() >= uniformHashCode));
				if (index == -1)
				{
					// if not found in traversal, then first element should be returned (we are on a ring)
					return (T) _sortedRingList.First();
				}
				else
				{
					return (T) _sortedRingList[index];
				}
			}
		}

		public override string ToString()
		{
			lock (_lockable)
			{
				return string.Format("All {0}:" + Environment.NewLine + "{1}",
					typeof(T).Name,
					EnumerableToString(
						_sortedRingList,
						elem => string.Format("{0}/x{1,8:X8}", elem, elem.GetUniformHashCode()),
						Environment.NewLine,
						false));
			}
		}

		public static string EnumerableToString<TInput>(IEnumerable<TInput> collection, Func<TInput, string> toString = null,
			string separator = ", ", bool putInBrackets = true)
		{
			if (collection == null)
			{
				if (putInBrackets) return "[]";
				else return "null";
			}
			var sb = new StringBuilder();
			if (putInBrackets) sb.Append("[");
			var enumerator = collection.GetEnumerator();
			bool firstDone = false;
			while (enumerator.MoveNext())
			{
				TInput value = enumerator.Current;
				string val;
				if (toString != null)
					val = toString(value);
				else
					val = value == null ? "null" : value.ToString();

				if (firstDone)
				{
					sb.Append(separator);
					sb.Append(val);
				}
				else
				{
					sb.Append(val);
					firstDone = true;
				}
			}
			if (putInBrackets) sb.Append("]");
			return sb.ToString();
		}
	}
}