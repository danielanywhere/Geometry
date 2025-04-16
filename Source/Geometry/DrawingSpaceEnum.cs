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
	//*	DrawingSpaceEnum																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of recognized drawing spaces.
	/// </summary>
	public enum DrawingSpaceEnum
	{
		/// <summary>
		/// No drawing space specified, or unknown.
		/// </summary>
		None = 0,
		/// <summary>
		/// Cartesian space. TR:+X,+Y, TL:-X,+Y, BL:-X,-Y, BR:+X,-Y.
		/// </summary>
		Cartesian,
		/// <summary>
		/// Display space. BR:+X,+Y, BL:-X,+Y, TL:-X,-Y, TR:+X,-Y.
		/// </summary>
		Display
	}
	//*-------------------------------------------------------------------------*

}
