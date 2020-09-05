@echo off
setlocal

set nuspec=%~1
set config=%~2
set targetDir=%~3

set return=goto :eof
set tempfile=%temp%\oledump_nuget.txt

rem Check the args
if "%nuspec%"=="" goto :syntax
if "%config%"=="" goto :syntax
if "%targetDir%"=="" goto :syntax


rem locate nuget
where /R "%userprofile%\.nuget\packages\nuget.commandline" nuget.exe | sort > %tempfile%
if errorlevel 1 (
	call :errorMessage "Could not find nuget.exe"
	goto :error
)
for /f "tokens=*" %%f in ('type "%tempfile%"') do set nuget="%%f"

echo %targetdir%

%nuget% pack "%nuspec%" -Properties "Configuration=%config%" -OutputDirectory "%targetDir%"
if errorlevel 1 (
	call :errorMessage "Nuget failed to pack. See the output window for more details."
	goto :error
)

%return%

:syntax
echo Syntax:
echo   %~nx0 nuspecFile configuration targetDir
goto :error

rem Write a Visual Studio error window compatible error message
:errorMessage
echo %~dpnx0: error: %~1
%return%


:error
exit /b 1