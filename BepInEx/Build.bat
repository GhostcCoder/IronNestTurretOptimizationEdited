@echo off
setlocal
cd /d "%~dp0"
echo ================================================================
echo Iron Nest Turret Optimization - BepInEx Round Persistence
echo Build
echo ================================================================
echo.
if not exist "Lib\BepInExCore\BepInEx.Core.dll" (
  echo [ERROR] BepInEx references are not set up. Run SetupGameAssemblies.bat first.
  pause
  exit /b 1
)
if not exist "Lib\Interop\Assembly-CSharp.dll" (
  echo [ERROR] Game interop references are not set up. Run SetupGameAssemblies.bat first.
  pause
  exit /b 1
)

dotnet build "IronNestTurretOptimization\IronNestTurretOptimization.csproj" -c Release --nologo
if errorlevel 1 (
  echo.
  echo BUILD FAILED.
  pause
  exit /b 1
)
if exist "Build\IronNestTurretOptimization.dll" (
  echo.
  echo [OK] Built: Build\IronNestTurretOptimization.dll
) else (
  echo.
  echo [ERROR] Build completed but DLL was not found.
  pause
  exit /b 1
)
pause
