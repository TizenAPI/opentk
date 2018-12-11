Name:       opentk
Summary:    C# binding for OpenGL, OpenGL ES
Version:    3.0.3
Release:    1
Group:      Development/Libraries
License:    MIT
URL:        https://github.com/opentk/opentk/
Source0:    %{name}-%{version}.tar.gz
Source1:    %{name}.manifest

BuildArch:   noarch
ExcludeArch: aarch64
AutoReqProv: no

BuildRequires: dotnet-build-tools
BuildRequires: csapi-tizenfx-nuget

%description
The Open Toolkit library is a fast, low-level C# binding for OpenGL, OpenGL ES and OpenAL.
It runs on all major platforms and powers hundreds of apps, games and scientific research.

%package nuget
Summary:   NuGet package for %{name}
Group:     Development/Libraries
AutoReqProv: no

%description nuget
NuGet package for %{name}

%package debug
Summary:   All .pdb files of Tizen .NET
Group:     Development/Libraries
AutoReqProv: no

%description debug
All .pdb files of Tizen .NET


%prep
%setup -q
cp %{SOURCE1} .


%build
%{?asan:export ASAN_OPTIONS=use_sigaltstack=false:allow_user_segv_handler=true:handle_sigfpe=false:`cat /ASAN_OPTIONS`}

./build-tizen.sh


%install

%define DOTNET_ASSEMBLY_PATH /usr/share/dotnet.tizen/framework
%define DOTNET_ASSEMBLY_DRAFT_PATH /usr/share/dotnet.tizen/framework/draft

mkdir -p %{buildroot}%{DOTNET_ASSEMBLY_DRAFT_PATH}

install -p -m 644 artifacts/bin/OpenTK.dll %{buildroot}%{DOTNET_ASSEMBLY_DRAFT_PATH}
install -p -m 644 artifacts/bin/OpenTK.pdb %{buildroot}%{DOTNET_ASSEMBLY_DRAFT_PATH}

mkdir -p %{buildroot}/nuget/draft
install -p -m 644 artifacts/OpenTK.*.nupkg %{buildroot}/nuget/draft


%post
rm -f %{DOTNET_ASSEMBLY_PATH}/OpenTK.dll
cp -a %{DOTNET_ASSEMBLY_DRAFT_PATH}/OpenTK.dll %{DOTNET_ASSEMBLY_PATH}/OpenTK.dll

%post debug
rm -f %{DOTNET_ASSEMBLY_PATH}/OpenTK.pdb
cp -a %{DOTNET_ASSEMBLY_DRAFT_PATH}/OpenTK.pdb %{DOTNET_ASSEMBLY_PATH}/OpenTK.pdb

%post nuget
rm -f /nuget/OpenTK.*.nupkg
cp -a /nuget/draft/OpenTK.*.nupkg /nuget/

%files
%license License.txt
%manifest %{name}.manifest
%attr(644,root,root) %{DOTNET_ASSEMBLY_DRAFT_PATH}/*.dll

%files nuget
%attr(644,root,root) /nuget/draft/*.nupkg

%files debug
%attr(644,root,root) %{DOTNET_ASSEMBLY_DRAFT_PATH}/*.pdb
