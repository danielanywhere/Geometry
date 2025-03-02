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
	//*	DirectionEnum																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of possible directions.
	/// </summary>
	[Flags]
	public enum DirectionEnum
	{
		/// <summary>
		/// No direction specified, or unknown.
		/// </summary>
		None = 0x00,
		//	Primary directions.
		/// <summary>
		/// Due north.
		/// </summary>
		North = 0x01,
		/// <summary>
		/// Due west.
		/// </summary>
		West = 0x02,
		/// <summary>
		/// Due south.
		/// </summary>
		South = 0x04,
		/// <summary>
		/// Due east.
		/// </summary>
		East = 0x08,
		//	Compound.
		/// <summary>
		/// Northwesterly.
		/// </summary>
		Northwest = 0x03,
		/// <summary>
		/// Southwesterly.
		/// </summary>
		Southwest = 0x06,
		/// <summary>
		/// Northeasterly.
		/// </summary>
		Northeast = 0x09,
		/// <summary>
		/// Southeasterly.
		/// </summary>
		Southeast = 0x0c
	}
	//*-------------------------------------------------------------------------*
}
