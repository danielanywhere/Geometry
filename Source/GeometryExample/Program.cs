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
using System.Diagnostics;
using System.Linq;
using System.Text;

using Geometry;

using static Geometry.GeometryUtil;

namespace GeometryExample
{
	//*-------------------------------------------------------------------------*
	//*	Program																																	*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Main instance of the GeometryExample application.
	/// </summary>
	public class Program
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		////*-----------------------------------------------------------------------*
		////* GetMatrixString																												*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return the string representation of the provided system numerics
		///// 4x4 matrix.
		///// </summary>
		///// <param name="matrix">
		///// Reference to a 4x4 matrix from System.Numerics.
		///// </param>
		///// <returns>
		///// The string representation of the current value of the provided
		///// matrix.
		///// </returns>
		//private static string GetMatrixString(System.Numerics.Matrix4x4 matrix)
		//{
		//	StringBuilder builder = new StringBuilder();

		//	builder.Append("{ ");
		//	builder.Append($"{matrix.M11:0.000}, ");
		//	builder.Append($"{matrix.M12:0.000}, ");
		//	builder.Append($"{matrix.M13:0.000}, ");
		//	builder.Append($"{matrix.M14:0.000}");
		//	builder.AppendLine(" },");
		//	builder.Append("{ ");
		//	builder.Append($"{matrix.M21:0.000}, ");
		//	builder.Append($"{matrix.M22:0.000}, ");
		//	builder.Append($"{matrix.M23:0.000}, ");
		//	builder.Append($"{matrix.M24:0.000}");
		//	builder.AppendLine(" },");
		//	builder.Append("{ ");
		//	builder.Append($"{matrix.M31:0.000}, ");
		//	builder.Append($"{matrix.M32:0.000}, ");
		//	builder.Append($"{matrix.M33:0.000}, ");
		//	builder.Append($"{matrix.M34:0.000}");
		//	builder.AppendLine(" },");
		//	builder.Append("{ ");
		//	builder.Append($"{matrix.M41:0.000}, ");
		//	builder.Append($"{matrix.M42:0.000}, ");
		//	builder.Append($"{matrix.M43:0.000}, ");
		//	builder.Append($"{matrix.M44:0.000}");
		//	builder.Append(" }");
		//	return builder.ToString();
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestArcBoundingBox																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test the Arc bounding box by validating all 40 possible combinations.
		/// </summary>
		private static void TestArcBoundingBox()
		{
			float angleEnd = 0f;
			float angleStart = 0f;
			FArea area = null;
			FPoint center = new FPoint(0, 0);
			int count = 0;
			int index = 0;
			FPoint[,] points = new FPoint[,]
			{
				{ new FPoint(10f, 10f), new FPoint(11f, 10f) },
				{ new FPoint(10f, 10f), new FPoint(8f, 10f) },
				{ new FPoint(10f, 10f), new FPoint(-10f, 10f) },
				{ new FPoint(10f, 10f), new FPoint(-10f, -10f) },
				{ new FPoint(10f, 10f), new FPoint(10f, -10f) },
				{ new FPoint(-10f, 10f), new FPoint(-11f, 10f) },
				{ new FPoint(-10f, 10f), new FPoint(-8f, 10f) },
				{ new FPoint(-10f, 10f), new FPoint(-10f, -10f) },
				{ new FPoint(-10f, 10f), new FPoint(10f, -10f) },
				{ new FPoint(-10f, 10f), new FPoint(10f, 10f) },
				{ new FPoint(-10f, -10f), new FPoint(-10f, -8f) },
				{ new FPoint(-10f, -10f), new FPoint(-10f, -11f) },
				{ new FPoint(-10f, -10f), new FPoint(10f, -10f) },
				{ new FPoint(-10f, -10f), new FPoint(10f, 10f) },
				{ new FPoint(-10f, -10f), new FPoint(-10f, 10f) },
				{ new FPoint(10f, -10f), new FPoint(10f, -8f) },
				{ new FPoint(10f, -10f), new FPoint(10f, -11f) },
				{ new FPoint(10f, -10f), new FPoint(10f, 10f) },
				{ new FPoint(10f, -10f), new FPoint(-10f, 10f) },
				{ new FPoint(10f, -10f), new FPoint(-10f, -10f) },
			};
			ArcDirectionEnum winding = ArcDirectionEnum.None;

			Console.WriteLine("** Testing arc bounding boxes **");
			count = points.GetLength(0);
			Console.WriteLine("Forward progression...");
			winding = ArcDirectionEnum.Forward;
			for(index = 0; index < count; index++)
			{
				angleStart =
					Trig.RadToDeg(Trig.GetLineAngle(center, points[index, 0]));
				angleEnd =
					Trig.RadToDeg(Trig.GetLineAngle(center, points[index, 1]));
				area = Circle.GetArcBoundingBox(
					center, points[index, 0], points[index, 1], winding);
				Console.Write($" {points[index, 0].X}, {points[index, 0].Y} -> ");
				Console.Write($"{points[index, 1].X}, {points[index, 1].Y}: ");
				Console.Write($"{angleStart:0.###}deg - {angleEnd:0.###}deg: ");
				Console.Write($"L:{area.Left:0.###}, T:{area.Top:0.###}, ");
				Console.Write($"R:{area.Right:0.###}, B:{area.Bottom:0.###}: ");
				Console.WriteLine($"W:{area.Width:0.###}, H:{area.Height:0.###}");
			}
			Console.WriteLine("Reverse progression...");
			winding = ArcDirectionEnum.Reverse;
			for(index = 0; index < count; index++)
			{
				angleStart =
					Trig.RadToDeg(Trig.GetLineAngle(center, points[index, 0]));
				angleEnd =
					Trig.RadToDeg(Trig.GetLineAngle(center, points[index, 1]));
				area = Circle.GetArcBoundingBox(
					center, points[index, 0], points[index, 1], winding);
				Console.Write($" {points[index, 0].X}, {points[index, 0].Y} -> ");
				Console.Write($"{points[index, 1].X}, {points[index, 1].Y}: ");
				Console.Write($"{angleStart:0.###}deg - {angleEnd:0.###}deg: ");
				Console.Write($"L:{area.Left:0.###}, T:{area.Top:0.###}, ");
				Console.Write($"R:{area.Right:0.###}, B:{area.Bottom:0.###}: ");
				Console.WriteLine($"W:{area.Width:0.###}, H:{area.Height:0.###}");
			}
			Console.WriteLine("");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RandomFloat																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a random single point floating point value within the specified
		/// range.
		/// </summary>
		/// <param name="minimum">
		/// The minimum allowable value in the range.
		/// </param>
		/// <param name="maximum">
		/// The maximum allowable value in the range.
		/// </param>
		/// <returns>
		/// A random floating point value in the requested range.
		/// </returns>
		private static float RandomFloat(float minimum, float maximum)
		{
			Random random = new Random((int)DateTime.Now.Ticks);
			return ConvertRange(0f, 1f, minimum, maximum, random.NextSingle());
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RandomFloatWhole																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a random single point floating point value whole number within
		/// the specified range.
		/// </summary>
		/// <param name="minimum">
		/// The minimum allowable value in the range.
		/// </param>
		/// <param name="maximum">
		/// The maximum allowable value in the range.
		/// </param>
		/// <returns>
		/// A random floating point value in the requested range.
		/// </returns>
		private static float RandomFloatWhole(float minimum, float maximum)
		{
			Random random = new Random((int)DateTime.Now.Ticks);
			return ((float)random.Next((int)minimum, (int)maximum));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Test3DCamera																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test Camera3D operations.
		/// </summary>
		private static void Test3DCamera()
		{

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Test3DLineProjection																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test the screen projection of a 3D line.
		/// </summary>
		private static void Test3DLineProjection()
		{
			Camera3D camera = new Camera3D()
			{
				Position = new FPoint3(18.715f, 11.05f, -9.3556f),
				LookAt = new FPoint3(5f, 0f, -0.36511f)
			};
			FLine line2 = null;
			FLine3 line3 = new FLine3(
				new FPoint3(0f, 0f, 0f),
				new FPoint3(10f, 0f, 0f),
				new FColor4(1f, 1f, 0f, 0f));

			Console.WriteLine("** Testing 3D Line Projection **");

			Console.WriteLine("Camera:");
			Console.WriteLine($"  LookAt:   {camera.LookAt}");
			Console.WriteLine($"  Position: {camera.Position}");
			Console.WriteLine($"  UpAxis:   {camera.UpAxis}");

			Console.WriteLine(" 3D Line:");
			Console.WriteLine($"  {line3.PointA}, ");
			Console.WriteLine($"  {line3.PointB}");

			line2 = camera.ProjectToScreen(line3);
			if(line2 != null)
			{
				Console.WriteLine(" Display Line:");
				Console.WriteLine($"  {line2.PointA}");
				Console.WriteLine($"  {line2.PointB}");
			}
			else
			{
				Console.WriteLine(" Display Line was not in view...");
			}
			Console.WriteLine("");

			Console.WriteLine(" 3D Line:");
			line3 = new FLine3(
				new FPoint3(0f, 0f, 2f),
				new FPoint3(10f, 0f, 2f),
				new FColor4(1f, 1f, 0f, 0f));
			Console.WriteLine($"  {line3.PointA}, ");
			Console.WriteLine($"  {line3.PointB}");

			line2 = camera.ProjectToScreen(line3);
			if(line2 != null)
			{
				Console.WriteLine(" Display Line:");
				Console.WriteLine($"  {line2.PointA}");
				Console.WriteLine($"  {line2.PointB}");
			}
			else
			{
				Console.WriteLine(" Display Line was not in view...");
			}
			Console.WriteLine("");

			Console.WriteLine(" 3D Line:");
			line3 = new FLine3(
				new FPoint3(0f, 0f, 8f),
				new FPoint3(10f, 0f, 8f),
				new FColor4(1f, 1f, 0f, 0f));
			Console.WriteLine($"  {line3.PointA}, ");
			Console.WriteLine($"  {line3.PointB}");

			line2 = camera.ProjectToScreen(line3);
			if(line2 != null)
			{
				Console.WriteLine(" Display Line:");
				Console.WriteLine($"  {line2.PointA}");
				Console.WriteLine($"  {line2.PointB}");
			}
			else
			{
				Console.WriteLine(" Display Line was not in view...");
			}
			Console.WriteLine("");

			Console.WriteLine(" 3D Line:");
			line3 = new FLine3(
				new FPoint3(0f, 1f, 8f),
				new FPoint3(10f, 1f, 8f),
				new FColor4(1f, 1f, 0f, 0f));
			Console.WriteLine($"  {line3.PointA}, ");
			Console.WriteLine($"  {line3.PointB}");

			line2 = camera.ProjectToScreen(line3);
			if(line2 != null)
			{
				Console.WriteLine(" Display Line:");
				Console.WriteLine($"  {line2.PointA}");
				Console.WriteLine($"  {line2.PointB}");
			}
			else
			{
				Console.WriteLine(" Display Line was not in view...");
			}
			Console.WriteLine("");

			Console.WriteLine("");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Test3DProjection																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test basic 3D projection.
		/// </summary>
		/// <remarks>
		/// In this version, the distance of the camera viewfinder from the eye is
		/// calculated from the angle of half of the X field of view and an
		/// opposite distance of 0.5, allowing for a total range of -1 to +1.
		/// X+ is right, Y+ is up, and Z+ is distance from the eye. The x field
		/// of view is 60 degrees and the y field of view 33.75 degrees to
		/// approximate the screen ratio of 1920 x 1080, which is the output
		/// area. The stated camera position and rotational orientation is the
		/// position of the eye directly in world space and the offset of the
		/// viewfinder is considered to be that at which the X field of view
		/// will stretch from -1 to +1. The camera rotates only on the X and Y
		/// axes to prevent view roll.
		/// </remarks>
		private static void Test3DProjection()
		{
			double camHorzDist = 0f;
			FVector3 camLookAt = null;
			FVector3 camPosition = null;
			FVector3 camRotation = null;
			FVector3 direction = null;
			int displayHeight = 1080;
			int displayWidth = 1920;
			float dispH = (float)displayHeight;
			float dispHHalf = dispH / 2f;
			float dispW = (float)displayWidth;
			float dispWHalf = dispW / 2f;
			//float distFar = 10f;
			//float distNear = 1f;
			float distViewfinder = 0f;
			float fovX = 60f;
			float fovY = 0f;
			FVector3 projectedPoint = null;
			float ratio = 0f;
			FVector3 sightAngle = null;
			FVector3 subject = null;
			double subjHorzDist = 0f;
			float viewDown = 0f;
			float viewLeft = 0f;
			float viewRight = 0f;
			float viewUp = 0f;
			float vXHalf = 0f;
			float vYHalf = 0f;
			int x = 0;
			int y = 0;

			Console.WriteLine("** Testing Direct World Projection **");

			ratio = (float)displayHeight / (float)displayWidth;
			fovY = fovX * ratio;
			vXHalf = Trig.DegToRad(fovX / 2f);
			vYHalf = Trig.DegToRad(fovY / 2f);
			distViewfinder = Trig.GetLineAdjFromAngOpp(vXHalf, 0.5f);

			camPosition = new FVector3(10f, 6f, -10f);
			camLookAt = new FVector3(6.5f, 0f, 0.5f);

			//	Rotate the camera to look at the LookAt value.
			direction = camLookAt - camPosition;
			camHorzDist = (float)Math.Sqrt(
				(double)direction.X * (double)direction.X +
				(double)direction.Z * (double)direction.Z);
			camRotation = new FVector3(
				(float)Math.Atan2((double)direction.Y, camHorzDist),
				(float)Math.Atan2((double)direction.X, (double)direction.Z), 0f);
			//	Prepare the viewport edges for quick comparison.
			viewLeft = camRotation.Y - vXHalf;
			viewRight = camRotation.Y + vXHalf;
			viewUp = camRotation.X + vYHalf;
			viewDown = camRotation.X - vYHalf;

			Console.WriteLine($" Camera Position: {camPosition}");
			Console.WriteLine($" Camera Look at:  {camLookAt}");
			Console.WriteLine($" Camera Rotation: {camRotation}");

			subject = new FVector3(0f, 0f, 0f);
			Console.WriteLine($" Subject:         {subject}");

			direction = subject - camPosition;
			subjHorzDist = Math.Sqrt(
				(double)direction.X * (double)direction.X +
				(double)direction.Z * (double)direction.Z);
			sightAngle = new FVector3(
				(float)Math.Atan2((double)direction.Y, subjHorzDist),
				(float)Math.Atan2((double)direction.X, (double)direction.Z), 0f);

			Console.WriteLine($"  Ortho dist:{direction}");
			Console.WriteLine($"  Sight raw: {sightAngle}");
			sightAngle.X -= camRotation.X;
			sightAngle.Y -= camRotation.Y;
			Console.WriteLine($"  Sight fix: {sightAngle}");

			////	In the sight angle, the rotation on X relates to the position on Y.
			//if(sightAngle.X >= viewDown && sightAngle.X <= viewUp &&
			//	sightAngle.Y >= viewLeft && sightAngle.Y <= viewRight)
			//{
			//	//	The subject is in view.
			//	Console.WriteLine("  Subject in view.");

				//	Project the point to the viewfinder.
				projectedPoint = new FVector3()
				{
					//	X position from rotation on Y.
					X = Trig.GetLineOppFromAngAdj(sightAngle.Y, distViewfinder),
					//	Y position from rotation on X.
					Y = Trig.GetLineOppFromAngAdj(sightAngle.X, distViewfinder)
				};
				Console.WriteLine($"  Projected: {projectedPoint}");
				//	Convert the projected point to the screen.
				x = (int)(dispWHalf + (dispWHalf * projectedPoint.X));
				y = (int)(dispHHalf + (dispHHalf * -projectedPoint.Y));
				Console.WriteLine($"  Screen:    {x}, {y}");
			//}
			//else
			//{
			//	Console.WriteLine("  Subject not in view.");
			//}
			Console.WriteLine("");

			subject = new FVector3(10f, 0f, 0f);
			Console.WriteLine($" Subject:         {subject}");

			direction = subject - camPosition;
			subjHorzDist = Math.Sqrt(
				(double)direction.X * (double)direction.X +
				(double)direction.Z * (double)direction.Z);
			sightAngle = new FVector3(
				(float)Math.Atan2((double)direction.Y, (double)direction.Z),
				(float)Math.Atan2((double)direction.X, (double)direction.Z),
				0f);

			Console.WriteLine($"  Ortho dist:{direction}");
			Console.WriteLine($"  Sight raw: {sightAngle}");
			sightAngle.X -= camRotation.X;
			sightAngle.Y -= camRotation.Y;
			Console.WriteLine($"  Sight fix: {sightAngle}");

			//	In the sight angle, the rotation on X relates to the position on Y.
			//if(sightAngle.X >= viewDown && sightAngle.X <= viewUp &&
			//	sightAngle.Y >= viewLeft && sightAngle.Y <= viewRight)
			//{
			//	//	The subject is in view.
			//	Console.WriteLine("  Subject in view.");

				//	Project the point to the viewfinder.
				projectedPoint = new FVector3()
				{
					//	X position from rotation on Y.
					X = Trig.GetLineOppFromAngAdj(sightAngle.Y, distViewfinder),
					//	Y position from rotation on X.
					Y = Trig.GetLineOppFromAngAdj(sightAngle.X, distViewfinder)
				};
				Console.WriteLine($"  Projected: {projectedPoint}");
				//	Convert the projected point to the screen.
				x = (int)(dispWHalf + (dispWHalf * projectedPoint.X));
				y = (int)(dispHHalf + (dispHHalf * -projectedPoint.Y));
				Console.WriteLine($"  Screen:    {x}, {y}");
			//}
			//else
			//{
			//	Console.WriteLine("  Subject not in view.");
			//}

			Console.WriteLine("");



		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestEllipseCoordinateByAngle																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test the Ellipse edge coordinate by a few sample angles.
		/// </summary>
		private static void TestEllipseCoordinateByAngle()
		{
			float angle = 0f;
			FArea box = null;
			FPoint coordinate = null;
			FEllipse ellipse = new FEllipse(
				RandomFloatWhole(0f + 300f, 1920f - 300f),
				RandomFloatWhole(0f + 300f, 1080f - 300f),
				RandomFloatWhole(50f, 300f),
				RandomFloatWhole(50f, 300f));
			int index = 0;
			Random random = new Random((int)DateTime.Now.Ticks);

			box = FEllipse.BoundingBox(ellipse);

			while(Math.Abs(ellipse.RadiusY - ellipse.RadiusX) < 40f)
			{
				ellipse.RadiusX = RandomFloatWhole(50f, 300f);
				ellipse.RadiusY = RandomFloatWhole(50f, 300f);
			}

			Console.WriteLine("** Testing Ellipse Coordinate by Angle **");
			Console.WriteLine(" Ellipse: " +
				$"{ellipse.Center.X:0.###}, {ellipse.Center.Y:0.###}, " +
				$"{ellipse.RadiusX:0.###}, {ellipse.RadiusY:0.###}");
			Console.WriteLine(" Bounding Box: " +
				$"{box.Left:0.###}, {box.Top:0.###}, " +
				$"{box.Right:0.###}, {box.Bottom:0.###}");

			for(index = 1; index < 11; index ++)
			{
				angle = random.NextSingle() * GeometryUtil.TwoPi;
				coordinate = FEllipse.GetCoordinateAtAngle(ellipse, angle);
				Console.WriteLine($"  Angle {index}: " +
					$"{Trig.RadToDeg(angle)}deg. " +
					$"X:{coordinate.X:0.###}, Y:{coordinate.Y:0.###}");
			}

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestEllipseLineIntersection																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test the Ellipse / Line intersection.
		/// </summary>
		private static void TestEllipseLineIntersection()
		{
			FPoint center = new FPoint(303.786f, 226.643f);
			FPoint[] intersections = null;
			FLine line = new FLine(new FPoint()
			{
				X = 560.000f,
				Y = 82.857f
			}, new FPoint()
			{
				X = 654.286f,
				Y = 220.000f
			});
			float radiusX = 228.143f;
			float radiusY = 125.2855f;

			Console.WriteLine("** Testing Ellipse / Line Intersection **");
			Console.WriteLine("Test 1 of 3");
			Console.WriteLine($" Center: {center.X}, {center.Y}");
			Console.WriteLine($" Radius: {radiusX}, {radiusY}");
			Console.WriteLine(" Line:");
			Console.WriteLine($"  Point A: {line.PointA.X}, {line.PointA.Y}");
			Console.WriteLine($"  Point B: {line.PointB.X}, {line.PointB.Y}");
			intersections =
				FEllipse.FindIntersections(center, radiusX, radiusY, line);
			if(intersections.Length == 0)
			{
				Console.WriteLine("No intersections found...");
			}
			else
			{
				Console.WriteLine(" Intersections:");
				foreach(FPoint pointItem in intersections)
				{
					Console.WriteLine($"  {pointItem.X}, {pointItem.Y}");
				}
			}
			Console.WriteLine("Test 2 of 3");
			center.X = 1278.786f;
			center.Y = 218.786f;
			line.PointA.X = center.X;
			line.PointA.Y = center.Y;
			line.PointB.X = 1374.072f;
			line.PointB.Y = 356.929f;
			Console.WriteLine($" Center: {center.X}, {center.Y}");
			Console.WriteLine($" Radius: {radiusX}, {radiusY}");
			Console.WriteLine(" Line:");
			Console.WriteLine($"  Point A: {line.PointA.X}, {line.PointA.Y}");
			Console.WriteLine($"  Point B: {line.PointB.X}, {line.PointB.Y}");
			intersections =
				FEllipse.FindIntersections(center, radiusX, radiusY, line);
			if(intersections.Length == 0)
			{
				Console.WriteLine("No intersections found...");
			}
			else
			{
				Console.WriteLine(" Intersections:");
				foreach(FPoint pointItem in intersections)
				{
					Console.WriteLine($"  {pointItem.X}, {pointItem.Y}");
				}
			}
			Console.WriteLine("Test 3 of 3");
			center.X = 300.958f;
			center.Y = 620.826f;
			line.PointA.X = 347.415f;
			line.PointA.Y = 473.334f;
			line.PointB.X = 489.178f;
			line.PointB.Y = 730.685f;
			Console.WriteLine($" Center: {center.X}, {center.Y}");
			Console.WriteLine($" Radius: {radiusX}, {radiusY}");
			Console.WriteLine(" Line:");
			Console.WriteLine($"  Point A: {line.PointA.X}, {line.PointA.Y}");
			Console.WriteLine($"  Point B: {line.PointB.X}, {line.PointB.Y}");
			intersections =
				FEllipse.FindIntersections(center, radiusX, radiusY, line);
			if(intersections.Length == 0)
			{
				Console.WriteLine("No intersections found...");
			}
			else
			{
				Console.WriteLine(" Intersections:");
				foreach(FPoint pointItem in intersections)
				{
					Console.WriteLine($"  {pointItem.X}, {pointItem.Y}");
				}
			}
			Console.WriteLine("");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestFLineIntersect																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test the FLine Intersect method.
		/// </summary>
		private static void TestFLineIntersect()
		{
			FLine line1 = new FLine(
				new FPoint(1301.75f, 609.6f),
				new FPoint(1301.75f, 158.485f));
			FLine line2 = new FLine(
				new FPoint(1301.75f, 160.0725f),
				new FPoint(1428.485f, 160.0725f));
			FPoint location = null;

			Console.WriteLine("** Testing FLine Intersect **");
			Console.WriteLine(" Line 1");
			Console.WriteLine($"  Point A: {line1.PointA.X}, {line1.PointA.Y}");
			Console.WriteLine($"  Point B: {line1.PointB.X}, {line1.PointB.Y}");
			Console.WriteLine(" Line 2");
			Console.WriteLine($"  Point A: {line2.PointA.X}, {line2.PointA.Y}");
			Console.WriteLine($"  Point B: {line2.PointB.X}, {line2.PointB.Y}");
			Console.WriteLine("Expecting: 1301.75, 160.0725");
			location = FLine.Intersect(line1, line2, true);
			Console.WriteLine($"Actual:    {location.X}, {location.Y}");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestFLineTranslateVector																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test the FLine TranslateVector method.
		/// </summary>
		private static void TestFLineTranslateVector()
		{
			FLine line = new FLine(
				new FPoint(
					RandomFloatWhole(0f, 1920f),
					RandomFloatWhole(0f, 1080f)),
				new FPoint(
					RandomFloatWhole(0f, 1920f),
					RandomFloatWhole(0f, 1080f)));

			Console.WriteLine("** Testing FLine TranslateVector **");
			Console.WriteLine(" Start");
			Console.WriteLine($"  Point A: {line.PointA.X:0}, {line.PointA.Y:0}");
			Console.WriteLine($"  Point B: {line.PointB.X:0}, {line.PointB.Y:0}");

			FLine.TranslateVector(line, 10f, ArcDirectionEnum.Increasing);
			Console.WriteLine(" End");
			Console.WriteLine($"  Point A: {line.PointA.X:0}, {line.PointA.Y:0}");
			Console.WriteLine($"  Point B: {line.PointB.X:0}, {line.PointB.Y:0}");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestMatrix3																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test FMatrix3 operations.
		/// </summary>
		private static void TestMatrix3()
		{

			FPoint point = new FPoint(
				RandomFloatWhole(0f, 1920f),
				RandomFloatWhole(0f, 1080f));
			FPoint3 point3 = new FPoint3(
				RandomFloat(-1f, 1f),
				RandomFloat(-5f, 5f),
				0.1f
				);

			Console.WriteLine("** Testing FMatrix3 **");
			Console.WriteLine("Transform view with top left anchor to bottom left.");
			Console.WriteLine(
				$" Starting point (top left anchor): {point.X:0}, {point.Y:0}");

			//	Flip Y.
			point = FMatrix3.Scale(point, new FPoint(1f, -1f));
			//	Reposition Y.
			point = FMatrix3.Translate(point, new FPoint(0f, 1080f));

			Console.WriteLine(
				$" Transformed point (bottom left anchor): {point.X:0}, {point.Y:0}");

			Console.WriteLine("");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestMatrix4																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test FMatrix 4 operations.
		/// </summary>
		private static void TestMatrix4()
		{
			FMatrix4 matrix = null;
			FVector3 vectorSource = new FVector3(1f, 0f, 0f);
			FVector3 vectorTarget = null;

			Console.WriteLine("** Testing FMatrix4 **");

			Console.WriteLine($"Vector Source: {vectorSource}");

			Console.WriteLine(" Test Rotate Source Z: 45deg");
			matrix = FMatrix4.GetRotationMatrixYawPitchRoll(0f, 0f,
				Trig.DegToRad(45f), AxisType.Z);
			vectorTarget = FMatrix4.Multiply(matrix, vectorSource);
			Console.WriteLine(" Blender: 0.707, 0.707, 0.000...");
			Console.WriteLine($" Rotated value: {vectorTarget}");

			Console.WriteLine(" Test Rotate Source Y: 45deg");
			matrix = FMatrix4.GetRotationMatrixYawPitchRoll(0f,
				Trig.DegToRad(45f), 0f, AxisType.Z);
			vectorTarget = FMatrix4.Multiply(matrix, vectorSource);
			Console.WriteLine(" Blender: 0.707, 0.000, -0.707...");
			Console.WriteLine($" Rotated value: {vectorTarget}");

			Console.WriteLine(" Test Rotate Source X: 45deg");
			matrix = FMatrix4.GetRotationMatrixYawPitchRoll(
				Trig.DegToRad(45f), 0f, 0f, AxisType.Z);
			vectorTarget = FMatrix4.Multiply(matrix, vectorSource);
			Console.WriteLine(" Blender: 1.000, 0.000, 0.000...");
			Console.WriteLine($" Rotated value: {vectorTarget}");

			Console.WriteLine(" Test Compound Rotation (Z-up: Z, X, Y, 45, 45, 45)");
			matrix = FMatrix4.GetRotationMatrixYawPitchRoll(
				Trig.DegToRad(45f), Trig.DegToRad(45f), Trig.DegToRad(45f),
				AxisType.Z);
			vectorTarget = FMatrix4.Multiply(matrix, vectorSource);
			Console.WriteLine(" Blender: 0.500, 0.500, -0.707...");
			Console.WriteLine($" Rotated value: {vectorTarget}");
			Console.WriteLine("You can that on this rotation in Blender, " +
				"there is no movement on X, indicating that the order is X, Y, Z");
			Console.WriteLine(
				"To test this hyptothesis, rotation values of 10, 20, 30 were " +
				"used and the FMatrix4.GetRotationMatrix was set\r\n" +
				"to XYZ order...");

			Console.WriteLine(" Test Compound Rotation (Z-up: X, Y, Z, 10, 20, 30)");
			matrix = FMatrix4.GetRotationMatrix(
				Trig.DegToRad(10f), Trig.DegToRad(20f), Trig.DegToRad(30f),
				AxisOrderEnum.XYZ);
			vectorTarget = FMatrix4.Multiply(matrix, vectorSource);
			Console.WriteLine(" Blender: 0.814, 0.470, -0.342...");
			Console.WriteLine($" Rotated value: {vectorTarget}");

			vectorSource = new FVector3(25f, 30f, 40f);
			Console.WriteLine($"Vector Source: {vectorSource}");

			Console.WriteLine(" Test Scale (10, 20, 30)");
			matrix = FMatrix4.GetScaleMatrix(10f, 20, 30f);
			vectorTarget = FMatrix4.Multiply(matrix, vectorSource);
			Console.WriteLine(" Expected value: 250.000, 600.000, 1200.000");
			Console.WriteLine($" Scaled value: {vectorTarget}");

			vectorSource = vectorTarget;
			Console.WriteLine($"Vector Source: {vectorSource}");

			Console.WriteLine(" Test Translation (-110, -177, 222)");
			matrix = FMatrix4.GetTranslationMatrix(-110f, -177f, 222f);
			vectorTarget = FMatrix4.Multiply(matrix, vectorSource);
			Console.WriteLine(" Expected value: 140.000, 423.000, 1422.000");
			Console.WriteLine($" Translated value: {vectorTarget}");

			Console.WriteLine(" Test determinant of:");
			Console.WriteLine("  4,  3,  2, 2");
			Console.WriteLine("  0,  1, -3, 3");
			Console.WriteLine("  0, -1,  3, 3");
			Console.WriteLine("  0,  3,  1, 1");
			matrix = new FMatrix4(new float[,]
			{
				{ 4f, 3f, 2f, 2f },
				{ 0f, 1f, -3f, 3f },
				{ 0f, -1f, 3f, 3f },
				{ 0f, 3f, 1f, 1f }
			});
			Console.WriteLine(" Expected: -240");
			Console.WriteLine($" Determinant: {FMatrix4.GetDeterminant(matrix)}");
			Console.WriteLine("");

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestShapeVertices																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test the vertices of some shapes.
		/// </summary>
		private static void TestShapeVertices()
		{
			FArea area = null;
			FEllipse ellipse = null;
			FLine line = null;
			FPath path = null;
			FPoint point = null;
			List<FPoint> points = null;

			Console.WriteLine("** Testing Shape Vertices **");

			//	Area.
			area = new FArea(314.550f, 343.889f, 466.488f, 236.685f);
			Console.WriteLine($" Area: {area.Left:0.###}, {area.Top:0.###}, " +
				$"{area.Width:0.###}, {area.Height:0.###}. Rotation: 15.5deg.");
			points = FArea.GetVertices(area, Trig.DegToRad(15.5f));
			foreach(FPoint pointItem in points)
			{
				Console.WriteLine($"  {pointItem.X:0.###}, {pointItem.Y:0.###}");
			}
			//	Ellipse.
			ellipse = new FEllipse(1157.1425f, 228.5715f,
				468.571f / 2f, 222.857f / 2f);
			Console.WriteLine(
				$" Ellipse: {ellipse.Center.X:0.###}, {ellipse.Center.Y:0.###}, " +
				$"{ellipse.RadiusX:0.###}, {ellipse.RadiusY:0.###}. " +
				"Rotation: -32deg.");
			points = FEllipse.GetVertices(ellipse, 36, Trig.DegToRad(-32));
			foreach(FPoint pointItem in points)
			{
				Console.WriteLine($"  {pointItem.X:0.###}, {pointItem.Y:0.###}");
			}
			//	Line.
			line = new FLine(
				new FPoint(1180.000f, 632.857f), new FPoint(1462.857f, 632.857f));
			Console.WriteLine(
				$" Line: {line.PointA.X:0.###}, {line.PointA.Y:0.###}, " +
				$"{line.PointB.X:0.###}, {line.PointB.Y:0.###}. Rotation: 52.17deg.");
			points = FLine.GetVertices(line, Trig.DegToRad(52.17f));
			foreach(FPoint pointItem in points)
			{
				Console.WriteLine($"  {pointItem.X:0.###}, {pointItem.Y:0.###}");
			}
			//	Path.
			path = new FPath()
			{
				new FPoint(1507.143f, 352.857f),
				new FPoint(1767.143f, 755.714f),
				new FPoint(1595.714f, 302.857f)
			};
			point = FPath.GetCenter(path);
			Console.WriteLine(" Path. " +
				$"Center: {point.X:0.###}, {point.Y:0.###}. Rotation: 12deg.:");
			foreach(FPoint pointItem in path)
			{
				Console.WriteLine($"  {pointItem.X:0.###}, {pointItem.Y:0.###}");
			}
			points = FPath.GetVertices(path, Trig.DegToRad(12f));
			Console.WriteLine("  Points:");
			foreach(FPoint pointItem in points)
			{
				Console.WriteLine($"   {pointItem.X:0.###}, {pointItem.Y:0.###}");
			}
			Console.WriteLine("");
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Main																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Configure and run the application.
		/// </summary>
		public static void Main(string[] args)
		{
			GeometryExampleActionType action = GeometryExampleActionType.None;
			bool bShowHelp = false; //	Flag - Explicit Show Help.
			string key = "";        //	Current Parameter Key.
			string lowerArg = "";   //	Current Lowercase Argument.
			StringBuilder message = new StringBuilder();
			Program prg = new Program();  //	Initialized instance.
			string text = "";

			Console.WriteLine("GeometryExample.exe");
			foreach(string arg in args)
			{
				lowerArg = arg.ToLower();
				key = "/?";
				if(lowerArg == key)
				{
					bShowHelp = true;
					continue;
				}
				key = "/action:";
				if(lowerArg.StartsWith(key))
				{
					text = arg.Substring(key.Length);
					if(Enum.TryParse<GeometryExampleActionType>(
						text, true, out action))
					{
						prg.mAction = action;
					}
					else
					{
						message.AppendLine($"Unrecognized action: {text}");
						bShowHelp = true;
					}
					continue;
				}
				key = "/controlpoint:";
				if(lowerArg.StartsWith(key))
				{
					prg.mControlPoint = FPoint.Parse(arg.Substring(key.Length), true);
					continue;
				}
				key = "/count:";
				if(lowerArg.StartsWith(key))
				{
					prg.mCount = ToInt(arg.Substring(key.Length));
					continue;
				}
				key = "/endpoint:";
				if(lowerArg.StartsWith(key))
				{
					prg.mEndPoint = FPoint.Parse(arg.Substring(key.Length), true);
					continue;
				}
				key = "/startpoint:";
				if(lowerArg.StartsWith(key))
				{
					prg.mStartPoint = FPoint.Parse(arg.Substring(key.Length), true);
					continue;
				}
				key = "/wait";
				if(lowerArg.StartsWith(key))
				{
					prg.mWaitAfterEnd = true;
					continue;
				}
			}
			if(!bShowHelp)
			{
				switch(prg.mAction)
				{
					case GeometryExampleActionType.QuadraticBezierPlotPoints:
					case GeometryExampleActionType.QuadraticBezierPlotPointsEquidistant:
						if(prg.mStartPoint == null)
						{
							message.Append("/startPoint is required for the ");
							message.AppendLine("QuadraticBezierPlotPoints action.");
							bShowHelp = true;
						}
						if(prg.mControlPoint == null)
						{
							message.Append("/controlPoint is required for the ");
							message.AppendLine("QuadraticBezierPlotPoints action.");
							bShowHelp = true;
						}
						if(prg.mEndPoint == null)
						{
							message.Append("/endPoint is required for the ");
							message.AppendLine("QuadraticBezierPlotPoints action.");
							bShowHelp = true;
						}
						if(prg.mCount == 0)
						{
							message.Append("A non-zero /count is required for the ");
							message.AppendLine("QuadraticBezierPlotPoints action.");
							bShowHelp = true;
						}
						break;
					case GeometryExampleActionType.None:
					default:
						break;
				}
			}
			if(bShowHelp)
			{
				//	Display Syntax.
				Console.WriteLine(message.ToString() + "\r\n" + ResourceMain.Syntax);
			}
			else
			{
				//	Run the configured application.
				prg.Run();
			}
			if(prg.mWaitAfterEnd)
			{
				Console.WriteLine("Press [Enter] to exit...");
				Console.ReadLine();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Action																																*
		//*-----------------------------------------------------------------------*
		private GeometryExampleActionType mAction = GeometryExampleActionType.None;
		/// <summary>
		/// Get/Set the action to execute for this session.
		/// </summary>
		public GeometryExampleActionType Action
		{
			get { return mAction; }
			set { mAction = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ControlPoint																													*
		//*-----------------------------------------------------------------------*
		private FPoint mControlPoint = null;
		/// <summary>
		/// Get/Set a reference to the control point.
		/// </summary>
		public FPoint ControlPoint
		{
			get { return mControlPoint; }
			set { mControlPoint = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Count																																	*
		//*-----------------------------------------------------------------------*
		private int mCount = 0;
		/// <summary>
		/// Get/Set the count of items or iterations to process.
		/// </summary>
		public int Count
		{
			get { return mCount; }
			set { mCount = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	EndPoint																															*
		//*-----------------------------------------------------------------------*
		private FPoint mEndPoint = null;
		/// <summary>
		/// Get/Set a reference to the end point.
		/// </summary>
		public FPoint EndPoint
		{
			get { return mEndPoint; }
			set { mEndPoint = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Run																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Run the configured application.
		/// </summary>
		public void Run()
		{
			int currentCol = 0;
			FPoint coordinate = null;
			List<FPoint> points = null;
			float time = 0f;
			int index = 0;

			//	Test FMatrix3 operations.
			TestMatrix3();

			//	Test FMatrix4 operations.
			TestMatrix4();

			//	Test basic 3D projection.
			Test3DProjection();

			//	Test 3D line projection.
			Test3DLineProjection();

			//	Test Shape Vertices.
			TestShapeVertices();

			//	Test Ellipse Coordinate by Angle.
			TestEllipseCoordinateByAngle();

			//	Test Ellipse / Line Intersection.
			TestEllipseLineIntersection();

			//	Test FLine Intersection.
			TestFLineIntersect();

			//	Test FLine TranslateVector.
			TestFLineTranslateVector();

			//	Test Arc bounding boxes.
			TestArcBoundingBox();

			Console.WriteLine($"**{mAction}**");
			switch(mAction)
			{
				case GeometryExampleActionType.QuadraticBezierPlotPoints:
					currentCol = 0;
					for(index = 0; index <= mCount; index ++)
					{
						if(index > 0)
						{
							Console.Write(", ");
							if(currentCol > 3)
							{
								Console.WriteLine("");
								currentCol = 0;
							}
						}
						time = (float)index / (float)mCount;
						coordinate = Bezier.GetQuadraticCurvePoint(
							mStartPoint, mControlPoint, mEndPoint, time);
						Console.Write($"[{coordinate.X:0.###}, {coordinate.Y:0.###}]");
						currentCol++;
					}
					break;
				case GeometryExampleActionType.QuadraticBezierPlotPointsEquidistant:
					points = Bezier.GetQuadraticCurvePointsEquidistant(mStartPoint,
						mControlPoint, mEndPoint, mCount);
					currentCol = 0;
					foreach(FPoint pointItem in points)
					{
						if(index > 0)
						{
							Console.Write(", ");
							if(currentCol > 3)
							{
								Console.WriteLine("");
								currentCol = 0;
							}
						}
						Console.Write($"[{pointItem.X:0.###}, {pointItem.Y:0.###}]");
						currentCol++;
						index++;
					}
					break;
			}
			Console.WriteLine("");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	StartPoint																														*
		//*-----------------------------------------------------------------------*
		private FPoint mStartPoint = null;
		/// <summary>
		/// Get/Set a reference to the start point.
		/// </summary>
		public FPoint StartPoint
		{
			get { return mStartPoint; }
			set { mStartPoint = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	WaitAfterEnd																													*
		//*-----------------------------------------------------------------------*
		private bool mWaitAfterEnd = false;
		/// <summary>
		/// Get/Set a value indicating whether to wait for user keypress after 
		/// processing has completed.
		/// </summary>
		public bool WaitAfterEnd
		{
			get { return mWaitAfterEnd; }
			set { mWaitAfterEnd = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*


}
