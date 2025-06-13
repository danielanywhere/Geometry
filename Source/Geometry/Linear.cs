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

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	Linear																																	*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Functionality and utility procedures for working with Linear Geometry.
	/// </summary>
	public class Linear
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
		//* GetLinePoint																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the point on the line relative to a decimal portion of the
		/// distance t between point 0.0 and point 1.0.
		/// </summary>
		/// <param name="point0">
		/// Reference to the first line point.
		/// </param>
		/// <param name="point1">
		/// Reference to the second line point.
		/// </param>
		/// <param name="t">
		/// The decimal portion along the line upon which the return point
		/// rests, relative to point 1. In other words, on a line from 0, 0 to
		/// 100, 100, a value of t = 1 would yield the point 100, 100.
		/// </param>
		/// <returns>
		/// A reference to the point on the line, relative to point 1,
		/// identified by t, if found. Otherwise, null.
		/// </returns>
		public static FPoint GetLinePoint(FPoint point0, FPoint point1, float t)
		{
			FPoint result = null;

			if(point0 != null && point1 != null)
			{
				result = Lerp(point0, point1, t);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Lerp																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the linear interpolation of the scalar value between start and
		/// end representing the specified progress as a value between 0 and 1.
		/// </summary>
		/// <param name="start">
		/// The starting position.
		/// </param>
		/// <param name="end">
		/// The ending position.
		/// </param>
		/// <param name="progress">
		/// The progress of completion between the start and end values.
		/// </param>
		/// <returns>
		/// The linear interpolated value between start and end as indicated by
		/// the progress value.
		/// </returns>
		public static double Lerp(double start, double end, double progress)
		{
			return start * (1d - progress) + end * progress;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the linear interpolation of the scalar value between start and
		/// end representing the specified progress as a value between 0 and 1.
		/// </summary>
		/// <param name="start">
		/// The starting position.
		/// </param>
		/// <param name="end">
		/// The ending position.
		/// </param>
		/// <param name="progress">
		/// The progress of completion between the start and end values.
		/// </param>
		/// <returns>
		/// The linear interpolated value between start and end as indicated by
		/// the progress value.
		/// </returns>
		public static float Lerp(float start, float end, float progress)
		{
			return start * (1f - progress) + end * progress;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the dual linear interpolation of the values between start and
		/// end points representing the specified progress as a value between 0
		/// and 1.
		/// </summary>
		/// <param name="start">
		/// Reference to the start point to be considered.
		/// </param>
		/// <param name="end">
		/// Reference to the end point to be considered.
		/// </param>
		/// <param name="progress">
		/// Current progress value.
		/// </param>
		/// <returns>
		/// The linear interpolated point value between start and end, as indicated
		/// by the progress value.
		/// </returns>
		public static FPoint Lerp(FPoint start, FPoint end,
			float progress)
		{
			FPoint result = new FPoint();

			if (start != null && end != null)
			{
				result.X = Lerp(start.X, end.X, progress);
				result.Y = Lerp(start.Y, end.Y, progress);
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the triple linear interpolation of the values between start and
		/// end points representing the specified progress as a value between 0 and
		/// 1.
		/// </summary>
		/// <param name="start">
		/// Reference to the start point to be considered.
		/// </param>
		/// <param name="end">
		/// Reference to the end point to be considered.
		/// </param>
		/// <param name="progress">
		/// Current progress value.
		/// </param>
		/// <returns>
		/// The linear interpolated point value between start and end, as indicated
		/// by the progress value.
		/// </returns>
		public static FPoint3 Lerp(FPoint3 start, FPoint3 end,
			float progress)
		{
			FPoint3 result = new FPoint3();

			if(start != null && end != null)
			{
				result.X = Lerp(start.X, end.X, progress);
				result.Y = Lerp(start.Y, end.Y, progress);
				result.Z = Lerp(start.Z, end.Z, progress);
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the triple linear interpolation of the values between start and
		/// end points representing the specified progress as a value between 0 and
		/// 1.
		/// </summary>
		/// <param name="start">
		/// Reference to the start point to be considered.
		/// </param>
		/// <param name="end">
		/// Reference to the end point to be considered.
		/// </param>
		/// <param name="progress">
		/// Current progress value.
		/// </param>
		/// <returns>
		/// The linear interpolated point value between start and end, as indicated
		/// by the progress value.
		/// </returns>
		public static FVector3 Lerp(FVector3 start, FVector3 end,
			float progress)
		{
			FVector3 result = new FVector3();

			if(start != null && end != null)
			{
				result.X = Lerp(start.X, end.X, progress);
				result.Y = Lerp(start.Y, end.Y, progress);
				result.Z = Lerp(start.Z, end.Z, progress);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
