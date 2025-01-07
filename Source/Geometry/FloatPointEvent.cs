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
	//*	FloatPointEventArgs																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event arguments for events where a floating point location is the main
	/// focus.
	/// </summary>
	public class FloatPointEventArgs : EventArgs
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
		public FloatPointEventArgs()
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
		public FloatPointEventArgs(FPoint newValue, FPoint originalValue = null)
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
		/// <param name="originalX">
		/// Optional original X.
		/// </param>
		/// <param name="originalY">
		/// Optional original Y.
		/// </param>
		public FloatPointEventArgs(float newX, float newY,
			float originalX = 0f, float originalY = 0f)
		{
			mNewValue.X = newX;
			mNewValue.Y = newY;
			mOriginalValue.X = originalX;
			mOriginalValue.Y = originalY;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NewValue																															*
		//*-----------------------------------------------------------------------*
		private FPoint mNewValue = new FPoint();
		/// <summary>
		/// Get/Set the new value.
		/// </summary>
		public FPoint NewValue
		{
			get { return mNewValue; }
			set { mNewValue = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	OriginalValue																													*
		//*-----------------------------------------------------------------------*
		private FPoint mOriginalValue = new FPoint();
		/// <summary>
		/// Get/Set the original value before the event.
		/// </summary>
		public FPoint OriginalValue
		{
			get { return mOriginalValue; }
			set { mOriginalValue = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* FloatPointEventHandler																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event handlers for events where a floating point 2-dimensional location
	/// is the main focus.
	/// </summary>
	/// <param name="sender">
	/// The object raising this value.
	/// </param>
	/// <param name="e">
	/// Float point event arguments.
	/// </param>
	public delegate void FloatPointEventHandler(object sender,
		FloatPointEventArgs e);
	//*-------------------------------------------------------------------------*

}
