ECHO ON
SET CURRENT_DIR=%~dp0
SET SOLUTION_DIR=%CURRENT_DIR%..
SET SOLUTION_PATH=%SOLUTION_DIR%\DinerClub.sln
SET MSBUILD_PART_PATH=MSBuild\12.0\Bin\MSBuild.exe
SET MSBUILD_PATH=%ProgramFiles(x86)%%MSBUILD_PART_PATH%

cd "%SOLUTION_DIR%"
"%MSBUILD_PATH%" "%SOLUTION_PATH% /target:Resources;Compile"
PAUSE;
