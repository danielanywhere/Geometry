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
using System.Text;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FScale																																	*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Scale factor with floating point values.
	/// </summary>
	public class FScale
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
		////*-----------------------------------------------------------------------*
		////* Delta																																	*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return a uniform scale delta between the old and new values.
		///// </summary>
		///// <param name="oldScale">
		///// The original scale.
		///// </param>
		///// <param name="newScale">
		///// The new scale.
		///// </param>
		///// <returns>
		///// Reference to a uniform scale representing the delta between
		///// original and new scales.
		///// </returns>
		//public static FScale Delta(float oldScale, float newScale)
		//{
		//	FScale result = new FScale();

		//	if(oldScale != newScale && oldScale != 0f)
		//	{
		//		result.mScaleX = result.mScaleY = newScale / oldScale;
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Scalar																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a scalar value representing the specified scale.
		/// </summary>
		/// <param name="scale">
		/// Reference to the scale to convert.
		/// </param>
		/// <returns>
		/// Reference to the scalar representation of the caller's scale.
		/// </returns>
		public static float Scalar(FScale scale)
		{
			float result = 1f;

			if(scale != null)
			{
				try
				{
					result = (float)Math.Sqrt(scale.mScaleX * scale.mScaleY);
				}
				catch { }
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ScaleX																																*
		//*-----------------------------------------------------------------------*
		private float mScaleX = 1f;
		/// <summary>
		/// Get/Set the X scale for this item.
		/// </summary>
		public float ScaleX
		{
			get { return mScaleX; }
			set { mScaleX = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ScaleY																																*
		//*-----------------------------------------------------------------------*
		private float mScaleY = 1f;
		/// <summary>
		/// Get/Set the Y scale for this item.
		/// </summary>
		public float ScaleY
		{
			get { return mScaleY; }
			set { mScaleY = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ToString																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the string representation of this item.
		/// </summary>
		/// <returns>
		/// String representation of the values in this instance.
		/// </returns>
		public override string ToString()
		{
			StringBuilder result = new StringBuilder();

			result.Append(string.Format("{0:000}", mScaleX));
			result.Append(',');
			result.Append(string.Format("{0:000}", mScaleY));
			return result.ToString();
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
