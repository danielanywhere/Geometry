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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FVector4																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Single precision floating point 4D Vector.
	/// </summary>
	public class FVector4
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		/// <summary>
		/// The constant X index in the vector.
		/// </summary>
		private const int vX = 0;
		/// <summary>
		/// The constant Y index in the vector.
		/// </summary>
		private const int vY = 1;
		/// <summary>
		/// The constant Z index in the vector.
		/// </summary>
		private const int vZ = 2;
		/// <summary>
		/// The constant W index in the vector.
		/// </summary>
		private const int vW = 3;

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
		/// Create a new instance of the FVector4 item.
		/// </summary>
		public FVector4()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FVector4 item.
		/// </summary>
		/// <param name="x">
		/// X magnitude.
		/// </param>
		/// <param name="y">
		/// Y magnitude.
		/// </param>
		/// <param name="z">
		/// Z magnitude.
		/// </param>
		/// <param name="w">
		/// W magnitude.
		/// </param>
		public FVector4(float x, float y, float z, float w) : this()
		{
			mValues[vX] = x;
			mValues[vY] = y;
			mValues[vZ] = z;
			mValues[vW] = w;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FVector4 item.
		/// </summary>
		/// <param name="vector">
		/// Reference to a vector whose values will be copied.
		/// </param>
		public FVector4(FVector4 vector)
		{
			Assign(vector, this);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	- operator overload																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of one vector subtracted from another.
		/// </summary>
		/// <param name="minuend">
		/// The part to be subtracted from.
		/// </param>
		/// <param name="subtrahend">
		/// The amount to subtract.
		/// </param>
		/// <returns>
		/// Reference to the vector subtraction result.
		/// </returns>
		public static FVector4 operator -(FVector4 minuend, FVector4 subtrahend)
		{
			FVector4 result = new FVector4();

			if(minuend != null && subtrahend != null)
			{
				Assign(result,
					minuend.mValues[vX] - subtrahend.mValues[vX],
					minuend.mValues[vY] - subtrahend.mValues[vY],
					minuend.mValues[vZ] - subtrahend.mValues[vZ],
					minuend.mValues[vW] - subtrahend.mValues[vW]);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	* operator overload																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of a vector multiplied by a scalar value.
		/// </summary>
		/// <param name="multiplicand">
		/// Reference to the multiplicand.
		/// </param>
		/// <param name="multiplier">
		/// Reference to the multiplier.
		/// </param>
		/// <returns>
		/// Reference to a new vector representing the result of the
		/// multiplication.
		/// </returns>
		public static FVector4 operator *(FVector4 multiplicand, float multiplier)
		{
			FVector4 result = new FVector4();

			if(multiplicand != null)
			{
				Assign(result,
					multiplicand.mValues[vX] * multiplier,
					multiplicand.mValues[vY] * multiplier,
					multiplicand.mValues[vZ] * multiplier,
					multiplicand.mValues[vW] * multiplier);
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the result of two vectors multiplied by one another.
		/// </summary>
		/// <param name="multiplicand">
		/// Reference to the multiplicand.
		/// </param>
		/// <param name="multiplier">
		/// Reference to the multiplier.
		/// </param>
		/// <returns>
		/// Reference to a new vector representing the result of the
		/// multiplication.
		/// </returns>
		public static FVector4 operator *(FVector4 multiplicand,
			FVector4 multiplier)
		{
			FVector4 result = new FVector4();

			if(multiplicand != null && multiplier != null)
			{
				Assign(result,
					multiplicand.mValues[vX] * multiplier.mValues[vX],
					multiplicand.mValues[vY] * multiplier.mValues[vY],
					multiplicand.mValues[vZ] * multiplier.mValues[vZ],
					multiplicand.mValues[vW] * multiplier.mValues[vW]);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	/ operator overload																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of one vector divided by the other.
		/// </summary>
		/// <param name="divisor">
		/// A reference to the divisor.
		/// </param>
		/// <param name="dividend">
		/// A reference to the dividend.
		/// </param>
		/// <returns>
		/// Reference to the vector division result.
		/// </returns>
		public static FVector4 operator /(FVector4 divisor, FVector4 dividend)
		{
			FVector4 result = new FVector4();

			if(divisor != null && dividend != null)
			{
				Assign(result,
					(dividend.mValues[vX] != 0f ?
						divisor.mValues[vX] / dividend.mValues[vX] : 0f),
					(dividend.mValues[vY] != 0f ?
						divisor.mValues[vY] / dividend.mValues[vY] : 0f),
					(dividend.mValues[vZ] != 0f ?
						divisor.mValues[vZ] / dividend.mValues[vZ] : 0f),
					(dividend.mValues[vW] != 0f ?
						divisor.mValues[vW] / dividend.mValues[vW] : 0f));
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	+ operator overload																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of a vector added by scalar value.
		/// </summary>
		/// <param name="addend1">
		/// Reference to the vector whose values will be added.
		/// </param>
		/// <param name="addend2">
		/// The scalar value to add.
		/// </param>
		/// <returns>
		/// Reference to a new vector representing the result of the addition.
		/// </returns>
		public static FVector4 operator +(FVector4 addend1, float addend2)
		{
			FVector4 result = new FVector4();

			if(addend1 != null)
			{
				result.mValues[vX] = addend1.mValues[vX] + addend2;
				result.mValues[vY] = addend1.mValues[vY] + addend2;
				result.mValues[vZ] = addend1.mValues[vZ] + addend2;
				result.mValues[vW] = addend1.mValues[vW] + addend2;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the result of the addition of two vectors.
		/// </summary>
		/// <param name="addend1">
		/// Reference to the first addend.
		/// </param>
		/// <param name="addend2">
		/// Reference to the second addend.
		/// </param>
		/// <returns>
		/// Reference to the vector addition result.
		/// </returns>
		public static FVector4 operator +(FVector4 addend1, FVector4 addend2)
		{
			FVector4 result = new FVector4();

			if(addend1 != null && addend2 != null)
			{
				Assign(result,
					addend1.mValues[vX] + addend2.mValues[vX],
					addend1.mValues[vY] + addend2.mValues[vY],
					addend1.mValues[vZ] + addend2.mValues[vZ],
					addend1.mValues[vW] + addend2.mValues[vW]);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Implicit FPoint = FVector4																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cast the FVector4 instance to an FPoint.
		/// </summary>
		/// <param name="value">
		/// Reference to the vector to be converted.
		/// </param>
		/// <returns>
		/// Reference to a newly created FPoint representing the values in
		/// the caller's vector.
		/// </returns>
		public static implicit operator FPoint(FVector4 value)
		{
			FPoint result = new FPoint();

			if(value != null)
			{
				if(value.mValues.Length > vX)
				{
					result.X = value.mValues[vX];
				}
				if(value.mValues.Length > vY)
				{
					result.Y = value.mValues[vY];
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Implicit FPoint3 = FVector4																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cast the FVector4 instance to an FPoint3.
		/// </summary>
		/// <param name="value">
		/// Reference to the FVector3 value to be converted.
		/// </param>
		/// <returns>
		/// Reference to a newly created FPoint3 whose values represent those
		/// in the caller's FVector3 source.
		/// </returns>
		public static implicit operator FPoint3(FVector4 value)
		{
			FPoint3 result = new FPoint3();

			if(value?.mValues.Length > 0)
			{
				if(value.mValues.Length > vX)
				{
					result.X = value.mValues[vX];
				}
				if(value.mValues.Length > vY)
				{
					result.Y = value.mValues[vY];
				}
				if(value.mValues.Length > vZ)
				{
					result.Z = value.mValues[vZ];
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Implicit FVector4 = FPoint3																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cast the FPoint3 instance to an FVector4.
		/// </summary>
		/// <param name="value">
		/// Reference to the point to be converted.
		/// </param>
		/// <returns>
		/// Reference to a newly created FVector4 representing the values in
		/// the caller's point.
		/// </returns>
		public static implicit operator FVector4(FPoint3 value)
		{
			FVector4 result = new FVector4();

			if(value != null)
			{
				result.mValues[vX] = value.X;
				result.mValues[vY] = value.Y;
				result.mValues[vZ] = value.Z;
				result.mValues[vW] = 1f;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Implicit FVector4 = FVector3																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cast the FVector3 instance to an FVector4.
		/// </summary>
		/// <param name="value">
		/// Reference to the point to be converted.
		/// </param>
		/// <returns>
		/// Reference to a newly created FVector4 representing the values in
		/// the caller's point.
		/// </returns>
		public static implicit operator FVector4(FVector3 value)
		{
			FVector4 result = new FVector4();

			if(value != null)
			{
				result.mValues[vX] = value.X;
				result.mValues[vY] = value.Y;
				result.mValues[vZ] = value.Z;
				result.mValues[vW] = 1f;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Implicit FVector3 = FVector4																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cast the FVector4 instance to an FVector3.
		/// </summary>
		/// <param name="value">
		/// Reference to the FVector4 value to be converted.
		/// </param>
		/// <returns>
		/// Reference to a newly created FVector3 whose values represent those
		/// in the caller's FVector4 source.
		/// </returns>
		public static implicit operator FVector3(FVector4 value)
		{
			int count = 0;
			int index = 0;
			FVector3 result = new FVector3();

			if(value?.mValues.Length > 0)
			{
				count = Math.Min(result.Values.Length, value.mValues.Length);
				for(index = 0; index < count; index++)
				{
					result.Values[index] = value.mValues[index];
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Implicit FVector2 = FVector4																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cast the FVector4 instance to an FVector2.
		/// </summary>
		/// <param name="value">
		/// Reference to the FVector4 value to be converted.
		/// </param>
		/// <returns>
		/// Reference to a newly created FVector2 whose values represent those
		/// in the caller's FVector3 source.
		/// </returns>
		public static implicit operator FVector2(FVector4 value)
		{
			int count = 0;
			int index = 0;
			FVector2 result = new FVector2();

			if(value?.mValues.Length > 0)
			{
				count = Math.Min(result.Values.Length, value.mValues.Length);
				for(index = 0; index < count; index++)
				{
					result.Values[index] = value.mValues[index];
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Assign																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the base values of the target vector from the source.
		/// </summary>
		/// <param name="source">
		/// Reference to the source vector.
		/// </param>
		/// <param name="target">
		/// Reference to the target vector.
		/// </param>
		public static void Assign(FVector4 source, FVector4 target)
		{
			if(source != null && target != null)
			{
				target.mValues[vX] = source.mValues[vX];
				target.mValues[vY] = source.mValues[vY];
				target.mValues[vZ] = source.mValues[vZ];
				target.mValues[vW] = source.mValues[vW];
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Set the value of the vector.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be populated.
		/// </param>
		/// <param name="value">
		/// Value to assign.
		/// </param>
		public static void Assign(FVector4 vector, float value)
		{
			if(vector != null)
			{
				vector.mValues[vX] =
					vector.mValues[vY] =
					vector.mValues[vZ] =
					vector.mValues[vW] = value;
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Set the value of the vector.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be populated.
		/// </param>
		/// <param name="x">
		/// X value to assign.
		/// </param>
		/// <param name="y">
		/// Y value to assign.
		/// </param>
		/// <param name="z">
		/// Z value to assign.
		/// </param>
		/// <param name="w">
		/// W value to assign.
		/// </param>
		public static void Assign(FVector4 vector, float x, float y, float z,
			float w)
		{
			if(vector != null)
			{
				vector.mValues[vX] = x;
				vector.mValues[vY] = y;
				vector.mValues[vZ] = z;
				vector.mValues[vW] = w;
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Set the value of the vector.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be populated.
		/// </param>
		/// <param name="values">
		/// Array of values to assign.
		/// </param>
		public static void Assign(FVector4 vector, float[] values)
		{
			if(vector != null && values?.Length > 0)
			{
				if(values.Length > vX)
				{
					vector.mValues[vX] = values[vX];
				}
				if(values.Length > vY)
				{
					vector.mValues[vY] = values[vY];
				}
				if(values.Length > vZ)
				{
					vector.mValues[vZ] = values[vZ];
				}
				if(values.Length > vW)
				{
					vector.mValues[vW] = values[vW];
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a deep clone of the caller's vector.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to clone.
		/// </param>
		/// <returns>
		/// Reference to the deep clone of the caller's value, if eligible.
		/// Otherwise, a blank FVector3.
		/// </returns>
		public static FVector4 Clone(FVector4 vector)
		{
			int count = 0;
			int index = 0;
			FVector4 result = new FVector4();

			if(vector != null)
			{
				count = vector.mValues.Length;
				if(result.mValues.Length != count)
				{
					result.mValues = new float[count];
				}
				for(index = 0; index < count; index++)
				{
					result.mValues[index] = vector.mValues[index];
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	CrossProduct																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the cross product of two vectors.
		/// </summary>
		/// <param name="vector1">
		/// Reference to the first vector.
		/// </param>
		/// <param name="vector2">
		/// Reference to the second vector.
		/// </param>
		/// <returns>
		/// Reference to the result of the cross product operation.
		/// </returns>
		/// <remarks>
		/// This version performs a 3D cross-product, ignoring the W element with
		/// the assumption that the usage case is with 3D coordinates.
		/// </remarks>
		public static FVector4 CrossProduct(FVector4 vector1, FVector4 vector2)
		{
			FVector4 result = new FVector4();

			if(vector1 != null && vector2 != null)
			{
				Assign(result,
					(vector1.mValues[vY] * vector2.mValues[vZ]) -
					(vector2.mValues[vY] * vector1.mValues[vZ]),
					((vector1.mValues[vX] * vector2.mValues[vZ]) -
					(vector2.mValues[vX] * vector1.mValues[vZ]) * -1f),
					(vector1.mValues[vX] * vector2.mValues[vY]) -
					(vector2.mValues[vX] * vector1.mValues[vY]),
					0f);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DotProduct																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the dot product of the two vectors.
		/// </summary>
		/// <param name="vectorA">
		/// Vector A.
		/// </param>
		/// <param name="vectorB">
		/// Vector B.
		/// </param>
		/// <returns>
		/// The dot product of the two vectors.
		/// </returns>
		public static float DotProduct(FVector4 vectorA, FVector4 vectorB)
		{
			float result = 0f;

			if(vectorA != null && vectorB != null)
			{
				result =
					(vectorA.mValues[vX] * vectorB.mValues[vX]) +
					(vectorA.mValues[vY] * vectorB.mValues[vY]) +
					(vectorA.mValues[vZ] * vectorB.mValues[vZ]) +
					(vectorA.mValues[vW] * vectorB.mValues[vW]);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Equals																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the properties of another vector are
		/// equal to this one.
		/// </summary>
		/// <param name="value">
		/// Reference to the value to compare.
		/// </param>
		/// <returns>
		/// True if the properties in the other object are the same as this one.
		/// Otherwise, false.
		/// </returns>
		public bool Equals(FVector4 value)
		{
			return (value != null &&
				value.mValues[vX] == this.mValues[vX] &&
				value.mValues[vY] == this.mValues[vY] &&
				value.mValues[vZ] == this.mValues[vZ] &&
				value.mValues[vW] == this.mValues[vW]);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetDestPoint																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the destination point from the caller's starting point, angles,
		/// and length.
		/// </summary>
		/// <param name="point">
		/// Coordinates of the starting point.
		/// </param>
		/// <param name="angles">
		/// Angles to apply, in radians. Notice that the Y rotation is ignored.
		/// X-axis is azimuth and Z-axis is zenith.
		/// </param>
		/// <param name="length">
		/// Length of the line.
		/// </param>
		/// <returns>
		/// Destination point of the line.
		/// </returns>
		/// <remarks>
		/// This version transfers the W coordinate of the caller's point
		/// directly to the result and solves for X, Y, and Z.
		/// </remarks>
		public static FVector4 GetDestPoint(FVector4 point, FVector3 angles,
			float length)
		{
			FVector4 result = new FVector4();

			if(point != null && angles != null && length != 0.0)
			{
				result.mValues[vW] = point.mValues[vW];
				result.mValues[vX] =
					point.mValues[vX] +
					(float)((double)length *
						Math.Sin((double)angles.Values[vZ]) *
						Math.Cos((double)angles.Values[vX]));
				result.mValues[vY] =
					point.mValues[vY] +
					(float)((double)length *
						Math.Sin((double)angles.Values[vZ]) *
						Math.Sin((double)angles.Values[vX]));
				result.mValues[vZ] =
					point.mValues[vZ] +
					(float)((double)length * Math.Cos((double)angles.Values[vZ]));
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetLineAngle																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the ray angles between two points.
		/// </summary>
		/// <param name="pointA">
		/// Coordinates of the first point.
		/// </param>
		/// <param name="pointB">
		/// Coordinates of the second point.
		/// </param>
		/// <returns>
		/// The X-axis (azimuth) and Z-axis (zenith) angles of the ray from
		/// point A to point B.
		/// </returns>
		/// <remarks>
		/// This version transfers the value of W from the caller's pointA source
		/// to the result, then solves for X, Y, Z.
		/// </remarks>
		public static FVector4 GetLineAngle(FVector4 pointA, FVector4 pointB)
		{
			float length = 0f;
			FPoint point = null;
			FVector4 result = new FVector4();
			FVector4 vecA = null;
			FVector4 vecB = null;

			if(pointA != null & pointB != null)
			{
				result.mValues[vW] = pointA.mValues[vW];
				vecA = new FVector4(pointA);
				vecB = new FVector4(pointB);
				//	Set the zenith.
				result.mValues[vZ] =
					Trig.GetLineAngle(
						vecA.mValues[vX], vecA.mValues[vY],
						vecB.mValues[vX], vecB.mValues[vY]);
				//	Zero the needle for X, Z.
				//	Line length is 2D because we don't know anything about Z when
				//	viewing from the Z axis.
				length = Trig.GetLineDistance(
					vecA.mValues[vX], vecA.mValues[vY],
					vecB.mValues[vX], vecB.mValues[vY]);
				point = new FPoint(vecA.mValues[vX], vecA.mValues[vY]);
				point = Trig.GetDestPoint(point, 0f, length);
				vecB.mValues[vX] = point.X;
				vecB.mValues[vY] = point.Y;
				result.mValues[vX] =
					Trig.GetLineAngle(
						vecA.mValues[vX], vecA.mValues[vZ],
						vecB.mValues[vX], vecB.mValues[vZ]);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetLineDist																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the distance of the line between the tips of two vectors.
		/// </summary>
		/// <param name="vectorA">
		/// First vector tip.
		/// </param>
		/// <param name="vectorB">
		/// Second vector tip.
		/// </param>
		/// <returns>
		/// Distance of line between two points.
		/// </returns>
		/// <remarks>
		/// This version ignores the value of W, solving for X, Y, Z.
		/// </remarks>
		public static float GetLineDistance(FVector4 vectorA, FVector4 vectorB)
		{
			float result = 0f;

			if(vectorA != null && vectorB != null)
			{
				return (float)Math.Sqrt(
					Math.Pow((double)(vectorB.mValues[vX] - vectorA.mValues[vX]), 2d) +
					Math.Pow((double)(vectorB.mValues[vY] - vectorA.mValues[vY]), 2d) +
					Math.Pow((double)(vectorB.mValues[vZ] - vectorA.mValues[vZ]), 2d));
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetMagnitude																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the raw lengths of each axis from point A to point B as a ray.
		/// </summary>
		/// <param name="point">
		/// Starting point.
		/// </param>
		/// <param name="destinationX">
		/// Destination X coordinate.
		/// </param>
		/// <param name="destinationY">
		/// Destination Y coordinate.
		/// </param>
		/// <param name="destinationZ">
		/// Destination Z coordinate.
		/// </param>
		/// <param name="destinationW">
		/// Destination W coordinate.
		/// </param>
		/// <returns>
		/// Ray of length values per axis.
		/// </returns>
		public static FVector4 GetMagnitude(FVector4 point,
			float destinationX, float destinationY, float destinationZ,
			float destinationW)
		{
			FVector4 result = new FVector4();

			if(point != null)
			{
				Assign(result,
					destinationX - point.mValues[vX],
					destinationY - point.mValues[vY],
					destinationZ - point.mValues[vZ],
					destinationW - point.mValues[vW]);
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the raw lengths of each axis from point A to point B as a ray.
		/// </summary>
		/// <param name="pointA">
		/// Starting point.
		/// </param>
		/// <param name="pointB">
		/// Ending point.
		/// </param>
		/// <returns>
		/// Ray of length values per axis.
		/// </returns>
		public static FVector4 GetMagnitude(FVector4 pointA, FVector4 pointB)
		{
			FVector4 result = new FVector4();

			if(pointA != null && pointB != null)
			{
				result = pointB - pointA;
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
		public static bool IsEmpty(FVector4 vector)
		{
			int count = 0;
			int index = 0;
			bool result = true;

			if(vector != null &&
				vector.mValues.Length > 0)
			{
				count = vector.mValues.Length;
				for(index = 0; index < count; index++)
				{
					if(vector.mValues[index] != 0f)
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	IsZero																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the vector value is zero.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to inspect.
		/// </param>
		/// <returns>
		/// True if all of the member values of the vector are zero. Otherwise,
		/// false.
		/// </returns>
		public static bool IsZero(FVector4 vector)
		{
			return IsEmpty(vector);
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
		/// <remarks>
		/// This version ignores the value of W.
		/// </remarks>
		public static float Length(FVector4 vector)
		{
			float result = 0f;

			if(vector != null)
			{
				result = (float)Math.Sqrt(
					Math.Pow((double)vector.mValues[vX], 2d) +
					Math.Pow((double)vector.mValues[vY], 2d) +
					Math.Pow((double)vector.mValues[vZ], 2d));
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Mask																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Mask one or more values out of the caller's vector.
		/// </summary>
		/// <param name="vector">
		/// Vector to copy.
		/// </param>
		/// <param name="mask">
		/// Names of one or more axis to mask from the return value.
		/// </param>
		/// <returns>
		/// Copy of caller's vector, where the specified axis have been masked out.
		/// </returns>
		public static FVector4 Mask(FVector4 vector, string mask)
		{
			string m = "";
			FVector4 result = new FVector4();

			if(mask?.Length > 0)
			{
				m = mask.ToLower();
				if(m.IndexOf("x") == -1)
				{
					result.mValues[vX] = vector.mValues[vX];
				}
				if(m.IndexOf("y") == -1)
				{
					result.mValues[vY] = vector.mValues[vY];
				}
				if(m.IndexOf("z") == -1)
				{
					result.mValues[vZ] = vector.mValues[vZ];
				}
				if(m.IndexOf("w") == -1)
				{
					result.mValues[vW] = vector.mValues[vW];
				}
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
		public static FVector4 Normalize(FVector4 vector)
		{
			float length = 0f;
			FVector4 result = new FVector4();

			if(vector != null)
			{
				length = Length(vector);
				if(length != 0.0)
				{
					result.mValues[0] = vector.mValues[0] / length;
					result.mValues[1] = vector.mValues[1] / length;
					result.mValues[2] = vector.mValues[2] / length;
					result.mValues[3] = vector.mValues[3] / length;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ReverseDirection																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Reverse the direction of the specified vector.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be reversed.
		/// </param>
		/// <returns>
		/// Reference to a new vector with all of the dimensions reversed.
		/// </returns>
		public static FVector4 ReverseDirection(FVector4 vector)
		{
			FVector4 result = new FVector4();

			if(vector != null)
			{
				result.mValues[vX] = vector.mValues[vX] * -1f;
				result.mValues[vY] = vector.mValues[vY] * -1f;
				result.mValues[vZ] = vector.mValues[vZ] * -1f;
				result.mValues[vW] = vector.mValues[vW] * -1f;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Scale																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the scaled vector.
		/// </summary>
		/// <param name="vector">
		/// Vector to scale.
		/// </param>
		/// <param name="scale">
		/// Scale to apply.
		/// </param>
		/// <returns>
		/// Vector with scaling applied.
		/// </returns>
		public static FVector4 Scale(FVector4 vector, FVector4 scale)
		{
			FVector4 result = null;

			if(vector != null && scale != null)
			{
				result = vector * scale;
			}
			else
			{
				result = new FVector4();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SetVectorLength																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a new vector of direction 'vector' with length = 'size'.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be converted.
		/// </param>
		/// <param name="size">
		/// Target size of the new vector.
		/// </param>
		/// <returns>
		/// Converted vector of specified size.
		/// </returns>
		public static FVector4 SetVectorLength(FVector4 vector, float size)
		{
			//	Normalize the vector.
			FVector4 vectorNormalized = FVector4.Normalize(vector);

			//scale the vector
			return vectorNormalized *= size;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Sum																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the sum of elements in the caller's vector.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be summed.
		/// </param>
		/// <returns>
		/// The sum of the values in the presented vector.
		/// </returns>
		public static float Sum(FVector4 vector)
		{
			float result = 0f;

			if(vector != null)
			{
				result =
					vector.mValues[vX] +
					vector.mValues[vY] +
					vector.mValues[vZ] +
					vector.mValues[vW];
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SwapYZ																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Swap the values of the Y and Z axis.
		/// </summary>
		/// <param name="vector">
		/// Vector containing values to swap.
		/// </param>
		/// <returns>
		/// Copy of the caller's vector where the handedness of the object
		/// has been reversed.
		/// </returns>
		public static FVector4 SwapYZ(FVector4 vector)
		{
			FVector4 result = new FVector4();

			if(vector != null)
			{
				Assign(result,
					vector.mValues[vX],
					vector.mValues[vZ],
					vector.mValues[vY],
					vector.mValues[vW]);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ToDeg																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the ray angle, expressed in degrees.
		/// </summary>
		/// <param name="rayAngle">
		/// Ray angles.
		/// </param>
		/// <returns>
		/// Angles of ray, expressed in degrees.
		/// </returns>
		public static FVector4 ToDeg(FVector4 rayAngle)
		{
			FVector4 result = new FVector4();

			if(rayAngle != null)
			{
				Assign(result,
					Trig.RadToDeg(rayAngle.mValues[vX]),
					Trig.RadToDeg(rayAngle.mValues[vY]),
					Trig.RadToDeg(rayAngle.mValues[vZ]),
					Trig.RadToDeg(rayAngle.mValues[vW]));
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TransferValues																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Transfer the core values of one vector to another.
		/// </summary>
		/// <param name="source">
		/// Reference to the source to be cloned.
		/// </param>
		/// <param name="target">
		/// Reference to the target to be assigned.
		/// </param>
		public static void TransferValues(FVector4 source, FVector4 target)
		{
			Assign(source, target);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ToString																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the string representation of this item.
		/// </summary>
		/// <returns>
		/// String representation of this item.
		/// </returns>
		public override string ToString()
		{
			return
				$"X:{mValues[vX]:0.000}, " +
				$"Y:{mValues[vY]:0.000}, " +
				$"Z:{mValues[vZ]:0.000}, " +
				$"W:{mValues[vW]:0.000}";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Values																																*
		//*-----------------------------------------------------------------------*
		private float[] mValues = new float[4];
		/// <summary>
		/// Get/Set a reference to the base array of values.
		/// </summary>
		[JsonIgnore]
		public float[] Values
		{
			get { return mValues; }
			set { mValues = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	W																																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the W coordinate of this value.
		/// </summary>
		[JsonProperty(Order = 3)]
		public float W
		{
			get { return mValues[vW]; }
			set { mValues[vW] = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	X																																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the X coordinate of this value.
		/// </summary>
		[JsonProperty(Order = 0)]
		public float X
		{
			get { return mValues[vX]; }
			set { mValues[vX] = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Y																																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the Y coordinate of this value.
		/// </summary>
		[JsonProperty(Order = 1)]
		public float Y
		{
			get { return mValues[vY]; }
			set { mValues[vY] = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Z																																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the Z coordinate of this value.
		/// </summary>
		[JsonProperty(Order = 2)]
		public float Z
		{
			get { return mValues[vZ]; }
			set { mValues[vZ] = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
