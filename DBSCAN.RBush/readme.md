DBSCAN.RBush
=====

This is a support library to link the DBSCAN algorithm with the RBush data structure
to improve the algorithmic cost of clustering items from O(N<sup>2</sup>) to O(N log N). 

This library builds on the code in both the [DBSCAN](https://www.nuget.org/packages/DBSCAN/)
and the [RBush](https://www.nuget.org/packages/RBush/) libraries. 

[![Build status](https://github.com/viceroypenguin/DBSCAN/actions/workflows/build.yml/badge.svg)](https://github.com/viceroypenguin/DBSCAN/actions)
[![License](https://img.shields.io/github/license/viceroypenguin/DBSCAN)](license)

DBSCAN.RBush: [![NuGet](https://img.shields.io/nuget/v/DBSCAN.RBush.svg?style=plastic)](https://www.nuget.org/packages/DBSCAN.RBush/)

## Install

Install with Nuget:
 * Secondary library: `Install-Package DBSCAN.RBush`

## Usage

Convenience functions have been provided in both libraries. Call the 
`CalculateClusters()` function in `DBSCAN.RBush`.

```csharp
var clusters = DbscanRBush.CalculateClusters(
    data,
    epsilon: 1.0,
    minimumPointsPerCluster: 4);
```

## Development

Clone the repository and open `DBSCAN.sln` in Visual Studio.

## Compatibility

DBSCAN should run on any .NET system that supports .NET Standard 1.2 (.NET Framework 4.5.1 or later; .NET Core 1.0 or later).


