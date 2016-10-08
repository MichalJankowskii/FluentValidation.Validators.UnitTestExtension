if exist %WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe set MSBUILD=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe
if exist "%ProgramFiles%\MSBuild\14.0\bin\msbuild.exe" set MSBUILD=%ProgramFiles%\MSBuild\14.0\bin\msbuild.exe
if exist "%ProgramFiles(x86)%\MSBuild\14.0\bin\msbuild.exe" set MSBUILD=%ProgramFiles(x86)%\MSBuild\14.0\bin\msbuild.exe


"%MSBUILD%" src/FluentValidation.Validators.UnitTestExtension.sln /property:Configuration=Publish