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
	//*	Bezier																																	*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Functionality and features for working with Bezier curves.
	/// </summary>
	public class Bezier
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		/// <summary>
		/// The sample multiplier for smoothing on equidistant approximations of
		/// Bezier curves. Set this value prior to compilation to balance between
		/// precision and speed. Minimum recommended value is 2.
		/// </summary>
		private static readonly int mEquidistantSampleMultiplier = 10;

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* GetCubicBoundingBox																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the bounding box area for the supplied cubic Bezier curve.
		/// </summary>
		/// <param name="p0">
		/// The starting point.
		/// </param>
		/// <param name="p1">
		/// Control point 1.
		/// </param>
		/// <param name="p2">
		/// Control point 2.
		/// </param>
		/// <param name="p3">
		/// The ending point.
		/// </param>
		/// <param name="sampleCount">
		/// Count of samples to take when generating the discrete curve edge.
		/// </param>
		/// <returns>
		/// Reference to the bounding box area of the provided cubic Bezier
		/// curve, if legitimate. Otherwise, an empty rectangle.
		/// </returns>
		public static FArea GetCubicBoundingBox(FVector2 p0,
			FVector2 p1, FVector2 p2, FVector2 p3, int sampleCount)
		{
			float index = 0f;
			float count = 0f;
			FArea result = new FArea();
			List<FVector2> samples = null;
			float t = 0f;

			if(p0 != null && p1 != null && p2 != null && p3 != null &&
				sampleCount > 3)
			{
				samples = new List<FVector2>();
				count = sampleCount;
				for(index = 0f; index < count; index ++)
				{
					t = index / count;
					samples.Add(GetCubicCurvePoint(p0, p1, p2, p3, t));
				}
				result.X = samples.Min(x => x.X);
				result.Y = samples.Min(y => y.Y);
				result.Right = samples.Max(x => x.X);
				result.Bottom = samples.Max(y => y.Y);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetCubicCurvePoint																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the natural point along a cubic Bezier curve as indicated by t.
		/// </summary>
		/// <param name="p0">
		/// Starting point.
		/// </param>
		/// <param name="p1">
		/// Control point 1.
		/// </param>
		/// <param name="p2">
		/// Control point 2.
		/// </param>
		/// <param name="p3">
		/// Ending point.
		/// </param>
		/// <param name="t">
		/// Current progress, in the range between 0 and 1.
		/// </param>
		/// <returns>
		/// The point along a cubic bezier curve that is indicated by t.
		/// </returns>
		public static FVector2 GetCubicCurvePoint(FVector2 p0,
			FVector2 p1, FVector2 p2, FVector2 p3, float t)
		{
			FVector2 result = null;
			FVector2 v1 = null;
			FVector2 v2 = null;

			if(p0 != null && p1 != null && p2 != null && p3 != null)
			{
				v1 = GetQuadraticCurvePoint(p0, p1, p2, t);
				v2 = GetQuadraticCurvePoint(p1, p2, p3, t);
				result = Linear.Lerp(v1, v2, t);
			}
			if(result == null)
			{
				result = new FVector2();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetCubicCurvePointsEquidistant																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a collection of equidistantly spaced points along a cubic
		/// Bezier curve.
		/// </summary>
		/// <param name="p0">
		/// Starting point.
		/// </param>
		/// <param name="p1">
		/// Control point 1.
		/// </param>
		/// <param name="p2">
		/// Control point 2.
		/// </param>
		/// <param name="p3">
		/// Ending point.
		/// </param>
		/// <param name="count">
		/// Count of points to sample on the curve.
		/// </param>
		/// <returns>
		/// Reference to a collection of equidistantly spaced points
		/// along the specified cubic curve that approximate the curve's
		/// natural edge.
		/// </returns>
		/// <remarks>
		/// In this version, all segments except the last will be of exactly
		/// the same length and the last segment will absorb any minor discrepancy
		/// between the next-to-last point and the ending point.
		/// </remarks>
		public static List<FVector2> GetCubicCurvePointsEquidistant(FVector2 p0,
			FVector2 p1, FVector2 p2, FVector2 p3, int count)
		{
			float angle = 0f;
			int index = 0;
			float lengthSample = 0f;
			float lengthSegment = 0f;
			float lengthTotal = 0f;
			FVector2 pointCurrent = null;
			FVector2 pointLast = null;
			FVector2 pointNext = null;
			List<FVector2> result = new List<FVector2>();
			int sampleCount = 0;
			List<FVector2> samples = new List<FVector2>();
			float time = 0f;

			if(p0 != null && p1 != null && p2 != null && count > 3)
			{
				//	Get 10x the sample points from the natural curve.
				sampleCount = count * mEquidistantSampleMultiplier;
				for(index = 0; index <= sampleCount; index++)
				{
					time = (float)index / (float)sampleCount;
					pointCurrent = GetCubicCurvePoint(p0, p1, p2, p3, time);
					samples.Add(pointCurrent);
					if(pointLast != null)
					{
						//	Calculate total length.
						lengthTotal += Trig.GetLineDistance(pointCurrent, pointLast);
					}
					pointLast = pointCurrent;
				}
				//	Reduce the number of operations for looping on samples.
				sampleCount++;
				//	Calculate line segment length.
				lengthSegment = lengthTotal / (float)count;
				//	The first segment begins at the starting point.
				pointCurrent = p0;
				result.Add(pointCurrent);
				for(index = 1; index < sampleCount; index++)
				{
					pointNext = samples[index];
					// Find the next sample point equal to or further than the segment
					// length.
					lengthSample = Trig.GetLineDistance(pointNext, pointCurrent);
					if(lengthSample >= lengthSegment || index + 1 == sampleCount)
					{
						// Set the angle of the current segment to the next point.
						angle = Trig.GetLineAngle(pointCurrent, pointNext);
						// Jump to the end of the current segment as new reference point.
						pointCurrent =
								Trig.GetDestPoint(pointCurrent, angle, lengthSegment);
						result.Add(pointCurrent);
					}
				}
				//	The stopping point is always the last point in the chain.
				if(result.Count > 1)
				{
					result[result.Count - 1] = p3;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetLinearBoundingBox																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the bounding box area for the supplied linear Bezier curve,
		/// aka a straight line.
		/// </summary>
		/// <param name="p0">
		/// Reference to the starting point.
		/// </param>
		/// <param name="p1">
		/// Reference to the ending point.
		/// </param>
		/// <returns>
		/// Reference to the bounding box area of the provided linear Bezier
		/// curve, if legitimate. Otherwise, an empty rectangle.
		/// </returns>
		public static FArea GetLinearBoundingBox(FVector2 p0, FVector2 p1)
		{
			FArea result = new FArea();
			List<FVector2> samples = null;

			if(p0 != null && p1 != null)
			{
				samples.Add(p0);
				samples.Add(p1);
				result.X = samples.Min(x => x.X);
				result.Y = samples.Min(y => y.Y);
				result.Right = samples.Max(x => x.X);
				result.Bottom = samples.Max(y => y.Y);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetLinearCurvePoint																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the point along a linear Bezier curve, otherwise known as a
		/// straight line, as indicated by t.
		/// </summary>
		/// <param name="p0">
		/// Starting point.
		/// </param>
		/// <param name="p1">
		/// Ending point.
		/// </param>
		/// <param name="t">
		/// Current progress, in the range between 0 and 1.
		/// </param>
		/// <returns>
		/// The point along a linear bezier curve that is indicated by t.
		/// </returns>
		public static FVector2 GetLinearCurvePoint(FVector2 p0, FVector2 p1,
			float t)
		{
			FVector2 result = null;

			if(p0 != null && p1 != null)
			{
				result = Linear.Lerp(p0, p1, t);
			}
			if(result == null)
			{
				result = new FVector2();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetLinearCurvePointsEquidistant																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a collection of equidistantly spaced points along a linear
		/// Bezier curve, which is identical to the set of points along a natural
		/// linear curve.
		/// </summary>
		/// <param name="p0">
		/// Starting point.
		/// </param>
		/// <param name="p1">
		/// Ending point.
		/// </param>
		/// <param name="count">
		/// Count of points to sample on the curve.
		/// </param>
		/// <returns>
		/// Reference to a collection of equidistantly spaced points
		/// along the specified linear curve (line).
		/// </returns>
		public static List<FVector2> GetLinearCurvePointsEquidistant(FVector2 p0,
			FVector2 p1, int count)
		{
			int index = 0;
			List<FVector2> result = new List<FVector2>();
			float time = 0f;

			if(p0 != null && p1 != null && count > 3)
			{
				for(index = 0; index <= count; index++)
				{
					time = (float)index / (float)count;
					result.Add(GetLinearCurvePoint(p0, p1, time));
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetQuadraticBoundingBox																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the bounding box area for the supplied quadratic Bezier curve.
		/// </summary>
		/// <param name="p0">
		/// The starting point.
		/// </param>
		/// <param name="p1">
		/// Control point 1.
		/// </param>
		/// <param name="p2">
		/// Reference to the ending point.
		/// </param>
		/// <param name="sampleCount">
		/// Count of samples to take when generating the discrete curve edge.
		/// </param>
		/// <returns>
		/// Reference to the bounding box area of the provided quadratic Bezier
		/// curve, if legitimate. Otherwise, an empty rectangle.
		/// </returns>
		public static FArea GetQuadraticBoundingBox(FVector2 p0,
			FVector2 p1, FVector2 p2, int sampleCount)
		{
			float index = 0f;
			float count = 0f;
			FArea result = new FArea();
			List<FVector2> samples = null;
			float t = 0f;

			if(p0 != null && p1 != null && p2 != null &&
				sampleCount > 3)
			{
				samples = new List<FVector2>();
				count = sampleCount;
				for(index = 0f; index < count; index++)
				{
					t = index / count;
					samples.Add(GetQuadraticCurvePoint(p0, p1, p2, t));
				}
				result.X = samples.Min(x => x.X);
				result.Y = samples.Min(y => y.Y);
				result.Right = samples.Max(x => x.X);
				result.Bottom = samples.Max(y => y.Y);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetQuadraticCurvePoint																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the point along a cubic Bezier curve as indicated by t.
		/// </summary>
		/// <param name="p0">
		/// Starting point.
		/// </param>
		/// <param name="p1">
		/// Control point 1.
		/// </param>
		/// <param name="p2">
		/// Ending point.
		/// </param>
		/// <param name="t">
		/// Current progress, in the range between 0 and 1.
		/// </param>
		/// <returns>
		/// The point along a cubic Bezier curve that is indicated by t.
		/// </returns>
		public static FVector2 GetQuadraticCurvePoint(FVector2 p0,
			FVector2 p1, FVector2 p2, float t)
		{
			FVector2 result = null;
			FVector2 xy1 = null;
			FVector2 xy2 = null;

			if(p0 != null && p1 != null && p2 != null)
			{
				xy1 = Linear.Lerp(p0, p1, t);
				xy2 = Linear.Lerp(p1, p2, t);
				result = Linear.Lerp(xy1, xy2, t);
			}
			if(result == null)
			{
				result = new FVector2();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetQuadraticCurvePointsEquidistant																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a collection of equidistantly spaced points along a quadratic
		/// Bezier curve.
		/// </summary>
		/// <param name="p0">
		/// Starting point.
		/// </param>
		/// <param name="p1">
		/// Control point 1.
		/// </param>
		/// <param name="p2">
		/// Ending point.
		/// </param>
		/// <param name="count">
		/// Count of points to sample on the curve.
		/// </param>
		/// <returns>
		/// Reference to a collection of equidistantly spaced points
		/// along the specified quadratic curve that approximate the curve's
		/// natural edge.
		/// </returns>
		/// <remarks>
		/// In this version, all segments except the last will be of exactly
		/// the same length and the last segment will absorb any minor discrepancy
		/// between the next-to-last point and the ending point.
		/// </remarks>
		public static List<FVector2> GetQuadraticCurvePointsEquidistant(
			FVector2 p0, FVector2 p1, FVector2 p2, int count)
		{
			float angle = 0f;
			int index = 0;
			float lengthSample = 0f;
			float lengthSegment = 0f;
			float lengthTotal = 0f;
			FVector2 pointCurrent = null;
			FVector2 pointLast = null;
			FVector2 pointNext = null;
			List<FVector2> result = new List<FVector2>();
			int sampleCount = 0;
			List<FVector2> samples = new List<FVector2>();
			float time = 0f;

			if(p0 != null && p1 != null && p2 != null && count > 3)
			{
				//	Get 10x the sample points from the natural curve.
				sampleCount = count * mEquidistantSampleMultiplier;
				for(index = 0; index <= sampleCount; index ++)
				{
					time = (float)index / (float)sampleCount;
					pointCurrent = GetQuadraticCurvePoint(p0, p1, p2, time);
					samples.Add(pointCurrent);
					if(pointLast != null)
					{
						//	Calculate total length.
						lengthTotal += Trig.GetLineDistance(pointCurrent, pointLast);
					}
					pointLast = pointCurrent;
				}
				//	Reduce the number of operations for looping on samples.
				sampleCount++;
				//	Calculate line segment length.
				lengthSegment = lengthTotal / (float)count;
				//	The first segment begins at the starting point.
				pointCurrent = p0;
				result.Add(pointCurrent);
				for(index = 1; index < sampleCount; index ++)
				{
					pointNext = samples[index];
					// Find the next sample point equal to or further than the segment
					// length.
					lengthSample = Trig.GetLineDistance(pointNext, pointCurrent);
					if(lengthSample >= lengthSegment || index + 1 == sampleCount)
					{
						// Set the angle of the current segment to the next point.
						angle = Trig.GetLineAngle(pointCurrent, pointNext);
						// Jump to the end of the current segment as new reference point.
						pointCurrent =
							Trig.GetDestPoint(pointCurrent, angle, lengthSegment);
						result.Add(pointCurrent);
					}
				}
				//	The stopping point is always the last point in the chain.
				if(result.Count > 1)
				{
					result[result.Count - 1] = p2;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}


