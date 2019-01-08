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

mkdir -p %{buildroot}%{DOTNET_ASSEMBLY_PATH}

install -p -m 644 artifacts/bin/OpenTK.dll %{buildroot}%{DOTNET_ASSEMBLY_PATH}
install -p -m 644 artifacts/bin/OpenTK.pdb %{buildroot}%{DOTNET_ASSEMBLY_PATH}

mkdir -p %{buildroot}/nuget
install -p -m 644 artifacts/OpenTK.*.nupkg %{buildroot}/nuget

%files
%license License.txt
%manifest %{name}.manifest
%attr(644,root,root) %{DOTNET_ASSEMBLY_PATH}/*.dll

%files nuget
%attr(644,root,root) /nuget/*.nupkg

%files debug
%attr(644,root,root) %{DOTNET_ASSEMBLY_PATH}/*.pdb
