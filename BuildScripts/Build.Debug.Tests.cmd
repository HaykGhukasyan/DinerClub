ECHO ON
SET CURRENT_DIR=%~dp0
SET SOLUTION_DIR=%CURRENT_DIR%..
SET BIN_DIR=%SOLUTION_DIR%\Bin\Debug
SET APP_PATH=%BIN_DIR%\DinerClub.exe

ECHO ************************** Acceptance Tests *********************
ECHO Morning Orders Tests
ECHO #######################
"%APP_PATH%" morning, 1, 2, 3
"%APP_PATH%" morning, 2, 1, 3
"%APP_PATH%" morning, 1, 2, 3, 4
"%APP_PATH%" morning, 1, 2, 3, 3, 3

ECHO Night Orders Tests
ECHO #######################
"%APP_PATH%" night, 1, 2, 3, 4
"%APP_PATH%" night, 1, 2, 2, 4
"%APP_PATH%" night, 1, 2, 3, 5
"%APP_PATH%" night, 1, 1, 2, 3, 5

PAUSE