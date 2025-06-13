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
using System.Text;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FMatrix4																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Single floating point 4x4 affine matrix.
	/// </summary>
	/// <remarks>
	/// This matrix is uniform by default when instantiated.
	/// </remarks>
	public class FMatrix4
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
		/// Create a new instance of the FMatrix4 item.
		/// </summary>
		public FMatrix4()
		{
			mValues = new float[,]
			{
				{ 1f, 0f, 0f, 0f },
				{ 0f, 1f, 0f, 0f },
				{ 0f, 0f, 1f, 0f },
				{ 0f, 0f, 0f, 1f }
			};
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FMatrix4 item.
		/// </summary>
		/// <param name="matrix">
		/// Reference to a set of values to load, arranged as four rows of four.
		/// </param>
		public FMatrix4(float[,] matrix) : this()
		{
			int x = 0;
			int y = 0;

			if(matrix != null &&
				matrix.GetLength(0) == 4 && matrix.GetLength(1) == 4)
			{
				for(y = 0; y < 4; y++)
				{
					//	Row.
					for(x = 0; x < 4; x++)
					{
						//	Column.
						mValues[y, x] = matrix[y, x];
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Indexer																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value from the specified row and column of the matrix.
		/// </summary>
		public float this[int row, int col]
		{
			get
			{
				return this[row, col];
			}
		}
		//*-----------------------------------------------------------------------*


		//*-----------------------------------------------------------------------*
		//* * operator overload																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Multiply two matrices and return the result.
		/// </summary>
		/// <param name="a">
		/// Reference to matrix A.
		/// </param>
		/// <param name="b">
		/// Reference to matrix B.
		/// </param>
		/// <returns>
		/// A reference to the result of the operation.
		/// </returns>
		public static FMatrix4 operator *(FMatrix4 a, FMatrix4 b)
		{
			return FMatrix4.Multiply(a, b);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ColumnToPoint																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a reference to a 3D point representing the values in the
		/// specified column of the provided matrix.
		/// </summary>
		/// <param name="matrix">
		/// Reference to the matrix containing the values to copy.
		/// </param>
		/// <param name="columnIndex">
		/// 0-based ordinal index of the column to copy.
		/// </param>
		/// <returns>
		/// Reference to a new single precision floating-point 3D point containing
		/// values from the specified column, if the matrix and column were
		/// legitimate. Otherwise, an empty FPoint3.
		/// </returns>
		public static FPoint3 ColumnToPoint(FMatrix4 matrix, int columnIndex)
		{
			FPoint3 result = new FPoint3();

			if(matrix != null &&
				matrix.mValues.GetLength(0) > 3 &&
				matrix.mValues.GetLength(1) > 3 &&
				columnIndex > -1 && columnIndex < 4)
			{
				result.X = matrix.mValues[0, columnIndex];
				result.Y = matrix.mValues[1, columnIndex];
				result.Z = matrix.mValues[2, columnIndex];
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetCofactors																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the array of cofactors from the provided matrix.
		/// </summary>
		/// <param name="matrix">
		/// Reference to the matrix to inspect.
		/// </param>
		/// <returns>
		/// Reference to an array of cofactors from the caller's array, if
		/// found. Otherwise, an empty matrix.
		/// </returns>
		public static FMatrix4 GetCofactors(FMatrix4 matrix)
		{
			int col = 0;
			float determinant = 0f;
			float[,] minor = null;
			int row = 0;

			FMatrix4 result = new FMatrix4();

			if(matrix != null)
			{
				for(row = 0; row < 4; row ++)
				{
					for(col = 0; col < 4; col ++)
					{
						// Compute the minor matrix for the current element.
						minor = GetMinorMatrix(matrix, row, col);
						// Calculate determinant of the minor.
						determinant = FMatrix3.GetDeterminant(minor);
						// Apply alternating signs (+/-).
						result.mValues[row, col] =
							determinant * ((row + col) % 2 == 0 ? 1 : -1);
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetDeterminant																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the determinant of the provided matrix.
		/// </summary>
		/// <param name="matrix">
		/// Reference to the matrix from which the determinant will be calculated.
		/// </param>
		/// <returns>
		/// The the unique solution of the linear equations of the matrix, if
		/// available. Otherwise, zero.
		/// </returns>
		/// <remarks>
		/// The method checks for the value of <see cref="SkipDeterminantPrecheck">
		/// SkipDeterminantPrecheck</see> to determine whether or not to perform
		/// low-cost analytical pre-tests that result in zero prior to running
		/// the full determinant calculation.
		/// </remarks>
		public static float GetDeterminant(FMatrix4 matrix)
		{
			bool bContinue = true;
			int col = 0;
			int col2 = 0;
			float[,] m = null;
			float result = 0f;
			int row = 0;
			int row2 = 0;

			if(matrix != null)
			{
				m = matrix.mValues;
				if(!mSkipDeterminantPrecheck)
				{
					//	Zero row check.
					for(row = 0; row < 4; row ++)
					{
						if(
							m[row, 0] == 0f &&
							m[row, 1] == 0f &&
							m[row, 2] == 0f &&
							m[row, 3] == 0f)
						{
							bContinue = false;
							break;
						}
					}
					if(bContinue)
					{
						//	Zero column check.
						for(col = 0; col < 4; col++)
						{
							if(
								m[0, col] == 0f &&
								m[1, col] == 0f &&
								m[2, col] == 0f &&
								m[3, col] == 0f)
							{
								bContinue = false;
								break;
							}
						}
					}
					if(bContinue)
					{
						//	Identical row check.
						for(row = 0; row < 3; row ++)
						{
							for(row2 = row + 1; row2 < 4; row2 ++)
							{
								if(
									m[row, 0] == m[row2, 0] &&
									m[row, 1] == m[row2, 1] &&
									m[row, 2] == m[row2, 2] &&
									m[row, 3] == m[row2, 3])
								{
									bContinue = false;
									break;
								}
							}
						}
					}
					if(bContinue)
					{
						//	Identical column check.
						for(col = 0; col < 3; col++)
						{
							for(col2 = col + 1; col2 < 4; col2++)
							{
								if(
									m[0, col] == m[col2, 0] &&
									m[1, col] == m[1, col2] &&
									m[2, col] == m[2, col2] &&
									m[3, col] == m[3, col2])
								{
									bContinue = false;
									break;
								}
							}
						}
					}
				}
				if(bContinue)
				{
					result =
						m[0, 0] *
							FMatrix3.GetDeterminant(
								m[1, 1], m[1, 2], m[1, 3],
								m[2, 1], m[2, 2], m[2, 3],
								m[3, 1], m[3, 2], m[3, 3]) -
						m[0, 1] *
							FMatrix3.GetDeterminant(
								m[1, 0], m[1, 2], m[1, 3],
								m[2, 0], m[2, 2], m[2, 3],
								m[3, 0], m[3, 2], m[3, 3]) +
						m[0, 2] *
							FMatrix3.GetDeterminant(
								m[1, 0], m[1, 1], m[1, 3],
								m[2, 0], m[2, 1], m[2, 3],
								m[3, 0], m[3, 1], m[3, 3]) -
						m[0, 3] *
							FMatrix3.GetDeterminant(
								m[1, 0], m[1, 1], m[1, 2],
								m[2, 0], m[2, 1], m[2, 2],
								m[3, 0], m[3, 1], m[3, 2]);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetMinorMatrix																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the minor matrix array of the supplied matrix.
		/// </summary>
		/// <param name="matrix">
		/// Reference to the matrix whose minor subset will be returned.
		/// </param>
		/// <param name="excludeRow">
		/// Ordinal index of the row to exclude.
		/// </param>
		/// <param name="excludeCol">
		/// Ordinal index of the column to exclude.
		/// </param>
		/// <returns>
		/// Reference to a two-dimensional 3 by 3 array of single precision
		/// floating point numbers representing a subject of the caller's matrix.
		/// </returns>
		public static float[,] GetMinorMatrix(FMatrix4 matrix,
			int excludeRow, int excludeCol)
		{
			int icol = 0;
			int irow = 0;
			int ocol = 0;
			int orow = 0;
			float[,] result = new float[3, 3];

			for(irow = 0; irow < 4; irow ++)
			{
				if(irow != excludeRow)
				{
					for(icol = 0; icol < 4; icol++)
					{
						if(icol != excludeCol)
						{
							result[orow, ocol] = matrix.mValues[irow, icol];
							ocol++;
						}
					}
					orow++;
					ocol = 0;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetRotationMatrix																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a rotation matrix for rotation around the specified axes.
		/// </summary>
		/// <param name="thetaX">
		/// The angle by which to rotate on the X axis.
		/// </param>
		/// <param name="thetaY">
		/// The angle by which to rotate on the Y axis.
		/// </param>
		/// <param name="thetaZ">
		/// The angle by which to rotate on the Z axis.
		/// </param>
		/// <param name="axisOrder">
		/// The order in which rotation is applied. Default = Y, X, Z,
		/// for Yaw, Pitch, and Roll in a Y-up system.
		/// </param>
		/// <returns>
		/// Reference to a rotation matrix, 
		/// </returns>
		/// <remarks>
		/// Blender's rotation order is X, Y, Z.
		/// </remarks>
		public static FMatrix4 GetRotationMatrix(
			float thetaX, float thetaY, float thetaZ,
			AxisOrderEnum axisOrder = AxisOrderEnum.YXZ)
		{
			//	Yaw, Pitch, Roll.
			float cp = (float)Math.Cos((double)thetaX);
			float sp = (float)Math.Sin((double)thetaX);
			float cy = (float)Math.Cos((double)thetaY);
			float sy = (float)Math.Sin((double)thetaY);
			float cr = (float)Math.Cos((double)thetaZ);
			float sr = (float)Math.Sin((double)thetaZ);

			FMatrix4 result = null;

			//	X.
			FMatrix4 xMatrix = new FMatrix4(new float[4, 4]
			{
				{ 1f, 0f, 0f, 0f },
				{ 0f, cp, -sp, 0f },
				{ 0f, sp, cp, 0f },
				{ 0f, 0f, 0f, 1f }
			});

			//	Y.
			FMatrix4 yMatrix = new FMatrix4(new float[4, 4]
			{
				{ cy, 0f, sy, 0f },
				{ 0f, 1f, 0f, 0f },
				{ -sy, 0f, cy, 0f },
				{ 0f, 0f, 0f, 1f }
			});

			//	Z.
			FMatrix4 zMatrix = new FMatrix4(new float[4, 4]
			{
				{ cr, -sr, 0f, 0f },
				{ sr, cr, 0f, 0f },
				{ 0f, 0f, 1f, 0f },
				{ 0f, 0f, 0f, 1f }
			});

			switch(axisOrder)
			{
				case AxisOrderEnum.XYZ:
					result = Multiply(Multiply(zMatrix, yMatrix), xMatrix);
					break;
				case AxisOrderEnum.XZY:
					result = Multiply(Multiply(yMatrix, zMatrix), xMatrix);
					break;
				case AxisOrderEnum.YXZ:
					result = Multiply(Multiply(zMatrix, xMatrix), yMatrix);
					break;
				case AxisOrderEnum.YZX:
					result = Multiply(Multiply(xMatrix, zMatrix), yMatrix);
					break;
				case AxisOrderEnum.ZXY:
					result = Multiply(Multiply(yMatrix, xMatrix), zMatrix);
					break;
				case AxisOrderEnum.ZYX:
					result = Multiply(Multiply(xMatrix, yMatrix), zMatrix);
					break;
			}

			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a rotation matrix for rotation around the specified axes.
		/// </summary>
		/// <param name="theta">
		/// The angles around the X, Y, and Z axes to set.
		/// </param>
		/// <param name="axisOrder">
		/// The order in which rotation is applied. Default = Y, X, Z,
		/// for Yaw, Pitch, and Roll in a Y-up system.
		/// </param>
		/// <returns>
		/// Reference to a rotation matrix, 
		/// </returns>
		public static FMatrix4 GetRotationMatrix(FVector3 theta,
			AxisOrderEnum axisOrder = AxisOrderEnum.YXZ)
		{
			FMatrix4 result = null;

			if(theta != null)
			{
				result = GetRotationMatrix(theta.X, theta.Y, theta.Z, axisOrder);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetRotationMatrixX																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a rotation matrix for rotation around the X axis.
		/// </summary>
		/// <param name="theta">
		/// The angle by which to rotate.
		/// </param>
		/// <returns>
		/// Reference to a rotation matrix, 
		/// </returns>
		public static FMatrix4 GetRotationMatrixX(float theta)
		{
			float ct = (float)Math.Cos((double)theta);
			float st = (float)Math.Sin((double)theta);

			//	X.
			FMatrix4 result = new FMatrix4(new float[4, 4]
			{
				{ 1f, 0f, 0f, 0f },
				{ 0f, ct, -st, 0f },
				{ 0f, st, ct, 0f },
				{ 0f, 0f, 0f, 1f }
			});

			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetRotationMatrixY																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a rotation matrix for rotation around the Y axis.
		/// </summary>
		/// <param name="theta">
		/// The angle by which to rotate.
		/// </param>
		/// <returns>
		/// Reference to a rotation matrix, 
		/// </returns>
		public static FMatrix4 GetRotationMatrixY(float theta)
		{
			float ct = (float)Math.Cos((double)theta);
			float st = (float)Math.Sin((double)theta);

			//	Y.
			FMatrix4 result = new FMatrix4(new float[4, 4]
			{
				{ ct, 0f, st, 0f },
				{ 0f, 1f, 0f, 0f },
				{ -st, 0f, ct, 0f },
				{ 0f, 0f, 0f, 1f }
			});

			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetRotationMatrixYawPitchRoll
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a rotation matrix for rotation around the specified axes.
		/// </summary>
		/// <param name="thetaX">
		/// The angle by which to rotate on the X axis.
		/// </param>
		/// <param name="thetaY">
		/// The angle by which to rotate on the Y axis.
		/// </param>
		/// <param name="thetaZ">
		/// The angle by which to rotate on the Z axis.
		/// </param>
		/// <param name="upAxis">
		/// The optional axis of the system, which aids in determining the
		/// yaw.
		/// </param>
		/// <returns>
		/// Reference to a rotation matrix, 
		/// </returns>
		/// <remarks>
		/// <para>
		/// When the Up axis is Z, yaw is Z, pitch is X, and roll is Y.
		/// </para>
		/// <para>
		/// When the Up axis is Y, yaw is Y, pitch is X, and roll is Z.
		/// </para>
		/// </remarks>
		public static FMatrix4 GetRotationMatrixYawPitchRoll(
			float thetaX, float thetaY, float thetaZ, AxisType upAxis = AxisType.Y)
		{
			//	Yaw, Pitch, Roll.
			float cp = (float)Math.Cos((double)thetaX);
			float sp = (float)Math.Sin((double)thetaX);
			float cy = (float)Math.Cos((double)thetaY);
			float sy = (float)Math.Sin((double)thetaY);
			float cr = (float)Math.Cos((double)thetaZ);
			float sr = (float)Math.Sin((double)thetaZ);

			FMatrix4 result = null;

			//	X.
			FMatrix4 xMatrix = new FMatrix4(new float[4, 4]
			{
				{ 1f, 0f, 0f, 0f },
				{ 0f, cp, -sp, 0f },
				{ 0f, sp, cp, 0f },
				{ 0f, 0f, 0f, 1f }
			});

			//	Y.
			FMatrix4 yMatrix = new FMatrix4(new float[4, 4]
			{
				{ cy, 0f, sy, 0f },
				{ 0f, 1f, 0f, 0f },
				{ -sy, 0f, cy, 0f },
				{ 0f, 0f, 0f, 1f }
			});

			//	Z.
			FMatrix4 zMatrix = new FMatrix4(new float[4, 4]
			{
				{ cr, -sr, 0f, 0f },
				{ sr, cr, 0f, 0f },
				{ 0f, 0f, 1f, 0f },
				{ 0f, 0f, 0f, 1f }
			});

			switch(upAxis)
			{
				case AxisType.Y:
					result = Multiply(Multiply(zMatrix, xMatrix), yMatrix);
					break;
				case AxisType.Z:
				default:
					result = Multiply(Multiply(yMatrix, xMatrix), zMatrix);
					break;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetRotationMatrixZ																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a rotation matrix for rotation around the Z axis.
		/// </summary>
		/// <param name="theta">
		/// The angle by which to rotate.
		/// </param>
		/// <returns>
		/// Reference to a rotation matrix, 
		/// </returns>
		public static FMatrix4 GetRotationMatrixZ(float theta)
		{
			float ct = (float)Math.Cos((double)theta);
			float st = (float)Math.Sin((double)theta);

			//	Z.
			FMatrix4 result = new FMatrix4(new float[4, 4]
			{
				{ ct, -st, 0f, 0f },
				{ st, ct, 0f, 0f },
				{ 0f, 0f, 1f, 0f },
				{ 0f, 0f, 0f, 1f }
			});

			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetScaleMatrix																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a scaling matrix for the caller's values.
		/// </summary>
		/// <param name="scaleX">
		/// The X factor by which to scale.
		/// </param>
		/// <param name="scaleY">
		/// The Y factor by which to scale.
		/// </param>
		/// <param name="scaleZ">
		/// The Z factor by which to scale.
		/// </param>
		/// <returns>
		/// A matrix to be used for scaling.
		/// </returns>
		public static FMatrix4 GetScaleMatrix(float scaleX, float scaleY,
			float scaleZ)
		{
			FMatrix4 result = new FMatrix4();

			result.mValues[0, 0] = scaleX;
			result.mValues[1, 1] = scaleY;
			result.mValues[2, 2] = scaleZ;
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetTranslationMatrix																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a translate matrix based on the specified translation values.
		/// </summary>
		/// <param name="translateX">
		/// The X translation.
		/// </param>
		/// <param name="translateY">
		/// The Y translation.
		/// </param>
		/// <param name="translateZ">
		/// The Z translation.
		/// </param>
		/// <returns>
		/// Reference to a new translation matrix.
		/// </returns>
		public static FMatrix4 GetTranslationMatrix(float translateX,
			float translateY, float translateZ)
		{
			FMatrix4 result = new FMatrix4();

			result.mValues[0, 3] = translateX;
			result.mValues[1, 3] = translateY;
			result.mValues[2, 3] = translateZ;
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a translate matrix based on the specified translation values.
		/// </summary>
		/// <param name="translation">
		/// The translation to apply.
		/// </param>
		/// <returns>
		/// Reference to a new translation matrix.
		/// </returns>
		public static FMatrix4 GetTranslationMatrix(FVector3 translation)
		{
			FMatrix4 result = new FMatrix4();

			if(translation != null)
			{
				result.mValues[0, 3] = translation.X;
				result.mValues[1, 3] = translation.Y;
				result.mValues[2, 3] = translation.Z;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Invert																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Invert the caller's matrix.
		/// </summary>
		/// <param name="source">
		/// Reference to the matrix whose values are to be inverted.
		/// </param>
		/// <returns>
		/// A reference to a new matrix representing the inverted version of the
		/// caller's matrix.
		/// </returns>
		public static FMatrix4 Invert(FMatrix4 source)
		{
			FMatrix4 adjugate = null;
			FMatrix4 cofactors = null;
			int col = 0;
			float determinant = 0f;
			FMatrix4 result = new FMatrix4();
			int row = 0;

			if(source != null)
			{
				determinant = GetDeterminant(source);
				if(Math.Abs(determinant) > float.Epsilon)
				{
					//	Determinant was successful.
					cofactors = GetCofactors(source);
					adjugate = Transpose(cofactors);
					for(row = 0; row < 4; row ++)
					{
						for(col = 0; col < 4; col ++)
						{
							result.mValues[row, col] =
								adjugate.mValues[row, col] / determinant;
						}
					}
				}
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
		public static bool IsEmpty(FMatrix4 matrix)
		{
			bool result = true;
			int x = 0;
			int y = 0;

			if(matrix != null &&
				matrix.mValues.GetLength(0) >= 4 &&
				matrix.mValues.GetLength(1) >= 4)
			{
				for(y = 0; y < 4; y++)
				{
					for(x = 0; x < 4; x++)
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
		/// Multiply two matrices and return the resulting matrix.
		/// </summary>
		/// <param name="matrixA">
		/// Reference to the first matrix.
		/// </param>
		/// <param name="matrixB">
		/// Reference to the second matrix.
		/// </param>
		/// <returns>
		/// Reference to the resulting matrix.
		/// </returns>
		public static FMatrix4 Multiply(FMatrix4 matrixA, FMatrix4 matrixB)
		{
			int column = 0;
			int k = 0;
			FMatrix4 result = new FMatrix4();
			int row = 0;
			float sum = 0f;

			if(matrixA != null && matrixB != null)
			{
				for(row = 0; row < 4; row ++)
				{
					for(column = 0; column < 4; column ++)
					{
						sum = 0f;
						for(k = 0; k < 4; k ++)
						{
							sum += matrixA.mValues[row, k] * matrixB.mValues[k, column];
						}
						result.mValues[row, column] = sum;
					}
				}
			}

			return result;
		}
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
		public static FVector4 Multiply(FMatrix4 matrix, FVector4 vector)
		{
			int col = 0;
			float[] inputValues = null;
			float[] outputValues = null;
			int row = 0;
			FVector4 result = new FVector4();
			float value = 0;   //	Output value.

			if(matrix != null && vector != null)
			{
				inputValues = FVector4.GetArray(vector);
				outputValues = new float[4];
				for(row = 0; row < 4; row++)
				{
					value = 0;
					for(col = 0; col < 4; col++)
					{
						value += inputValues[col] * matrix.Values[row, col];
					}
					outputValues[row] = value;
				}
				FVector4.SetArray(result, outputValues);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RowToPoint																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a reference to a 3D point representing the values in the
		/// specified row of the provided matrix.
		/// </summary>
		/// <param name="matrix">
		/// Reference to the matrix containing the values to copy.
		/// </param>
		/// <param name="rowIndex">
		/// 0-based ordinal index of the row to copy.
		/// </param>
		/// <returns>
		/// Reference to a new single precision floating-point 3D point containing
		/// values from the specified column, if the matrix and column were
		/// legitimate. Otherwise, an empty FPoint3.
		/// </returns>
		public static FPoint3 RowToPoint(FMatrix4 matrix, int rowIndex)
		{
			FPoint3 result = new FPoint3();

			if(matrix != null &&
				matrix.mValues.GetLength(0) > 3 &&
				matrix.mValues.GetLength(1) > 3 &&
				rowIndex > -1 && rowIndex < 4)
			{
				result.X = matrix.mValues[rowIndex, 0];
				result.Y = matrix.mValues[rowIndex, 1];
				result.Z = matrix.mValues[rowIndex, 2];
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Scale																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Scale a point by the specified translation.
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
		public static FPoint Scale(FPoint point, FPoint scale)
		{
			return (FPoint)FMatrix3.Scale((FVector2)point, (FVector2)scale);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Scale a point by the specified translation.
		/// </summary>
		/// <param name="point">
		/// Reference point.
		/// </param>
		/// <param name="scale">
		/// Scale for the X, Y, Z axes.
		/// </param>
		/// <returns>
		/// Scaled point.
		/// </returns>
		public static FPoint3 Scale(FPoint3 point, FVector3 scale)
		{
			return (FPoint3)Scale((FVector3)point, scale);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Scale a vector by the specified translation.
		/// </summary>
		/// <param name="point">
		/// Reference point.
		/// </param>
		/// <param name="scale">
		/// Scale for the X, Y, Z axes.
		/// </param>
		/// <returns>
		/// Scaled vector.
		/// </returns>
		public static FVector4 Scale(FVector3 point, FVector3 scale)
		{
			FMatrix4 matrix = new FMatrix4();
			FVector4 result = new FVector4();

			if(point != null && scale != null)
			{
				matrix.Values[0, 0] = scale.X;
				matrix.Values[1, 1] = scale.Y;
				matrix.Values[2, 2] = scale.Z;
				result = Multiply(matrix, point);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SetIdentity																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the content to identity.
		/// </summary>
		public void SetIdentity()
		{
			int column = 0;
			int row = 0;

			for(row = 0; row < 4; row ++)
			{
				for(column = 0; column < 4; column ++)
				{
					mValues[row, column] = ((row == column) ? 1f : 0f);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SkipDeterminantPrecheck																								*
		//*-----------------------------------------------------------------------*
		private static bool mSkipDeterminantPrecheck = true;
		/// <summary>
		/// Get/Set a value indicating whether pre-checks will be skipped when
		/// calculating the determinant of a matrix.
		/// </summary>
		/// <remarks>
		/// The value defaults to true. Prechecks are most useful for improving
		/// performance when your matrices are going to be using a lot of redundant
		/// and repeated values.
		/// </remarks>
		public static bool SkipDeterminantPrecheck
		{
			get { return mSkipDeterminantPrecheck; }
			set { mSkipDeterminantPrecheck = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ToString																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the string representation of this instance.
		/// </summary>
		/// <returns>
		/// The string representation of this object.
		/// </returns>
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			int col = 0;
			int row = 0;

			for(row = 0; row < 4; row ++)
			{
				if(row > 0)
				{
					builder.AppendLine(",");
				}
				builder.Append("{ ");
				for(col = 0; col < 4; col ++)
				{
					if(col > 0)
					{
						builder.Append(", ");
					}
					builder.Append($"{mValues[row, col]:0.000}");
				}
				builder.Append(" }");
			}
			return builder.ToString();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Translate																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Translate a point by the specified translation.
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
		public static FPoint3 Translate(FPoint3 point, FVector3 translation)
		{
			return (FPoint3)Translate((FVector3)point, translation);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
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
		public static FVector4 Translate(FVector3 point, FVector3 translation)
		{
			FMatrix4 matrix = new FMatrix4();
			FVector4 result = new FVector4();

			if(point != null)
			{
				matrix.Values[0, 3] = translation.X;
				matrix.Values[1, 3] = translation.Y;
				matrix.Values[2, 3] = translation.Z;
				result = Multiply(matrix, point);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Transpose																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a version of the caller's matrix where the rows and columns
		/// have been swapped.
		/// </summary>
		/// <param name="matrix">
		/// Reference to the matrix to be transposed.
		/// </param>
		/// <returns>
		/// Reference to a new matrix representing the swapped rows and columns
		/// of the caller's object.
		/// </returns>
		public static FMatrix4 Transpose(FMatrix4 matrix)
		{
			int col = 0;
			int row = 0;
			FMatrix4 result = new FMatrix4();

			if(matrix != null)
			{
				for(row = 0; row < 4; row ++)
				{
					for(col = 0; col < 4; col ++)
					{
						result.mValues[col, row] = matrix.mValues[row, col];
					}
				}
			}
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
