@echo off
set CURRENT_DIR=%cd%
echo %CURRENT_DIR%
sc create "RoomSearch Collector" start= auto binPath= "C:\Program Files (x86)\HTCompany\RoomSearch.CollectorEngine.Installer\RoomSearch.CollectorEngine.exe"
sc start "RoomSearch Collector"