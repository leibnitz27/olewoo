@echo off
setlocal EnableExtensions
set return=goto :eof
set checkError=if errorlevel 1 goto :error

cd /d "%~dp0"

echo Getting the version number from version.txt
call :getNewVersion newVersion
%checkError%

set newVersionCommas=%newVersion:.=,%

rem Non-optimal regexes: useful characters for OR, lookbehind and " are troublesome in batch scripting
set versionNumberRegex=\d+(?:\.\d+){2,3}
set assemblyVersionRegex=(Assembly(?:File)?Version(?:Attribute)?\(.)%versionNumberRegex%
set assemblyVersionReplacement=${1}%newVersion%
set rcVersionInfoRegex=((?:[FILEPRODUCT]+)VERSION\s+)\d+(?:,\d+){3}
set rcVersionInfoReplacement=${1}%newVersion:.=,%
set rcDetailsRegex=(VALUE\s+.[FileProduct]+Version.,\s*\S)%versionNumberRegex%(.)
set rcDetailsReplacement=${1}%newVersion%$2
set nuspecVersionRegex=(.version.)%versionNumberRegex%(./version.)
set nuspecVersionReplacement=${1}%newVersion%${2}
set wixVersionRegex=(.\?define\s+OLEWOO_VERSION\s*=\s*.)%versionNumberRegex%(.\s*\?.)
set wixVersionReplacement=${1}%newVersion%${2}

echo Updating oledump
call :regexReplace "..\oledump\Properties\AssemblyInfo.cs" "%assemblyVersionRegex%" "%assemblyVersionReplacement%" utf8
%checkError%

echo Updating olewoo_cs
call :regexReplace "..\olewoo_cs\Properties\AssemblyInfo.cs" "%assemblyVersionRegex%" "%assemblyVersionReplacement%" utf8
%checkError%

echo Updating olewoo_ui
call :regexReplace "..\olewoo_ui\Properties\AssemblyInfo.cs" "%assemblyVersionRegex%" "%assemblyVersionReplacement%" utf8
%checkError%

echo Updating olewoo_interop 
call :regexReplace "..\olewoo_interop\AssemblyInfo.cpp" "%assemblyVersionRegex%" "%assemblyVersionReplacement%" utf8
%checkError%

call :regexReplace "..\olewoo_interop\app.rc" "%rcDetailsRegex%" "%rcDetailsReplacement%" unicode
%checkError%

call :regexReplace "..\olewoo_interop\app.rc" "%rcVersionInfoRegex%" "%rcVersionInfoReplacement%" unicode
%checkError%

echo Updating nuget packager
call :regexReplace "..\oledump_nuget\OleWoo.OleDump.nuspec" "%nuspecVersionRegex%" "%nuspecVersionReplacement%" utf8
%checkError%

echo Updating installer project
call :regexReplace "..\olewoo_installer\Version.wxi" "%wixVersionRegex%" "%wixVersionReplacement%" utf8
%checkError%

echo Done.

%return%

:getNewVersion
rem sets the environment variable specified by the first argument to the last value found in the version.txt file
for /f "tokens=*" %%f in ('type %~dp0\version.txt') do set %1=%%f
if "%newVersion%"=="" exit /b 1
%return%


:regexReplace
rem %1 input file path
rem %2 search pattern
rem %3 replacement pattern
rem %4 output encoding
set tempFile=%temp%\olewoo.tmp
powershell (Get-Content -path "%~1" -Raw -Encoding utf8) -replace '%~2','%~3' ^| out-file "%tempFile%" -encoding %~4 -NoNewline
if %errorlevel% NEQ 0 exit /b 1

copy "%tempFile%" "%~1" > nul
if errorlevel 1 exit /b 1

%return%

:error
echo ERROR
exit /b 1

