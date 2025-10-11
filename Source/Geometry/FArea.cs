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
using System.ComponentModel;
using System.Linq;

using Newtonsoft.Json;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	FArea																																		*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// An area within float coordinates.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The use of this class is similar to that of RectangleF. The difference
	/// is that this is item creates a first-class object - usable in all
	/// circumstances where references are appropriate.
	/// </para>
	/// <para>
	/// The edges of this class can be changed by setting Bottom, Left, Right,
	/// and Top. To position the area, set the X and Y coordinates.
	/// </para>
	/// </remarks>
	[TypeConverter(typeof(FAreaTypeConverter))]
	public class FArea
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnBottomChanged																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the BottomChanged event when the value of the Bottom property
		/// has changed.
		/// </summary>
		/// <param name="e">
		/// Float event arguments.
		/// </param>
		protected virtual void OnBottomChanged(FloatEventArgs e)
		{
			BottomChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnHeightChanged																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the HeightChanged event when the value of the Height property
		/// has changed.
		/// </summary>
		/// <param name="e">
		/// Float event arguments.
		/// </param>
		protected virtual void OnHeightChanged(FloatEventArgs e)
		{
			HeightChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnLeftChanged																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the LeftChanged event when the value of the Left property
		/// has changed.
		/// </summary>
		/// <param name="e">
		/// Float event arguments.
		/// </param>
		protected virtual void OnLeftChanged(FloatEventArgs e)
		{
			LeftChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnLocationChanged																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the LocationChanged event when the values of the X or Y
		/// properties have changed.
		/// </summary>
		/// <param name="e">
		/// Float point event arguments.
		/// </param>
		protected virtual void OnLocationChanged(FloatPointEventArgs e)
		{
			LocationChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnRightChanged																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the RightChanged event when the value of the Right property
		/// has changed.
		/// </summary>
		/// <param name="e">
		/// Float event arguments.
		/// </param>
		protected virtual void OnRightChanged(FloatEventArgs e)
		{
			RightChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnTopChanged																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the TopChanged event when the value of the Top property
		/// has changed.
		/// </summary>
		/// <param name="e">
		/// Float event arguments.
		/// </param>
		protected virtual void OnTopChanged(FloatEventArgs e)
		{
			TopChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnWidthChanged																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the WidthChanged event when the value of the Width property
		/// has changed.
		/// </summary>
		/// <param name="e">
		/// Float event arguments.
		/// </param>
		protected virtual void OnWidthChanged(FloatEventArgs e)
		{
			WidthChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the FArea Item.
		/// </summary>
		public FArea()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FArea item.
		/// </summary>
		/// <param name="original">
		/// Reference to the original item to copy.
		/// </param>
		public FArea(FArea original)
		{
			if(original != null)
			{
				mLeft = original.mLeft;
				mTop = original.mTop;
				mRight = original.mRight;
				mBottom = original.mBottom;
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FArea Item.
		/// </summary>
		/// <param name="x">
		/// X coordinate.
		/// </param>
		/// <param name="y">
		/// Y coordinate.
		/// </param>
		/// <param name="width">
		/// Width of the area.
		/// </param>
		/// <param name="height">
		/// Height of the area.
		/// </param>
		public FArea(float x, float y, float width, float height)
		{
			mLeft = x;
			mTop = y;
			Width = width;
			Height = height;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FArea item.
		/// </summary>
		/// <param name="locationTopLeft">
		/// The top left location.
		/// </param>
		/// <param name="locationBottomRight">
		/// The bottom right location.
		/// </param>
		public FArea(FVector2 locationTopLeft, FVector2 locationBottomRight)
		{
			if(locationTopLeft != null)
			{
				mLeft = locationTopLeft.X;
				mTop = locationTopLeft.Y;
				if(locationBottomRight == null)
				{
					mRight = mLeft;
					mBottom = mTop;
				}
			}
			if(locationBottomRight != null)
			{
				mRight = locationBottomRight.X;
				mBottom = locationBottomRight.Y;
				if(locationTopLeft == null)
				{
					mLeft = mRight;
					mTop = mBottom;
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FArea item.
		/// </summary>
		/// <param name="location">
		/// The location of the area.
		/// </param>
		/// <param name="size">
		/// The shape of the area.
		/// </param>
		public FArea(FVector2 location, FSize size)
		{
			if(location != null)
			{
				mLeft = location.X;
				mTop = location.Y;
				if(size == null)
				{
					mRight = mLeft;
					mBottom = mTop;
				}
			}
			if(size != null)
			{
				mRight = mLeft + size.Width;
				mBottom = mTop + size.Height;
			}
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Implicit FArea = Rectangle																						*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast a Rectangle value to FArea.
		///// </summary>
		///// <param name="value">
		///// The area to convert.
		///// </param>
		///// <remarks>
		///// This operator is not available when compiling without GDI+.
		///// </remarks>
		//public static implicit operator FArea(Rectangle value)
		//{
		//	FArea result = new FArea();

		//	if(value != Rectangle.Empty)
		//	{
		//		result.mLeft = value.Left;
		//		result.mTop = value.Top;
		//		result.mRight = value.Right;
		//		result.mBottom = value.Bottom;
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Implicit FArea = RectangleF																					*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast a RectangleF value to FArea.
		///// </summary>
		///// <param name="value">
		///// The area to convert.
		///// </param>
		///// <remarks>
		///// This operator is not available when compiling without GDI+.
		///// </remarks>
		//public static implicit operator FArea(RectangleF value)
		//{
		//	FArea result = new FArea();

		//	if(value != RectangleF.Empty)
		//	{
		//		result.mLeft = value.Left;
		//		result.mTop = value.Top;
		//		result.mRight = value.Right;
		//		result.mBottom = value.Bottom;
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Implicit Rectangle = FArea																						*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast an FArea value to Rectangle.
		///// </summary>
		///// <param name="value">
		///// The area to convert.
		///// </param>
		///// <remarks>
		///// This operator is not available when compiling without GDI+.
		///// </remarks>
		//public static implicit operator Rectangle(FArea value)
		//{
		//	Rectangle rv = Rectangle.Empty;

		//	if(value != null)
		//	{
		//		rv = new Rectangle(
		//			(int)value.Left, (int)value.Top,
		//			(int)value.Width, (int)value.Height);
		//	}
		//	return rv;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Implicit RectangleF = FArea																					*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast an FArea value to RectangleF.
		///// </summary>
		///// <param name="value">
		///// The area to convert.
		///// </param>
		///// <remarks>
		///// This operator is not available when compiling without GDI+.
		///// </remarks>
		//public static implicit operator RectangleF(FArea value)
		//{
		//	RectangleF rv = RectangleF.Empty;

		//	if(value != null)
		//	{
		//		rv = new RectangleF(value.Left, value.Top, value.Width, value.Height);
		//	}
		//	return rv;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	_Implicit SKRect = FArea																							*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Cast an FArea value to SKRect.
		///// </summary>
		///// <param name="value">
		///// The area to convert.
		///// </param>
		//public static implicit operator SKRect(FArea value)
		//{
		//	SKRect rv = SKRect.Empty;

		//	if(value != null)
		//	{
		//		rv = new SKRect(value.Left, value.Top, value.Right, value.Bottom);
		//	}
		//	return rv;
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Bottom																																*
		//*-----------------------------------------------------------------------*
		private float mBottom = 0f;
		/// <summary>
		/// Get/Set the bottom coordinate.
		/// </summary>
		[JsonIgnore]
		public float Bottom
		{
			get { return mBottom; }
			set
			{
				float original = mBottom;

				if(!mReadOnly)
				{
					mBottom = value;
					if(original != value)
					{
						OnBottomChanged(new FloatEventArgs(value, original));
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* BottomChanged																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the bottom value has changed.
		/// </summary>
		public event FloatEventHandler BottomChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* BoundingBox																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the bounding box that contains all of the specified areas.
		/// </summary>
		/// <param name="areas">
		/// Reference to a list of areas to enclose.
		/// </param>
		/// <returns>
		/// Reference to the smallest bounding box that encloses all of the
		/// constituent areas.
		/// </returns>
		public static FArea BoundingBox(List<FArea> areas)
		{
			FArea result = new FArea();

			if(areas?.Count > 0)
			{
				result.mLeft =
					Math.Min(areas.Min(x => x.mLeft), areas.Min(x => x.mRight));
				result.mTop =
					Math.Min(areas.Min(y => y.mTop), areas.Min(y => y.mBottom));
				result.mRight =
					Math.Max(areas.Max(x => x.mRight), areas.Max(x => x.Left));
				result.mBottom =
					Math.Max(areas.Max(y => y.mBottom), areas.Max(y => y.Top));
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Clear																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clear the area without raising an event.
		/// </summary>
		public void Clear()
		{
			if(!mReadOnly)
			{
				mLeft = 0f;
				mRight = 0f;
				mTop = 0f;
				mBottom = 0f;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a memberwise clone of the provided area.
		/// </summary>
		/// <param name="source">
		/// Reference to the source area to be cloned.
		/// </param>
		/// <returns>
		/// Reference to a new FArea instance where the primitive member values
		/// are the same as those in the source, if a legitimate source was
		/// provided. Otherwise, an empty FArea.
		/// </returns>
		public static FArea Clone(FArea source)
		{
			FArea result = new FArea();

			if(source != null)
			{
				result.mBottom = source.mBottom;
				result.mLeft = source.mLeft;
				result.mReadOnly = source.mReadOnly;
				result.mRight = source.mRight;
				result.mTop = source.mTop;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Contains																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the container area fully contains the
		/// specified subject.
		/// </summary>
		/// <param name="container">
		/// Reference to the container area.
		/// </param>
		/// <param name="subject">
		/// Reference to the candidate subject.
		/// </param>
		/// <returns>
		/// True if the subject is fully within the container area. Otherwise,
		/// false.
		/// </returns>
		public static bool Contains(FArea container, FArea subject)
		{
			float containerBottom = 0f;
			float containerLeft = 0f;
			float containerRight = 0f;
			float containerTop = 0f;
			bool result = false;
			float subjectBottom = 0f;
			float subjectLeft = 0f;
			float subjectRight = 0f;
			float subjectTop = 0f;

			if(container != null && subject != null)
			{
				//	Convert the areas to global space for comparison.
				containerLeft = Math.Min(container.mLeft, container.mRight);
				containerTop = Math.Min(container.mTop, container.mBottom);
				containerRight = Math.Max(container.mRight, container.mLeft);
				containerBottom = Math.Max(container.mBottom, container.mTop);
				subjectLeft = Math.Min(subject.mLeft, subject.mRight);
				subjectTop = Math.Min(subject.mTop, subject.mBottom);
				subjectRight = Math.Max(subject.mRight, subject.mLeft);
				subjectBottom = Math.Max(subject.mBottom, subject.mTop);

				if(containerLeft <= subjectLeft &&
					containerTop <= subjectTop &&
					containerRight >= subjectRight &&
					containerBottom >= subjectBottom)
				{
					result = true;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Delta																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the delta values between the caller's area and the new and
		/// original areas.
		/// </summary>
		/// <param name="originalArea">
		/// Reference to the original area.
		/// </param>
		/// <param name="newArea">
		/// Reference to the new area.
		/// </param>
		/// <returns>
		/// Reference to the relative bounds.
		/// </returns>
		public static FArea Delta(FArea originalArea, FArea newArea)
		{
			FArea result = new FArea();

			if(originalArea != null && newArea != null)
			{
				result.mLeft = newArea.mLeft - originalArea.mLeft;
				result.mTop = newArea.mTop - originalArea.mTop;
				result.mRight = newArea.mRight - originalArea.mRight;
				result.mBottom = newArea.mBottom - originalArea.mBottom;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetArea																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the total area of the specified area object.
		/// </summary>
		/// <param name="area">
		/// Reference to the area to be inspected.
		/// </param>
		/// <returns>
		/// The total area, in the object's units, of the supplied area object.
		/// </returns>
		public static float GetArea(FArea area)
		{
			float result = 0f;

			if(area != null)
			{
				result = area.Width * area.Height;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetCenter																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the center point of the specified area.
		/// </summary>
		/// <param name="area">
		/// Reference to the area to be inspected.
		/// </param>
		/// <returns>
		/// Reference to the center point of the provided area, if legitimate.
		/// Otherwise, null.
		/// </returns>
		public static FVector2 GetCenter(FArea area)
		{
			FVector2 result = null;

			if(area != null)
			{
				result = new FVector2(
					area.Left + ((area.Right - area.Left) / 2f),
					area.Top + ((area.Bottom - area.Top) / 2f)
					);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetIntersections																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the intersections of two areas.
		/// </summary>
		/// <param name="area1">
		/// Reference to the first area to be tested.
		/// </param>
		/// <param name="area2">
		/// Reference to the second area to be tested.
		/// </param>
		/// <returns>
		/// Reference to a list of points where the two areas intersect, if found.
		/// Otherwise, an empty list.
		/// </returns>
		public static List<FVector2> GetIntersections(FArea area1, FArea area2)
		{
			List<FLine> lines1 = null;
			List<FLine> lines2 = null;
			FVector2 point = null;
			List<FVector2> result = new List<FVector2>();

			if(area1 != null && area2 != null)
			{
				lines1 = GetLines(area1);
				lines2 = GetLines(area2);
				foreach(FLine line1Item in lines1)
				{
					foreach(FLine line2Item in lines2)
					{
						point = FLine.Intersect(line1Item, line2Item, false);
						if(point != null)
						{
							result.Add(point);
						}
					}
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the intersections of a line with a rectangle.
		/// </summary>
		/// <param name="area">
		/// Reference to the area to test.
		/// </param>
		/// <param name="line">
		/// Reference to the line to test.
		/// </param>
		/// <returns>
		/// Reference to a list of points where the supplied line intersects with
		/// the specified rectangle, if found. Otherwise, an empty list.
		/// </returns>
		public static List<FVector2> GetIntersections(FArea area, FLine line)
		{
			List<FLine> lines = null;
			FVector2 point = null;
			List<FVector2> result = new List<FVector2>();

			if(area != null && line != null)
			{
				lines = GetLines(area);
				foreach(FLine lineItem in lines)
				{
					point = FLine.Intersect(lineItem, line, false);
					if(point != null)
					{
						result.Add(point);
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetLines																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return four lines representing the bounding edge of the provided area.
		/// </summary>
		/// <param name="area">
		/// Reference to the area for which the lines will be returned.
		/// </param>
		/// <param name="rotation">
		/// Optional angle of local shape rotatation, in radians.
		/// </param>
		/// <returns>
		/// Reference to a list of four lines, if the area was legitimate and
		/// non-empty. Otherwise, an empty array.
		/// </returns>
		/// <remarks>
		/// <para>
		/// In drawing space, the lines from this method are arranged in a
		/// counter-clockwise progression from top-right, including the top, left,
		/// bottom, then right sides.
		/// </para>
		/// <para>
		/// All of the lines are constructed from common adjoining points, which
		/// allows you to move any point in the shape without breaking its
		/// connection to either of its lines.
		/// </para>
		/// </remarks>
		public static List<FLine> GetLines(FArea area, float rotation = 0f)
		{
			int index = 0;
			List<FVector2> points = null;
			List<FLine> result = new List<FLine>();

			if(!FArea.IsEmpty(area))
			{
				if(rotation == 0f)
				{
					//	Non-rotated output is faster.
					result.AddRange(new FLine[]
					{
					new FLine(
						new FVector2(area.Right, area.Top),
						new FVector2(area.Left, area.Top)),
					new FLine(
						new FVector2(area.Left, area.Top),
						new FVector2(area.Left, area.Bottom)),
					new FLine(
						new FVector2(area.Left, area.Bottom),
						new FVector2(area.Right, area.Bottom)),
					new FLine(
						new FVector2(area.Right, area.Bottom),
						new FVector2(area.Right, area.Top))
					});
				}
				else
				{
					points = GetVertices(area, rotation);
					for(index = 0; index < 3; index ++)
					{
						result.Add(new FLine(points[index], points[index + 1]));
					}
					result.Add(new FLine(points[3], points[0]));
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* GetLocation																														*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return the location of the provided area as a floating point point.
		///// </summary>
		///// <param name="area">
		///// Reference to the area to inspect.
		///// </param>
		///// <returns>
		///// Reference to a new point containing the area's location, if legitimate.
		///// Otherwise, an empty point.
		///// </returns>
		//public static FVector2 GetLocation(FArea area)
		//{
		//	FVector2 result = new FVector2();

		//	if(area != null)
		//	{
		//		result.X = area.X;
		//		result.Y = area.Y;
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* GetSize																																*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return the size of the provided area as a floating point size.
		///// </summary>
		///// <param name="area">
		///// Reference to the area to inspect.
		///// </param>
		///// <returns>
		///// Reference to the new size containing the area's size, if legitimate.
		///// Otherwise, an empty size.
		///// </returns>
		//public static FSize GetSize(FArea area)
		//{
		//	FSize result = new FSize();

		//	if(area != null)
		//	{
		//		result.Width = area.Width;
		//		result.Height = area.Height;
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetVertices																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the vertices of the area.
		/// </summary>
		/// <param name="area">
		/// Reference to the area whose vertices will be inspected.
		/// </param>
		/// <param name="rotation">
		/// Optional angle of local shape rotatation, in radians.
		/// </param>
		/// <returns>
		/// Reference to a list of floating-point points representing the vertices
		/// of the area.
		/// </returns>
		public static List<FVector2> GetVertices(FArea area, float rotation = 0f)
		{
			FVector2 center = null;
			FVector2 location = null;
			FVector2 point = null;
			List<FVector2> result = new List<FVector2>();
			FArea workingArea = null;

			if(area != null)
			{
				if(rotation == 0f)
				{
					//	Vertices with no rotation is much faster.
					result.AddRange(new FVector2[]
					{
						new FVector2(area.mRight, area.mTop),
						new FVector2(area.mLeft, area.mTop),
						new FVector2(area.mLeft, area.mBottom),
						new FVector2(area.mRight, area.mBottom)
					});
				}
				else
				{
					//	Avoid affecting the caller's area object.
					workingArea = Clone(area);
					center = new FVector2(
						workingArea.mLeft + (workingArea.Width / 2f),
						workingArea.mTop + (workingArea.Height / 2f));
					location = new FVector2(workingArea.mLeft, workingArea.mTop);
					//	Translate to origin.
					Translate(workingArea, FVector2.Negate(center));
					//	Rotate and translate back.
					point =
						FVector2.Rotate(workingArea.mRight, workingArea.mTop, rotation);
					FVector2.Translate(point, center);
					result.Add(point);
					point =
						FVector2.Rotate(workingArea.mLeft, workingArea.mTop, rotation);
					FVector2.Translate(point, center);
					result.Add(point);
					point =
						FVector2.Rotate(workingArea.mLeft, workingArea.mBottom, rotation);
					FVector2.Translate(point, center);
					result.Add(point);
					point =
						FVector2.Rotate(workingArea.mRight, workingArea.mBottom, rotation);
					FVector2.Translate(point, center);
					result.Add(point);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* HasIntersection																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the first provided area intersects
		/// with the second one.
		/// </summary>
		/// <param name="area1">
		/// Reference to the first area to test.
		/// </param>
		/// <param name="area2">
		/// Reference to the second area to test.
		/// </param>
		/// <returns>
		/// True if the two areas intersect. Otherwise, false.
		/// </returns>
		public static bool HasIntersection(FArea area1, FArea area2)
		{
			List<FLine> lines1 = null;
			List<FLine> lines2 = null;
			bool result = false;

			if(area1 != null && area2 != null)
			{
				lines1 = GetLines(area1);
				lines2 = GetLines(area2);
				foreach(FLine line1Item in lines1)
				{
					foreach(FLine line2Item in lines2)
					{
						if(FLine.HasIntersection(line1Item, line2Item, false))
						{
							result = true;
							break;
						}
					}
					if(result)
					{
						break;
					}
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a value indicating whether the specified area intersects with
		/// the provided line.
		/// </summary>
		/// <param name="area">
		/// Reference to the area to test.
		/// </param>
		/// <param name="line">
		/// Reference to the line to test.
		/// </param>
		/// <returns>
		/// True if the supplied line intersects with the given intersection.
		/// Otherwise, false.
		/// </returns>
		public static bool HasIntersection(FArea area, FLine line)
		{
			List<FLine> lines = null;
			bool result = false;

			if(area != null && line != null)
			{
				lines = GetLines(area);
				foreach(FLine lineItem in lines)
				{
					if(FLine.HasIntersection(lineItem, line, false))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* HasVolume																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified area has a usable
		/// volume.
		/// </summary>
		/// <param name="area">
		/// Reference to the area to inspect.
		/// </param>
		/// <returns>
		/// True if the area has a usable volume. Otherwise, false.
		/// </returns>
		public static bool HasVolume(FArea area)
		{
			bool result = false;

			result = area != null && area.Width != 0f && area.Height != 0f;
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Height																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the height of the area.
		/// </summary>
		[JsonProperty(Order = 3)]
		public float Height
		{
			get { return mBottom - mTop; }
			set
			{
				float original = mBottom - mTop;

				if(!mReadOnly)
				{
					mBottom = mTop + value;
					if(original != value)
					{
						OnHeightChanged(new FloatEventArgs(value, original));
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* HeightChanged																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the height value has changed.
		/// </summary>
		public event FloatEventHandler HeightChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsDifferent																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the values of the two areas are
		/// different.
		/// </summary>
		/// <param name="value1">
		/// Reference to the first value to compare.
		/// </param>
		/// <param name="value2">
		/// Reference to the second value to compare.
		/// </param>
		/// <returns>
		/// Value indicating whether the two values are different.
		/// </returns>
		public static bool IsDifferent(FArea value1, FArea value2)
		{
			bool result = false;

			if(value1 != null && value2 != null)
			{
				result =
					value1.mBottom != value2.mBottom ||
					value1.mLeft != value2.mLeft ||
					value1.mRight != value2.mRight ||
					value1.mTop != value2.mTop;
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
		/// Return a value indicating whether the provided area is empty.
		/// </summary>
		/// <param name="area">
		/// Reference to the area to inspect.
		/// </param>
		/// <returns>
		/// True if the specified area is empty. Otherwise, false.
		/// </returns>
		public static bool IsEmpty(FArea area)
		{
			bool result = true;

			if(area != null)
			{
				result = (area.mLeft == 0f &&
					area.mRight == 0f &&
					area.mTop == 0f &&
					area.mBottom == 0f);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsPointAtCorner																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the provided point is at one of the
		/// corners of the caller's rectangle.
		/// </summary>
		/// <param name="area">
		/// Reference to the area to test for corner.
		/// </param>
		/// <param name="point">
		/// Reference to the point to compare with corners.
		/// </param>
		/// <returns>
		/// True if the caller's point is located at one of the corners of the
		/// supplied area.
		/// </returns>
		public static bool IsPointAtCorner(FArea area, FVector2 point)
		{
			bool result = false;

			if(area != null && point != null)
			{
				result =
					((area.Left == point.X && area.Top == point.Y) ||
					(area.Right == point.X && area.Top == point.Y) ||
					(area.Left == point.X && area.Bottom == point.Y) ||
					(area.Right == point.X && area.Bottom == point.Y));
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Left																																	*
		//*-----------------------------------------------------------------------*
		private float mLeft = 0f;
		/// <summary>
		/// Get/Set the left coordinate.
		/// </summary>
		[JsonIgnore]
		public float Left
		{
			get { return mLeft; }
			set
			{
				float original = mLeft;

				if(!mReadOnly)
				{
					mLeft = value;
					if(original != value)
					{
						OnLeftChanged(new FloatEventArgs(value, original));
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* LeftChanged																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the left value has changed.
		/// </summary>
		public event FloatEventHandler LeftChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Location																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the location of the specified area.
		/// </summary>
		/// <param name="area">
		/// Area to inspect.
		/// </param>
		/// <returns>
		/// Reference to the FVector2 representing the location of the caller's
		/// area, if found. Otherwise, an empty FVector2.
		/// </returns>
		public static FVector2 Location(FArea area)
		{
			FVector2 result = null;

			if(area != null)
			{
				result = new FVector2(area.mLeft, area.mTop);
			}
			else
			{
				result = new FVector2();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* LocationChanged																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the overall location has changed.
		/// </summary>
		public event FloatPointEventHandler LocationChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* LocationOpposite																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the opposite corner location of the specified area.
		/// </summary>
		/// <param name="area">
		/// Area to inspect.
		/// </param>
		/// <returns>
		/// Reference to the FVector2 representing the opposite location of the
		/// caller's area, if found. Otherwise, an empty FVector2.
		/// </returns>
		public static FVector2 LocationOpposite(FArea area)
		{
			FVector2 result = null;

			if(area != null)
			{
				result = new FVector2(area.mRight, area.mBottom);
			}
			else
			{
				result = new FVector2();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* MoveTo																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Move the specified area to the new location.
		/// </summary>
		/// <param name="area">
		/// The area to be moved.
		/// </param>
		/// <param name="x">
		/// New X coordinate.
		/// </param>
		/// <param name="y">
		/// New Y coordinate.
		/// </param>
		public static void MoveTo(FArea area, float x, float y)
		{
			if(area != null && !area.mReadOnly)
			{
				area.X = x;
				area.Y = y;
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Move the specified area to the new location.
		/// </summary>
		/// <param name="area">
		/// The area to be moved.
		/// </param>
		/// <param name="newLocation">
		/// The new location for the area.
		/// </param>
		public static void MoveTo(FArea area, FVector2 newLocation)
		{
			if(area != null && !area.mReadOnly && newLocation != null)
			{
				area.X = newLocation.X;
				area.Y = newLocation.Y;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ReadOnly																															*
		//*-----------------------------------------------------------------------*
		private bool mReadOnly = false;
		/// <summary>
		/// Get/Set a value indicating whether this item is read-only.
		/// </summary>
		public bool ReadOnly
		{
			get { return mReadOnly; }
			set { mReadOnly = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RaiseCoordinatesChanged																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raise the coordinate changed events.
		/// </summary>
		public void RaiseCoordinatesChanged()
		{
			OnLeftChanged(new FloatEventArgs(mLeft));
			OnTopChanged(new FloatEventArgs(mTop));
			OnRightChanged(new FloatEventArgs(mRight));
			OnBottomChanged(new FloatEventArgs(mBottom));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Right																																	*
		//*-----------------------------------------------------------------------*
		private float mRight = 0f;
		/// <summary>
		/// Get/Set the right coordinate.
		/// </summary>
		[JsonIgnore]
		public float Right
		{
			get { return mRight; }
			set
			{
				float original = mRight;

				if(!mReadOnly)
				{
					mRight = value;
					if(original != value)
					{
						OnRightChanged(new FloatEventArgs(value, original));
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RightChanged																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the right value has changed.
		/// </summary>
		public event FloatEventHandler RightChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Scale																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the scale of the two areas.
		/// </summary>
		/// <param name="newArea">
		/// Reference to the new area (numerator).
		/// </param>
		/// <param name="oldArea">
		/// Reference to the old or original area (denominator).
		/// </param>
		/// <returns>
		/// Reference to the newly created scale containing the scaling factor
		/// between the caller's two areas.
		/// </returns>
		public static FScale Scale(FArea newArea, FArea oldArea)
		{
			FScale result = new FScale();

			if(newArea != null && oldArea != null)
			{
				if(oldArea.Width != 0f)
				{
					result.ScaleX = newArea.Width / oldArea.Width;
				}
				if(oldArea.Height != 0f)
				{
					result.ScaleY = newArea.Height / oldArea.Height;
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the scaled version of the provided area with the specified
		/// scale.
		/// </summary>
		/// <param name="source">
		/// Reference to the reference area.
		/// </param>
		/// <param name="scale">
		/// The scale to apply to the caller's area.
		/// </param>
		/// <returns>
		/// The representation of the caller's area, where the specified scale
		/// has been applied.
		/// </returns>
		public static FArea Scale(FArea source, float scale)
		{
			FArea result = new FArea();

			if(source != null)
			{
				result.mLeft = source.mLeft * scale;
				result.mTop = source.mTop * scale;
				result.Width = source.Width * scale;
				result.Height = source.Height * scale;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Set																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the details of the area.
		/// </summary>
		/// <param name="area">
		/// Reference to the area to set.
		/// </param>
		/// <param name="x">
		/// X coordinate.
		/// </param>
		/// <param name="y">
		/// Y coordinate.
		/// </param>
		/// <param name="width">
		/// Width of the area.
		/// </param>
		/// <param name="height">
		/// Height of the area.
		/// </param>
		public static void Set(FArea area, float x, float y,
			float width, float height)
		{
			//mLocation.X = x;
			//mLocation.Y = y;
			//mSize.Width = width;
			//mSize.Height = height;
			if(area != null && !area.mReadOnly)
			{
				area.mLeft = x;
				area.mTop = y;
				area.Width = width;
				area.Height = height;
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Set the details of the target area from the source.
		/// </summary>
		/// <param name="source">
		/// Source area to copy.
		/// </param>
		/// <param name="target">
		/// Target area to set.
		/// </param>
		/// <param name="offsetX">
		/// Optional horizontal offset from source.
		/// </param>
		/// <param name="offsetY">
		/// Optional vertical offset from source.
		/// </param>
		public static void Set(FArea source, FArea target,
			float offsetX = 0f, float offsetY = 0f)
		{
			if(source != null && target != null && !target.mReadOnly)
			{
				target.mLeft = source.mLeft + offsetX;
				target.mTop = source.mTop + offsetY;
				target.mBottom = source.mBottom + offsetY;
				target.mRight = source.mRight + offsetX;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SetLocation																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the location of the specified area.
		/// </summary>
		/// <param name="area">
		/// Reference to the area to update.
		/// </param>
		/// <param name="location">
		/// Reference to the location to set.
		/// </param>
		public static void SetLocation(FArea area, FVector2 location)
		{
			if(area != null && !area.mReadOnly && location != null)
			{
				area.X = location.X;
				area.Y = location.Y;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SetSize																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the size of the target area.
		/// </summary>
		/// <param name="source">
		/// Reference to the source area.
		/// </param>
		/// <param name="target">
		/// Reference to the area to update.
		/// </param>
		public static void SetSize(FArea source, FArea target)
		{
			if(source != null && target != null && !target.mReadOnly)
			{
				target.Width = source.Width;
				target.Height = source.Height;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Shrink																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a shrunken version of the specified area.
		/// </summary>
		/// <param name="area">
		/// The area to shrink.
		/// </param>
		/// <param name="amount">
		/// The amount by which to uniformly shrink the area.
		/// </param>
		/// <param name="allowZeroCrossing">
		/// Optional value indicating whether or not to allow a zero-crossing on
		/// the width or height. Default = false.
		/// </param>
		/// <returns>
		/// Reference to the shrunken area.
		/// </returns>
		public static FArea Shrink(FArea area, float amount,
			bool allowZeroCrossing = false)
		{
			FArea result = null;

			if(area != null)
			{
				result = new FArea(area);
				if(allowZeroCrossing)
				{
					result.Width -= amount;
					result.Height -= amount;
				}
				else
				{
					if(result.Width >= 0f && result.Width > amount)
					{
						result.Width -= amount;
					}
					else if(result.Width < 0f && Math.Abs(result.Width) > amount)
					{
						result.Width += amount;
					}
					else
					{
						result.Width = 0f;
					}
					if(result.Height >= 0f && result.Height > amount)
					{
						result.Height -= amount;
					}
					else if(result.Height < 0f && Math.Abs(result.Height) > amount)
					{
						result.Height += amount;
					}
					else
					{
						result.Height = 0f;
					}
				}
			}
			else
			{
				result = new FArea();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Size																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the size of the specified area.
		/// </summary>
		/// <param name="area">
		/// Reference to the area for which the size will be retrieved.
		/// </param>
		/// <returns>
		/// Reference to the size of the specified area, if found. Otherwise,
		/// an empty size.
		/// </returns>
		public static FSize Size(FArea area)
		{
			FSize result = null;

			if(area != null)
			{
				result = new FSize(area.Width, area.Height);
			}
			else
			{
				result = new FSize();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Top																																		*
		//*-----------------------------------------------------------------------*
		private float mTop = 0f;
		/// <summary>
		/// Get/Set the top coordinate.
		/// </summary>
		[JsonIgnore]
		public float Top
		{
			get { return mTop; }
			set
			{
				float original = mTop;

				if(!mReadOnly)
				{
					mTop = value;
					if(original != value)
					{
						OnTopChanged(new FloatEventArgs(value, original));
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TopChanged																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the top value has changed.
		/// </summary>
		public event FloatEventHandler TopChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ToString																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the string representation of this instance.
		/// </summary>
		/// <returns>
		/// Reference to the string representation of this item.
		/// </returns>
		public override string ToString()
		{
			string result = "{" + $"{mLeft},{mTop},{Width},{Height}" + "}";
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
		public static void TransferValues(FArea source, FArea target)
		{
			float originalBottom = 0f;
			float originalLeft = 0f;
			float originalRight = 0f;
			float originalTop = 0f;

			if(source != null && target != null)
			{
				originalBottom = target.mBottom;
				originalLeft = target.mLeft;
				originalRight = target.mRight;
				originalTop = target.mTop;
				target.mBottom = source.mBottom;
				target.mLeft = source.mLeft;
				target.mRight = source.mRight;
				target.mTop = source.mTop;
				if(originalLeft != source.mLeft)
				{
					target.OnLeftChanged(new FloatEventArgs(source.mLeft, originalLeft));
					target.OnLocationChanged(
						new FloatPointEventArgs(
							Location(source), new FVector2(originalLeft, originalTop)));
				}
				if(originalTop != source.mTop)
				{
					target.OnTopChanged(new FloatEventArgs(source.mTop, originalTop));
					target.OnLocationChanged(
						new FloatPointEventArgs(
							Location(source), new FVector2(originalLeft, originalTop)));
				}
				if(originalRight != source.mRight)
				{
					target.OnRightChanged(
						new FloatEventArgs(source.mRight, originalRight));
				}
				if(originalBottom != source.mBottom)
				{
					target.OnBottomChanged(
						new FloatEventArgs(source.mBottom, originalBottom));
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Translate																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Translate the specified area by a relative amount.
		/// </summary>
		/// <param name="area">
		/// The area to be moved.
		/// </param>
		/// <param name="offsetX">
		/// The X offset by which to move the area.
		/// </param>
		/// <param name="offsetY">
		/// The Y offset by which to move the area.
		/// </param>
		public static void Translate(FArea area, float offsetX, float offsetY)
		{
			if(area != null && !area.mReadOnly)
			{
				area.X += offsetX;
				area.Y += offsetY;
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Translate the specified area to the new location.
		/// </summary>
		/// <param name="area">
		/// The area to be moved.
		/// </param>
		/// <param name="offset">
		/// The offset by which to move the area.
		/// </param>
		public static void Translate(FArea area, FVector2 offset)
		{
			if(area != null && !area.mReadOnly && offset != null)
			{
				area.X += offset.X;
				area.Y += offset.Y;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Width																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the width of the area.
		/// </summary>
		[JsonProperty(Order = 2)]
		public float Width
		{
			get { return mRight - mLeft; }
			set
			{
				float original = mRight - mLeft;

				if(!mReadOnly)
				{
					mRight = mLeft + value;
					if(original != value)
					{
						OnWidthChanged(new FloatEventArgs(value, original));
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* WidthChanged																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the width value has changed.
		/// </summary>
		public event FloatEventHandler WidthChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	X																																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the X coordinate.
		/// </summary>
		[JsonProperty(Order = 0)]
		public float X
		{
			get { return mLeft; }
			set
			{
				float dx = value - mLeft;
				float original = mLeft;

				if(!mReadOnly)
				{
					mLeft = value;
					mRight += dx;
					if(original != value)
					{
						OnLocationChanged(
							new FloatPointEventArgs(value, mTop, original, mTop));
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Y																																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the Y coordinate.
		/// </summary>
		[JsonProperty(Order = 1)]
		public float Y
		{
			get { return mTop; }
			set
			{
				float dy = value - mTop;
				float original = mTop;

				if(!mReadOnly)
				{
					mTop = value;
					mBottom += dy;
					if(original != value)
					{
						OnLocationChanged(
							new FloatPointEventArgs(mLeft, value, mLeft, original));
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	FAreaTypeConverter																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Type conversion for FArea that allows editing of child properties in a
	/// property grid.
	/// </summary>
	public class FAreaTypeConverter : TypeConverter
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
