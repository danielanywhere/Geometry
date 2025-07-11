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
	//*	FloatPointEventArgs																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event arguments for events where a floating point coordinate values are
	/// the main focus.
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
		/// <param name="axisName">
		/// Name of the axis for which the event will be fired.
		/// </param>
		/// <param name="newValue">
		/// Current location.
		/// </param>
		/// <param name="originalValue">
		/// Optional original value.
		/// </param>
		public FloatPointEventArgs(string axisName, float newValue,
			float originalValue = 0f)
		{
			AxisValueTrackerItem value = new AxisValueTrackerItem()
			{
				AxisName = (axisName?.Length > 0 ? axisName : ""),
				NewValue = newValue,
				OriginalValue = originalValue
			};
			mValues.Add(value);
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
		/// Original X.
		/// </param>
		/// <param name="originalY">
		/// Original Y.
		/// </param>
		public FloatPointEventArgs(float newX, float newY,
			float originalX, float originalY)
		{
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "X",
				NewValue = newX,
				OriginalValue = originalX
			});
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "Y",
				NewValue = newY,
				OriginalValue = originalY
			});
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
		/// Original X.
		/// </param>
		/// <param name="originalY">
		/// Original Y.
		/// </param>
		/// <param name="originalZ">
		/// Original Z.
		/// </param>
		public FloatPointEventArgs(float newX, float newY, float newZ,
			float originalX, float originalY, float originalZ)
		{
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "X",
				NewValue = newX,
				OriginalValue = originalX
			});
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "Y",
				NewValue = newY,
				OriginalValue = originalY
			});
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "Z",
				NewValue = newZ,
				OriginalValue = originalZ
			});
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
		/// <param name="newW">
		/// Current W coordinate value.
		/// </param>
		/// <param name="originalX">
		/// Original X.
		/// </param>
		/// <param name="originalY">
		/// Original Y.
		/// </param>
		/// <param name="originalZ">
		/// Original Z.
		/// </param>
		/// <param name="originalW">
		/// Original W.
		/// </param>
		public FloatPointEventArgs(float newX, float newY, float newZ, float newW,
			float originalX, float originalY, float originalZ, float originalW)
		{
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "X",
				NewValue = newX,
				OriginalValue = originalX
			});
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "Y",
				NewValue = newY,
				OriginalValue = originalY
			});
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "Z",
				NewValue = newZ,
				OriginalValue = originalZ
			});
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "W",
				NewValue = newW,
				OriginalValue = originalW
			});
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FloatPointEventArgs Item.
		/// </summary>
		/// <param name="newValue">
		/// The new value to record.
		/// </param>
		/// <param name="originalValue">
		/// The original value to record.
		/// </param>
		public FloatPointEventArgs(FPoint newValue, FPoint originalValue)
		{
			float nX = 0f;
			float nY = 0f;
			float oX = 0f;
			float oY = 0f;

			if(newValue != null)
			{
				nX = newValue.X;
				nY = newValue.Y;
			}
			if(originalValue != null)
			{
				oX = originalValue.X;
				oY = originalValue.Y;
			}
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "X",
				NewValue = nX,
				OriginalValue = oX
			});
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "Y",
				NewValue = nY,
				OriginalValue = oY
			});
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FloatPointEventArgs Item.
		/// </summary>
		/// <param name="newValue">
		/// The new value to record.
		/// </param>
		/// <param name="originalValue">
		/// The original value to record.
		/// </param>
		public FloatPointEventArgs(FVector2 newValue, FVector2 originalValue)
		{
			float nX = 0f;
			float nY = 0f;
			float oX = 0f;
			float oY = 0f;

			if(newValue != null)
			{
				nX = newValue.X;
				nY = newValue.Y;
			}
			if(originalValue != null)
			{
				oX = originalValue.X;
				oY = originalValue.Y;
			}
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "X",
				NewValue = nX,
				OriginalValue = oX
			});
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "Y",
				NewValue = nY,
				OriginalValue = oY
			});
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FloatPointEventArgs Item.
		/// </summary>
		/// <param name="newValue">
		/// The new value to record.
		/// </param>
		/// <param name="originalValue">
		/// The original value to record.
		/// </param>
		public FloatPointEventArgs(FVector3 newValue, FVector3 originalValue)
		{
			float nX = 0f;
			float nY = 0f;
			float nZ = 0f;
			float oX = 0f;
			float oY = 0f;
			float oZ = 0f;

			if(newValue != null)
			{
				nX = newValue.X;
				nY = newValue.Y;
				nZ = newValue.Z;
			}
			if(originalValue != null)
			{
				oX = originalValue.X;
				oY = originalValue.Y;
				oZ = originalValue.Z;
			}
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "X",
				NewValue = nX,
				OriginalValue = oX
			});
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "Y",
				NewValue = nY,
				OriginalValue = oY
			});
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "Z",
				NewValue = nZ,
				OriginalValue = oZ
			});
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FloatPointEventArgs Item.
		/// </summary>
		/// <param name="newValue">
		/// The new value to record.
		/// </param>
		/// <param name="originalValue">
		/// The original value to record.
		/// </param>
		public FloatPointEventArgs(FVector4 newValue, FVector4 originalValue)
		{
			float nW = 0f;
			float nX = 0f;
			float nY = 0f;
			float nZ = 0f;
			float oW = 0f;
			float oX = 0f;
			float oY = 0f;
			float oZ = 0f;

			if(newValue != null)
			{
				nX = newValue.X;
				nY = newValue.Y;
				nZ = newValue.Z;
				nW = newValue.W;
			}
			if(originalValue != null)
			{
				oX = originalValue.X;
				oY = originalValue.Y;
				oZ = originalValue.Z;
				oW = originalValue.W;
			}
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "X",
				NewValue = nX,
				OriginalValue = oX
			});
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "Y",
				NewValue = nY,
				OriginalValue = oY
			});
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "Z",
				NewValue = nZ,
				OriginalValue = oZ
			});
			mValues.Add(new AxisValueTrackerItem()
			{
				AxisName = "W",
				NewValue = nW,
				OriginalValue = oW
			});
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Values																																*
		//*-----------------------------------------------------------------------*
		private AxisValueTrackerCollection mValues =
			new AxisValueTrackerCollection();
		/// <summary>
		/// Get a reference to the collection of axis values being tracked in this
		/// event.
		/// </summary>
		public AxisValueTrackerCollection Values
		{
			get { return mValues; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* FloatPointEventHandler																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event handlers for events where a floating point coordinate value
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
