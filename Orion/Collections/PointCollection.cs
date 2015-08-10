using System;
using System.Collections;
using System.Collections.Generic;

namespace Orion.Collections
{
	public class PointCollection : ICollection
	{
		private List<Point> _points;
		private int _xMax, _xMin;
		private int _yMax, _yMin;

		public int Count
		{
			get
			{
				return _points.Count;
			}
		}

		public bool IsSynchronized
		{
			get
			{
				return ((ICollection)_points).IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return ((ICollection)_points).SyncRoot;
			}
		}

		public PointCollection()
		{
			_points = new List<Point>();
		}

		public PointCollection(string points)
		{
			_points = new List<Point>();

            foreach (string point in points.Split(','))
			{
				string[] xy = point.Split('.');

				if (xy.Length != 2)
				{
					continue;
				}

				int x, y;
				if (!Int32.TryParse(xy[0], out x) || !Int32.TryParse(xy[1], out y))
				{
					continue;
				}

				AddPoint(x, y);
			}
		}

		[Temporary("This is for unit testing and can be removed safely")]
		public void ReverseTest()
		{
			_points.Reverse();
		}

		/// <summary>
		/// Adds a point to the collection
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void AddPoint(int x, int y)
		{
			AddPoint(new Point(x, y));
		}

		/// <summary>
		/// Adds a point to the collection
		/// </summary>
		/// <param name="point"></param>
		public void AddPoint(Point point)
		{
			_points.Add(point);

			//Check if the new point sets a new min/max for x or y
			int i = Count - 1;
			if (_points[i].X > _xMax)
			{
				_xMax = _points[i].X;
			}
			if (_points[i].Y > _yMax)
			{
				_yMax = _points[i].Y;
			}
			if (_points[i].X < _xMin)
			{
				_xMin = _points[i].X;
			}
			if (_points[i].Y < _yMin)
			{
				_yMin = _points[i].Y;
			}
		}

		/// <summary>
		/// Determines if the given point is in this collection's area
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public bool IsInArea(Point p)
		{
			//Return false if the point is blatantly out of range
			if (p.X < _xMin || p.X > _xMax || p.Y < _yMin || p.Y > _yMax)
			{
				return false;
			}

			//2 points mean the region is a line or a rectangle.
			//These only need a simple bounds check
			if (Count == 2)
			{
				//Test if point is inside the bounds of the rectangle
				if (p.X >= _points[0].X && p.Y >= _points[0].Y
					&& p.X <= _points[1].X && p.Y <= _points[1].Y)
				{
					return true;
				}
			}

			//This block does a winding number calculation. 
			//If wn comes out as 0, the point is outside the polygon
			int wn = 0;
			for (int i = 0; i < Count; i++)
			{
				Point p1 = _points[i];

				//if our point is equal to one of the points defining the polygon, it can be considered 'inside'
				if (p1.Equals(p))
				{
					return true;
				}

				Point p2 = i == Count - 1 ? _points[0] : _points[i + 1];

				if (p1.Y <= p.Y)
				{
					if (p2.Y > p.Y)
					{
						if (Position(p1, p2, p) > 0)
						{
							wn++;
						}
					}
				}
				else
				{
					if (p2.Y <= p.Y)
					{
						if (Position(p1, p2, p) < 0)
						{
							wn--;
						}
					}
				}
			}

			return wn != 0;
		}

		/// <summary>
		/// Determines if the given (x, y) point is in this collection's area
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public bool IsInArea(int x, int y)
		{
			return IsInArea(new Point(x, y));
		}

		/// <summary>
		/// Determines the position of p2 relative to p0 and p1 (left, right, or in line with)
		/// </summary>
		/// <param name="p0"></param>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		/// <returns></returns>
		private int Position(Point p0, Point p1, Point p2)
		{
			return ((p1.X - p0.X) * (p2.Y - p0.Y) - (p2.X - p0.X) * (p1.Y - p0.Y));
		}

		public void CopyTo(Array array, int index)
		{
			((ICollection)_points).CopyTo(array, index);
		}

		public IEnumerator GetEnumerator()
		{
			return ((ICollection)_points).GetEnumerator();
		}
	}
}
