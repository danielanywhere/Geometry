/*
 * Copyright (c). 2005 - 2025 Daniel Patterson, MCSD (danielanywhere).
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

using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

using static Geometry.GeometryUtil;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FVector2																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Single floating point 2D Vector.
	/// </summary>
	public class FVector2
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	OnCoordinateChanged																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raise the CoordinateChanged event whenever coordinates have changed.
		/// </summary>
		/// <param name="e">
		/// Float point event arguments.
		/// </param>
		protected virtual void OnCoordinateChanged(FloatPointEventArgs e)
		{
			CoordinateChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new Instance of the FVector2 Item.
		/// </summary>
		public FVector2()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the FVector2 Item.
		/// </summary>
		/// <param name="x">
		/// X coordinate.
		/// </param>
		/// <param name="y">
		/// Y coordinate.
		/// </param>
		public FVector2(float x, float y)
		{
			mX = x;
			mY = y;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the FVector2 Item.
		/// </summary>
		/// <param name="source">
		/// Reference to an instance of an FVector2 to use for reference values.
		/// </param>
		public FVector2(FVector2 source)
		{
			if(source != null)
			{
				mX = source.X;
				mY = source.Y;
			}
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Implicit FPoint = FVector2																						*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast the FVector2 instance to an FPoint.
		///// </summary>
		///// <param name="value">
		///// Reference to the vector to be converted.
		///// </param>
		///// <returns>
		///// Reference to a newly created FPoint representing the values in
		///// the caller's vector.
		///// </returns>
		//public static implicit operator FPoint(FVector2 value)
		//{
		//	FPoint result = new FPoint();

		//	if(value != null)
		//	{
		//		if(value.mValues.Length > 0)
		//		{
		//			result.X = value.mValues[0];
		//		}
		//		if(value.mValues.Length > 1)
		//		{
		//			result.Y = value.mValues[1];
		//		}
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Implicit FVector2 = FPoint																						*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast the FPoint instance to a FVector2.
		///// </summary>
		///// <param name="value">
		///// Reference to the point to be converted.
		///// </param>
		///// <returns>
		///// Reference to the newly created vector representing the values in the
		///// caller's point.
		///// </returns>
		//public static implicit operator FVector2(FPoint value)
		//{
		//	FVector2 result = null;

		//	if(value != null)
		//	{
		//		result = new FVector2(value.X, value.Y);
		//	}
		//	if(result == null)
		//	{
		//		//	Last chance. Safe return.
		//		result = new FVector2();
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

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
		public static FVector2 operator *(float scalar, FVector2 point)
		{
			FVector2 result = new FVector2();

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
		public static FVector2 operator *(FVector2 point, float scalar)
		{
			FVector2 result = new FVector2();

			if(point != null && scalar != 0f)
			{
				result.mX = scalar * point.mX;
				result.mY = scalar * point.mY;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* _Operator scalar / FPoint																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of a scalar divided by a point.
		/// </summary>
		/// <param name="scalar">
		/// The scalar divisor.
		/// </param>
		/// <param name="point">
		/// The point divident.
		/// </param>
		/// <returns>
		/// Result of the multiplication.
		/// </returns>
		public static FVector2 operator /(float scalar, FVector2 point)
		{
			FVector2 result = new FVector2();

			if(point != null)
			{
				if(point.mX != 0f)
				{
					result.mX = scalar / point.mX;
				}
				if(point.mY != 0f)
				{
					result.mY = scalar / point.mY;
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the result of a point divided by a scalar.
		/// </summary>
		/// <param name="point">
		/// The point divisor.
		/// </param>
		/// <param name="scalar">
		/// The scalar dividend.
		/// </param>
		/// <returns>
		/// Result of the multiplication.
		/// </returns>
		public static FVector2 operator /(FVector2 point, float scalar)
		{
			FVector2 result = new FVector2();

			if(point != null && scalar != 0f)
			{
				result.mX = point.mX / scalar;
				result.mY = point.mY / scalar;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* _Operator +																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of the values of two vectors added together.
		/// </summary>
		/// <param name="vectorA">
		/// First vector to add.
		/// </param>
		/// <param name="vectorB">
		/// Second vector to add.
		/// </param>
		/// <returns>
		/// Result of the addition.
		/// </returns>
		public static FVector2 operator +(FVector2 vectorA, FVector2 vectorB)
		{
			FVector2 result = new FVector2();

			if(vectorA != null)
			{
				result.mX = vectorA.mX;
				result.mY = vectorA.mY;
			}
			if(vectorB != null)
			{
				result.mX += vectorB.mX;
				result.mY += vectorB.mY;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* _Operator -																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of one vector subtracted from another.
		/// </summary>
		/// <param name="vectorA">
		/// Reference to the minuend vector.
		/// </param>
		/// <param name="vectorB">
		/// Reference to the subtrahend vector.
		/// </param>
		/// <returns>
		/// Result of the subtraction.
		/// </returns>
		public static FVector2 operator -(FVector2 vectorA, FVector2 vectorB)
		{
			FVector2 result = new FVector2();

			if(vectorA != null)
			{
				result.mX = vectorA.mX;
				result.mY = vectorA.mY;
			}
			if(vectorB != null)
			{
				result.mX -= vectorB.mX;
				result.mY -= vectorB.mY;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Operator !=																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether values of two vectors are not equal.
		/// </summary>
		/// <param name="vectorA">
		/// Reference to the first vector to compare.
		/// </param>
		/// <param name="vectorB">
		/// Reference to the second vector to compare.
		/// </param>
		/// <returns>
		/// True if the two objects are substantially not equal in value.
		/// Otherwise, false.
		/// </returns>
		[DebuggerStepThrough]
		public static bool operator !=(FVector2 vectorA, FVector2 vectorB)
		{
			bool result = true;

			if((object)vectorA != null && (object)vectorB != null)
			{
				result = !(vectorA == vectorB);
			}
			else if((object)vectorA == null && (object)vectorB == null)
			{
				result = false;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Operator ==																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether values of two points are equal.
		/// </summary>
		/// <param name="vectorA">
		/// Reference to the first vector to compare.
		/// </param>
		/// <param name="vectorB">
		/// Reference to the second vector to compare.
		/// </param>
		/// <returns>
		/// True if the two objects are substantially equal in value. Otherwise,
		/// false.
		/// </returns>
		[DebuggerStepThrough]
		public static bool operator ==(FVector2 vectorA, FVector2 vectorB)
		{
			bool result = true;

			if((object)vectorA != null && (object)vectorB != null)
			{
				result = (
					(vectorA.mX == vectorB.mX) &&
					(vectorA.mY == vectorB.mY));
			}
			else if((object)vectorA != null || (object)vectorB != null)
			{
				result = false;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Clear																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clear the values on the specified vector.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to clear.
		/// </param>
		public static void Clear(FVector2 vector)
		{
			if(vector != null && !vector.mReadOnly)
			{
				vector.mX = vector.mY = 0f;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a memberwise clone of the provided vector.
		/// </summary>
		/// <param name="source">
		/// Reference to the source vector to be cloned.
		/// </param>
		/// <returns>
		/// Reference to a new FVector2 instance where the primitive member values
		/// are the same as those in the source, if a legitimate source was
		/// provided. Otherwise, an empty FVector2.
		/// </returns>
		public static FVector2 Clone(FVector2 source)
		{
			FVector2 result = new FVector2();

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
		//*	CoordinateChanged																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a coordinate has changed.
		/// </summary>
		public event FloatPointEventHandler CoordinateChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Delta																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the coordinate difference between two vectors.
		/// </summary>
		/// <param name="vectorA">
		/// Reference to the first vector to compare.
		/// </param>
		/// <param name="vectorB">
		/// Reference to the second vector to compare.
		/// </param>
		/// <returns>
		/// Coordinate difference between two vectors.
		/// </returns>
		public static FVector2 Delta(FVector2 vectorA, FVector2 vectorB)
		{
			FVector2 result = new FVector2();

			if(vectorA != null && vectorB != null)
			{
				result.mX = vectorB.mX - vectorA.mX;
				result.mY = vectorB.mY - vectorA.mY;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Dot																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the dot product of two vectors.
		/// </summary>
		/// <param name="value1">
		/// Reference to the first vector to compare.
		/// </param>
		/// <param name="value2">
		/// Reference to the second vector to compare.
		/// </param>
		/// <returns>
		/// The dot product of the two input vectors.
		/// </returns>
		public static float Dot(FVector2 value1, FVector2 value2)
		{
			float result = 0f;

			if(value1 != null && value2 != null)
			{
				result = value1.X * value2.X + value1.Y * value2.Y;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

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

			if(obj is FVector2 @vector)
			{
				if(vector.mX == mX && vector.mY == mY)
				{
					result = true;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetArray																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return an array of values representing the member axes of the provided
		/// item.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector whose values will be converted.
		/// </param>
		/// <returns>
		/// Reference to a new array of values representing those in the provided
		/// vector, if found. Otherwise, an empty array.
		/// </returns>
		public static float[] GetArray(FVector2 vector)
		{
			float[] result = null;

			if(vector != null)
			{
				result = new float[2];
				result[0] = vector.mX;
				result[1] = vector.mY;
			}
			if(result == null)
			{
				result = new float[0];
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
		/// <param name="vector">
		/// Reference to the vector to be inverted.
		/// </param>
		/// <returns>
		/// Reference to the caller's vector with inverted values.
		/// </returns>
		public static FVector2 Invert(FVector2 vector)
		{
			FVector2 result = new FVector2();

			if(vector != null)
			{
				if(vector.mX != 0f)
				{
					result.mX = 1f / vector.mX;
				}
				if(vector.mY != 0f)
				{
					result.mY = 1f / vector.mY;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsDifferent																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether two vectors are different.
		/// </summary>
		/// <param name="vectorA">
		/// Reference to the first vector to compare.
		/// </param>
		/// <param name="vectorB">
		/// Reference to the second vector to compare.
		/// </param>
		/// <returns>
		/// True if the two vectors are different. Otherwise, false.
		/// </returns>
		public static bool IsDifferent(FVector2 vectorA, FVector2 vectorB)
		{
			bool result = false;

			if(vectorA != null && vectorB != null)
			{
				result = vectorA.mX != vectorB.mX ||
					vectorA.mY != vectorB.mY;
			}
			else if(vectorA != null || vectorB != null)
			{
				result = true;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsEmpty																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified vector is empty.
		/// </summary>
		/// <param name="vector">
		/// Reference to the object to inspect.
		/// </param>
		/// <returns>
		/// True if the specified vector is empty. Otherwise, false.
		/// </returns>
		public static bool IsEmpty(FVector2 vector)
		{
			bool result = true;

			if(vector != null &&
				(vector.mX != 0f || vector.mY != 0f))
			{
				result = false;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Length																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the length of the vector.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to measure.
		/// </param>
		/// <returns>
		/// Length of the pure vector.
		/// </returns>
		public static float Length(FVector2 vector)
		{
			return Magnitude(vector);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Magnitude																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the absolute magnitude of the provided vector.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector for which the magnitude will be found.
		/// </param>
		/// <returns>
		/// The absolute magnitude of the caller's vector.
		/// </returns>
		public static float Magnitude(FVector2 vector)
		{
			float result = 0f;

			if(vector != null)
			{
				result = (float)Math.Sqrt(
					(double)(vector.mX * vector.mX + vector.mY * vector.mY));
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* MagnitudeSquared																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the magnitude of the vector squared.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to inspect.
		/// </param>
		/// <returns>
		/// The magnitude of the supplied vector squared.
		/// </returns>
		public static float MagnitudeSquared(FVector2 vector)
		{
			float result = 0f;
			double x = 0d;
			double y = 0d;

			if(vector != null)
			{
				x = (double)vector.mX;
				y = (double)vector.mY;
				result = (float)((x * x) + (y * y));
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MiddlePoint																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the middle coordinate between two vectors.
		/// </summary>
		/// <param name="vectorA">
		/// Reference to the first vector to test.
		/// </param>
		/// <param name="vectorB">
		/// Reference to the second vector to test.
		/// </param>
		/// <returns>
		/// Reference to a vector that represents the exact middle coordinate of
		/// the caller's virtual line.
		/// </returns>
		public static FVector2 MiddlePoint(FVector2 vectorA, FVector2 vectorB)
		{
			FVector2 result = new FVector2();

			if(vectorA != null && vectorB != null)
			{
				result.X = (vectorA.X + vectorB.X) / 2f;
				result.Y = (vectorA.Y + vectorB.Y) / 2f;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Negate																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Negate the values of the caller's coordinate.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be negated.
		/// </param>
		/// <returns>
		/// Reference to the caller's vector with negated values.
		/// </returns>
		public static FVector2 Negate(FVector2 vector)
		{
			FVector2 result = new FVector2();

			if (vector != null)
			{
				result.mX = 0f - vector.mX;
				result.mY = 0f - vector.mY;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Normalize																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Normalize the values of the provided vector to unit value.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to convert.
		/// </param>
		/// <returns>
		/// Normalized version of the caller's vector.
		/// </returns>
		/// <remarks>
		/// The normalized value of the vector represents each leg as a percentage
		/// of object's total length.
		/// </remarks>
		public static FVector2 Normalize(FVector2 vector)
		{
			float length = 0f;
			FVector2 result = new FVector2();

			if(vector != null)
			{
				length = Length(vector);
				if(length != 0.0f)
				{
					result.mX = vector.mX / length;
					result.mY = vector.mY / length;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Offset																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a new instance the caller's vector, translated by the specified
		/// offset.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be offset.
		/// </param>
		/// <param name="dx">
		/// X distance from the original vector.
		/// </param>
		/// <param name="dy">
		/// Y distance from the original vector.
		/// </param>
		/// <returns>
		/// Reference to a new vector at the specified offset from the original.
		/// </returns>
		public static FVector2 Offset(FVector2 vector, float dx, float dy)
		{
			FVector2 result = new FVector2(vector.mX + dx, vector.mY + dy);
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Parse																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Parse a coordinate string and return its FVector2 representation.
		/// </summary>
		/// <param name="coordinate">
		/// The coordinate string to parse.
		/// </param>
		/// <param name="allowNull">
		/// Value indicating whether to allow a null return value if the input
		/// string was invalid.
		/// </param>
		/// <returns>
		/// Newly created FVector2 value representing the caller's input, if
		/// that input was legal or allowNull was false. Otherwise, a null value.
		/// </returns>
		public static FVector2 Parse(string coordinate, bool allowNull = false)
		{
			bool bX = false;
			bool bY = false;
			int index = 0;
			MatchCollection matches = null;
			FVector2 result = null;
			string text = "";

			if(coordinate?.Length > 0)
			{
				text = coordinate.Trim();
				if(text.StartsWith("{") && text.EndsWith("}"))
				{
					//	JSON object.
					try
					{
						result = JsonConvert.DeserializeObject<FVector2>(coordinate);
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
				result = new FVector2();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ReadOnly																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="ReadOnly">ReadOnly</see>.
		/// </summary>
		protected bool mReadOnly = false;
		/// <summary>
		/// Get/Set a value indicating whether this item is read-only.
		/// </summary>
		[JsonIgnore]
		public bool ReadOnly
		{
			get { return mReadOnly; }
			set { mReadOnly = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Rotate																																*
		//*-----------------------------------------------------------------------*
		///// <summary>
		///// Rotate the caller's vector around the origin.
		///// </summary>
		///// <param name="x">
		///// The X value to be rotated.
		///// </param>
		///// <param name="y">
		///// The Y value to be rotated.
		///// </param>
		///// <param name="theta">
		///// The angle at which to rotate the vector, in radians.
		///// </param>
		///// <returns>
		///// Reference to a representation of the caller's vector after being
		///// rotated by the specified angle around the origin.
		///// </returns>
		//public static FVector2 Rotate(float x, float y, float theta)
		//{
		//	FVector2 result = new FVector2();

		//	result.mX =
		//		(float)((double)x * Math.Cos((double)theta) -
		//			(double)y * Math.Sin((double)theta));
		//	result.mY =
		//		(float)((double)x * Math.Sin((double)theta) +
		//			(double)y * Math.Cos((double)theta));
		//	return result;
		//}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
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
		public static FVector2 Rotate(FVector2 point, float theta)
		{
			FVector2 result = new FVector2();

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
		/// Scale the caller's vector by a uniform factor.
		/// </summary>
		/// <param name="vector">
		/// The vector to be scaled.
		/// </param>
		/// <param name="scale">
		/// The factor by which the point will be scaled.
		/// </param>
		/// <returns>
		/// Reference to the uniformly scaled vector.
		/// </returns>
		public static FVector2 Scale(FVector2 vector, float scale)
		{
			FVector2 result = new FVector2();

			if(vector != null)
			{
				result.mX = vector.mX * scale;
				result.mY = vector.mY * scale;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Set																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the values of the vector in an abbreviated call.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector whose values will be set.
		/// </param>
		/// <param name="x">
		/// The X value to assign.
		/// </param>
		/// <param name="y">
		/// The Y value to assign.
		/// </param>
		public static void Set(FVector2 vector, float x, float y)
		{
			if(vector != null)
			{
				vector.X = x;
				vector.Y = y;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SetArray																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the specified vector's values from the contents of the provided
		/// array.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector whose values will be converted.
		/// </param>
		/// <param name="values">
		/// Reference to an array of values to transfer to the properties of
		/// the vector.
		/// </param>
		public static void SetArray(FVector2 vector, float[] values)
		{
			if(vector != null && values?.Length > 0)
			{
				vector.mX = values[0];
				if(values.Length > 1)
				{
					vector.mY = values[1];
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TransferValues																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Transfer the member values of one instance to another.
		/// </summary>
		/// <param name="source">
		/// Reference to the source vector whose values will be assigned.
		/// </param>
		/// <param name="target">
		/// Reference to the target vector that will receive the values.
		/// </param>
		public static void TransferValues(FVector2 source, FVector2 target)
		{
			if(source != null && target != null && !target.mReadOnly)
			{
				//	It would be possible to allow a null target, but that strategy
				//	would have to assigned an 'out' value to the parameter, or to
				//	return the newly created value.
				target.X = source.mX;
				target.Y = source.mY;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Translate																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Translate the values of the caller's vector by the provided offset.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be translated.
		/// </param>
		/// <param name="offset">
		/// Reference to the offset to apply to the vector.
		/// </param>
		public static void Translate(FVector2 vector, FVector2 offset)
		{
			if(vector != null && offset != null)
			{
				vector.X += offset.mX;
				vector.Y += offset.mY;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ToString																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the string representation of this item.
		/// </summary>
		/// <returns>
		/// String representation of the values of this point.
		/// </returns>
		public override string ToString()
		{
			StringBuilder result = new StringBuilder();

			result.Append($"{mX:0.000}");
			result.Append(',');
			result.Append($"{mY:0.000}");
			return result.ToString();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	X																																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="X">X</see>.
		/// </summary>
		protected float mX = 0f;
		/// <summary>
		/// Get/Set the X value of the coordinate.
		/// </summary>
		[JsonProperty(Order = 0)]
		public float X
		{
			get { return mX; }
			set
			{
				float original = mX;

				if(!mReadOnly)
				{
					mX = value;
					if(original != value)
					{
						OnCoordinateChanged(
							new FloatPointEventArgs("X", value, original));
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Y																																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Y">Y</see>.
		/// </summary>
		protected float mY = 0f;
		/// <summary>
		/// Get/Set the Y value of the coordinate.
		/// </summary>
		[JsonProperty(Order = 1)]
		public float Y
		{
			get { return mY; }
			set
			{
				float original = mY;

				if(!mReadOnly)
				{
					mY = value;
					if(original != value)
					{
						OnCoordinateChanged(
							new FloatPointEventArgs("Y", value, original));
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Zero																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Zero the values of the specified vector.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be modified.
		/// </param>
		public static void Zero(FVector2 vector)
		{
			if(!vector.mReadOnly)
			{
				vector.mX = 0.0f;
				vector.mY = 0.0f;
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
