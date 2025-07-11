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
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

using static Geometry.GeometryUtil;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FVector3																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Single floating point 3D Vector.
	/// </summary>
	public class FVector3
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
		/// Create a new instance of the FVector3 Item.
		/// </summary>
		public FVector3()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FVector3 Item.
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
		public FVector3(float x, float y, float z) : this()
		{
			mX = x;
			mY = y;
			mZ = z;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FVector3 Item.
		/// </summary>
		/// <param name="vector">
		/// Reference to a vector whose values will be copied.
		/// </param>
		public FVector3(FVector3 vector)
		{
			Assign(vector, this);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Implicit FPoint = FVector3																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cast the FVector3 instance to an FPoint.
		/// </summary>
		/// <param name="value">
		/// Reference to the vector to be converted.
		/// </param>
		/// <returns>
		/// Reference to a newly created FPoint representing the values in
		/// the caller's vector.
		/// </returns>
		public static implicit operator FPoint(FVector3 value)
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

		////*-----------------------------------------------------------------------*
		////*	_Implicit FPoint3 = FVector3																					*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast the FVector3 instance to an FPoint3.
		///// </summary>
		///// <param name="value">
		///// Reference to the FVector3 value to be converted.
		///// </param>
		///// <returns>
		///// Reference to a newly created FPoint3 whose values represent those
		///// in the caller's FVector3 source.
		///// </returns>
		//public static implicit operator FPoint3(FVector3 value)
		//{
		//	FPoint3 result = new FPoint3();

		//	if(value?.mValues.Length > 0)
		//	{
		//		result.X = value.mX;
		//		if(value.mValues.Length > vY)
		//		{
		//			result.Y = value.mY;
		//		}
		//		if(value.mValues.Length > vZ)
		//		{
		//			result.Z = value.mZ;
		//		}
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Implicit FVector3 = FPoint3																					*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast the FPoint3 instance to an FVector3.
		///// </summary>
		///// <param name="value">
		///// Reference to the point to be converted.
		///// </param>
		///// <returns>
		///// Reference to a newly created FVector3 representing the values in
		///// the caller's point.
		///// </returns>
		//public static implicit operator FVector3(FPoint3 value)
		//{
		//	FVector3 result = new FVector3();

		//	if(value != null)
		//	{
		//		result.mX = value.X;
		//		result.mY = value.Y;
		//		result.mZ = value.Z;
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Implicit FVector2 = FVector3																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cast the FVector3 instance to an FVector2.
		/// </summary>
		/// <param name="value">
		/// Reference to the FVector3 value to be converted.
		/// </param>
		/// <returns>
		/// Reference to a newly created FVector2 whose values represent those
		/// in the caller's FVector3 source.
		/// </returns>
		public static implicit operator FVector2(FVector3 value)
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
		public static FVector3 operator -(FVector3 minuend, FVector3 subtrahend)
		{
			FVector3 result = new FVector3();

			if(minuend != null && subtrahend != null)
			{
				Assign(result,
					minuend.mX - subtrahend.mX,
					minuend.mY - subtrahend.mY,
					minuend.mZ - subtrahend.mZ);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Operator *																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of a vector multiplied by a scalar.
		/// </summary>
		/// <param name="multiplicand">
		/// The scalar value to multiply.
		/// </param>
		/// <param name="multiplier">
		/// The vector to multiply.
		/// </param>
		/// <returns>
		/// Result of the multiplication.
		/// </returns>
		public static FVector3 operator *(float multiplicand, FVector3 multiplier)
		{
			FVector3 result = new FVector3();

			if(multiplier != null && multiplicand != 0f)
			{
				result.mX = multiplicand * multiplier.mX;
				result.mY = multiplicand * multiplier.mY;
				result.mZ = multiplicand * multiplier.mZ;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
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
		public static FVector3 operator *(FVector3 multiplicand, float multiplier)
		{
			FVector3 result = new FVector3();

			if(multiplicand != null)
			{
				Assign(result,
					multiplicand.mX * multiplier,
					multiplicand.mY * multiplier,
					multiplicand.mZ * multiplier);
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
		public static FVector3 operator *(FVector3 multiplicand,
			FVector3 multiplier)
		{
			FVector3 result = new FVector3();

			if(multiplicand != null && multiplier != null)
			{
				Assign(result,
					multiplicand.mX * multiplier.mX,
					multiplicand.mY * multiplier.mY,
					multiplicand.mZ * multiplier.mZ);
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
		public static FVector3 operator /(FVector3 divisor, FVector3 dividend)
		{
			FVector3 result = new FVector3();

			if(divisor != null && dividend != null)
			{
				Assign(result,
					(dividend.mX != 0f ?
						divisor.mX / dividend.mX : 0f),
					(dividend.mY != 0f ?
						divisor.mY / dividend.mY : 0f),
					(dividend.mZ != 0f ?
						divisor.mZ / dividend.mZ : 0f));
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
		/// The scalar value to add.
		/// </param>
		/// <param name="addend2">
		/// Reference to the vector whose values will be added.
		/// </param>
		/// <returns>
		/// Reference to a new vector representing the result of the addition.
		/// </returns>
		public static FVector3 operator +(float addend1, FVector3 addend2)
		{
			FVector3 result = new FVector3();

			if(addend2 != null)
			{
				result.mX = addend2.mX + addend1;
				result.mY = addend2.mY + addend1;
				result.mZ = addend2.mZ + addend1;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
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
		public static FVector3 operator +(FVector3 addend1, float addend2)
		{
			FVector3 result = new FVector3();

			if(addend1 != null)
			{
				result.mX = addend1.mX + addend2;
				result.mY = addend1.mY + addend2;
				result.mZ = addend1.mZ + addend2;
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
		public static FVector3 operator +(FVector3 addend1, FVector3 addend2)
		{
			FVector3 result = new FVector3();

			if(addend1 != null && addend2 != null)
			{
				Assign(result,
					addend1.mX + addend2.mX,
					addend1.mY + addend2.mY,
					addend1.mZ + addend2.mZ);
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
		public static bool operator !=(FVector3 vectorA, FVector3 vectorB)
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
		/// Return a value indicating whether values of two vectors are equal.
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
		public static bool operator ==(FVector3 vectorA, FVector3 vectorB)
		{
			bool result = true;

			if((object)vectorA != null && (object)vectorB != null)
			{
				result = (
					(vectorA.mX == vectorB.mX) &&
					(vectorA.mY == vectorB.mY) &&
					(vectorA.mZ == vectorB.mZ));
			}
			else if((object)vectorA != null || (object)vectorB != null)
			{
				result = false;
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
		public static void Assign(FVector3 source, FVector3 target)
		{
			if(source != null && target != null)
			{
				target.mX = source.mX;
				target.mY = source.mY;
				target.mZ = source.mZ;
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
		public static void Assign(FVector3 vector, float value)
		{
			if(vector != null)
			{
				vector.mX =
					vector.mY =
					vector.mZ = value;
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
		public static void Assign(FVector3 vector, float x, float y, float z)
		{
			if(vector != null)
			{
				vector.mX = x;
				vector.mY = y;
				vector.mZ = z;
			}
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
		public static void Clear(FVector3 vector)
		{
			if(vector != null && !vector.mReadOnly)
			{
				vector.X = vector.Y = vector.Z = 0f;
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
		public static FVector3 Clone(FVector3 vector)
		{
			FVector3 result = new FVector3();

			if(vector != null)
			{
				result.mReadOnly = vector.mReadOnly;
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
		public static FVector3 CrossProduct(FVector3 vector1, FVector3 vector2)
		{
			FVector3 result = new FVector3();

			if(vector1 != null && vector2 != null)
			{
				Assign(result,
					(vector1.mY * vector2.mZ) -
					(vector2.mY * vector1.mZ),
					((vector1.mX * vector2.mZ) -
					(vector2.mX * vector1.mZ) * -1f),
					(vector1.mX * vector2.mY) -
					(vector2.mX * vector1.mY));
			}
			return result;
		}
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
		public static FVector3 Delta(FVector3 vectorA, FVector3 vectorB)
		{
			FVector3 result = new FVector3();

			if(vectorA != null && vectorB != null)
			{
				result.mX = vectorB.mX - vectorA.mX;
				result.mY = vectorB.mY - vectorA.mY;
				result.mZ = vectorB.mZ - vectorA.mZ;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Dot																																		*
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
		public static float Dot(FVector3 vectorA, FVector3 vectorB)
		{
			float result = 0f;

			if(vectorA != null && vectorB != null)
			{
				result =
					(vectorA.mX * vectorB.mX) +
					(vectorA.mY * vectorB.mY) +
					(vectorA.mZ * vectorB.mZ);
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

			if(obj is FVector3 @vector)
			{
				if(vector.mX == mX && vector.mY == mY && vector.mZ == mZ)
				{
					result = true;
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a value indicating whether the values in the supplied vector
		/// are equal to the specified elemental associations.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be compared.
		/// </param>
		/// <param name="x">
		/// The X value to compare.
		/// </param>
		/// <param name="y">
		/// The Y value to compare.
		/// </param>
		/// <param name="z">
		/// The Z value to compare.
		/// </param>
		/// <returns>
		/// Value indicating whether the axis values are equal to those found in
		/// the caller's supplied vector.
		/// </returns>
		public static bool Equals(FVector3 vector, float x, float y, float z)
		{
			bool result = false;

			if(vector != null)
			{
				result = vector.mX == x &&
					vector.mY == y &&
					vector.mZ == z;
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
		public static float[] GetArray(FVector3 vector)
		{
			float[] result = null;

			if(vector != null)
			{
				result = new float[3];
				result[0] = vector.mX;
				result[1] = vector.mY;
				result[2] = vector.mZ;
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
		/// <param name="theta">
		/// Angles to apply, in radians. Notice that in this version, the Z
		/// rotation (roll) is ignored. X = pitch and Y = yaw.
		/// </param>
		/// <param name="length">
		/// Length of the line.
		/// </param>
		/// <returns>
		/// Destination point of the line.
		/// </returns>
		public static FVector3 GetDestPoint(FVector3 point, FVector3 theta,
			float length)
		{
			double cosPhi = 0d;
			FVector3 direction = null;
			double phiPitch = 0d;
			FVector3 result = new FVector3();
			double thetaYaw = 0d;

			if(point != null && theta != null && length != 0.0)
			{
				phiPitch = (double)theta.X;
				thetaYaw = (double)theta.Y;
				cosPhi = Math.Cos(phiPitch);
				direction = new FVector3(
					(float)(cosPhi * Math.Cos(thetaYaw)),
					(float)Math.Sin(phiPitch),
					(float)(cosPhi * Math.Sin(thetaYaw)));
				result = point + (length * direction);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetEulerAngle																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the 3D Euler angle of the line between point 
		/// </summary>
		/// <param name="vectorA">
		/// Reference to the first vector on the line, or starting point.
		/// </param>
		/// <param name="vectorB">
		/// Reference to the second vector on the line, or ending point.
		/// </param>
		/// <returns>
		/// The pitch (X) and yaw (Y) of the angle between the two specified
		/// vectors, where vectorA is considered to be the base and vectorB
		/// is considered to be the target. Roll (Z) is ignored in this version
		/// because there isn't enough context to create it.
		/// </returns>
		public static FVector3 GetEulerAngle(FVector3 vectorA, FVector3 vectorB)
		{
			double deltaX = 0d;
			double deltaY = 0d;
			double deltaZ = 0d;
			double pitch = 0d;
			FVector3 result = null;
			double yaw = 0d;

			if(vectorA != null && vectorB != null)
			{
				deltaX = (double)vectorB.X - (double)vectorA.X;
				deltaY = (double)vectorB.Y - (double)vectorA.Y;
				deltaZ = (double)vectorB.Z - (double)vectorA.Z;
				pitch = Math.Atan2(deltaY,
					Math.Sqrt(deltaX * deltaX + deltaZ * deltaZ));
				yaw = Math.Atan2(deltaY, deltaX);
				result = new FVector3((float)pitch, (float)yaw, 0f);
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
			result *= (factor + mZ.GetHashCode());
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
		public static FVector3 GetLineAngle(FVector3 pointA, FVector3 pointB)
		{
			float length = 0f;
			FVector2 point = null;
			FVector3 result = new FVector3();
			FVector3 vecA = null;
			FVector3 vecB = null;

			if(pointA != null & pointB != null)
			{
				vecA = new FVector3(pointA);
				vecB = new FVector3(pointB);
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
		public static float GetLineDistance(FVector3 vectorA, FVector3 vectorB)
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
		/// <returns>
		/// Ray of length values per axis.
		/// </returns>
		public static FVector3 GetMagnitude(FVector3 point,
			float destinationX, float destinationY, float destinationZ)
		{
			FVector3 result = new FVector3();

			if(point != null)
			{
				Assign(result,
					destinationX - point.mX,
					destinationY - point.mY,
					destinationZ - point.mZ);
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
		public static FVector3 GetMagnitude(FVector3 pointA, FVector3 pointB)
		{
			FVector3 result = new FVector3();

			if(pointA != null && pointB != null)
			{
				result = pointB - pointA;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	IntersectRay																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the intersection of the caller's ray and face.
		/// </summary>
		/// <param name="rayBase">
		/// Starting coordinate of the ray.
		/// </param>
		/// <param name="rayLengths">
		/// Lengths in each direction from point A to point B.
		/// </param>
		/// <param name="faceCoordinates">
		/// Coordinates of the face to test for.
		/// </param>
		/// <param name="result">
		/// An output parameter containing the coordinate of the face where
		/// intersected by the ray, if found. Otherwise, null.
		/// </param>
		/// <returns>
		/// The status of the intersection operation.
		/// </returns>
		/// <remarks>
		/// In this version, the face coordinates can contain a single triangle
		/// or a single quad. In the case of a quad, the shape is triangulated and
		/// both triangles are tested.
		/// </remarks>
		public static IntersectionStatusEnum IntersectRay(FVector3 rayBase,
			FVector3 rayLengths, List<FVector3> faceCoordinates, out FVector3 result)
		{
			//	This method is derived from:
			//	http://geomalgorithms.com/a06-_intersect-2.html
			//	By default, it works on a single triangle.
			float a = 0f;
			float b = 0f;
			float D = 0f;
			FVector3 dir = null;   //	Direction.
			FVector3 n = null;     //	Normal.
			float r = 0f;
			float s = 0f;
			IntersectionStatusEnum status = IntersectionStatusEnum.None;
			float t = 0f;
			int triCount = 0;
			int triIndex = 0;
			int triP0 = 0;
			int triP1 = 0;
			int triP2 = 0;
			FVector3 u = null;
			float uu = 0f;
			float uv = 0f;
			FVector3 v = null;
			float vv = 0f;
			FVector3 w = null;
			FVector3 w0 = null;
			float wu = 0f;
			float wv = 0f;

			result = null;
			if(rayBase != null && rayLengths != null && !IsZero(rayLengths) &&
				faceCoordinates?.Count > 2)
			{
				triCount = faceCoordinates.Count - 2;
				if(triCount > 0 && triCount < 3)
				{
					for(triIndex = 0; triIndex < triCount; triIndex++)
					{
						status = IntersectionStatusEnum.None;
						triP0 = 0;
						triP1 = 1 + triIndex;
						triP2 = 2 + triIndex;
						u = faceCoordinates[triP1] - faceCoordinates[triP0];
						v = faceCoordinates[triP2] - faceCoordinates[triP1];
						n = Normalize(CrossProduct(u, v));
						if(IsZero(n))
						{
							//	Triangle is degenerate.
							status = IntersectionStatusEnum.ShapeInvalid;
							continue;
						}
						if(status == IntersectionStatusEnum.None)
						{
							dir = Normalize(rayLengths);
							w0 = rayBase - faceCoordinates[triP0];
							a = 0f - Dot(n, w0);
							b = Dot(n, dir);
							if(Math.Abs(b) < double.Epsilon)
							{
								//	Ray is parallel to the plane.
								if(a == 0f)
								{
									//	Ray is in the plane.
									status = IntersectionStatusEnum.RayParallelInsidePlane;
									continue;
								}
								else
								{
									//	Ray is disjoint from plane.
									status = IntersectionStatusEnum.RayParallelDisconnected;
									continue;
								}
							}
						}
						if(status == IntersectionStatusEnum.None)
						{
							//	Get intersect point of ray with triangle plane.
							r = a / b;
							if(r < 0f)
							{
								//	Ray is pointing away from plane.
								status = IntersectionStatusEnum.RayPointsAway;
								continue;
							}
						}
						if(status == IntersectionStatusEnum.None)
						{
							//	Intersect ray point with plane.
							result = rayBase + (dir * r);
							//	Check to see if result is inside face.
							uu = Dot(u, u);
							uv = Dot(u, v);
							vv = Dot(v, v);
							w = result - faceCoordinates[triP0];
							wu = Dot(w, u);
							wv = Dot(w, v);
							D = (uv * uv) - (uu * vv);
							//	Get and test parametric coordinates.
							s = ((uv * wv) - (vv * wu)) / D;
							if(s < 0f || s > 1f)
							{
								//	result is outside of face.
								status = IntersectionStatusEnum.RayOutsideFaceOnS;
								continue;
							}
							else
							{
								t = ((uv * wu) - (uu * wv)) / D;
								if(t < 0f || (s + t) > 1f)
								{
									status = IntersectionStatusEnum.RayOutsideFaceOnT;
									continue;
								}
								else
								{
									status = IntersectionStatusEnum.OK;
									break;
								}
							}
						}
					}
				}
			}
			return status;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Invert																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a copy of the caller's vector where the values have been
		/// inverted.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to invert.
		/// </param>
		/// <returns>
		/// A copy of the caller's vector where the values have been inverted.
		/// </returns>
		public static FVector3 Invert(FVector3 vector)
		{
			FVector3 result = new FVector3();

			if(vector != null)
			{
				if(vector.mX != 0f)
				{
					result.mX = 0f - vector.mX;
				}
				if(vector.mY != 0f)
				{
					result.mY = 0f - vector.mY;
				}
				if(vector.mZ != 0f)
				{
					result.mZ = 0f - vector.mZ;
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
		public static bool IsDifferent(FVector3 vectorA, FVector3 vectorB)
		{
			bool result = false;

			if(vectorA != null && vectorB != null)
			{
				result = vectorA.mX != vectorB.mX ||
					vectorA.mY != vectorB.mY ||
					vectorA.mZ != vectorB.mZ;
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
		public static bool IsEmpty(FVector3 vector)
		{
			bool result = true;

			if(vector != null &&
				(vector.mX != 0f || vector.mY != 0f || vector.mZ != 0f))
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
		public static bool IsZero(FVector3 vector)
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
		public static float Length(FVector3 vector)
		{
			//float result = 0f;

			//if(vector != null)
			//{
			//	result = (float)Math.Sqrt(
			//		Math.Pow((double)vector.mX, 2d) +
			//		Math.Pow((double)vector.mY, 2d) +
			//		Math.Pow((double)vector.mZ, 2d));
			//}
			//return result;
			return Magnitude(vector);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Magnitude																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the absolute magnitude of the provided point.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector for which the magnitude will be found.
		/// </param>
		/// <returns>
		/// The absolute magnitude of the caller's point.
		/// </returns>
		public static float Magnitude(FVector3 vector)
		{
			float result = 0f;

			if(vector != null)
			{
				result = (float)Math.Sqrt(
					(double)(
					vector.mX * vector.mX +
					vector.mY * vector.mY +
					vector.mZ * vector.mZ));
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
		public static float MagnitudeSquared(FVector3 vector)
		{
			float result = 0f;
			double x = 0d;
			double y = 0d;
			double z = 0d;

			if(vector != null)
			{
				x = (double)vector.mX;
				y = (double)vector.mY;
				z = (double)vector.mZ;
				result = (float)((x * x) + (y * y) + (z * z));
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
		public static FVector3 Mask(FVector3 vector, string mask)
		{
			string m = "";
			FVector3 result = new FVector3();

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
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MiddlePoint																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the middle coordinate between two points.
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
		public static FVector3 MiddlePoint(FVector3 vectorA, FVector3 vectorB)
		{
			FVector3 result = new FVector3();

			if(vectorA != null && vectorB != null)
			{
				result.X = (vectorA.X + vectorB.X) / 2f;
				result.Y = (vectorA.Y + vectorB.Y) / 2f;
				result.Z = (vectorA.Z + vectorB.Z) / 2f;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Negate																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a copy of the caller's vector where the values have been
		/// negated.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to negate.
		/// </param>
		/// <returns>
		/// A copy of the caller's vector where the values have been negated.
		/// </returns>
		public static FVector3 Negate(FVector3 vector)
		{
			FVector3 result = new FVector3();

			if (vector != null)
			{
				result.mX = 0f - vector.mX;
				result.mY = 0f - vector.mY;
				result.mZ = 0f - vector.mZ;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Normal																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the normal of the caller's polygon.
		/// </summary>
		/// <param name="points">
		/// Polygon for which to calculate the normal.
		/// </param>
		/// <returns>
		/// The normal of the provided polygon.
		/// </returns>
		public static FVector3 Normal(List<FVector3> points)
		{
			int count = 0;
			FVector3 current = null;
			int index = 0;
			FVector3 next = null;
			FVector3 result = new FVector3();

			if(points?.Count > 2)
			{
				count = points.Count;
				for(index = 0; index < count; index++)
				{
					current = points[index];
					if(index == count - 1)
					{
						next = points[0];
					}
					else
					{
						next = points[index + 1];
					}
					result.mX +=
						(current.mY - next.mY) *
						(current.mZ + next.mZ);
					result.mY +=
						(current.mZ - next.mZ) *
						(current.mX + next.mX);
					result.mZ +=
						(current.mX - next.mX) *
						(current.mY + next.mY);
				}
			}
			return Normalize(result);
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
		public static FVector3 Normalize(FVector3 vector)
		{
			float length = 0f;
			FVector3 result = new FVector3();

			if(vector != null)
			{
				length = Length(vector);
				if(length != 0.0f)
				{
					result.mX = vector.mX / length;
					result.mY = vector.mY / length;
					result.mZ = vector.mZ / length;
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
		/// <param name="dz">
		/// Z distance from the original vector.
		/// </param>
		/// <returns>
		/// Reference to a new vector at the specified offset from the original.
		/// </returns>
		public static FVector3 Offset(FVector3 vector,
			float dx, float dy, float dz)
		{
			FVector3 result = null;

			if(vector != null)
			{
				result = new FVector3(vector.mX + dx, vector.mY + dy, vector.mZ + dz);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Parse																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Parse a coordinate string and return its FVector3 representation.
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
		public static FVector3 Parse(string coordinate, bool allowNull = false)
		{
			bool bX = false;
			bool bY = false;
			bool bZ = false;
			int index = 0;
			MatchCollection matches = null;
			FPoint3 result = null;
			string text = "";

			if(coordinate?.Length > 0)
			{
				text = coordinate.Trim();
				if(text.StartsWith("{") && text.EndsWith("}"))
				{
					//	JSON object.
					try
					{
						result = JsonConvert.DeserializeObject<FPoint3>(coordinate);
					}
					catch { }
				}
				else
				{
					//	Freehand.
					matches = Regex.Matches(coordinate, ResourceMain.rxCoordinate);
					if(matches.Count > 0)
					{
						result = new FPoint3();
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
								case "z":
									result.mZ = ToFloat(GetValue(matchItem, "number"));
									bZ = true;
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
										case 2:
											if(!bZ)
											{
												result.mZ = ToFloat(GetValue(matchItem, "number"));
												bZ = true;
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
				result = new FPoint3();
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
		public static FVector3 ReverseDirection(FVector3 vector)
		{
			FVector3 result = new FVector3();

			if(vector != null)
			{
				result.mX = vector.mX * -1f;
				result.mY = vector.mY * -1f;
				result.mZ = vector.mZ * -1f;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Rotate																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a vector representing the source value rotated around the
		/// origin.
		/// </summary>
		/// <param name="vector">
		/// Value to rotate.
		/// </param>
		/// <param name="thetaX">
		/// Angle to rotate on the X-axis, in radians.
		/// </param>
		/// <param name="thetaY">
		/// Angle to rotate on the Y-axis, in radians.
		/// </param>
		/// <param name="thetaZ">
		/// Angle to rotate on the Z-axis, in radians.
		/// </param>
		/// <returns>
		/// Result of rotation of the vector around the universal center (0, 0).
		/// </returns>
		public static FVector3 Rotate(FVector3 vector,
			float thetaX, float thetaY, float thetaZ)
		{
			FVector3 result = null;

			if(vector != null)
			{
				result = new FVector3(vector);
				if(thetaX != 0.0f)
				{
					result = FMatrix3.RotateX(result, thetaX);
				}
				if(thetaY != 0.0f)
				{
					result = FMatrix3.RotateY(result, thetaY);
				}
				if(thetaZ != 0.0f)
				{
					result = FMatrix3.RotateZ(result, thetaZ);
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a vector representing the source value rotated around the
		/// origin.
		/// </summary>
		/// <param name="vector">
		/// Value to rotate.
		/// </param>
		/// <param name="theta">
		/// Reference to the X, Y, and Z rotation values to apply.
		/// </param>
		/// <returns>
		/// Result of rotation of the vector around the universal center (0, 0).
		/// </returns>
		public static FVector3 Rotate(FVector3 vector, FVector3 theta)
		{
			FVector3 result = null;

			if(vector != null && theta != null)
			{
				result = new FVector3(vector);
				if(theta.X != 0.0f)
				{
					result = FMatrix3.RotateX(result, theta.X);
				}
				if(theta.Y != 0.0f)
				{
					result = FMatrix3.RotateY(result, theta.Y);
				}
				if(theta.Z != 0.0f)
				{
					result = FMatrix3.RotateZ(result, theta.Z);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RotateX																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a copy of the caller's vector, rotated on the X-axis.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be rotated.
		/// </param>
		/// <param name="theta">
		/// Angle to rotate, in radians.
		/// </param>
		/// <returns>
		/// Reference to a new vector representing the caller's value, rotated
		/// by the specified angle, around the universal origin (0, 0).
		/// </returns>
		public static FVector3 RotateX(FVector3 vector, float theta)
		{
			return FMatrix3.RotateX(vector, theta);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RotateY																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a copy of the caller's vector, rotated on the Y-axis.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be rotated.
		/// </param>
		/// <param name="theta">
		/// Angle to rotate, in radians.
		/// </param>
		/// <returns>
		/// Reference to a new vector representing the caller's value, rotated
		/// by the specified angle, around the universal origin (0, 0).
		/// </returns>
		public static FVector3 RotateY(FVector3 vector, float theta)
		{
			return FMatrix3.RotateY(vector, theta);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RotateZ																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a copy of the caller's vector, rotated on the Z-axis.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be rotated.
		/// </param>
		/// <param name="theta">
		/// Angle to rotate, in radians.
		/// </param>
		/// <returns>
		/// Reference to a new vector representing the caller's value, rotated
		/// by the specified angle, around the universal origin (0, 0).
		/// </returns>
		public static FVector3 RotateZ(FVector3 vector, float theta)
		{
			return FMatrix3.RotateZ(vector, theta);
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
		public static FVector3 Scale(FVector3 vector, float scale)
		{
			FVector3 result = new FVector3();

			if(vector != null)
			{
				result.mX = vector.mX * scale;
				result.mY = vector.mY * scale;
				result.mZ = vector.mZ * scale;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
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
		public static FVector3 Scale(FVector3 vector, FVector3 scale)
		{
			FVector3 result = new FVector3();

			if(vector != null)
			{
				result.mX = vector.mX * scale.mX;
				result.mY = vector.mY * scale.mY;
				result.mZ = vector.mZ * scale.mZ;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Set																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the values of the specified vector using an abbreviated method.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to set.
		/// </param>
		/// <param name="x">
		/// X value.
		/// </param>
		/// <param name="y">
		/// Y value.
		/// </param>
		/// <param name="z">
		/// Z value.
		/// </param>
		public static void Set(FVector3 vector, float x, float y, float z)
		{
			if(vector != null)
			{
				vector.mX = x;
				vector.mY = y;
				vector.mZ = z;
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
		public static void SetArray(FVector3 vector, float[] values)
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
		public static FVector3 SetVectorLength(FVector3 vector, float size)
		{
			//	Normalize the vector.
			FVector3 vectorNormalized = FVector3.Normalize(vector);

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
		public static float Sum(FVector3 vector)
		{
			float result = 0f;

			if(vector != null)
			{
				result = vector.mX + vector.mY + vector.mZ;
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
		public static FVector3 SwapYZ(FVector3 vector)
		{
			FVector3 result = new FVector3();

			if(vector != null)
			{
				Assign(result,
					vector.mX, vector.mZ, vector.mY);
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
		public static FVector3 ToDeg(FVector3 rayAngle)
		{
			FVector3 result = new FVector3();

			if(rayAngle != null)
			{
				FVector3.Assign(result,
					Trig.RadToDeg(rayAngle.mX),
					Trig.RadToDeg(rayAngle.mY),
					Trig.RadToDeg(rayAngle.mZ));
			}
			return result;
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
			result.Append(',');
			result.Append($"{mZ:0.000}");
			return result.ToString();
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
		public static void TransferValues(FVector3 source, FVector3 target)
		{
			if(source != null && target != null)
			{
				target.mReadOnly = source.mReadOnly;
				target.mX = source.mX;
				target.mY = source.mY;
				target.mZ = source.mZ;
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
		/// Reference to the offset to apply to the point.
		/// </param>
		public static void Translate(FVector3 vector, FVector3 offset)
		{
			if(vector != null && offset != null)
			{
				vector.mX += offset.mX;
				vector.mY += offset.mY;
				vector.mZ += offset.mZ;
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
