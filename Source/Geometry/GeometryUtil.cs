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
using System.Text.RegularExpressions;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	GeometryUtil																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Utility features and functionality for the Geometry library.
	/// </summary>
	public class GeometryUtil
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
		//* Clamp																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clamp the supplied value within the allowed minimum and maximum values.
		/// </summary>
		/// <param name="value">
		/// The value to clamp.
		/// </param>
		/// <param name="minimum">
		/// The minimum allowable value.
		/// </param>
		/// <param name="maximum">
		/// The maximum allowable value.
		/// </param>
		/// <returns>
		/// The caller's clamped value.
		/// </returns>
		public static float Clamp(float value,
			float minimum = 0, float maximum = 1)
		{
			return Math.Max(minimum, Math.Min(value, maximum));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ConvertRange																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert a value from one range to another.
		/// </summary>
		/// <param name="sourceStart">
		/// The starting value of the source range.
		/// </param>
		/// <param name="sourceEnd">
		/// The ending value of the source range.
		/// </param>
		/// <param name="targetStart">
		/// The starting value of the target range.
		/// </param>
		/// <param name="targetEnd">
		/// The ending value of the target range.
		/// </param>
		/// <param name="value">
		/// The value to convert.
		/// </param>
		/// <returns>
		/// The representation of the caller's source value in the target range.
		/// </returns>
		public static float ConvertRange(float sourceStart, float sourceEnd,
			float targetStart, float targetEnd, float value)
		{
			double newDiff = (double)targetEnd - (double)targetStart;
			double originalDiff = (double)sourceEnd - (double)sourceStart;
			double ratio = (originalDiff != 0d ? newDiff / originalDiff : 1d);

			return (float)(((double)value - (double)sourceStart) *
				ratio / originalDiff + (double)targetStart);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CopySign																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a floating-point value with the magnitude of its first argument
		/// and the sign of its second argument
		/// </summary>
		/// <param name="magnitude">
		/// The magnitude to use.
		/// </param>
		/// <param name="sign">
		/// The sign to use.
		/// </param>
		/// <returns>
		/// The caller's magnitude, in the indicated sign.
		/// </returns>
		public static double CopySign(double magnitude, double sign)
		{
			return Math.Abs(magnitude) * (sign >= 0d ? 1d : -1d);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a floating-point value with the magnitude of its first argument
		/// and the sign of its second argument
		/// </summary>
		/// <param name="magnitude">
		/// The magnitude to use.
		/// </param>
		/// <param name="sign">
		/// The sign to use.
		/// </param>
		/// <returns>
		/// The caller's magnitude, in the indicated sign.
		/// </returns>
		public static float CopySign(float magnitude, float sign)
		{
			return (float)(Math.Abs((double)magnitude) * (sign >= 0f ? 1d : -1d));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Epsilon																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The smallest practical number in this version.
		/// </summary>
		public static readonly float Epsilon = 1e-6f;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetValue																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the value of the specified group member in the provided match.
		/// </summary>
		/// <param name="match">
		/// Reference to the match to be inspected.
		/// </param>
		/// <param name="groupName">
		/// Name of the group for which the value will be found.
		/// </param>
		/// <returns>
		/// The value found in the specified group, if found. Otherwise, empty
		/// string.
		/// </returns>
		public static string GetValue(Match match, string groupName)
		{
			string result = "";

			if(match != null && match.Groups[groupName] != null &&
				match.Groups[groupName].Value != null)
			{
				result = match.Groups[groupName].Value;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the value of the specified group member in a match found with
		/// the provided source and pattern.
		/// </summary>
		/// <param name="source">
		/// Source string to search.
		/// </param>
		/// <param name="pattern">
		/// Regular expression pattern to apply.
		/// </param>
		/// <param name="groupName">
		/// Name of the group for which the value will be found.
		/// </param>
		/// <returns>
		/// The value found in the specified group, if found. Otherwise, empty
		/// string.
		/// </returns>
		public static string GetValue(string source, string pattern,
			string groupName)
		{
			Match match = null;
			string result = "";

			if(source?.Length > 0 && pattern?.Length > 0 && groupName?.Length > 0)
			{
				match = Regex.Match(source, pattern);
				if(match.Success)
				{
					result = GetValue(match, groupName);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* HalfPi																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Session-wide reusable 0.5*PI value.
		/// </summary>
		public static readonly float HalfPi = (float)(Math.PI * 0.5f);
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReverseSourcePolarity																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Apply the reverse polarity of the source value to the target value,
		/// returning the new target value.
		/// </summary>
		/// <param name="source">
		/// Source value whose reverse polarity will be applied.
		/// </param>
		/// <param name="target">
		/// Target whose value will be polarized.
		/// </param>
		/// <returns>
		/// A value representing the caller's target value to which the reverse
		/// polarity of the source has been applied.
		/// </returns>
		public static float ReverseSourcePolarity(float source, float target)
		{
			float result = target;

			if(source > 0f)
			{
				//	Target polarity will be negative.
				if(target > 0f)
				{
					result = 0f - target;
				}
			}
			else
			{
				//	Target polarity will be positive.
				if(target < 0f)
				{
					result = 0f - target;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SignEqual																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the signs are equal between
		/// values A and B.
		/// </summary>
		/// <param name="valueA">
		/// First value to compare.
		/// </param>
		/// <param name="valueB">
		/// Second value to compare.
		/// </param>
		/// <returns>
		/// True if the signs of the numbers are the same. Otherwise, false.
		/// </returns>
		public static bool SignEqual(float valueA, float valueB)
		{
			return (
				(valueA >= 0f && valueB >= 0f) ||
				(valueA < 0f && valueB < 0f));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SignNotEqual																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the signs are unequal between
		/// values A and B.
		/// </summary>
		/// <param name="valueA">
		/// First value to compare.
		/// </param>
		/// <param name="valueB">
		/// Second value to compare.
		/// </param>
		/// <returns>
		/// True if the signs of the numbers are different. Otherwise, false.
		/// </returns>
		public static bool SignNotEqual(float valueA, float valueB)
		{
			return (
				(valueA > 0f && valueB < 0f) ||
				(valueA < 0f && valueB > 0f));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SourcePolarity																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Apply the polarity of the source value to the target value,
		/// returning the new target value.
		/// </summary>
		/// <param name="source">
		/// Source value whose polarity will be applied.
		/// </param>
		/// <param name="target">
		/// Target whose value will be polarized.
		/// </param>
		/// <returns>
		/// A value representing the caller's target value to which the
		/// polarity of the source has been applied.
		/// </returns>
		public static float SourcePolarity(float source, float target)
		{
			float result = target;

			if(source > 0f)
			{
				//	Target polarity will be positive.
				if(target < 0f)
				{
					result = 0f - target;
				}
			}
			else
			{
				//	Target polarity will be negative.
				if(target > 0f)
				{
					result = 0f - target;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ToFloat																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Provide fail-safe conversion of string to numeric value.
		/// </summary>
		/// <param name="value">
		/// Value to convert.
		/// </param>
		/// <returns>
		/// Floating point value. 0 if not convertible.
		/// </returns>
		public static float ToFloat(object value)
		{
			float result = 0f;
			if(value != null)
			{
				result = ToFloat(value.ToString());
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Provide fail-safe conversion of string to numeric value.
		/// </summary>
		/// <param name="value">
		/// Value to convert.
		/// </param>
		/// <returns>
		/// Floating point value. 0 if not convertible.
		/// </returns>
		public static float ToFloat(string value)
		{
			float result = 0f;
			try
			{
				result = float.Parse(value);
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ToInt																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Provide fail-safe conversion of string to numeric value.
		/// </summary>
		/// <param name="value">
		/// Value to convert.
		/// </param>
		/// <returns>
		/// Int32 value. 0 if not convertible.
		/// </returns>
		public static int ToInt(object value)
		{
			int result = 0;
			if(value != null)
			{
				result = ToInt(value.ToString());
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Provide fail-safe conversion of string to numeric value.
		/// </summary>
		/// <param name="value">
		/// Value to convert.
		/// </param>
		/// <returns>
		/// Int32 value. 0 if not convertible.
		/// </returns>
		public static int ToInt(string value)
		{
			int result = 0;
			try
			{
				result = int.Parse(value);
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TwoPi																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Session-wide reusable 2*PI value (Tau).
		/// </summary>
		public static readonly float TwoPi = (float)(Math.PI * 2f);
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
