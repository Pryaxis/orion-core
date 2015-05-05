using System;
using System.Collections.Generic;

namespace Orion.Utilities
{
	/// <summary>
	/// Class that provides methods to sort lists of IComparable data types
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Sorter<T> where T : IComparable
	{
		/// <summary>
		/// Sorts the provided IList using a MergeSort algorithm
		/// </summary>
		/// <param name="unsorted"></param>
		public void MergeSort(IList<T> unsorted)
		{
			MergeSort(unsorted, 0, unsorted.Count);
		}

		private void MergeSort(IList<T> unsorted, int low, int high)
		{
			int N = high - low;
			if (N <= 1)
				return;

			int mid = low + N/2;

			MergeSort(unsorted, low, mid);
			MergeSort(unsorted, mid, high);

			T[] aux = new T[N];
			int i = low, j = mid;
			for (int k = 0; k < N; k++)
			{
				if (i == mid)
				{
					aux[k] = unsorted[j++];
				}
				else if (j == high)
				{
					aux[k] = unsorted[i++];
				}
				else if (unsorted[j].CompareTo(unsorted[i]) < 0)
				{
					aux[k] = unsorted[j++];
				}
				else
				{
					aux[k] = unsorted[i++];
				}
			}

			for (int k = 0; k < N; k++)
			{
				unsorted[low + k] = aux[k];
			}
		}
	}
}