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
using System.Net.Http.Headers;
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

			//	Test FMatrix3 operations.
			TestMatrix3();

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
