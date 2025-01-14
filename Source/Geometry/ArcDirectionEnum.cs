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
	//*	ArcDirectionEnum																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of allowable arc drawing directions.
	/// </summary>
	/// <remarks>
	/// The arc direction is the flow of the edge of the arc from the specified
	/// starting point or angle to the ending point or angle. In Cartesian space,
	/// ArcDirectionEnum.Forward is a counterclockwise progression from the
	/// positive-going ray along the X-axis, while in Display space, the value
	/// ArcDirectionEnum.Forward represents a clockwise progression from the
	/// positive-going rad along the X-axis.
	/// </remarks>
	public enum ArcDirectionEnum
	{
		/// <summary>
		/// Arc direction not specified or unknown.
		/// </summary>
		None = 0,
		/// <summary>
		/// The arc progresses in an increasing angular path from start to finish.
		/// </summary>
		Forward = 1,
		/// <summary>
		/// The arc progresses in an increasing angular path from start to finish.
		/// This value is compatible with Forward.
		/// </summary>
		Increasing = 1,
		/// <summary>
		/// The arc progresses in a decreasing angular path from start to finish.
		/// </summary>
		Reverse = 2,
		/// <summary>
		/// The arc progresses is a decreasing angular path from start to finish.
		/// This value is compatible with Reverse.
		/// </summary>
		Decreasing = 2
	}
	//*-------------------------------------------------------------------------*
}
