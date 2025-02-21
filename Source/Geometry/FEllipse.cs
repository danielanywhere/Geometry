using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FEllipse																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Definition of an ellipse and its supporting characteristics.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The general form of an ellipse is:
	/// </para>
	/// <para>
	/// ((x - h)<sup>2</sup> / a<sup>2</sup>) +
	/// ((y - k)<sup>2</sup> / b<sup>2</sup>) = 1
	/// </para>
	/// <para>
	/// ... where h and h are the center coordinate, a is the x radius, and b
	/// is the y radius.
	/// </para>
	/// <para>
	/// If center X, Y is -5, 2, the X radius is 3, and the Y radius is 4, the
	/// same equation can be written as:
	/// </para>
	/// <para>
	/// ((x + 5)<sup>2</sup> / 9 +
	/// ((y - 2)<sup>2</sup> / 16
	/// </para>
	/// <para>
	/// The focal points are found using
	/// c<sup>2</sup> = b<sup>2</sup> - a<sup>2</sup>
	/// </para>
	/// <para>
	/// ... meaning c = Sqrt(Pow(RadiusY, 2) - Pow(RadiusX, 2)).
	/// </para>
	/// <para>
	/// The focal points are placed on the major of the two axes, if one is
	/// found, otherwise, on the X axis.
	/// </para>
	/// </remarks>
	public class FEllipse
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* mCenter_CoordinateChanged																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The center coordinate has been changed at its member values.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Floating point point event arguments.
		/// </param>
		private void mCenter_CoordinateChanged(object sender,
			FloatPointEventArgs e)
		{
			OnCenterChanged(e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UpdateFocalPoints																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the focal points on the provided ellipse for the current center
		/// and radii.
		/// </summary>
		/// <param name="ellipse">
		/// Reference to the ellipse to update.
		/// </param>
		private static void UpdateFocalPoints(FEllipse ellipse)
		{
			float c = 0f;

			if(ellipse != null)
			{
				c = (float)Math.Sqrt(Math.Pow((double)ellipse.mRadiusY, 2d) -
					Math.Pow((double)ellipse.mRadiusX, 2d));
				ellipse.mFocalPoint2.ReadOnly = false;
				ellipse.mFocalPoint1.ReadOnly = false;
				if(ellipse.mRadiusY > ellipse.mRadiusX)
				{
					//	Place the focal points on the Y major axis.
					ellipse.mFocalPoint1.X = ellipse.mCenter.X;
					ellipse.mFocalPoint1.Y = ellipse.mCenter.Y - c;
					ellipse.mFocalPoint2.X = ellipse.mCenter.X;
					ellipse.mFocalPoint2.Y = ellipse.mCenter.Y + c;
				}
				else
				{
					//	Place the focal points on the X major axis.
					ellipse.mFocalPoint1.X = ellipse.mCenter.X - c;
					ellipse.mFocalPoint1.Y = ellipse.mCenter.Y;
					ellipse.mFocalPoint2.X = ellipse.mCenter.X + c;
					ellipse.mFocalPoint2.Y = ellipse.mCenter.Y;
				}
				ellipse.mFocalPoint1.ReadOnly = true;
				ellipse.mFocalPoint2.ReadOnly = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnCenterChanged																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the CenterChanged event when the center value has changed.
		/// </summary>
		/// <param name="e">
		/// Reference to the floating-point point event event arguments for this
		/// event.
		/// </param>
		protected virtual void OnCenterChanged(FloatPointEventArgs e)
		{
			CenterChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnRadiusXChanged																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the RadiusX changed event when the value of the RadiusX
		/// property has changed.
		/// </summary>
		/// <param name="e">
		/// Float event arguments.
		/// </param>
		protected virtual void OnRadiusXChanged(FloatEventArgs e)
		{
			RadiusXChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnRadiusYChanged																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the RadiusY changed event when the value of the RadiusY
		/// property has changed.
		/// </summary>
		/// <param name="e">
		/// Float event arguments.
		/// </param>
		protected virtual void OnRadiusYChanged(FloatEventArgs e)
		{
			RadiusYChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the FEllipse Item.
		/// </summary>
		public FEllipse()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FEllipse Item.
		/// </summary>
		/// <param name="centerX">
		/// The center coordinate on the x-axis (h).
		/// </param>
		/// <param name="centerY">
		/// The center coordinate on the y-axis (k).
		/// </param>
		/// <param name="radiusX">
		/// The x-axis radius (a).
		/// </param>
		/// <param name="radiusY">
		/// The y-axis radius (b).
		/// </param>
		public FEllipse(float centerX, float centerY, float radiusX,
			float radiusY) : this()
		{
			this.mCenter.X = centerX;
			this.mCenter.Y = centerY;
			this.mRadiusX = radiusX;
			this.mRadiusY = radiusY;
			UpdateFocalPoints(this);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* BoundingBox																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the bounding box of the specified ellipse.
		/// </summary>
		/// <param name="ellipse">
		/// Reference to the ellipse for which the bounding box will be returned.
		/// </param>
		/// <returns>
		/// Reference to an area containing the specified ellipse, if legitimate.
		/// Otherwise, an empty area.
		/// </returns>
		public static FArea BoundingBox(FEllipse ellipse)
		{
			float h = 0f;
			FArea result = null;
			float w = 0f;
			float x = 0f;
			float y = 0f;

			if(ellipse != null)
			{
				x = ellipse.mCenter.X - ellipse.mRadiusX;
				y = ellipse.mCenter.Y - ellipse.mRadiusY;
				w = (ellipse.mCenter.X + ellipse.mRadiusX) - x;
				h = (ellipse.mCenter.Y + ellipse.mRadiusY) - y;

				result = new FArea(x, y, w, h);
			}
			if(result == null)
			{
				result = new FArea();
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the bounding box that contains all of the specified ellipses.
		/// </summary>
		/// <param name="ellipses">
		/// Reference to a list of ellipses to enclose.
		/// </param>
		/// <returns>
		/// Reference to the smallest bounding box that encloses all of the
		/// constituent ellipses, if legitimate. Otherwise, an empty area.
		/// </returns>
		public static FArea BoundingBox(List<FEllipse> ellipses)
		{
			float bx = 0f;
			float by = 0f;
			float eh = 0f;
			float ew = 0f;
			float ex = 0f;
			float ey = 0f;
			FArea result = null;

			if(ellipses?.Count > 0)
			{
				bx = ellipses.Min(x => x.mCenter.X - x.mRadiusX);
				by = ellipses.Min(y => y.mCenter.Y - y.mRadiusY);
				ex = ellipses.Max(x => x.mCenter.X + x.mRadiusX);
				ey = ellipses.Max(y => y.mCenter.Y + y.mRadiusY);
				ew = ex - bx;
				eh = ey - by;

				result = new FArea(bx, by, ew, eh);
			}
			if(result == null)
			{
				result = new FArea();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Center																																*
		//*-----------------------------------------------------------------------*
		private FPoint mCenter = new FPoint();
		/// <summary>
		/// Get/Set a reference to the center coordinate.
		/// </summary>
		public FPoint Center
		{
			get { return mCenter; }
			set
			{
				float newX = 0f;
				float newY = 0f;
				float originalX = 0f;
				float originalY = 0f;

				if(!Object.ReferenceEquals(mCenter, value))
				{
					if(mCenter != null)
					{
						originalX = mCenter.X;
						originalY = mCenter.Y;
						mCenter.CoordinateChanged -= mCenter_CoordinateChanged;
					}
					mCenter = value;
					if(mCenter != null)
					{
						newX = mCenter.X;
						newY = mCenter.Y;
						mCenter.CoordinateChanged += mCenter_CoordinateChanged;
					}
					UpdateFocalPoints(this);
					OnCenterChanged(
						new FloatPointEventArgs(newX, newY, originalX, originalY));
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CenterChanged																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the center of the ellipse has changed.
		/// </summary>
		public event FloatPointEventHandler CenterChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Clear																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clear the ellipse.
		/// </summary>
		public void Clear()
		{
			mCenter.X = 0f;
			mCenter.Y = 0f;
			mFocalPoint2.ReadOnly = false;
			mFocalPoint1.ReadOnly = false;
			mFocalPoint1.X = 0f;
			mFocalPoint1.Y = 0f;
			mFocalPoint2.X = 0f;
			mFocalPoint2.Y = 0f;
			mFocalPoint1.ReadOnly = true;
			mFocalPoint2.ReadOnly = true;
			mRadiusX = 0f;
			mRadiusY = 0f;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a deep clone of the caller's ellipse.
		/// </summary>
		/// <param name="ellipse">
		/// Reference to the ellipse to clone.
		/// </param>
		/// <returns>
		/// Reference to a new ellipse that is a complete deep clone of the
		/// original, if the provided value was legitimate. Otherwise, an empty
		/// ellipse.
		/// </returns>
		public static FEllipse Clone(FEllipse ellipse)
		{
			FEllipse result = new FEllipse();

			if(ellipse != null)
			{
				result.mCenter = new FPoint(ellipse.mCenter);
				result.mFocalPoint1 = new FPoint(ellipse.mFocalPoint1);
				result.mFocalPoint2 = new FPoint(ellipse.mFocalPoint2);
				result.mRadiusX = ellipse.mRadiusX;
				result.mRadiusY = ellipse.mRadiusY;
				result.mFocalPoint1.ReadOnly = true;
				result.mFocalPoint2.ReadOnly = true;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* FindIntersections																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return an array of coordinates containing zero or more intersections
		/// between the edge of the ellipse and a provided line.
		/// </summary>
		/// <param name="ellipse">
		/// Reference to the ellipse to test for intersections.
		/// </param>
		/// <param name="line">
		/// Reference to the line being tested for intersection.
		/// </param>
		/// <param name="allowImaginary">
		/// Value indicating whether imaginary intersections on the line will
		/// be included.
		/// </param>
		/// <returns>
		/// Reference to an array of coordinates where the line crosses the edge
		/// of the ellipse, if intersections were found. Otherwise, an empty array.
		/// </returns>
		public static FPoint[] FindIntersections(FEllipse ellipse, FLine line,
			bool allowImaginary = false)
		{
			FPoint[] result = null;

			if(ellipse != null && line != null)
			{
				result = FindIntersections(ellipse.mCenter,
					ellipse.mRadiusX, ellipse.mRadiusY, line, allowImaginary);
			}
			if(result == null)
			{
				result = new FPoint[0];
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
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
		/// <param name="allowImaginary">
		/// Value indicating whether imaginary intersections on the line will
		/// be included.
		/// </param>
		/// <returns>
		/// Reference to an array of coordinates where the line crosses the edge
		/// of the ellipse, if intersections were found. Otherwise, an empty array.
		/// </returns>
		public static FPoint[] FindIntersections(FPoint center, float radiusX,
			float radiusY, FLine line, bool allowImaginary = false)
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
					if(allowImaginary || FLine.IsPointOnLine(line, point))
					{
						result.Add(point);
					}
					point = new FPoint((float)(x1 + t2 * dx), (float)(y1 + t2 * dy));
					if(allowImaginary || FLine.IsPointOnLine(line, point))
					{
						result.Add(point);
					}
				}
			}
			return result.ToArray();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	FocalPoint1																														*
		//*-----------------------------------------------------------------------*
		private FPoint mFocalPoint1 = new FPoint();
		/// <summary>
		/// Get a reference to the first focal point.
		/// </summary>
		public FPoint FocalPoint1
		{
			get { return mFocalPoint1; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	FocalPoint2																														*
		//*-----------------------------------------------------------------------*
		private FPoint mFocalPoint2 = new FPoint();
		/// <summary>
		/// Get a reference to the second focal point.
		/// </summary>
		public FPoint FocalPoint2
		{
			get { return mFocalPoint2; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetArea																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the area in the caller's ellipse.
		/// </summary>
		/// <param name="ellipse">
		/// Reference to the ellipse to measure.
		/// </param>
		/// <returns>
		/// The area of the ellipse.
		/// </returns>
		public static float GetArea(FEllipse ellipse)
		{
			float result = 0f;

			if(ellipse != null)
			{
				result = (float)(Math.PI *
					(double)ellipse.mRadiusX * (double)ellipse.mRadiusY);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetCoordinateAtAngle																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a coordinate of the edge of the shape at the specified angle.
		/// </summary>
		/// <param name="ellipse">
		/// Reference to the ellipse whose edge coordinate will be found.
		/// </param>
		/// <param name="angle">
		/// The angle from center at which to find the coordinate, in radians.
		/// </param>
		/// <returns>
		/// Reference to a new point representing the coordinate on the edge of
		/// the ellipse at the specified angle, if legitimate. Otherwise, an
		/// empty point.
		/// </returns>
		public static FPoint GetCoordinateAtAngle(FEllipse ellipse, float angle)
		{
			FPoint result = new FPoint();

			if(ellipse != null)
			{
				result.X = (float)((double)ellipse.mCenter.X +
					(double)ellipse.mRadiusX * Math.Cos(angle));
				result.Y = (float)((double)ellipse.mCenter.Y +
					(double)ellipse.mRadiusY * Math.Sin(angle));
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetPerimeter																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the perimeter of the shape.
		/// </summary>
		/// <param name="ellipse">
		/// Reference to the ellipse for which the perimeter will be returned.
		/// </param>
		/// <returns>
		/// The perimeter of the ellipse.
		/// </returns>
		/// <remarks>
		/// The perimeter of the ellipse is an approprimate value. This variation
		/// was discovered by Srinivasa Ramanujan in the 1910s.
		/// </remarks>
		public static float GetPerimeter(FEllipse ellipse)
		{
			double a = 0f;
			double b = 0f;
			float result = 0f;

			if(ellipse != null)
			{
				a = (double)ellipse.mRadiusX;
				b = (double)ellipse.mRadiusY;
				result = (float)(Math.PI * (3d * (a + b) -
					Math.Sqrt((3d * a + b) * (a + 3d * b))));
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetStringLength																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the length of the string between the two focal points used to
		/// draw the entire perimeter.
		/// </summary>
		/// <param name="ellipse">
		/// Reference to the ellipse whose perimeter string will be measured.
		/// </param>
		/// <returns>
		/// Length of an imaginary string, tied between two focal points, that can
		/// reach to any point in the perimeter of the ellipse.
		/// </returns>
		public static float GetStringLength(FEllipse ellipse)
		{
			float result = 0f;
			float x = 0f;
			float y = 0f;

			if(ellipse != null)
			{
				if(ellipse.mRadiusY > ellipse.mRadiusX)
				{
					//	Major Y radius.
					y = ellipse.mCenter.Y - ellipse.mRadiusY;
					result = (ellipse.mFocalPoint1.Y - y) + (ellipse.mFocalPoint2.Y - y);
				}
				else
				{
					//	Major X radius.
					x = ellipse.mCenter.X - ellipse.mRadiusX;
					result = (ellipse.mFocalPoint1.X - x) + (ellipse.mFocalPoint2.X - x);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* HasVolume																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified ellipse has a usable
		/// volume.
		/// </summary>
		/// <param name="ellipse">
		/// Reference to the ellipse to inspect.
		/// </param>
		/// <returns>
		/// True if the ellipse has a usable volume. Otherwise, false.
		/// </returns>
		public static bool HasVolume(FEllipse ellipse)
		{
			bool result = false;

			result = ellipse != null &&
				ellipse.mRadiusX != 0f && ellipse.mRadiusY != 0f;
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsDifferent																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the values of the two ellipses are
		/// different.
		/// </summary>
		/// <param name="value1">
		/// Reference to the first value to compare.
		/// </param>
		/// <param name="value2">
		/// Reference to the second value to compare.
		/// </param>
		/// <returns>
		/// Value indicating whether the two ellipses are different.
		/// </returns>
		public static bool IsDifferent(FEllipse value1, FEllipse value2)
		{
			bool result = false;

			if(value1 != null && value2 != null)
			{
				result =
					!value1.mCenter.Equals(value2.mCenter) ||
					!value1.mFocalPoint1.Equals(value2.mFocalPoint1) ||
					!value1.mFocalPoint2.Equals(value2.mFocalPoint2) ||
					value1.mRadiusX != value2.mRadiusX ||
					value1.mRadiusY != value2.mRadiusY;
			}
			else if(value1 != null || value2 != null)
			{
				result = true;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsEmpty																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the provided ellipse is empty.
		/// </summary>
		/// <param name="ellipse">
		/// Reference to the ellipse to inspect.
		/// </param>
		/// <returns>
		/// True if the specified ellipse is empty. Otherwise, false.
		/// </returns>
		public static bool IsEmpty(FEllipse ellipse)
		{
			bool result = true;

			if(ellipse != null)
			{
				result = (FPoint.IsEmpty(ellipse.mCenter) &&
					FPoint.IsEmpty(ellipse.mFocalPoint1) &&
					FPoint.IsEmpty(ellipse.mFocalPoint2) &&
					ellipse.mRadiusX == 0f &&
					ellipse.mRadiusY == 0f);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RadiusX																																*
		//*-----------------------------------------------------------------------*
		private float mRadiusX = 0f;
		/// <summary>
		/// Get/Set the X-axis radius.
		/// </summary>
		public float RadiusX
		{
			get { return mRadiusX; }
			set
			{
				float original = mRadiusX;

				mRadiusX = value;
				if(original != value)
				{
					UpdateFocalPoints(this);
					OnRadiusXChanged(new FloatEventArgs(value, original));
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RadiusXChanged																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the x-axis radius value has changed.
		/// </summary>
		public event FloatEventHandler RadiusXChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RadiusY																																*
		//*-----------------------------------------------------------------------*
		private float mRadiusY = 0f;
		/// <summary>
		/// Get/Set the Y-axis radius.
		/// </summary>
		public float RadiusY
		{
			get { return mRadiusY; }
			set
			{
				float original = mRadiusY;

				mRadiusY = value;
				if(original != value)
				{
					UpdateFocalPoints(this);
					OnRadiusYChanged(new FloatEventArgs(value, original));
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RadiusYChanged																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the y-axis radius value has changed.
		/// </summary>
		public event FloatEventHandler RadiusYChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Scale																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the scale factor between the two ellipses.
		/// </summary>
		/// <param name="newEllipse">
		/// Reference to the new ellipse (numerator).
		/// </param>
		/// <param name="oldEllipse">
		/// Reference to the old or original ellipse (denominator).
		/// </param>
		/// <returns>
		/// Reference to the newly created scale containing the scaling factor
		/// between the caller's two ellipses.
		/// </returns>
		public static FScale Scale(FEllipse newEllipse, FEllipse oldEllipse)
		{
			FScale result = new FScale();

			if(newEllipse != null && oldEllipse != null)
			{
				if(oldEllipse.mRadiusX != 0f)
				{
					result.ScaleX = newEllipse.mRadiusX / oldEllipse.mRadiusX;
				}
				if(oldEllipse.mRadiusY != 0f)
				{
					result.ScaleY = newEllipse.mRadiusY / oldEllipse.mRadiusY;
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the scaled version of the provided ellipse with the specified
		/// scale.
		/// </summary>
		/// <param name="source">
		/// Reference to the reference ellipse.
		/// </param>
		/// <param name="scale">
		/// The scale to apply to the caller's ellipse.
		/// </param>
		/// <returns>
		/// A new representation of the caller's ellipse, where the specified scale
		/// has been applied, if legitimate. Otherwise, an empty ellipse.
		/// </returns>
		public static FEllipse Scale(FEllipse source, float scale)
		{
			FEllipse result = new FEllipse();

			if(source != null)
			{
				result.mCenter.X = source.mCenter.X * scale;
				result.mCenter.Y = source.mCenter.Y * scale;
				result.mRadiusX = source.mRadiusX * scale;
				result.mRadiusY = source.mRadiusY * scale;
				UpdateFocalPoints(result);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TransferValues																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Transfer the member values from the source to the target, raising any
		/// associated events where appropriate.
		/// </summary>
		/// <param name="source">
		/// Reference to the source shape.
		/// </param>
		/// <param name="target">
		/// Reference to the target shape.
		/// </param>
		public static void TransferValues(FEllipse source, FEllipse target)
		{
			float originalRadiusX = 0f;
			float originalRadiusY = 0f;

			if(source != null && target != null)
			{
				FPoint.TransferValues(source.mCenter, target.mCenter);
				originalRadiusX = target.mRadiusX;
				originalRadiusY = target.mRadiusY;
				target.mRadiusX = source.mRadiusX;
				target.mRadiusY = source.mRadiusY;
				UpdateFocalPoints(target);
				if(originalRadiusX != source.mRadiusX)
				{
					target.OnRadiusXChanged(
						new FloatEventArgs(source.mRadiusX, originalRadiusX));
				}
				if(originalRadiusY != source.mRadiusY)
				{
					target.OnRadiusYChanged(
						new FloatEventArgs(source.mRadiusY, originalRadiusY));
				}
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	FEllipseCollection																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of FEllipse Items.
	/// </summary>
	public class FEllipseCollection : List<FEllipse>
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


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	FEllipseTypeConverter																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Type conversion for FEllipse that allows editing of child properties in a
	/// property grid.
	/// </summary>
	public class FEllipseTypeConverter : TypeConverter
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
		//* CanConvertTo																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether this converter can convert to the
		/// specified destination type.
		/// </summary>
		/// <param name="context">
		/// Context to inspect.
		/// </param>
		/// <param name="destinationType">
		/// Destination type to query for.
		/// </param>
		/// <returns>
		/// True if this type converter is able to convert to the specified
		/// type. Otherwise, false.
		/// </returns>
		/// <remarks>
		/// Overridden specifically to support serialization in JSON.NET. If
		/// string type is disallowed, string serialization won't be affected.
		/// </remarks>
		public override bool CanConvertTo(ITypeDescriptorContext context,
			Type destinationType)
		{
			bool result = false;

			if(destinationType != typeof(string))
			{
				result = base.CanConvertTo(context, destinationType);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetProperties																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the collection of property descriptors for the context, object,
		/// and attributes provided.
		/// </summary>
		/// <param name="context">
		/// The context to consider.
		/// </param>
		/// <param name="value">
		/// The live object from which the properties will be retrieved.
		/// </param>
		/// <param name="attributes">
		/// Array of attributes by which to filter the return properties.
		/// </param>
		/// <returns>
		/// Reference to a collection of property descriptors matching the
		/// caller's specification.
		/// </returns>
		/// <remarks>
		/// In this version, none of the incoming parameters are checked.
		/// </remarks>
		public override PropertyDescriptorCollection GetProperties(
			ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(FArea));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetPropertiesSupported																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the GetProperties method is supported
		/// in the specified context.
		/// </summary>
		/// <param name="context">
		/// Context to check for support.
		/// </param>
		/// <returns>
		/// True if the GetProperties method is supported in the specified context.
		/// Otherwise, false.
		/// </returns>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*


}
