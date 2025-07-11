# Dan's Geometry Library

An easy-to-implement function library providing numeric operations to handle solutions to typical problems encountered in the field of geometry, including Bezier Curves, Linear Interpolation, and Trigonometry.


Basic Example:

```cs
using System;
using Geometry;

// Calculate the hypotenuse of a triangle given its
// angle, in Degrees, and adjacent side.
namespace MyProject
{
 public class Program
 {
  public static void Main(string[] args)
  {
   float hyp = Trig.GetLineHypFromAngAdj(
    Trig.DegToRad(12.3f), 177.5f
   );
   Console.WriteLine($"Hypotenuse: {hyp} units...");

   // 3D to 2D Direct World-To-Screen Projection.
   Camera3D camera = new Camera3D()
   {
    DisplayWidth = 690,
    DisplayHeight = 359,
    Position = new FPoint3(1500f, 1500f, -1000f),
    LookAt = new FPoint3()
   };
   FVector3 worldPoint = new FVector3(-1301.750f,0.000f,609.600f);
   FPoint screenPoint = camera.ProjectToScreen(worldPoint);
   Console.WriteLine($"Screen Point: {screenPoint}");
  }
 }
}

```

## Updates

| Version | Description |
|---------|-------------|
| 25.2711.4233 | **Matrix4.ColumnToVector(matrix, columnIndex)** and **Matrix4.RowToVector(matrix, rowIndex)** functions have been added; the **Trig** class has now been converted to treat all 2-value inputs and outputs as FVector2, instead of FPoint, which allows all of the methods to be compatible with both types; the **MagnitudeSquared** method has been added to the FVector2 class to return a quick sum of all values squared for quick comparisons; the **FVector2** class now offers a **Length** function. Note that this is a redirection to the **FVector2.Magnitude** function; **Trig.GetLineOppFromAdjHyp** was repaired by setting the preliminary result to absolute before taking the square root, which mainly avoids small negative fractions; **FLine(x1, y1, x2, y2)** constructor has been added; **IsPointOnEllipse(FEllipse, FVector2)** has been added to the **FEllipse** class; **FEllipse.GetPointOnEllipseEdge(FEllipse, FVector2)** function has been added; **Rotation** property has been added to the **FEllipse** class. The optional **FEllipse.GetVertices shapeRotation** parameter has been removed. The rotation value of the shape is now read directly from the instance's Rotation property; **FPoint.ClosestPoint(FPoint, List&lt;FPoint&gt;)** overload has been added to find the closest point in a list to a given reference point; The static **FLine.GetCenter(FLine)** function has been added to return the center location of the line; **FPoint** and **FVector2** now both have implicit conversions available to **FVector4**; **FPoint.Invert** function has been corrected to return the reciprocals of the caller's member values. The **FPoint.Negate** function has been added to return the negative versions of the caller's member values; **FVector3.Invert** function has been corrected to return the reciprocals of the caller's member values. The **FVector3.Negate** function has been added to return the negative versions of the caller's member values; **FVector2.Invert** function has been corrected to return the reciprocals of the caller's member values. The **FVector2.Negate** function has been added to return the negative versions of the caller's member values; **FRotation3.Invert** function has been corrected to return the reciprocals of the caller's member values. The **FRotation3.Negate** function has been added to return the negative versions of the caller's member values; **FPoint3.Invert** function has been corrected to return the reciprocals of the caller's member values. The **FPoint3.Negate** function has been added to return the negative versions of the caller's member values; **FPoint.GetCenter(List&lt;FPoint&gt;)** function has been added to return a centroid of a point collection; added the **FArea.GetCenter(FArea)** function to retrieve the center coordinate of the specified area; the function **FEllipse.GetVerticesInArc(ellipse, pointCount, startAngle, sweepAngle)** has been added to return vertices in an arc of the ellipse, beginning at a starting angle and progressing through a positive or negative sweep for the number of points specified; **FVector2** now supports implicit division with scalar values; **FVector2.Normalize(FVector2)** function has been added; |
| 25.2613.4238 | Added the class **FQuaternion** to support robust rotations in 3D space. This quaternion includes FromAxisAngle / ToAxisAngle, FromEuler / ToEuler (ZYX order),  FromPitchRollYaw / ToPitchRollYaw (XYZ order), both Lerp and Slerp, Multiplication, Division, Dot Product and all of the other methods and functionality you would generally expect from a quaternion; in **FMatrix4**, added the methods **GetRotationMatrixX(float theta)**, **GetRotationMatrixY(float theta)**, and **GetRotationMatrixZ(float theta)** to provide individual rotation axis preparations in 4x4 format; also in **FMatrix4**, a [int row, int column] indexer was added to allow direct array access from the object instead of having to specify the Values property (ie 'my4x4[3, 3]' vs. my4x4.Values[3,3]'), **ColumnToPoint(FMatrix4 matrix, int columnIndex)** was added to retrieve the first three values in the column specified by ordinal index, and **RowToPoint(FMatrix4 matrix, int rowIndex)** was added to retrieve the first three values in the row specified by the ordinal index. |
| 25.2530.4054 | Added the class **FRotation3** to specifically handle the concept of 3D Euler rotations. The new class is very similar to **FPoint3**. |
| 25.2429.4547 | Repaired a bug in **Linear.Lerp(FVector3, FVector3)**; Breaking changes: **FloatPointEventArgs** now supports a list of axis tracking values, instead of the fixed properties X and Y. This allows any number of axis-related value events to be tracked. **FloatPoint3EventArgs** and **FloatPoint3EventHandler** have been replaced by **FloatPointEventArgs** and **FloatPointEventHandler**, respectively. **FVector3.DotProduct** has been renamed to **FVector3.Dot** to maintain naming consistency with other similar classes. |
| 25.2422.4040 | Upgrades: **Camera3D** and **CameraOrtho** cameras now support change in view using the **CameraDistance** and **Rotation** properties, instead of **LookAt**. However, bear in mind that **LookAt** can also initially be used to accurately set **CameraDistance**; **FVector3.GetEulerAngle** function has been added; Error in **FVector3.GetDestPoint** has been repaired. |
| 25.2414.4032 | Optimization: **Camera3D** ScaleX and ScaleY values are now only calculated when display information changes, reducing the total number of multiplications per vertex from 21 to 17 and the total number of divisions from 6 to 2. |
| 25.2406.3841 | A vertical screen compression bug has been fixed in **Camera3D**; **Circle.GetVertices** method has been added. |
| 25.2404.4129 | **CameraOrtho**, a tiny orthographic camera with the same direct world-to-screen projection capability as its sibling **Camera3D**, has been introduced in this version; **Camera3D.ProjectToScreen** now accepts coordinates in the caller's own world orientation, automatically translating them prior to rendering; Rotation axis order has been rearranged in **FMatrix3.Rotate(.., AxisType)** to reflect the correct outcomes for various up-axes; **FPoint3** can now be implicitly created from **FPoint** to support operations where a transition from 2D to 3D takes place; **Linear.Lerp** overloads have been added for **FPoint3** and **FVector3**. |
| 25.2401.4023 | Bug-fix only; A **Camera3D** can now be orbited around a subject, crossing look-at lines on  the horizontal and vertical planes without an orientation flip. The hack from the previous version has been removed and replaced with the logic to avoid a flipping condition. Please keep in mind this camera implements an "always up" style of view, relative to its local Y axis. When passing from one side of the look-at X or Z planes, the view will flip to keep the camera upright for that relationship. |
| 25.2331.4041 | Bug-fix only; A minor hack has been added on **Camera3D** that nudges the camera slightly to avoid zero-crossing mishaps between Camera and LookAt positions. |
| 25.2330.3855 | Bug-fix only; **Camera3D.ProjectToScreen(x)** functions have been repaired and tested. |
| 25.2327.4545 | No breaking changes; Basic low-level 3D rendering has been added, along with the classes Camera3D, FColor4, FLine3, FMatrix4, FPoint3, and FVector4. Project to screen support currently includes FPoint3 (3D point) and FLine3 (3D line), with many more coming in the near future. |
| 25.2317.4012 | No breaking changes; The following methods have been added to improve intersection handling: **FArea.HasIntersection(FArea, FLine)**, **FArea.GetIntersections(FArea, FLine)**, **FArea.HasIntersection(FArea, FArea)**, and **FArea.GetIntersections(FArea, FArea)** |
| 25.2307.4248 | No breaking changes; In **FArea**, optional rotation parameter has been added to the **GetLines()** method to allow rotation of the shape around its local center.; **GetVertices()** method has been added to these classes: **FArea**, **FEllipse**, **FLine**, **FPath**; **GetLines()** method has been added to these classes: **FEllipse**, **FPath**, **GetLine()** method has been added to the FLine class; **Translate()** method has been added to the **FPath** class; the following new methods were added to the **FPoint** class: **Invert**, **Rotate**, **Translate** |
| 25.2303.4537 | One breaking change: The obsolete **Ellipse** utility class has been removed. Please use identical methods in the **FEllipse** class. |
| 25.1228.4607 | No breaking changes; **FArea.GetLines(FArea area)** added; **FArea.IsPointAtCorner(FArea area, FPoint point)** added; **FLine.GetClosestPoint(FLine line, FPoint point)** added; **FLine.GetIntersectingLine(List<FLine> lines, FPoint point)** added; **FLine.GetIntersectingLines(List<FLine> lines, FPoint point)** added.; **FLine.IsPointAtEnd(FLine line, FPoint point)** added; **FPoint.Dot(FPoint value1, FPoint value2)** added |
| 25.1221.4026 | No breaking changes; **FEllipse** class has been added as a first-class geometric primitive shape with built-in functions for cloning, transferring values between shapes, retrieving the bounding box of the shape, retrieving the focal points and the imaginary string length used to draw a perimeter in the real world, intersecting with lines, and plotting coordinates on the edge at an angle from center; **Ellipse** and its method **FindIntersections** have been marked as depreciated and will be removed in the near future, so please change your references to the identical **FEllipse.FindIntersections** method at your earliest convenience. |
| 25.1218.4115 | No breaking changes; Ellipse class has been added, with **FPoint[] FindIntersections(FPoint center, float radiusX, float radiusY, FLine line)**, which finds 0, 1, or 2 intersections between the edge of an ellipse and a line. |
| 25.1207.4429 | No breaking changes; Updates to API documentation. |
| 25.1206.4136 | No breaking changes; Repaired bug in **FLine.Intersect** that was causing minor inaccuracies. |
| 25.1205.4515 | No breaking changes; Added **Translate** and **TranslateVector** methods to the **FLine** class. |
| 25.1131.3500 | No breaking changes; Added the **FMatrix** and **FVector** classes from another internal library; **Translate** method has been added to the **FArea** class; **IsEmpty** method is now available on all geometric descriptor classes; **FArea** class now fully supports negative widths and heights. |
| 25.1116.3952 | No breaking changes; Re-activated complete support for intuitive orientation of angular functions by introducing the **DrawingSpaceEnum** enumeration; Re-appreciated the **WindingOrientationEnum** enumeration to allow all angular methods to express an intuitive direction, in conjunction with a **DrawingSpaceEnum** value, where appropriate. |
| 25.1114.3932 | Added *Circle* class with the methods **GetArcBoundingBox**, **GetQuadrant**, **GetQuadrantCrossings**, and **GetQuadrantsOccupied**; Marked **WindingOrientationEnum** as depreciated because the terms clockwise and counterclockwise are subjective according to the drawing space to which they are applied - please use **ArcDirectionEnum** for most related purposes; Changed GetInsideParallelLine(FPoint, FPoint, **WindingOrientationEnum**, float) to GetInsideParallelLine(FPoint, FPoint, **ArcDirectionEnum**, float) to promote compatibility with both Display space and Cartesian space; Corrected the output of **Trig.GetPathOrientation** method to return the angle in terms of the natural angular progression of an arc to make it compatible with both Display and Cartesian spaces. |
| 25.1110.4639 | Added static methods **GetCubicBoundingBox**, **GetLinearBoundingBox**, **GetQuadraticBoundingBox** to the *Bezier* class; Added **Clone** methods to the *FArea*, *FLine*, *FPath*, *FPoint*, *FScale*, and *FSize* classes; Added static method **GetLinePoint** to the *Linear* class; Added the **NormalizeRad** method to replace the depreciated **ReduceRad** method in the *Trig* class. |
| 25.1108.3829 | Rename all **Assign** methods to **TransferValues** |
| 25.1105.1217 | Initial public release. |


## More Information

For more information, please see the GitHub project:
[danielanywhere/Geometry](https://github.com/danielanywhere/Geometry)

Full API documentation is available at this library's [GitHub User Page](https://danielanywhere.github.io/Geometry).

