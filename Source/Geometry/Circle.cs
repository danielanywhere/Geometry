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
using System.Linq;

using static Geometry.GeometryUtil;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	Circle																																	*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Features and functionality for working with circles.
	/// </summary>
	public class Circle
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		/// <summary>
		/// Array of quadrant sets crossed in an arc from beginning angle to end
		/// angle, in forward direction, ordered by starting quadrant position in
		/// bits 3..2, and ending quadrant position in bits 1..0.
		/// For example, array[0b0100] identifies start:1, end: 0.
		/// </summary>
		private static readonly int[][] mQuadrantCrossingForward =
			{
				new int[0],
				new int[] { 0 },
				new int[] { 0, 1 },
				new int[] { 0, 1, 2 },
				new int[] { 1, 2, 3 },
				new int[0],
				new int[] { 1 },
				new int[] { 1, 2 },
				new int[] { 2, 3 },
				new int[] { 0, 2, 3 },
				new int[0],
				new int[] { 2 },
				new int[] { 3 },
				new int[] { 0, 3 },
				new int[] { 0, 1, 3 },
				new int[0]
			};
		/// <summary>
		/// Array of quadrant sets crossed in an arc from beginning angle to end
		/// angle, in reverse direction, ordered by starting quadrant position in
		/// bits 3..2, and ending quadrant position in bits 1..0.
		/// For example, array[0b0100] identifies start:1, end: 0.
		/// </summary>
		private static readonly int[][] mQuadrantCrossingReverse =
			{
				new int[0],
				new int[] { 1, 2, 3 },
				new int[] { 2, 3 },
				new int[] { 3 },
				new int[] { 0 },
				new int[0],
				new int[] { 0, 2, 3 },
				new int[] { 0, 3 },
				new int[] { 0, 1 },
				new int[] { 1 },
				new int[0],
				new int[] { 0, 1, 3 },
				new int[] { 0, 1, 2 },
				new int[] { 1, 2 },
				new int[] { 2 },
				new int[0]
			};
		/// <summary>
		/// Array of quadrant sets occupied by an arc from beginning angle to end
		/// angle, in forward direction, ordered by starting quadrant position in
		/// bits 3..2, and ending quadrant position in bits 1..0.
		/// For example, array[0b0100] identifies start:1, end: 0.
		/// </summary>
		private static readonly int[][] mQuadrantsForward =
			{
				new int[] { 0 },
				new int[] { 0, 1 },
				new int[] { 0, 1, 2 },
				new int[] { 0, 1, 2, 3 },
				new int[] { 0, 1, 2, 3 },
				new int[] { 1 },
				new int[] { 1, 2 },
				new int[] { 1, 2, 3 },
				new int[] { 0, 2, 3 },
				new int[] { 0, 1, 2, 3 },
				new int[] { 2 },
				new int[] { 2, 3 },
				new int[] { 0, 3 },
				new int[] { 0, 1, 3 },
				new int[] { 0, 1, 2, 3 },
				new int[] { 3 }
			};
		/// <summary>
		/// Array of quadrant sets occupied by an arc from beginning angle to end
		/// angle, in reverse direction, ordered by starting quadrant position in
		/// bits 3..2, and ending quadrant position in bits 1..0.
		/// For example, array[0b0100] identifies start:1, end: 0.
		/// </summary>
		private static readonly int[][] mQuadrantsReverse =
			{
				new int[] { 0 },
				new int[] { 0, 1, 2, 3 },
				new int[] { 0, 2, 3 },
				new int[] { 0, 3 },
				new int[] { 0, 1 },
				new int[] { 1 },
				new int[] { 0, 1, 2, 3 },
				new int[] { 0, 1, 3 },
				new int[] { 0, 1, 2 },
				new int[] { 1, 2 },
				new int[] { 2 },
				new int[] { 0, 1, 2, 3 },
				new int[] { 0, 1, 2, 3 },
				new int[] { 1, 2, 3 },
				new int[] { 2, 3 },
				new int[] { 3 }
			};

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* GetAngleAtSliceIndex																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Given a circle sliced symmetrically into multiple equal slices with a
		/// reference to 0 degrees, return the starting angle of the slice at the
		/// specified index, in radians.
		/// </summary>
		/// <param name="slices">
		/// Count of slices defined in the circle.
		/// </param>
		/// <param name="index">
		/// The index of the slice at which to retrieve the starting angle.
		/// </param>
		/// <returns>
		/// The angle within the circle at which the specified slice begins. Index
		/// 0 will always return zero, the beginning of any slice of the circle.
		/// </returns>
		/// <remarks>
		/// <para>
		/// This method will return correct values for negative indices and indices
		/// greater than the number of slices defined, to support cases where
		/// a scenario includes multiple turns in either direction.
		/// </para>
		/// <para>
		/// If your case might include a multi-turn scenario, consider calling
		/// Trig.NormalizeRad on the result of this method to maintain an angle
		/// less than a single turn.
		/// </para>
		/// </remarks>
		public static float GetAngleAtSliceIndex(int slices, int index)
		{
			float result = 0f;

			if(slices > 0)
			{
				result = (TwoPi / (float)slices) * (float)index;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetArcBoundingBox																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a reference to the bounding box for a circular arc being
		/// measured by center point, start point, and end point, with a winding
		/// direction.
		/// </summary>
		/// <param name="center">
		/// Reference to the circle center.
		/// </param>
		/// <param name="start">
		/// Reference to the start point.
		/// </param>
		/// <param name="end">
		/// Reference to the end point.
		/// </param>
		/// <param name="winding">
		/// The winding direction taken to create the arc.
		/// </param>
		/// <returns>
		/// Reference to the bounding box occupied by the specified arc, if
		/// legitimate values were presented. Otherwise, a empty rectangle.
		/// </returns>
		public static FArea GetArcBoundingBox(FVector2 center,
			FVector2 start, FVector2 end, ArcDirectionEnum winding)
		{
			float angleEnd = 0f;
			float angleStart = 0f;
			FVector2 endActual = null;
			int[] crossings = null;
			float maxX = 0f;
			float maxY = 0f;
			float minX = 0f;
			float minY = 0f;
			float radius = 0f;
			FArea result = new FArea();
			List<FVector2> samples = new List<FVector2>();

			if(center != null && start != null && end != null &&
				winding != ArcDirectionEnum.None)
			{
				samples.Add(start);

				//	The specified end point is arbitrary. The actual end point
				//	is the point on the curve corresponding to the angle
				//	established from the center to the stated end point.
				radius = (float)Math.Sqrt(
					Math.Pow(start.X - center.X, 2f) +
					Math.Pow(start.Y - center.Y, 2f));

				angleStart = Trig.GetLineAngle(center, start);
				angleEnd = Trig.GetLineAngle(center, end);

				endActual = new FVector2(Trig.GetDestPoint(center, angleEnd, radius));
				samples.Add(endActual);

				crossings = GetQuadrantCrossings(angleStart, angleEnd, winding);
				if(crossings.Length == 0)
				{
					if((winding == ArcDirectionEnum.Forward &&
						angleStart >= angleEnd) ||
						(winding == ArcDirectionEnum.Reverse &&
						angleEnd >= angleStart))
					{
						//	If both angles reside in a single quadrant and the end is
						//	prior to the start, then all quadrants are occupied.
						crossings = new int[] { 0, 1, 2, 3 };
					}
				}
				if(crossings.Length > 0)
				{
					//	Quadrant boundaries are spanned by the shape.
					if(crossings.Contains(0))
					{
						samples.Add(new FVector2(
							Trig.GetDestPoint(center, 0.5f * (float)Math.PI, radius)));
					}
					if(crossings.Contains(1))
					{
						samples.Add(new FVector2(
							Trig.GetDestPoint(center, 1.0f * (float)Math.PI, radius)));
					}
					if(crossings.Contains(2))
					{
						samples.Add(new FVector2(
							Trig.GetDestPoint(center, 1.5f * (float)Math.PI, radius)));
					}
					if(crossings.Contains(3))
					{
						samples.Add(new FVector2(
							Trig.GetDestPoint(center, 0.0f * (float)Math.PI, radius)));
					}
				}

				minX = samples.Min(x => x.X);
				minY = samples.Min(y => y.Y);
				maxX = samples.Max(x => x.X);
				maxY = samples.Max(y => y.Y);
				result.Left = minX;
				result.Top = minY;
				result.Right = maxX;
				result.Bottom = maxY;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a reference to the bounding box for a circular arc being
		/// measured by center point, start point, and end point, with a winding
		/// direction.
		/// </summary>
		/// <param name="center">
		/// Reference to the circle center.
		/// </param>
		/// <param name="start">
		/// Reference to the start point.
		/// </param>
		/// <param name="end">
		/// Reference to the end point.
		/// </param>
		/// <param name="winding">
		/// The winding direction taken to create the arc.
		/// </param>
		/// <param name="drawingSpace">
		/// The drawing space in which the winding direction applies.
		/// Default = Display.
		/// </param>
		/// <returns>
		/// Reference to the bounding box occupied by the specified arc, if
		/// legitimate values were presented. Otherwise, a empty rectangle.
		/// </returns>
		public static FArea GetArcBoundingBox(FVector2 center,
			FVector2 start, FVector2 end, WindingOrientationEnum winding,
			DrawingSpaceEnum drawingSpace = DrawingSpaceEnum.Display)
		{
			ArcDirectionEnum angularRevolution = ArcDirectionEnum.None;
			FArea result = null;

			switch(winding)
			{
				case WindingOrientationEnum.Clockwise:
					switch(drawingSpace)
					{
						case DrawingSpaceEnum.Cartesian:
							angularRevolution = ArcDirectionEnum.Reverse;
							break;
						case DrawingSpaceEnum.Display:
							angularRevolution = ArcDirectionEnum.Forward;
							break;
					}
					break;
				case WindingOrientationEnum.CounterClockwise:
					switch(drawingSpace)
					{
						case DrawingSpaceEnum.Cartesian:
							angularRevolution = ArcDirectionEnum.Forward;
							break;
						case DrawingSpaceEnum.Display:
							angularRevolution = ArcDirectionEnum.Reverse;
							break;
					}
					break;
			}
			if(angularRevolution != ArcDirectionEnum.None)
			{
				result = GetArcBoundingBox(center, start, end, angularRevolution);
			}
			if(result == null)
			{
				result = new FArea();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetQuadrant																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the ordinal index of quadrant in which the specified angle is
		/// located.
		/// </summary>
		/// <param name="angle">
		/// The angle for which the quadrant index will be found, in radians.
		/// </param>
		/// <returns>
		/// The ordinal quadrant index occupied by the specified angle.
		/// </returns>
		/// <remarks>
		/// Following are the quadrant assignments in this version.
		/// <list type="bullet">
		/// <item><b>Quadrant 0</b>. +X, +Y.</item>
		/// <item><b>Quadrant 1</b>. -X, +Y.</item>
		/// <item><b>Quadrant 2</b>. -X, -Y.</item>
		/// <item><b>Quadrant 3</b>. +X, -Y.</item>
		/// </list>
		/// </remarks>
		public static int GetQuadrant(float angle)
		{
			float part = (float)((double)Trig.NormalizeRad(angle) / Math.PI);
			int result = 0;

			if(part >= 0f && part < 0.5f)
			{
				result = 0;
			}
			else if(part >= 0.5 && part < 1.0f)
			{
				result = 1;
			}
			else if(part >= 1.0f && part < 1.5f)
			{
				result = 2;
			}
			else
			{
				result = 3;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetQuadrantCrossings																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return an array containing the ordinal quadrant crossings of the
		/// quadrants occupied by the specified arc.
		/// </summary>
		/// <param name="startAngle">
		/// The starting angle, in radians.
		/// </param>
		/// <param name="endAngle">
		/// The ending angle, in radians.
		/// </param>
		/// <param name="windingDirection">
		/// The winding direction taken by the arc.
		/// </param>
		/// <returns>
		/// Reference to an array of ordinal quadrants crossed out of by
		/// the arc, if any where found. Otherwise, an empty array.
		/// </returns>
		/// <remarks>
		/// In this version, quadrant 0 is the area in +X, +Y,
		/// quadrant 1 is -X, +Y, quadrant 2 is -X, -Y,
		/// and quadrant 3 is +X, -Y.
		/// </remarks>
		public static int[] GetQuadrantCrossings(float startAngle, float endAngle,
			ArcDirectionEnum windingDirection)
		{
			int quadrantEnd = GetQuadrant(endAngle);
			int quadrantIndex = 0;
			int quadrantStart = GetQuadrant(startAngle);
			int[] result = null;

			quadrantIndex = (quadrantStart << 2) | quadrantEnd;

			switch(windingDirection)
			{
				case ArcDirectionEnum.Forward:
					result = mQuadrantCrossingForward[quadrantIndex];
					break;
				case ArcDirectionEnum.Reverse:
					result = mQuadrantCrossingReverse[quadrantIndex];
					break;
			}
			if(result == null)
			{
				result = new int[0];
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return an array containing the ordinal quadrant crossings of the
		/// quadrants occupied by the specified arc.
		/// </summary>
		/// <param name="startAngle">
		/// The starting angle, in radians.
		/// </param>
		/// <param name="endAngle">
		/// The ending angle, in radians.
		/// </param>
		/// <param name="windingDirection">
		/// The winding direction taken by the arc.
		/// </param>
		/// <param name="drawingSpace">
		/// The drawing space for which this request is being prepared.
		/// Default = Display.
		/// </param>
		/// <returns>
		/// Reference to an array of ordinal quadrants crossed out of by
		/// the arc, if any where found. Otherwise, an empty array.
		/// </returns>
		/// <remarks>
		/// In this version, quadrant 0 is the area in +X, +Y,
		/// quadrant 1 is -X, +Y, quadrant 2 is -X, -Y,
		/// and quadrant 3 is +X, -Y.
		/// </remarks>
		public static int[] GetQuadrantCrossings(float startAngle, float endAngle,
			WindingOrientationEnum windingDirection,
			DrawingSpaceEnum drawingSpace = DrawingSpaceEnum.Display)
		{
			int[] result = null;


			switch(windingDirection)
			{
				case WindingOrientationEnum.Clockwise:
					switch(drawingSpace)
					{
						case DrawingSpaceEnum.Cartesian:
							result = GetQuadrantCrossings(startAngle, endAngle,
								ArcDirectionEnum.Decreasing);
							break;
						case DrawingSpaceEnum.Display:
							result = GetQuadrantCrossings(startAngle, endAngle,
								ArcDirectionEnum.Increasing);
							break;
					}
					break;
				case WindingOrientationEnum.CounterClockwise:
					switch(drawingSpace)
					{
						case DrawingSpaceEnum.Cartesian:
							result = GetQuadrantCrossings(startAngle, endAngle,
								ArcDirectionEnum.Increasing);
							break;
						case DrawingSpaceEnum.Display:
							result = GetQuadrantCrossings(startAngle, endAngle,
								ArcDirectionEnum.Decreasing);
							break;
					}
					break;
			}
			if(result == null)
			{
				result = new int[0];
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetQuadrantsOccupied																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return an array containing the ordinal quadrant positions of the
		/// quadrants occupied by the specified arc.
		/// </summary>
		/// <param name="startAngle">
		/// The starting angle, in radians.
		/// </param>
		/// <param name="endAngle">
		/// The ending angle, in radians.
		/// </param>
		/// <param name="windingDirection">
		/// The winding direction taken by the arc.
		/// </param>
		/// <returns>
		/// Reference to an array of ordinal quadrant positions occupied by
		/// the arc, if any where found. Otherwise, an empty array.
		/// </returns>
		/// <remarks>
		/// In this version, quadrant 0 is the area in +X, +Y,
		/// quadrant 1 is -X, +Y, quadrant 2 is -X, -Y,
		/// and quadrant 3 is +X, -Y.
		/// </remarks>
		public static int[] GetQuadrantsOccupied(float startAngle, float endAngle,
			ArcDirectionEnum windingDirection)
		{
			int quadrantEnd = GetQuadrant(endAngle);
			int quadrantIndex = 0;
			int quadrantStart = GetQuadrant(startAngle);
			int[] result = null;

			quadrantIndex = (quadrantStart << 2) | quadrantEnd;

			switch(windingDirection)
			{
				case ArcDirectionEnum.Forward:
					result = mQuadrantsForward[quadrantIndex];
					break;
				case ArcDirectionEnum.Reverse:
					result = mQuadrantsReverse[quadrantIndex];
					break;
			}
			if(result == null)
			{
				result = new int[0];
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return an array containing the ordinal quadrant positions of the
		/// quadrants occupied by the specified arc.
		/// </summary>
		/// <param name="startAngle">
		/// The starting angle, in radians.
		/// </param>
		/// <param name="endAngle">
		/// The ending angle, in radians.
		/// </param>
		/// <param name="windingDirection">
		/// The winding direction taken by the arc.
		/// </param>
		/// <param name="drawingSpace">
		/// The drawing space for which this request is being prepared.
		/// Default = Display.
		/// </param>
		/// <returns>
		/// Reference to an array of ordinal quadrant positions occupied by
		/// the arc, if any where found. Otherwise, an empty array.
		/// </returns>
		/// <remarks>
		/// In this version, quadrant 0 is the area in +X, +Y,
		/// quadrant 1 is -X, +Y, quadrant 2 is -X, -Y,
		/// and quadrant 3 is +X, -Y.
		/// </remarks>
		public static int[] GetQuadrantsOccupied(float startAngle, float endAngle,
			WindingOrientationEnum windingDirection,
			DrawingSpaceEnum drawingSpace = DrawingSpaceEnum.Display)
		{
			int[] result = null;

			switch(windingDirection)
			{
				case WindingOrientationEnum.Clockwise:
					switch(drawingSpace)
					{
						case DrawingSpaceEnum.Cartesian:
							result = GetQuadrantsOccupied(startAngle, endAngle,
								ArcDirectionEnum.Reverse);
							break;
						case DrawingSpaceEnum.Display:
							result = GetQuadrantsOccupied(startAngle, endAngle,
								ArcDirectionEnum.Forward);
							break;
					}
					break;
				case WindingOrientationEnum.CounterClockwise:
					switch(drawingSpace)
					{
						case DrawingSpaceEnum.Cartesian:
							result = GetQuadrantsOccupied(startAngle, endAngle,
								ArcDirectionEnum.Forward);
							break;
						case DrawingSpaceEnum.Display:
							result = GetQuadrantsOccupied(startAngle, endAngle,
								ArcDirectionEnum.Reverse);
							break;
					}
					break;
			}
			if(result == null)
			{
				result = new int[0];
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetVertices																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a list of vertices for a circle of the specified size centered
		/// at the provided location.
		/// </summary>
		/// <param name="center">
		/// Reference to the point at which the circle is centered.
		/// </param>
		/// <param name="radius">
		/// Radius of the circle.
		/// </param>
		/// <param name="vertexCount">
		/// Count of vertices to return.
		/// </param>
		/// <param name="thetaOffset">
		/// The optional rotation of the shape, in radians.
		/// </param>
		/// <returns>
		/// Reference to a list of evenly-spaced vertices around the edge of the
		/// indicated circle, if valid. Otherwise, an empty list.
		/// </returns>
		public static List<FVector2> GetVertices(FVector2 center, float radius,
			int vertexCount, float thetaOffset = 0f)
		{
			float angle = thetaOffset;
			int index = 0;
			List<FVector2> result = new List<FVector2>();
			float space = 0f;

			if(center != null && radius > 0f && vertexCount > 0)
			{
				angle = thetaOffset;
				space = TwoPi / (float)vertexCount;
				for(index = 0; index < vertexCount; index++)
				{
					result.Add(new FVector2(Trig.GetDestPoint(center, angle, radius)));
					angle += space;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
