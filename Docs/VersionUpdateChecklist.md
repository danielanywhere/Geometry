# Version Update Checklist

Perform the following tasks when publishing a new version of *Dans.Geometry.Library*.

 - [ ] Make any changes necessary to **ReadMe.docx** and run **CreateReadMe.cmd**, if applicable.
 - [ ] Make sure the project is in **Debug** mode.
 - [ ] Update the version number in **Geometry.csproj**.
 - [ ] Compile and test all changes to the library.
 - [ ] Update **Source/NuGet/README.md**. This step is required prior to compile because the file is included in the NuGet package.
 - [ ] Switch the project mode to **Release** and compile.
 - [ ] Check the GitHub project online to make sure any issues to be addressed in this version have been completed.
 - [ ] Open **Scripts/GeometryDocumentation.shfbproj** and update the version number in **HelpFileVersion**.
 - [ ] Open **Scripts/GeometryDocumentation.shfbproj** in SHFB and compile the new version of the API.
 - [ ] Check in API documentation on **danielanywhere.github.io**. Use the summary 'Geometry API updates for version {Version}'.
 - [ ] Check in source changes on **danielanywhere/Geometry**. Use the summary 'Updates for version {Version}'.
 - [ ] Upload new release version to **NuGet**.
 - [ ] Update or close any associated GitHub issues.

