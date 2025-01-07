# Scale(FArea, FArea) Method


Return the scale of the two areas.



## Definition
**Namespace:** <a href="eb409b48-e279-bdb4-daf3-3196b72d55a2.md">Geometry</a>  
**Assembly:** Geometry (in Geometry.dll) Version: 25.1105.1217+b6bb0e7fb3781583b73a2836dfe45a1dcf927a94

**C#**
``` C#
public static FScale Scale(
	FArea newArea,
	FArea oldArea
)
```
**VB**
``` VB
Public Shared Function Scale ( 
	newArea As FArea,
	oldArea As FArea
) As FScale
```



#### Parameters
<dl><dt>  <a href="bb9e7df7-af91-41d9-e4eb-f0500ec02002.md">FArea</a></dt><dd>Reference to the new area (numerator).</dd><dt>  <a href="bb9e7df7-af91-41d9-e4eb-f0500ec02002.md">FArea</a></dt><dd>Reference to the old or original area (denominator).</dd></dl>

#### Return Value
<a href="8751e565-0ebe-e38a-1423-a8beec9293ee.md">FScale</a>  
Reference to the newly created scale containing the scaling factor between the caller's two areas.

## See Also


#### Reference
<a href="bb9e7df7-af91-41d9-e4eb-f0500ec02002.md">FArea Class</a>  
<a href="48f780ea-7d83-f6f8-caf0-988251d8bf7b.md">Scale Overload</a>  
<a href="eb409b48-e279-bdb4-daf3-3196b72d55a2.md">Geometry Namespace</a>  
