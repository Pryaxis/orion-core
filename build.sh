#!/bin/bash

set -e
dotnet --info
dotnet restore

dotnet build -f netstandard2.0 -c Release src/Orion/Orion.csproj
dotnet test -f netcoreapp2.2 -c Release tests/Orion.Tests/Orion.Tests.csproj