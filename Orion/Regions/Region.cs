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
		public List<int> AllowedGroups { get; set; }
		/// <summary>
		/// User IDs allowed to use this region
		/// </summary>
		public List<int> AllowedUsers { get; set; }
		/// <summary>
		/// Points that make up the bounds of this region
		/// </summary>
		public PointCollection Points { get; set; }

		public void AddPoint(int x, int y)
		{
			Points.AddPoint(x, y);
		}

		public void AddPoint(Point p)
		{
			Points.AddPoint(p);
		}

		public bool ContainsPoint(int x, int y)
		{
			return Points.IsInArea(x, y);
		}

		public bool ContainsPoint(Point p)
		{
			return Points.IsInArea(p);
		}

		public Region()
		{
			Points = new PointCollection();
		}
	}
}
