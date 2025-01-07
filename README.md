# Geometry Library

An easy-to-implement function library providing numeric operations to
handle solutions to typical problems encountered in the field of
geometry, including Bezier Curves, Linear Interpolation (Lerp), and
Trigonometry.

<p>&nbsp;</p>

## Table of Contents

You can jump to any section of this page from the following list.

-   [25 Years of .NET](#_25_Years_of).

-   [Yet Another Geometry Library](#yet-another-geometry-library).

-   [Installation](#installation).

-   [Usage Notes](#usage-notes).

<p>&nbsp;</p>

## 25 Years of .NET

Although .NET doesn't officially turn 25 until February 13, 2027, I'm
starting the celebration a little early.

To commemorate 25 years since the public release of the .NET framework,
I'm open sourcing this and several other of my long-lived libraries and
applications. Most of these have only previously been used privately in
our own internal company productivity during the early 21st century but
I hope they might find a number of new uses to complete the next 25
years.

I have every intention of keeping these libraries and applications
maintained, so if you happen to run into anything you would like to see
added, changed, or repaired, just let me know in the Issues section and
I'll get it done for you as time permits.

<p>&nbsp;</p>

Sincerely,

**Daniel Patterson, MCSD (danielanywhere)**

<p>&nbsp;</p>

## Yet Another Geometry Library

Geometry is a surprisingly persistent companion in my work. Time and
again, I find myself tackling scenarios where geometric functionality is
not just helpful - it’s essential. Whether it’s converting vectors to
absolute coordinates, calculating the length of a guy wire with a
Bézier-shaped sag, or determining the number of passes required for a
mill bit to clear an area bounded by a polyline, geometry is always at
the core of the solution. These repeated encounters inspired me to
develop and maintain this geometric function library.

This library has a long history, originating in 1997 when I first
created it in the VBA scripting language to extend Microsoft Excel’s
capabilities through their Office Automation extensions. It quickly
became evident that its utility extended far beyond those early roots,
prompting me to convert the library to C# in 2001. Since then, it has
been an integral tool in my internal .NET projects, continuously
evolving to meet new challenges.

The current version introduces three key components:

1.  **Trig Class**: Equipped with all the standard tools for working
    with angles and trigonometric functions, it offers seamless support
    for angular computations.

2.  **Linear Class**: Focused on basic linear interpolation, this class
    includes overloads of the Lerp method, making it simple to
    interpolate scalar values or points with precision.

3.  **Bezier Class**: A high-performance, lightweight module for
    identifying points along any type of Bézier curve. Whether your
    focus is precision or speed, this class delivers both, enabling
    quick and efficient calculations even for complex curves.

Through these features, this library aims to simplify and streamline
geometric operations, empowering you to tackle projects with confidence
and ease.

Other related classes from around our software environment will be added
soon to this library, so stay tuned...

<p>&nbsp;</p>

## Installation

You can include this library in any .NET project using any supported
programming language or target system. This library compiles as **.NET
Standard 2.0** and is available in **NuGet** as

<center><b><h3>Dan's Geometry Library</h3></b></center>

<p>&nbsp;</p>

**Instructions For Installation**

In **Visual Studio Community** or **Visual Studio Professional**
editions:

-   Right-click your project name in **Solution Explorer**.

-   From the context menu, select **Manage NuGet Packages**.

-   Click **Browse**.

-   In the **Search** textbox, type **Dan's Geometry Library**.

-   Accept the license agreement.

-   In your code add the header line **using Geometry;**

<p>&nbsp;</p>

In **Visual Studio Code**:

-   Run the command **NuGet: Add NuGet Package** (typically
    \[Ctrl\]\[Shift\]\[P\]).

-   If there are multiple projects in the solution, select the open
    project to which the package will be assigned.

-   In the **Search** textbox, type **Dan's Geometry Library**.

-   Select the package.

-   Select the version you wish to apply.

<p>&nbsp;</p>

## Usage Notes

This library is intended to be used on any target system, avoiding any
kind of Windows dependencies whatsoever. As a result, replacements for
GDI+ dimensional objects, like Point, Rectangle, and others normally
found in .NET system libraries like System.Drawing have been defined for
generic public use with or without a statically typed graphics system.

For the full documentation of this library, please see this Wiki page.
