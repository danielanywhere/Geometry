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
using static Geometry.GeometryUtil;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FQuaternion																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Single precision floating-point Quaternion.
	/// </summary>
	public class FQuaternion
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
		/// Create a new Instance of the FQuaternion object.
		/// </summary>
		public FQuaternion()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the FQuaternion object.
		/// </summary>
		/// <param name="x">
		/// X rotational value.
		/// </param>
		/// <param name="y">
		/// Y rotational value.
		/// </param>
		/// <param name="z">
		/// Z rotational value.
		/// </param>
		/// <param name="w">
		/// W scalar value.
		/// </param>
		public FQuaternion(float x, float y, float z, float w)
		{
			mX = x;
			mY = y;
			mZ = z;
			mW = w;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the FQuaternion object.
		/// </summary>
		/// <param name="vector">
		/// The vector rotation part of the quaternion.
		/// </param>
		/// <param name="scalar">
		/// The scalar part of the quaternion.
		/// </param>
		public FQuaternion(FVector3 vector, float scalar)
		{
			if(vector != null)
			{
				mX = vector.X;
				mY = vector.Y;
				mZ = vector.Z;
			}
			mW = scalar;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* _Operator -																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of the caller's quaternion values negated.
		/// </summary>
		/// <param name="quaternion">
		/// Reference to a quaternion to negate.
		/// </param>
		/// <returns>
		/// Reference to a newly created quaternion where the values from the
		/// original object have been negated.
		/// </returns>
		public static FQuaternion operator -(FQuaternion quaternion)
		{
			return Negate(quaternion);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the result of one quaternion subtracted from another.
		/// </summary>
		/// <param name="minuend">
		/// Reference to the minuend quaternion.
		/// </param>
		/// <param name="subtrahend">
		/// Reference to the subtrahend quaternion.
		/// </param>
		/// <returns>
		/// Result of the subtraction.
		/// </returns>
		public static FQuaternion operator -(FQuaternion minuend,
			FQuaternion subtrahend)
		{
			return Subtract(minuend, subtrahend);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Operator *																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of a quaternion multiplied by a scalar.
		/// </summary>
		/// <param name="multiplicand">
		/// The scalar value to multiply.
		/// </param>
		/// <param name="multiplier">
		/// The quaternion to multiply.
		/// </param>
		/// <returns>
		/// Result of the multiplication.
		/// </returns>
		public static FQuaternion operator *(float multiplicand,
			FQuaternion multiplier)
		{
			return Multiply(multiplier, multiplicand);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the result of a quaternion multiplied by a scalar value.
		/// </summary>
		/// <param name="multiplicand">
		/// Reference to the multiplicand.
		/// </param>
		/// <param name="multiplier">
		/// The multiplier.
		/// </param>
		/// <returns>
		/// Reference to a new quaternion representing the result of the
		/// multiplication.
		/// </returns>
		public static FQuaternion operator *(FQuaternion multiplicand,
			float multiplier)
		{
			return Multiply(multiplicand, multiplier);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the result of two quaternions multiplied by one another.
		/// </summary>
		/// <param name="multiplicand">
		/// Reference to the multiplicand.
		/// </param>
		/// <param name="multiplier">
		/// Reference to the multiplier.
		/// </param>
		/// <returns>
		/// Reference to a new quaternion representing the result of the
		/// multiplication.
		/// </returns>
		public static FQuaternion operator *(FQuaternion multiplicand,
			FQuaternion multiplier)
		{
			return Multiply(multiplicand, multiplier);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the result of a quaternion multiplied by a vector.
		/// </summary>
		/// <param name="muliplicand">
		/// Reference to the multiplicand.
		/// </param>
		/// <param name="multiplier">
		/// Reference to the multiplier.
		/// </param>
		/// <returns>
		/// Reference to a new vector representing the result of the operation,
		/// if legitimate.
		/// </returns>
		public static FVector3 operator *(FQuaternion muliplicand,
			FVector3 multiplier)
		{
			FQuaternion combined = null;
			FQuaternion point = null;
			FVector3 result = new FVector3();

			if(muliplicand != null && multiplier != null)
			{
				point = new FQuaternion(multiplier.X, multiplier.Y, multiplier.Z, 0f);
				combined = muliplicand * point * FQuaternion.Conjugate(muliplicand);
				FVector3.Set(result, combined.X, combined.Y, combined.Z);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Operator /																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of one quaternion divided by the other.
		/// </summary>
		/// <param name="divisor">
		/// A reference to the divisor.
		/// </param>
		/// <param name="dividend">
		/// A reference to the dividend.
		/// </param>
		/// <returns>
		/// Reference to the quaternion division result.
		/// </returns>
		public static FQuaternion operator /(FQuaternion divisor,
			FQuaternion dividend)
		{
			return Divide(divisor, dividend);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* _Operator +																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of the addition of two quaternions.
		/// </summary>
		/// <param name="addend1">
		/// Reference to the first addend.
		/// </param>
		/// <param name="addend2">
		/// Reference to the second addend.
		/// </param>
		/// <returns>
		/// Reference to the quaternion addition result.
		/// </returns>
		public static FQuaternion operator +(FQuaternion addend1,
			FQuaternion addend2)
		{
			return Add(addend1, addend2);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Operator !=																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether values of two quaternions are not
		/// equal.
		/// </summary>
		/// <param name="itemA">
		/// Reference to the first quaternion to compare.
		/// </param>
		/// <param name="itemB">
		/// Reference to the second quaternion to compare.
		/// </param>
		/// <returns>
		/// True if the two objects are substantially not equal in value.
		/// Otherwise, false.
		/// </returns>
		[DebuggerStepThrough]
		public static bool operator !=(FQuaternion itemA, FQuaternion itemB)
		{
			bool result = true;

			if((object)itemA != null && (object)itemB != null)
			{
				result = !(itemA == itemB);
			}
			else if((object)itemA == null && (object)itemB == null)
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
		/// Return a value indicating whether values of two quaternions are equal.
		/// </summary>
		/// <param name="itemA">
		/// Reference to the first quaternion to compare.
		/// </param>
		/// <param name="itemB">
		/// Reference to the second quaternion to compare.
		/// </param>
		/// <returns>
		/// True if the two objects are substantially equal in value. Otherwise,
		/// false.
		/// </returns>
		[DebuggerStepThrough]
		public static bool operator ==(FQuaternion itemA, FQuaternion itemB)
		{
			bool result = true;

			if((object)itemA != null && (object)itemB != null)
			{
				result = (
					(itemA.mW == itemB.mW) &&
					(itemA.mX == itemB.mX) &&
					(itemA.mY == itemB.mY) &&
					(itemA.mZ == itemB.mZ));
			}
			else if((object)itemA != null || (object)itemB != null)
			{
				result = false;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Add																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add two quaternions and return the result.
		/// </summary>
		/// <param name="addend1">
		/// Reference to the first quaternion to add.
		/// </param>
		/// <param name="addend2">
		/// Reference to the second quaternion to add.
		/// </param>
		/// <returns>
		/// Reference to a new quaternion representing the result of the addition,
		/// if legitimate.
		/// </returns>
		public static FQuaternion Add(FQuaternion addend1, FQuaternion addend2)
		{
			FQuaternion result = new FQuaternion();

			if(addend1 != null && addend2 != null)
			{
				Set(result,
					addend1.mX + addend2.mX,
					addend1.mY + addend2.mY,
					addend1.mZ + addend2.mZ,
					addend1.mW + addend2.mW);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create and return a deep copy of the specified quaternion.
		/// </summary>
		/// <param name="quaternion">
		/// Reference to the quaternion to clone.
		/// </param>
		/// <returns>
		/// Reference to a newly created quaternion, if the supplied value was
		/// legitimate.
		/// </returns>
		public static FQuaternion Clone(FQuaternion quaternion)
		{
			FQuaternion result = new FQuaternion();

			if(quaternion != null)
			{
				result.mX = quaternion.mX;
				result.mY = quaternion.mY;
				result.mZ = quaternion.mZ;
				result.mW = quaternion.mW;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Concatenate																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Concatenate the first quaternion rotation onto the second one.
		/// </summary>
		/// <param name="quaternion1">
		/// Reference to the first quaternion representing the base upon which
		/// the other is to be added.
		/// </param>
		/// <param name="quaternion2">
		/// Reference to the second quaternion representing the extension to
		/// add to the base.
		/// </param>
		/// <returns>
		/// Reference to a newly created quaternion where the second item has
		/// been concatenated onto the first, if legitimate.
		/// </returns>
		public static FQuaternion Concatenate(FQuaternion quaternion1,
			FQuaternion quaternion2)
		{
			double crossX = 0d;
			double crossY = 0d;
			double crossZ = 0d;
			double mag = 0d;
			double q1w = 0d;
			double q1x = 0d;
			double q1y = 0d;
			double q1z = 0d;
			double q2w = 0d;
			double q2x = 0d;
			double q2y = 0d;
			double q2z = 0d;
			FQuaternion result = new FQuaternion();

			if(quaternion1 != null && quaternion2 != null)
			{
				q1w = (double)quaternion2.mW;
				q1x = (double)quaternion2.mX;
				q1y = (double)quaternion2.mY;
				q1z = (double)quaternion2.mZ;
				q2w = (double)quaternion1.mW;
				q2x = (double)quaternion1.mX;
				q2y = (double)quaternion1.mY;
				q2z = (double)quaternion1.mZ;

				crossX = (q1y * q2z) - (q1z * q2y);
				crossY = (q1z * q2x) - (q1x * q2z);
				crossZ = (q1x * q2y) - (q1y * q2x);

				mag = (q1x * q2x) + (q1y * q2y) + (q1z * q2z);

				result.mX = (float)((q1x * q2w) + (q2x * q1w) + crossX);
				result.mY = (float)((q1y * q2w) + (q2y * q1w) + crossY);
				result.mZ = (float)((q1z * q2w) + (q2z * q1w) + crossZ);
				result.mW = (float)((q1w * q2w) - mag);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Conjugate																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the conjugate of the quaternion, which is to say the negation
		/// of the imaginary parts only.
		/// </summary>
		/// <param name="quaternion">
		/// Reference to the quaternion to conjugate.
		/// </param>
		/// <returns>
		/// Reference to the conjugated form of the caller's quaternion, where
		/// only the imaginary parts have been negated, if valid.
		/// </returns>
		public static FQuaternion Conjugate(FQuaternion quaternion)
		{
			FQuaternion result = new FQuaternion();

			if(quaternion != null)
			{
				Set(result,
					0f - quaternion.mX,
					0f - quaternion.mY,
					0f - quaternion.mZ,
					quaternion.mW);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CoordinateChanged																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the value of a coordinate has changed.
		/// </summary>
		public event FloatPointEventHandler CoordinateChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Divide																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Divide the divisor by the supplied dividend and return the result in
		/// a new quaternion.
		/// </summary>
		/// <param name="divisor">
		/// Reference to the divisor to serve as the base.
		/// </param>
		/// <param name="dividend">
		/// REference to the dividend into which to separate the base.
		/// </param>
		/// <returns>
		/// Reference to the result of the division, if legitimate values were
		/// provided.
		/// </returns>
		public static FQuaternion Divide(FQuaternion divisor, FQuaternion dividend)
		{
			double crossX = 0d;
			double crossY = 0d;
			double crossZ = 0d;
			double inverse = 0d;
			double lengthSquared = 0d;
			double mag = 0d;
			double q1w = 0d;
			double q1x = 0d;
			double q1y = 0d;
			double q1z = 0d;
			double q2w = 0d;
			double q2x = 0d;
			double q2y = 0d;
			double q2z = 0d;
			FQuaternion result = new FQuaternion();

			if(divisor != null && dividend != null)
			{
				q1w = (double)divisor.W;
				q1x = (double)divisor.X;
				q1y = (double)divisor.Y;
				q1z = (double)divisor.Z;
				q2w = (double)dividend.W;
				q2x = (double)dividend.X;
				q2y = (double)dividend.Y;
				q2z = (double)dividend.Z;

				lengthSquared = (q2x * q2x) + (q2y * q2y) + (q2z * q2z) + (q2w * q2w);
				if(lengthSquared != 0f)
				{
					inverse = 1d / lengthSquared;
				}

				q2x = (0d - q2x) * inverse;
				q2y = (0d - q2y) * inverse;
				q2z = (0d - q2z) * inverse;
				q2w *= inverse;

				crossX = (q1y * q2z) - (q1z * q2y);
				crossY = (q1z * q2x) - (q1x * q2z);
				crossZ = (q1x * q2y) - (q1y * q2x);

				mag = (q1x * q2x) + (q1y * q2y) + (q1z * q2z);

				result.mX = (float)((q1x * q2w) + (q2x * q1w) + crossX);
				result.mY = (float)((q1y * q2w) + (q2y * q1w) + crossY);
				result.mZ = (float)((q1z * q2w) + (q2z * q1w) + crossZ);
				result.mW = (float)((q1w * q2w) - mag);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Dot																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the dot product of two quaternions.
		/// </summary>
		/// <param name="quaternion1">
		/// Reference to the first quaternion to compare.
		/// </param>
		/// <param name="quaternion2">
		/// Reference to the second quaternion to compare.
		/// </param>
		/// <returns>
		/// The dot product of the supplied quaternions.
		/// </returns>
		public static float Dot(FQuaternion quaternion1, FQuaternion quaternion2)
		{
			float result = 0f;

			if(quaternion1 != null && quaternion2 != null)
			{
				result = (quaternion1.mX * quaternion2.mX) +
					(quaternion1.mY * quaternion2.mY) +
					(quaternion1.mZ * quaternion2.mZ) +
					(quaternion1.mW * quaternion2.mW);
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

			if(obj is FQuaternion @quaternion)
			{
				if(quaternion.mX == mX && quaternion.mY == mY && quaternion.mZ == mZ &&
					quaternion.mW == mW)
				{
					result = true;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* FromAxisAngle																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a quaternion from an arbitrary axis and an angle.
		/// </summary>
		/// <param name="axis">
		/// Reference to the axis around which the rotation is guing to take place.
		/// </param>
		/// <param name="angle">
		/// The angle of rotation that is going to be applied around the axis,
		/// in radians.
		/// </param>
		/// <returns>
		/// Reference to a newly created quaternion where rotation around the
		/// caller's arbitrary axis has been applied, if legitimate.
		/// </returns>
		public static FQuaternion FromAxisAngle(FVector3 axis, float angle)
		{
			double half = (double)angle / 2d;
			FQuaternion result = new FQuaternion();
			double sine = 0d;

			if(axis != null)
			{
				sine = Math.Sin(half);

				result.mX = (float)((double)axis.X * sine);
				result.mY = (float)((double)axis.Y * sine);
				result.mZ = (float)((double)axis.Z * sine);
				result.mW = (float)Math.Cos(half);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* FromEuler																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the reference to a quaternion that has been created from
		/// a vector with Tait-Euler angles.
		/// </summary>
		/// <param name="euler">
		/// Reference to the vector containing the Euler angles.
		/// </param>
		/// <returns>
		/// Reference to a newly created quaternion representing the supplied
		/// X, Y, and Z rotations, in ZYX order.
		/// </returns>
		public static FQuaternion FromEuler(FVector3 euler)
		{
			double cosY = 0d;
			double cosX = 0d;
			double cosZ = 0d;
			double half = 0d;
			FQuaternion result = new FQuaternion();
			double sinY = 0d;
			double sinX = 0d;
			double sinZ = 0d;


			if(euler != null)
			{
				half = euler.X * 0.5d;
				sinX = Math.Sin(half);
				cosX = Math.Cos(half);

				half = euler.Y * 0.5d;
				sinY = Math.Sin(half);
				cosY = Math.Cos(half);

				half = euler.Z * 0.5d;
				sinZ = Math.Sin(half);
				cosZ = Math.Cos(half);

				result.mX = (float)((cosZ * cosY * sinX) - (sinZ * sinY * cosX));
				result.mY = (float)((cosZ * sinY * cosX) + (sinZ * cosY * sinX));
				result.mZ = (float)((sinZ * cosY * cosX) - (cosZ * sinY * sinX));
				result.mW = (float)((cosZ * cosY * cosX) + (sinZ * sinY * sinX));
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* FromMatrix																														*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return the reference to a quaternion that has been created from a
		///// 4x4 matrix.
		///// </summary>
		///// <param name="matrix">
		///// Reference to the matrix to represent.
		///// </param>
		///// <returns>
		///// Reference to a newly created quaternion that represents the caller's
		///// </returns>
		//public static FQuaternion FromMatrix(FMatrix4 matrix)
		//{
		//	double inverse = 0d;
		//	double m11 = 0d;
		//	double m12 = 0d;
		//	double m13 = 0d;
		//	double m21 = 0d;
		//	double m22 = 0d;
		//	double m23 = 0d;
		//	double m31 = 0d;
		//	double m32 = 0d;
		//	double m33 = 0d;
		//	FQuaternion result = new FQuaternion();
		//	double scale = 0d;

		//	if(matrix != null)
		//	{
		//		m11 = (double)matrix[1, 1];
		//		m22 = (double)matrix[2, 2];
		//		m33 = (double)matrix[3, 3];
		//		scale = m11 + m22 + m33;
		//		if(scale > 0d)
		//		{
		//			scale = Math.Sqrt(scale + 1d);
		//			result.mW = (float)(scale * 0.5d);
		//			scale = 0.5d / scale;
		//			result.mX =
		//				(float)(((double)matrix[2, 3] - (double)matrix[3, 2]) * scale);
		//			result.mY =
		//				(float)(((double)matrix[3, 1] - (double)matrix[1, 3]) * scale);
		//			result.mZ =
		//				(float)(((double)matrix[1, 2] - (double)matrix[2, 1]) * scale);
		//		}
		//		else
		//		{
		//			m12 = (double)matrix[1, 2];
		//			m13 = (double)matrix[1, 3];
		//			m21 = (double)matrix[2, 1];
		//			m23 = (double)matrix[2, 3];
		//			m31 = (double)matrix[3, 1];
		//			m32 = (double)matrix[3, 2];
		//			if(m11 >= m22 && m11 >= m33)
		//			{
		//				scale = Math.Sqrt(1d + m11 - m22 - m33);
		//				inverse = 0.5d / scale;
		//				result.mX = (float)(0.5d * scale);
		//				result.mY = (float)((m12 + m21) * inverse);
		//				result.mZ = (float)((m13 + m31) * inverse);
		//				result.mW = (float)((m23 - m32) * inverse);
		//			}
		//			else if(m22 > m33)
		//			{
		//				scale = Math.Sqrt(1d + m22 - m11 - m33);
		//				inverse = 0.5d / scale;
		//				result.mX = (float)((m21 + m22) * inverse);
		//				result.mY = (float)(0.5d * scale);
		//				result.mZ = (float)((m32 + m23) * inverse);
		//				result.mW = (float)((m31 - m13) * inverse);
		//			}
		//			else
		//			{
		//				scale = Math.Sqrt(1d + m33 - m11 - m22);
		//				inverse = 0.5d / scale;
		//				result.mX = (float)((m31 + m13) * inverse);
		//				result.mY = (float)((m32 + m23) * inverse);
		//				result.mZ = (float)(0.5d * scale);
		//				result.mW = (float)((m12 - m21) * inverse);
		//			}
		//		}
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* FromPitchRollYaw																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the reference to a quaternion that has been created from
		/// pitch, roll, and yaw navigational values.
		/// </summary>
		/// <param name="pitch">
		/// The angle of pitch, in radians.
		/// </param>
		/// <param name="roll">
		/// The angle of roll, in radians.
		/// </param>
		/// <param name="yaw">
		/// The angle of yaw, in radians.
		/// </param>
		/// <returns>
		/// Reference to a newly created quaternion representing the supplied
		/// pitch, roll, and yaw values (XYZ ordering).
		/// </returns>
		public static FQuaternion FromPitchRollYaw(float pitch, float roll,
			float yaw)
		{
			double cosX = 0d;
			double cosY = 0d;
			double cosZ = 0d;
			double half = 0d;
			FQuaternion result = new FQuaternion();
			double sinX = 0d;
			double sinY = 0d;
			double sinZ = 0d;

			//	X.
			half = pitch * 0.5d;
			sinX = Math.Sin(half);
			cosX = Math.Cos(half);

			//	Y.
			half = roll * 0.5d;
			sinY = Math.Sin(half);
			cosY = Math.Cos(half);

			//	Z.
			half = yaw * 0.5d;
			sinZ = Math.Sin(half);
			cosZ = Math.Cos(half);

			result.mX = (float)((cosX * cosY * sinZ) - (sinX * sinY * cosZ));
			result.mY = (float)((cosX * sinY * cosZ) + (sinX * cosY * sinZ));
			result.mZ = (float)((sinX * cosY * cosZ) - (cosX * sinY * sinZ));
			result.mW = (float)((cosX * cosY * cosZ) + (sinX * sinY * sinZ));
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* FromXRotation																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create and return a new quaternion from a single X rotation.
		/// </summary>
		/// <param name="angle">
		/// The angle, in radians, at which to rotate on the X axis.
		/// </param>
		/// <returns>
		/// Reference to a newly created quaternion where the X axis is rotated
		/// by the specified angle.
		/// </returns>
		public static FQuaternion FromXRotation(float angle)
		{
			return FromAxisAngle(new FVector3(1f, 0f, 0f), angle);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* FromYRotation																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create and return a new quaternion from a single Y rotation.
		/// </summary>
		/// <param name="angle">
		/// The angle, in radians, at which to rotate on the Y axis.
		/// </param>
		/// <returns>
		/// Reference to a newly created quaternion where the Y axis is rotated
		/// by the specified angle.
		/// </returns>
		public static FQuaternion FromYRotation(float angle)
		{
			return FromAxisAngle(new FVector3(0f, 1f, 0f), angle);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* FromZRotation																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create and return a new quaternion from a single Z rotation.
		/// </summary>
		/// <param name="angle">
		/// The angle, in radians, at which to rotate on the Z axis.
		/// </param>
		/// <returns>
		/// Reference to a newly created quaternion where the Z axis is rotated
		/// by the specified angle.
		/// </returns>
		public static FQuaternion FromZRotation(float angle)
		{
			return FromAxisAngle(new FVector3(0f, 0f, 1f), angle);
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
			int result = 2025061101;

			factor = 0 - (int)((double)result * 0.25);

			result *= (factor + mX.GetHashCode());
			result *= (factor + mY.GetHashCode());
			result *= (factor + mZ.GetHashCode());
			result *= (factor + mW.GetHashCode());
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Identity																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return an identity Quaterion value, which indicates zero rotation.
		/// </summary>
		/// <returns>
		/// Reference to a new Quaternion whose value has been set to identity,
		/// or no rotation.
		/// </returns>
		public static FQuaternion Identity()
		{
			return new FQuaternion();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Inverse																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the inverse of the caller's quaternion.
		/// </summary>
		/// <param name="quaternion">
		/// Reference to the quaternion to be inverted.
		/// </param>
		/// <returns>
		/// Reference to the inverted quaternion, if valid.
		/// </returns>
		public static FQuaternion Inverse(FQuaternion quaternion)
		{
			double inverseMagnitudeSquared = 0d;
			double magnitudeSquared = 0d;
			FQuaternion result = new FQuaternion();

			if(quaternion != null)
			{
				magnitudeSquared = (double)MagnitudeSquared(quaternion);
				if(magnitudeSquared != 0d)
				{
					inverseMagnitudeSquared = 1d / magnitudeSquared;
					result.mX =
						(float)((double)(0f - quaternion.mX) * inverseMagnitudeSquared);
					result.mY =
						(float)((double)(0f - quaternion.mY) * inverseMagnitudeSquared);
					result.mZ =
						(float)((double)(0f - quaternion.mZ) * inverseMagnitudeSquared);
					result.mW = (float)((double)quaternion.mW * inverseMagnitudeSquared);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsIdentity																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the supplied quaternion has an
		/// identity value.
		/// </summary>
		/// <param name="quaternion">
		/// Reference to the quaternion to inspect.
		/// </param>
		/// <returns>
		/// True if the quaternion has a zero rotation value. Otherwise, false.
		/// </returns>
		public static bool IsIdentity(FQuaternion quaternion)
		{
			bool result = false;

			if(quaternion != null &&
				quaternion.mX == 0f &&
				quaternion.mY == 0f &&
				quaternion.mZ == 0f &&
				quaternion.mW == 1f)
			{
				result = true;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Length																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the length of the supplied quaternion.
		/// </summary>
		/// <param name="quaternion">
		/// Reference to the quaternion whose length will be calculated.
		/// </param>
		/// <returns>
		/// The length of the quaternion.
		/// </returns>
		public static float Length(FQuaternion quaternion)
		{
			float result = 0f;
			double w = 0d;
			double x = 0d;
			double y = 0d;
			double z = 0d;

			if(quaternion != null)
			{
				w = (double)quaternion.mW;
				x = (double)quaternion.mX;
				y = (double)quaternion.mY;
				z = (double)quaternion.mZ;
				result = (float)Math.Sqrt((x * x) + (y * y) + (z * z) + (w * w));
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Lerp																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a quaternion representing the linear interpolation between
		/// the two supplied quaternions.
		/// </summary>
		/// <param name="quaternion1">
		/// Reference to the starting quaternion to compare.
		/// </param>
		/// <param name="quaternion2">
		/// Reference to the ending quaternion to compare.
		/// </param>
		/// <param name="portion">
		/// The portion between the first and second quaternions to capture, as
		/// a decimal value between 0 and 1.
		/// </param>
		/// <returns>
		/// Reference to a new quaternion representing the specified portion
		/// between the first and second supplied quaternions, if legitimate
		/// values were supplied.
		/// </returns>
		public static FQuaternion Lerp(FQuaternion quaternion1,
			FQuaternion quaternion2, float portion)
		{
			double dPortion = (double)portion;
			double inverse = 0d;
			double magSquare = 0d;
			FQuaternion result = new FQuaternion();
			double w = 0d;
			double x = 0d;
			double y = 0d;
			double z = 0d;

			if(quaternion1 != null && quaternion2 != null)
			{
				x = Linear.Lerp((double)quaternion1.mX, (double)quaternion2.mX,
					dPortion);
				y = Linear.Lerp((double)quaternion1.mY, (double)quaternion2.mY,
					dPortion);
				z = Linear.Lerp((double)quaternion1.mZ, (double)quaternion2.mZ,
					dPortion);
				w = Linear.Lerp((double)quaternion1.mW, (double)quaternion2.mW,
					dPortion);

				//	Normalize the result.
				magSquare = (x * x) + (y * y) + (z * z) + (w * w);
				if(magSquare != 0d)
				{
					inverse = 1d / Math.Sqrt(magSquare);
				}
				result.mX = (float)(x * inverse);
				result.mY = (float)(y * inverse);
				result.mZ = (float)(z * inverse);
				result.mW = (float)(w * inverse);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* MagnitudeSquared																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the magnitude of the quaternion squared.
		/// </summary>
		/// <param name="quaternion">
		/// Reference to the quaternion to inspect.
		/// </param>
		/// <returns>
		/// The magnitude of the supplied quaternion squared.
		/// </returns>
		public static float MagnitudeSquared(FQuaternion quaternion)
		{
			float result = 0f;
			double w = 0d;
			double x = 0d;
			double y = 0d;
			double z = 0d;

			if(quaternion != null)
			{
				x = (double)quaternion.mX;
				y = (double)quaternion.mY;
				z = (double)quaternion.mZ;
				w = (double)quaternion.mW;
				result = (float)((x * x) + (y * y) + (z * z) + (w * w));
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Multiply																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Multiply a quaternion by a scalar value and return the result in a
		/// new quaternion.
		/// </summary>
		/// <param name="multiplicand">
		/// Reference to the quaternion to be multiplied.
		/// </param>
		/// <param name="multiplier">
		/// The value by which to multiply the quaterion's members.
		/// </param>
		/// <returns>
		/// Reference to a newly created quaternion containing the result of the
		/// multiplication, if legitimate.
		/// </returns>
		public static FQuaternion Multiply(FQuaternion multiplicand,
			float multiplier)
		{
			FQuaternion result = new FQuaternion();

			if(multiplicand != null)
			{
				result.mX = multiplicand.mX * multiplier;
				result.mY = multiplicand.mY * multiplier;
				result.mZ = multiplicand.mZ * multiplier;
				result.mW = multiplicand.mW * multiplier;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Multiply the values of two quaternions and return the result to the
		/// caller in a new quaternion.
		/// </summary>
		/// <param name="multiplicand">
		/// Reference to the starting quaternion to be multiplied upon.
		/// </param>
		/// <param name="multiplier">
		/// Reference to the quaternion to be multplied.
		/// </param>
		/// <returns>
		/// Reference to a new quaternion representing the result of the
		/// multiplication, if found.
		/// </returns>
		public static FQuaternion Multiply(FQuaternion multiplicand,
			FQuaternion multiplier)
		{
			double crossX = 0d;
			double crossY = 0d;
			double crossZ = 0d;
			double mag = 0d;
			double q1w = 0d;
			double q1x = 0d;
			double q1y = 0d;
			double q1z = 0d;
			double q2w = 0d;
			double q2x = 0d;
			double q2y = 0d;
			double q2z = 0d;
			FQuaternion result = new FQuaternion();

			if(multiplicand != null && multiplier != null)
			{
				q1x = (double)multiplicand.mX;
				q1y = (double)multiplicand.mY;
				q1z = (double)multiplicand.mZ;
				q1w = (double)multiplicand.mW;
				q2x = (double)multiplier.mX;
				q2y = (double)multiplier.mY;
				q2z = (double)multiplier.mZ;
				q2w = (double)multiplier.mW;

				crossX = (q1y * q2z) - (q1z * q2y);
				crossY = (q1z * q2x) - (q1x * q2z);
				crossZ = (q1x * q2y) - (q1y * q2x);

				mag = (q1x * q2x) + (q1y * q2y) + (q1z * q2z);

				result.mX = (float)((q1x * q2w) + (q2x * q1w) + crossX);
				result.mY = (float)((q1y * q2w) + (q2y * q1w) + crossY);
				result.mZ = (float)((q1z * q2w) + (q2z * q1w) + crossZ);
				result.mW = (float)((q1w * q2w) - mag);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Negate																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a negated version of the supplied quaternion.
		/// </summary>
		/// <param name="quaternion">
		/// Reference to the quaternion to inspect.
		/// </param>
		/// <returns>
		/// Reference to a newly created quaternion representing the negated
		/// contents of the caller's value, if legitimate.
		/// </returns>
		public static FQuaternion Negate(FQuaternion quaternion)
		{
			FQuaternion result = new FQuaternion();

			if(quaternion != null)
			{
				result.mX = 0f - quaternion.mX;
				result.mY = 0f - quaternion.mY;
				result.mZ = 0f - quaternion.mZ;
				result.mW = 0f - quaternion.mW;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Normalize																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a normalized version of the supplied quaternion.
		/// </summary>
		/// <param name="quaternion">
		/// Reference to the quaternion to normalize.
		/// </param>
		/// <returns>
		/// Reference to a normalized version of the quaternion, whose elements
		/// total 1.0.
		/// </returns>
		public static FQuaternion Normalize(FQuaternion quaternion)
		{
			double inverseNormal = 1d;
			double magnitude = 0d;
			FQuaternion result = new FQuaternion();

			if(quaternion != null)
			{
				magnitude = (double)Length(quaternion);
				if(magnitude != 0d)
				{
					inverseNormal = 1d / magnitude;
					result.mX = (float)((double)quaternion.mX * inverseNormal);
					result.mY = (float)((double)quaternion.mY * inverseNormal);
					result.mZ = (float)((double)quaternion.mZ * inverseNormal);
					result.mW = (float)((double)quaternion.mW * inverseNormal);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Set																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the properties of the specified quaternion through an abbreviated
		/// method.
		/// </summary>
		/// <param name="quaternion">
		/// Reference to the quaternion whose properties are to be updated.
		/// </param>
		/// <param name="x">
		/// X value to set.
		/// </param>
		/// <param name="y">
		/// Y value to set.
		/// </param>
		/// <param name="z">
		/// Z value to set.
		/// </param>
		/// <param name="w">
		/// W value to set.
		/// </param>
		/// <remarks>
		/// Using the set method bypasses any events in this version.
		/// </remarks>
		public static void Set(FQuaternion quaternion, float x, float y, float z,
			float w)
		{
			if(quaternion != null)
			{
				quaternion.mX = x;
				quaternion.mY = y;
				quaternion.mZ = z;
				quaternion.mW = w;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SetIdentity																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clear the contents of the provided quaternion, setting its value to
		/// that of identity.
		/// </summary>
		/// <param name="quaternion">
		/// Reference to the quaternion to reset.
		/// </param>
		/// <remarks>
		/// This method bypasses events in this version.
		/// </remarks>
		public static void SetIdentity(FQuaternion quaternion)
		{
			Set(quaternion, 0f, 0f, 0f, 1f);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SLerp																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the specified Spherical Linear Interpolation value between the
		/// two provided quaternions.
		/// </summary>
		/// <param name="quaternion1">
		/// Reference to the starting quaternion in the range.
		/// </param>
		/// <param name="quaternion2">
		/// Reference to the ending quaternion in the range.
		/// </param>
		/// <param name="portion">
		/// The portion of the distance between the first and second quaternions,
		/// as a decimal value between 0 and 1.
		/// </param>
		/// <returns>
		/// Reference to a new quaternion containing a spherical linear
		/// interpolation between the two supplied by the caller.
		/// </returns>
		public static FQuaternion SLerp(FQuaternion quaternion1,
			FQuaternion quaternion2, float portion)
		{
			double cosHalf = 0d;
			double half = 0d;
			double positionA = 0d;
			double positionB = 0d;
			FQuaternion result = new FQuaternion();
			double sinHalf = 0d;
			double w1 = 0d;
			double w2 = 0d;
			double x1 = 0d;
			double x2 = 0d;
			double y1 = 0d;
			double y2 = 0d;
			double z1 = 0d;
			double z2 = 0d;


			if(quaternion1 != null && quaternion2 != null)
			{
				x1 = (double)quaternion1.mX;
				y1 = (double)quaternion1.mY;
				z1 = (double)quaternion1.mZ;
				w1 = (double)quaternion1.mW;
				x2 = (double)quaternion2.mX;
				y2 = (double)quaternion2.mY;
				z2 = (double)quaternion2.mZ;
				w2 = (double)quaternion2.mW;

				cosHalf = (x1 * x2) + (y1 * y2) + (z1 * z2) + (w1 * w2);
				if(Math.Abs(cosHalf) > 1d)
				{
					// if q1 = q2 or qa = 0 - q2 then theta = 0 and we can return q1.
					TransferValues(quaternion1, result);
				}
				else
				{
					half = Math.Acos(cosHalf);
					sinHalf = Math.Sqrt(1d - cosHalf * cosHalf);
					// If theta is 180 degrees, then the result isn't well defined.
					// Rotate around any axis normal to either q1 or q2.
					if(Math.Abs(sinHalf) < Epsilon)
					{
						result.mX = (float)(x1 * 0.5d + x2 * 0.5d);
						result.mY = (float)(y1 * 0.5d + y2 * 0.5d);
						result.mZ = (float)(z1 * 0.5d + z2 * 0.5d);
						result.mW = (float)(w1 * 0.5d + w2 * 0.5d);
					}
					else
					{
						//	Use the general Slerp calculation.
						positionA = Math.Sin((1d - portion) * half) / sinHalf;
						positionB = Math.Sin(portion * half) / sinHalf;
						result.mX = (float)(x1 * positionA + x2 * positionB);
						result.mY = (float)(y1 * positionA + y2 * positionB);
						result.mZ = (float)(z1 * positionA + z2 * positionB);
						result.mW = (float)(w1 * positionA + w2 * positionB);
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Subtract																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Subtract the second quaternion from the first, returning the result as
		/// a new quaternion.
		/// </summary>
		/// <param name="minuend">
		/// Reference to the quaternion representing the starting value.
		/// </param>
		/// <param name="subtrahend">
		/// Reference to the quaternion representing the amount to be subtracted.
		/// </param>
		/// <returns>
		/// Reference to a new quaternion containing the result of the subtraction,
		/// if legitimate.
		/// </returns>
		public static FQuaternion Subtract(FQuaternion minuend,
			FQuaternion subtrahend)
		{
			FQuaternion result = new FQuaternion();

			if(minuend != null && subtrahend != null)
			{
				result.mX = minuend.mX - subtrahend.mX;
				result.mY = minuend.mY - subtrahend.mY;
				result.mZ = minuend.mZ - subtrahend.mZ;
				result.mW = minuend.mW - subtrahend.mW;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ToAxisAngle																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the arbitrary axis vector and associated angle from the
		/// caller's quaternion.
		/// </summary>
		/// <param name="quaternion">
		/// Reference to the quaternion to inspect.
		/// </param>
		/// <returns>
		/// Reference to a four dimensional vector where X, Y, and Z represent
		/// the axis in space and W represents the angle at which that axis
		/// is twisted.
		/// </returns>
		/// <remarks>
		/// For more information on the formula upon which this method is
		/// based, see
		/// <see href="https://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToAngle/#google_vignette">
		/// quaternionToAngle on euclideanspace.com</see>.
		/// </remarks>
		public static FVector4 ToAxisAngle(FQuaternion quaternion)
		{
			double angle = 0d;
			double divider = 0d;
			FVector4 result = new FVector4();
			double w = 0d;

			if(quaternion != null)
			{
				w = (double)quaternion.W;
				angle = Math.Acos(w) * 2d;
				divider = Math.Sqrt(1d - (w * w));

				if(divider != 0d)
				{
					//	The axis can be calculated.
					divider = 1d / divider;
					result.X = (float)((double)quaternion.X * divider);
					result.Y = (float)((double)quaternion.Y * divider);
					result.Z = (float)((double)quaternion.Z * divider);
					result.W = (float)angle;
				}
				else
				{
					//	Return an arbitrary normalized axis.
					result.X = 1f;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ToEuler																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a vector containing Tait-Euler angles representing the
		/// specified quaternion.
		/// </summary>
		/// <param name="quaternion">
		/// Reference to the quaternion to be converted.
		/// </param>
		/// <returns>
		/// Reference to a vector containing Tait-Euler angles, ordered as ZYX,
		/// if valid.
		/// </returns>
		public static FVector3 ToEuler(FQuaternion quaternion)
		{
			double cosR_cosP = 0d;
			double cosY_cosP = 0d;
			FVector3 result = new FVector3();
			double sinP = 0d;
			double sinR_cosP = 0d;
			double sinY_cosP = 0d;
			double w = 0d;
			double x = 0d;
			double y = 0d;
			double z = 0d;

			if(quaternion != null)
			{
				w = (double)quaternion.W;
				x = (double)quaternion.X;
				y = (double)quaternion.Y;
				z = (double)quaternion.Z;

				//	X.
				sinR_cosP = 2d * ((w * x) + (y * z));
				cosR_cosP = 1d - 2d * ((x * x) + (y * y));
				result.X = (float)(Math.Atan2(sinR_cosP, cosR_cosP));

				//	Y.
				sinP = 2d * ((w * y) - (z * x));
				if(Math.Abs(sinP) >= 1d)
				{
					//	For this value to 90 degrees when out of range.
					result.Y = (float)CopySign(Math.PI / 2d, sinP);
				}
				else
				{
					result.Y = (float)Math.Asin(sinP);
				}

				//	Z.
				sinY_cosP = 2d * ((w * z) + (x * y));
				cosY_cosP = 1d - 2d * ((y * y) + (z * z));
				result.Z = (float)Math.Atan2(sinY_cosP, cosY_cosP);

				if(float.IsNaN(result.X))
				{
					result.X = 0f;
				}
				if(float.IsNaN(result.Y))
				{
					result.Y = 0f;
				}
				if(float.IsNaN(result.Z))
				{
					result.Z = 0f;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ToPitchRollYaw																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a vector where the values represent the Euler rotations of the
		/// caller's quaternion.
		/// </summary>
		/// <param name="quaternion">
		/// Reference to the quaternion to convert.
		/// </param>
		/// <returns>
		/// Reference to a newly created vector containing Euler rotations, if
		/// valid (XYZ order).
		/// </returns>
		public static FVector3 ToPitchRollYaw(FQuaternion quaternion)
		{
			double cosR = 0d;
			double cosY = 0d;
			FQuaternion normalized = null;
			FVector3 result = new FVector3();
			double sinP = 0d;
			double sinR = 0d;
			double sinY = 0d;
			double w = 0d;
			double x = 0d;
			double y = 0d;
			double z = 0d;

			if(quaternion != null)
			{
				normalized = FQuaternion.Normalize(quaternion);
				x = (double)normalized.X;
				y = (double)normalized.Y;
				z = (double)normalized.Z;
				w = (double)normalized.W;

				//	Pitch.
				sinP = 2d * ((w * y) - (x * z));
				result.X = (float)Math.Asin(sinP);

				//	Roll.
				sinR = 2d * ((w * z) + (x * y));
				cosR = 1d - 2d * ((y * y) + (z * z));
				result.Y = (float)Math.Atan2(sinR, cosR);

				//	Yaw.
				sinY = 2d * ((w * x) + (y * z));
				cosY = 1d - 2d * ((x * x) + (y * y));
				result.Z = (float)Math.Atan2(sinY, cosY);

				if(float.IsNaN(result.X))
				{
					result.X = 0f;
				}
				if(float.IsNaN(result.Y))
				{
					result.Y = 0f;
				}
				if(float.IsNaN(result.Z))
				{
					result.Z = 0f;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ToString																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the string representation of this object.
		/// </summary>
		/// <returns>
		/// The string representation of this quaternion.
		/// </returns>
		public override string ToString()
		{
			return $"X:{mX:0.###}, Y:{mY:0.###}, Z:{mZ:0.###}, W:{mW:0.###}";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TransferValues																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Transfer the values of the source quaternion to the target.
		/// </summary>
		/// <param name="source">
		/// The source quaternion whose values will be copied.
		/// </param>
		/// <param name="target">
		/// The target quaternion to receive the values.
		/// </param>
		/// <remarks>
		/// This method raises events where appropriate.
		/// </remarks>
		public static void TransferValues(FQuaternion source, FQuaternion target)
		{
			if(source != null && target != null)
			{
				target.X = source.mX;
				target.Y = source.mY;
				target.Z = source.mZ;
				target.W = source.mW;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	W																																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="W">W</see>.
		/// </summary>
		private float mW = 1f;
		/// <summary>
		/// Get/Set the value of the W coordinate.
		/// </summary>
		public float W
		{
			get { return mW; }
			set
			{
				float originalValue = mW;
				mW = value;
				if(mW != originalValue)
				{
					OnCoordinateChanged(
						new FloatPointEventArgs("W", value, originalValue));
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
		private float mX = 0f;
		/// <summary>
		/// Get/Set the value of the X coordinate.
		/// </summary>
		public float X
		{
			get { return mX; }
			set
			{
				float originalValue = mX;
				mX = value;
				if(mX != originalValue)
				{
					OnCoordinateChanged(
						new FloatPointEventArgs("X", value, originalValue));
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
		private float mY = 0f;
		/// <summary>
		/// Get/Set the value of the Y coordinate.
		/// </summary>
		public float Y
		{
			get { return mY; }
			set
			{
				float originalValue = mY;
				mY = value;
				if(mY != originalValue)
				{
					OnCoordinateChanged(
						new FloatPointEventArgs("Y", value, originalValue));
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
		private float mZ = 0f;
		/// <summary>
		/// Get/Set the value of the Z coordinate.
		/// </summary>
		public float Z
		{
			get { return mZ; }
			set
			{
				float originalValue = mZ;
				mZ = value;
				if(mZ != originalValue)
				{
					OnCoordinateChanged(
						new FloatPointEventArgs("Z", value, originalValue));
				}
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
