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
		/// Reference to an array of coordaintes where the line crosses the edge
		/// of the ellipse, if intersections were found. Otherwise, an empty array.
		/// </returns>
		public static FPoint[] FindIntersections(FPoint center, float radiusX,
			float radiusY, FLine line)
		{
			double A = 0d;
			double a = 0d;
			double B = 0d;
			double b = 0d;
			double C = 0d;
			double D = 0d;
			double dx = 0d;
			double dy = 0d;
			double h = 0d;
			double ix = 0d;
			double iy = 0d;
			double k = 0d;
			FPoint point = null;
			List<FPoint> result = new List<FPoint>();
			double sqrtD = 0d;
			double t = 0d;
			double t1 = 0d;
			double t2 = 0d;
			double x1 = 0d;
			double x2 = 0d;
			double y1 = 0d;
			double y2 = 0d;

			if(center != null && radiusX != 0f && radiusY != 0f &&
				!FLine.IsEmpty(line))
			{
				h = center.X;
				k = center.Y;
				a = radiusX;
				b = radiusY;

				x1 = line.PointA.X;
				y1 = line.PointA.Y;
				x2 = line.PointB.X;
				y2 = line.PointB.Y;

				dx = x2 - x1;
				dy = y2 - y1;

				// Quadratic coefficients
				A = (dx * dx) / (a * a) + (dy * dy) / (b * b);
				B = 2 * ((x1 - h) * dx / (a * a) + (y1 - k) * dy / (b * b));
				C = ((x1 - h) * (x1 - h)) / (a * a) +
					((y1 - k) * (y1 - k)) / (b * b) - 1d;

				// Compute the discriminant
				D = B * B - 4 * A * C;

				if(D == 0d)
				{
					// One intersection (tangent)
					t = -B / (2 * A);
					ix = x1 + t * dx;
					iy = y1 + t * dy;

					result.Add(new FPoint((float)ix, (float)iy));
				}
				else if(D > 0d)
				{
					// Two intersections through ellipse, one of which may be
					// imaginary.
					sqrtD = Math.Sqrt(D);

					t1 = (-B + sqrtD) / (2 * A);
					t2 = (-B - sqrtD) / (2 * A);

					point = new FPoint((float)(x1 + t1 * dx), (float)(y1 + t1 * dy));
					if(FLine.IsPointOnLine(line, point))
					{
						result.Add(point);
					}
					point = new FPoint((float)(x1 + t2 * dx), (float)(y1 + t2 * dy));
					if(FLine.IsPointOnLine(line, point))
					{
						result.Add(point);
					}
				}
			}
			return result.ToArray();
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
