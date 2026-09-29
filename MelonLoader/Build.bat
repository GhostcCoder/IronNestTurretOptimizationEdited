@echo off
setlocal
cd /d "%~dp0"
echo ================================================================
echo Iron Nest Turret Optimization - MelonLoader Port v10 - Round Persistence
echo Build
echo ================================================================
echo.
if not exist "Lib\Game\Assembly-CSharp.dll" (
  echo [ERROR] Game references are not set up. Run SetupGameAssemblies.bat first.
  pause
  exit /b 1
)
if not exist "Lib\GameReferences.props" (
  echo [ERROR] GameReferences.props is missing. Run SetupGameAssemblies.bat first.
  pause
  exit /b 1
)
if not exist "Lib\MelonLoader\MelonLoader.dll" (
  echo [ERROR] MelonLoader references are not set up. Run SetupGameAssemblies.bat first.
  pause
  exit /b 1
)

dotnet build "IronNestTurretOptimization-MelonLoader.csproj" -c Release --nologo
if errorlevel 1 (
  echo.
  echo BUILD FAILED.
  pause
  exit /b 1
)
echo.
echo [OK] Built: Build\IronNestTurretOptimization.dll
pause
