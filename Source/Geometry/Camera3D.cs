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

//	TODO: Wire the value change events of the Position and LookAt properties.

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	Camera3D																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// A primitive 3D camera for transforming points in 3D global space to 2D
	/// drawing space.
	/// </summary>
	public class Camera3D
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private float mAspectRatio = 0f;
		private float mDisplayHeightHalf = 0f;
		private float mDisplayWidthHalf = 0f;
		private float mFieldOfViewY = 0f;
		private float mViewfinderDistance = 0f;
		private float mViewfinderDown = 0f;
		private float mViewfinderLeft = 0f;
		private float mViewfinderRight = 0f;
		private float mViewfinderUp = 0f;
		private float mViewfinderXHalf = 0f;
		private float mViewfinderYHalf = 0f;

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* ConvertCameraToWorld																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert the caller's camera-oriented vector to one matching the
		/// selected world setting.
		/// </summary>
		/// <param name="vector">
		/// Reference to the vector to be converted.
		/// </param>
		/// <returns>
		/// Reference to a 3D vector where Y and Z axes have been transposed,
		/// and / or the value of the Y or Z axes have been flipped, as necessary,
		/// if a valid vector has been supplied. Otherwise, null.
		/// </returns>
		protected virtual FVector3 ConvertCameraToWorld(FVector3 vector)
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
		/// Convert the caller's world-oriented vector to one matching the
		/// selected camera setting.
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
		/// Update the display-related settings.
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
			mFieldOfViewY = mFieldOfView * mAspectRatio;
			mViewfinderXHalf = Trig.DegToRad(mFieldOfView / 2f);
			mViewfinderYHalf = Trig.DegToRad(mFieldOfViewY / 2f);
			mViewfinderDistance = Trig.GetLineAdjFromAngOpp(mViewfinderXHalf, 0.5f);
			mDisplayWidthHalf = (float)mDisplayWidth / 2f;
			mDisplayHeightHalf = (float)mDisplayHeight / 2f;
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
			FVector3 direction = null;
			double horizontalDistance = 0d;

			mLookAtInternal = ConvertWorldToCamera(mLookAt);
			mPositionInternal = ConvertWorldToCamera(mPosition);

			switch(mRotationMode)
			{
				case ObjectRotationMode.LookAt:
					direction = mLookAtInternal - mPositionInternal;
					horizontalDistance = Math.Sqrt(
						(double)direction.X * (double)direction.X +
						(double)direction.Z * (double)direction.Z);
					mRotationInternal = new FVector3(
						(float)Math.Atan2((double)direction.Y, horizontalDistance),
						(float)Math.Atan2((double)direction.X, (double)direction.Z), 0f);
					break;
				case ObjectRotationMode.EulerRotation:
				default:
					mRotationInternal = new FVector3(
						Trig.DegToRad(mRotation.X),
						Trig.DegToRad(mRotation.Y),
						Trig.DegToRad(mRotation.Z));
					break;
			}
			//	Prepare the viewport edges for quick comparison.
			mViewfinderLeft = mRotationInternal.Y - mViewfinderXHalf;
			mViewfinderRight = mRotationInternal.Y + mViewfinderXHalf;
			mViewfinderUp = mRotationInternal.X + mViewfinderYHalf;
			mViewfinderDown = mRotationInternal.X - mViewfinderYHalf;

		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the Camera3D Item.
		/// </summary>
		public Camera3D()
		{
			UpdateDisplay();
			UpdatePositions();
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
		//*	FieldOfView																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="FieldOfView">FieldOfView</see>.
		/// </summary>
		private float mFieldOfView = 60f;
		/// <summary>
		/// Get/Set the camera's field of view, in degrees.
		/// </summary>
		public float FieldOfView
		{
			get { return mFieldOfView; }
			set
			{
				mFieldOfView = value;
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
		/// In a right-handed world orientation, viewing the XZ plane,
		/// the Z axis increases in a downward direction. In contrast, in a
		/// left-handed world orientation, viewing the XZ plane, the Z axis
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
		private FPoint3 mLookAtInternal = new FPoint3();
		/// <summary>
		/// Get/Set a reference to the location upon which the camera is focused,
		/// in world coordinates.
		/// </summary>
		/// <remarks>
		/// Although the internal camera is transposed using a Y-up right-handed
		/// system, all of the positions are expressed in the host's world system
		/// to avoid any confusion.
		/// </remarks>
		public FPoint3 LookAt
		{
			get { return mLookAt; }
			set
			{
				mLookAt = value;
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
		private FPoint3 mPositionInternal = new FPoint3();
		/// <summary>
		/// Get/Set a reference to the position of the camera, in world
		/// coordinates.
		/// </summary>
		/// <remarks>
		/// Although the internal camera is transposed using a Y-up right-handed
		/// system, all of the positions are expressed in the host's world system
		/// to avoid any confusion.
		/// </remarks>
		public FPoint3 Position
		{
			get { return mPosition; }
			set
			{
				mPosition = value;
				UpdatePositions();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ProjectToScreen																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Project the provided 3D line to a 2D version that can be displayed on
		/// the caller's display.
		/// </summary>
		/// <param name="line">
		/// Reference to the line to be projected to 2D.
		/// </param>
		/// <returns>
		/// Reference to a two dimensional line compatible with display on the
		/// caller's screen, if valid. Otherwise, null.
		/// </returns>
		public FLine ProjectToScreen(FLine3 line)
		{
			FVector3 direction = null;
			double horizontalDistance = 0d;
			FVector3 projectedPoint = null;
			FLine result = null;
			FVector3 sightAngle = null;

			if(line != null)
			{
				projectedPoint = new FVector3();
				result = new FLine();
				//	Point A.
				direction = line.PointA - mPositionInternal;
				horizontalDistance = Math.Sqrt(
					(double)direction.X * (double)direction.X +
					(double)direction.Z * (double)direction.Z);
				sightAngle = new FVector3(
					(float)Math.Atan2((double)direction.Y, horizontalDistance),
					(float)Math.Atan2((double)direction.X, (double)direction.Z), 0f);
				sightAngle.X -= mRotationInternal.X;
				sightAngle.Y -= mRotationInternal.Y;
				//	X position from rotation on Y.
				projectedPoint.X =
					Trig.GetLineOppFromAngAdj(sightAngle.Y, mViewfinderDistance);
				//	Y position from rotation on X.
				projectedPoint.Y =
					Trig.GetLineOppFromAngAdj(sightAngle.X, mViewfinderDistance);
				result.PointA.X =
					mDisplayWidthHalf + (mDisplayWidthHalf * projectedPoint.X);
				result.PointA.Y =
					mDisplayHeightHalf + (mDisplayHeightHalf * -projectedPoint.Y);
				//	Point B.
				direction = line.PointB - mPositionInternal;
				horizontalDistance = Math.Sqrt(
					(double)direction.X * (double)direction.X +
					(double)direction.Z * (double)direction.Z);
				sightAngle = new FVector3(
					(float)Math.Atan2((double)direction.Y, horizontalDistance),
					(float)Math.Atan2((double)direction.X, (double)direction.Z), 0f);
				sightAngle.X -= mRotationInternal.X;
				sightAngle.Y -= mRotationInternal.Y;
				//	X position from rotation on Y.
				projectedPoint.X =
					Trig.GetLineOppFromAngAdj(sightAngle.Y, mViewfinderDistance);
				//	Y position from rotation on X.
				projectedPoint.Y =
					Trig.GetLineOppFromAngAdj(sightAngle.X, mViewfinderDistance);
				result.PointB.X =
					mDisplayWidthHalf + (mDisplayWidthHalf * projectedPoint.X);
				result.PointB.Y =
					mDisplayHeightHalf + (mDisplayHeightHalf * -projectedPoint.Y);
			}

			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Project the provided 3D point to a 2D version that can be displayed on
		/// the caller's display.
		/// </summary>
		/// <param name="point">
		/// Reference to the point to be projected to 2D.
		/// </param>
		/// <returns>
		/// Reference to a two dimensional point compatible with display on the
		/// caller's screen, if valid. Otherwise, null.
		/// </returns>
		public FPoint ProjectToScreen(FPoint3 point)
		{
			FVector3 direction = null;
			double horizontalDistance = 0d;
			FVector3 projectedPoint = null;
			FPoint result = null;
			FVector3 sightAngle = null;

			if(point != null)
			{
				direction = point - mPositionInternal;
				horizontalDistance = Math.Sqrt(
					(double)direction.X * (double)direction.X +
					(double)direction.Z * (double)direction.Z);
				sightAngle = new FVector3(
					(float)Math.Atan2((double)direction.Y, horizontalDistance),
					(float)Math.Atan2((double)direction.X, (double)direction.Z), 0f);
				sightAngle.X -= mRotationInternal.X;
				sightAngle.Y -= mRotationInternal.Y;
				projectedPoint = new FVector3()
				{
					//	X position from rotation on Y.
					X = Trig.GetLineOppFromAngAdj(sightAngle.Y, mViewfinderDistance),
					//	Y position from rotation on X.
					Y = Trig.GetLineOppFromAngAdj(sightAngle.X, mViewfinderDistance)
				};
				result = new FPoint()
				{
					X = mDisplayWidthHalf + (mDisplayWidthHalf * projectedPoint.X),
					Y = mDisplayHeightHalf + (mDisplayHeightHalf * -projectedPoint.Y)
				};
			}
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
		public FVector3 Rotation
		{
			get { return mRotation; }
			set { mRotation = value; }
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
