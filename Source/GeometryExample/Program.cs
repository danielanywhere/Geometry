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
using System.Threading;
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
			FVector2 center = new FVector2(0, 0);
			int count = 0;
			int index = 0;
			FVector2[,] points = new FVector2[,]
			{
				{ new FVector2(10f, 10f), new FVector2(11f, 10f) },
				{ new FVector2(10f, 10f), new FVector2(8f, 10f) },
				{ new FVector2(10f, 10f), new FVector2(-10f, 10f) },
				{ new FVector2(10f, 10f), new FVector2(-10f, -10f) },
				{ new FVector2(10f, 10f), new FVector2(10f, -10f) },
				{ new FVector2(-10f, 10f), new FVector2(-11f, 10f) },
				{ new FVector2(-10f, 10f), new FVector2(-8f, 10f) },
				{ new FVector2(-10f, 10f), new FVector2(-10f, -10f) },
				{ new FVector2(-10f, 10f), new FVector2(10f, -10f) },
				{ new FVector2(-10f, 10f), new FVector2(10f, 10f) },
				{ new FVector2(-10f, -10f), new FVector2(-10f, -8f) },
				{ new FVector2(-10f, -10f), new FVector2(-10f, -11f) },
				{ new FVector2(-10f, -10f), new FVector2(10f, -10f) },
				{ new FVector2(-10f, -10f), new FVector2(10f, 10f) },
				{ new FVector2(-10f, -10f), new FVector2(-10f, 10f) },
				{ new FVector2(10f, -10f), new FVector2(10f, -8f) },
				{ new FVector2(10f, -10f), new FVector2(10f, -11f) },
				{ new FVector2(10f, -10f), new FVector2(10f, 10f) },
				{ new FVector2(10f, -10f), new FVector2(-10f, 10f) },
				{ new FVector2(10f, -10f), new FVector2(-10f, -10f) },
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
				Position = new FVector3(18.715f, 11.05f, -9.3556f),
				LookAt = new FVector3(5f, 0f, -0.36511f)
			};
			FLine line2 = null;
			FLine3 line3 = new FLine3(
				new FVector3(0f, 0f, 0f),
				new FVector3(10f, 0f, 0f),
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
				new FVector3(0f, 0f, 2f),
				new FVector3(10f, 0f, 2f),
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
				new FVector3(0f, 0f, 8f),
				new FVector3(10f, 0f, 8f),
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
				new FVector3(0f, 1f, 8f),
				new FVector3(10f, 1f, 8f),
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
			Camera3D camera = null;
			int index = 0;
			FVector2 point2 = null;
			List<FVector3> points = new List<FVector3>();
			int row = 0;
			int rowCount = 0;

			Console.WriteLine("** Testing Direct World Projection **");
			Console.WriteLine(" Basic Camera Test");
			camera = new Camera3D()
			{
				DisplayWidth = 690,
				DisplayHeight = 359,
				LookAt = new FVector3()
			};
			camera.Position = new FVector3(1500f, 1500f, -1000f);

			float[,] items = new float[,]
			{
			{-1301.750f,0.000f,609.600f},
			{-1301.750f,0.000f,-609.600f},
			{1301.750f,0.000f,-609.600f},
			{1301.750f,0.000f,609.600f},

			{-1301.750f,0.010f,609.600f},
			{1301.750f,0.010f,609.600f},
			{-1301.750f,0.010f,457.200f},
			{1301.750f,0.010f,457.200f},
			{-1301.750f,0.010f,304.800f},
			{1301.750f,0.010f,304.800f},
			{-1301.750f,0.010f,152.400f},
			{1301.750f,0.010f,152.400f},
			{-1301.750f,0.010f,0.000f},
			{1301.750f,0.010f,0.000f},
			{-1301.750f,0.010f,-152.400f},
			{1301.750f,0.010f,-152.400f},
			{-1301.750f,0.010f,-304.800f},
			{1301.750f,0.010f,-304.800f},
			{-1301.750f,0.010f,-457.200f},
			{1301.750f,0.010f,-457.200f},
			{-1301.750f,0.010f,-609.600f},
			{1301.750f,0.010f,-609.600f},

			{-1301.750f,0.010f,609.600f},
			{-1301.750f,0.010f,-609.600f},
			{-976.313f,0.010f,609.600f},
			{-976.313f,0.010f,-609.600f},
			{-650.875f,0.010f,609.600f},
			{-650.875f,0.010f,-609.600f},
			{-325.438f,0.010f,609.600f},
			{-325.438f,0.010f,-609.600f},
			{0.000f,0.010f,609.600f},
			{0.000f,0.010f,-609.600f},
			{325.438f,0.010f,609.600f},
			{325.438f,0.010f,-609.600f},
			{650.875f,0.010f,609.600f},
			{650.875f,0.010f,-609.600f},
			{976.313f,0.010f,609.600f},
			{976.313f,0.010f,-609.600f},
			{1301.750f,0.010f,609.600f},
			{1301.750f,0.010f,-609.600f}

			};

			points.Clear();
			rowCount = items.GetLength(0);
			for(row = 0; row < rowCount; row ++)
			{
				points.Add(new FVector3(items[row, 0], items[row, 1], items[row, 2]));
			}

			index = 0;
			foreach(FVector3 pointItem in points)
			{
				point2 = camera.ProjectToScreen(pointItem);
				Console.WriteLine($" {index.ToString().PadLeft(2, '0')}. " +
					$"{point2.X:0}, {point2.Y:0}");
				index++;
			}
			Console.WriteLine("");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Test3DRotationMode																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test and compare results of setting the camera by LookAt and
		/// Euler rotation modes.
		/// </summary>
		private static void Test3DRotationMode()
		{
			Camera3D camera = new Camera3D();
			FVector3 storedRotation = null;

			Console.WriteLine("** Testing 3D Rotation Mode **");
			Console.WriteLine(" Mode: Look-at.");
			Console.WriteLine(" Position: 0, 0, 0");
			Console.WriteLine(" Look at: 10, 10, 10");
			camera.RotationMode = ObjectRotationMode.LookAt;
			camera.Position = new FVector3(0f, 0f, 0f);
			camera.LookAt = new FVector3(10f, 10f, 10f);
			storedRotation = camera.Rotation;
			Console.WriteLine($"  Camera distance: {camera.CameraDistance}");
			Console.WriteLine($"  Rotation: {camera.Rotation}");

			Console.WriteLine(" Changing mode to EulerRotation.");
			camera.RotationMode = ObjectRotationMode.EulerRotation;
			Console.WriteLine(" Resetting rotation.");
			camera.Rotation = new FVector3(0f, 0f, 0f);
			Console.WriteLine($"  Look at: {camera.LookAt}");
			Console.WriteLine($"  Set rotation: {storedRotation}");
			camera.Rotation = storedRotation;
			Console.WriteLine($"  Look at: {camera.LookAt}");
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
			FVector2 coordinate = null;
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
			FVector2 center = new FVector2(303.786f, 226.643f);
			FVector2[] intersections = null;
			FLine line = new FLine(new FVector2()
			{
				X = 560.000f,
				Y = 82.857f
			}, new FVector2()
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
				foreach(FVector2 pointItem in intersections)
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
				foreach(FVector2 pointItem in intersections)
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
				foreach(FVector2 pointItem in intersections)
				{
					Console.WriteLine($"  {pointItem.X}, {pointItem.Y}");
				}
			}
			Console.WriteLine("");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestFAreaHasVolume																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test FArea.HasVolume.
		/// </summary>
		private static void TestFAreaHasVolume()
		{
			FArea area = new FArea()
			{
				X = -500f,
				Y = float.MinValue / 2f,
				Width = 240f,
				Height = float.MaxValue
			};

			Console.WriteLine("** Testing FArea HasVolume **");
			Console.WriteLine(
				$" Area: {area.X}, {area.Y}, {area.Width}, {area.Height}");
			Console.WriteLine($" HasVolume: {FArea.HasVolume(area)}");
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
				new FVector2(1301.75f, 609.6f),
				new FVector2(1301.75f, 158.485f));
			FLine line2 = new FLine(
				new FVector2(1301.75f, 160.0725f),
				new FVector2(1428.485f, 160.0725f));
			FVector2 location = null;

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
				new FVector2(
					RandomFloatWhole(0f, 1920f),
					RandomFloatWhole(0f, 1080f)),
				new FVector2(
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

			FVector2 point = new FVector2(
				RandomFloatWhole(0f, 1920f),
				RandomFloatWhole(0f, 1080f));
			FVector3 point3 = new FVector3(
				RandomFloat(-1f, 1f),
				RandomFloat(-5f, 5f),
				0.1f
				);

			Console.WriteLine("** Testing FMatrix3 **");
			Console.WriteLine("Transform view with top left anchor to bottom left.");
			Console.WriteLine(
				$" Starting point (top left anchor): {point.X:0}, {point.Y:0}");

			//	Flip Y.
			point = FMatrix3.Scale(point, new FVector2(1f, -1f));
			//	Reposition Y.
			point = FMatrix3.Translate(point, new FVector2(0f, 1080f));

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
		//* TestQuaternionAdd																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test quaternion addition.
		/// </summary>
		private static void TestQuaternionAdd()
		{
			FQuaternion q1 = new FQuaternion(0.813f, 0.355f, 0.377f, 0.266f);
			FQuaternion q2 = new FQuaternion(0.339f, 0.352f, 0.140f, 0.861f);
			FQuaternion q3 = FQuaternion.Add(q1, q2);

			Console.WriteLine("** Testing Quaternion Addition **");
			Console.WriteLine($"{q1} +\r\n{q2} =\r\n{q3}\r\n");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionClone																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test quaternion clone.
		/// </summary>
		private static void TestQuaternionClone()
		{
			FQuaternion q1 = new FQuaternion(0.596f, 0.007f, 0.380f, 0.707f);
			FQuaternion q2 = FQuaternion.Clone(q1);

			Console.WriteLine("** Testing Quaternion Cloning **");
			Console.WriteLine($"Original:     {q1}");
			Console.WriteLine($"Clone:        {q2}");
			q1.X = 0.5741f;
			q1.Y = 0.6036f;
			q1.Z = 0.3831f;
			q1.W = 0.3992f;
			Console.WriteLine($"Orig Changed: {q1}");
			Console.WriteLine($"Clone:        {q2}\r\n");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionConcatenate																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test quaternion concatenate.
		/// </summary>
		private static void TestQuaternionConcatenate()
		{
			FQuaternion q1 = new FQuaternion(0.610f, 0.357f, 0.538f, 0.460f);
			FQuaternion q2 = new FQuaternion(0.355f, 0.574f, 0.640f, 0.367f);
			FQuaternion q3 = FQuaternion.Concatenate(q1, q2);

			Console.WriteLine("** Testing Quaternion Concatenation **");
			Console.WriteLine($"{q1} cat\r\n{q2} =\r\n{q3}\r\n");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionConjugate																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test quaternion conjugate.
		/// </summary>
		private static void TestQuaternionConjugate()
		{
			FQuaternion q1 = new FQuaternion(0.375f, 0.436f, 0.334f, 0.747f);
			FQuaternion q2 = FQuaternion.Conjugate(q1);

			Console.WriteLine("** Testing Quaternion Conjugation **");
			Console.WriteLine($"{q1} conjugated =\r\n{q2}\r\n");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionDivide																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test quaternion division.
		/// </summary>
		private static void TestQuaternionDivide()
		{
			FQuaternion q1 = new FQuaternion(0.667f, 0.430f, 0.002f, 0.608f);
			FQuaternion q2 = new FQuaternion(0.815f, 0.517f, 0.058f, 0.255f);
			FQuaternion q3 = FQuaternion.Divide(q1, q2);

			Console.WriteLine("** Testing Quaternion Division **");
			Console.WriteLine($"{q1} /\r\n{q2} =\r\n{q3}\r\n");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionDot																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test quaternion dot product.
		/// </summary>
		private static void TestQuaternionDot()
		{
			FQuaternion q1 = new FQuaternion(0.608f, 0.249f, 0.753f, 0.036f);
			FQuaternion q2 = new FQuaternion(0.673f, 0.648f, 0.351f, 0.069f);
			float dot = FQuaternion.Dot(q1, q2);

			Console.WriteLine("** Testing Quaternion Dot Product'ing **");
			Console.WriteLine($"{q1} dot\r\n{q2} =\r\n{dot:0.000}\r\n");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionEquals																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test equality of two quaternions.
		/// </summary>
		private static void TestQuaternionEquals()
		{
			FQuaternion q1 = new FQuaternion(0.503f, 0.557f, 0.620f, 0.231f);
			FQuaternion q2 = new FQuaternion(0.503f, 0.557f, 0.620f, 0.231f);

			Console.WriteLine("** Testing Quaternion Equivalency **");
			Console.WriteLine($"{q1} ==\r\n{q2} ?\r\n{q1.Equals(q2)}\r\n");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionFromAxisAngle																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test new quaternion from axis angle.
		/// </summary>
		private static void TestQuaternionFromAxisAngle()
		{
			float angle = Trig.DegToRad(90f);
			FVector3 v1 = new FVector3(1f, 0f, 0f);
			FVector4 v2 = null;
			FQuaternion q1 = FQuaternion.FromAxisAngle(v1, angle);

			Console.WriteLine("** Testing Quaternion From Axis Angle **");
			Console.WriteLine($"Vector: {v1}, Angle: {angle:0.000} ->\r\n{q1}");
			Console.WriteLine("Expecting above: X:0.707, Y:0, Z:0, W:0.707");
			v2 = FQuaternion.ToAxisAngle(q1);
			Console.WriteLine($"Translated Back (Angle in W): {v2}\r\n");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionFromEuler																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test new quaternion from Tait-Euler rotation.
		/// </summary>
		private static void TestQuaternionFromEuler()
		{
			FVector3 v1 = new FVector3(0f, Trig.DegToRad(45f), Trig.DegToRad(45f));
			FQuaternion q1 = FQuaternion.FromEuler(v1);

			Console.WriteLine("** Testing Quaternion From Euler Rotation (ZYX) **");
			Console.WriteLine($"Vector: {v1} ->\r\n{q1}");
			v1 = FQuaternion.ToEuler(q1);
			Console.WriteLine($"Converted back to Euler: {v1}\r\n");
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* TestQuaternionFromMatrix																							*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Test new quaternion from 4x4 matrix.
		///// </summary>
		//private static void TestQuaternionFromMatrix()
		//{
		//	Console.WriteLine("** Testing Quaternion From Matrix **");
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionFromPitchRollYaw																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test new quaternion from pitch, roll, and yaw (XYZ order).
		/// </summary>
		private static void TestQuaternionFromPitchRollYaw()
		{
			float fP = Trig.DegToRad(45f);
			float fR = Trig.DegToRad(45f);
			float fY = Trig.DegToRad(0f);
			FQuaternion q1 = FQuaternion.FromPitchRollYaw(fP, fR, fY);
			FVector3 v1 = null;

			Console.WriteLine(
				"** Testing Quaternion From Pitch, Roll, Yaw (XYZ) **");
			Console.WriteLine($"PRY: {fP:0.000},{fR:0.000},{fY:0.000} ->\r\n{q1}");
			v1 = FQuaternion.ToPitchRollYaw(q1);
			Console.WriteLine($"Converted back to PRY: {v1}\r\n");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionFromXRotation																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test new quaternion from an X-only rotation.
		/// </summary>
		private static void TestQuaternionFromXRotation()
		{
			float angle = Trig.DegToRad(20f);
			FQuaternion q1 = FQuaternion.FromXRotation(angle);
			FVector3 v1 = null;

			Console.WriteLine("** Testing Quaternion From X Rotation **");
			Console.WriteLine($"Angle: {angle:0.000} ->\r\n{q1}");
			v1 = FQuaternion.ToEuler(q1);
			Console.WriteLine($"Converted back: {v1}\r\n");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionFromYRotation																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test new quaternion from a Y-only rotation.
		/// </summary>
		private static void TestQuaternionFromYRotation()
		{
			float angle = Trig.DegToRad(30f);
			FQuaternion q1 = FQuaternion.FromYRotation(angle);
			FVector3 v1 = null;

			Console.WriteLine("** Testing Quaternion From Y Rotation **");
			Console.WriteLine($"Angle: {angle:0.000} ->\r\n{q1}");
			v1 = FQuaternion.ToEuler(q1);
			Console.WriteLine($"Converted back: {v1}\r\n");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionFromZRotation																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test new quaternion from a Z-only rotation.
		/// </summary>
		private static void TestQuaternionFromZRotation()
		{
			float angle = Trig.DegToRad(40f);
			FQuaternion q1 = FQuaternion.FromZRotation(angle);
			FVector3 v1 = null;

			Console.WriteLine("** Testing Quaternion From Z Rotation **");
			Console.WriteLine($"Angle: {angle:0.000} ->\r\n{q1}");
			v1 = FQuaternion.ToEuler(q1);
			Console.WriteLine($"Converted back: {v1}\r\n");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionInverse																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test inversion of quaternions.
		/// </summary>
		private static void TestQuaternionInverse()
		{
			FQuaternion q1 = new FQuaternion(0.161f, 0.032f, 0.056f, 0.963f);
			FQuaternion q2 = FQuaternion.Inverse(q1);

			Console.WriteLine("** Testing Quaternion Inverse **");
			Console.WriteLine($"{q1} inverse =\r\n{q2}\r\n");

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionIsIdentity																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test identity indication on a quaternion.
		/// </summary>
		private static void TestQuaternionIsIdentity()
		{
			FQuaternion q1 = new FQuaternion(0.434f, 0.630f, 0.397f, 0.507f);

			Console.WriteLine("** Testing if Quaternion Is Identity **");
			Console.WriteLine($"{q1} identity? {FQuaternion.IsIdentity(q1)}");
			FQuaternion.SetIdentity(q1);
			Console.WriteLine($"{q1} identity? {FQuaternion.IsIdentity(q1)}\r\n");

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionLength																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test quaternion length (magnitude).
		/// </summary>
		private static void TestQuaternionLength()
		{
			float fL = 0f;
			FQuaternion q1 = new FQuaternion(0.393f, 0.432f, 0.511f, 0.677f);

			fL = FQuaternion.Length(q1);

			Console.WriteLine("** Testing Quaternion Length **");
			Console.WriteLine($"{q1} length (expecting ~ 1.03) =\r\n{fL:0.000}\r\n");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionLerp																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test linear interpolation on quaternions.
		/// </summary>
		private static void TestQuaternionLerp()
		{
			float fP = 0.5f;
			FQuaternion q1 = new FQuaternion(0.226f, 0.342f, 0.430f, 0.902f);
			FQuaternion q2 = new FQuaternion(0.185f, 0.384f, 0.343f, 0.625f);
			FQuaternion q3 = FQuaternion.Lerp(q1, q2, fP);

			Console.WriteLine("** Testing Quaternion Lerp **");
			Console.WriteLine($"{q1} lerp {fP:0.0} ->\r\n{q2} =\r\n{q3}\r\n");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionMagnitudeSquared																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test quaternion magnitude squared.
		/// </summary>
		private static void TestQuaternionMagnitudeSquared()
		{
			FQuaternion q1 = new FQuaternion(0.374f, 0.546f, 0.938f, 0.522f);
			float fM = FQuaternion.MagnitudeSquared(q1);

			Console.WriteLine("** Testing Quaternion Magnitude Squared **");
			Console.WriteLine($"{q1} m2 = {fM:0.000}\r\n");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionMultiply																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test quaternion multiplication.
		/// </summary>
		private static void TestQuaternionMultiply()
		{
			FQuaternion q1 = new FQuaternion(0.315f, 0.926f, 0.690f, 0.428f);
			FQuaternion q2 = new FQuaternion(0.205f, 0.105f, 0.222f, 0.246f);
			FQuaternion q3 = FQuaternion.Multiply(q1, q2);

			Console.WriteLine("** Testing Quaternion Multiplication **");
			Console.WriteLine($"{q1} X\r\n{q2} =\r\n{q3}\r\n");

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionNegate																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test negation on quaternions.
		/// </summary>
		private static void TestQuaternionNegate()
		{
			FQuaternion q1 = new FQuaternion(0.300f, 0.500f, 0.609f, 0.599f);
			FQuaternion q2 = FQuaternion.Negate(q1);

			Console.WriteLine("** Testing Quaternion Negation **");
			Console.WriteLine($"{q1} negated =\r\n{q2}\r\n");

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionNormalize																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test normalization on quaternions.
		/// </summary>
		private static void TestQuaternionNormalize()
		{
			FQuaternion q1 = new FQuaternion(0.545f, 0.269f, 0.830f, 0.223f);
			FQuaternion q2 = FQuaternion.Normalize(q1);

			Console.WriteLine("** Testing Quaternion Normalization **");
			Console.WriteLine($"{q1} normalized =\r\n{q2}\r\n");
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* TestQuaternionRotate																									*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Test rotation on quaternions.
		///// </summary>
		//private static void TestQuaternionRotate()
		//{
		//	Console.WriteLine("** Testing Quaternion Rotation **");
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionSet																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test setting quaternion values.
		/// </summary>
		private static void TestQuaternionSet()
		{
			FQuaternion q1 = new FQuaternion(0.139f, 0.049f, 0.579f, 0.059f);

			Console.WriteLine("** Testing Quaternion Set Values **");
			Console.WriteLine($"Starting value: {q1}");
			Console.WriteLine($"Setting to: {0.225f},{0.041f},{0.292f},{0.911f}");
			FQuaternion.Set(q1, 0.225f, 0.041f, 0.292f, 0.911f);
			Console.WriteLine($"After set:      {q1}\r\n");
		}
		//*-----------------------------------------------------------------------*

			//*-----------------------------------------------------------------------*
			//* TestQuaternionSetIdentity																							*
			//*-----------------------------------------------------------------------*
			/// <summary>
			/// Test setting quaternion to identity value.
			/// </summary>
		private static void TestQuaternionSetIdentity()
		{
			FQuaternion q1 = new FQuaternion(0.067f, 0.917f, 0.428f, 0.377f);

			Console.WriteLine("** Testing Quaternion Identity Set **");
			Console.WriteLine($"Before reset: {q1}");
			FQuaternion.SetIdentity(q1);
			Console.WriteLine($"After reset:  {q1}\r\n");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionSLerp																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test spherical linear interpolation on quaternions.
		/// </summary>
		private static void TestQuaternionSLerp()
		{
			float fP = 0.5f;
			FQuaternion q1 = new FQuaternion(0.489f, 0.428f, 0.007f, 0.396f);
			FQuaternion q2 = new FQuaternion(0.430f, 0.269f, 1.000f, 0.015f);
			FQuaternion q3 = FQuaternion.Lerp(q1, q2, fP);

			Console.WriteLine("** Testing Quaternion SLerp **");
			Console.WriteLine($"{q1} slerp {fP:0.0} ->\r\n{q2} =\r\n{q3}\r\n");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionSubtract																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test subtraction on quaternions.
		/// </summary>
		private static void TestQuaternionSubtract()
		{
			FQuaternion q1 = new FQuaternion(0.762f, 0.767f, 0.981f, 0.159f);
			FQuaternion q2 = new FQuaternion(0.168f, 0.735f, 0.242f, 0.902f);
			FQuaternion q3 = FQuaternion.Subtract(q1, q2);

			Console.WriteLine("** Testing Quaternion Subtraction **");
			Console.WriteLine($"{q1} -\r\n{q2} =\r\n{q3}\r\n");

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestQuaternionToAxisAngle																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test creating a vector direction axis and an angle on a quaternion.
		/// </summary>
		private static void TestQuaternionToAxisAngle()
		{
			FQuaternion q1 = new FQuaternion(0.707f, 0f, 0f, 0.707f);
			FVector4 v4 = FQuaternion.ToAxisAngle(q1);

			Console.WriteLine("** Testing Quaternion To Axis Angle **");
			Console.WriteLine($"Axis angle of {q1} =\r\n{v4}");
			Console.WriteLine("Expecting above: X:1.000, Y:0, Z:0, W:1.571\r\n");
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* TestQuaternionToEuler																									*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Test creating Tait-Euler rotation vectors with ZYX ordering on
		///// quaternions.
		///// </summary>
		//private static void TestQuaternionToEuler()
		//{
		//	FQuaternion q1 = new FQuaternion(0.402f, 0.970f, 0.801f, 0.855f);
		//	FVector3 v3 = FQuaternion.ToEuler(q1);

		//	Console.WriteLine("** Testing Quaternion To Euler Rotation (ZYX) **");
		//	Console.WriteLine($"{q1} to euler =\r\n{v3:0.000}\r\n");
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* TestQuaternionToPitchRollYaw																					*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Test creating Tait-Euler rotation vectors with XYZ ordering on
		///// quaternions.
		///// </summary>
		//private static void TestQuaternionToPitchRollYaw()
		//{
		//	FQuaternion q1 = new FQuaternion(0.951f, 0.004f, 0.742f, 0.148f);
		//	FVector3 v3 = FQuaternion.ToPitchRollYaw(q1);

		//	Console.WriteLine("** Testing Quaternion To Euler Rotation (XYZ) **");
		//	Console.WriteLine($"{q1} to pitch,roll,yaw =\r\n{v3:0.000}\r\n");
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* TestSetPrecision																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Test the SetPrecision functions.
		/// </summary>
		private static void TestSetPrecision()
		{
			float max = 10000f;
			float min = -10000f;
			int precision = 0;
			FVector2 vector2 = new FVector2(
				RandomFloat(min, max),
				RandomFloat(min, max));
			FVector3 vector3 = new FVector3(
				RandomFloat(min, max),
				RandomFloat(min, max),
				RandomFloat(min, max));
			FVector4 vector4 = new FVector4(
				RandomFloat(min, max),
				RandomFloat(min, max),
				RandomFloat(min, max),
				RandomFloat(min, max));

			Console.WriteLine("** Testing SetPrecision **");

			Console.WriteLine($" FVector2. X:{vector2.X}; Y:{vector2.Y}");
			precision = (int)RandomFloatWhole(1, 5);
			Console.WriteLine($" Set precision to: {precision}");
			FVector2.SetPrecision(vector2, precision);
			Console.WriteLine($" Result.   X:{vector2.X}; Y:{vector2.Y}");
			Console.WriteLine();

			Console.WriteLine(
				$" FVector3. X:{vector3.X}; Y:{vector3.Y}; Z:{vector3.Z}");
			precision = (int)RandomFloatWhole(1, 5);
			Console.WriteLine($" Set precision to: {precision}");
			FVector3.SetPrecision(vector3, precision);
			Console.WriteLine(
				$" Result.   X:{vector3.X}; Y:{vector3.Y}; Z:{vector3.Z}");
			Console.WriteLine();

			Console.WriteLine(
				$" FVector4. X:{vector4.X}; Y:{vector4.Y}; Z:{vector4.Z}; " +
				$"W:{vector4.W}");
			precision = (int)RandomFloatWhole(1, 5);
			Console.WriteLine($" Set precision to: {precision}");
			FVector4.SetPrecision(vector4, precision);
			Console.WriteLine(
				$" Result.   X:{vector4.X}; Y:{vector4.Y}; Z:{vector4.Z}; " +
				$"W:{vector4.W}");
			Console.WriteLine();

			Console.WriteLine();

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
			FVector2 point = null;
			List<FVector2> points = null;

			Console.WriteLine("** Testing Shape Vertices **");

			//	Area.
			area = new FArea(314.550f, 343.889f, 466.488f, 236.685f);
			Console.WriteLine($" Area: {area.Left:0.###}, {area.Top:0.###}, " +
				$"{area.Width:0.###}, {area.Height:0.###}. Rotation: 15.5deg.");
			points = FArea.GetVertices(area, Trig.DegToRad(15.5f));
			foreach(FVector2 pointItem in points)
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
			foreach(FVector2 pointItem in points)
			{
				Console.WriteLine($"  {pointItem.X:0.###}, {pointItem.Y:0.###}");
			}
			//	Line.
			line = new FLine(
				new FVector2(1180.000f, 632.857f), new FVector2(1462.857f, 632.857f));
			Console.WriteLine(
				$" Line: {line.PointA.X:0.###}, {line.PointA.Y:0.###}, " +
				$"{line.PointB.X:0.###}, {line.PointB.Y:0.###}. Rotation: 52.17deg.");
			points = FLine.GetVertices(line, Trig.DegToRad(52.17f));
			foreach(FVector2 pointItem in points)
			{
				Console.WriteLine($"  {pointItem.X:0.###}, {pointItem.Y:0.###}");
			}
			//	Path.
			path = new FPath()
			{
				new FVector2(1507.143f, 352.857f),
				new FVector2(1767.143f, 755.714f),
				new FVector2(1595.714f, 302.857f)
			};
			point = FPath.GetCenter(path);
			Console.WriteLine(" Path. " +
				$"Center: {point.X:0.###}, {point.Y:0.###}. Rotation: 12deg.:");
			foreach(FVector2 pointItem in path)
			{
				Console.WriteLine($"  {pointItem.X:0.###}, {pointItem.Y:0.###}");
			}
			points = FPath.GetVertices(path, Trig.DegToRad(12f));
			Console.WriteLine("  Points:");
			foreach(FVector2 pointItem in points)
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
					prg.mControlPoint = FVector2.Parse(arg.Substring(key.Length), true);
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
					prg.mEndPoint = FVector2.Parse(arg.Substring(key.Length), true);
					continue;
				}
				key = "/startpoint:";
				if(lowerArg.StartsWith(key))
				{
					prg.mStartPoint = FVector2.Parse(arg.Substring(key.Length), true);
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
		private FVector2 mControlPoint = null;
		/// <summary>
		/// Get/Set a reference to the control point.
		/// </summary>
		public FVector2 ControlPoint
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
		private FVector2 mEndPoint = null;
		/// <summary>
		/// Get/Set a reference to the end point.
		/// </summary>
		public FVector2 EndPoint
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
			FVector2 coordinate = null;
			List<FVector2> points = null;
			float time = 0f;
			int index = 0;

			TestFAreaHasVolume();

			TestSetPrecision();

			//	Quaternions.
			TestQuaternionAdd();
			TestQuaternionClone();
			TestQuaternionConcatenate();
			TestQuaternionConjugate();
			TestQuaternionDivide();
			TestQuaternionDot();
			TestQuaternionEquals();
			TestQuaternionFromAxisAngle();
			TestQuaternionFromEuler();
			//	Not yet implemented.
			//TestQuaternionFromMatrix();
			TestQuaternionFromPitchRollYaw();
			TestQuaternionFromXRotation();
			TestQuaternionFromYRotation();
			TestQuaternionFromZRotation();
			TestQuaternionInverse();
			TestQuaternionIsIdentity();
			TestQuaternionLength();
			TestQuaternionLerp();
			TestQuaternionMagnitudeSquared();
			TestQuaternionMultiply();
			TestQuaternionNegate();
			TestQuaternionNormalize();
			//	Not yet implemented.
			//TestQuaternionRotate();
			TestQuaternionSet();
			TestQuaternionSetIdentity();
			TestQuaternionSLerp();
			TestQuaternionSubtract();
			TestQuaternionToAxisAngle();
			//	The following two items are tested inline with the FromX variations.
			//TestQuaternionToEuler();
			//TestQuaternionToPitchRollYaw();




			//	Test Camera3D rotation mode.
			Test3DRotationMode();

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
					foreach(FVector2 pointItem in points)
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
		private FVector2 mStartPoint = null;
		/// <summary>
		/// Get/Set a reference to the start point.
		/// </summary>
		public FVector2 StartPoint
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
