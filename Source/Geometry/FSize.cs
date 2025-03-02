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
using System.ComponentModel;
using System.Text;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FSize																																		*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Floating-point Size as first-class object.
	/// </summary>
	[TypeConverter(typeof(FAreaTypeConverter))]
	public class FSize
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnSizeChanged																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SizeChanged event when a property of this item has changed.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected virtual void OnSizeChanged(EventArgs e)
		{
			SizeChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the FSize Item.
		/// </summary>
		public FSize()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FSize Item.
		/// </summary>
		/// <param name="width">
		/// Size width.
		/// </param>
		/// <param name="height">
		/// Size height.
		/// </param>
		public FSize(float width, float height)
		{
			mWidth = width;
			mHeight = height;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Implicit FSize = SizeF																								*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast a Size value to an FSize.
		///// </summary>
		///// <param name="value">
		///// The size to convert.
		///// </param>
		///// <remarks>
		///// This operator is not available when compiling without GDI+.
		///// </remarks>
		//public static implicit operator FSize(SizeF value)
		//{
		//	FSize result = new FSize();

		//	if(value.Width != 0 || value.Height != 0)
		//	{
		//		result.mWidth = value.Width;
		//		result.mHeight = value.Height;
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Implicit SizeF = FSize																								*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast an FSize value to a SizeF.
		///// </summary>
		///// <param name="value">
		///// The size to convert.
		///// </param>
		///// <remarks>
		///// This operator is not available when compiling without GDI+.
		///// </remarks>
		//public static implicit operator SizeF(FSize value)
		//{
		//	SizeF result = SizeF.Empty;

		//	if(value.Width != 0 || value.Height != 0)
		//	{
		//		result = new SizeF((int)value.mWidth, (int)value.mHeight);
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Implicit SKSize = FSize																							*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast an FPoint value to SKSize.
		///// </summary>
		///// <param name="value">
		///// The size to convert.
		///// </param>
		//public static implicit operator SKSize(FSize value)
		//{
		//	SKSize rv = new SKSize();

		//	if(value != null)
		//	{
		//		rv.Width = value.mWidth;
		//		rv.Height = value.mHeight;
		//	}
		//	return rv;
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a memberwise clone of the provided size.
		/// </summary>
		/// <param name="source">
		/// Reference to the source size to be cloned.
		/// </param>
		/// <returns>
		/// Reference to a new FSize instance where the primitive member values
		/// are the same as those in the source, if a legitimate source was
		/// provided. Otherwise, an empty FSize.
		/// </returns>
		public static FSize Clone(FSize source)
		{
			FSize result = new FSize();

			if(source != null)
			{
				result.mHeight = source.mHeight;
				result.mWidth = source.mWidth;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsDifferent																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether two sizes are different.
		/// </summary>
		/// <param name="sizeA">
		/// Reference to the first size to be compared.
		/// </param>
		/// <param name="sizeB">
		/// Reference to the second size to be compared.
		/// </param>
		/// <returns>
		/// Value indicating whether the sizes are different.
		/// </returns>
		public static bool IsDifferent(FSize sizeA, FSize sizeB)
		{
			bool result = false;

			if(sizeA != null && sizeB != null)
			{
				result = sizeA.mWidth != sizeB.mWidth ||
					sizeA.mHeight != sizeB.mHeight;
			}
			else if(sizeA != null || sizeB != null)
			{
				result = true;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsEmpty																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified size is empty.
		/// </summary>
		/// <param name="size">
		/// Reference to the object to inspect.
		/// </param>
		/// <returns>
		/// True if the specified size is empty. Otherwise, false.
		/// </returns>
		public static bool IsEmpty(FSize size)
		{
			bool result = true;

			if(size != null)
			{
				result = (size.mHeight == 0f && size.mWidth == 0f);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Height																																*
		//*-----------------------------------------------------------------------*
		private float mHeight = 0f;
		/// <summary>
		/// Get/Set the height of the item.
		/// </summary>
		public float Height
		{
			get { return mHeight; }
			set
			{
				bool changed = (mHeight != value);

				mHeight = value;
				if(changed)
				{
					OnSizeChanged(new EventArgs());
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Scale																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the caller's size, uniformly scaled by the specified factor.
		/// </summary>
		/// <param name="size">
		/// Size to be converted.
		/// </param>
		/// <param name="scale">
		/// Factor by which to scale.
		/// </param>
		/// <returns>
		/// A reference to the newly scaled size.
		/// </returns>
		public static FSize Scale(FSize size, float scale)
		{
			FSize result = new FSize();

			if(size != null && scale != 0f)
			{
				result.mWidth = size.mWidth * scale;
				result.mHeight = size.mHeight * scale;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SizeChanged																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when one of the properties of this item have changed.
		/// </summary>
		public event EventHandler SizeChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ToString																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the string representation of this item.
		/// </summary>
		/// <returns>
		/// String representation of this item's values.
		/// </returns>
		public override string ToString()
		{
			StringBuilder result = new StringBuilder();

			result.Append(string.Format("{0:000}", mWidth));
			result.Append(',');
			result.Append(string.Format("{0:000}", mHeight));
			return result.ToString();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Width																																	*
		//*-----------------------------------------------------------------------*
		private float mWidth = 0;
		/// <summary>
		/// Get/Set the width of the object.
		/// </summary>
		public float Width
		{
			get { return mWidth; }
			set
			{
				bool changed = (mWidth != value);

				mWidth = value;
				if(changed)
				{
					OnSizeChanged(new EventArgs());
				}
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	FSizeTypeConverter																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Type conversion for FSize that allows editing of child properties in a
	/// property grid.
	/// </summary>
	public class FSizeTypeConverter : TypeConverter
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
		//* CanConvertTo																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether this converter can convert to the
		/// specified destination type.
		/// </summary>
		/// <param name="context">
		/// Context to inspect.
		/// </param>
		/// <param name="destinationType">
		/// Destination type to query for.
		/// </param>
		/// <returns>
		/// True if this type converter is able to convert to the specified
		/// type. Otherwise, false.
		/// </returns>
		/// <remarks>
		/// Overridden specifically to support serialization in JSON.NET. If
		/// string type is disallowed, string serialization won't be affected.
		/// </remarks>
		public override bool CanConvertTo(ITypeDescriptorContext context,
			Type destinationType)
		{
			bool result = false;

			if(destinationType != typeof(string))
			{
				result = base.CanConvertTo(context, destinationType);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetProperties																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the collection of property descriptors for the context, object,
		/// and attributes provided.
		/// </summary>
		/// <param name="context">
		/// The context to consider.
		/// </param>
		/// <param name="value">
		/// The live object from which the properties will be retrieved.
		/// </param>
		/// <param name="attributes">
		/// Array of attributes by which to filter the return properties.
		/// </param>
		/// <returns>
		/// Reference to a collection of property descriptors matching the
		/// caller's specification.
		/// </returns>
		/// <remarks>
		/// In this version, none of the incoming parameters are checked.
		/// </remarks>
		public override PropertyDescriptorCollection GetProperties(
			ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(FSize));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetPropertiesSupported																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the GetProperties method is supported
		/// in the specified context.
		/// </summary>
		/// <param name="context">
		/// Context to check for support.
		/// </param>
		/// <returns>
		/// True if the GetProperties method is supported in the specified context.
		/// Otherwise, false.
		/// </returns>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
