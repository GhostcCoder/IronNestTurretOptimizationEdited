@echo off
setlocal EnableExtensions EnableDelayedExpansion
cd /d "%~dp0"

echo ================================================================
echo Iron Nest Turret Optimization - BepInEx Round Persistence
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
if not exist "%GAME_DIR%\BepInEx\core\BepInEx.Core.dll" (
  echo [ERROR] BepInEx core files were not found:
  echo         %GAME_DIR%\BepInEx\core
  pause
  exit /b 1
)
if not exist "%GAME_DIR%\BepInEx\interop\Assembly-CSharp.dll" (
  echo [ERROR] Assembly-CSharp.dll was not found:
  echo         %GAME_DIR%\BepInEx\interop
  echo.
  echo Start the game with BepInEx once so IL2CPP interop assemblies are generated.
  pause
  exit /b 1
)

if exist "Lib\BepInExCore" rmdir /S /Q "Lib\BepInExCore"
if exist "Lib\Interop" rmdir /S /Q "Lib\Interop"
mkdir "Lib\BepInExCore"
mkdir "Lib\Interop"

for %%F in (
  0Harmony.dll
  BepInEx.Core.dll
  BepInEx.Unity.IL2CPP.dll
  Il2CppInterop.Runtime.dll
) do (
  if not exist "%GAME_DIR%\BepInEx\core\%%F" (
    echo [ERROR] Missing BepInEx reference: %%F
    pause
    exit /b 1
  )
  copy /Y "%GAME_DIR%\BepInEx\core\%%F" "Lib\BepInExCore\%%F" >nul
)

for %%F in (
  Assembly-CSharp.dll
  Il2Cppmscorlib.dll
  Unity.InputSystem.dll
  UnityEngine.AnimationModule.dll
  UnityEngine.CoreModule.dll
  UnityEngine.InputModule.dll
) do (
  if not exist "%GAME_DIR%\BepInEx\interop\%%F" (
    echo [ERROR] Missing interop reference: %%F
    pause
    exit /b 1
  )
  copy /Y "%GAME_DIR%\BepInEx\interop\%%F" "Lib\Interop\%%F" >nul
)

echo.
echo [OK] BepInEx references copied.
echo [OK] Game interop references copied.
echo.
echo Next run Build.bat
pause
