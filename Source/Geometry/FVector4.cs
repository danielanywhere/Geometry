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
			mX = x;
			mY = y;
			mZ = z;
			mW = w;
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
				result.X = value.mX;
				result.Y = value.mY;
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

			if(value != null)
			{
				result.X = value.mX;
				result.Y = value.mY;
				result.Z = value.mZ;
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
				result.mX = value.X;
				result.mY = value.Y;
				result.mZ = value.Z;
				result.mW = 1f;
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
				result.mX = value.X;
				result.mY = value.Y;
				result.mZ = value.Z;
				result.mW = 1f;
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
			FVector3 result = new FVector3();

			if(value != null)
			{
				result.X = value.mX;
				result.Y = value.mY;
				result.Z = value.mZ;
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
			FVector2 result = new FVector2();

			if(value != null)
			{
				result.X = value.mX;
				result.Y = value.mY;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Operator -																														*
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
					minuend.mX - subtrahend.mX,
					minuend.mY - subtrahend.mY,
					minuend.mZ - subtrahend.mZ,
					minuend.mW - subtrahend.mW);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Operator *																														*
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
					multiplicand.mX * multiplier,
					multiplicand.mY * multiplier,
					multiplicand.mZ * multiplier,
					multiplicand.mW * multiplier);
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
					multiplicand.mX * multiplier.mX,
					multiplicand.mY * multiplier.mY,
					multiplicand.mZ * multiplier.mZ,
					multiplicand.mW * multiplier.mW);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Operator /																														*
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
					(dividend.mX != 0f ?
						divisor.mX / dividend.mX : 0f),
					(dividend.mY != 0f ?
						divisor.mY / dividend.mY : 0f),
					(dividend.mZ != 0f ?
						divisor.mZ / dividend.mZ : 0f),
					(dividend.mW != 0f ?
						divisor.mW / dividend.mW : 0f));
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Operator +																														*
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
				result.mX = addend1.mX + addend2;
				result.mY = addend1.mY + addend2;
				result.mZ = addend1.mZ + addend2;
				result.mW = addend1.mW + addend2;
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
					addend1.mX + addend2.mX,
					addend1.mY + addend2.mY,
					addend1.mZ + addend2.mZ,
					addend1.mW + addend2.mW);
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
				target.mX = source.mX;
				target.mY = source.mY;
				target.mZ = source.mZ;
				target.mW = source.mW;
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
				vector.mX =
					vector.mY =
					vector.mZ =
					vector.mW = value;
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
				vector.mX = x;
				vector.mY = y;
				vector.mZ = z;
				vector.mW = w;
			}
		}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		///// <summary>
		///// Set the value of the vector.
		///// </summary>
		///// <param name="vector">
		///// Reference to the vector to be populated.
		///// </param>
		///// <param name="values">
		///// Array of values to assign.
		///// </param>
		//public static void Assign(FVector4 vector, float[] values)
		//{
		//	if(vector != null && values?.Length > 0)
		//	{
		//		if(values.Length > vX)
		//		{
		//			vector.mX = values[vX];
		//		}
		//		if(values.Length > vY)
		//		{
		//			vector.mY = values[vY];
		//		}
		//		if(values.Length > vZ)
		//		{
		//			vector.mZ = values[vZ];
		//		}
		//		if(values.Length > vW)
		//		{
		//			vector.mW = values[vW];
		//		}
		//	}
		//}
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
			FVector4 result = new FVector4();

			if(vector != null)
			{
				result.mReadOnly = vector.mReadOnly;
				result.mW = vector.mW;
				result.mX = vector.mX;
				result.mY = vector.mY;
				result.mZ = vector.mZ;
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
					(vector1.mY * vector2.mZ) -
					(vector2.mY * vector1.mZ),
					((vector1.mX * vector2.mZ) -
					(vector2.mX * vector1.mZ) * -1f),
					(vector1.mX * vector2.mY) -
					(vector2.mX * vector1.mY),
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
					(vectorA.mX * vectorB.mX) +
					(vectorA.mY * vectorB.mY) +
					(vectorA.mZ * vectorB.mZ) +
					(vectorA.mW * vectorB.mW);
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
				value.mX == this.mX &&
				value.mY == this.mY &&
				value.mZ == this.mZ &&
				value.mW == this.mW);
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
		public static float[] GetArray(FVector4 vector)
		{
			float[] result = null;

			if(vector != null)
			{
				result = new float[4];
				result[0] = vector.mX;
				result[1] = vector.mY;
				result[2] = vector.mZ;
				result[3] = vector.mW;
			}
			if(result == null)
			{
				result = new float[0];
			}
			return result;
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
				result.mW = point.mW;
				result.mX =
					point.mX +
					(float)((double)length *
						Math.Sin((double)angles.Z) *
						Math.Cos((double)angles.X));
				result.mY =
					point.mY +
					(float)((double)length *
						Math.Sin((double)angles.Z) *
						Math.Sin((double)angles.X));
				result.mZ =
					point.mZ +
					(float)((double)length * Math.Cos((double)angles.Z));
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
				result.mW = pointA.mW;
				vecA = new FVector4(pointA);
				vecB = new FVector4(pointB);
				//	Set the zenith.
				result.mZ =
					Trig.GetLineAngle(
						vecA.mX, vecA.mY,
						vecB.mX, vecB.mY);
				//	Zero the needle for X, Z.
				//	Line length is 2D because we don't know anything about Z when
				//	viewing from the Z axis.
				length = Trig.GetLineDistance(
					vecA.mX, vecA.mY,
					vecB.mX, vecB.mY);
				point = new FPoint(vecA.mX, vecA.mY);
				point = Trig.GetDestPoint(point, 0f, length);
				vecB.mX = point.X;
				vecB.mY = point.Y;
				result.mX =
					Trig.GetLineAngle(
						vecA.mX, vecA.mZ,
						vecB.mX, vecB.mZ);
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
					Math.Pow((double)(vectorB.mX - vectorA.mX), 2d) +
					Math.Pow((double)(vectorB.mY - vectorA.mY), 2d) +
					Math.Pow((double)(vectorB.mZ - vectorA.mZ), 2d));
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
					destinationX - point.mX,
					destinationY - point.mY,
					destinationZ - point.mZ,
					destinationW - point.mW);
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
			bool result = true;

			if(vector != null &&
				(vector.mX != 0f || vector.mY != 0f ||
					vector.mZ != 0f || vector.mW != 0f))
			{
				result = false;
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
					Math.Pow((double)vector.mX, 2d) +
					Math.Pow((double)vector.mY, 2d) +
					Math.Pow((double)vector.mZ, 2d));
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
					result.mX = vector.mX;
				}
				if(m.IndexOf("y") == -1)
				{
					result.mY = vector.mY;
				}
				if(m.IndexOf("z") == -1)
				{
					result.mZ = vector.mZ;
				}
				if(m.IndexOf("w") == -1)
				{
					result.mW = vector.mW;
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
					result.mX = vector.mX / length;
					result.mY = vector.mY / length;
					result.mZ = vector.mZ / length;
					result.mW = vector.mW / length;
				}
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
				result.mX = vector.mX * -1f;
				result.mY = vector.mY * -1f;
				result.mZ = vector.mZ * -1f;
				result.mW = vector.mW * -1f;
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
		public static void SetArray(FVector4 vector, float[] values)
		{
			if(vector != null && values?.Length > 0)
			{
				vector.mX = values[0];
				if(values.Length > 1)
				{
					vector.mY = values[1];
				}
				if(values.Length > 2)
				{
					vector.mZ = values[2];
				}
				if(values.Length > 3)
				{
					vector.mW = values[3];
				}
			}
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
					vector.mX +
					vector.mY +
					vector.mZ +
					vector.mW;
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
					vector.mX,
					vector.mZ,
					vector.mY,
					vector.mW);
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
					Trig.RadToDeg(rayAngle.mX),
					Trig.RadToDeg(rayAngle.mY),
					Trig.RadToDeg(rayAngle.mZ),
					Trig.RadToDeg(rayAngle.mW));
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
				$"X:{mX:0.000}, " +
				$"Y:{mY:0.000}, " +
				$"Z:{mZ:0.000}, " +
				$"W:{mW:0.000}";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	W																																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="W">W</see>.
		/// </summary>
		protected float mW = 0f;
		/// <summary>
		/// Get/Set the W value for this coordinate.
		/// </summary>
		[JsonProperty(Order = 3)]
		public float W
		{
			get { return mW; }
			set
			{
				float original = mW;

				if(!mReadOnly)
				{
					mW = value;
					if(original != value)
					{
						OnCoordinateChanged(
							new FloatPointEventArgs("W", value, original));
					}
				}
			}
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
		/// Get/Set the x coordinate.
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
		/// Get/Set the y coordinate.
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
		//*	Z																																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Z">Z</see>.
		/// </summary>
		protected float mZ = 0f;
		/// <summary>
		/// Get/Set the y coordinate.
		/// </summary>
		[JsonProperty(Order = 1)]
		public float Z
		{
			get { return mZ; }
			set
			{
				float original = mZ;

				if(!mReadOnly)
				{
					mZ = value;
					if(original != value)
					{
						OnCoordinateChanged(
							new FloatPointEventArgs("Z", value, original));
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
