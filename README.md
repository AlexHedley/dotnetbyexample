# .NET By Example

[![.NET](https://img.shields.io/badge/.NET-%23512BD4.svg?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/c%23-239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![F#](https://img.shields.io/badge/F%23-3498DB?style=for-the-badge&logo=fsharp&logoColor=white)](https://fsharp.org)
[![VB.NET](https://img.shields.io/badge/VB.NET-512BD4?style=for-the-badge&logo=visualbasic&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/visual-basic/)
[![License: MIT](https://img.shields.io/badge/License-MIT-lightgrey.svg?style=for-the-badge)](LICENSE) <!-- https://opensource.org/licenses/MIT -->
[![Dependabot](https://img.shields.io/badge/dependabot-025E8C?style=for-the-badge&logo=dependabot&logoColor=white)](https://github.com/AlexHedley/dotnetbyexample/security/dependabot)
[![Dependency-Check](https://img.shields.io/badge/DependencyCheck-f78d0a.svg?style=for-the-badge&logo=dependencycheck&logoColor=white)](https://alexhedley.github.io/dotnetbyexample/reports/dependency-check-report.html)

[![Dependabot Updates](https://github.com/AlexHedley/dotnetbyexample/actions/workflows/dependabot/dependabot-updates/badge.svg)](https://github.com/AlexHedley/dotnetbyexample/actions/workflows/dependabot/dependabot-updates)
[![Dependency Check](https://github.com/AlexHedley/dotnetbyexample/actions/workflows/depcheck.yml/badge.svg)](https://github.com/AlexHedley/dotnetbyexample/actions/workflows/depcheck.yml)

> Content and build toolchain for _.NET by Example_, a site that teaches .NET (C#, F#, VB.NET) via annotated example programs/scripts.

## Overview

The .NET by Example site is built by extracting code and comments from source files in [examples](examples/) and rendering them using templates into a static public directory. The programs implementing this build process are in tools, along with dependencies specified in the [.csproj](src/dotnetbyexample/dotnetbyexample.csproj).

The built public directory can be served by any static content system. The production site uses [GitHub](https://github.com) [Pages](https://docs.github.com/en/pages), for example - https://alexhedley.github.io/dotnetbyexample/.

Inspired by [Go by Example](https://gobyexample.com/) ([Source](https://github.com/mmcgrana/gobyexample))

## examples

A 📂 folder containing a `.csx`, `.vb`, `.fs` (and `.bat` with run instructions) file complete with code and code comments is converted to a corresponding `.html` file.

- 📂[examples](examples/)

Please raise a new 💡[Discussion](https://github.com/AlexHedley/dotnetbyexample/discussions/new?category=ideas) with any ideas for another example, then MRs are welcome :).

## src

- 📂[src](src/)

## build

`cd src`

`dotnet build --configuration Release`

## run

Locally

`dotnet run --project src/dotnetbyexample/dotnetbyexample.csproj`

> src\dotnetbyexample\bin\Release\net9.0\dotnetbyexample.exe

## docs

- 📂[docs](docs/README.md)

## Dependencies

If you wish to run the examples locally you can use `dotnet script`, although see [Announcing dotnet run app.cs – A simpler way to start with C# and .NET 10](https://devblogs.microsoft.com/dotnet/announcing-dotnet-run-app/) and I will update the scripts over time.

**dotnet script**

https://github.com/dotnet-script/dotnet-script

`dotnet tool install -g dotnet-script`

`dotnet tool list -g`

`dotnet tool uninstall dotnet-script -g`
