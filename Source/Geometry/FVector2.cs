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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FVector2																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Single floating point 2D Vector.
	/// </summary>
	public class FVector2
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
		/// Create a new Instance of the FVector2 Item.
		/// </summary>
		public FVector2()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the FVector2 Item.
		/// </summary>
		/// <param name="x">
		/// X coordinate.
		/// </param>
		/// <param name="y">
		/// Y coordinate.
		/// </param>
		public FVector2(float x, float y)
		{
			mValues[0] = x;
			mValues[1] = y;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Implicit FPoint = FVector2																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cast the FVector2 instance to an FPoint.
		/// </summary>
		/// <param name="value">
		/// Reference to the vector to be converted.
		/// </param>
		/// <returns>
		/// Reference to a newly created FPoint representing the values in
		/// the caller's vector.
		/// </returns>
		public static implicit operator FPoint(FVector2 value)
		{
			FPoint result = new FPoint();

			if(value != null)
			{
				if(value.mValues.Length > 0)
				{
					result.X = value.mValues[0];
				}
				if(value.mValues.Length > 1)
				{
					result.Y = value.mValues[1];
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Implicit FVector2 = FPoint																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cast the FPoint instance to a FVector2.
		/// </summary>
		/// <param name="value">
		/// Reference to the point to be converted.
		/// </param>
		/// <returns>
		/// Reference to the newly created vector representing the values in the
		/// caller's point.
		/// </returns>
		public static implicit operator FVector2(FPoint value)
		{
			FVector2 result = null;

			if(value != null)
			{
				result = new FVector2(value.X, value.Y);
			}
			if(result == null)
			{
				//	Last chance. Safe return.
				result = new FVector2();
			}
			return result;
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
		/// Otherwise, a blank FVector2.
		/// </returns>
		public static FVector2 Clone(FVector2 vector)
		{
			int count = 0;
			int index = 0;
			FVector2 result = new FVector2();

			if(vector != null)
			{
				count = vector.mValues.Length;
				if(result.mValues.Length != count)
				{
					result.mValues = new float[count];
				}
				for(index = 0; index < count; index++)
				{
					result.mValues[index] = vector.mValues[index];
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
		public static bool IsEmpty(FVector2 vector)
		{
			bool result = true;
			int x = 0;

			if(vector != null &&
				vector.mValues.Length >= 2)
			{
				for(x = 0; x < 2; x++)
				{
					if(vector.mValues[x] != 0f)
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
		//*	Values																																*
		//*-----------------------------------------------------------------------*
		private float[] mValues = new float[2];
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


	}
	//*-------------------------------------------------------------------------*
}
