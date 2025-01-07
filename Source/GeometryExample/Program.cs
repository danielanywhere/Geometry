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
						message.Append("No action has been specified. ");
						message.AppendLine("Please provide an /action parameter.");
						bShowHelp = true;
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
