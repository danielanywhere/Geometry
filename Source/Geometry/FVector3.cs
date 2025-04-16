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

using Newtonsoft.Json;

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
		/// <summary>
		/// The constant X index in a vector.
		/// </summary>
		private const int vX = 0;
		/// <summary>
		/// The constant Y index in a vector.
		/// </summary>
		private const int vY = 1;
		/// <summary>
		/// The constant Z index in a vector.
		/// </summary>
		private const int vZ = 2;

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
			mValues[vX] = x;
			mValues[vY] = y;
			mValues[vZ] = z;
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
		public static FVector3 operator -(FVector3 minuend, FVector3 subtrahend)
		{
			FVector3 result = new FVector3();

			if(minuend != null && subtrahend != null)
			{
				Assign(result,
					minuend.mValues[vX] - subtrahend.mValues[vX],
					minuend.mValues[vY] - subtrahend.mValues[vY],
					minuend.mValues[vZ] - subtrahend.mValues[vZ]);
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
		public static FVector3 operator *(FVector3 multiplicand, float multiplier)
		{
			FVector3 result = new FVector3();

			if(multiplicand != null)
			{
				Assign(result,
					multiplicand.mValues[vX] * multiplier,
					multiplicand.mValues[vY] * multiplier,
					multiplicand.mValues[vZ] * multiplier);
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
					multiplicand.mValues[vX] * multiplier.mValues[vX],
					multiplicand.mValues[vY] * multiplier.mValues[vY],
					multiplicand.mValues[vZ] * multiplier.mValues[vZ]);
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
		public static FVector3 operator /(FVector3 divisor, FVector3 dividend)
		{
			FVector3 result = new FVector3();

			if(divisor != null && dividend != null)
			{
				Assign(result,
					(dividend.mValues[vX] != 0f ?
						divisor.mValues[vX] / dividend.mValues[vX] : 0f),
					(dividend.mValues[vY] != 0f ?
						divisor.mValues[vY] / dividend.mValues[vY] : 0f),
					(dividend.mValues[vZ] != 0f ?
						divisor.mValues[vZ] / dividend.mValues[vZ] : 0f));
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
		public static FVector3 operator +(FVector3 addend1, float addend2)
		{
			FVector3 result = new FVector3();

			if(addend1 != null)
			{
				result.mValues[vX] = addend1.mValues[vX] + addend2;
				result.mValues[vY] = addend1.mValues[vY] + addend2;
				result.mValues[vZ] = addend1.mValues[vZ] + addend2;
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
					addend1.mValues[vX] + addend2.mValues[vX],
					addend1.mValues[vY] + addend2.mValues[vY],
					addend1.mValues[vZ] + addend2.mValues[vZ]);
			}
			return result;
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
		//*	_Implicit FPoint3 = FVector3																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cast the FVector3 instance to an FPoint3.
		/// </summary>
		/// <param name="value">
		/// Reference to the FVector3 value to be converted.
		/// </param>
		/// <returns>
		/// Reference to a newly created FPoint3 whose values represent those
		/// in the caller's FVector3 source.
		/// </returns>
		public static implicit operator FPoint3(FVector3 value)
		{
			FPoint3 result = new FPoint3();

			if(value?.mValues.Length > 0)
			{
				result.X = value.mValues[vX];
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
		//*	_Implicit FVector3 = FPoint3																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cast the FPoint3 instance to an FVector3.
		/// </summary>
		/// <param name="value">
		/// Reference to the point to be converted.
		/// </param>
		/// <returns>
		/// Reference to a newly created FVector3 representing the values in
		/// the caller's point.
		/// </returns>
		public static implicit operator FVector3(FPoint3 value)
		{
			FVector3 result = new FVector3();

			if(value != null)
			{
				result.mValues[vX] = value.X;
				result.mValues[vY] = value.Y;
				result.mValues[vZ] = value.Z;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

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
			int count = 0;
			int index = 0;
			FVector2 result = new FVector2();

			if(value?.mValues.Length > 0)
			{
				count = Math.Min(result.Values.Length, value.mValues.Length);
				for(index = 0; index < count; index ++)
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
		public static void Assign(FVector3 source, FVector3 target)
		{
			if(source != null && target != null)
			{
				target.mValues[vX] = source.mValues[vX];
				target.mValues[vY] = source.mValues[vY];
				target.mValues[vZ] = source.mValues[vZ];
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
				vector.mValues[vX] =
					vector.mValues[vY] =
					vector.mValues[vZ] = value;
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
				vector.mValues[vX] = x;
				vector.mValues[vY] = y;
				vector.mValues[vZ] = z;
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
		public static void Assign(FVector3 vector, float[] values)
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
			int count = 0;
			int index = 0;
			FVector3 result = new FVector3();

			if(vector != null)
			{
				count = vector.mValues.Length;
				if(result.mValues.Length != count)
				{
					result.mValues = new float[count];
				}
				for(index = 0; index < count; index ++)
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
		public static FVector3 CrossProduct(FVector3 vector1, FVector3 vector2)
		{
			FVector3 result = new FVector3();

			if(vector1 != null && vector2 != null)
			{
				Assign(result,
					(vector1.mValues[vY] * vector2.mValues[vZ]) -
					(vector2.mValues[vY] * vector1.mValues[vZ]),
					((vector1.mValues[vX] * vector2.mValues[vZ]) -
					(vector2.mValues[vX] * vector1.mValues[vZ]) * -1f),
					(vector1.mValues[vX] * vector2.mValues[vY]) -
					(vector2.mValues[vX] * vector1.mValues[vY]));
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
		public static float DotProduct(FVector3 vectorA, FVector3 vectorB)
		{
			float result = 0f;

			if(vectorA != null && vectorB != null)
			{
				result =
					(vectorA.mValues[vX] * vectorB.mValues[vX]) +
					(vectorA.mValues[vY] * vectorB.mValues[vY]) +
					(vectorA.mValues[vZ] * vectorB.mValues[vZ]);
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
		public bool Equals(FVector3 value)
		{
			return (value != null &&
				value.mValues[vX] == this.mValues[vX] &&
				value.mValues[vY] == this.mValues[vY] &&
				value.mValues[vZ] == this.mValues[vZ]);
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
				result = vector.mValues[vX] == x &&
					vector.mValues[vY] == y &&
					vector.mValues[vZ] == z;
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
		/// Angles to apply, in radians. Notice that the Y rotation is ignored.
		/// X-axis is azimuth and Z-axis is zenith.
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
			FVector3 result = new FVector3();

			if(point != null && theta != null && length != 0.0)
			{
				result.mValues[vX] =
					point.mValues[vX] +
					(float)((double)length *
						Math.Sin((double)theta.mValues[vZ]) *
						Math.Cos((double)theta.mValues[vX]));
				result.mValues[vY] =
					point.mValues[vY] +
					(float)((double)length *
						Math.Sin((double)theta.mValues[vZ]) *
						Math.Sin((double)theta.mValues[vX]));
				result.mValues[vZ] =
					point.mValues[vZ] +
					(float)((double)length * Math.Cos((double)theta.mValues[vZ]));
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
		public static FVector3 GetLineAngle(FVector3 pointA, FVector3 pointB)
		{
			float length = 0f;
			FPoint point = null;
			FVector3 result = new FVector3();
			FVector3 vecA = null;
			FVector3 vecB = null;

			if(pointA != null & pointB != null)
			{
				vecA = new FVector3(pointA);
				vecB = new FVector3(pointB);
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
		public static float GetLineDistance(FVector3 vectorA, FVector3 vectorB)
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
					destinationX - point.mValues[vX],
					destinationY - point.mValues[vY],
					destinationZ - point.mValues[vZ]);
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
							a = 0f - DotProduct(n, w0);
							b = DotProduct(n, dir);
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
							uu = DotProduct(u, u);
							uv = DotProduct(u, v);
							vv = DotProduct(v, v);
							w = result - faceCoordinates[triP0];
							wu = DotProduct(w, u);
							wv = DotProduct(w, v);
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
			int count = 0;
			int index = 0;
			FVector3 result = new FVector3();

			if(vector != null)
			{
				count = vector.mValues.Length;
				for(index = 0; index < count; index ++)
				{
					result.mValues[index] = 0f - vector.mValues[index];
				}
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
			int count = 0;
			int index = 0;
			bool result = true;

			if(vector != null &&
				vector.mValues.Length > 0)
			{
				count = vector.mValues.Length;
				for(index = 0; index < count; index ++)
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
		public static FVector3 Mask(FVector3 vector, string mask)
		{
			string m = "";
			FVector3 result = new FVector3();

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
					result.mValues[vX] +=
						(current.mValues[vY] - next.mValues[vY]) *
						(current.mValues[vZ] + next.mValues[vZ]);
					result.mValues[vY] +=
						(current.mValues[vZ] - next.mValues[vZ]) *
						(current.mValues[vX] + next.mValues[vX]);
					result.mValues[vZ] +=
						(current.mValues[vX] - next.mValues[vX]) *
						(current.mValues[vY] + next.mValues[vY]);
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
					result.mValues[0] = vector.mValues[0] / length;
					result.mValues[1] = vector.mValues[1] / length;
					result.mValues[2] = vector.mValues[2] / length;
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
		public static FVector3 ReverseDirection(FVector3 vector)
		{
			FVector3 result = new FVector3();

			if(vector != null)
			{
				result.mValues[vX] = vector.mValues[vX] * -1f;
				result.mValues[vY] = vector.mValues[vY] * -1f;
				result.mValues[vZ] = vector.mValues[vZ] * -1f;
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
				if(thetaZ != 0.0)
				{
					result = FMatrix3.RotateZ(result, thetaZ);
				}
				if(thetaY != 0.0)
				{
					result = FMatrix3.RotateY(result, thetaY);
				}
				if(thetaX != 0.0)
				{
					result = FMatrix3.RotateX(result, thetaX);
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
		public static FVector3 Scale(FVector3 vector, FVector3 scale)
		{
			FVector3 result = null;

			if(vector != null && scale != null)
			{
				result = vector * scale;
			}
			else
			{
				result = new FVector3();
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
				result = vector.mValues[vX] + vector.mValues[vY] + vector.mValues[vZ];
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
					vector.mValues[vX], vector.mValues[vZ], vector.mValues[vY]);
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
					Trig.RadToDeg(rayAngle.mValues[vX]),
					Trig.RadToDeg(rayAngle.mValues[vY]),
					Trig.RadToDeg(rayAngle.mValues[vZ]));
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
		public static void TransferValues(FVector3 source, FVector3 target)
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
				$"{mValues[vX]:0.000}, " +
				$"{mValues[vY]:0.000}, " +
				$"{mValues[vZ]:0.000}";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Values																																*
		//*-----------------------------------------------------------------------*
		private float[] mValues = new float[3];
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
