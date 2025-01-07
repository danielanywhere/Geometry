# SlopeInterceptItem Class


Expression of a line in slope intercept form.



## Definition
**Namespace:** <a href="eb409b48-e279-bdb4-daf3-3196b72d55a2.md">Geometry</a>  
**Assembly:** Geometry (in Geometry.dll) Version: 25.1105.1217+b6bb0e7fb3781583b73a2836dfe45a1dcf927a94

**C#**
``` C#
public class SlopeInterceptItem
```
**VB**
``` VB
Public Class SlopeInterceptItem
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  SlopeInterceptItem</td></tr>
</table>



## Constructors
<table>
<tr>
<td><a href="1173e6ca-9717-06f0-29f7-bf4940b8f350.md">SlopeInterceptItem(FLine)</a></td>
<td>Create a new Instance of the SlopeInterceptItem Item.</td></tr>
<tr>
<td><a href="a3a75613-6c6d-bbed-828c-abf70c74c3c1.md">SlopeInterceptItem(FPoint, FPoint)</a></td>
<td>Create a new Instance of the SlopeInterceptItem Item.</td></tr>
</table>

## Properties
<table>
<tr>
<td><a href="0fe602b7-9beb-34c7-70fb-994f6aeceb0f.md">B</a></td>
<td>Get/Set the value of the b member of y = mx + b.</td></tr>
<tr>
<td><a href="c8d34b4d-4640-07c8-b6f3-344a869d1356.md">BaseLine</a></td>
<td>Get/Set the base line to use in the case that one of slopes is vertical.</td></tr>
<tr>
<td><a href="f6f01fb8-86a1-b56c-8f42-c93031cb177c.md">Intercept</a></td>
<td>Get/Set the intercept (b) value of this item.</td></tr>
<tr>
<td><a href="a68c20c6-c81c-4b07-9618-8a8713a005f7.md">M</a></td>
<td>Get/Set the m property of y = mx + b.</td></tr>
<tr>
<td><a href="e74be563-fe3d-61d2-41b3-d4da307f0d1c.md">Slope</a></td>
<td>Get/Set the slope (m) of this item.</td></tr>
<tr>
<td><a href="45d0901f-2d95-d68c-dc3e-15d28ca7d11d.md">X</a></td>
<td>Get/Set the x member of y = mx + b.</td></tr>
<tr>
<td><a href="7264d811-f9c7-d619-35fd-36a5fa9e3a6e.md">Y</a></td>
<td>Get/Set the y member of y = mx + b.</td></tr>
</table>

## Methods
<table>
<tr>
<td><a href="5a8768f8-ce18-bd4d-5dff-fdcb9735b8e2.md">Convert(SlopeInterceptItem, FLine)</a></td>
<td>Set the caller's slope intercept values to represent the specified line in the form of y = mx + b.</td></tr>
<tr>
<td><a href="e288f9d8-12e9-96b4-8743-467cfa4d9051.md">Convert(SlopeInterceptItem, FPoint, FPoint)</a></td>
<td>Set the caller's slope intercept values to represent the specified line in the form of y = mx + b.</td></tr>
<tr>
<td><a href="https://learn.microsoft.com/dotnet/api/system.object.equals#system-object-equals(system-object)" target="_blank" rel="noopener noreferrer">Equals</a></td>
<td><br />(Inherited from <a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>)</td></tr>
<tr>
<td><a href="https://learn.microsoft.com/dotnet/api/system.object.finalize" target="_blank" rel="noopener noreferrer">Finalize</a></td>
<td><br />(Inherited from <a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>)</td></tr>
<tr>
<td><a href="https://learn.microsoft.com/dotnet/api/system.object.gethashcode" target="_blank" rel="noopener noreferrer">GetHashCode</a></td>
<td><br />(Inherited from <a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>)</td></tr>
<tr>
<td><a href="b3f672bf-32a2-920e-5d23-78c017908e1a.md">GetSlope(FLine)</a></td>
<td>Return the slope of the provided line.</td></tr>
<tr>
<td><a href="eb9d1dbb-54dc-d87c-6eec-7e75a7b94b60.md">GetSlope(FPoint, FPoint)</a></td>
<td>Return the slope of the line expressed by two points.</td></tr>
<tr>
<td><a href="https://learn.microsoft.com/dotnet/api/system.object.gettype" target="_blank" rel="noopener noreferrer">GetType</a></td>
<td><br />(Inherited from <a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>)</td></tr>
<tr>
<td><a href="da906495-1395-0985-619a-1557573029f6.md">HasIntersection</a></td>
<td>Return a value indicating whether the linear slope intercept intersects with the specified real line.</td></tr>
<tr>
<td><a href="0ecfb8fd-c38d-66ce-e2ee-c2e188e7adee.md">Intersect</a></td>
<td>Calculate an intersection between the two lines and return the result as a point.</td></tr>
<tr>
<td><a href="https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone" target="_blank" rel="noopener noreferrer">MemberwiseClone</a></td>
<td><br />(Inherited from <a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>)</td></tr>
<tr>
<td><a href="https://learn.microsoft.com/dotnet/api/system.object.tostring" target="_blank" rel="noopener noreferrer">ToString</a></td>
<td><br />(Inherited from <a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>)</td></tr>
</table>

## See Also


#### Reference
<a href="eb409b48-e279-bdb4-daf3-3196b72d55a2.md">Geometry Namespace</a>  
