using Orion.SQL;
using Orion.Utilities;
using System.Collections.Generic;

namespace Orion.Regions
{
	public class Region
	{
		/// <summary>
		/// Region unique ID
		/// </summary>
		public int ID { get; set; }
		/// <summary>
		/// Region's displayed name
		/// </summary>
		public string DisplayName { get; set; }
		/// <summary>
		/// Group IDs allowed to use this region
		/// </summary>
		public IntCollection AllowedGroups { get; set; }
		/// <summary>
		/// User IDs allowed to use this region
		/// </summary>
		public IntCollection AllowedUsers { get; set; }
		/// <summary>
		/// Region's protection status
		/// </summary>
		public bool Protected { get; set; }
		/// <summary>
		/// Points that make up the bounds of this region
		/// </summary>
		public PointCollection Points { get; set; }

		/// <summary>
		/// Adds a boundary-defining point to the region
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void AddPoint(int x, int y)
		{
			Points.AddPoint(x, y);
		}

		/// <summary>
		/// Adds a boundary-defining point to the region
		/// </summary>
		/// <param name="p"></param>
		public void AddPoint(Point p)
		{
			Points.AddPoint(p);
		}

		/// <summary>
		/// Checks if the given point is inside the region
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public bool ContainsPoint(int x, int y)
		{
			return Points.IsInArea(x, y);
		}

		/// <summary>
		/// Checks if the given point is inside the region
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public bool ContainsPoint(Point p)
		{
			return Points.IsInArea(p);
		}

		public void LoadFromQuery(QueryResult result)
		{
			if (result.Read())
			{
				ID = result.Get<int>("ID");
				DisplayName = result.Get<string>("DisplayName");
				AllowedGroups = new IntCollection(result.Get<string>("AllowedGroups"));
				AllowedUsers = new IntCollection(result.Get<string>("AllowedUsers"));
				Points = new PointCollection(result.Get<string>("Points"));
			}
		}

		public Region()
		{
			ID = -1;
			DisplayName = "Region";
			AllowedGroups = new IntCollection();
			AllowedUsers = new IntCollection();
			Protected = false;
			Points = new PointCollection();
		}
	}
}
