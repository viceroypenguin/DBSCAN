DBSCAN
=====

This is an implementation of the DBSCAN clustering algorithm in .NET. The primary
advantage of this library over other DBSCAN implementations is that this library
allows the use of spatial indexes, and is agnostic to the index. 

Most implementations of DBSCAN use an O(N) search over every data point to find 
nearby data points. Sincethis search is executed for every point, the overall 
clustering time is O(N<sup>2</sup>). This is fine for smaller data sets, but
better tools are needed to review larger data sets (say 100,000 points).

If we can provide a better index for searching for nearby data points, then we
can reduce the overall time for clustering. For example, using an R-Tree or a 
K-d tree can reduce time to find neighbors to O(log n), which cuts the overall
clustering to O(n log n).

A default spatial index using the above is provided in the primary library as
`ListSpatialIndex<>`. Alternatively, an R-Tree library is available in the 
secondary library as `RBushSpatialIndex<>`. 


[![Build status](https://ci.appveyor.com/api/projects/status/gd5mg5pfp5ho6607/branch/master?svg=true)](https://ci.appveyor.com/project/viceroypenguin/dbscan/branch/master)

## Install

Install with Nuget:
 * Primary library: `Install-Package DBSCAN`
 * Secondary library: `Install-Package DBSCAN.RBush`

## Usage

Convenience functions have been provided in both libraries. Call the 
`CalculateClusters()` function in either `DBSCAN` or `DBSCAN.RBush`.

```csharp
var clusters = DBSCAN.CalculateClusters(
    data,
    epsilon: 1.0,
    minimumPointsPerCluster: 4);
```
or
```csharp
var clusters = DBSCANRBush.CalculateClusters(
    data,
    epsilon: 1.0,
    minimumPointsPerCluster: 4);
```

If you have implemented an alternative `ISpatialIndex<T>`, then you 
can provide the index directly to `DBSCAN.CalculateClusters()`:

```csharp
var clusters = DBSCAN.CalculateClusters(
    index,
    epsilon: 1.0,
    minimumPointsPerCluster: 4);
```


## Development

Clone the repository and open `DBSCAN.sln` in Visual Studio.

## Compatibility

DBSCAN should run on any .NET system that supports .NET Standard 1.2 (.NET Framework 4.5.1 or later; .NET Core 1.0 or later).


