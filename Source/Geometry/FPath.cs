/*
 * Copyright (c). 1997 - 2025 Daniel Patterson, MCSD (danielanywhere).
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 * 
 */

using System.Collections.Generic;
using System.Linq;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FPath																																		*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of points.
	/// </summary>
	public class FPath : List<FPoint>
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	Add																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add an item to the path by member values.
		/// </summary>
		/// <param name="x">
		/// The X coordinate value to add.
		/// </param>
		/// <param name="y">
		/// The Y coordinate value to add.
		/// </param>
		/// <returns>
		/// Reference to the newly created and added point.
		/// </returns>
		public FPoint Add(float x, float y)
		{
			FPoint result = new FPoint();
			result.X = x;
			result.Y = y;
			this.Add(result);
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a memberwise clone of the provided path.
		/// </summary>
		/// <param name="source">
		/// Reference to the source path to be cloned.
		/// </param>
		/// <returns>
		/// Reference to a new FPath instance where the primitive member values
		/// are the same as those in the source, if a legitimate source was
		/// provided. Otherwise, an empty FPath.
		/// </returns>
		public static FPath Clone(FPath source)
		{
			FPath result = new FPath();

			if(source != null)
			{
				foreach(FPoint pointItem in source)
				{
					result.Add(FPoint.Clone(pointItem));
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetCenter																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the center of the polygon.
		/// </summary>
		/// <param name="path">
		/// List of points to inspect.
		/// </param>
		/// <returns>
		/// Reference to the point at the center the aggregate of points in the
		/// path.
		/// </returns>
		public static FPoint GetCenter(List<FPoint> path)
		{
			FPoint result = new FPoint(-1.0f, -1.0f);
			float xa = 0.0f;     //	X Max.
			float xc = 0.0f;     //	X Center.
			float xi = 0.0f;     //	X Min.
			float ya = 0.0f;     //	Y Max.
			float yc = 0.0f;     //	Y Center.
			float yi = 0.0f;     //	Y Min.

			if(path?.Count > 0)
			{
				xa = MaxX(path);
				xi = MinX(path);
				xc = xa - ((xa - xi) / 2.0f);
				ya = MaxY(path);
				yi = MinY(path);
				yc = ya - ((ya - yi) / 2.0f);
				result.X = xc;
				result.Y = yc;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetLines																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a collection of lines representing the supplied path.
		/// </summary>
		/// <param name="path">
		/// Reference to the path to be represented.
		/// </param>
		/// <param name="rotation">
		/// Optional angle of local shape rotatation, in radians.
		/// </param>
		/// <returns>
		/// Reference to a collection of lines representing the provided path.
		/// </returns>
		/// <remarks>
		/// <para>
		/// All of the lines are constructed from common adjoining points, which
		/// allows you to move any point in the shape without breaking its
		/// connection to either of its lines.
		/// </para>
		/// </remarks>
		public static List<FLine> GetLines(FPath path, float rotation = 0f)
		{
			int count = 0;
			int index = 0;
			List<FPoint> points = null;
			List<FLine> result = new List<FLine>();

			if(path != null)
			{
				points = GetVertices(path, rotation);
				count = points.Count - 1;
				if(count > -1)
				{
					for(index = 0; index < count; index++)
					{
						result.Add(new FLine(points[index], points[index + 1]));
					}
					result.Add(new FLine(points[count], points[0]));
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetVertices																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the collection of vertices on the specified path.
		/// </summary>
		/// <param name="path">
		/// Reference to the path to be inspected.
		/// </param>
		/// <param name="rotation">
		/// Optional angle of local shape rotatation, in radians.
		/// </param>
		/// <returns>
		/// Reference to a list of floating-point points representing the vertices
		/// of the area.
		/// </returns>
		public static List<FPoint> GetVertices(FPath path, float rotation = 0f)
		{
			FPoint center = null;
			FPoint point = null;
			List<FPoint> result = new List<FPoint>();
			FPath workingPath = null;

			if(path != null)
			{
				//	Avoid touching the caller's object.
				workingPath = FPath.Clone(path);
				center = GetCenter(workingPath);
				//	Translate to origin.
				Translate(workingPath, FPoint.Invert(center));
				//	Rotate and translate back.
				foreach(FPoint pointItem in workingPath)
				{
					point = FPoint.Rotate(pointItem, rotation);
					FPoint.Translate(point, center);
					result.Add(point);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsEmpty																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified path is empty.
		/// </summary>
		/// <param name="path">
		/// Reference to the object to inspect.
		/// </param>
		/// <returns>
		/// True if the specified path is empty. Otherwise, false.
		/// </returns>
		public static bool IsEmpty(FPath path)
		{
			bool result = true;

			if(path != null)
			{
				result = (path.Count == 0);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	IsPointInPolygon																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified point is in the
		/// caller's polygon.
		/// </summary>
		/// <param name="path">
		/// The path to check to test for.
		/// </param>
		/// <param name="point">
		/// The point to test.
		/// </param>
		/// <param name="allowEdge">
		/// Value indicating whether edge is included as a match.
		/// </param>
		/// <returns>
		/// True if the specified point exists within the polygon. Otherwise,
		/// false.
		/// </returns>
		public static bool IsPointInPolygon(List<FPoint> path, FPoint point,
			bool allowEdge = true)
		{
			bool bContinue = true;
			int count = 0;        //	Vertex count.
			int i = 0;            //	Current vertex index.
			int j = 0;            //	Trailing vertex index.
			float resolution = 0.0005f;
			bool result = false;  //	Cooresponds to an odd number of nodes.
			FPoint vi = null;
			FPoint vj = null;
			float x = 0f;        //	Point under test.
			float y = 0f;        //	Point under test.

			if(path?.Count > 0 && point != null)
			{
				x = point.X;
				y = point.Y;
				count = path.Count;
				if(!allowEdge)
				{
					//	Check all corners.
					for(i = 0; i < count; i++)
					{
						vi = path[i];
						if(point.X == vi.X && point.Y == vi.Y)
						{
							result = false;
							bContinue = false;
							break;
						}
					}
					//	Check all edges.
					for(i = 0; i < count; i++)
					{
						vi = path[i];
						vj = path[i + 1 < count ? i + 1 : 0];
						if(FLine.IsPointNearLine(new FLine(vi, vj), point, resolution))
						{
							result = false;
							bContinue = false;
							break;
						}
					}
				}
				if(bContinue)
				{
					for(i = 0, j = count - 1; i < count; j = i, i++)
					{
						vi = path[i];
						vj = path[j];
						if((vi.Y < y && vj.Y >= y ||
							vj.Y < y && vi.Y >= y) &&
							(vi.X <= x || vj.X <= x))
						{
							if(vi.X + (y - vi.Y) /
								(vj.Y - vi.Y) *
								(vj.X - vi.X) < x)
							{
								result = !result;
							}
						}
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* MaxX																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the maximum X coordinate value found in the provided list of
		/// points.
		/// </summary>
		/// <param name="path">
		/// Reference to a collection of points to search.
		/// </param>
		/// <returns>
		/// The maximum X value found in the collection.
		/// </returns>
		public static float MaxX(List<FPoint> path)
		{
			float result = 0.0f;

			if(path?.Count > 0)
			{
				result = path.Max(x => x.X);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* MaxY																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the maximum Y coordinate value found in the provided list of
		/// points.
		/// </summary>
		/// <param name="path">
		/// Reference to a collection of points to search.
		/// </param>
		/// <returns>
		/// The maximum Y value found in the collection.
		/// </returns>
		public static float MaxY(List<FPoint> path)
		{
			float result = 0.0f;

			if(path?.Count > 0)
			{
				result = path.Max(y => y.Y);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* MinX																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the minumum X coordinate value found in the provided list of
		/// points.
		/// </summary>
		/// <param name="path">
		/// Reference to a collection of points to search.
		/// </param>
		/// <returns>
		/// The minimum X value found in the collection.
		/// </returns>
		public static float MinX(List<FPoint> path)
		{
			float result = 0.0f;

			if(path?.Count > 0)
			{
				result = path.Min(x => x.X);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* MinY																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the minimum Y coordinate value found in the provided list of
		/// points.
		/// </summary>
		/// <param name="path">
		/// Reference to a collection of points to search.
		/// </param>
		/// <returns>
		/// The minimum Y value found in the collection.
		/// </returns>
		public static float MinY(List<FPoint> path)
		{
			float result = 0.0f;

			if(path?.Count > 0)
			{
				result = path.Min(y => y.Y);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Translate																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Translate all of the elements in the path by a uniform distance.
		/// </summary>
		/// <param name="path">
		/// Reference to the path to be translated.
		/// </param>
		/// <param name="offset">
		/// Reference by the axial values by which the path with be moved.
		/// </param>
		public static void Translate(FPath path, FPoint offset)
		{
			if(path != null && offset != null)
			{
				foreach(FPoint pointItem in path)
				{
					FPoint.Translate(pointItem, offset);
				}
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	FPathCollection																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of FPath Items.
	/// </summary>
	public class FPathCollection : List<FPath>
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************


	}
	//*-------------------------------------------------------------------------*

}
