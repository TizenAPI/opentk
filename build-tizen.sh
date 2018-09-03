#!/bin/bash

CONFIGURATION=Release

# Build tools
dotnet build -c $CONFIGURATION src/Generator.Bind/Generator.Bind.NETCore.csproj
dotnet build -c $CONFIGURATION src/Generator.Rewrite/Generator.Rewrite.NETCore.csproj

# Run Binding
dotnet src/Generator.Bind/bin/$CONFIGURATION/netcoreapp2.0/Bind.dll

# Build OpenTK
dotnet build -c $CONFIGURATION src/OpenTK/OpenTK.Tizen.csproj

# Pack OpenTK.Tizen
dotnet pack --no-build -c $CONFIGURATION src/OpenTK/OpenTK.Tizen.csproj
