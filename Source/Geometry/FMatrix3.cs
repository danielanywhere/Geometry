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

using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		/// Create a new Instance of the FMatrix3 Item.
		/// </summary>
		public FMatrix3()
		{
			mValues = new float[,]
			{
				{ 1, 0, 0 },
				{ 0, 1, 0 },
				{ 0, 0, 1 }
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
			int row = 0;
			FVector3 result = new FVector3();
			float value = 0;   //	Output value.

			for(row = 0; row < 3; row++)
			{
				value = 0;
				for(col = 0; col < 3; col++)
				{
					value += vector.Values[col] * matrix.Values[row, col];
				}
				result.Values[row] = value;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Rotate																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Rotate the 2D point by a specified angle, in radians.
		/// </summary>
		/// <param name="point">
		/// Reference to the point to be rotated.
		/// </param>
		/// <param name="angle">
		/// The angle by which to rotate the point, in radians.
		/// </param>
		/// <returns>
		/// The rotated 2D point, relative to 0,0.
		/// </returns>
		public static FPoint Rotate(FPoint point, float angle)
		{
			return Rotate((FVector2)point, angle);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Rotate the 2D vector by a specified angle, in radians.
		/// </summary>
		/// <param name="point">
		/// Reference to the vector to be rotated.
		/// </param>
		/// <param name="angle">
		/// The angle by which to rotate the point, in radians.
		/// </param>
		/// <returns>
		/// The rotated 2D point, relative to 0,0.
		/// </returns>
		public static FVector3 Rotate(FVector2 point, float angle)
		{
			FMatrix3 matrix = new FMatrix3();
			FVector3 result = new FVector3();
			FVector3 source = new FVector3();

			matrix.Values[0, 0] = (float)Math.Cos(angle);
			matrix.Values[0, 1] = (float)(0d - Math.Sin(angle));
			matrix.Values[1, 0] = (float)Math.Sin(angle);
			matrix.Values[1, 1] = (float)Math.Cos(angle);
			source.Values = new float[] { point.Values[0], point.Values[1], 1f };

			result = Multiply(matrix, source);
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
			return Scale((FVector2)point, (FVector2)scale);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
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
		public static FVector3 Scale(FVector2 point, FVector2 scale)
		{
			FMatrix3 matrix = new FMatrix3();
			FVector3 result = new FVector3();
			FVector3 source = new FVector3();

			matrix.Values[0, 0] = scale.Values[0];
			matrix.Values[1, 1] = scale.Values[1];
			source.Values = new float[] { point.Values[0], point.Values[1], 1f };
			result = Multiply(matrix, source);
			return result;
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
		public static FPoint Translate(FPoint point, FPoint translation)
		{
			return Translate((FVector2)point, (FVector2)translation);
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
		public static FVector3 Translate(FVector2 point, FVector2 translation)
		{
			FMatrix3 matrix = new FMatrix3();
			FVector3 result = new FVector3();
			FVector3 source = new FVector3();

			matrix.Values[0, 2] = translation.Values[0];
			matrix.Values[1, 2] = translation.Values[1];
			source.Values = new float[] { point.Values[0], point.Values[1], 1f };
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
