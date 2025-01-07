# Intersect Method


Calculate an intersection between the two lines and return the result as a point.



## Definition
**Namespace:** <a href="eb409b48-e279-bdb4-daf3-3196b72d55a2.md">Geometry</a>  
**Assembly:** Geometry (in Geometry.dll) Version: 25.1105.1217+b6bb0e7fb3781583b73a2836dfe45a1dcf927a94

**C#**
``` C#
public static FPoint Intersect(
	SlopeInterceptItem lineA,
	SlopeInterceptItem lineB
)
```
**VB**
``` VB
Public Shared Function Intersect ( 
	lineA As SlopeInterceptItem,
	lineB As SlopeInterceptItem
) As FPoint
```



#### Parameters
<dl><dt>  <a href="fc9e4d24-8cf6-ad7a-adef-13dc5a0936f6.md">SlopeInterceptItem</a></dt><dd>Reference to the first line to intersect.</dd><dt>  <a href="fc9e4d24-8cf6-ad7a-adef-13dc5a0936f6.md">SlopeInterceptItem</a></dt><dd>Reference to the second line to intersect.</dd></dl>

#### Return Value
<a href="477a6142-7b25-5977-263a-a8e4e3c4f582.md">FPoint</a>  
Reference to a double point indicating where the caller's lines intersected, if an intersection was found. Otherwise, null.

## See Also


#### Reference
<a href="fc9e4d24-8cf6-ad7a-adef-13dc5a0936f6.md">SlopeInterceptItem Class</a>  
<a href="eb409b48-e279-bdb4-daf3-3196b72d55a2.md">Geometry Namespace</a>  
