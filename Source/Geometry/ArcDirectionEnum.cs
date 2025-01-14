using System;
using System.Collections.Generic;
using System.Text;

namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	ArcDirectionEnum																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of allowable arc drawing directions.
	/// </summary>
	/// <remarks>
	/// The arc direction is the flow of the edge of the arc from the specified
	/// starting point or angle to the ending point or angle. In Cartesian space,
	/// ArcDirectionEnum.Forward is a counterclockwise progression from the
	/// positive-going ray along the X-axis, while in Display space, the value
	/// ArcDirectionEnum.Forward represents a clockwise progression from the
	/// positive-going rad along the X-axis.
	/// </remarks>
	public enum ArcDirectionEnum
	{
		/// <summary>
		/// Arc direction not specified or unknown.
		/// </summary>
		None = 0,
		/// <summary>
		/// The arc progresses in an increasing angular path from start to finish.
		/// </summary>
		Forward = 1,
		/// <summary>
		/// The arc progresses in an increasing angular path from start to finish.
		/// This value is compatible with Forward.
		/// </summary>
		Increasing = 1,
		/// <summary>
		/// The arc progresses in a decreasing angular path from start to finish.
		/// </summary>
		Reverse = 2,
		/// <summary>
		/// The arc progresses is a decreasing angular path from start to finish.
		/// This value is compatible with Reverse.
		/// </summary>
		Decreasing = 2
	}
	//*-------------------------------------------------------------------------*
}
