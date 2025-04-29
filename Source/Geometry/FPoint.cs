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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Newtonsoft.Json;

using static Geometry.GeometryUtil;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FPoint																																	*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Single-precision 2D point.
	/// </summary>
	/// <remarks>
	/// This class exists to take advantage of first-class object behavior
	/// during operation. It is similar in usage to the System.Windows.PointF,
	/// but is accessible by reference wherever it is used. As a result, once
	/// the item is assigned in a helper function, its members continue to
	/// carry exactly the same information as the root instance until they
	/// are changed.
	/// </remarks>
	public class FPoint : FVector2
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		////*-----------------------------------------------------------------------*
		////*	OnCoordinateChanged																										*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Raise the CoordinateChanged event whenever coordinates have changed.
		///// </summary>
		///// <param name="e">
		///// Float point event arguments.
		///// </param>
		//protected virtual void OnCoordinateChanged(FloatPointEventArgs e)
		//{
		//	CoordinateChanged?.Invoke(this, e);
		//}
		////*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new Instance of the FPoint Item.
		/// </summary>
		public FPoint()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the FPoint Item.
		/// </summary>
		/// <param name="x">
		/// X coordinate.
		/// </param>
		/// <param name="y">
		/// Y coordinate.
		/// </param>
		public FPoint(float x, float y)
		{
			mX = x;
			mY = y;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the FPoint Item.
		/// </summary>
		/// <param name="source">
		/// Reference to an instance of a FPoint to use for reference values.
		/// </param>
		public FPoint(FPoint source)
		{
			if(source != null)
			{
				mX = source.X;
				mY = source.Y;
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the FPoint Item.
		/// </summary>
		/// <param name="source">
		/// Reference to an instance of an FVector2 to use for reference values.
		/// </param>
		public FPoint(FVector2 source)
		{
			if(source != null)
			{
				mX = source.X;
				mY = source.Y;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* _Operator scalar * FPoint																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of a point multiplied by a scalar.
		/// </summary>
		/// <param name="scalar">
		/// The scalar value to multiply.
		/// </param>
		/// <param name="point">
		/// The point to multiply.
		/// </param>
		/// <returns>
		/// Result of the multiplication.
		/// </returns>
		public static FPoint operator *(float scalar, FPoint point)
		{
			FPoint result = new FPoint();

			if(point != null && scalar != 0f)
			{
				result.mX = scalar * point.mX;
				result.mY = scalar * point.mY;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the result of a point multiplied by a scalar.
		/// </summary>
		/// <param name="point">
		/// The point to multiply.
		/// </param>
		/// <param name="scalar">
		/// The scalar value to multiply.
		/// </param>
		/// <returns>
		/// Result of the multiplication.
		/// </returns>
		public static FPoint operator *(FPoint point, float scalar)
		{
			FPoint result = new FPoint();

			if(point != null && scalar != 0f)
			{
				result.mX = scalar * point.mX;
				result.mY = scalar * point.mY;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* _Operator +																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of the values of two points added together.
		/// </summary>
		/// <param name="pointA">
		/// First point to add.
		/// </param>
		/// <param name="pointB">
		/// Second point to add.
		/// </param>
		/// <returns>
		/// Result of the addition.
		/// </returns>
		public static FPoint operator +(FPoint pointA, FPoint pointB)
		{
			FPoint result = new FPoint();

			if(pointA != null)
			{
				result.mX = pointA.mX;
				result.mY = pointA.mY;
			}
			if(pointB != null)
			{
				result.mX += pointB.mX;
				result.mY += pointB.mY;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* _Operator -																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of one point subtracted from another.
		/// </summary>
		/// <param name="pointA">
		/// Reference to the minuend point.
		/// </param>
		/// <param name="pointB">
		/// Reference to the subtrahend point.
		/// </param>
		/// <returns>
		/// Result of the subtraction.
		/// </returns>
		public static FPoint operator -(FPoint pointA, FPoint pointB)
		{
			FPoint result = new FPoint();

			if(pointA != null)
			{
				result.mX = pointA.mX;
				result.mY = pointA.mY;
			}
			if(pointB != null)
			{
				result.mX -= pointB.mX;
				result.mY -= pointB.mY;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Operator FPoint != FPoint																						*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return a value indicating whether values of two points are not equal.
		///// </summary>
		///// <param name="pointA">
		///// Reference to the first point to compare.
		///// </param>
		///// <param name="pointB">
		///// Reference to the second point to compare.
		///// </param>
		///// <returns>
		///// True if the two objects are substantially not equal in value.
		///// Otherwise, false.
		///// </returns>
		//[DebuggerStepThrough]
		//public static bool operator !=(FPoint pointA, FPoint pointB)
		//{
		//	bool result = true;

		//	if ((object)pointA != null && (object)pointB != null)
		//	{
		//		result = !(pointA == pointB);
		//	}
		//	else if ((object)pointA == null && (object)pointB == null)
		//	{
		//		result = false;
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Operator FPoint == FPoint																						*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return a value indicating whether values of two points are equal.
		///// </summary>
		///// <param name="pointA">
		///// Reference to the first point to compare.
		///// </param>
		///// <param name="pointB">
		///// Reference to the second point to compare.
		///// </param>
		///// <returns>
		///// True if the two objects are substantially equal in value. Otherwise,
		///// false.
		///// </returns>
		//[DebuggerStepThrough]
		//public static bool operator ==(FPoint pointA, FPoint pointB)
		//{
		//	bool result = true;

		//	if((object)pointA != null && (object)pointB != null)
		//	{
		//		result = (
		//			(pointA.mX == pointB.mX) &&
		//			(pointA.mY == pointB.mY));
		//	}
		//	else if((object)pointA != null || (object)pointB != null)
		//	{
		//		result = false;
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Implicit FPoint = SKPoint																						*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast an SKPoint value to FPoint.
		///// </summary>
		///// <param name="value">
		///// The point to convert.
		///// </param>
		//public static implicit operator FPoint(SKPoint value)
		//{
		//	FPoint rv = new FPoint();

		//	if(value != null)
		//	{
		//		rv.mX = value.X;
		//		rv.mY = value.Y;
		//	}
		//	return rv;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Implicit FPoint = Point																							*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast a Point value to FPoint.
		///// </summary>
		///// <param name="value">
		///// The point to convert.
		///// </param>
		///// <remarks>
		///// This operator is not available when compiling without GDI+.
		///// </remarks>
		//public static implicit operator FPoint(System.Drawing.Point value)
		//{
		//	FPoint result = new FPoint();

		//	if(value.X != 0 || value.Y != 0)
		//	{
		//		result.mX = value.X;
		//		result.mY = value.Y;
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Implicit FPoint = PointF																							*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast a PointF value to FPoint.
		///// </summary>
		///// <param name="value">
		///// The point to convert.
		///// </param>
		///// <remarks>
		///// This operator is not available when compiling without GDI+.
		///// </remarks>
		//public static implicit operator FPoint(System.Drawing.PointF value)
		//{
		//	FPoint result = new FPoint();

		//	if(value.X != 0f || value.Y != 0f)
		//	{
		//		result.mX = value.X;
		//		result.mY = value.Y;
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Implicit Point = FPoint																							*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast an FPoint value to Point.
		///// </summary>
		///// <param name="value">
		///// The point to convert.
		///// </param>
		///// <remarks>
		///// This operator is not available when compiling without GDI+.
		///// </remarks>
		//public static implicit operator Point(FPoint value)
		//{
		//	Point result = new Point();

		//	if(value != null)
		//	{
		//		result.X = (int)value.mX;
		//		result.Y = (int)value.mY;
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Implicit PointF = FPoint																							*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast an FPoint value to PointF.
		///// </summary>
		///// <param name="value">
		///// The point to convert.
		///// </param>
		///// <remarks>
		///// This operator is not available when compiling without GDI+.
		///// </remarks>
		//public static implicit operator PointF(FPoint value)
		//{
		//	PointF result = PointF.Empty;

		//	if(value != null)
		//	{
		//		result.X = value.mX;
		//		result.Y = value.mY;
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Implicit SKPoint = FPoint																						*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast an FPoint value to SKPoint.
		///// </summary>
		///// <param name="value">
		///// The point to convert.
		///// </param>
		//public static implicit operator SKPoint(FPoint value)
		//{
		//	SKPoint rv = new SKPoint();

		//	if(value != null)
		//	{
		//		rv.X = value.X;
		//		rv.Y = value.Y;
		//	}
		//	return rv;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* Clear																																	*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Clear the values on the specified point.
		///// </summary>
		///// <param name="point">
		///// Reference to the point to clear.
		///// </param>
		//public static void Clear(FPoint point)
		//{
		//	if(point != null && !point.mReadOnly)
		//	{
		//		point.mX = point.mY = 0f;
		//	}
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a memberwise clone of the provided point.
		/// </summary>
		/// <param name="source">
		/// Reference to the source point to be cloned.
		/// </param>
		/// <returns>
		/// Reference to a new FPoint instance where the primitive member values
		/// are the same as those in the source, if a legitimate source was
		/// provided. Otherwise, an empty FPoint.
		/// </returns>
		public static FPoint Clone(FPoint source)
		{
			FPoint result = new FPoint();

			if(source != null)
			{
				result.mReadOnly = source.mReadOnly;
				result.mX = source.mX;
				result.mY = source.mY;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ClosestPoint																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the closest point to the check-point.
		/// </summary>
		/// <param name="checkPoint">
		/// The check point to which the closest point in the list will be found.
		/// </param>
		/// <param name="areas">
		/// Reference to a list of areas whose points will be compared.
		/// </param>
		/// <returns>
		/// A point corresponding to the location of the closest area in the
		/// list.
		/// </returns>
		public static FPoint ClosestPoint(FPoint checkPoint, List<FArea> areas)
		{
			List<float> distances = new List<float>();
			int minIndex = -1;
			float minValue = 0;
			FPoint result = null;

			if (checkPoint != null && areas?.Count > 0)
			{
				foreach (FArea areaItem in areas)
				{
					distances.Add(
						Math.Abs(Trig.GetLineDistance(
							checkPoint.mX, checkPoint.mY, areaItem.X, areaItem.Y)));
				}
				minValue = distances.Min();
				minIndex = distances.IndexOf(minValue);
				result = FArea.Location(areas[minIndex]);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	CoordinateChanged																											*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Fired when a coordinate has changed.
		///// </summary>
		//public event FloatPointEventHandler CoordinateChanged;
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Delta																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the coordinate difference between two points.
		/// </summary>
		/// <param name="pointA">
		/// Reference to the first point to compare.
		/// </param>
		/// <param name="pointB">
		/// Reference to the second point to compare.
		/// </param>
		/// <returns>
		/// Coordinate difference between two points.
		/// </returns>
		public static FPoint Delta(FPoint pointA, FPoint pointB)
		{
			FPoint result = new FPoint();

			if (pointA != null && pointB != null)
			{
				result.mX = pointB.mX - pointA.mX;
				result.mY = pointB.mY - pointA.mY;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* Dot																																		*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return the dot product of two points.
		///// </summary>
		///// <param name="value1">
		///// Reference to the first point to compare.
		///// </param>
		///// <param name="value2">
		///// Reference to the second point to compare.
		///// </param>
		///// <returns>
		///// The dot product of the two input points.
		///// </returns>
		//public static float Dot(FPoint value1, FPoint value2)
		//{
		//	float result = 0f;

		//	if(value1 != null && value2 != null)
		//	{
		//		result = value1.X * value2.X + value1.Y * value2.Y;
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Equals																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether this item's members are equal to the
		/// members of the caller's item.
		/// </summary>
		/// <param name="obj">
		/// Reference to the object to which this value is being compared.
		/// </param>
		/// <returns>
		/// A value indicating whether this value is substantially equal to the
		/// caller's provided item.
		/// </returns>
		public override bool Equals(object obj)
		{
			bool result = false;

			if (obj is FPoint @point)
			{
				if (point.mX == mX && point.mY == mY)
				{
					result = true;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetHashCode																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the unique hash code for this instance.
		/// </summary>
		/// <returns>
		/// The hash code of this item and value.
		/// </returns>
		public override int GetHashCode()
		{
			int factor = 0;
			int result = 2020122801;

			factor = 0 - (int)((double)result * 0.25);

			result *= (factor + mX.GetHashCode());
			result *= (factor + mY.GetHashCode());
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Invert																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Invert the values of the caller's coordinate.
		/// </summary>
		/// <param name="point">
		/// Reference to the point to be inverted.
		/// </param>
		/// <returns>
		/// Reference to the caller's point with inverted values.
		/// </returns>
		public static FPoint Invert(FPoint point)
		{
			FPoint result = new FPoint();

			if(point != null)
			{
				result.mX = 0f - point.mX;
				result.mY = 0f - point.mY;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* IsDifferent																														*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return a value indicating whether two points are different.
		///// </summary>
		///// <param name="pointA">
		///// Reference to the first point to compare.
		///// </param>
		///// <param name="pointB">
		///// Reference to the second point to compare.
		///// </param>
		///// <returns>
		///// True if the two points are different. Otherwise, false.
		///// </returns>
		//public static bool IsDifferent(FPoint pointA, FPoint pointB)
		//{
		//	bool result = false;

		//	if(pointA != null && pointB != null)
		//	{
		//		result = pointA.mX != pointB.mX ||
		//			pointA.mY != pointB.mY;
		//	}
		//	else if(pointA != null || pointB != null)
		//	{
		//		result = true;
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* IsEmpty																																*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return a value indicating whether the specified point is empty.
		///// </summary>
		///// <param name="point">
		///// Reference to the object to inspect.
		///// </param>
		///// <returns>
		///// True if the specified point is empty. Otherwise, false.
		///// </returns>
		//public static bool IsEmpty(FPoint point)
		//{
		//	bool result = true;

		//	if(point != null)
		//	{
		//		result = (point.mX == 0f && point.mY == 0f);
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* Magnitude																															*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return the absolute magnitude of the provided point.
		///// </summary>
		///// <param name="point">
		///// Reference to the point for which the magnitude will be found.
		///// </param>
		///// <returns>
		///// The absolute magnitude of the caller's point.
		///// </returns>
		//public static float Magnitude(FPoint point)
		//{
		//	float result = 0f;

		//	if(point != null)
		//	{
		//		result = (float)Math.Sqrt(
		//			(double)(point.mX * point.mX + point.mY * point.mY));
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MiddlePoint																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the middle coordinate between two points.
		/// </summary>
		/// <param name="pointA">
		/// Reference to the first point to test.
		/// </param>
		/// <param name="pointB">
		/// Reference to the second point to test.
		/// </param>
		/// <returns>
		/// Reference to a point that represents the exact middle coordinate of the
		/// caller's virtual line.
		/// </returns>
		public static FPoint MiddlePoint(FPoint pointA, FPoint pointB)
		{
			FPoint result = new FPoint();

			if (pointA != null && pointB != null)
			{
				result.X = (pointA.X + pointB.X) / 2f;
				result.Y = (pointA.Y + pointB.Y) / 2f;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Offset																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a new instance the caller's point, translated by the specified
		/// offset.
		/// </summary>
		/// <param name="point">
		/// Reference to the point to be offset.
		/// </param>
		/// <param name="dx">
		/// X distance from the original point.
		/// </param>
		/// <param name="dy">
		/// Y distance from the original point.
		/// </param>
		/// <returns>
		/// Reference to a new point at the specified offset from the original.
		/// </returns>
		public static FPoint Offset(FPoint point, float dx, float dy)
		{
			FPoint result = new FPoint(point.mX + dx, point.mY + dy);
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Parse																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Parse a coordinate string and return its FPoint representation.
		/// </summary>
		/// <param name="coordinate">
		/// The coordinate string to parse.
		/// </param>
		/// <param name="allowNull">
		/// Value indicating whether to allow a null return value if the input
		/// string was invalid.
		/// </param>
		/// <returns>
		/// Newly created FPoint value representing the caller's input, if
		/// that input was legal or allowNull was false. Otherwise, a null value.
		/// </returns>
		public static new FPoint Parse(string coordinate, bool allowNull = false)
		{
			bool bX = false;
			bool bY = false;
			int index = 0;
			MatchCollection matches = null;
			FPoint result = null;
			string text = "";

			if(coordinate?.Length > 0)
			{
				text = coordinate.Trim();
				if(text.StartsWith("{") && text.EndsWith("}"))
				{
					//	JSON object.
					try
					{
						result = JsonConvert.DeserializeObject<FPoint>(coordinate);
					}
					catch { }
				}
				else
				{
					//	Freehand.
					matches = Regex.Matches(coordinate, ResourceMain.rxCoordinate);
					if(matches.Count > 0)
					{
						result = new FPoint();
						foreach(Match matchItem in matches)
						{
							text = GetValue(matchItem, "label").ToLower();
							switch(text)
							{
								case "x":
									result.mX = ToFloat(GetValue(matchItem, "number"));
									bX = true;
									break;
								case "y":
									result.mY = ToFloat(GetValue(matchItem, "number"));
									bY = true;
									break;
								default:
									switch(index)
									{
										case 0:
											if(!bX)
											{
												result.mX = ToFloat(GetValue(matchItem, "number"));
												bX = true;
											}
											break;
										case 1:
											if(!bY)
											{
												result.mY = ToFloat(GetValue(matchItem, "number"));
												bY = true;
											}
											break;
									}
									break;
							}
							index++;
						}
					}
				}
			}
			if(result == null && !allowNull)
			{
				result = new FPoint();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	ReadOnly																															*
		////*-----------------------------------------------------------------------*
		//private bool mReadOnly = false;
		///// <summary>
		///// Get/Set a value indicating whether this item is read-only.
		///// </summary>
		//[JsonIgnore]
		//public bool ReadOnly
		//{
		//	get { return mReadOnly; }
		//	set { mReadOnly = value; }
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Rotate																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Rotate the caller's point around the origin.
		/// </summary>
		/// <param name="x">
		/// The X value to be rotated.
		/// </param>
		/// <param name="y">
		/// The Y value to be rotated.
		/// </param>
		/// <param name="theta">
		/// The angle at which to rotate the point, in radians.
		/// </param>
		/// <returns>
		/// Reference to a representation of the caller's point after being
		/// rotated by the specified angle around the origin.
		/// </returns>
		public static FPoint Rotate(float x, float y, float theta)
		{
			FPoint result = new FPoint();

			result.mX =
				(float)((double)x * Math.Cos((double)theta) -
					(double)y * Math.Sin((double)theta));
			result.mY =
				(float)((double)x * Math.Sin((double)theta) +
					(double)y * Math.Cos((double)theta));
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Rotate the caller's point around the origin.
		/// </summary>
		/// <param name="point">
		/// Reference to the point to be rotated.
		/// </param>
		/// <param name="theta">
		/// The angle at which to rotate the point, in radians.
		/// </param>
		/// <returns>
		/// Reference to a representation of the caller's point after being
		/// rotated by the specified angle around the origin.
		/// </returns>
		public static FPoint Rotate(FPoint point, float theta)
		{
			FPoint result = new FPoint();

			if(point != null)
			{
				result.mX =
					(float)((double)point.mX * Math.Cos((double)theta) -
						(double)point.mY * Math.Sin((double)theta));
				result.mY =
					(float)((double)point.mX * Math.Sin((double)theta) +
						(double)point.mY * Math.Cos((double)theta));
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Scale																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Scale the caller's point by a uniform factor.
		/// </summary>
		/// <param name="point">
		/// The point to be scaled.
		/// </param>
		/// <param name="scale">
		/// The factor by which the point will be scaled.
		/// </param>
		/// <returns>
		/// Reference to the uniformly scaled point.
		/// </returns>
		public static FPoint Scale(FPoint point, float scale)
		{
			FPoint result = new FPoint();

			if (point != null)
			{
				result.mX = point.mX * scale;
				result.mY = point.mY * scale;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* Translate																															*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Translate the values of the caller's point by the provided offset.
		///// </summary>
		///// <param name="point">
		///// Reference to the point to be translated.
		///// </param>
		///// <param name="offset">
		///// Reference to the offset to apply to the point.
		///// </param>
		//public static void Translate(FPoint point, FPoint offset)
		//{
		//	if(point != null && offset != null)
		//	{
		//		point.X += offset.mX;
		//		point.Y += offset.mY;
		//	}
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TransferValues																												*
		//*-----------------------------------------------------------------------*
		///// <summary>
		///// Transfer the member values of one instance to another.
		///// </summary>
		///// <param name="source">
		///// Reference to the source point whose values will be assigned.
		///// </param>
		///// <param name="target">
		///// Reference to the target point that will receive the values.
		///// </param>
		//public static void TransferValues(FPoint source, FPoint target)
		//{
		//	if(source != null && target != null && !target.mReadOnly)
		//	{
		//		//	It would be possible to allow a null target, but that strategy
		//		//	would have to assigned an 'out' value to the parameter, or to
		//		//	return the newly created value.
		//		target.X = source.mX;
		//		target.Y = source.mY;
		//	}
		//}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Transfer member values to the specified target.
		/// </summary>
		/// <param name="target">
		/// Reference to the item to be set.
		/// </param>
		/// <param name="x">
		/// X coordinate to assign.
		/// </param>
		/// <param name="y">
		/// Y coordinate to assign.
		/// </param>
		public static void TransferValues(FPoint target, float x, float y)
		{
			if(target != null && !target.mReadOnly)
			{
				target.X = x;
				target.Y = y;
			}
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	X																																			*
		////*-----------------------------------------------------------------------*
		//private float mX = 0f;
		///// <summary>
		///// Get/Set the X value of the coordinate.
		///// </summary>
		//[JsonProperty(Order = 0)]
		//public float X
		//{
		//	get { return mX; }
		//	set
		//	{
		//		float original = mX;

		//		if (!mReadOnly)
		//		{
		//			mX = value;
		//			if (original != value)
		//			{
		//				OnCoordinateChanged(
		//					new FloatPointEventArgs()
		//					{
		//						OriginalValue = new FPoint(original, mY),
		//						NewValue = new FPoint(value, mY)
		//					});
		//			}
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	Y																																			*
		////*-----------------------------------------------------------------------*
		//private float mY = 0f;
		///// <summary>
		///// Get/Set the Y value of the coordinate.
		///// </summary>
		//[JsonProperty(Order = 1)]
		//public float Y
		//{
		//	get { return mY; }
		//	set
		//	{
		//		float original = mY;

		//		if (!mReadOnly)
		//		{
		//			mY = value;
		//			if (original != value)
		//			{
		//				OnCoordinateChanged(
		//					new FloatPointEventArgs()
		//					{
		//						OriginalValue = new FPoint(mX, original),
		//						NewValue = new FPoint(mX, value)
		//					});
		//			}
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	ToString																															*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return the string representation of this item.
		///// </summary>
		///// <returns>
		///// String representation of the values of this point.
		///// </returns>
		//public override string ToString()
		//{
		//	StringBuilder result = new StringBuilder();

		//	result.Append($"{mX:0.000}");
		//	result.Append(',');
		//	result.Append($"{mY:0.000}");
		//	return result.ToString();
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	Zero																																	*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Zero the values of the specified point.
		///// </summary>
		///// <param name="point">
		///// Reference to the point to be modified.
		///// </param>
		//public static void Zero(FPoint point)
		//{
		//	if (!point.mReadOnly)
		//	{
		//		point.mX = 0.0f;
		//		point.mY = 0.0f;
		//	}
		//}
		////*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	FPointCollection																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of FPoint Items.
	/// </summary>
	public class FPointCollection : List<FPoint>
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
		//* Add																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add an item to the collection.
		/// </summary>
		/// <param name="item">
		/// Reference to the item to add.
		/// </param>
		public new void Add(FPoint item)
		{
			if(item != null && !mReadOnly)
			{
				base.Add(item);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ReadOnly																															*
		//*-----------------------------------------------------------------------*
		private bool mReadOnly = false;
		/// <summary>
		/// Get/Set a value indicating whether this item is read-only.
		/// </summary>
		public bool ReadOnly
		{
			get { return mReadOnly; }
			set { mReadOnly = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
