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
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

using static Geometry.GeometryUtil;


//	TODO: Add Suspended property to allow multiple changes to take place on single event.

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FPoint3																																	*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Single precision 3D point.
	/// </summary>
	/// <remarks>
	/// This class exists to take advantage of first-class object behavior
	/// during operation. It is is accessible by reference wherever it is used.
	/// As a result, once the item is assigned in a helper function, its members
	/// continue to carry exactly the same information as the root instance until
	/// they are changed.
	/// </remarks>
	[Obsolete("FPoint3 is obsolete. Please use FVector3.")]
	public class FPoint3
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
		/// Create a new Instance of the FPoint3 Item.
		/// </summary>
		public FPoint3()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the FPoint3 Item.
		/// </summary>
		/// <param name="x">
		/// X coordinate.
		/// </param>
		/// <param name="y">
		/// Y coordinate.
		/// </param>
		/// <param name="z">
		/// Z coordinate.
		/// </param>
		public FPoint3(float x, float y, float z)
		{
			mX = x;
			mY = y;
			mZ = z;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the FPoint3 Item.
		/// </summary>
		/// <param name="source">
		/// Reference to an instance of an FPoint3 to use for reference values.
		/// </param>
		public FPoint3(FPoint3 source)
		{
			if(source != null)
			{
				mX = source.X;
				mY = source.Y;
				mZ = source.Z;
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the FPoint3 Item.
		/// </summary>
		/// <param name="source">
		/// Reference to an instance of an FVector3 to use for reference values.
		/// </param>
		public FPoint3(FVector3 source)
		{
			if(source != null)
			{
				mX = source.X;
				mY = source.Y;
				mZ = source.Z;
			}
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

			if(value != null)
			{
				result.mX = value.X;
				result.mY = value.Y;
				result.mZ = value.Z;
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
				result.X = value.mX;
				result.Y = value.mY;
				result.Z = value.mZ;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Implicit FPoint3 = FPoint																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cast the FPoint instance to a FPoint3.
		/// </summary>
		/// <param name="value">
		/// Reference to the 2D point to convert.
		/// </param>
		/// <returns>
		/// Reference to the newly created 3D copy of the caller's 2D point, if
		/// valid. Otherwise, an empty point.
		/// </returns>
		public static implicit operator FPoint3(FPoint value)
		{
			FPoint3 result = null;

			if(value != null)
			{
				result = new FPoint3()
				{
					mX = value.X,
					mY = value.Y
				};
			}
			if(result == null)
			{
				result = new FPoint3();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* _Operator *																														*
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
		public static FPoint3 operator *(float scalar, FPoint3 point)
		{
			FPoint3 result = new FPoint3();

			if(point != null && scalar != 0f)
			{
				result.mX = scalar * point.mX;
				result.mY = scalar * point.mY;
				result.mZ = scalar * point.mZ;
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
		public static FPoint3 operator *(FPoint3 point, float scalar)
		{
			FPoint3 result = new FPoint3();

			if(point != null && scalar != 0f)
			{
				result.mX = scalar * point.mX;
				result.mY = scalar * point.mY;
				result.mZ = scalar * point.mZ;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Operator /																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of one point divided by the other.
		/// </summary>
		/// <param name="divisor">
		/// A reference to the divisor.
		/// </param>
		/// <param name="dividend">
		/// A reference to the dividend.
		/// </param>
		/// <returns>
		/// Reference to the point division result.
		/// </returns>
		public static FPoint3 operator /(FPoint3 divisor, FPoint3 dividend)
		{
			FPoint3 result = new FPoint3();

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
		public static FPoint3 operator +(FPoint3 pointA, FPoint3 pointB)
		{
			FPoint3 result = new FPoint3();

			if(pointA != null)
			{
				result.mX = pointA.mX;
				result.mY = pointA.mY;
				result.mZ = pointA.mZ;
			}
			if(pointB != null)
			{
				result.mX += pointB.mX;
				result.mY += pointB.mY;
				result.mZ += pointB.mZ;
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
		public static FPoint3 operator -(FPoint3 pointA, FPoint3 pointB)
		{
			FPoint3 result = new FPoint3();

			if(pointA != null)
			{
				result.mX = pointA.mX;
				result.mY = pointA.mY;
				result.mZ = pointA.mZ;
			}
			if(pointB != null)
			{
				result.mX -= pointB.mX;
				result.mY -= pointB.mY;
				result.mZ -= pointB.mZ;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Operator !=																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether values of two points are not equal.
		/// </summary>
		/// <param name="pointA">
		/// Reference to the first point to compare.
		/// </param>
		/// <param name="pointB">
		/// Reference to the second point to compare.
		/// </param>
		/// <returns>
		/// True if the two objects are substantially not equal in value.
		/// Otherwise, false.
		/// </returns>
		[DebuggerStepThrough]
		public static bool operator !=(FPoint3 pointA, FPoint3 pointB)
		{
			bool result = true;

			if((object)pointA != null && (object)pointB != null)
			{
				result = !(pointA == pointB);
			}
			else if((object)pointA == null && (object)pointB == null)
			{
				result = false;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Operator FPoint3 == FPoint3																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether values of two points are equal.
		/// </summary>
		/// <param name="pointA">
		/// Reference to the first point to compare.
		/// </param>
		/// <param name="pointB">
		/// Reference to the second point to compare.
		/// </param>
		/// <returns>
		/// True if the two objects are substantially equal in value. Otherwise,
		/// false.
		/// </returns>
		[DebuggerStepThrough]
		public static bool operator ==(FPoint3 pointA, FPoint3 pointB)
		{
			bool result = true;

			if((object)pointA != null && (object)pointB != null)
			{
				result = (
					(pointA.mX == pointB.mX) &&
					(pointA.mY == pointB.mY) &&
					(pointA.mZ == pointB.mZ));
			}
			else if((object)pointA != null || (object)pointB != null)
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
		/// Set the base values of the target point from the source.
		/// </summary>
		/// <param name="source">
		/// Reference to the source point.
		/// </param>
		/// <param name="target">
		/// Reference to the target point.
		/// </param>
		public static void Assign(FPoint3 source, FPoint3 target)
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
		/// Set the value of the point.
		/// </summary>
		/// <param name="point">
		/// Reference to the point to be populated.
		/// </param>
		/// <param name="value">
		/// Value to assign.
		/// </param>
		public static void Assign(FPoint3 point, float value)
		{
			if(point != null)
			{
				point.mX =
					point.mY =
					point.mZ = value;
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Set the value of the point.
		/// </summary>
		/// <param name="point">
		/// Reference to the point to be populated.
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
		public static void Assign(FPoint3 point, float x, float y, float z)
		{
			if(point != null)
			{
				point.mX = x;
				point.mY = y;
				point.mZ = z;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a memberwise clone of the provided point.
		/// </summary>
		/// <param name="point">
		/// Reference to the source point to be cloned.
		/// </param>
		/// <returns>
		/// Reference to a new FPoint instance where the primitive member values
		/// are the same as those in the source, if a legitimate source was
		/// provided. Otherwise, an empty FPoint.
		/// </returns>
		public static FPoint3 Clone(FPoint3 point)
		{
			FPoint3 result = new FPoint3();

			if(point != null)
			{
				result.mReadOnly = point.mReadOnly;
				result.mX = point.mX;
				result.mY = point.mY;
				result.mZ = point.mZ;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a memberwise clone of the provided point.
		/// </summary>
		/// <param name="vector">
		/// Reference to the source vector to be cloned.
		/// </param>
		/// <returns>
		/// Reference to a new FPoint instance where the primitive member values
		/// are the same as those in the source, if a legitimate source was
		/// provided. Otherwise, an empty FPoint.
		/// </returns>
		public static FPoint3 Clone(FVector3 vector)
		{
			FPoint3 result = new FPoint3();

			if(vector != null)
			{
				result.mReadOnly = vector.ReadOnly;
				result.mX = vector.X;
				result.mY = vector.Y;
				result.mZ = vector.Z;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Color																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Color">Color</see>.
		/// </summary>
		private FColor4 mColor = new FColor4(1f, 0f, 0f, 0f);
		/// <summary>
		/// Get/Set a reference to the color of this line.
		/// </summary>
		public FColor4 Color
		{
			get { return mColor; }
			set { mColor = value; }
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
		public static FPoint3 Delta(FPoint3 pointA, FPoint3 pointB)
		{
			FPoint3 result = new FPoint3();

			if(pointA != null && pointB != null)
			{
				result.mX = pointB.mX - pointA.mX;
				result.mY = pointB.mY - pointA.mY;
				result.mZ = pointB.mZ - pointA.mZ;
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

			if(obj is FPoint3 @point)
			{
				if(point.mX == mX && point.mY == mY && point.mZ == mZ)
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
			result *= (factor + mZ.GetHashCode());
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
		public static FPoint3 Invert(FPoint3 point)
		{
			FPoint3 result = new FPoint3();

			if(point != null)
			{
				if(point.mX != 0f)
				{
					result.mX = 1f / point.mX;
				}
				if(point.mY != 0f)
				{
					result.mY = 1f / point.mY;
				}
				if(point.mZ != 0f)
				{
					result.mZ = 1f / point.mZ;
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
		public static FPoint3 MiddlePoint(FPoint3 pointA, FPoint3 pointB)
		{
			FPoint3 result = new FPoint3();

			if(pointA != null && pointB != null)
			{
				result.X = (pointA.X + pointB.X) / 2f;
				result.Y = (pointA.Y + pointB.Y) / 2f;
				result.Z = (pointA.Z + pointB.Z) / 2f;
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
		/// <param name="point">
		/// Reference to the point to be negated.
		/// </param>
		/// <returns>
		/// Reference to the caller's point with negated values.
		/// </returns>
		public static FPoint3 Negate(FPoint3 point)
		{
			FPoint3 result = new FPoint3();

			if (point != null)
			{
				result.mX = 0f - point.mX;
				result.mY = 0f - point.mY;
				result.mZ = 0f - point.mZ;
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
		/// <param name="dz">
		/// Z distance from the original point.
		/// </param>
		/// <returns>
		/// Reference to a new point at the specified offset from the original.
		/// </returns>
		public static FPoint3 Offset(FPoint3 point, float dx, float dy, float dz)
		{
			FPoint3 result = null;

			if(point != null)
			{
				result = new FPoint3(point.mX + dx, point.mY + dy, point.mZ + dz);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Parse																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Parse a coordinate string and return its FPoint3 representation.
		/// </summary>
		/// <param name="coordinate">
		/// The coordinate string to parse.
		/// </param>
		/// <param name="allowNull">
		/// Value indicating whether to allow a null return value if the input
		/// string was invalid.
		/// </param>
		/// <returns>
		/// Newly created FPoint3 value representing the caller's input, if
		/// that input was legal or allowNull was false. Otherwise, a null value.
		/// </returns>
		public static FPoint3 Parse(string coordinate, bool allowNull = false)
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
		private bool mReadOnly = false;
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
		/// <summary>
		/// Rotate the caller's point around the origin.
		/// </summary>
		/// <param name="x">
		/// The X value to be rotated.
		/// </param>
		/// <param name="y">
		/// The Y value to be rotated.
		/// </param>
		/// <param name="z">
		/// The Z value to be rotated.
		/// </param>
		/// <param name="thetaX">
		/// The angle at which to rotate the point around the X axis, in radians.
		/// </param>
		/// <param name="thetaY">
		/// The angle at which to rotate the point around the Y axis, in radians.
		/// </param>
		/// <param name="thetaZ">
		/// The angle at which to rotate the point around the Z axis, in radians.
		/// </param>
		/// <returns>
		/// Reference to a representation of the caller's point after being
		/// rotated by the specified angles around the origin.
		/// </returns>
		public static FPoint3 Rotate(float x, float y, float z,
			float thetaX, float thetaY, float thetaZ)
		{
			FMatrix3 mxX = new FMatrix3()
			{
				Values = new float[,]
				{
					{ 1f, 0f, 0f },
					{ 0f, (float)Math.Cos(thetaX), 0f - (float)Math.Sin(thetaX) },
					{ 0f, (float)Math.Sin(thetaX), (float)Math.Cos(thetaX) }
				}
			};
			FMatrix3 mxY = new FMatrix3()
			{
				Values = new float[,]
				{
					{ (float)Math.Cos(thetaY), 0f, (float)Math.Sin(thetaY) },
					{ 0f, 1f, 0f },
					{ 0f - (float)Math.Sin(thetaY), 0f, (float)Math.Cos(thetaY) }
				}
			};
			FMatrix3 mxZ = new FMatrix3()
			{
				Values = new float[,]
				{
					{ (float)Math.Cos(thetaZ), 0f - (float)Math.Sin(thetaZ), 0f },
					{ (float)Math.Sin(thetaZ), (float)Math.Cos(thetaZ), 0f },
					{ 0f, 0f, 1f }
				},
			};
			FVector3 point = new FPoint3(x, y, z);

			point = FMatrix3.Multiply(mxZ, point);
			point = FMatrix3.Multiply(mxY, point);
			point = FMatrix3.Multiply(mxX, point);

			return new FPoint3(point);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Rotate the caller's point around the origin.
		/// </summary>
		/// <param name="point">
		/// Reference to the point to be rotated.
		/// </param>
		/// <param name="thetaX">
		/// The angle at which to rotate the point on the X axis, in radians.
		/// </param>
		/// <param name="thetaY">
		/// The angle at which to rotate the point on the Y axis, in radians.
		/// </param>
		/// <param name="thetaZ">
		/// The angle at which to rotate the point on the Z axis, in radians.
		/// </param>
		/// <returns>
		/// Reference to a representation of the caller's point after being
		/// rotated by the specified angle around the origin.
		/// </returns>
		public static FPoint3 Rotate(FPoint3 point,
			float thetaX, float thetaY, float thetaZ)
		{
			FMatrix3 mxX = new FMatrix3()
			{
				Values = new float[,]
				{
					{ 1f, 0f, 0f },
					{ 0f, (float)Math.Cos(thetaX), 0f - (float)Math.Sin(thetaX) },
					{ 0f, (float)Math.Sin(thetaX), (float)Math.Cos(thetaX) }
				}
			};
			FMatrix3 mxY = new FMatrix3()
			{
				Values = new float[,]
				{
					{ (float)Math.Cos(thetaY), 0f, (float)Math.Sin(thetaY) },
					{ 0f, 1f, 0f },
					{ 0f - (float)Math.Sin(thetaY), 0f, (float)Math.Cos(thetaY) }
				}
			};
			FMatrix3 mxZ = new FMatrix3()
			{
				Values = new float[,]
				{
					{ (float)Math.Cos(thetaZ), 0f - (float)Math.Sin(thetaZ), 0f },
					{ (float)Math.Sin(thetaZ), (float)Math.Cos(thetaZ), 0f },
					{ 0f, 0f, 1f }
				},
			};
			FVector3 localPoint = null;

			if(point != null)
			{
				localPoint = point;
				localPoint = FMatrix3.Multiply(mxZ, localPoint);
				localPoint = FMatrix3.Multiply(mxY, localPoint);
				localPoint = FMatrix3.Multiply(mxX, localPoint);
			}
			return new FPoint3(localPoint);
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
		public static FPoint3 Scale(FPoint3 point, float scale)
		{
			FPoint3 result = new FPoint3();

			if(point != null)
			{
				result.mX = point.mX * scale;
				result.mY = point.mY * scale;
				result.mZ = point.mZ * scale;
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
		//*	X																																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="X">X</see>.
		/// </summary>
		private float mX = 0f;
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
		private float mY = 0f;
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
		private float mZ = 0f;
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
