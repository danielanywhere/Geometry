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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using static Geometry.GeometryUtil;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FLine																																		*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Line with single-precision points.
	/// </summary>
	public class FLine
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
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new Instance of the FLine Item.
		/// </summary>
		public FLine()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the FLine Item.
		/// </summary>
		/// <param name="pointA">
		/// Reference to the first point in the new line.
		/// </param>
		/// <param name="pointB">
		/// Reference to the second point in the new line.
		/// </param>
		public FLine(FPoint pointA, FPoint pointB)
		{
			mPointA = pointA;
			mPointB = pointB;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a memberwise clone of the provided line.
		/// </summary>
		/// <param name="source">
		/// Reference to the source line to be cloned.
		/// </param>
		/// <returns>
		/// Reference to a new FLine instance where the primitive member values
		/// are the same as those in the source, if a legitimate source was
		/// provided. Otherwise, an empty FLine.
		/// </returns>
		public static FLine Clone(FLine source)
		{
			FLine result = new FLine();

			if(source != null)
			{
				result.mPointA.X = source.mPointA.X;
				result.mPointA.Y = source.mPointA.Y;
				result.mPointB.X = source.mPointB.X;
				result.mPointB.Y = source.mPointB.Y;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetClosestPoint																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the closest point between the caller's line and an arbitrary
		/// point.
		/// </summary>
		/// <param name="line">
		/// Reference to the line to be checked.
		/// </param>
		/// <param name="point">
		/// Reference to the point to test for proximity.
		/// </param>
		/// <returns>
		/// Reference to the closest point between the caller's line and an
		/// arbitrary external point, if valid. Otherwise, null.
		/// </returns>
		public static FPoint GetClosestPoint(FLine line, FPoint point)
		{
			FPoint ab = null;
			float abab = 0f;
			FPoint ac = null;
			float acab = 0f;
			FPoint result = null;
			float t = 0f;

			if(!FLine.IsEmpty(line) && point != null)
			{
				ab = line.PointB - line.PointA;
				ac = point - line.PointA;

				abab = FPoint.Dot(ab, ab);
				acab = FPoint.Dot(ac, ab);

				if(abab != 0f)
				{
					t = acab / abab;
				}

				// Clamp t to ensure the closest point stays within the segment [A, B].
				t = Math.Max(0, Math.Min(1, t));

				result = line.PointA + ab * t;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetIntersectingLine																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a reference to the first line intersecting the caller's point
		/// from the supplied list of lines.
		/// </summary>
		/// <param name="lines">
		/// Reference to a list of lines, one or more of which might contain
		/// the caller's point.
		/// </param>
		/// <param name="point">
		/// Reference to the point to compare.
		/// </param>
		/// <returns>
		/// Reference to the first line in the collection containing the caller's
		/// point, if found. Otherwise, null.
		/// </returns>
		public static FLine GetIntersectingLine(List<FLine> lines, FPoint point)
		{
			FLine result = null;

			if(lines?.Count > 0)
			{
				foreach(FLine lineItem in lines)
				{
					if(FLine.IsPointOnLine(lineItem, point))
					{
						result = lineItem;
						break;
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetIntersectingLines																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a list of lines intersecting the caller's point
		/// within the supplied list of lines.
		/// </summary>
		/// <param name="lines">
		/// Reference to a list of lines, one or more of which might contain
		/// the caller's point.
		/// </param>
		/// <param name="point">
		/// Reference to the point to compare.
		/// </param>
		/// <returns>
		/// Reference to a list of lines in the collection containing the
		/// caller's point, if found. Otherwise, an empty list.
		/// </returns>
		public static List<FLine> GetIntersectingLines(List<FLine> lines,
			FPoint point)
		{
			List<FLine> result = new List<FLine>();

			if(lines?.Count > 0)
			{
				foreach(FLine lineItem in lines)
				{
					if(FLine.IsPointOnLine(lineItem, point))
					{
						result.Add(lineItem);
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetLine																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a rotated version of the supplied line.
		/// </summary>
		/// <param name="line">
		/// Reference to the line to be rotated.
		/// </param>
		/// <param name="rotation">
		/// Optional angle of local shape rotatation, in radians.
		/// </param>
		/// <returns>
		/// Reference to the representation of the caller's line, rotated to the
		/// specified rotation.
		/// </returns>
		public static FLine GetLine(FLine line, float rotation = 0f)
		{
			List<FPoint> points = null;
			FLine result = new FLine();

			if(line != null)
			{
				points = GetVertices(line, rotation);
				if(points.Count == 2)
				{
					result.mPointA = points[0];
					result.mPointB = points[1];
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetVertices																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the vertices of the line.
		/// </summary>
		/// <param name="line">
		/// Reference to the line whose ends will be inspected.
		/// </param>
		/// <param name="rotation">
		/// Optional angle of local shape rotatation, in radians.
		/// </param>
		/// <returns>
		/// Reference to a list of floating-point points representing the vertices
		/// of the line.
		/// </returns>
		public static List<FPoint> GetVertices(FLine line, float rotation = 0f)
		{
			FPoint center = null;
			FPoint location = null;
			FPoint point = null;
			List<FPoint> result = new List<FPoint>();
			FLine workingLine = null;

			if(line != null)
			{
				if(rotation == 0f)
				{
					//	Vertices with no rotation is much faster.
					result.AddRange(new FPoint[]
					{
						line.mPointA,
						line.mPointB
					});
				}
				else
				{
					//	Avoid affecting the caller's line object.
					workingLine = Clone(line);
					center = new FPoint(
						workingLine.mPointA.X +
							((workingLine.mPointB.X - workingLine.mPointA.X) / 2f),
						workingLine.mPointA.Y +
							((workingLine.mPointB.Y - workingLine.mPointA.Y) / 2f));
					location = FPoint.Clone(workingLine.mPointA);
					//	Translate to origin.
					Translate(workingLine, FPoint.Invert(center));
					//	Rotate and translate back.
					point = FPoint.Rotate(workingLine.mPointA, rotation);
					FPoint.Translate(point, center);
					result.Add(point);
					point = FPoint.Rotate(workingLine.mPointB, rotation);
					FPoint.Translate(point, center);
					result.Add(point);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	HasIntersection																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether two lines share an intersection,
		/// either real or imagined.
		/// </summary>
		/// <param name="lineA">
		/// Reference to the first line to compare for intersection.
		/// </param>
		/// <param name="lineB">
		/// Reference to the second line to compare for intersection.
		/// </param>
		/// <param name="allowImaginary">
		/// Value indicating whether or not the planes of the two lines
		/// can be said to have an intersection outside of the physical
		/// line boundaries.
		/// </param>
		/// <returns>
		/// A value indicating whether an intersection exists for the lines or
		/// their imaginary planes.
		/// </returns>
		public static bool HasIntersection(FLine lineA, FLine lineB,
			bool allowImaginary = false)
		{
			FPoint point = null;
			bool result = false;
			if(lineA != null && lineB != null)
			{
				point = Intersect(lineA, lineB, allowImaginary);
				if(!float.IsNegativeInfinity(point.X) &&
					!float.IsNegativeInfinity(point.Y))
				{
					//	Both values were resolved. The line has at least an imaginary
					//	crossing.
					if(allowImaginary ||
						(IsPointNearLine(lineA, point, 0.0005f) &&
						IsPointNearLine(lineB, point, 0.0005f)))
					{
						//	Either imaginary is allowed or the intersection was not
						//	imaginary.
						result = true;
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	HasSharedPoints																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the two lines have one or more
		/// shared points.
		/// </summary>
		/// <param name="lineA">
		/// Reference to the first line to test for a shared point.
		/// </param>
		/// <param name="lineB">
		/// Reference to the second line to test for a shared point.
		/// </param>
		/// <returns>
		/// True if the two lines share an intersecting point. Otherwise, false.
		/// </returns>
		public static bool HasSharedPoints(FLine lineA, FLine lineB)
		{
			bool result = false;
			if(lineA != null && lineB != null)
			{
				if((lineA.mPointA.X == lineB.mPointA.X &&
					lineA.mPointA.Y == lineB.mPointA.Y) ||
					(lineA.mPointA.X == lineB.mPointB.X &&
					lineA.mPointA.Y == lineB.mPointB.Y) ||
					(lineA.mPointB.X == lineB.mPointA.X &&
					lineA.mPointB.Y == lineB.mPointA.Y) ||
					(lineA.mPointB.X == lineB.mPointB.X &&
					lineA.mPointB.Y == lineB.mPointB.Y))
				{
					result = true;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Intersect																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Calculate an intersection between the two lines and return the result
		/// as a point.
		/// </summary>
		/// <param name="lineA">
		/// Reference to the first line to consider.
		/// </param>
		/// <param name="lineB">
		/// Reference to the second line to consider.
		/// </param>
		/// <param name="allowImaginary">
		/// Value indicating whether the two lines can be said to have an
		/// intersection, even when it occurs on a line's plane outside of
		/// its boundaries.
		/// </param>
		/// <returns>
		/// Reference to the point at which the two lines intersect, if
		/// an intersection was found. Otherwise, a point where X and Y
		/// are set to float.NegativeInfinity.
		/// </returns>
		public static FPoint Intersect(FLine lineA,
			FLine lineB, bool allowImaginary = false)
		{
			double rd = 0f;
			FPoint result =
				new FPoint(float.NegativeInfinity, float.NegativeInfinity);
			double rn = 0f;
			double x1 = 0f;
			double x2 = 0f;
			double x3 = 0f;
			double x4 = 0f;
			double y1 = 0f;
			double y2 = 0f;
			double y3 = 0f;
			double y4 = 0f;

			if(lineA != null && lineB != null)
			{
				//	Using the matrix determinant of a four point square matrix.
				x1 = lineA.mPointA.X;
				y1 = lineA.mPointA.Y;
				x2 = lineA.mPointB.X;
				y2 = lineA.mPointB.Y;
				x3 = lineB.mPointA.X;
				y3 = lineB.mPointA.Y;
				x4 = lineB.mPointB.X;
				y4 = lineB.mPointB.Y;
				rn = (x1 * y2 - y1 * x2) * (x3 - x4) -
					(x1 - x2) * (x3 * y4 - y3 * x4);
				rd = (x1 - x2) * (y3 - y4) -
					(y1 - y2) * (x3 - x4);
				result.X = (rd != 0d ? (float)(rn / rd) : float.NegativeInfinity);
				rn = (x1 * y2 - y1 * x2) * (y3 - y4) -
					(y1 - y2) * (x3 * y4 - y3 * x4);
				rd = (x1 - x2) * (y3 - y4) -
					(y1 - y2) * (x3 - x4);
				result.Y = (rd != 0d ? (float)(rn / rd) : float.NegativeInfinity);

				if(!allowImaginary &&
					!IsPointNearLine(lineA, result, 0.0005f) &&
					!IsPointNearLine(lineB, result, 0.0005f))
				{
					//	The intersection was only imaginary, and does not sit on either
					//	physical line. In this case the intersection of the lines should
					//	be the middle point between line1.pointB : line2.pointA.
					result = FPoint.MiddlePoint(lineA.mPointB, lineB.mPointA);
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Given line AB that has one point nearer than the Point C vector
		/// magnitude and one point further away, return Point D where vector C
		/// intersects line AB.
		/// </summary>
		/// <param name="line">
		/// Reference to line AB.
		/// </param>
		/// <param name="vector">
		/// Reference to the vector C point.
		/// </param>
		/// <param name="magnitude">
		/// The magnitude of vector C.
		/// </param>
		/// <returns>
		/// Reference to point D where vector C intersects line AB, if a
		/// point could be matched. Otherwise, null.
		/// </returns>
		public static FPoint Intersect(FLine line, FPoint vector, float magnitude)
		{
			FPoint result = null;

			if(line != null)
			{
				result = Intersect(line.mPointA, line.mPointB, vector, magnitude);
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Given line AB that has one point nearer than the Point C vector
		/// magnitude and one point further away, return Point D where vector C
		/// intersects line AB.
		/// </summary>
		/// <param name="pointA">
		/// Reference to point A in line AB.
		/// </param>
		/// <param name="pointB">
		/// Reference to point B in line AB.
		/// </param>
		/// <param name="vector">
		/// Reference to the vector C point.
		/// </param>
		/// <param name="magnitude">
		/// The magnitude of vector C.
		/// </param>
		/// <returns>
		/// Reference to point D where vector C intersects line AB, if a
		/// point could be matched. Otherwise, null.
		/// </returns>
		public static FPoint Intersect(FPoint pointA, FPoint pointB,
			FPoint vector, float magnitude)
		{
			FPoint ab = null;
			float abLength = 0f;
			FPoint result = null;
			float t = 0f;

			if(pointA != null && pointB != null && vector != null)
			{
				ab = pointB - pointA;
				abLength = FPoint.Magnitude(ab);
				if(abLength != 0f)
				{
					t = magnitude / abLength;
					result = pointA + t * ab;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsEmpty																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified line is empty.
		/// </summary>
		/// <param name="line">
		/// Reference to the object to inspect.
		/// </param>
		/// <returns>
		/// True if the specified line is empty. Otherwise, false.
		/// </returns>
		public static bool IsEmpty(FLine line)
		{
			bool result = true;

			if(line != null)
			{
				result = (line.mPointA.X == 0f && line.mPointA.Y == 0f &&
					line.mPointB.X == 0f && line.mPointB.Y == 0f);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsPointAtEnd																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the provided point is at one of the
		/// ends of the specified line.
		/// </summary>
		/// <param name="line">
		/// Reference to the line to test.
		/// </param>
		/// <param name="point">
		/// The point to compare.
		/// </param>
		/// <returns>
		/// True if the specified point is at one of the ends of the line.
		/// </returns>
		public static bool IsPointAtEnd(FLine line, FPoint point)
		{
			bool result = false;

			if(line != null && point != null)
			{
				result =
					((line.PointA.X == point.X && line.PointA.Y == point.Y) ||
					(line.PointB.X == point.X && line.PointB.Y == point.Y));
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	IsPointInArea																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the given point lies inside the
		/// bounding area defined by the points of the two provided lines.
		/// </summary>
		/// <param name="lineA">
		/// Reference to the first two points of the area.
		/// </param>
		/// <param name="lineB">
		/// Reference to the second two points of the area.
		/// </param>
		/// <param name="point">
		/// Reference to the point to check for containment.
		/// </param>
		/// <returns>
		/// True if the caller's point exists in the area established by the
		/// four points of the two lines. Otherwise, false.
		/// </returns>
		public static bool IsPointInArea(FLine lineA, FLine lineB, FPoint point)
		{
			float maxX = 0f;
			float maxY = 0f;
			float minX = 0f;
			float minY = 0f;
			bool result = false;

			if(lineA != null && lineB != null && point != null)
			{
				minX = Math.Min(Math.Min(Math.Min(lineA.mPointA.X, lineA.mPointB.X),
					lineB.mPointA.X), lineB.mPointB.X);
				minY = Math.Min(Math.Min(Math.Min(lineA.mPointA.Y, lineA.mPointB.Y),
					lineB.mPointA.Y), lineB.mPointB.Y);
				maxX = Math.Max(Math.Max(Math.Max(lineA.mPointA.X, lineA.mPointB.X),
					lineB.mPointA.X), lineB.mPointB.X);
				maxY = Math.Max(Math.Max(Math.Max(lineA.mPointA.Y, lineA.mPointB.Y),
					lineB.mPointA.Y), lineB.mPointB.Y);
				if(point.X >= minX && point.Y <= maxX &&
					point.Y >= minY && point.Y <= maxY)
				{
					result = true;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	IsPointNearLine																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the caller's point is near the
		/// specified line, within the given resolution.
		/// </summary>
		/// <param name="line">
		/// Reference to the line to test for proximity.
		/// </param>
		/// <param name="point">
		/// Reference to the point to test for proximity.
		/// </param>
		/// <param name="resolution">
		/// The maximum distance at which the point would be considered to be
		/// near the line.
		/// </param>
		/// <returns>
		/// True if the given point is within resolution distance the specified
		/// line. Otherwise, false.
		/// </returns>
		public static bool IsPointNearLine(FLine line, FPoint point,
			float resolution)
		{
			float angle = 0f;
			float ntangent = 0f;
			FPath path = null;
			bool result = false;
			float tangent = 0f;
			float x = 0f;
			float y = 0f;

			if(line != null && point != null)
			{
				angle = Trig.GetLineAngle(line);
				tangent = angle + (float)(0.5 * Math.PI);
				ntangent = (float)(tangent + Math.PI);
				if(tangent < 0)
				{
					tangent += (float)(2 * Math.PI);
				}
				else if(tangent > (2 * Math.PI))
				{
					tangent -= (float)(2 * Math.PI);
				}
				if(ntangent < 0)
				{
					ntangent += (float)(2 * Math.PI);
				}
				else if(ntangent > (2 * Math.PI))
				{
					ntangent -= (float)(2 * Math.PI);
				}
				path = new FPath();
				x = line.mPointA.X + Trig.GetLineAdjFromAngHyp(tangent, resolution);
				y = line.mPointA.Y + Trig.GetLineOppFromAngHyp(tangent, resolution);
				path.Add(x, y);
				x += Trig.GetLineAdjFromAngHyp(ntangent, resolution * 2f);
				y += Trig.GetLineOppFromAngHyp(ntangent, resolution * 2f);
				path.Add(x, y);
				x = line.mPointB.X + Trig.GetLineAdjFromAngHyp(ntangent, resolution);
				y = line.mPointB.Y + Trig.GetLineOppFromAngHyp(ntangent, resolution);
				path.Add(x, y);
				x += Trig.GetLineAdjFromAngHyp(tangent, resolution * 2f);
				y += Trig.GetLineOppFromAngHyp(tangent, resolution * 2f);
				path.Add(x, y);
				result = FPath.IsPointInPolygon(path, point);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsPointNearPoint																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether one point is near another.
		/// </summary>
		/// <param name="point1">
		/// Reference to the first point to compare.
		/// </param>
		/// <param name="point2">
		/// Reference to the second point to compare.
		/// </param>
		/// <param name="resolution">
		/// The scope of resolution that determines nearness.
		/// </param>
		/// <returns>
		/// True if the points are within the specified distance from one
		/// another to be considered as near. Otherwise, false.
		/// </returns>
		public static bool IsPointNearPoint(FPoint point1, FPoint point2,
			float resolution)
		{
			float lineLen = 0f;
			bool result = false;

			if(point1 != null && point2 != null)
			{
				lineLen = Trig.GetLineDistance(point1, point2);
				if(lineLen <= resolution)
				{
					result = true;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	IsPointOnLine																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the caller's point is on the
		/// specified line.
		/// </summary>
		/// <param name="line">
		/// Reference to the line to test.
		/// </param>
		/// <param name="point">
		/// Reference to the point to test for membership.
		/// </param>
		/// <returns>
		/// True if the specified point is on the provided line. Otherwise,
		/// false.
		/// </returns>
		public static bool IsPointOnLine(FLine line, FPoint point)
		{
			float maxX = 0f;
			float maxY = 0f;
			float minX = 0f;
			float minY = 0f;
			bool result = false;

			if(line != null && point != null)
			{
				maxX = Math.Max(line.mPointA.X, line.mPointB.X);
				maxY = Math.Max(line.mPointA.Y, line.mPointB.Y);
				minX = Math.Min(line.mPointA.X, line.mPointB.X);
				minY = Math.Min(line.mPointA.Y, line.mPointB.Y);
				if(line.mPointA.X == line.mPointB.X)
				{
					//	Line is vertical.
					if(point.X == line.mPointA.X && line.mPointA.Y != line.mPointB.Y)
					{
						//	Line is not a point.
						result =
							point.Y >= minY &&
							point.Y <= maxY;
					}
				}
				else if(point.Y == line.mPointA.Y && line.mPointA.Y == line.mPointB.Y)
				{
					//	Line is horizontal.
					result =
						point.X >= minX &&
						point.X <= maxX;
				}
				else if(point.X >= minX && point.X <= maxX &&
					point.Y >= minY && point.Y <= maxY)
				{
					//	Line is diagonalish, and X is in the bounding area.
					result =
						((point.X - line.mPointA.X) +
						(line.mPointB.X - point.X) ==
						(line.mPointB.X - line.mPointA.X)) &&
						((point.Y - line.mPointA.Y) +
						(line.mPointB.Y - point.Y) ==
						(line.mPointB.Y - line.mPointA.Y));
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PointA																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Inheritable point A field for reduced instruction cycle access.
		/// </summary>
		protected FPoint mPointA = new FPoint();
		/// <summary>
		/// Get/Set a reference to point A.
		/// </summary>
		public virtual FPoint PointA
		{
			get { return mPointA; }
			set { mPointA = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PointB																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Inheritable point B field for reduced instruction cycle access.
		/// </summary>
		protected FPoint mPointB = new FPoint();
		/// <summary>
		/// Get/Set a reference to point B.
		/// </summary>
		public virtual FPoint PointB
		{
			get { return mPointB; }
			set { mPointB = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TransferValues																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Transfer member values to the specified line.
		/// </summary>
		/// <param name="target">
		/// Reference to the target line to receive the new points.
		/// </param>
		/// <param name="pointA">
		/// Reference to the point to be assigned to the target PointA property.
		/// </param>
		/// <param name="pointB">
		/// Reference to the point to be assigned to the target PointB property.
		/// </param>
		public static void TransferValues(FLine target,
			FPoint pointA, FPoint pointB)
		{
			FPoint.TransferValues(pointA, target.mPointA);
			FPoint.TransferValues(pointB, target.mPointB);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Translate																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Translate the points of the provided target line by the specified
		/// offset.
		/// </summary>
		/// <param name="target">
		/// Reference to the target line to be translated.
		/// </param>
		/// <param name="offset">
		/// Reference to the offset by which to translate the line.
		/// </param>
		public static void Translate(FLine target, FPoint offset)
		{
			if(target != null && offset != null)
			{
				target.mPointA.X += offset.X;
				target.mPointA.Y += offset.Y;
				target.mPointB.X += offset.X;
				target.mPointB.Y += offset.Y;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TranslateVector																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Translate the target line as a vector from its left or right sides,
		/// along the line's direction of travel.
		/// </summary>
		/// <param name="target">
		/// Reference to the target line to be translated.
		/// </param>
		/// <param name="offset">
		/// The offset amount by which to translate the line parallel to its
		/// current axis.
		/// </param>
		/// <param name="direction">
		/// Optional value indicating whether the offset will be applied to the
		/// left side tangent of the line if Forward, or to the right side tangent
		/// of the line if Reverse, relative to its direction of travel from
		/// PointA to PointB. Default is Forward.
		/// </param>
		public static void TranslateVector(FLine target, float offset,
			ArcDirectionEnum direction = ArcDirectionEnum.Forward)
		{
			float angle = 0f;
			FPoint point = null;

			if(target != null && offset != 0f && direction != ArcDirectionEnum.None)
			{
				angle = Trig.GetLineAngle(target);

				switch(direction)
				{
					case ArcDirectionEnum.Forward:
						angle += HalfPi;
						break;
					case ArcDirectionEnum.Reverse:
						angle -= HalfPi;
						break;
				}
				point = Trig.GetDestPoint(target.mPointA, angle, offset);
				Translate(target, FPoint.Delta(point, target.mPointA));
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	FLineCollection																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of FLine Items.
	/// </summary>
	public class FLineCollection : List<FLine>
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
