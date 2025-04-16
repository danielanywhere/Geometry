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

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	SlopeInterceptCollection																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of SlopeInterceptItem Items.
	/// </summary>
	public class SlopeInterceptCollection : List<SlopeInterceptItem>
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

	//*-------------------------------------------------------------------------*
	//*	SlopeInterceptItem																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Expression of a line in slope intercept form.
	/// </summary>
	public class SlopeInterceptItem
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
		///// <summary>
		///// Create a new Instance of the SlopeInterceptItem Item.
		///// </summary>
		//public SlopeInterceptItem()
		//{
		//}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the SlopeInterceptItem Item.
		/// </summary>
		/// <param name="line">
		/// Reference to a floating point cartesian line.
		/// </param>
		public SlopeInterceptItem(FLine line)
		{
			mBaseLine = line;
			Convert(this, line);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the SlopeInterceptItem Item.
		/// </summary>
		/// <param name="pointA">
		/// Reference to the first point of a cartesian line.
		/// </param>
		/// <param name="pointB">
		/// Reference to the second point of a cartesian line.
		/// </param>
		public SlopeInterceptItem(FPoint pointA, FPoint pointB)
		{
			mBaseLine = new FLine(pointA, pointB);
			Convert(this, pointA, pointB);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	B																																			*
		//*-----------------------------------------------------------------------*
		private float mB = 0f;
		/// <summary>
		/// Get/Set the value of the b member of y = mx + b.
		/// </summary>
		public float B
		{
			get { return mB; }
			set { mB = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	BaseLine																															*
		//*-----------------------------------------------------------------------*
		private FLine mBaseLine = null;
		/// <summary>
		/// Get/Set the base line to use in the case that one of slopes is
		/// vertical.
		/// </summary>
		public FLine BaseLine
		{
			get { return mBaseLine; }
			set { mBaseLine = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Convert																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the caller's slope intercept values to represent the specified
		/// line in the form of y = mx + b.
		/// </summary>
		/// <param name="target">
		/// Reference to an existing slope intercept item to receive the converted
		/// values.
		/// </param>
		/// <param name="source">
		/// Reference to the source value to convert.
		/// </param>
		public static void Convert(SlopeInterceptItem target, FLine source)
		{
			if(source != null && target != null)
			{
				Convert(target, source.PointA, source.PointB);
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Set the caller's slope intercept values to represent the specified
		/// line in the form of y = mx + b.
		/// </summary>
		/// <param name="target">
		/// Reference to an existing slope intercept item to receive the converted
		/// values.
		/// </param>
		/// <param name="pointA">
		/// Reference to the first end of the existing line.
		/// </param>
		/// <param name="pointB">
		/// Reference to the second end of the existing line.
		/// </param>
		public static void Convert(SlopeInterceptItem target,
			FPoint pointA, FPoint pointB)
		{
			//	y = (m * x) + b
			//	m = (y2 - y1) / (x2 - x1)
			//	pointA.y = (m * pointA.x) + b
			//	pointA.y - b = (m * pointA.x)
			//	b = 0 - ((m * pointA.x) - pointA.y)
			target.mM = GetSlope(pointA, pointB);
			target.mX = pointA.X;
			target.mY = pointA.Y;
			if(target.mM != 0)
			{
				//	Non-vertical, non-horizontal line.
				//	y = mx + b
				//	b = mx - y
				target.mB = 0f - ((target.mM * target.mX) - target.mY);
			}
			//else
			//{
			//	//	Vertical line.
			//	//	y = x + b
			//	//	b = x - y
			//	target.mB = 0 - (target.mX - target.mY);
			//}
			//	Y was already set by point. Result will be the same.
			//			item.mY = (item.mM * item.mX) + item.mB;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetSlope																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the slope of the provided line.
		/// </summary>
		/// <param name="line">
		/// Reference to the line for which to calculate the slope.
		/// </param>
		/// <returns>
		/// The algebraic slope of the line (y2 - y1) / (x2 - x1).
		/// </returns>
		public static float GetSlope(FLine line)
		{
			float result = 0f;
			if(line != null)
			{
				result = GetSlope(line.PointA, line.PointB);
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the slope of the line expressed by two points.
		/// </summary>
		/// <param name="pointA">
		/// The first end of the line to measure.
		/// </param>
		/// <param name="pointB">
		/// The second end of the line to measure.
		/// </param>
		/// <returns>
		/// The algebraic slope of the line (y2 - y1) / (x2 - x1).
		/// </returns>
		public static float GetSlope(FPoint pointA, FPoint pointB)
		{
			//	m = (y2 - y1) / (x2 - x1)
			float result = 0f;   //	Vertical line by default.

			if(pointA != null && pointB != null)
			{
				if(Math.Abs(pointB.X - pointA.X) > double.Epsilon)
				{
					//	Line is not vertical.
					result = (pointB.Y - pointA.Y) / (pointB.X - pointA.X);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	HasIntersection																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the linear slope intercept intersects
		/// with the specified real line.
		/// </summary>
		/// <param name="slope">
		/// Reference to a preconfigured slope intercept object.
		/// </param>
		/// <param name="line">
		/// A line to test for intersection on the slope.
		/// </param>
		/// <returns>
		/// True if the caller's line intercepts with the slope intercept object.
		/// Otherwise, false.
		/// </returns>
		public static bool HasIntersection(SlopeInterceptItem slope, FLine line)
		{
			FPoint pointC = Intersect(slope, new SlopeInterceptItem(line));
			bool result = false;

			if(!double.IsNaN(pointC.X) && !double.IsNaN(pointC.Y))
			{
				result = FLine.IsPointOnLine(line, pointC);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Intercept																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the intercept (b) value of this item.
		/// </summary>
		public float Intercept
		{
			get { return mB; }
			set { mB = value; }
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
		/// Reference to the first line to intersect.
		/// </param>
		/// <param name="lineB">
		/// Reference to the second line to intersect.
		/// </param>
		/// <returns>
		/// Reference to a double point indicating where the caller's lines
		/// intersected, if an intersection was found. Otherwise, null.
		/// </returns>
		public static FPoint Intersect(SlopeInterceptItem lineA,
			SlopeInterceptItem lineB)
		{
			float distX = 0f;
			float distY = 0f;
			float perX = 0f;
			float perY = 0f;
			FPoint result =
				new FPoint(float.NegativeInfinity, float.NegativeInfinity);

			if(lineA.mB != 0 && lineB.mB != 0)
			{
				//	Non-horizontal, non-vertical line.
				result.X = (lineB.mB - lineA.mB) / (lineA.mM - lineB.mM);
				result.Y = (lineA.mM * result.X) + lineA.mB;
			}
			else if(lineA.mB == 0)
			{
				//	Line A is vertical or horizontal.
				if(lineA.mBaseLine != null)
				{
					if(lineA.mBaseLine.PointA.X == lineA.mBaseLine.PointB.X)
					{
						//	Line A is vertical.
						result.X = lineA.mX;
						//	Find Y on line B between Y start and end points.
						if(lineB.mBaseLine != null)
						{
							if(lineB.mBaseLine.PointA.Y == lineB.mBaseLine.PointB.Y)
							{
								//	Line B is horizontal.
								result.Y = lineB.mBaseLine.PointA.Y;
							}
							else
							{
								//	Total distance of X.
								distX =
									Math.Abs(
										lineB.mBaseLine.PointB.X - lineB.mBaseLine.PointA.X);
								//	Percentage of X.
								perX = Math.Abs(result.X - lineB.mBaseLine.PointA.X) / distX;
								//	Total distance of Y.
								distY =
									Math.Abs(
										lineB.mBaseLine.PointB.Y - lineB.mBaseLine.PointA.Y);
								result.Y =
									Math.Min(lineB.mBaseLine.PointA.Y,
									lineB.mBaseLine.PointB.Y) +
									(distY * perX);
							}
						}
					}
					else
					{
						//	Line A is horizontal.
						result.Y = lineA.mY;
						//	Find X on line B between X start and end points.
						if(lineB.mBaseLine != null)
						{
							if(lineB.mBaseLine.PointA.X == lineB.mBaseLine.PointB.X)
							{
								//	Line B is vertical.
								result.X = lineB.mBaseLine.PointA.X;
							}
							else
							{
								//	Total distance of Y.
								distY =
									Math.Abs(
										lineB.mBaseLine.PointB.Y - lineB.mBaseLine.PointA.Y);
								//	Percentage of Y.
								perY = Math.Abs(result.Y - lineB.mBaseLine.PointA.Y) / distY;
								//	Total distance of X.
								distX =
									Math.Abs(
										lineB.mBaseLine.PointB.X - lineB.mBaseLine.PointA.X);
								//	Distance from origin.
								result.X =
									Math.Min(lineB.mBaseLine.PointA.X,
									lineB.mBaseLine.PointB.X) +
									(distX * perY);
							}
						}
					}
				}
			}
			else if(lineB.mB == 0)
			{
				//	Line B is vertical or horizontal.
				if(lineB.mBaseLine != null)
				{
					if(lineB.mBaseLine.PointA.X == lineB.mBaseLine.PointB.X)
					{
						//	Line B is vertical.
						result.X = lineB.mX;
						//	Find Y on line A between start and end points.
						if(lineA.mBaseLine != null)
						{
							if(lineA.mBaseLine.PointA.Y == lineA.mBaseLine.PointB.Y)
							{
								//	Line A is horizontal.
								result.Y = lineA.mBaseLine.PointA.Y;
							}
							else
							{
								//	Total distance of X.
								distX =
									Math.Abs(lineA.mBaseLine.PointB.X - lineA.mBaseLine.PointA.X);
								//	Percentage of X.
								perX = Math.Abs(result.X - lineA.mBaseLine.PointA.X) / distX;
								//	Total distance of Y.
								distY =
									Math.Abs(lineA.mBaseLine.PointB.Y - lineA.mBaseLine.PointA.Y);
								result.Y =
									Math.Min(lineA.mBaseLine.PointA.Y,
									lineA.mBaseLine.PointB.Y) +
									(distY * perX);
							}
						}
					}
					else
					{
						//	Line B is horizontal.
						result.Y = lineB.mY;
						//	Find X on line A between start and end points.
						if(lineA.mBaseLine != null)
						{
							if(lineA.mBaseLine.PointA.X == lineA.mBaseLine.PointB.X)
							{
								//	Line A is vertical.
								result.X = lineA.mBaseLine.PointA.X;
							}
							else
							{
								//	Total distance of Y.
								distY =
									Math.Abs(lineA.mBaseLine.PointB.Y - lineA.mBaseLine.PointA.Y);
								//	Percentage of Y.
								perY = Math.Abs(result.Y - lineA.mBaseLine.PointA.Y) / distY;
								//	Total distance of X.
								distX =
									Math.Abs(lineA.mBaseLine.PointB.X - lineA.mBaseLine.PointA.X);
								result.X =
									Math.Min(lineA.mBaseLine.PointA.X,
									lineA.mBaseLine.PointB.X) +
									(distX * perY);
							}
						}
					}
				}
			}
			if(double.IsNegativeInfinity(result.X) ||
				double.IsNegativeInfinity(result.Y))
			{
				result = null;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	M																																			*
		//*-----------------------------------------------------------------------*
		private float mM = 0;
		/// <summary>
		/// Get/Set the m property of y = mx + b.
		/// </summary>
		public float M
		{
			get { return mM; }
			set { mM = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Slope																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the slope (m) of this item.
		/// </summary>
		public float Slope
		{
			get { return mM; }
			set { mM = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	X																																			*
		//*-----------------------------------------------------------------------*
		private float mX = 0f;
		/// <summary>
		/// Get/Set the x member of y = mx + b.
		/// </summary>
		public float X
		{
			get { return mX; }
			set { mX = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Y																																			*
		//*-----------------------------------------------------------------------*
		private float mY = 0f;
		/// <summary>
		/// Get/Set the y member of y = mx + b.
		/// </summary>
		public float Y
		{
			get { return mY; }
			set { mY = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
