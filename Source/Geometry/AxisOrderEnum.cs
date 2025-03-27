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
	//*	AxisOrderEnum																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of possible axis processing orders.
	/// </summary>
	public enum AxisOrderEnum
	{
		/// <summary>
		/// No order specified or order not defined.
		/// </summary>
		None = 0,
		/// <summary>
		/// X, Y, Z.
		/// </summary>
		XYZ,
		/// <summary>
		/// Y, X, Z
		/// </summary>
		YXZ,
		/// <summary>
		/// Z, X, Y
		/// </summary>
		ZXY,
		/// <summary>
		/// X, Z, Y
		/// </summary>
		XZY,
		/// <summary>
		/// Y, Z, X
		/// </summary>
		YZX,
		/// <summary>
		/// Z, Y, X
		/// </summary>
		ZYX
	}
	//*-------------------------------------------------------------------------*

}
