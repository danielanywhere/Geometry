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
	//*	FloatEventArgs																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event arguments for events using a floating point value.
	/// </summary>
	public class FloatEventArgs : EventArgs
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
		/// Create a new instance of the FloatEventArgs Item.
		/// </summary>
		public FloatEventArgs()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FloatEventArgs Item.
		/// </summary>
		/// <param name="newValue">
		/// Current value.
		/// </param>
		/// <param name="originalValue">
		/// Optional original value.
		/// </param>
		public FloatEventArgs(float newValue, float originalValue = 0f)
		{
			mNewValue = newValue;
			mOriginalValue = originalValue;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NewValue																															*
		//*-----------------------------------------------------------------------*
		private float mNewValue = 0f;
		/// <summary>
		/// Get/Set the new value.
		/// </summary>
		public float NewValue
		{
			get { return mNewValue; }
			set { mNewValue = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	OriginalValue																													*
		//*-----------------------------------------------------------------------*
		private float mOriginalValue = 0f;
		/// <summary>
		/// Get/Set the original value before the event.
		/// </summary>
		public float OriginalValue
		{
			get { return mOriginalValue; }
			set { mOriginalValue = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* FloatEventHandler																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Handler for events where a floating point value is the central focus.
	/// </summary>
	/// <param name="sender">
	/// The object raising this event.
	/// </param>
	/// <param name="e">
	/// Float event arguments.
	/// </param>
	public delegate void FloatEventHandler(object sender, FloatEventArgs e);
	//*-------------------------------------------------------------------------*

}
