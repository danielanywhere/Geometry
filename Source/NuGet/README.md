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
| 25.1108.3829 | Rename all **Assign** methods to **TransferValues** |
| 25.1105.1217 | Initial public release. |


## More Information

For more information, please see the GitHub project:
[danielanywhere/Geometry](https://github.com/danielanywhere/Geometry)

Full API documentation is available at this library's [GitHub User Page](https://danielanywhere.github.io/Geometry).

