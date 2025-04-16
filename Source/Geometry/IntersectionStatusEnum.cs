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
	//*	IntersectionStatusEnum																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// The status of a line or shape intersection.
	/// </summary>
	public enum IntersectionStatusEnum
	{
		/// <summary>
		/// No intersection found or undefined.
		/// </summary>
		None = 0,
		/// <summary>
		/// Intersection was successful.
		/// </summary>
		OK,
		/// <summary>
		/// The provided shape was invalid.
		/// </summary>
		ShapeInvalid,
		/// <summary>
		/// The ray is in the plane.
		/// </summary>
		RayParallelInsidePlane,
		/// <summary>
		/// The ray is disjointed from the plane.
		/// </summary>
		RayParallelDisconnected,
		/// <summary>
		/// The ray points away from the plane.
		/// </summary>
		RayPointsAway,
		/// <summary>
		///	The ray is outside the face sides.
		/// </summary>
		RayOutsideFaceOnS,
		/// <summary>
		/// The ray is outside the face top.
		/// </summary>
		RayOutsideFaceOnT
	}
	//*-------------------------------------------------------------------------*
}
