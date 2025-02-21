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
using System.Net.Http.Headers;
using System.Text;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	Ellipse																																	*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Tools and functionality for working with ellipses.
	/// </summary>
	public class Ellipse
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
		//* FindIntersections																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return an array of coordinates containing zero or more intersections
		/// between the edge of the ellipse and a provided line.
		/// </summary>
		/// <param name="center">
		/// Reference to the center coordinate of the ellipse.
		/// </param>
		/// <param name="radiusX">
		/// The X-axis radius of the ellipse.
		/// </param>
		/// <param name="radiusY">
		/// The Y-axis radius of the ellipse.
		/// </param>
		/// <param name="line">
		/// Reference to the line being tested for intersection.
		/// </param>
		/// <returns>
		/// Reference to an array of coordinates where the line crosses the edge
		/// of the ellipse, if intersections were found. Otherwise, an empty array.
		/// </returns>
		[Obsolete("Please call FEllipse.FindIntersections instead.")]
		public static FPoint[] FindIntersections(FPoint center, float radiusX,
			float radiusY, FLine line)
		{
			return FEllipse.FindIntersections(center, radiusX, radiusY, line, false);
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
