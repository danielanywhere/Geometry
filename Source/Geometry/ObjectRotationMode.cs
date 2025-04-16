namespace Geometry
{
	//*-------------------------------------------------------------------------*
	//*	ObjectRotationMode																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of possible object rotation methods.
	/// </summary>
	public enum ObjectRotationMode
	{
		/// <summary>
		/// The rotation mode of the object is unknown or undefined.
		/// </summary>
		None = 0,
		/// <summary>
		/// The object's rotation is calculated internally using a look-at
		/// value. This mode of rotation has no roll rotation.
		/// </summary>
		LookAt,
		/// <summary>
		/// The object's rotation is calculated directly from the euler X, Y, and Z
		/// angles specified.
		/// </summary>
		EulerRotation,
		/// <summary>
		/// The object's rotation is calculated from an X, Y, Z, W quaternion
		/// value.
		/// </summary>
		QuaternionRotation
	}
	//*-------------------------------------------------------------------------*
}
