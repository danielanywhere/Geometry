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
	//*	FRotation3																															*
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
	[Obsolete("FRotation3 is obsolete. Please use FVector3.")]
	public class FRotation3
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	OnRotationChanged																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raise the RotationChanged event whenever coordinates have changed.
		/// </summary>
		/// <param name="e">
		/// Float point event arguments.
		/// </param>
		protected virtual void OnRotationChanged(FloatPointEventArgs e)
		{
			RotationChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new Instance of the FRotation3 Item.
		/// </summary>
		public FRotation3()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the FRotation3 Item.
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
		public FRotation3(float x, float y, float z)
		{
			mX = x;
			mY = y;
			mZ = z;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the FRotation3 Item.
		/// </summary>
		/// <param name="source">
		/// Reference to an instance of an FRotation3 to use for reference values.
		/// </param>
		public FRotation3(FRotation3 source)
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
		/// Create a new Instance of the FRotation3 Item.
		/// </summary>
		/// <param name="source">
		/// Reference to an instance of an FVector3 to use for reference values.
		/// </param>
		public FRotation3(FVector3 source)
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
		//*	_Implicit FRotation3 = FVector3																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cast the FVector3 instance to a FRotation3.
		/// </summary>
		/// <param name="value">
		/// Reference to the 3D vector to convert.
		/// </param>
		/// <returns>
		/// Reference to the newly created 3D copy of the caller's 3D vector, if
		/// valid. Otherwise, an empty rotation.
		/// </returns>
		public static implicit operator FRotation3(FVector3 value)
		{
			FRotation3 result = null;

			if(value != null)
			{
				result = new FRotation3()
				{
					mX = value.X,
					mY = value.Y,
					mZ = value.Z
				};
			}
			if(result == null)
			{
				result = new FRotation3();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Implicit FVector3 = FRotation3																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cast the FRotation3 instance to a FVector3.
		/// </summary>
		/// <param name="value">
		/// Reference to the 3D rotation to convert.
		/// </param>
		/// <returns>
		/// Reference to the newly created 3D copy of the caller's 3D rotation, if
		/// valid. Otherwise, an empty vector.
		/// </returns>
		public static implicit operator FVector3(FRotation3 value)
		{
			FVector3 result = null;

			if(value != null)
			{
				result = new FVector3()
				{
					X = value.mX,
					Y = value.mY,
					Z = value.mZ
				};
			}
			if(result == null)
			{
				result = new FVector3();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* _Operator -																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of one rotation subtracted from another.
		/// </summary>
		/// <param name="rotationA">
		/// Reference to the minuend rotation.
		/// </param>
		/// <param name="rotationB">
		/// Reference to the subtrahend rotation.
		/// </param>
		/// <returns>
		/// Result of the subtraction.
		/// </returns>
		public static FRotation3 operator -(FRotation3 rotationA,
			FRotation3 rotationB)
		{
			FRotation3 result = new FRotation3();

			if(rotationA != null)
			{
				result.mX = rotationA.mX;
				result.mY = rotationA.mY;
				result.mZ = rotationA.mZ;
			}
			if(rotationB != null)
			{
				result.mX -= rotationB.mX;
				result.mY -= rotationB.mY;
				result.mZ -= rotationB.mZ;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* _Operator *																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of a rotation multiplied by a scalar.
		/// </summary>
		/// <param name="scalar">
		/// The scalar value to multiply.
		/// </param>
		/// <param name="rotation">
		/// The rotation to multiply.
		/// </param>
		/// <returns>
		/// Result of the multiplication.
		/// </returns>
		public static FRotation3 operator *(float scalar, FRotation3 rotation)
		{
			FRotation3 result = new FRotation3();

			if(rotation != null && scalar != 0f)
			{
				result.mX = scalar * rotation.mX;
				result.mY = scalar * rotation.mY;
				result.mZ = scalar * rotation.mZ;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the result of a rotation multiplied by a scalar.
		/// </summary>
		/// <param name="rotation">
		/// The rotation to multiply.
		/// </param>
		/// <param name="scalar">
		/// The scalar value to multiply.
		/// </param>
		/// <returns>
		/// Result of the multiplication.
		/// </returns>
		public static FRotation3 operator *(FRotation3 rotation, float scalar)
		{
			FRotation3 result = new FRotation3();

			if(rotation != null && scalar != 0f)
			{
				result.mX = scalar * rotation.mX;
				result.mY = scalar * rotation.mY;
				result.mZ = scalar * rotation.mZ;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Operator /																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of one rotation divided by the other.
		/// </summary>
		/// <param name="divisor">
		/// A reference to the divisor.
		/// </param>
		/// <param name="dividend">
		/// A reference to the dividend.
		/// </param>
		/// <returns>
		/// Reference to the rotation division result.
		/// </returns>
		public static FRotation3 operator /(FRotation3 divisor,
			FRotation3 dividend)
		{
			FRotation3 result = new FRotation3();

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
		/// Return the result of the values of two rotations added together.
		/// </summary>
		/// <param name="rotationA">
		/// First rotation to add.
		/// </param>
		/// <param name="rotationB">
		/// Second rotation to add.
		/// </param>
		/// <returns>
		/// Result of the addition.
		/// </returns>
		public static FRotation3 operator +(FRotation3 rotationA,
			FRotation3 rotationB)
		{
			FRotation3 result = new FRotation3();

			if(rotationA != null)
			{
				result.mX = rotationA.mX;
				result.mY = rotationA.mY;
				result.mZ = rotationA.mZ;
			}
			if(rotationB != null)
			{
				result.mX += rotationB.mX;
				result.mY += rotationB.mY;
				result.mZ += rotationB.mZ;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Operator !=																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether values of two rotations are not
		/// equal.
		/// </summary>
		/// <param name="rotationA">
		/// Reference to the first rotation to compare.
		/// </param>
		/// <param name="rotationB">
		/// Reference to the second rotation to compare.
		/// </param>
		/// <returns>
		/// True if the two objects are substantially not equal in value.
		/// Otherwise, false.
		/// </returns>
		[DebuggerStepThrough]
		public static bool operator !=(FRotation3 rotationA, FRotation3 rotationB)
		{
			bool result = true;

			if((object)rotationA != null && (object)rotationB != null)
			{
				result = !(rotationA == rotationB);
			}
			else if((object)rotationA == null && (object)rotationB == null)
			{
				result = false;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Operator FRotation3 == FRotation3																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether values of two rotations are equal.
		/// </summary>
		/// <param name="rotationA">
		/// Reference to the first rotation to compare.
		/// </param>
		/// <param name="rotationB">
		/// Reference to the second rotation to compare.
		/// </param>
		/// <returns>
		/// True if the two objects are substantially equal in value. Otherwise,
		/// false.
		/// </returns>
		[DebuggerStepThrough]
		public static bool operator ==(FRotation3 rotationA, FRotation3 rotationB)
		{
			bool result = true;

			if((object)rotationA != null && (object)rotationB != null)
			{
				result = (
					(rotationA.mX == rotationB.mX) &&
					(rotationA.mY == rotationB.mY) &&
					(rotationA.mZ == rotationB.mZ));
			}
			else if((object)rotationA != null || (object)rotationB != null)
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
		/// Set the base values of the target rotation from the source.
		/// </summary>
		/// <param name="source">
		/// Reference to the source rotation.
		/// </param>
		/// <param name="target">
		/// Reference to the target rotation.
		/// </param>
		public static void Assign(FRotation3 source, FRotation3 target)
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
		/// Set the value of the rotation.
		/// </summary>
		/// <param name="rotation">
		/// Reference to the rotation to be populated.
		/// </param>
		/// <param name="value">
		/// Value to assign.
		/// </param>
		public static void Assign(FRotation3 rotation, float value)
		{
			if(rotation != null)
			{
				rotation.mX =
					rotation.mY =
					rotation.mZ = value;
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Set the value of the rotation.
		/// </summary>
		/// <param name="rotation">
		/// Reference to the rotation to be populated.
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
		public static void Assign(FRotation3 rotation, float x, float y, float z)
		{
			if(rotation != null)
			{
				rotation.mX = x;
				rotation.mY = y;
				rotation.mZ = z;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a memberwise clone of the provided rotation.
		/// </summary>
		/// <param name="rotation">
		/// Reference to the source rotation to be cloned.
		/// </param>
		/// <returns>
		/// Reference to a new FRotation3 instance where the primitive member
		/// values are the same as those in the source, if a legitimate source was
		/// provided. Otherwise, an empty FRotation3.
		/// </returns>
		public static FRotation3 Clone(FRotation3 rotation)
		{
			FRotation3 result = new FRotation3();

			if(rotation != null)
			{
				result.mReadOnly = rotation.mReadOnly;
				result.mX = rotation.mX;
				result.mY = rotation.mY;
				result.mZ = rotation.mZ;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a memberwise clone of the provided rotation.
		/// </summary>
		/// <param name="vector">
		/// Reference to the source vector to be cloned.
		/// </param>
		/// <returns>
		/// Reference to a new FRotation3 instance where the primitive member
		/// values are the same as those in the source, if a legitimate source was
		/// provided. Otherwise, an empty FRotation3.
		/// </returns>
		public static FRotation3 Clone(FVector3 vector)
		{
			FRotation3 result = new FRotation3();

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

		//	TODO: ClosestRotation(FRotation3 checkRotation, List<FArea3> areas)

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
		//* Delta																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the coordinate difference between two rotations.
		/// </summary>
		/// <param name="rotationA">
		/// Reference to the first rotation to compare.
		/// </param>
		/// <param name="rotationB">
		/// Reference to the second rotation to compare.
		/// </param>
		/// <returns>
		/// Coordinate difference between two rotations.
		/// </returns>
		public static FRotation3 Delta(FRotation3 rotationA, FRotation3 rotationB)
		{
			FRotation3 result = new FRotation3();

			if(rotationA != null && rotationB != null)
			{
				result.mX = rotationB.mX - rotationA.mX;
				result.mY = rotationB.mY - rotationA.mY;
				result.mZ = rotationB.mZ - rotationA.mZ;
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

			if(obj is FRotation3 @rotation)
			{
				if(rotation.mX == mX && rotation.mY == mY && rotation.mZ == mZ)
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
		/// Invert the values of the caller's rotation.
		/// </summary>
		/// <param name="rotation">
		/// Reference to the rotation to be inverted.
		/// </param>
		/// <returns>
		/// Reference to the caller's rotation with inverted values.
		/// </returns>
		public static FRotation3 Invert(FRotation3 rotation)
		{
			FRotation3 result = new FRotation3();

			if(rotation != null)
			{
				if(rotation.mX != 0f)
				{
					result.mX = 1f / rotation.mX;
				}
				if(rotation.mY != 0f)
				{
					result.mY = 1f / rotation.mY;
				}
				if(rotation.mZ != 0f)
				{
					result.mZ = 1f / rotation.mZ;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MiddleRotation																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the middle coordinate between two rotations.
		/// </summary>
		/// <param name="rotationA">
		/// Reference to the first rotation to test.
		/// </param>
		/// <param name="rotationB">
		/// Reference to the second rotation to test.
		/// </param>
		/// <returns>
		/// Reference to a rotation that represents the exact middle theta of the
		/// caller's total rotation.
		/// </returns>
		public static FRotation3 MiddleRotation(FRotation3 rotationA,
			FRotation3 rotationB)
		{
			FRotation3 result = new FRotation3();

			if(rotationA != null && rotationB != null)
			{
				result.X = (rotationA.X + rotationB.X) / 2f;
				result.Y = (rotationA.Y + rotationB.Y) / 2f;
				result.Z = (rotationA.Z + rotationB.Z) / 2f;
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
		/// <param name="rotation">
		/// Reference to the rotation to be negated.
		/// </param>
		/// <returns>
		/// Reference to the caller's rotation with inverted values.
		/// </returns>
		public static FRotation3 Negate(FRotation3 rotation)
		{
			FRotation3 result = new FRotation3();

			if (rotation != null)
			{
				result.mX = 0f - rotation.mX;
				result.mY = 0f - rotation.mY;
				result.mZ = 0f - rotation.mZ;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Offset																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a new instance the caller's rotation, translated by the
		/// specified offset.
		/// </summary>
		/// <param name="rotation">
		/// Reference to the rotation to be offset.
		/// </param>
		/// <param name="dx">
		/// X distance from the original rotation.
		/// </param>
		/// <param name="dy">
		/// Y distance from the original rotation.
		/// </param>
		/// <param name="dz">
		/// Z distance from the original rotation.
		/// </param>
		/// <returns>
		/// Reference to a new rotation at the specified offset from the original.
		/// </returns>
		public static FRotation3 Offset(FRotation3 rotation, float dx, float dy,
			float dz)
		{
			FRotation3 result = null;

			if(rotation != null)
			{
				result = new FRotation3(rotation.mX + dx, rotation.mY + dy,
					rotation.mZ + dz);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Parse																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Parse a coordinate string and return its FRotation3 representation.
		/// </summary>
		/// <param name="coordinate">
		/// The coordinate string to parse.
		/// </param>
		/// <param name="allowNull">
		/// Value indicating whether to allow a null return value if the input
		/// string was invalid.
		/// </param>
		/// <returns>
		/// Newly created FRotation3 value representing the caller's input, if
		/// that input was legal or allowNull was false. Otherwise, a null value.
		/// </returns>
		public static FRotation3 Parse(string coordinate,
			bool allowNull = false)
		{
			bool bX = false;
			bool bY = false;
			bool bZ = false;
			int index = 0;
			MatchCollection matches = null;
			FRotation3 result = null;
			string text = "";

			if(coordinate?.Length > 0)
			{
				text = coordinate.Trim();
				if(text.StartsWith("{") && text.EndsWith("}"))
				{
					//	JSON object.
					try
					{
						result = JsonConvert.DeserializeObject<FRotation3>(coordinate);
					}
					catch { }
				}
				else
				{
					//	Freehand.
					matches = Regex.Matches(coordinate, ResourceMain.rxCoordinate);
					if(matches.Count > 0)
					{
						result = new FRotation3();
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
				result = new FRotation3();
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
		//*	RotationChanged																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a rotation has changed.
		/// </summary>
		public event FloatPointEventHandler RotationChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Scale																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Scale the caller's rotation by a uniform factor.
		/// </summary>
		/// <param name="rotation">
		/// The rotation to be scaled.
		/// </param>
		/// <param name="scale">
		/// The factor by which the rotation will be scaled.
		/// </param>
		/// <returns>
		/// Reference to the uniformly scaled rotation.
		/// </returns>
		public static FRotation3 Scale(FRotation3 rotation, float scale)
		{
			FRotation3 result = new FRotation3();

			if(rotation != null)
			{
				result.mX = rotation.mX * scale;
				result.mY = rotation.mY * scale;
				result.mZ = rotation.mZ * scale;
			}
			return result;
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
						OnRotationChanged(
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
						OnRotationChanged(
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
						OnRotationChanged(
							new FloatPointEventArgs("Z", value, original));
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
