@echo off
setlocal

set /p DB_USER=Enter the database user: 
set /p DB_PASSWORD=Enter the database password: 
set DATABASE = smartware

set "ROOTFOLDER=c:\Program Files"

set "FILE=mysql.exe"

for /r "%ROOTFOLDER%" %%f in (*%FILE%*) do (
    set "MYSQL_PATH=%%f"
    goto :FileTrovato
)
echo The mysqlClient "%MYSQL_PATH%" was not found.
pause
exit /b

:FileTrovato
echo mysql client found: %MYSQL_PATH%
cd /d "%~dp0"


"%MYSQL_PATH%" -u %DB_USER% -p%DB_PASSWORD% < "%~dp0\smartWare_CreateDB.sql"
"%MYSQL_PATH%" -u %DB_USER% -p%DB_PASSWORD% -e "USE smartware"

echo Database and user created correctly.

echo  Running the table insertion script... 
"%MYSQL_PATH%" -u smartware -psmartware smartware < "%~dp0\smartWare_CreateTables.sql"
echo Table insertion script completed.

REM del config.cnf
pause
endlocal
