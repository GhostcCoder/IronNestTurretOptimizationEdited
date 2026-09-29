@echo off
setlocal
cd /d "%~dp0"
echo ================================================================
echo Install Turret Optimization - MelonLoader Round Persistence
echo ================================================================
set "GAME_DIR="
set /p "GAME_DIR=Enter the full game directory path: "
set "GAME_DIR=%GAME_DIR:"=%"

echo.
set "DLL=%~dp0Build\IronNestTurretOptimization.dll"
if not exist "%DLL%" (
  echo [ERROR] Build\IronNestTurretOptimization.dll was not found. Run Build.bat first.
  pause
  exit /b 1
)
if not defined GAME_DIR (
  echo [ERROR] No game directory was entered.
  pause
  exit /b 1
)
if not exist "%GAME_DIR%\Mods" (
  echo [ERROR] Mods folder was not found:
  echo         %GAME_DIR%\Mods
  pause
  exit /b 1
)
copy /Y "%DLL%" "%GAME_DIR%\Mods\IronNestTurretOptimization.dll" >nul
if errorlevel 1 (
  echo [ERROR] DLL copy failed.
  pause
  exit /b 1
)
echo [OK] Installed to:
echo      %GAME_DIR%\Mods\IronNestTurretOptimization.dll
pause
