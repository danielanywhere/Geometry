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

using Newtonsoft.Json;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FColor4																																	*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// A four element color definition with single-precision decimal values.
	/// </summary>
	public class FColor4
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
		/// Create a new instance of the FColor4 Item.
		/// </summary>
		public FColor4()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FColor4 Item.
		/// </summary>
		/// <param name="alpha">
		/// Alpha level as a decimal value between 0 and 1.
		/// </param>
		/// <param name="red">
		/// Red level as a decimal value between 0 and 1.
		/// </param>
		/// <param name="green">
		/// Green level as a decimal value between 0 and 1.
		/// </param>
		/// <param name="blue">
		/// Blue level as a decimal value between 0 and 1.
		/// </param>
		public FColor4(float alpha, float red, float green, float blue)
		{
			mAlpha = alpha;
			mRed = red;
			mGreen = green;
			mBlue = blue;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	- operator overload																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of one color subtracted from another.
		/// </summary>
		/// <param name="minuend">
		/// The part to be subtracted from.
		/// </param>
		/// <param name="subtrahend">
		/// The amount to subtract.
		/// </param>
		/// <returns>
		/// Reference to the color subtraction result.
		/// </returns>
		public static FColor4 operator -(FColor4 minuend, FColor4 subtrahend)
		{
			FColor4 result = new FColor4();

			if(minuend != null && subtrahend != null)
			{
				result.mAlpha = minuend.mAlpha - subtrahend.mAlpha;
				result.mBlue = minuend.mBlue - subtrahend.mBlue;
				result.mGreen = minuend.mGreen - subtrahend.mGreen;
				result.mRed = minuend.mRed - subtrahend.mRed;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	* operator overload																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of a color multiplied by a scalar value.
		/// </summary>
		/// <param name="multiplicand">
		/// Reference to the multiplicand.
		/// </param>
		/// <param name="multiplier">
		/// Reference to the multiplier.
		/// </param>
		/// <returns>
		/// Reference to a new color representing the result of the
		/// multiplication.
		/// </returns>
		public static FColor4 operator *(FColor4 multiplicand, float multiplier)
		{
			FColor4 result = new FColor4();

			if(multiplicand != null)
			{
				result.mAlpha = multiplicand.mAlpha * multiplier;
				result.mBlue = multiplicand.mBlue * multiplier;
				result.mGreen = multiplicand.mGreen * multiplier;
				result.mRed = multiplicand.mRed * multiplier;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the result of two colors multiplied by one another.
		/// </summary>
		/// <param name="multiplicand">
		/// Reference to the multiplicand.
		/// </param>
		/// <param name="multiplier">
		/// Reference to the multiplier.
		/// </param>
		/// <returns>
		/// Reference to a new color representing the result of the
		/// multiplication.
		/// </returns>
		public static FColor4 operator *(FColor4 multiplicand,
			FColor4 multiplier)
		{
			FColor4 result = new FColor4();

			if(multiplicand != null && multiplier != null)
			{
				result.mAlpha = multiplicand.mAlpha * multiplier.mAlpha;
				result.mBlue = multiplicand.mBlue * multiplier.mBlue;
				result.mGreen = multiplicand.mGreen * multiplier.mGreen;
				result.mRed = multiplicand.mRed * multiplier.mRed;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	/ operator overload																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of one color divided by the other.
		/// </summary>
		/// <param name="divisor">
		/// A reference to the divisor.
		/// </param>
		/// <param name="dividend">
		/// A reference to the dividend.
		/// </param>
		/// <returns>
		/// Reference to a new color representing the division result.
		/// </returns>
		public static FColor4 operator /(FColor4 divisor, FColor4 dividend)
		{
			FColor4 result = new FColor4();

			if(divisor != null && dividend != null)
			{
				if(dividend.mAlpha != 0f)
				{
					result.mAlpha = divisor.mAlpha / dividend.mAlpha;
				}
				if(dividend.mBlue != 0f)
				{
					result.mBlue = divisor.mBlue / dividend.mBlue;
				}
				if(dividend.mGreen != 0f)
				{
					result.mGreen = divisor.mGreen / dividend.mGreen;
				}
				if(dividend.mRed != 0f)
				{
					result.mRed = divisor.mRed / dividend.mRed;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	+ operator overload																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the result of a color added by scalar value.
		/// </summary>
		/// <param name="addend1">
		/// Reference to the color whose values will be added.
		/// </param>
		/// <param name="addend2">
		/// The scalar value to add.
		/// </param>
		/// <returns>
		/// Reference to a new color representing the result of the addition.
		/// </returns>
		public static FColor4 operator +(FColor4 addend1, float addend2)
		{
			FColor4 result = new FColor4();

			if(addend1 != null)
			{
				result.mAlpha = addend1.mAlpha + addend2;
				result.mBlue = addend1.mBlue + addend2;
				result.mGreen = addend1.mGreen + addend2;
				result.mRed = addend1.mRed + addend2;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the result of the addition of two colors.
		/// </summary>
		/// <param name="addend1">
		/// Reference to the first addend.
		/// </param>
		/// <param name="addend2">
		/// Reference to the second addend.
		/// </param>
		/// <returns>
		/// Reference to the color addition result.
		/// </returns>
		public static FColor4 operator +(FColor4 addend1, FColor4 addend2)
		{
			FColor4 result = new FColor4();

			if(addend1 != null && addend2 != null)
			{
				result.mAlpha = addend1.mAlpha + addend2.mAlpha;
				result.mBlue = addend1.mBlue + addend2.mBlue;
				result.mGreen = addend1.mGreen + addend2.mGreen;
				result.mRed = addend1.mRed + addend2.mRed;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Alpha																																	*
		//*-----------------------------------------------------------------------*
		private float mAlpha = 0f;
		/// <summary>
		/// Get/Set the alpha channel intensity.
		/// </summary>
		[JsonProperty(Order = 0)]
		public float Alpha
		{
			get { return mAlpha; }
			set { mAlpha = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Blue																																	*
		//*-----------------------------------------------------------------------*
		private float mBlue = 0f;
		/// <summary>
		/// Get/Set the intensity of the blue channel.
		/// </summary>
		[JsonProperty(Order = 3)]
		public float Blue
		{
			get { return mBlue; }
			set { mBlue = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a memberwise clone of the provided color.
		/// </summary>
		/// <param name="source">
		/// Reference to the source color to be cloned.
		/// </param>
		/// <returns>
		/// Reference to a new FColor4 instance where the primitive member values
		/// are the same as those in the source, if a legitimate source was
		/// provided. Otherwise, an empty FColor4.
		/// </returns>
		public static FColor4 Clone(FColor4 source)
		{
			FColor4 result = new FColor4();

			if(source != null)
			{
				result.mAlpha = source.mAlpha;
				result.mBlue = source.mBlue;
				result.mGreen = source.mGreen;
				result.mRed = source.mRed;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Green																																	*
		//*-----------------------------------------------------------------------*
		private float mGreen = 0f;
		/// <summary>
		/// Get/Set the intensity of the green channel.
		/// </summary>
		[JsonProperty(Order = 2)]
		public float Green
		{
			get { return mGreen; }
			set { mGreen = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Red																																		*
		//*-----------------------------------------------------------------------*
		private float mRed = 0f;
		/// <summary>
		/// Get/Set the intensity of the red channel.
		/// </summary>
		[JsonProperty(Order = 1)]
		public float Red
		{
			get { return mRed; }
			set { mRed = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TransferValues																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Transfer the member values of one instance to another.
		/// </summary>
		/// <param name="source">
		/// Reference to the source color whose values will be assigned.
		/// </param>
		/// <param name="target">
		/// Reference to the target color that will receive the values.
		/// </param>
		public static void TransferValues(FColor4 source, FColor4 target)
		{
			if(source != null && target != null)
			{
				target.mAlpha = source.mAlpha;
				target.mBlue = source.mBlue;
				target.mGreen = source.mGreen;
				target.mRed = source.mRed;
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
