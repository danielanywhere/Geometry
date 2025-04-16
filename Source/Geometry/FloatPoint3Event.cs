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

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FloatPoint3EventArgs																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event arguments for events where a floating point location is the main
	/// focus.
	/// </summary>
	public class FloatPoint3EventArgs : EventArgs
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
		/// Create a new instance of the FloatPointEventArgs Item.
		/// </summary>
		public FloatPoint3EventArgs()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FloatPointEventArgs Item.
		/// </summary>
		/// <param name="newValue">
		/// Current location.
		/// </param>
		/// <param name="originalValue">
		/// Optional original value.
		/// </param>
		public FloatPoint3EventArgs(FPoint3 newValue, FPoint3 originalValue = null)
		{
			if(newValue != null)
			{
				mNewValue = newValue;
			}
			if(originalValue != null)
			{
				mOriginalValue = originalValue;
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FloatPointEventArgs Item.
		/// </summary>
		/// <param name="newX">
		/// Current X coordinate value.
		/// </param>
		/// <param name="newY">
		/// Current Y coordinate value.
		/// </param>
		/// <param name="newZ">
		/// Current Z coordinate value.
		/// </param>
		/// <param name="originalX">
		/// Optional original X.
		/// </param>
		/// <param name="originalY">
		/// Optional original Y.
		/// </param>
		/// <param name="originalZ">
		/// Optional original Z.
		/// </param>
		public FloatPoint3EventArgs(float newX, float newY, float newZ,
			float originalX = 0f, float originalY = 0f, float originalZ = 0f)
		{
			mNewValue.X = newX;
			mNewValue.Y = newY;
			mNewValue.Z = newZ;
			mOriginalValue.X = originalX;
			mOriginalValue.Y = originalY;
			mOriginalValue.Z = originalZ;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NewValue																															*
		//*-----------------------------------------------------------------------*
		private FPoint3 mNewValue = new FPoint3();
		/// <summary>
		/// Get/Set the new value.
		/// </summary>
		public FPoint3 NewValue
		{
			get { return mNewValue; }
			set { mNewValue = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	OriginalValue																													*
		//*-----------------------------------------------------------------------*
		private FPoint3 mOriginalValue = new FPoint3();
		/// <summary>
		/// Get/Set the original value before the event.
		/// </summary>
		public FPoint3 OriginalValue
		{
			get { return mOriginalValue; }
			set { mOriginalValue = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* FloatPoint3EventHandler																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event handlers for events where a floating point 3-dimensional location
	/// is the main focus.
	/// </summary>
	/// <param name="sender">
	/// The object raising this value.
	/// </param>
	/// <param name="e">
	/// Float point event arguments.
	/// </param>
	public delegate void FloatPoint3EventHandler(object sender,
		FloatPoint3EventArgs e);
	//*-------------------------------------------------------------------------*

}
