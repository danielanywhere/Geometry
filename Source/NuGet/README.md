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

   // 3D to 2D Direct World Projection.
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

