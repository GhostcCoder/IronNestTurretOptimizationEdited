@echo off
setlocal EnableExtensions EnableDelayedExpansion
cd /d "%~dp0"

echo ================================================================
echo Iron Nest Turret Optimization - MelonLoader Round Persistence
echo Setup Game Assemblies
echo ================================================================
echo.
set "GAME_DIR="
set /p "GAME_DIR=Enter the full game directory path: "
set "GAME_DIR=%GAME_DIR:"=%"

echo.
if not defined GAME_DIR (
  echo [ERROR] No game directory was entered.
  pause
  exit /b 1
)
set "GAME_ASM=%GAME_DIR%\MelonLoader\Il2CppAssemblies"
set "MELON_ASM=%GAME_DIR%\MelonLoader\net6"

if not exist "%GAME_ASM%\Assembly-CSharp.dll" (
  echo [ERROR] Assembly-CSharp.dll was not found in:
  echo         %GAME_ASM%
  echo.
  echo Start the game with MelonLoader once so the IL2CPP assemblies are generated.
  pause
  exit /b 1
)
if not exist "%MELON_ASM%\MelonLoader.dll" (
  echo [ERROR] MelonLoader.dll was not found in:
  echo         %MELON_ASM%
  echo.
  echo Make sure MelonLoader has started the game at least once.
  pause
  exit /b 1
)

if exist "Lib\Game" rmdir /S /Q "Lib\Game"
if exist "Lib\MelonLoader" rmdir /S /Q "Lib\MelonLoader"
if exist "Lib\GameReferences.props" del /Q "Lib\GameReferences.props"
mkdir "Lib\Game"
mkdir "Lib\MelonLoader"

echo Copying generated IL2CPP game assemblies...
copy /Y "%GAME_ASM%\*.dll" "Lib\Game\" >nul
if errorlevel 1 (
  echo [ERROR] Failed to copy the generated game assemblies.
  pause
  exit /b 1
)

for %%F in (
  MelonLoader.dll
  0Harmony.dll
  Il2CppInterop.Runtime.dll
) do (
  if not exist "%MELON_ASM%\%%F" (
    echo [ERROR] Missing MelonLoader reference: %%F
    pause
    exit /b 1
  )
  copy /Y "%MELON_ASM%\%%F" "Lib\MelonLoader\%%F" >nul
)

echo Generating explicit MSBuild references...
(
  echo ^<Project^>
  echo   ^<ItemGroup^>
  for %%F in ("Lib\Game\*.dll") do (
    echo     ^<Reference Include="%%~nF"^>
    echo       ^<HintPath^>Lib\Game\%%~nxF^</HintPath^>
    echo       ^<Private^>false^</Private^>
    echo     ^</Reference^>
  )
  echo   ^</ItemGroup^>
  echo ^</Project^>
) > "Lib\GameReferences.props"

if not exist "Lib\GameReferences.props" (
  echo [ERROR] Failed to generate Lib\GameReferences.props
  pause
  exit /b 1
)

echo.
for /f %%C in ('dir /b "Lib\Game\*.dll" ^| find /c /v ""') do echo [OK] Game DLL count: %%C
 echo [OK] MelonLoader references copied.
echo [OK] Explicit MSBuild references generated.
echo.
echo Next run Build.bat
pause
