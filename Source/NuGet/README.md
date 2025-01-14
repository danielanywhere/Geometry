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
  }
 }
}

```

## Updates

| Version | Description |
|---------|-------------|
| 25.1114.3932 | Added *Circle* class with the methods **GetArcBoundingBox**, **GetQuadrant**, **GetQuadrantCrossings**, and **GetQuadrantsOccupied**; Marked **WindingOrientationEnum** as depreciated because the terms clockwise and counterclockwise are subjective according to the drawing space to which they are applied - please use **ArcDirectionEnum** for most related purposes; Changed GetInsideParallelLine(FPoint, FPoint, **WindingOrientationEnum**, float) to GetInsideParallelLine(FPoint, FPoint, **ArcDirectionEnum**, float) to promote compatibility with both Display space and Cartesian space; Corrected the output of **Trig.GetPathOrientation** method to return the angle in terms of the natural angular progression of an arc to make it compatible with both Display and Cartesian spaces.
| 25.1110.4639 | Added static methods **GetCubicBoundingBox**, **GetLinearBoundingBox**, **GetQuadraticBoundingBox** to the *Bezier* class; Added **Clone** methods to the *FArea*, *FLine*, *FPath*, *FPoint*, *FScale*, and *FSize* classes; Added static method **GetLinePoint** to the *Linear* class; Added the **NormalizeRad** method to replace the depreciated **ReduceRad** method in the *Trig* class. |
| 25.1108.3829 | Rename all **Assign** methods to **TransferValues** |
| 25.1105.1217 | Initial public release. |


## More Information

For more information, please see the GitHub project:
[danielanywhere/Geometry](https://github.com/danielanywhere/Geometry)

Full API documentation is available at this library's [GitHub User Page](https://danielanywhere.github.io/Geometry).

