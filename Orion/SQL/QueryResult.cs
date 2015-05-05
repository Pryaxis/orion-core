using System;
using System.Data;
using Orion.Extensions;

namespace Orion.SQL
{
	public class QueryResult : IDisposable
	{
		public IDbConnection Connection { get; protected set; }
		public IDataReader Reader { get; protected set; }

		public QueryResult(IDbConnection conn, IDataReader reader)
		{
			Connection = conn;
			Reader = reader;
		}

		~QueryResult()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (Reader != null)
				{
					Reader.Dispose();
					Reader = null;
				}
				if (Connection != null)
				{
					Connection.Dispose();
					Connection = null;
				}
			}
		}

		/// <summary>
		/// Attempt to advance to the next database record
		/// </summary>
		/// <returns>true if another record exists</returns>
		public bool Read()
		{
			if (Reader == null)
				return false;
			return Reader.Read();
		}

		/// <summary>
		/// Attempt to get the value in the given column
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="column">Column to obtain value from</param>
		/// <returns></returns>
		public T Get<T>(string column)
		{
			if (Reader == null)
				return default(T);
			return Reader.Get<T>(Reader.GetOrdinal(column));
		}
	}
}
