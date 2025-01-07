# GetSlope(FPoint, FPoint) Method


Return the slope of the line expressed by two points.



## Definition
**Namespace:** <a href="eb409b48-e279-bdb4-daf3-3196b72d55a2.md">Geometry</a>  
**Assembly:** Geometry (in Geometry.dll) Version: 25.1105.1217+b6bb0e7fb3781583b73a2836dfe45a1dcf927a94

**C#**
``` C#
public static float GetSlope(
	FPoint pointA,
	FPoint pointB
)
```
**VB**
``` VB
Public Shared Function GetSlope ( 
	pointA As FPoint,
	pointB As FPoint
) As Single
```



#### Parameters
<dl><dt>  <a href="477a6142-7b25-5977-263a-a8e4e3c4f582.md">FPoint</a></dt><dd>The first end of the line to measure.</dd><dt>  <a href="477a6142-7b25-5977-263a-a8e4e3c4f582.md">FPoint</a></dt><dd>The second end of the line to measure.</dd></dl>

#### Return Value
<a href="https://learn.microsoft.com/dotnet/api/system.single" target="_blank" rel="noopener noreferrer">Single</a>  
The algebraic slope of the line (y2 - y1) / (x2 - x1).

## See Also


#### Reference
<a href="fc9e4d24-8cf6-ad7a-adef-13dc5a0936f6.md">SlopeInterceptItem Class</a>  
<a href="01ba5b74-55a4-cbbf-78d5-93380d114427.md">GetSlope Overload</a>  
<a href="eb409b48-e279-bdb4-daf3-3196b72d55a2.md">Geometry Namespace</a>  
