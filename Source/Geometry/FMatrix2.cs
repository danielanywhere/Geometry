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
	//*	FMatrix2																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Single floating point 2x2 linear matrix.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This matrix is uniform by default when instantiated.
	/// </para>
	/// <para>
	/// This matrix can perform linear and 2D operations only. If you need to
	/// perform translation, consider using the FMatrix3 instead.
	/// </para>
	/// </remarks>
	public class FMatrix2
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
		/// Create a new Instance of the FMatrix2 Item.
		/// </summary>
		public FMatrix2()
		{
			mValues = new float[,]
			{
				{ 1, 0 },
				{ 0, 1 }
			};
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
		public static bool IsEmpty(FMatrix2 matrix)
		{
			bool result = true;
			int x = 0;
			int y = 0;

			if(matrix != null &&
				matrix.mValues.GetLength(0) >= 2 &&
				matrix.mValues.GetLength(1) >= 2)
			{
				for(y = 0; y < 2; y ++)
				{
					for(x = 0; x < 2; x ++)
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
		public static FVector2 Multiply(FMatrix2 matrix, FVector2 vector)
		{
			int col = 0;
			float[] inputValues = null;
			float[] outputValues = null;
			int row = 0;
			FVector2 result = new FVector2();
			float value = 0f;   //	Output value.

			if(matrix != null && vector != null)
			{
				inputValues = FVector2.GetArray(vector);
				outputValues = new float[2];
				for(row = 0; row < 2; row++)
				{
					value = 0;
					for(col = 0; col < 2; col++)
					{
						value += inputValues[col] * matrix.Values[row, col];
					}
					outputValues[row] = value;
				}
				FVector2.SetArray(result, outputValues);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Rotate																																*
		//*-----------------------------------------------------------------------*
		///// <summary>
		///// Rotate the 2D point by a specified angle, in radians.
		///// </summary>
		///// <param name="point">
		///// Reference to the point to be rotated.
		///// </param>
		///// <param name="theta">
		///// The angle by which to rotate the point, in radians.
		///// </param>
		///// <returns>
		///// The rotated point, relative to 0,0.
		///// </returns>
		//public static FPoint Rotate(FPoint point, float theta)
		//{
		//	return Rotate(point, theta);
		//}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Rotate the 2D vector by a specified angle, in radians.
		/// </summary>
		/// <param name="point">
		/// Reference to the vector to be rotated.
		/// </param>
		/// <param name="theta">
		/// The angle by which to rotate the point, in radians.
		/// </param>
		/// <returns>
		/// The rotated point, relative to 0,0.
		/// </returns>
		public static FVector2 Rotate(FVector2 point, float theta)
		{
			FMatrix2 matrix = new FMatrix2();
			FVector2 result = new FVector2();

			matrix.Values[0, 0] = (float)Math.Cos(theta);
			matrix.Values[0, 1] = (float)(0d - Math.Sin(theta));
			matrix.Values[1, 0] = (float)Math.Sin(theta);
			matrix.Values[1, 1] = (float)Math.Cos(theta);

			result = Multiply(matrix, point);
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
