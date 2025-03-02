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
using System.Runtime.CompilerServices;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	Trig																																		*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Easy trigonometry functionality.
	/// </summary>
	/// <remarks>
	/// The official cheer of this object: "Sine cosine cosine sine, 3.14159!"
	/// </remarks>
	public class Trig
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
		//*	CalcAutoCADBulge																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Calculate the AutoCAD bulge of a curve and return an array of points
		/// representing lines in a segmented curve.
		/// </summary>
		/// <param name="previousPoint">
		/// Previous point coordinates.
		/// </param>
		/// <param name="currentPoint">
		/// Current point coordinates.
		/// </param>
		/// <param name="bulge">
		/// AutoDesk bulge calculation factor.
		/// </param>
		/// <param name="sweepStep">
		/// Step of sweep between each segment, in degrees.
		/// </param>
		/// <returns>
		/// Returns reference to a collection of points from the start of the
		/// curve to the end.
		/// </returns>
		/// <remarks>
		/// To match AutoCAD's hard-coded angular orientation, this method
		/// always uses the Cartesian drawing space.
		/// </remarks>
		public static List<FPoint> CalcAutoCADBulge(
			FPoint previousPoint, FPoint currentPoint,
			float bulge, float sweepStep)
		{
			float angleCenter = 0;
			float angleHalf = 0;
			float angleIncluded = (float)(Math.Atan(bulge) * 4.0);
			float angleStart = 0;
			float angleSweep = 0;
			float bulgeAngle = 0;
			float bulgeAngleRemain = 0;
			float bulgeAntiAngle = 0;
			FPoint centerPoint = new FPoint(0, 0);
			DirectionEnum direction =
				GetDirection(previousPoint, currentPoint, DrawingSpaceEnum.Cartesian);
			float lineAngle = 0;
			float lineDist1 = 0;
			float lineDist2 = 0;
			FPoint midPoint = new FPoint();
			float pointAngle = GetLineAngle(previousPoint, currentPoint);
			float radius = 0;
			List<FPoint> points = new List<FPoint>();

			if(sweepStep == 0)
			{
				sweepStep = 30.0f;
			}

			//console.log("pointAngle: " + pointAngle);
			//console.log("calcBulge: " +
			//"Previous - x: " + previousPoint.x + "; " +
			//	"y: " + previousPoint.y + "; " +
			//	"Current - x: " + currentPoint.x + "; " +
			//	"y: " + currentPoint.y + "; " +
			//	"Bulge: " + bulge);

			angleHalf = angleIncluded / 2.0f;
			//console.log("Angleincluded: " + angleIncluded);
			//console.log("AngleHalf: " + angleHalf);

			lineAngle = GetLineAngle(previousPoint, currentPoint);
			//console.log("LineAngle: " + lineAngle);

			midPoint.X = (previousPoint.X + currentPoint.X) / 2.0f;
			midPoint.Y = (previousPoint.Y + currentPoint.Y) / 2.0f;

			//console.log("MidPoint - x: " + midPoint.x + "; y: " + midPoint.y);

			lineDist2 = Math.Abs(GetLineDistance(previousPoint, midPoint));
			lineDist1 = Math.Abs(bulge * lineDist2);

			//console.log("LineDist - d2: " + lineDist2 + "; d1: " + lineDist1);

			bulgeAngle = GetLineAngFromAdjOpp(lineDist2, lineDist1);
			bulgeAngleRemain = (float)(Math.PI - ((0.5 * Math.PI) + bulgeAngle));
			bulgeAntiAngle = bulgeAngleRemain - bulgeAngle;
			angleCenter = pointAngle - bulgeAntiAngle;

			//console.log("BulgeAngle: " + bulgeAngle);
			//console.log("BulgeAngleRemain: " + bulgeAngleRemain);
			//console.log("BulgeAntiAngle: " + bulgeAntiAngle);
			//console.log("AngleCenter: " + angleCenter);

			radius = Math.Abs(GetLineHypFromAngOpp(angleHalf, lineDist2));

			//console.log("Radius: " + radius);
			//console.log("Direction: " + direction);

			if(direction == DirectionEnum.East)
			{
				//	Due east direction.		
				//console.log("Due east direction...");
				if(bulge >= 0)
				{
					//console.log("Bulge >= 0");
					centerPoint.X = previousPoint.X +
						GetLineAdjFromAngHyp(angleCenter, radius);
					centerPoint.Y = previousPoint.Y +
						GetLineOppFromAngHyp(angleCenter, radius);
					angleStart = (float)(lineAngle + (0.5 * Math.PI) -
						Math.Abs(angleHalf) - (0 - angleIncluded));
					angleSweep = 0 - angleIncluded;
				}
				else
				{
					// console.log("Bulge < 0");
					// centerPoint.x = previousPoint.x +
					// 	getLineAdjFromAngHyp(angleCenter, radius);
					// centerPoint.y = previousPoint.y +
					// 	getLineOppFromAngHyp(angleCenter, radius);
					// angleStart =	pointAngle + (0.5 * Math.PI) - angleHalf;
					// angleSweep = angleIncluded;
					//console.log("Bulge < 0");
					centerPoint.X = previousPoint.X +
						GetLineAdjFromAngHyp(angleCenter, radius);
					centerPoint.Y = previousPoint.Y +
						GetLineOppFromAngHyp(angleCenter, radius);
					angleStart = (float)(lineAngle + (0.5 * Math.PI) -
						Math.Abs(angleHalf) + (0 - angleIncluded));
					angleSweep = angleIncluded;
				}
			}
			else if((direction & DirectionEnum.East) != 0)
			{
				//	Easterly direction.
				//console.log("Easterly direction...");
				if(bulge >= 0)
				{
					//console.log("Bulge >= 0");
					centerPoint.X = currentPoint.X +
						GetLineAdjFromAngHyp((float)(angleCenter + Math.PI), radius);
					centerPoint.Y = currentPoint.Y +
						GetLineOppFromAngHyp((float)(angleCenter + Math.PI), radius);
					angleStart =
						(float)(pointAngle + (0.5 * Math.PI) - angleHalf - Math.PI);
					angleSweep = angleIncluded;
				}
				else
				{
					// console.log("Bulge < 0");
					// centerPoint.x = previousPoint.x +
					// 	getLineAdjFromAngHyp(angleCenter, radius);
					// centerPoint.y = previousPoint.y +
					// 	getLineOppFromAngHyp(angleCenter, radius);
					// angleStart =	pointAngle + (0.5 * Math.PI) - angleHalf;
					// angleSweep = angleIncluded;
					//console.log("Bulge < 0");
					centerPoint.X = previousPoint.X +
						GetLineAdjFromAngHyp(angleCenter, radius);
					centerPoint.Y = previousPoint.Y +
						GetLineOppFromAngHyp(angleCenter, radius);
					angleStart = (float)(lineAngle + (0.5 * Math.PI) -
						Math.Abs(angleHalf) + (0 - angleIncluded));
					angleSweep = angleIncluded;
				}
			}
			else if(direction == DirectionEnum.West)
			{
				//	Due west direction.
				//console.log("Due west direction...");
				if(bulge >= 0)
				{
					//console.log("Bulge >= 0");
					centerPoint.X = previousPoint.X +
						0 - (GetLineAdjFromAngHyp(0 - bulgeAntiAngle, radius));
					centerPoint.Y = previousPoint.Y +
						GetLineOppFromAngHyp(0 - bulgeAntiAngle, radius);
					angleStart = bulgeAntiAngle;
					angleSweep = angleIncluded;
				}
				else
				{
					//console.log("Bulge < 0");
					centerPoint.X = currentPoint.X +
						GetLineAdjFromAngHyp(angleCenter, radius);
					centerPoint.Y = currentPoint.Y +
						GetLineOppFromAngHyp(angleCenter, radius);
					angleStart = (float)(lineAngle + (0.5 * Math.PI) +
						Math.Abs(angleHalf) - (0 - angleIncluded));
					angleSweep = 0 - angleIncluded;
				}
			}
			else if((direction & DirectionEnum.West) != 0)
			{
				//	Westerly direction.
				//console.log("Westerly direction...");
				if(bulge >= 0)
				{
					//console.log("Bulge >= 0");
					centerPoint.X = currentPoint.X +
						GetLineAdjFromAngHyp((float)(angleCenter + Math.PI), radius);
					centerPoint.Y = currentPoint.Y +
						GetLineOppFromAngHyp((float)(angleCenter + Math.PI), radius);
					angleStart = (float)(lineAngle - (0.5 * Math.PI) +
						Math.Abs(angleHalf) + (0 - angleIncluded));
					angleSweep = angleIncluded;
				}
				else
				{
					//console.log("Bulge < 0");
					centerPoint.X = previousPoint.X +
						GetLineAdjFromAngHyp(angleCenter, radius);
					centerPoint.Y = previousPoint.Y +
						GetLineOppFromAngHyp(angleCenter, radius);
					angleStart = (float)(lineAngle + (0.5 * Math.PI) -
						Math.Abs(angleHalf) + (0 - angleIncluded));
					angleSweep = angleIncluded;
				}
			}

			//console.log("CenterPoint - x: " + centerPoint.x + "; " +
			//	"y: " + centerPoint.y);
			//console.log("AngleStart: " + angleStart);
			//console.log("AngleSweep: " + angleSweep);

			points = SegmentCurve(centerPoint, radius,
				RadToDeg(NormalizeRad(angleStart)), RadToDeg(angleSweep),
				sweepStep);
			return points;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ClampEpsilonDistance																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clamp the length value to the configured number of decimal
		/// points.
		/// </summary>
		/// <param name="value">
		/// The value to adjust.
		/// </param>
		/// <returns>
		/// The caller's value limited in range to the configured number of
		/// decimal points.
		/// </returns>
		public static float ClampEpsilonDistance(float value)
		{
			float result = 0f;

			if(!float.IsNaN(value) && value != 0f)
			{
				if(value > 0)
				{
					result =
						float.Parse(value.ToString("0." +
						new string('0', mEpsilonDistance)));
				}
				else
				{
					result =
						float.Parse(value.ToString("0." +
						new string('0', mEpsilonDistance)));
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ClampEpsilonRotation																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clamp the rotation value to the configured number of decimal
		/// points.
		/// </summary>
		/// <param name="value">
		/// The value to adjust.
		/// </param>
		/// <returns>
		/// The caller's value limited in range to the configured number of
		/// decimal points.
		/// </returns>
		public static float ClampEpsilonRotation(float value)
		{
			float result = 0f;

			if(!float.IsNaN(value) && value != 0f)
			{
				if(value > 0)
				{
					result =
						float.Parse(value.ToString("0." +
						new string('0', mEpsilonRotation)));
				}
				else
				{
					result =
						float.Parse(value.ToString("0." +
						new string('0', mEpsilonRotation)));
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DegToRad																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert degrees to radians.
		/// </summary>
		/// <param name="degrees">
		/// Degrees.
		/// </param>
		/// <returns>
		/// Radians.
		/// </returns>
		public static float DegToRad(float degrees)
		{
			return degrees / 57.2957795130823f;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	EpsilonDistance																												*
		//*-----------------------------------------------------------------------*
		private static int mEpsilonDistance = 6;
		/// <summary>
		/// Get/Set the number of decimal points used in distance epsilon values.
		/// </summary>
		/// <remarks>
		/// The default value for this property is 6.
		/// </remarks>
		public static int EpsilonDistance
		{
			get { return mEpsilonDistance; }
			set { mEpsilonDistance = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	EpsilonRotation																												*
		//*-----------------------------------------------------------------------*
		private static int mEpsilonRotation = 6;
		/// <summary>
		/// Get/Set the number of decimal points used to establish the epsilon
		/// value for rotations.
		/// </summary>
		/// <remarks>
		/// The default value for this property is 6.
		/// </remarks>
		public static int EpsilonRotation
		{
			get { return mEpsilonRotation; }
			set { mEpsilonRotation = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetDestPoint																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the destination coordinate.
		/// </summary>
		/// <param name="centerX">
		/// Center x coordinate.
		/// </param>
		/// <param name="centerY">
		/// Center y coordinate.
		/// </param>
		/// <param name="angle">
		/// Angle, in radians.
		/// </param>
		/// <param name="distance">
		/// Line distance.
		/// </param>
		/// <returns>
		/// Destination coordinate, as a double precision point.
		/// </returns>
		public static FPoint GetDestPoint(float centerX, float centerY,
			float angle, float distance)
		{
			FPoint rv = new FPoint(); //	Return value.

			rv.X = centerX + (distance * (float)Math.Cos(angle));
			rv.Y = centerY + (distance * (float)Math.Sin(angle));
			return rv;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the destination coordinate.
		/// </summary>
		/// <param name="center">
		/// Center coordinates.
		/// </param>
		/// <param name="angle">
		/// The angle of the vector.
		/// </param>
		/// <param name="distance">
		/// The length of the vector.
		/// </param>
		/// <returns>
		/// The destation point of a vector from a central point, if found.
		/// Otherwise, null.
		/// </returns>
		public static FPoint GetDestPoint(FPoint center, float angle,
			float distance)
		{
			FPoint rv = null; //	Return value.

			if(center != null)
			{
				rv = new FPoint();
				rv.X = center.X + (distance * (float)Math.Cos(angle));
				rv.Y = center.Y + (distance * (float)Math.Sin(angle));
			}
			return rv;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetDestPointX																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the x coordinate of the destination point.
		/// </summary>
		/// <param name="centerX">
		/// Center x coordinate.
		/// </param>
		/// <param name="angle">
		/// Angle, in radians.
		/// </param>
		/// <param name="distance">
		/// Line distance.
		/// </param>
		/// <returns>
		/// Destination x coordinate.
		/// </returns>
		public static float GetDestPointX(float centerX,
			float angle, float distance)
		{
			return centerX + (distance * (float)Math.Cos(angle));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetDestPointY																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the y coordinate of the destination point.
		/// </summary>
		/// <param name="centerY">
		/// Center y coordinate.
		/// </param>
		/// <param name="angle">
		/// Angle, in radians.
		/// </param>
		/// <param name="distance">
		/// Line distance.
		/// </param>
		/// <returns>
		/// Destination y coordinate.
		/// </returns>
		public static float GetDestPointY(float centerY,
			float angle, float distance)
		{
			return centerY + (distance * (float)Math.Sin(angle));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetDirection																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the direction of the line.
		/// </summary>
		/// <param name="startPoint">
		/// Start coordinates.
		/// </param>
		/// <param name="endPoint">
		/// End coordinates.
		/// </param>
		/// <param name="drawingSpace">
		/// The drawing space for which the result is being prepared.
		/// Default = Display.
		/// </param>
		/// <returns>
		/// Direction mask with 8 point resolution.
		/// </returns>
		public static DirectionEnum GetDirection(FPoint startPoint,
			FPoint endPoint,
			DrawingSpaceEnum drawingSpace = DrawingSpaceEnum.Display)
		{
			DirectionEnum result = DirectionEnum.None;

			switch(drawingSpace)
			{
				case DrawingSpaceEnum.Cartesian:
				case DrawingSpaceEnum.Display:
					if(endPoint.X > startPoint.X)
					{
						result |= DirectionEnum.East;
					}
					else if(endPoint.X < startPoint.X)
					{
						result |= DirectionEnum.West;
					}
					break;
			}
			switch(drawingSpace)
			{
				case DrawingSpaceEnum.Cartesian:
					if(endPoint.Y > startPoint.Y)
					{
						result |= DirectionEnum.North;
					}
					else if(endPoint.Y < startPoint.Y)
					{
						result |= DirectionEnum.South;
					}
					break;
				case DrawingSpaceEnum.Display:
					if(endPoint.Y > startPoint.Y)
					{
						result |= DirectionEnum.South;
					}
					else if(endPoint.Y < startPoint.Y)
					{
						result |= DirectionEnum.North;
					}
					break;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetInsideParallelLine																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the line parallel to the caller's, located to the inside of the
		/// edge.
		/// </summary>
		/// <param name="pointA">
		/// First point.
		/// </param>
		/// <param name="pointB">
		/// Second point.
		/// </param>
		/// <param name="orientation">
		/// Path orientation, relative to a natural arc.
		/// </param>
		/// <param name="thickness">
		/// Thickness of the parallel area.
		/// </param>
		/// <returns>
		/// Reference to a line parallel to and inside of the caller's specified
		/// line, with reference to the specified thickness.
		/// </returns>
		public static FLine GetInsideParallelLine(FPoint pointA, FPoint pointB,
			ArcDirectionEnum orientation, float thickness)
		{
			float angle = 0f;
			FLine result = new FLine();
			float tangent = 0f;

			if(pointA != null && pointB != null)
			{
				if(thickness == 0)
				{
					//	If the parallel area is zero, then source line is target line.
					FPoint.TransferValues(pointA, result.PointA);
					FPoint.TransferValues(pointB, result.PointB);
				}
				else
				{
					angle = GetLineAngle(pointA, pointB);
					if(orientation == ArcDirectionEnum.Increasing)
					{
						tangent = angle + (float)(0.5 * Math.PI);
					}
					else
					{
						tangent = angle - (float)(0.5 * Math.PI);
					}
					if(tangent < 0)
					{
						tangent += (float)(2 * Math.PI);
					}
					else if(tangent > (2 * Math.PI))
					{
						tangent -= (float)(2 * Math.PI);
					}
					result.PointA.X =
						pointA.X + GetLineAdjFromAngHyp(tangent, thickness);
					result.PointA.Y =
						pointA.Y + GetLineOppFromAngHyp(tangent, thickness);
					result.PointB.X =
						pointB.X + GetLineAdjFromAngHyp(tangent, thickness);
					result.PointB.Y =
						pointB.Y + GetLineOppFromAngHyp(tangent, thickness);
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the line parallel to the caller's, located to the inside of the
		/// edge.
		/// </summary>
		/// <param name="pointA">
		/// First point.
		/// </param>
		/// <param name="pointB">
		/// Second point.
		/// </param>
		/// <param name="thickness">
		/// Thickness of the parallel area.
		/// </param>
		/// <param name="orientation">
		/// Path orientation, relative to the specified drawing space.
		/// </param>
		/// <param name="drawingSpace">
		/// Drawing space for which the results are being prepared.
		/// Default = Display.
		/// </param>
		/// <returns>
		/// Reference to a line parallel to and inside of the caller's specified
		/// line, with reference to the specified thickness.
		/// </returns>
		public static FLine GetInsideParallelLine(FPoint pointA, FPoint pointB,
			float thickness, WindingOrientationEnum orientation,
			DrawingSpaceEnum drawingSpace = DrawingSpaceEnum.Display)
		{
			FLine result = null;

			switch(orientation)
			{
				case WindingOrientationEnum.Clockwise:
					switch(drawingSpace)
					{
						case DrawingSpaceEnum.Cartesian:
							result = GetInsideParallelLine(pointA, pointB,
								ArcDirectionEnum.Reverse, thickness);
							break;
						case DrawingSpaceEnum.Display:
							result = GetInsideParallelLine(pointA, pointB,
								ArcDirectionEnum.Forward, thickness);
							break;
					}
					break;
				case WindingOrientationEnum.CounterClockwise:
					switch(drawingSpace)
					{
						case DrawingSpaceEnum.Cartesian:
							result = GetInsideParallelLine(pointA, pointB,
								ArcDirectionEnum.Forward, thickness);
							break;
						case DrawingSpaceEnum.Display:
							result = GetInsideParallelLine(pointA, pointB,
								ArcDirectionEnum.Reverse, thickness);
							break;
					}
					break;
			}
			if(result == null)
			{
				result = new FLine();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetLineAdjFromAngHyp																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the length of the adjacent line from the angle and hypotenuse
		/// length.
		/// </summary>
		/// <param name="angle">
		/// Angle, in radians.
		/// </param>
		/// <param name="hypotenuse">
		/// Hypotenuse length.
		/// </param>
		/// <returns>
		/// Length of adjacent line.
		/// </returns>
		public static float GetLineAdjFromAngHyp(float angle, float hypotenuse)
		{
			//	cos(angle) = adjacent / hypotenuse
			//	hypotenuse = adjacent / cos(angle)
			//	SOH CAH TOA
			//		Sin(a) = Opposite / Hypotenuse
			//		Cos(a) = Adjacent / Hypotenuse
			//		Tan(a) = Opposite / Adjacent
			return (float)Math.Cos(angle) * hypotenuse;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetLineAdjFromAngOpp																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the length of the adjacent line from the angle and opposite
		/// length.
		/// </summary>
		/// <param name="angle">
		/// Angle, in radians.
		/// </param>
		/// <param name="opposite">
		/// Length of opposite line.
		/// </param>
		/// <returns>
		/// Length of adjacent line.
		/// </returns>
		public static float GetLineAdjFromAngOpp(float angle, float opposite)
		{
			//	cos(angle) = adjacent / hypotenuse
			//	hypotenuse = adjacent / cos(angle)
			//	SOH CAH TOA
			//		Sin(a) = Opposite / Hypotenuse
			//		Cos(a) = Adjacent / Hypotenuse
			//		Tan(a) = Opposite / Adjacent
			double result = 0.0;
			double tan = Math.Tan(angle);

			if(tan != 0.0)
			{
				result = opposite / tan;
			}
			return (float)result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetLineAdjFromHypOpp																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the adjacent line from hypotenuse and opposite sides.
		/// </summary>
		/// <param name="hypotenuse">
		/// Length of hypotenuse.
		/// </param>
		/// <param name="opposite">
		/// Length of opposite.
		/// </param>
		/// <returns>
		/// Length of adjacent side.
		/// </returns>
		public static float GetLineAdjFromHypOpp(float hypotenuse,
			float opposite)
		{
			//	B<sup>2</sup> = A<sup>2</sup> - C<sup>2</sup>
			return
				(float)(Math.Sqrt(Math.Pow(hypotenuse, 2) - Math.Pow(opposite, 2)));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetLineAngFromAdjOpp																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the angle from the adjacent and opposite lengths.
		/// </summary>
		/// <param name="adjacent">
		/// Length of adjacent line.
		/// </param>
		/// <param name="opposite">
		/// Length of opposite line.
		/// </param>
		/// <returns>
		/// Angle, in radians.
		/// </returns>
		public static float GetLineAngFromAdjOpp(float adjacent, float opposite)
		{
			//	cos(angle) = adjacent / hypotenuse
			//	hypotenuse = adjacent / cos(angle)
			//	SOH CAH TOA
			//		Sin(a) = Opposite / Hypotenuse
			//		Cos(a) = Adjacent / Hypotenuse
			//		Tan(a) = Opposite / Adjacent
			double result = 0.0;

			if(adjacent != 0.0)
			{
				result = Math.Atan(opposite / adjacent);
			}
			return (float)result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetLineAngle																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the angle of the specified line, in radians.
		/// </summary>
		/// <param name="x1">
		/// First point x coordinate.
		/// </param>
		/// <param name="y1">
		/// First point y coordinate.
		/// </param>
		/// <param name="x2">
		/// Second point x coordinate.
		/// </param>
		/// <param name="y2">
		/// Second point y coordinate.
		/// </param>
		/// <returns>
		/// Angle of the line, in radians.
		/// </returns>
		public static float GetLineAngle(float x1, float y1,
			float x2, float y2)
		{
			double rad = 0;   //	Radians.

			if(x1 == x2)
			{
				if(y2 > y1)
				{
					rad = Math.PI * 0.5;
				}
				else
				{
					rad = Math.PI * 1.5;
				}
			}
			else
			{
				rad = Math.Atan((y2 - y1) / (x2 - x1));
				if(x2 < x1)
				{
					rad += Math.PI;
				}
			}
			return (float)rad;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the angle of the specified line, in radians.
		/// </summary>
		/// <param name="line">
		/// Reference to a double-precision line.
		/// </param>
		/// <returns>
		/// Angle of the line, in radians.
		/// </returns>
		public static float GetLineAngle(FLine line)
		{
			return GetLineAngle(line.PointA.X, line.PointA.Y,
				line.PointB.X, line.PointB.Y);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the angle of the specified line, in radians.
		/// </summary>
		/// <param name="vertex1">
		/// First point x coordinate.
		/// </param>
		/// <param name="vertex2">
		/// Second point x coordinate.
		/// </param>
		/// <returns>
		/// Angle of the line, in radians.
		/// </returns>
		public static float GetLineAngle(FPoint vertex1, FPoint vertex2)
		{
			return GetLineAngle(vertex1.X, vertex1.Y, vertex2.X, vertex2.Y);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetLineDistance																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the line distance between two points.
		/// </summary>
		/// <param name="x1">
		/// First point x coordinate.
		/// </param>
		/// <param name="y1">
		/// First point y coordinate.
		/// </param>
		/// <param name="x2">
		/// Second point x coordinate.
		/// </param>
		/// <param name="y2">
		/// Second point y coordinate.
		/// </param>
		/// <returns>
		/// Absolute distance between two points.
		/// </returns>
		public static float GetLineDistance(float x1, float y1,
			float x2, float y2)
		{
			return
				(float)(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2)));
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the line distance between two points.
		/// </summary>
		/// <param name="x1">
		/// First point x coordinate.
		/// </param>
		/// <param name="y1">
		/// First point y coordinate.
		/// </param>
		/// <param name="z1">
		/// First point z coordinate.
		/// </param>
		/// <param name="x2">
		/// Second point x coordinate.
		/// </param>
		/// <param name="y2">
		/// Second point y coordinate.
		/// </param>
		/// <param name="z2">
		/// Second point z coordinate.
		/// </param>
		/// <returns>
		/// Absolute distance between two points.
		/// </returns>
		public static float GetLineDistance(float x1, float y1, float z1,
			float x2, float y2, float z2)
		{
			return (float)(Math.Sqrt(
				Math.Pow((x2 - x1), 2) +
				Math.Pow((y2 - y1), 2) +
				Math.Pow((z2 - z1), 2)));
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the line distance between two points.
		/// </summary>
		/// <param name="line">
		/// Reference to a line to measure.
		/// </param>
		/// <returns>
		/// Absolute distance between two points.
		/// </returns>
		public static float GetLineDistance(FLine line)
		{
			return GetLineDistance(line.PointA.X, line.PointA.Y,
				line.PointB.X, line.PointB.Y);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the line distance between two points.
		/// </summary>
		/// <param name="vertex1">
		/// First point.
		/// </param>
		/// <param name="vertex2">
		/// Second point.
		/// </param>
		/// <returns>
		/// Absolute distance between two points.
		/// </returns>
		public static float GetLineDistance(FPoint vertex1, FPoint vertex2)
		{
			return GetLineDistance(vertex1.X, vertex1.Y, vertex2.X, vertex2.Y);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetLineHypFromAdjOpp																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the hypotenuse length from adjacent and opposite sides.
		/// </summary>
		/// <param name="adjacent">
		/// Length of the adjacent side.
		/// </param>
		/// <param name="opposite">
		/// Length of the opposite side.
		/// </param>
		/// <returns>
		/// Length of the hypotenuse.
		/// </returns>
		public static float GetLineHypFromAdjOpp(float adjacent, float opposite)
		{
			//	A<sup>2</sup> = B<sup>2</sup> + C<sup>2</sup>
			return
				(float)(Math.Sqrt(Math.Pow(adjacent, 2) + Math.Pow(opposite, 2)));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetLineHypFromAngAdj																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the length of the hypotenuse from the angle and adjacent length.
		/// </summary>
		/// <param name="angle">
		/// Angle, in radians.
		/// </param>
		/// <param name="adjacent">
		/// Length of the adjacent line.
		/// </param>
		/// <returns>
		/// Length of hypotenuse.
		/// </returns>
		public static float GetLineHypFromAngAdj(float angle, float adjacent)
		{
			//	cos(angle) = adjacent / hypotenuse
			//	hypotenuse = adjacent / cos(angle)
			//	SOH CAH TOA
			//		Sin(a) = Opposite / Hypotenuse
			//		Cos(a) = Adjacent / Hypotenuse
			//		Tan(a) = Opposite / Adjacent
			double cos = Math.Cos(angle);
			double result = 0.0;

			if(cos != 0.0)
			{
				result = adjacent / cos;
			}
			return (float)result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetLineHypFromAngOpp																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the length of the hypotenuse from the angle and opposite length.
		/// </summary>
		/// <param name="angle">
		/// Angle, in radians.
		/// </param>
		/// <param name="opposite">
		/// Length of the opposide side.
		/// </param>
		/// <returns>
		/// Length of hypotenuse.
		/// </returns>
		public static float GetLineHypFromAngOpp(float angle, float opposite)
		{
			//	cos(angle) = adjacent / hypotenuse
			//	hypotenuse = adjacent / cos(angle)
			//	SOH CAH TOA
			//		Sin(a) = Opposite / Hypotenuse
			//		Cos(a) = Adjacent / Hypotenuse
			//		Tan(a) = Opposite / Adjacent
			double result = 0.0;
			double sin = Math.Sin(angle);

			if(sin != 0.0)
			{
				result = opposite / sin;
			}
			return (float)result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetLineOppFromAdjHyp																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the length of the opposite line from adjacent and hypotenuse
		/// lengths.
		/// </summary>
		/// <param name="adjacent">
		/// Length of adjacent side.
		/// </param>
		/// <param name="hypotenuse">
		/// Length of hypotenuse.
		/// </param>
		/// <returns>
		/// Length of the opposite line.
		/// </returns>
		public static float GetLineOppFromAdjHyp(float adjacent,
			float hypotenuse)
		{
			//	C<sup>2</sup> = A<sup>2</sup> - B<sup>2</sup>
			return
				(float)(Math.Sqrt(Math.Pow(hypotenuse, 2) - Math.Pow(adjacent, 2)));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetLineOppFromAngAdj																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the length of the opposite line from the angle and adjacent
		/// length.
		/// </summary>
		/// <param name="angle">
		/// Angle, in radians.
		/// </param>
		/// <param name="adjacent">
		/// Length of the adjacent line.
		/// </param>
		/// <returns>
		/// Length of the opposite line.
		/// </returns>
		public static float GetLineOppFromAngAdj(float angle, float adjacent)
		{
			//	tan(angle) = opposite / adjacent
			//	opposite = tan(angle) * adjacent
			//	SOH CAH TOA
			//		Sin(a) = Opposite / Hypotenuse
			//		Cos(a) = Adjacent / Hypotenuse
			//		Tan(a) = Opposite / Adjacent
			return (float)(Math.Tan(angle) * adjacent);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetLineOppFromAngHyp																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the length of the opposite line from the angle and hypotenuse
		/// length.
		/// </summary>
		/// <param name="angle">
		/// Angle, in radians.
		/// </param>
		/// <param name="hypotenuse">
		/// Length of the hypotenuse.
		/// </param>
		/// <returns>
		/// Length of the opposite line.
		/// </returns>
		public static float GetLineOppFromAngHyp(float angle, float hypotenuse)
		{
			//	cos(angle) = adjacent / hypotenuse
			//	hypotenuse = adjacent / cos(angle)
			//	SOH CAH TOA
			//		Sin(a) = Opposite / Hypotenuse
			//		Cos(a) = Adjacent / Hypotenuse
			//		Tan(a) = Opposite / Adjacent
			return (float)(Math.Sin(angle) * hypotenuse);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetPathOrientation																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the direction of point progression within the shape, in terms
		/// of whether the angle is increasing or decreasing in angle relative to
		/// a natural arc.
		/// </summary>
		/// <param name="path">
		/// Reference to the collection of points for which a winding orientation
		/// will be found.
		/// </param>
		/// <returns>
		/// Path orientation of the shape, relative to the angular progression of
		/// a natural arc.
		/// </returns>
		/// <remarks>
		/// <para>
		/// This method is compatible with either Display or Cartesian space.
		/// </para>
		/// <para>
		/// Zero points or a single point in the collection will result
		/// in an orientation of None.
		/// </para>
		/// </remarks>
		public static ArcDirectionEnum GetPathOrientation(
			List<FPoint> path)
		{
			int count = 0;
			int index = 0;
			FPoint point1 = null;
			FPoint point2 = null;
			ArcDirectionEnum result = ArcDirectionEnum.None;
			List<double> tallies = new List<double>();
			double total = 0d;

			//	Sum of edges (x2 - x1) * (y2 + y1)
			if(path?.Count > 1)
			{
				count = path.Count;
				for(index = 0; index < count; index++)
				{
					if(index + 1 < count)
					{
						//	This point and the next.
						point1 = path[index];
						point2 = path[index + 1];
					}
					else
					{
						//	This point and the first.
						point1 = path[index];
						point2 = path[0];
					}
					tallies.Add((point2.X - point1.X) * (point2.Y + point1.Y));
				}
				total = tallies.Sum();
				if(total >= 0)
				{
					result = ArcDirectionEnum.Increasing;
				}
				else
				{
					result = ArcDirectionEnum.Decreasing;
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the direction of point progression within the shape, in terms
		/// of clockwise or counterclockwise rotation.
		/// </summary>
		/// <param name="path">
		/// Reference to the collection of points for which a winding orientation
		/// will be found.
		/// </param>
		/// <param name="drawingSpace">
		/// The drawing space for which the results are being prepared.
		/// Default = Display.
		/// </param>
		/// <returns>
		/// Path orientation of the shape, relative to the angular progression of
		/// a natural arc.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Zero points or a single point in the collection will result
		/// in an orientation of None.
		/// </para>
		/// </remarks>
		public static WindingOrientationEnum GetPathOrientation(
			List<FPoint> path,
			DrawingSpaceEnum drawingSpace = DrawingSpaceEnum.Display)
		{
			ArcDirectionEnum natural = GetPathOrientation(path);
			WindingOrientationEnum result = WindingOrientationEnum.None;

			switch(drawingSpace)
			{
				case DrawingSpaceEnum.Cartesian:
					switch(natural)
					{
						case ArcDirectionEnum.Forward:
							result = WindingOrientationEnum.CounterClockwise;
							break;
						case ArcDirectionEnum.Reverse:
							result = WindingOrientationEnum.Clockwise;
							break;
					}
					break;
				case DrawingSpaceEnum.Display:
					switch(natural)
					{
						case ArcDirectionEnum.Forward:
							result = WindingOrientationEnum.Clockwise;
							break;
						case ArcDirectionEnum.Reverse:
							result = WindingOrientationEnum.CounterClockwise;
							break;
					}
					break;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NormalizeRad																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Normalize the number of radians to a single turn.
		/// </summary>
		/// <param name="angle">
		/// Angle, in radians.
		/// </param>
		/// <returns>
		/// Angle, in radians, between 0 and 2pi.
		/// </returns>
		public static float NormalizeRad(float angle)
		{
			double result = angle;
			if(angle >= 0)
			{
				//	Positive.
				while(result > (Math.PI * 2))
				{
					result -= (Math.PI * 2);
				}
			}
			else
			{
				//	Negative.
				while(result < 0)
				{
					result += (Math.PI * 2);
				}
			}
			return (float)result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RadToDeg																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the degrees equivalent of the specified radian value.
		/// </summary>
		/// <param name="radians">
		/// Radians to convert.
		/// </param>
		/// <returns>
		/// Degrees equivalent of the specified radians.
		/// </returns>
		public static float RadToDeg(float radians)
		{
			return radians * 57.2957795130823f;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ReduceRad																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Reduce the number of radians to a single turn.
		/// </summary>
		/// <param name="angle">
		/// Angle, in radians.
		/// </param>
		/// <returns>
		/// Angle, in radians, between 0 and 2pi.
		/// </returns>
		[Obsolete("ReduceRad is depreciated, please use NormalizeRad instead.")]
		public static float ReduceRad(float angle)
		{
			return NormalizeRad(angle);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SegmentCurve																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Segment a curve into individual lines.
		/// </summary>
		/// <param name="centerPoint">
		/// Coordinates of the center of the curve.
		/// </param>
		/// <param name="radius">
		/// Radius of the curve.
		/// </param>
		/// <param name="startAngle">
		/// Angle at which the curve starts, in degrees.
		/// </param>
		/// <param name="sweepAngle">
		/// Sweep of the curve, in degrees, in counter-clockwise motion.
		/// If negative, the sweep is clockwise.
		/// </param>
		/// <param name="stepAngle">
		/// Angle of each generated segment from the last, in degrees.
		/// </param>
		/// <returns>
		/// Collection of points, including the start and end.
		/// </returns>
		/// <remarks>
		/// All increasing sweeps are counted in incrementing steps while
		/// all decreasing sweeps are counted in decrementing steps.
		/// If you need for the ray to pass through the 0 point during
		/// generation, as in 350 to 10 in a decreasing direction, or 10 to 350 in
		/// an increasing direction, then set the destination angle higher than
		/// 360 or less than 0, as appropriate for the direction of cast,
		/// respectively.
		/// </remarks>
		public static List<FPoint> SegmentCurve(FPoint centerPoint,
			float radius, float startAngle, float sweepAngle, float stepAngle)
		{
			FPoint currentPoint = null;
			float currentRad = 0;
			FPoint endPoint = new FPoint(0, 0);
			List<FPoint> points = new List<FPoint>();
			float startRad = DegToRad(startAngle);
			float stepRad = DegToRad(stepAngle);
			float stopAngle = startAngle + sweepAngle;
			float stopRad = DegToRad(stopAngle);
			float sweepRad = DegToRad(sweepAngle);

			if(sweepRad >= 0)
			{
				//	Increasing.
				for(currentRad = startRad;
					currentRad < stopRad;
					currentRad += stepRad)
				{
					currentPoint = new FPoint(
						centerPoint.X + GetLineAdjFromAngHyp(currentRad, radius),
						centerPoint.Y + GetLineOppFromAngHyp(currentRad, radius)
					);
					points.Add(currentPoint);
				}
			}
			else
			{
				//	Decreasing.
				for(currentRad = startRad;
					currentRad > stopRad;
					currentRad -= stepRad)
				{
					currentPoint = new FPoint(
						centerPoint.X + GetLineAdjFromAngHyp(currentRad, radius),
						centerPoint.Y + GetLineOppFromAngHyp(currentRad, radius)
					);
					points.Add(currentPoint);
				}
			}

			//	Stop point.
			endPoint.X = centerPoint.X + GetLineAdjFromAngHyp(stopRad, radius);
			endPoint.Y = centerPoint.Y + GetLineOppFromAngHyp(stopRad, radius);
			points.Add(endPoint);
			return points;
		}
		//*-----------------------------------------------------------------------*



	}
	//*-------------------------------------------------------------------------*
}

