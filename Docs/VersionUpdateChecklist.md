# Version Update Checklist

Perform the following tasks when publishing a new version of *Dans.Geometry.Library*.

 - [ ] Make sure the project is in **Debug** mode.
 - [ ] Update the version number in **Geometry.csproj**.
 - [ ] Compile and test all changes to the library.
 - [ ] Switch the project mode to **Release** and compile.
 - [ ] Update **Source/NuGet/README.md**.
 - [ ] Open **Scripts/GeometryDocumentation.shfbproj** and update the version number in **HelpFileVersion**.
 - [ ] Open **Scripts/GeometryDocumentation.shfbproj** in SHFB and compile the new version of the API.
 - [ ] Check in API documentation on **danielanywhere.github.io**. Use the summary 'Geometry API updates for version {Version}'.
 - [ ] Check in source changes on **danielanywhere/Geometry**. Use the summary 'Updates for version {Version}'.
 - [ ] Upload new release version to **NuGet**.

