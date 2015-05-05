using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Orion.SQL;

namespace Orion.Extensions
{
	public static class DbEx
	{
		/// <summary>
		/// Executes a query on a database.
		/// </summary>
		/// <param name="olddb">Database to query</param>
		/// <param name="query">Query string with parameters as @0, @1, etc.</param>
		/// <param name="args">Parameters to be put in the query</param>
		/// <returns>Rows affected by query</returns>
		[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		public static int Query(this IDbConnection olddb, string query, params object[] args)
		{
			using (IDbConnection db = olddb.CloneEx())
			{
				db.Open();
				using (IDbCommand com = db.CreateCommand())
				{
					com.CommandText = query;
					for (int i = 0; i < args.Length; i++)
						com.AddParameter("@" + i, args[i]);

					return com.ExecuteNonQuery();
				}
			}
		}

		/// <summary>
		/// Executes a query on a database.
		/// </summary>
		/// <param name="olddb">Database to query</param>
		/// <param name="query">Query string with parameters as @0, @1, etc.</param>
		/// <param name="args">Parameters to be put in the query</param>
		/// <returns>Query result as IDataReader</returns>
		[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		public static QueryResult QueryReader(this IDbConnection olddb, string query, params object[] args)
		{
			IDbConnection db = olddb.CloneEx();
			db.Open();
			using (IDbCommand com = db.CreateCommand())
			{
				com.CommandText = query;
				for (int i = 0; i < args.Length; i++)
					com.AddParameter("@" + i, args[i]);

				return new QueryResult(db, com.ExecuteReader());
			}
		}

		public static QueryResult QueryReaderDict(this IDbConnection olddb, string query, Dictionary<string, object> values)
		{
			IDbConnection db = olddb.CloneEx();
			db.Open();
			using (IDbCommand com = db.CreateCommand())
			{
				com.CommandText = query;
				foreach (KeyValuePair<string, object> kv in values)
					com.AddParameter("@" + kv.Key, kv.Value);

				return new QueryResult(db, com.ExecuteReader());
			}
		}

		public static IDbDataParameter AddParameter(this IDbCommand command, string name, object data)
		{
			IDbDataParameter parm = command.CreateParameter();
			parm.ParameterName = name;
			parm.Value = data;
			command.Parameters.Add(parm);
			return parm;
		}

		public static IDbConnection CloneEx(this IDbConnection conn)
		{
			IDbConnection clone = (IDbConnection) Activator.CreateInstance(conn.GetType());
			clone.ConnectionString = conn.ConnectionString;
			return clone;
		}

		public static SqlType GetSqlType(this IDbConnection conn)
		{
			string name = conn.GetType().Name;
			if (name == "SqliteConnection")
				return SqlType.Sqlite;
			if (name == "MySqlConnection")
				return SqlType.Mysql;
			return SqlType.Unknown;
		}

		private static readonly Dictionary<Type, Func<IDataReader, int, object>> ReadFuncs = new Dictionary
			<Type, Func<IDataReader, int, object>>
		{
			{
				typeof (bool),
				(s, i) => s.GetBoolean(i)
			},
			{
				typeof (bool?),
				(s, i) => s.IsDBNull(i) ? null : (object) s.GetBoolean(i)
			},
			{
				typeof (byte),
				(s, i) => s.GetByte(i)
			},
			{
				typeof (byte?),
				(s, i) => s.IsDBNull(i) ? null : (object) s.GetByte(i)
			},
			{
				typeof (Int16),
				(s, i) => s.GetInt16(i)
			},
			{
				typeof (Int16?),
				(s, i) => s.IsDBNull(i) ? null : (object) s.GetInt16(i)
			},
			{
				typeof (Int32),
				(s, i) => s.GetInt32(i)
			},
			{
				typeof (Int32?),
				(s, i) => s.IsDBNull(i) ? null : (object) s.GetInt32(i)
			},
			{
				typeof (Int64),
				(s, i) => s.GetInt64(i)
			},
			{
				typeof (Int64?),
				(s, i) => s.IsDBNull(i) ? null : (object) s.GetInt64(i)
			},
			{
				typeof (string),
				(s, i) => s.GetString(i)
			},
			{
				typeof (decimal),
				(s, i) => s.GetDecimal(i)
			},
			{
				typeof (decimal?),
				(s, i) => s.IsDBNull(i) ? null : (object) s.GetDecimal(i)
			},
			{
				typeof (float),
				(s, i) => s.GetFloat(i)
			},
			{
				typeof (float?),
				(s, i) => s.IsDBNull(i) ? null : (object) s.GetFloat(i)
			},
			{
				typeof (double),
				(s, i) => s.GetDouble(i)
			},
			{
				typeof (double?),
				(s, i) => s.IsDBNull(i) ? null : (object) s.GetDouble(i)
			},
			{
				typeof (object),
				(s, i) => s.GetValue(i)
			},
		};

		public static T Get<T>(this IDataReader reader, string column)
		{
			return reader.Get<T>(reader.GetOrdinal(column));
		}

		public static T Get<T>(this IDataReader reader, int column)
		{
			if (reader.IsDBNull(column))
				return default(T);

			if (ReadFuncs.ContainsKey(typeof (T)))
				return (T) ReadFuncs[typeof (T)](reader, column);

			throw new NotImplementedException();
		}
	}
}