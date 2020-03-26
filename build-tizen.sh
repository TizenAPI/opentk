#!/bin/bash

SCRIPT_FILE=$(readlink -f $0)
SCRIPT_DIR=$(dirname $SCRIPT_FILE)

CONFIGURATION=Release

PROJECT_BIND=$SCRIPT_DIR/src/Generator.Bind/Generator.Bind.NETCore.csproj
PROJECT_REWRITE=$SCRIPT_DIR/src/Generator.Rewrite/Generator.Rewrite.NETCore.csproj
PROJECT_OPENTK=$SCRIPT_DIR/src/OpenTK/OpenTK.Tizen.csproj

BIND_EXE="dotnet $SCRIPT_DIR/src/Generator.Bind/bin/$CONFIGURATION/netcoreapp3.0/Bind.dll"

ARTIFACTS_DIR=$SCRIPT_DIR/artifacts

build() {
  if [ -d /nuget ]; then
    NUGET_SOURCE_OPT="-s /nuget -s $SCRIPT_DIR/packages"
  fi

  # Build tools
  dotnet restore $NUGET_SOURCE_OPT $PROJECT_BIND
  dotnet build --no-restore -c $CONFIGURATION $PROJECT_BIND

  dotnet restore $NUGET_SOURCE_OPT $PROJECT_REWRITE
  dotnet build --no-restore -c $CONFIGURATION $PROJECT_REWRITE

  # Run Binding
  $BIND_EXE -mode:es11
  $BIND_EXE -mode:es20

  # Build OpenTK
  dotnet restore $NUGET_SOURCE_OPT $PROJECT_OPENTK
  dotnet build --no-restore -c $CONFIGURATION $PROJECT_OPENTK

  # Pack OpenTK.Tizen
  dotnet pack --no-restore --no-build -c $CONFIGURATION $PROJECT_OPENTK

  # Artifacts
  mkdir -p $ARTIFACTS_DIR/bin/ref
  cp -f $SCRIPT_DIR/src/OpenTK/bin/$CONFIGURATION/netstandard2.0/OpenTK.* $ARTIFACTS_DIR/bin/
  cp -f $SCRIPT_DIR/src/OpenTK/bin/$CONFIGURATION/netstandard2.0/ref/* $ARTIFACTS_DIR/bin/ref/
  cp -f $SCRIPT_DIR/src/OpenTK/bin/$CONFIGURATION/OpenTK.Tizen.*.nupkg $ARTIFACTS_DIR
}

clean() {
  dotnet clean -c $CONFIGURATION $PROJECT_BIND
  dotnet clean -c $CONFIGURATION $PROJECT_REWRITE
  dotnet clean -c $CONFIGURATION $PROJECT_OPENTK

  rm -fr $ARTIFACTS_DIR
}


export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true

cmd=$1; shift

if [ "x$cmd" == "xclean" ]; then
  clean
else
  build
fi
