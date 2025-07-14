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

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FLine3																																	*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Definition of a 3D line.
	/// </summary>
	public class FLine3
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
		/// Create a new instance of the FLine3 Item.
		/// </summary>
		public FLine3()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FLine3 Item.
		/// </summary>
		/// <param name="pointA">
		/// Reference to the point that will act as the first point in the line.
		/// </param>
		/// <param name="pointB">
		/// Reference to the point that will act as the secont point in the line.
		/// </param>
		/// <param name="color">
		/// Optional color for the line. If provided, that object will act as
		/// the color for the line.
		/// </param>
		public FLine3(FVector3 pointA, FVector3 pointB, FColor4 color = null)
		{
			if(pointA != null)
			{
				mPointA = pointA;
			}
			if(pointB != null)
			{
				mPointB = pointB;
			}
			if(color != null)
			{
				mColor = color;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a memberwise clone of the provided line.
		/// </summary>
		/// <param name="source">
		/// Reference to the source line to be cloned.
		/// </param>
		/// <returns>
		/// Reference to a new FLine3 instance where the primitive member values
		/// are the same as those in the source, if a legitimate source was
		/// provided. Otherwise, an empty FLine3.
		/// </returns>
		public static FLine3 Clone(FLine3 source)
		{
			FLine3 result = new FLine3();

			if(source != null)
			{
				result.mColor = FColor4.Clone(source.mColor);
				result.mPointA = FVector3.Clone(source.mPointA);
				result.mPointB = FVector3.Clone(source.mPointB);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Color																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Color">Color</see>.
		/// </summary>
		private FColor4 mColor = new FColor4(1f, 0f, 0f, 0f);
		/// <summary>
		/// Get/Set a reference to the color of this line.
		/// </summary>
		public FColor4 Color
		{
			get { return mColor; }
			set { mColor = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PointA																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="PointA">PointA</see>.
		/// </summary>
		private FVector3 mPointA = new FVector3();
		/// <summary>
		/// Get/Set a reference to the first point on the line.
		/// </summary>
		public FVector3 PointA
		{
			get { return mPointA; }
			set { mPointA = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PointB																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="PointB">PointB</see>.
		/// </summary>
		private FVector3 mPointB = new FVector3();
		/// <summary>
		/// Get/Set a reference to the second point on the line.
		/// </summary>
		public FVector3 PointB
		{
			get { return mPointB; }
			set { mPointB = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TransferValues																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Transfer the member values of one instance to another.
		/// </summary>
		/// <param name="source">
		/// Reference to the source line whose values will be assigned.
		/// </param>
		/// <param name="target">
		/// Reference to the target line that will receive the values.
		/// </param>
		public static void TransferValues(FLine3 source, FLine3 target)
		{
			if(source != null && target != null)
			{
				target.mColor = FColor4.Clone(source.mColor);
				target.mPointA = FVector3.Clone(source.mPointA);
				target.mPointB = FVector3.Clone(source.mPointB);
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*



}
