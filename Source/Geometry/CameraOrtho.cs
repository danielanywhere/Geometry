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
#define NoShowTrace

using System;

using static Geometry.GeometryUtil;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	CameraOrtho																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Orthographic 3D camera.
	/// </summary>
	public class CameraOrtho
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private float mAspectRatio = 0f;
		private FVector3 mCamDistance = null;
		private FVector3 mCamForward = null;
		private FVector3 mCamUp = null;
		private FVector3 mCamRight = null;
		private float mDisplayHeightHalf = 0f;
		private float mDisplayWidthHalf = 0f;
		private float mTargetObjectHeight = 1f;
		private FVector3 mWorldUp = null;

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* ConvertCameraToWorld																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Used internally to convert the caller's camera-oriented vector to one
		/// matching the selected world setting.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be converted.
		/// </param>
		/// <returns>
		/// Reference to a 3D vector where Y and Z axes have been transposed,
		/// and / or the value of the Y or Z axes have been flipped, as necessary,
		/// if a valid vector has been supplied. Otherwise, null.
		/// </returns>
		protected virtual FPoint3 ConvertCameraToWorld(FVector3 vector)
		{
			FPoint3 result = null;

			if(vector != null)
			{
				result = new FPoint3();
				if(mUpAxis == AxisType.Y &&
					mHandedness == HandType.Left)
				{
					//	The world orientation matches the camera.
					result = FPoint3.Clone(vector);
				}
				else if(mUpAxis == AxisType.Z)
				{
					//	The world is in Z-up orientation.
					//	Handedness is automatically left.
					//	Local:
					//	X right.
					//	Y up.
					//	Z back.
					//	Convert to:
					//	X right.
					//	Y forward.
					//	Z up.
					result.X = vector.X;
					result.Y = vector.Z;
					result.Z = vector.Y;
				}
				else
				{
					//	The world is in Y-up orientation.
					//	Handedness is a factor.
					result.X = vector.X;
					result.Y = vector.Y;
					result.Z = vector.Z;
					if(mHandedness == HandType.Right)
					{
						//	The camera view is left-handed. Convert to right.
						result.Z = 0f - result.Z;
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ConvertWorldToCamera																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Used internally to convert the caller's world-oriented vector to one
		/// matching the selected camera setting.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be converted.
		/// </param>
		/// <returns>
		/// Reference to a 3D vector where Y and Z axes have been transposed,
		/// and / or the value of the Y or Z axes have been flipped, as necessary,
		/// if a valid vector has been supplied. Otherwise, null.
		/// </returns>
		protected virtual FVector3 ConvertWorldToCamera(FVector3 vector)
		{
			FVector3 result = null;

			if(vector != null)
			{
				result = new FVector3();

				if(mUpAxis == AxisType.Y &&
					mHandedness == HandType.Left)
				{
					//	The world orientation matches the camera.
					result = FVector3.Clone(vector);
				}
				else if(mUpAxis == AxisType.Z)
				{
					//	The world is in Z-up orientation.
					//	Handedness is automatically left.
					//	X right.
					//	Y forward.
					//	Z up.
					//	Convert to:
					//	X right.
					//	Y up.
					//	Z back.
					result.X = vector.X;
					result.Y = vector.Z;
					result.Z = vector.Y;
				}
				else
				{
					//	The world is in Y-up orientation.
					//	Handedness is a factor.
					result.X = vector.X;
					result.Y = vector.Y;
					result.Z = vector.Z;
					if(mHandedness == HandType.Right)
					{
						//	The camera view is left-handed. Convert to right.
						result.Z = 0f - result.Z;
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UpdateDisplay																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the internal display-related settings.
		/// </summary>
		protected virtual void UpdateDisplay()
		{
			if(mDisplayWidth != 0)
			{
				mAspectRatio =
					(float)mDisplayHeight / (float)mDisplayWidth;
			}
			else
			{
				mAspectRatio = 0f;
			}
			mDisplayWidthHalf = (float)mDisplayWidth / 2f;
			mDisplayHeightHalf = (float)mDisplayHeight / 2f;
			mTargetObjectHeight = mTargetObjectWidth * mAspectRatio;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UpdatePositions																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the internal positions for the current camera settings.
		/// </summary>
		protected virtual void UpdatePositions()
		{
			FVector3 camUpPreset = null;
			float forwardLeak = 0f;

			mLookAtInternal = ConvertWorldToCamera(mLookAt);
			mPositionInternal = ConvertWorldToCamera(mPosition);

			switch(mRotationMode)
			{
				case ObjectRotationMode.EulerRotation:
					mRotationInternal = new FVector3(
						Trig.DegToRad(mRotation.X),
						Trig.DegToRad(mRotation.Y + 90f),
						Trig.DegToRad(mRotation.Z));
					mLookAtInternal =
						FVector3.GetDestPoint(mPositionInternal, mRotationInternal,
							mCameraDistance);
					mLookAt = ConvertCameraToWorld(mLookAtInternal);
					break;
				case ObjectRotationMode.LookAt:
				default:
					mCamDistance = mLookAtInternal - mPositionInternal;
					//	Save the absolute distance from the camera to the subject.
					mCameraDistance = FVector3.Length(mCamDistance);
					mRotationInternal =
						FVector3.GetEulerAngle(mPositionInternal, mLookAtInternal);
					mRotation.X = Trig.RadToDeg(mRotationInternal.X);
					mRotation.Y = Trig.RadToDeg(mRotationInternal.Y) - 90f;
					mRotation.Z = Trig.RadToDeg(mRotationInternal.Z);
					break;
			}

			//	Create the perspective camera basis.
			mCamForward = FVector3.Normalize(mCamDistance);
			mWorldUp = new FVector3(0f, 1f, 0f);
			if(Math.Abs(FVector3.Dot(mCamForward, mWorldUp)) > 0.99999f)
			{
				//	If near-vertical forward occurs, switch perspective.
				mWorldUp = new FVector3(0f, 0f, 1f);
			}
			mCamRight =
				FVector3.Normalize(FVector3.CrossProduct(mWorldUp, mCamForward));
			camUpPreset =
				FVector3.Normalize(FVector3.CrossProduct(mCamForward, mCamRight));
			if(SignEqual(camUpPreset.Y, mCamDistance.Y))
			{
				camUpPreset.Y = 0f - camUpPreset.Y;
			}
			if(camUpPreset.Equals(mCamForward))
			{
				//	The camera is directly to the right or the left of look-at.
#if ShowTrace
				Trace.WriteLine("Camera3D.UpdatePositions. Break here...");
#endif
				mCamUp = FVector3.Negate(FVector3.Normalize(FVector3.CrossProduct(mCamRight, mCamForward)));
			}
			else
			{
				forwardLeak = FVector3.Dot(camUpPreset, mCamForward);
				mCamUp = FVector3.Normalize(camUpPreset - (mCamForward * forwardLeak));
			}
#if ShowTrace
			Trace.WriteLine($"Cam Forward: {{{mCamForward}}},");
			Trace.WriteLine($"Cam Right:   {{{mCamRight}}},");
			Trace.WriteLine($"Cam Up Raw:  {{{camUpPreset}}},");
			Trace.WriteLine($"Forard leak: {forwardLeak:0.000},");
			Trace.WriteLine($"Cam Up Adj:  {{{mCamUp}}},");
			Trace.WriteLine("");
#endif
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		/// <summary>
		/// Private member for <see cref="CameraDistance">CameraDistance</see>.
		/// </summary>
		private float mCameraDistance = 0f;
		/// <summary>
		/// Get/Set the absolute distance from the camera to the subject.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This value is ignored except when externally setting the Rotation
		/// property while the RotationMode property is EulerRotation, and should
		/// be set prior to adjusting Rotation.
		/// </para>
		/// <para>
		/// The value can be prepared automatically for a scene by setting the
		/// LookAt property first, which results in a practical value for this
		/// property. Afterward, rotations and repositioning can take place in
		/// any order and the LookAt property will be updated appropriately.
		/// </para>
		/// </remarks>
		public float CameraDistance
		{
			get { return mCameraDistance; }
			set { mCameraDistance = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DisplayHeight																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="DisplayHeight">DisplayHeight</see>.
		/// </summary>
		private int mDisplayHeight = 1080;
		/// <summary>
		/// Get/Set the height of the display frame.
		/// </summary>
		public int DisplayHeight
		{
			get { return mDisplayHeight; }
			set
			{
				mDisplayHeight = value;
				UpdateDisplay();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DisplayWidth																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="DisplayWidth">DisplayWidth</see>.
		/// </summary>
		private int mDisplayWidth = 1920;
		/// <summary>
		/// Get/Set the width of the display frame.
		/// </summary>
		public int DisplayWidth
		{
			get { return mDisplayWidth; }
			set
			{
				mDisplayWidth = value;
				UpdateDisplay();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Handedness																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Handedness">Handedness</see>.
		/// </summary>
		private HandType mHandedness = HandType.Left;
		/// <summary>
		/// Get/Set the handedness type of the world coordinate system.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The internal camera always works in the left-handed coordinate system.
		/// When the world is using another system, the coordinates of a point are
		/// converted to the camera orientation prior to projection.
		/// </para>
		/// <para>
		/// In a right-handed world orientation, viewing the XZ plane in 2D,
		/// the Z axis increases in a downward direction. In contrast, in a
		/// left-handed world orientation, viewing the XZ plane in 2D, the Z axis
		/// increases in an upward direction.
		/// </para>
		/// <para><b>More Information</b></para>
		/// <para>
		/// In both of the following conventions, the second finger represents the
		/// X axis, the second finger represents the Y axis, and the thumb
		/// represents the Z axis.
		/// </para>
		/// <para>
		/// <b>Right Handed</b>. In the right-handed system, use your right hand
		/// index finger to point at yourself.The second finger moves out to the
		/// right and the thumb points up.
		/// </para>	
		/// <para>
		/// <b>Left Handed</b>. In the left-handed system, use your left hand index
		/// finger to point away from yourself. The second finger moves out to
		/// the right and the thumb points up.
		/// </para>
		/// </remarks>
		public HandType Handedness
		{
			get { return mHandedness; }
			set
			{
				mHandedness = value;
				UpdatePositions();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	LookAt																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="LookAt">LookAt</see>.
		/// </summary>
		private FPoint3 mLookAt = new FPoint3();
		/// <summary>
		/// Private member for <see cref="LookAt">LookAt</see>.
		/// </summary>
		private FVector3 mLookAtInternal = new FVector3();
		/// <summary>
		/// Get/Set a reference to the location upon which the camera is focused,
		/// in world coordinates.
		/// </summary>
		/// <remarks>
		/// Although the internal camera is transposed using a Y-up left-handed
		/// system, all of the positions are expressed in the host's world system
		/// to avoid any confusion.
		/// </remarks>
		public FPoint3 LookAt
		{
			get { return mLookAt; }
			set
			{
				if(value != null)
				{
					mLookAt = value;
				}
				else
				{
					mLookAt = new FPoint3();
				}
				UpdatePositions();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Position																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Position">Position</see>.
		/// </summary>
		private FPoint3 mPosition = new FPoint3();
		/// <summary>
		/// Private member for <see cref="Position">Position</see>.
		/// </summary>
		private FVector3 mPositionInternal = new FVector3();
		/// <summary>
		/// Get/Set a reference to the position of the camera, in world
		/// coordinates.
		/// </summary>
		/// <remarks>
		/// Although the internal camera is transposed using a Y-up left-handed
		/// system, all of the positions are expressed in the host's world system
		/// to avoid any confusion.
		/// </remarks>
		public FPoint3 Position
		{
			get { return mPosition; }
			set
			{
				if(value != null)
				{
					mPosition = value;
				}
				else
				{
					mPosition = new FPoint3();
				}
				UpdatePositions();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ProjectToScreen																												*
		//*-----------------------------------------------------------------------*
#if ShowTrace
		/// <summary>
		/// Project the provided 3D line to a 2D version that can be displayed on
		/// the caller's display.
		/// </summary>
		/// <param name="subject">
		/// Reference to the line, in World coordinates, to be projected to 2D.
		/// </param>
		/// <param name="trace">
		/// Value indicating whether trace output should be rendered for this
		/// call.
		/// </param>
		/// <returns>
		/// Reference to a two dimensional line compatible with display on the
		/// caller's screen, if valid. Otherwise, null.
		/// </returns>
		public FLine ProjectToScreen(FLine3 subject, bool trace = false)
#else
		/// <summary>
		/// Project the provided 3D line to a 2D version that can be displayed on
		/// the caller's display.
		/// </summary>
		/// <param name="subject">
		/// Reference to the line, in World coordinates, to be projected to 2D.
		/// </param>
		/// <returns>
		/// Reference to a two dimensional line compatible with display on the
		/// caller's screen, if valid. Otherwise, null.
		/// </returns>
		public FLine ProjectToScreen(FLine3 subject)
#endif
		{
			FLine result = null;

			if(subject != null)
			{
				result = new FLine()
				{
					PointA = ProjectToScreen(subject.PointA
#if ShowTrace
						, trace, "PointA"
#endif
					),
					PointB = ProjectToScreen(subject.PointB
#if ShowTrace
						, trace, "PointB"
#endif
						)
				};
			}

			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
#if ShowTrace
		/// <summary>
		/// Project the provided 3D point to a 2D version that can be displayed on
		/// the caller's display.
		/// </summary>
		/// <param name="subject">
		/// Reference to the point, in World coordinates, to be projected to 2D.
		/// </param>
		/// <param name="trace">
		/// Value indicating whether trace output should be rendered for this
		/// call.
		/// </param>
		/// <param name="traceShapeName">
		/// Optional name of the shape for which the trace is being generated.
		/// Ignored if trace == false.
		/// </param>
		/// <returns>
		/// Reference to a two dimensional point compatible with display on the
		/// caller's screen, if valid. Otherwise, null.
		/// </returns>
		public FPoint ProjectToScreen(FPoint3 subject, bool trace = false,
			string traceShapeName = "")
#else
		/// <summary>
		/// Project the provided 3D point to a 2D version that can be displayed on
		/// the caller's display.
		/// </summary>
		/// <param name="subject">
		/// Reference to the point, in World coordinates, to be projected to 2D.
		/// </param>
		/// <returns>
		/// Reference to a two dimensional point compatible with display on the
		/// caller's screen, if valid. Otherwise, null.
		/// </returns>
		public FPoint ProjectToScreen(FPoint3 subject)
#endif
		{
			double camX = 0d;
			double camY = 0d;
			//double camZ = 0d;
#if ShowTrace
			string name = "";
#endif
			//double normX = 0d;
			//double normY = 0d;
			FPoint result = new FPoint();
			double scaleX = 0d;
			double scaleY = 0d;
			FVector3 toSubject = null;

			if(subject != null)
			{
				// Vector from camera to subject.
				toSubject =
					ConvertWorldToCamera(subject) - (FVector3)mPositionInternal;

				camX = FVector3.Dot(toSubject, mCamRight);
				camY = FVector3.Dot(toSubject, mCamUp);
				//camZ = FVector3.DotProduct(toSubject, mCamForward);

				//scaleX = 1.0d / Math.Tan((double)mViewfinderXHalf);
				//scaleY = scaleX * mAspectRatio;

				scaleX = camX / ((double)mTargetObjectWidth * 0.5d);
				scaleY = camY / ((double)mTargetObjectHeight * 0.5d);

				//	Convert to screen space.
				result.X = (float)(((double)mDisplayWidth * 0.5d) +
					(scaleX * ((double)mDisplayWidth * 0.5d)));
				result.Y = (float)(((double)mDisplayHeight * 0.5d) -
					(scaleY * ((double)mDisplayHeight * 0.5d)));

			}
#if ShowTrace
			if(trace)
			{
				if(traceShapeName?.Length > 0)
				{
					name = traceShapeName;
				}
				Trace.WriteLine($" {name}CamX: {camX:0.000},");
				Trace.WriteLine($" {name}CamY: {camY:0.000},");
				//Trace.WriteLine($" {name}CamZ: {camZ:0.000},");
				Trace.WriteLine($" {name}ScaleX: {scaleX:0.000},");
				Trace.WriteLine($" {name}ScaleY: {scaleY:0.000},");
				//Trace.WriteLine($" {name}NormX: {normX:0.000},");
				//Trace.WriteLine($" {name}NormY: {normY:0.000},");
				Trace.WriteLine($" {name}ScreenX: {result.X:0.000}, ");
				Trace.WriteLine($" {name}ScreenY: {result.Y:0.000}, ");
				Trace.WriteLine("");
			}
#endif
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Rotation																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Rotation">Rotation</see>.
		/// </summary>
		private FVector3 mRotation = new FVector3();
		/// <summary>
		/// Private member for <see cref="Rotation">Rotation</see>.
		/// </summary>
		private FVector3 mRotationInternal = new FVector3();
		/// <summary>
		/// Get/Set a reference to the 3D rotation of the camera.
		/// </summary>
		/// <remarks>
		/// <para>
		/// In this version, Pitch rotates upward and Yaw rotates left.
		/// </para>
		/// <para>
		/// Natural camera-forward (eg Rotation 0,0,0) is forward on
		/// Z-axis.
		/// </para>
		/// </remarks>
		public FVector3 Rotation
		{
			get { return mRotation; }
			set
			{
				if(value != null)
				{
					mRotation = value;
				}
				else
				{
					mRotation = new FVector3();
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RotationMode																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="RotationMode">RotationMode</see>.
		/// </summary>
		private ObjectRotationMode mRotationMode = ObjectRotationMode.LookAt;
		/// <summary>
		/// Get/Set a value indicating which type of rotation the camera is using.
		/// </summary>
		public ObjectRotationMode RotationMode
		{
			get { return mRotationMode; }
			set { mRotationMode = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TargetObjectWidth																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="TargetObjectWidth">
		/// TargetObjectWidth</see>.
		/// </summary>
		private float mTargetObjectWidth = 1f;
		/// <summary>
		/// Get/Set the maximum target object width.
		/// </summary>
		public float TargetObjectWidth
		{
			get { return mTargetObjectWidth; }
			set
			{
				mTargetObjectWidth = value;
				UpdateDisplay();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	UpAxis																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="UpAxis">UpAxis</see>.
		/// </summary>
		private AxisType mUpAxis = AxisType.Y;
		/// <summary>
		/// Get/Set the up axis of the world.
		/// </summary>
		/// <remarks>
		/// <para>
		/// In the camera projection, the up axis will always be Y, since the
		/// resulting 2D image contains only X and Y.
		/// </para>
		/// <para>
		/// However, the world is most likely arranged as having either Y-up,
		/// where X and Z are on the horizontal plane, or Z-up, where X and Y are
		/// on the horizontal plane.
		/// </para>
		/// <para>
		/// All of the transformations in this camera assume that the world
		/// arrangement is Y-up. When Z-up is indicated here, an additional
		/// conversion is applied to the point just prior to projection to
		/// convert its values to the appropriate orientation.
		/// </para>
		/// </remarks>
		public AxisType UpAxis
		{
			get { return mUpAxis; }
			set
			{
				mUpAxis = value;
				UpdatePositions();
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
