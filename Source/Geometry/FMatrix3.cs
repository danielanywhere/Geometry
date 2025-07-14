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

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FMatrix3																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Single floating point 3x3 affine matrix.
	/// </summary>
	/// <remarks>
	/// This matrix is uniform by default when instantiated.
	/// </remarks>
	public class FMatrix3
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
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the FMatrix3 item.
		/// </summary>
		public FMatrix3()
		{
			mValues = new float[,]
			{
				{ 1f, 0f, 0f },
				{ 0f, 1f, 0f },
				{ 0f, 0f, 1f }
			};
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FMatrix3 item.
		/// </summary>
		/// <param name="matrix">
		/// Reference to a set of values to load, arranged as three rows of three.
		/// </param>
		public FMatrix3(float[,] matrix) : this()
		{
			int x = 0;
			int y = 0;

			if(matrix != null &&
				matrix.GetLength(0) == 3 && matrix.GetLength(1) == 3)
			{
				for(y = 0; y < 3; y++)
				{
					//	Row.
					for(x = 0; x < 3; x++)
					{
						//	Column.
						mValues[y, x] = matrix[y, x];
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetDeterminant																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the 3x3 determinant of the caller's value set, using no
		/// pre-checks.
		/// </summary>
		/// <param name="a1">
		/// Row 1 column 1 value.
		/// </param>
		/// <param name="a2">
		/// Row 1 column 2 value.
		/// </param>
		/// <param name="a3">
		/// Row 1 column 3 value.
		/// </param>
		/// <param name="b1">
		/// Row 2 column 1 value.
		/// </param>
		/// <param name="b2">
		/// Row 2 column 2 value.
		/// </param>
		/// <param name="b3">
		/// Row 2 column 3 value.
		/// </param>
		/// <param name="c1">
		/// Row 3 column 1 value.
		/// </param>
		/// <param name="c2">
		/// Row 3 column 2 vlaue.
		/// </param>
		/// <param name="c3">
		/// Row 3 column 3 value.
		/// </param>
		/// <returns>
		/// The determinant of the 3 x 3 set of values.
		/// </returns>
		public static float GetDeterminant(
			float a1, float a2, float a3,
			float b1, float b2, float b3,
			float c1, float c2, float c3)
		{
			float result =
				a1 * (b2 * c3 - b3 * c2) -
				a2 * (b1 * c3 - b3 * c1) +
				a3 * (b1 * c2 - b2 * c1);
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the 3x3 determinant of the caller's value set, using no
		/// pre-checks.
		/// </summary>
		/// <param name="values">
		/// Reference to a two dimensional array of values representing the content
		/// of the matrix.
		/// </param>
		/// <returns>
		/// The determinant of the 3 x 3 set of values.
		/// </returns>
		public static float GetDeterminant(float[,] values)
		{
			float result = 0f;

			if(values?.Length > 0 &&
				values.GetLength(0) == 3 && values.GetLength(1) == 3)
			{
				result =
					values[0, 0] *
						(values[1, 1] * values[2, 2] - values[1, 2] * values[2, 1]) -
					values[0, 1] *
						(values[1, 0] * values[2, 2] - values[1, 2] * values[2, 0]) +
					values[0, 2] *
						(values[1, 0] * values[2, 1] - values[1, 1] * values[2, 0]);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsEmpty																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified matrix is empty.
		/// </summary>
		/// <param name="matrix">
		/// Reference to the object to inspect.
		/// </param>
		/// <returns>
		/// True if the specified matrix is empty. Otherwise, false.
		/// </returns>
		public static bool IsEmpty(FMatrix3 matrix)
		{
			bool result = true;
			int x = 0;
			int y = 0;

			if(matrix != null &&
				matrix.mValues.GetLength(0) >= 3 &&
				matrix.mValues.GetLength(1) >= 3)
			{
				for(y = 0; y < 3; y++)
				{
					for(x = 0; x < 3; x++)
					{
						if(matrix.mValues[x, y] != 0f)
						{
							result = false;
							break;
						}
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Multiply																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Multiply a vector by a transformation and return the result.
		/// </summary>
		/// <param name="matrix">
		/// Reference to the matrix by which to be multiplied.
		/// </param>
		/// <param name="vector">
		/// Reference to the vector to multiply.
		/// </param>
		/// <returns>
		/// Result of vector multiplied by matrix.
		/// </returns>
		public static FVector3 Multiply(FMatrix3 matrix, FVector3 vector)
		{
			int col = 0;
			float[] inputValues = null;
			float[] outputValues = null;
			int row = 0;
			FVector3 result = new FVector3();
			float value = 0;   //	Output value.

			if(matrix != null && vector != null)
			{
				inputValues = FVector3.GetArray(vector);
				outputValues = new float[3];
				for(row = 0; row < 3; row++)
				{
					value = 0;
					for(col = 0; col < 3; col++)
					{
						value += inputValues[col] * matrix.Values[row, col];
					}
					outputValues[row] = value;
				}
				FVector3.SetArray(result, outputValues);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	Rotate																																*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Rotate the 3D point by a specified angle, in radians.
		///// </summary>
		///// <param name="point">
		///// Reference to the point to be rotated.
		///// </param>
		///// <param name="thetaX">
		///// The angle around the X axis by which to rotate the point, in radians.
		///// </param>
		///// <param name="thetaY">
		///// The angle around the Y axis by which to rotate the point, in radians.
		///// </param>
		///// <param name="thetaZ">
		///// The angle around the Z axis by which to rotate the point, in radians.
		///// </param>
		///// <param name="upAxis">
		///// The optional 'up' direction axis used to help determine which axis is
		///// yaw.
		///// </param>
		///// <returns>
		///// The rotated 3D point, relative to 0,0,0.
		///// </returns>
		///// <remarks>
		///// <para>
		///// The order of rotations is Yaw, Pitch, Roll, with the UpDirection
		///// helping to distinguish which axis is Yaw.
		///// </para>
		///// <para>
		///// Yaw, pitch and roll are descriptions from the perspective of the pilot.
		///// <list type="bullet">
		///// <item>Yaw is left/right turning rotation with a constant
		///// horizon.</item>
		///// <item>Pitch is forward/back rotation.</item>
		///// <item>Roll is wingtip left/right rotation.</item>
		///// </list>
		///// </para>
		///// </remarks>
		//public static FVector3 Rotate(FVector3 point,
		//	float thetaX, float thetaY, float thetaZ,
		//	AxisType upAxis = AxisType.Z)
		//{
		//	FVector3 result = null;

		//	if(point != null)
		//	{
		//		result = FVector3.Clone(point);
		//		switch(upAxis)
		//		{
		//			case AxisType.X:
		//				if(thetaZ != 0f)
		//				{
		//					result = RotateZ(result, thetaZ);
		//				}
		//				if(thetaY != 0f)
		//				{
		//					result = RotateY(result, thetaY);
		//				}
		//				if(thetaX != 0f)
		//				{
		//					result = RotateX(result, thetaX);
		//				}
		//				break;
		//			case AxisType.Y:
		//				if(thetaX != 0f)
		//				{
		//					result = RotateX(result, thetaX);
		//				}
		//				if(thetaZ != 0f)
		//				{
		//					result = RotateZ(result, thetaZ);
		//				}
		//				if(thetaY != 0f)
		//				{
		//					result = RotateY(result, thetaY);
		//				}
		//				break;
		//			case AxisType.Z:
		//			default:
		//				if(thetaX != 0f)
		//				{
		//					result = RotateX(result, thetaX);
		//				}
		//				if(thetaY != 0f)
		//				{
		//					result = RotateY(result, thetaY);
		//				}
		//				if(thetaZ != 0f)
		//				{
		//					result = RotateZ(result, thetaZ);
		//				}
		//				break;
		//		}
		//	}
		//	return result;
		//}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		///// <summary>
		///// Rotate the 3D point by a specified angle, in radians.
		///// </summary>
		///// <param name="point">
		///// Reference to the point to be rotated.
		///// </param>
		///// <param name="theta">
		///// The angles around the X, Y, and Z axis by which to rotate the point, in
		///// radians.
		///// </param>
		///// <param name="upAxis">
		///// The optional 'up' direction axis used to help determine which axis is
		///// yaw.
		///// </param>
		///// <returns>
		///// The rotated 3D point, relative to 0,0,0.
		///// </returns>
		///// <remarks>
		///// <para>
		///// The order of rotations is Yaw, Pitch, Roll, with the UpDirection
		///// helping to distinguish which axis is Yaw.
		///// </para>
		///// <para>
		///// Yaw, pitch and roll are descriptions from the perspective of the pilot.
		///// <list type="bullet">
		///// <item>Yaw is left/right turning rotation with a constant
		///// horizon.</item>
		///// <item>Pitch is forward/back rotation.</item>
		///// <item>Roll is wingtip left/right rotation.</item>
		///// </list>
		///// </para>
		///// </remarks>
		//public static FVector3 Rotate(FVector3 point,
		//	FVector3 theta, AxisType upAxis = AxisType.Z)
		//{
		//	FVector3 result = null;

		//	if(point != null && theta != null)
		//	{
		//		result = Rotate(point, theta.X, theta.Y, theta.Z, upAxis);
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Rotate																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Rotate the 3D point by a specified angle, in radians.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be rotated.
		/// </param>
		/// <param name="thetaX">
		/// The angle around the X axis by which to rotate the vector, in radians.
		/// </param>
		/// <param name="thetaY">
		/// The angle around the Y axis by which to rotate the vector, in radians.
		/// </param>
		/// <param name="thetaZ">
		/// The angle around the Z axis by which to rotate the vector, in radians.
		/// </param>
		/// <param name="upAxis">
		/// The optional 'up' direction axis used to help determine which axis is
		/// yaw.
		/// </param>
		/// <returns>
		/// The rotated 3D vector, relative to 0,0,0.
		/// </returns>
		/// <remarks>
		/// <para>
		/// The order of rotations is Yaw, Pitch, Roll, with the UpDirection
		/// helping to distinguish which axis is Yaw.
		/// </para>
		/// <para>
		/// Yaw, pitch and roll are descriptions from the perspective of the pilot.
		/// <list type="bullet">
		/// <item>Yaw is left/right turning rotation with a constant
		/// horizon.</item>
		/// <item>Pitch is forward/back rotation.</item>
		/// <item>Roll is wingtip left/right rotation.</item>
		/// </list>
		/// </para>
		/// </remarks>
		public static FVector3 Rotate(FVector3 vector,
			float thetaX, float thetaY, float thetaZ,
			AxisType upAxis = AxisType.Z)
		{
			FVector3 result = null;

			if(vector != null)
			{
				result = FVector3.Clone(vector);
				switch(upAxis)
				{
					case AxisType.X:
						if(thetaZ != 0f)
						{
							result = RotateZ(result, thetaZ);
						}
						if(thetaY != 0f)
						{
							result = RotateY(result, thetaY);
						}
						if(thetaX != 0f)
						{
							result = RotateX(result, thetaX);
						}
						break;
					case AxisType.Y:
						if(thetaX != 0f)
						{
							result = RotateX(result, thetaX);
						}
						if(thetaZ != 0f)
						{
							result = RotateZ(result, thetaZ);
						}
						if(thetaY != 0f)
						{
							result = RotateY(result, thetaY);
						}
						break;
					case AxisType.Z:
					default:
						if(thetaX != 0f)
						{
							result = RotateX(result, thetaX);
						}
						if(thetaY != 0f)
						{
							result = RotateY(result, thetaY);
						}
						if(thetaZ != 0f)
						{
							result = RotateZ(result, thetaZ);
						}
						break;
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Rotate the 3D point by a specified angle, in radians.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be rotated.
		/// </param>
		/// <param name="theta">
		/// The angles around the X, Y, and Z axis by which to rotate the vector,
		/// in radians.
		/// </param>
		/// <param name="upAxis">
		/// The optional 'up' direction axis used to help determine which axis is
		/// yaw.
		/// </param>
		/// <returns>
		/// The rotated 3D vector, relative to 0,0,0.
		/// </returns>
		/// <remarks>
		/// <para>
		/// The order of rotations is Yaw, Pitch, Roll, with the UpDirection
		/// helping to distinguish which axis is Yaw.
		/// </para>
		/// <para>
		/// Yaw, pitch and roll are descriptions from the perspective of the pilot.
		/// <list type="bullet">
		/// <item>Yaw is left/right turning rotation with a constant
		/// horizon.</item>
		/// <item>Pitch is forward/back rotation.</item>
		/// <item>Roll is wingtip left/right rotation.</item>
		/// </list>
		/// </para>
		/// </remarks>
		public static FVector3 Rotate(FVector3 vector,
			FVector3 theta, AxisType upAxis = AxisType.Z)
		{
			FVector3 result = null;

			if(vector != null && theta != null)
			{
				result = Rotate(vector, theta.X, theta.Y, theta.Z, upAxis);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RotateX																																*
		//*-----------------------------------------------------------------------*
		///// <summary>
		///// Rotate the point by the specified angle and return the new value to
		///// the caller.
		///// </summary>
		///// <param name="point">
		///// Point containing the source value to rotate.
		///// </param>
		///// <param name="theta">
		///// The angle of rotation to apply to the axis, in radians.
		///// </param>
		///// <returns>
		///// New point containing the rotated coordinates.
		///// </returns>
		//public static FVector3 RotateX(FVector3 point, float theta)
		//{
		//	return new FVector3(RotateX((FVector3)point, theta));
		//}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Rotate the vector by the specified angle and return the new value to
		/// the caller.
		/// </summary>
		/// <param name="vector">
		/// Vector containing the source value to rotate.
		/// </param>
		/// <param name="theta">
		/// The angle of rotation to apply to the axis, in radians.
		/// </param>
		/// <returns>
		/// New vector containing the rotated coordinates.
		/// </returns>
		public static FVector3 RotateX(FVector3 vector, float theta)
		{
			float asi = 0f;
			float cos = 0f;
			FMatrix3 matrix = null;
			FVector3 result = new FVector3(vector);
			float sin = 0f;

			if(vector != null && theta != 0f)
			{
				cos = (float)Math.Cos((double)theta);
				sin = (float)Math.Sin((double)theta);
				asi = 0f - sin;
				matrix = new FMatrix3(new float[,]
				{
					{ 1f, 0f,  0f },
					{ 0f, cos, asi },
					{ 0f, sin, cos }
				});
				result = FMatrix3.Multiply(matrix, vector);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RotateY																																*
		//*-----------------------------------------------------------------------*
		///// <summary>
		///// Rotate the point by the specified angle and return the new value to
		///// the caller.
		///// </summary>
		///// <param name="point">
		///// Point containing the source value to rotate.
		///// </param>
		///// <param name="theta">
		///// The angle of rotation to apply to the axis, in radians.
		///// </param>
		///// <returns>
		///// New vector containing the rotated coordinates.
		///// </returns>
		//public static FVector3 RotateY(FVector3 point, float theta)
		//{
		//	return new FVector3(RotateY((FVector3)point, theta));
		//}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Rotate the vector by the specified angle and return the new value to
		/// the caller.
		/// </summary>
		/// <param name="vector">
		/// Vector containing the source value to rotate.
		/// </param>
		/// <param name="theta">
		/// The angle of rotation to apply to the axis, in radians.
		/// </param>
		/// <returns>
		/// New vector containing the rotated coordinates.
		/// </returns>
		public static FVector3 RotateY(FVector3 vector, float theta)
		{
			float asi = 0f;
			float cos = 0f;
			FMatrix3 matrix = null;
			FVector3 result = new FVector3(vector);
			float sin = 0f;

			if(vector != null && theta != 0f)
			{
				cos = (float)Math.Cos((double)theta);
				sin = (float)Math.Sin((double)theta);
				asi = 0f - sin;
				matrix = new FMatrix3(new float[,]
				{
					{ cos, 0f, sin },
					{ 0f,  1f, 0f  },
					{ asi, 0f, cos }
				});
				result = FMatrix3.Multiply(matrix, vector);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RotateZ																																*
		//*-----------------------------------------------------------------------*
		///// <summary>
		///// Rotate the opint by the specified angle and return the new value to
		///// the caller.
		///// </summary>
		///// <param name="point">
		///// Point containing the source value to rotate.
		///// </param>
		///// <param name="theta">
		///// The angle of rotation to apply to the axis, in radians.
		///// </param>
		///// <returns>
		///// New vector containing the rotated coordinates.
		///// </returns>
		//public static FVector3 RotateZ(FVector3 point, float theta)
		//{
		//	return new FVector3(RotateZ((FVector3)point, theta));
		//}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Rotate the vector by the specified angle and return the new value to
		/// the caller.
		/// </summary>
		/// <param name="vector">
		/// Vector containing the source value to rotate.
		/// </param>
		/// <param name="theta">
		/// The angle of rotation to apply to the axis, in radians.
		/// </param>
		/// <returns>
		/// New vector containing the rotated coordinates.
		/// </returns>
		public static FVector3 RotateZ(FVector3 vector, float theta)
		{
			float asi = 0f;
			float cos = 0f;
			FMatrix3 matrix = null;
			FVector3 result = new FVector3(vector);
			float sin = 0f;

			if(vector != null && theta != 0f)
			{
				cos = (float)Math.Cos((double)theta);
				sin = (float)Math.Sin((double)theta);
				asi = 0f - sin;
				matrix = new FMatrix3(new float[,]
				{
					{ cos, asi, 0f },
					{ sin, cos, 0f },
					{ 0f,  0f,  1f }
				});
				result = FMatrix3.Multiply(matrix, vector);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Scale																																	*
		//*-----------------------------------------------------------------------*
		///// <summary>
		///// Scale a point by the specified translation.
		///// </summary>
		///// <param name="point">
		///// Reference point.
		///// </param>
		///// <param name="scale">
		///// Scale for the X and Y axes.
		///// </param>
		///// <returns>
		///// Scaled point.
		///// </returns>
		//public static FVector2 Scale(FVector2 point, FVector2 scale)
		//{
		//	return new FVector2(Scale((FVector2)point, (FVector2)scale));
		//}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Scale a vector by the specified translation.
		/// </summary>
		/// <param name="point">
		/// Reference point.
		/// </param>
		/// <param name="scale">
		/// Scale for the X and Y axes.
		/// </param>
		/// <returns>
		/// Scaled point.
		/// </returns>
		public static FVector2 Scale(FVector2 point, FVector2 scale)
		{
			FMatrix3 matrix = new FMatrix3();
			FVector3 result = new FVector3();
			FVector3 source = null;

			if(point != null && scale != null)
			{
				matrix.Values[0, 0] = scale.X;
				matrix.Values[1, 1] = scale.Y;
				source = new FVector3();
				source.X = point.X;
				source.Y = point.Y;
				source.Z = 1f;
				result = Multiply(matrix, source);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Translate																															*
		//*-----------------------------------------------------------------------*
		///// <summary>
		///// Translate a point by the specified translation.
		///// </summary>
		///// <param name="point">
		///// Reference point.
		///// </param>
		///// <param name="translation">
		///// Distance by which to move the point.
		///// </param>
		///// <returns>
		///// Translated point.
		///// </returns>
		//public static FVector2 Translate(FVector2 point, FVector2 translation)
		//{
		//	return new FVector2(Translate((FVector2)point, (FVector2)translation));
		//}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Translate a vector by the specified translation.
		/// </summary>
		/// <param name="point">
		/// Reference point.
		/// </param>
		/// <param name="translation">
		/// Distance by which to move the point.
		/// </param>
		/// <returns>
		/// Translated point.
		/// </returns>
		public static FVector3 Translate(FVector2 point, FVector2 translation)
		{
			FMatrix3 matrix = new FMatrix3();
			FVector3 result = new FVector3();
			FVector3 source = new FVector3();

			matrix.Values[0, 2] = translation.X;
			matrix.Values[1, 2] = translation.Y;
			source.X = point.X;
			source.Y = point.Y;
			source.Z = 1f;
			result = Multiply(matrix, source);
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Values																																*
		//*-----------------------------------------------------------------------*
		private float[,] mValues = null;
		/// <summary>
		/// Get/Set a reference to the array of values in this matrix.
		/// </summary>
		public float[,] Values
		{
			get { return mValues; }
			set { mValues = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
