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
		//*	Assign																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Assign values to the specified line.
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
		public static void Assign(FLine target, FPoint pointA, FPoint pointB)
		{
			FPoint.Assign(pointA, target.mPointA);
			FPoint.Assign(pointB, target.mPointB);
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
			float rd = 0f;
			FPoint result =
				new FPoint(float.NegativeInfinity, float.NegativeInfinity);
			float rn = 0f;
			float x1 = 0f;
			float x2 = 0f;
			float x3 = 0f;
			float x4 = 0f;
			float y1 = 0f;
			float y2 = 0f;
			float y3 = 0f;
			float y4 = 0f;

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
				result.X = (rd != 0 ? rn / rd : float.NegativeInfinity);
				rn = (x1 * y2 - y1 * x2) * (y3 - y4) -
					(y1 - y2) * (x3 * y4 - y3 * x4);
				rd = (x1 - x2) * (y3 - y4) -
					(y1 - y2) * (x3 - x4);
				result.Y = (rd != 0 ? rn / rd : float.NegativeInfinity);

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
